#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Linq;

namespace Atdl4net.Notification
{
    /// <summary>
    /// Event argument that provides strategy load information.
    /// </summary>
    public class StrategyLoadedEventArgs : EventArgs
    {
        /// <summary>
        /// Index number of this strategy within the strategy file.
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// Total number of strategies within the strategy file being loaded.
        /// </summary>
        public int Total { get; private set; }

        /// <summary>
        /// Name of the loaded strategy.
        /// </summary>
        public string StrategyName { get; private set; }

        /// <summary>
        /// Initializes a new <see cref="StrategyLoadedEventArgs"/> instance.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="total"></param>
        /// <param name="strategyName"></param>
        public StrategyLoadedEventArgs(int index, int total, string strategyName)
        {
            Index = index;
            Total = total;
            StrategyName = strategyName;
        }
    }
}
