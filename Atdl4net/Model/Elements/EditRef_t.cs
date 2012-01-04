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

using System.Collections.Generic;
using Atdl4net.Diagnostics;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Fix;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Model.Enumerations;
using Atdl4net.Resources;
using Atdl4net.Utility;
using Atdl4net.Validation;
using Common.Logging;

namespace Atdl4net.Model.Elements
{
    /// <summary>
    /// Represents a FIXatdl EditRef_t.
    /// </summary>
    public class EditRef_t<T> : IEdit<T>, IResolvable<Strategy_t, T> where T : class, IValueProvider
    {
        // Use Atdl4net.Validation namespace rather than Atdl4net.Model.Elements for debugging purposes
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Validation");

        private Edit_t<T> _referencedEdit;

        public EditRef_t(string id)
        {
            Id = id;
        }

        /// <summary>
        /// Refers to an ID of a previously defined edit element. The edit element may be defined at the strategy level or at the strategies level.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Provides a string representation of this EditRef_t, primarily for debugging purposes.
        /// </summary>
        /// <returns>String representation of this EditRef_t.</returns>
        public override string ToString()
        {
            return _referencedEdit != null ? _referencedEdit.ToString() : string.Empty;
        }

        /// <summary>
        /// Evaluates this EditRef based on the current field values and any additional FIX field values that this
        /// EditRef references.
        /// </summary>
        /// <param name="additionalValues">Any additional FIX field values that may be required in the Edit evaluation.</param>
        public void Evaluate(FixFieldValueProvider additionalValues)
        {
            _referencedEdit.Evaluate(additionalValues);
        }

        #region IEdit_t Members

        public string Field
        {
            get { return _referencedEdit.Field; }
            set { _referencedEdit.Field = value; }
        }

        public string Field2
        {
            get { return _referencedEdit.Field2; }
            set { _referencedEdit.Field2 = value; }
        }

        public Operator_t? Operator
        {
            get { return _referencedEdit.Operator; }
            set { _referencedEdit.Operator = value; }
        }

        public LogicOperator_t? LogicOperator
        {
            get { return _referencedEdit.LogicOperator; }
            set { _referencedEdit.LogicOperator = value; }
        }

        public string Value
        {
            get { return _referencedEdit.Value; }
            set { _referencedEdit.Value = value; }
        }

        public object FieldValue { get { return _referencedEdit.FieldValue; } }

        public object Field2Value { get { return _referencedEdit.Field2Value; } }

        public bool CurrentState { get { return _referencedEdit.CurrentState; } }

        public EditEvaluatingCollection<T> Edits { get { return _referencedEdit.Edits; } }

        /// <summary>
        /// Gets the set of sources for the data to be evaluated as part of this StrategyEdit.
        /// </summary>
        public HashSet<string> Sources { get { return _referencedEdit.Sources; } }

        #endregion

        #region IResolvable<Strategy_t> Members

        void IResolvable<Strategy_t, T>.Resolve(Strategy_t strategy, ISimpleDictionary<T> sourceCollection)
        {
            if (strategy.Edits.Contains(Id))
            {
                _referencedEdit = strategy.Edits.Clone<T>(Id);

                _log.Debug(m=>m("EditRef Id {0} linked to new Edit_t resolved from Strategy '{1}'", Id, strategy.Name));
            }
            else
            {
                Strategies_t strategies = (strategy.Parent as Strategies_t);

                if (strategies != null && strategies.Edits.Contains(Id))
                {
                    _referencedEdit = strategies.Edits.Clone<T>(Id);

                    _log.Debug(m => m("EditRef Id {0} linked to new Edit_t resolved resolved from Strategies level", Id));
                }
                else
                    throw ThrowHelper.New<ReferencedObjectNotFoundException>(this, ErrorMessages.EditRefResolutionFailure, Id);
            }

            (_referencedEdit as IResolvable<Strategy_t, T>).Resolve(strategy, sourceCollection);
        }

        #endregion IBinIResolvabledable<Strategy_t> Members
    }
}
