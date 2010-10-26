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
using Atdl4net.Model.Elements;
using Atdl4net.Model.Enumerations;
using Atdl4net.Resources;
using Atdl4net.Utility;
using System;
using System.Collections.ObjectModel;

namespace Atdl4net.Model.Collections
{
    /// <summary>
    /// Collection used to store typed instances of Edit_t, either for validating parameters via StrategyEdit, or 
    /// for implementing StateRules using control values.  This collection also provides the ability evaluate the Edits that
    /// it contains.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EditEvaluatingCollection<T> : Collection<IEdit_t<T>>, IResolvable<Strategy_t, T>, IKeyedObject
    {
        private bool _currentState;

        public LogicOperator_t? LogicOperator { get; set; }

        public bool CurrentState { get { return _currentState; } }

        public EditEvaluatingCollection()
        {
            (this as IKeyedObject).RefKey = RefKeyGenerator.GetNextKey(typeof(EditEvaluatingCollection<T>));

            Logger.DebugFormat("New EditEvaluatingCollection created as EditEvaluatingCollection[{0}].", (this as IKeyedObject).RefKey);
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public new void Add(IEdit_t<T> item)
        {
            base.Add(item);

            Logger.DebugFormat("IEdit_t[{0}] added to EditEvaluatingCollection[{1}].",
                (item as IKeyedObject).RefKey, (this as IKeyedObject).RefKey);
        }

        /// <summary>
        /// Evaluates this instance.
        /// </summary>
        public void Evaluate()
        {
            Logger.DebugFormat("Evaluating EditEvaluatingCollection[{0}] with {1} elements; current state = {2}.",
                (this as IKeyedObject).RefKey, this.Count, _currentState);

            if (LogicOperator == null)
                throw ThrowHelper.New<InvalidOperationException>(this, ErrorMessages.MissingLogicalOperatorOnSetOfEdits);

            bool shortCircuit = false;
            bool newState = (LogicOperator == LogicOperator_t.And) ? true : false;

            foreach (IEdit_t<T> item in this.Items)
            {
                if (shortCircuit)
                    break;

                item.Evaluate();

                switch (LogicOperator)
                {
                    case LogicOperator_t.And:
                        newState &= item.CurrentState;
                        if (!newState)
                            shortCircuit = true;
                        break;

                    case LogicOperator_t.Or:
                        newState |= item.CurrentState;
                        if (newState)
                            shortCircuit = true;
                        break;

                    case LogicOperator_t.Not:
                        newState = !item.CurrentState;
                        break;

                    case LogicOperator_t.Xor:
                        newState ^= item.CurrentState;
                        break;
                }
            }

            Logger.DebugFormat("Evaluation of EditEvaluatingCollection[{0}] yielded {1}.",
                (this as IKeyedObject).RefKey, newState);

            _currentState = newState;
        }

        #region IResolvable<Strategy_t, T> Members

        // TODO: Unbind needed?
        void IResolvable<Strategy_t, T>.Resolve(Strategy_t strategy, IDictionary<T> sourceCollection)
        {
            foreach (IEdit_t<T> item in this.Items)
            {
                (item as IResolvable<Strategy_t, T>).Resolve(strategy, sourceCollection);
            }
        }

        #endregion IResolvable<Strategy_t> Members

        #region IKeyedObject Members

        string IKeyedObject.RefKey { get; set; }

        #endregion IKeyedObject Members
    }
}
