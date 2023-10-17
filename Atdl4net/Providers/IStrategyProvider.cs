#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using Atdl4net.Model.Elements;
using Atdl4net.Notification;

namespace Atdl4net.Providers
{
    public interface IStrategyProvider : INotifyStrategyLoaded
    {
        StrategyInfoCollection AllStrategies { get; }

        Strategy_t this[StrategyKey key] { get; }

        ProviderIdCollection GetProviders();
    }
}
