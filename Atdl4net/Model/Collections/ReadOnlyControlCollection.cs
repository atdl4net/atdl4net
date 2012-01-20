#region Copyright (c) 2010-2012, Cornerstone Technology Limited. http://atdl4net.org
//
//   This software is released under both commercial and open-source licenses.
//
//   If you received this software under the commercial license, the terms of that license can be found in the
//   Commercial.txt file in the Licenses folder.  If you received this software under the open-source license,
//   the following applies:
//
//      This file is part of Atdl4net.
//
//      Atdl4net is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public 
//      License as published by the Free Software Foundation, either version 2.1 of the License, or (at your option) any later version.
// 
//      Atdl4net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
//      of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU Lesser General Public License for more details.
//
//      You should have received a copy of the GNU Lesser General Public License along with Atdl4net.  If not, see
//      http://www.gnu.org/licenses/.
//
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Fix;
using Atdl4net.Model.Elements;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Resources;
using Atdl4net.Utility;
using Atdl4net.Validation;
using Common.Logging;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Collections
{
    public class ReadOnlyControlCollection : IParentable<Strategy_t>, IEnumerable<Control_t>, ISimpleDictionary<Control_t>
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Model.Collections");

        private Strategy_t _owner;
        private readonly Dictionary<string, Control_t> _controls = new Dictionary<string, Control_t>();

        public ReadOnlyControlCollection(Strategy_t owner)
        {
            _owner = owner;
        }

        internal void SourceCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Control_t item in e.NewItems)
                    {
                        if (_controls.ContainsKey(item.Id))
                            throw ThrowHelper.New<DuplicateKeyException>(this, ErrorMessages.AttemptToAddDuplicateKey, item.Id, "Controls");

                        _controls.Add(item.Id, item);
                    }
                    break;

                // MSDN documentation says helpfully: "The content of the collection changed dramatically."
                case NotifyCollectionChangedAction.Reset:
                    _controls.Clear();
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (Control_t item in e.OldItems)
                    {
                        if (_controls.ContainsKey(item.Id))
                            _controls.Remove(item.Id);
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    for (int n = 0; n < e.OldItems.Count; n++)
                    {
                        _controls[((Control_t)e.OldItems[n]).Id] = (Control_t)e.NewItems[n];
                    }
                    break;
            }
        }

        public bool Contains(string controlId)
        {
            return _controls.ContainsKey(controlId);
        }

        public Control_t this[string controlId]
        {
            get
            {
                Control_t value;

                if (_controls.TryGetValue(controlId, out value))
                    return value;
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads the initial values for each control based on the InitPolicy, InitFixField and InitValue attributes.
        /// </summary>
        /// <param name="controlInitValueProvider">Value provider for initializing control values from InitFixField.</param>
        /// <remarks>The spec states: 'If the value of the initPolicy attribute is undefined or equal to "UseValue" and the initValue attribute is 
        /// defined then initialize with initValue.  If the value is equal to "UseFixField" then attempt to initialize with the value of 
        /// the tag specified in the initFixField attribute. If the value is equal to "UseFixField" and it is not possible to access the 
        /// value of the specified fix tag then revert to using initValue. If the value is equal to "UseFixField", the field is not accessible,
        /// and initValue is not defined, then do not initialize.</remarks>
        public void LoadDefaults(FixFieldValueProvider controlInitValueProvider)
        {
            Control_t control = null;

            try
            {
                // Two passes necessary - first one to set up all the values...
                foreach (Control_t thisControl in this)
                {
                    control = thisControl;

                    thisControl.LoadInitValue(controlInitValueProvider);
                }
            }
            catch (System.Exception ex)
            {
                throw ThrowHelper.Rethrow(this, ex, ErrorMessages.InitControlValueError, control != null ? control.Id : "(unknown)");
            }

            // ... and the second to update all the StateRules based on the state of all values.
            foreach (Control_t thisControl in this)
                thisControl.StateRules.EvaluateAll();
        }

        /// <summary>
        /// Updates the parameter values from the controls in this control collection.
        /// </summary>
        /// <param name="parameters">Collection of parameters to be updated.</param>
        /// <param name="shortCircuit">If true, this method returns as soon as any error is found; if false, an attempt is made to update all parameter
        /// values before the method returns.</param>
        /// <param name="validationResults">If one or more validations fail, this parameter contains a list of ValidationResults; null otherwise.</param>
        public bool TryUpdateParameterValues(ParameterCollection parameters, bool shortCircuit, out IList<ValidationResult> validationResults)
        {
            bool isValid = true;
            validationResults = null;

            foreach (Control_t control in this)
            {
                string parameter = control.ParameterRef;

                if (parameter != null)
                {
                    if (!parameters.Contains(parameter))
                        throw ThrowHelper.New<ReferencedObjectNotFoundException>(this, ErrorMessages.UnresolvedParameterRefError, parameter);

                    ValidationResult result = parameters[parameter].SetValueFromControl(control);

                    if (!result.IsValid)
                    {
                        if (validationResults == null)
                            validationResults = new List<ValidationResult>();

                        validationResults.Add(result);

                        if (shortCircuit)
                            return false;

                        isValid = false;
                    }
                }
            }

            return isValid;
        }

        /// <summary>
        /// Updates the values of each control from its respective parameter.
        /// </summary>
        /// <param name="parameters">Parameter collection.</param>
        public void UpdateValuesFromParameters(ParameterCollection parameters)
        {
            foreach (Control_t control in this)
            {
                bool hasParameterRef = control.ParameterRef != null;
                bool isValidParameter = hasParameterRef && parameters.Contains(control.ParameterRef);
                IParameter parameter = isValidParameter ? parameters[control.ParameterRef] : null;
                object parameterValue = isValidParameter ? parameter.GetCurrentValue() : null;

                if (hasParameterRef && !isValidParameter)
                    throw ThrowHelper.New<ReferencedObjectNotFoundException>(this, ErrorMessages.UnresolvedParameterRefError, control.ParameterRef);

                // We only want to update the control value if the parameter has a value
                if (parameterValue != null)
                {
                    _log.Debug(m => m("Updating control {0} value from parameter {1}", control.Id, parameter.Name));

                    control.SetValueFromParameter(parameter);
                }
            }

            foreach (Control_t control in this)
                control.StateRules.EvaluateAll();
        }

        /// <summary>
        /// Resets every control in this collection to its empty state.
        /// </summary>
        public void ResetAll()
        {
            foreach (Control_t control in this)
                control.Reset();
        }

        /// <summary>
        /// Resolves all the dependencies between each control's StateRules and their dependent control values.
        /// </summary>
        public void ResolveAll()
        {
            foreach (Control_t control in this)
                control.StateRules.ResolveAll(_owner);
        }

        #region IParentable<Strategy_t> Members

        /// <summary>
        /// Gets/sets the parent/owner of this control collection.
        /// </summary>
        Strategy_t IParentable<Strategy_t>.Parent
        {
            get { return _owner; }
            set { _owner = value; }
        }

        #endregion

        #region IEnumerable<Control_t> Members

        IEnumerator<Control_t> IEnumerable<Control_t>.GetEnumerator()
        {
            foreach (Control_t control in _controls.Values)
            {
                yield return control;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Control_t>)this).GetEnumerator();
        }

        #endregion IEnumerable<Control_t> Members
    }
}
