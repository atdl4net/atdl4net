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

using System;
using System.Collections.ObjectModel;
using Atdl4net.Diagnostics;
using Atdl4net.Model.Elements;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Model.Enumerations;
using Atdl4net.Resources;
using Atdl4net.Utility;
using Common.Logging;

namespace Atdl4net.Model.Collections
{
    /// <summary>
    /// Collection used to store typed instances of Edit_t, either for validating parameters via StrategyEdit, or 
    /// for implementing StateRules using control values.  This collection also provides the ability to evaluate the Edits that
    /// it contains.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EditEvaluatingCollection<T> : Collection<IEdit<T>>, IResolvable<Strategy_t, T>
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Model.Collections");

        private bool _currentState;

        public LogicOperator_t? LogicOperator { get; set; }

        public bool CurrentState { get { return _currentState; } }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public new void Add(IEdit<T> item)
        {
            base.Add(item);

            _log.Debug(m=>m("Edit_t {0} added to EditEvaluatingCollection", item.ToString()));
        }

        /// <summary>
        /// Evaluates this instance.
        /// </summary>
        public void Evaluate()
        {
            _log.Debug(m=>m("Evaluating EditEvaluatingCollection with {0} elements; current state = {1}", this.Count, _currentState.ToString().ToLower()));

            if (LogicOperator == null)
                throw ThrowHelper.New<InvalidOperationException>(this, ErrorMessages.MissingLogicalOperatorOnSetOfEdits);

            bool shortCircuit = false;
            bool newState = (LogicOperator == LogicOperator_t.And) ? true : false;
            int xorCount = 0;

            foreach (IEdit<T> item in this.Items)
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

                    // From the spec: "As a convention we define XOR as 'one and only one', which means it evaluates to true when one
                    // and only one of its operands is true. If none or more than one of its operands is true then XOR is false."
                    case LogicOperator_t.Xor:
                        if (item.CurrentState)
                            xorCount++;
                        newState = xorCount == 1;
                        break;
                }

                _log.Debug(m => m("EditEvaluatingCollection state is now {0}", newState.ToString().ToLower()));
            }

            _currentState = newState;
        }

        #region IResolvable<Strategy_t, T> Members

        // TODO: Unbind needed?
        void IResolvable<Strategy_t, T>.Resolve(Strategy_t strategy, ISimpleDictionary<T> sourceCollection)
        {
            foreach (IEdit<T> item in this.Items)
            {
                (item as IResolvable<Strategy_t, T>).Resolve(strategy, sourceCollection);
            }
        }

        #endregion IResolvable<Strategy_t> Members
    }
}
