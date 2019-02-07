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

using System.Collections.ObjectModel;
using Atdl4net.Fix;
using Atdl4net.Model.Elements;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Utility;
using Common.Logging;

namespace Atdl4net.Model.Collections
{
    /// <summary>
    /// Collection class of <see cref="StrategyEdit_t">StrategyEdit</see>s.
    /// </summary>
    public class StrategyEditCollection : Collection<StrategyEdit_t>
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Model.Collections");

        /// <summary>
        /// Validates all the <see cref="StrategyEdit_t">StrategyEdit</see>s in this collection.
        /// </summary>
        /// <param name="additionalValues">Any additional FIX field values that may be required in the Edit evaluation.</param>
        /// <param name="shortCircuit">If true, this method returns as soon as any error is found; if false, all StrategyEdits
        /// are evaluated before the method returns.</param>
        public bool EvaluateAll(FixFieldValueProvider additionalValues, bool shortCircuit)
        {
            bool valid = true;

            foreach (StrategyEdit_t strategyEdit in Items)
            {
                strategyEdit.Evaluate(additionalValues);

                if (!strategyEdit.CurrentState)
                {
                    if (shortCircuit)
                        return false;

                    valid = false;
                }
            }

            return valid;
        }

        protected override void InsertItem(int index, StrategyEdit_t item)
        {
            _log.Debug(m => m("StrategyEdit added"));

            base.InsertItem(index, item);
        }

        /// <summary>
        /// Resolves all interdependencies e.g. edits to edit refs, control values to edits, etc.  Called once
        /// all strategies have been loaded as there may be dependencies on EditRefs at the global level.
        /// </summary>
        public void ResolveAll(Strategy_t owningStrategy)
        {
            foreach (StrategyEdit_t strategyEdit in this)
                (strategyEdit as IResolvable<Strategy_t, IParameter>).Resolve(owningStrategy, owningStrategy.Parameters);
        }
    }
}
