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
using System.Collections.ObjectModel;
using Atdl4net.Diagnostics;
using Atdl4net.Model.Elements;
using Atdl4net.Utility;
using Common.Logging;

namespace Atdl4net.Model.Collections
{
    public class StateRuleCollection : Collection<StateRule_t>
    {
        private static readonly ILog _log = LogManager.GetLogger("StateRules");

        private Control_t _owner;

        public StateRuleCollection(Control_t owner)
        {
            _owner = owner;
        }

        public new void Add(StateRule_t item)
        {
            (item as IParentable<Control_t>).Parent = _owner;

            base.Add(item);

            _log.DebugFormat("StateRule[{0}] added to StateRules for Control[{1}] Id={2}.",
                (item as IKeyedObject).RefKey, (_owner as IKeyedObject).RefKey, _owner.Id);
        }

        public void EvaluateAll()
        {
            foreach (StateRule_t rule in Items)
                rule.Evaluate();
        }

        /// <summary>
        /// Resolves all edit refs, connects all edits to their controls.
        /// </summary>
        /// <param name="strategy"></param>
        public void ResolveAll(Strategy_t strategy)
        {
            foreach (StateRule_t rule in Items)
                (rule as IResolvable<Strategy_t, Control_t>).Resolve(strategy, strategy.Controls);
        }
    }
}
