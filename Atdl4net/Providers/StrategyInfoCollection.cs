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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Atdl4net.Providers
{
    public class StrategyInfoCollection : ObservableCollection<StrategyInfo>
    {
        private readonly Dictionary<StrategyKey, StrategyInfo> _strategyDictionary = new Dictionary<StrategyKey, StrategyInfo>();

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (StrategyInfo item in e.NewItems)
                        _strategyDictionary.Add(item.StrategyKey, item);
                    break;

                // MSDN documentation says helpfully: "The content of the collection changed dramatically."
                case NotifyCollectionChangedAction.Reset:
                    _strategyDictionary.Clear();
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (StrategyInfo item in e.OldItems)
                        _strategyDictionary.Remove(item.StrategyKey);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    for (int n = 0; n < e.OldItems.Count; n++)
                        _strategyDictionary[(e.OldItems[n] as StrategyInfo).StrategyKey] = e.NewItems[n] as StrategyInfo;
                    break;
            }

            base.OnCollectionChanged(e);
        }

        public StrategyInfo this[StrategyKey key]
        {
            get
            {
                return _strategyDictionary[key];
            }
        }

        public void Remove(string providerId)
        {
            List<StrategyInfo> itemsToRemove = new List<StrategyInfo>();

            foreach (StrategyInfo si in Items)
            {
                if (si.ProviderId == providerId)
                    itemsToRemove.Add(si);
            }

            foreach (StrategyInfo si in itemsToRemove)
                base.Remove(si);
        }
    }
}
