#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using Atdl4net.Model.Enumerations;

namespace Atdl4net.Providers
{
    public class StrategyInfo
    {
        public StrategyKey StrategyKey { get; private set; }
        public string ProviderId { get; private set; }
        public string StrategyName { get; private set; }
        public string UiRep { get; private set; }
        public Region ApplicableRegions { get; private set; }

        public StrategyInfo(string providerId, string strategyName, string uiRep, Region applicableRegions)
        {
            ProviderId = providerId;
            StrategyName = strategyName;
            UiRep = uiRep;
            ApplicableRegions = applicableRegions;

            StrategyKey = new StrategyKey();
        }
    }
}
