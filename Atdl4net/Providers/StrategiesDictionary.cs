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

using Atdl4net.Model.Elements;
using System.Collections.Generic;

namespace Atdl4net.Providers
{
    public class StrategiesDictionary : Dictionary<string, Strategies_t>
    {
        private readonly StrategyInfoCollection _allStrategies = new StrategyInfoCollection();

        public StrategyInfoCollection AllStrategies 
        {
            get
            {
                return _allStrategies;
            }
        }

        public new void Add(string providerId, Strategies_t strategies)
        {
            foreach (Strategy_t strategy in strategies)
                _allStrategies.Add(new StrategyInfo(providerId, strategy.Name, strategy.UiRep, strategy.Regions.GetApplicableRegions()));

            base.Add(providerId, strategies);
        }

        public new void Remove(string providerId)
        {
            _allStrategies.Remove(providerId);

            base.Remove(providerId);
        }

        public new Strategies_t this[string providerId]
        {
            get { return base[providerId]; }

            set
            {
                if (base.ContainsKey(providerId))
                    Remove(providerId);

                Add(providerId, value);
            }
        }

        public Strategy_t this[StrategyKey key]
        {
            get 
            {             
                StrategyInfo info = _allStrategies[key];

                Strategies_t strategies = this[info.ProviderId];

                return strategies[info.StrategyName];
            }
        }
    }
}
