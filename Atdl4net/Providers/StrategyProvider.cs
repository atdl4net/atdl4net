#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion
using System.IO;
using System.Linq;
using Atdl4net.Model.Elements;
using Atdl4net.Notification;
using Atdl4net.Xml;

namespace Atdl4net.Providers
{
    public class StrategyProvider : IStrategyProvider
    {
        protected readonly StrategiesReader _strategiesReader = new StrategiesReader();
        protected readonly StrategiesDictionary _strategiesDictionary = new StrategiesDictionary();

        public StrategyProvider()
        {
            _strategiesReader.StrategyLoaded += new System.EventHandler<StrategyLoadedEventArgs>(OnStrategyLoaded);
        }

        public void Load(string providerId, Stream stream)
        {
            Strategies_t strategies = _strategiesReader.Load(stream);

            _strategiesDictionary[providerId] = strategies;
        }

        public void Load(string providerId, string path)
        {
            Strategies_t strategies = _strategiesReader.Load(path);

            _strategiesDictionary[providerId] = strategies;
        }

        public Strategies_t GetStrategiesByProvider(string providerId)
        {
            return _strategiesDictionary[providerId];
        }

        public Strategy_t GetStrategyByName(string providerId, string name, bool resetStrategy)
        {
            Strategies_t strategies = _strategiesDictionary[providerId];

            Strategy_t strategy = strategies[name];

            if (resetStrategy)
                strategy.Reset();

            return strategy;
        }

        #region IStrategyProvider Members

        public StrategyInfoCollection AllStrategies
        {
            get { return _strategiesDictionary.AllStrategies; }
        }

        public Strategy_t this[StrategyKey key]
        {
            get { return key != null ? _strategiesDictionary[key] : null; }
        }

        public ProviderIdCollection GetProviders()
        {
            ProviderIdCollection providers = new ProviderIdCollection();

            foreach (string provider in _strategiesDictionary.Keys)
                providers.Add(provider);

            return providers;
        }

        #endregion

        #region INotifyStrategyLoad Members and Support Methods

        protected virtual void OnStrategyLoaded(object sender, StrategyLoadedEventArgs e)
        {
            System.EventHandler<StrategyLoadedEventArgs> strategyLoaded = StrategyLoaded;

            if (strategyLoaded != null)
                strategyLoaded(this, e);
        }

        public event System.EventHandler<StrategyLoadedEventArgs> StrategyLoaded;

        #endregion
    }
}
