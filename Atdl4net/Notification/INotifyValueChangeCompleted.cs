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
    /// Interface that types must implement to be able to notify of value change completed events.  Change completed
    /// events are raised once a control's change in value has been applied to any StateRules it affects.
    /// </summary>
    public interface INotifyValueChangeCompleted
    {
        /// <summary>
        /// Raised when a value change has been completed.
        /// </summary>
        event EventHandler<ValueChangeCompletedEventArgs> ValueChangeCompleted;
    }
}
