#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;

namespace Atdl4net.Notification
{
    /// <summary>
    /// Interface that types must implement in order to provide notification of change of state.
    /// </summary>
    public interface INotifyStateChanged
    {
        /// <summary>
        /// Raised whenever state changes.
        /// </summary>
        event EventHandler<StateChangedEventArgs> StateChanged;
    }
}
