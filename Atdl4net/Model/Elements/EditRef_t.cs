#region Copyright (c) 2010, Cornerstone Technology Limited. http://atdl4net.org
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
//      License as published by the Free Software Foundation, version 3.
// 
//      Atdl4net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
//      of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU Lesser General Public License for more details.
//
//      You should have received a copy of the GNU Lesser General Public License along with Atdl4net.  If not, see
//      http://www.gnu.org/licenses/.
//
#endregion

using Atdl4net.Diagnostics;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Enumerations;
using Atdl4net.Resources;
using Atdl4net.Utility;

namespace Atdl4net.Model.Elements
{
    /// <summary>
    /// Represents a FIXatdl EditRef_t.
    /// </summary>
    public class EditRef_t<T> : IEdit_t<T>, IResolvable<Strategy_t, T>, IKeyedObject where T : class, IValueProvider
    {
        private Edit_t<T> _referencedEdit;

        public EditRef_t(string id)
        {
            Id = id;

            (this as IKeyedObject).RefKey = RefKeyGenerator.GetNextKey(typeof(EditRef_t<T>));

            Logger.DebugFormat("New EditRef_t created as EditRef[{0}], Id='{1}'.", (this as IKeyedObject).RefKey, id);
        }

        /// <summary>
        /// Refers to an ID of a previously defined edit element. The edit element may be defined at the strategy level or at the strategies level.
        /// </summary>
        public string Id { get; set; }

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

        public object FieldValue
        {
            get { return _referencedEdit.FieldValue; }
        }

        public object Field2Value
        {
            get { return _referencedEdit.Field2Value; }
        }

        public bool CurrentState
        {
            get { return _referencedEdit.CurrentState; }
        }

        public EditEvaluatingCollection<T> Edits
        {
            get { return _referencedEdit.Edits; }
        }

        #endregion

        public void Evaluate()
        {
            _referencedEdit.Evaluate();
        }

        #region IResolvable<Strategy_t> Members

        void IResolvable<Strategy_t, T>.Resolve(Strategy_t strategy, IDictionary<T> sourceCollection)
        {
            if (strategy.Edits.Contains(Id))
            {
                _referencedEdit = strategy.Edits.Clone<T>(Id);

                Logger.DebugFormat("EditRef[{0}], Id='{1}' linked to new Edit[{2}] resolved from Strategy[{3}].",
                    (this as IKeyedObject).RefKey, Id, (_referencedEdit as IKeyedObject).RefKey, (strategy as IKeyedObject).RefKey);
            }
            else
            {
                Strategies_t strategies = (strategy.Parent as Strategies_t);

                if (strategies != null && strategies.Edits.Contains(Id))
                {
                    _referencedEdit = strategies.Edits.Clone<T>(Id);

                    Logger.DebugFormat("EditRef[{0}], Id='{1}' linked to new Edit[{2}] resolved from Strategies level.",
                        (this as IKeyedObject).RefKey, Id, (_referencedEdit as IKeyedObject).RefKey);
                }
                else
                    throw ThrowHelper.New<ReferencedObjectNotFoundException>(this, ErrorMessages.EditRefResolutionFailure, Id);
            }

            (_referencedEdit as IResolvable<Strategy_t, T>).Resolve(strategy, sourceCollection);
        }

        #endregion IBinIResolvabledable<Strategy_t> Members

        #region IKeyedObject Members

        string IKeyedObject.RefKey { get; set; }

        #endregion IKeyedObject Members
    }
}
