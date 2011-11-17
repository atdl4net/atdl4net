#region Copyright (c) 2010-2011, Cornerstone Technology Limited. http://atdl4net.org
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

using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Model.Controls;
using Atdl4net.Model.Elements;
using Atdl4net.Resources;
using Atdl4net.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Collections
{
    public class ReadOnlyControlCollection : IParentable<Strategy_t>, IEnumerable<Control_t>, IDictionary<Control_t>
    {
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

        public void LoadDefaults()
        {
            Control_t control = null;

            try
            {
                // Two passes necessary - first one to set up all the values...
                foreach (Control_t thisControl in this)
                {
                    control = thisControl;

                    thisControl.LoadDefault();
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

        public void UpdateValuesFromParameters(ParameterCollection parameters)
        {
            foreach (Control_t control in this)
            {
                try
                {
                    control.LoadDefault();

                    if (control.ParameterRef != null && parameters[control.ParameterRef].ControlValue != null)
                    {
                        IParameter_t sourceParameter = parameters[control.ParameterRef];

                        object value = ParameterValueConverter.Convert(sourceParameter, control);

                        control.SetValue(value);
                    }
                }
                catch (KeyNotFoundException ex)
                {
                    throw ThrowHelper.New<ReferencedObjectNotFoundException>(this, ex, ErrorMessages.UnresolvedParameterRefError, control.ParameterRef);
                }
            }

            foreach (Control_t thisControl in this)
                thisControl.StateRules.EvaluateAll();
        }

        public void UpdateParameterValues(ParameterCollection parameters)
        {
            foreach (Control_t control in this)
            {
                try
                {
                    if (control.ParameterRef != null)
                    {
                        IParameter_t targetParameter = parameters[control.ParameterRef];

                        targetParameter.ControlValue = ParameterValueConverter.Convert(control, targetParameter);
                    }
                }
                catch (FormatException ex)
                {
                    IParameter_t targetParameter = parameters[control.ParameterRef];

                    throw ThrowHelper.New<InvalidCastException>(this, ex, ErrorMessages.DataConversionError, control.GetValue(), targetParameter.Type, targetParameter.Name);
                }
                catch (InvalidCastException ex)
                {
                    IParameter_t targetParameter = parameters[control.ParameterRef];

                    throw ThrowHelper.New<InvalidCastException>(this, ex, ErrorMessages.DataConversionError, control.GetValue(), targetParameter.Type, targetParameter.Name);
                }
                catch (KeyNotFoundException ex)
                {
                    throw ThrowHelper.New<ReferencedObjectNotFoundException>(this, ex, ErrorMessages.UnresolvedParameterRefError, control.ParameterRef);
                }
            }
        }

        #region IParentable<Strategy_t> Members

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
