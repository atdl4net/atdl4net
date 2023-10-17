#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
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
