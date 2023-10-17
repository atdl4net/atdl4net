#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;

namespace Atdl4net.Notification
{
    /// <summary>
    /// Interface that types must implement in order to be notified of changes in user interface control values.
    /// </summary>
    public interface INotifyValueChanged
    {
        /// <summary>
        /// Raised when a control's value has changed.
        /// </summary>
        event EventHandler<ValueChangedEventArgs> ValueChanged;
    }
}
