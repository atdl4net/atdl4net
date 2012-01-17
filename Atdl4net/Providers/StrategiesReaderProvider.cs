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

using System.Linq;
using Atdl4net.Model.Elements;
using Atdl4net.Notification;
using Atdl4net.Xml;

namespace Atdl4net.Providers
{
    public class StrategiesReaderProvider : IStrategyProvider
    {
        protected readonly StrategiesReader _strategiesReader = new StrategiesReader();
        protected readonly StrategiesDictionary _strategiesDictionary = new StrategiesDictionary();

        public StrategiesReaderProvider()
        {
            _strategiesReader.StrategyLoaded += new System.EventHandler<StrategyLoadedEventArgs>(OnStrategyLoaded);
        }

        public void Load(StreamProviderContext context)
        {
            Strategies_t strategies = _strategiesReader.Load(context.Source);

            _strategiesDictionary[context.ProviderId] = strategies;
        }

        public void Load(FileProviderContext context)
        {
            Strategies_t strategies = _strategiesReader.Load(context.Source);

            _strategiesDictionary[context.ProviderId] = strategies;
        }

        public Strategies_t GetStrategiesByProvider(string providerId)
        {
            return _strategiesDictionary[providerId];
        }

        public Strategy_t GetStrategyByName(string providerId, string name)
        {
            Strategies_t strategies = _strategiesDictionary[providerId];

            return strategies[name];
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
