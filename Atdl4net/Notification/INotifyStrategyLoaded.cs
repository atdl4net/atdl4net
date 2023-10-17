#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;

namespace Atdl4net.Notification
{
    /// <summary>
    /// Interface that types must implement in order to notify strategy loaded events.
    /// </summary>
    public interface INotifyStrategyLoaded
    {
        /// <summary>
        /// Raised whenever a new strategy has been loaded.
        /// </summary>
        event EventHandler<StrategyLoadedEventArgs> StrategyLoaded;
    }
}
