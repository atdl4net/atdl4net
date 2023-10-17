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
    /// Event argument that provides information about a change of strategy.
    /// </summary>
    public class StrategyChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the strategy name that this event relates to.
        /// </summary>
        public string StrategyName { get; private set; }

        /// <summary>
        /// Initializes a <see cref=" StrategyChangedEventArgs"/> with the supplied name.
        /// </summary>
        /// <param name="strategyName">Strategy name.</param>
        public StrategyChangedEventArgs(string strategyName)
        {
            StrategyName = strategyName;
        }
    }
}
