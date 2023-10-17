#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
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
