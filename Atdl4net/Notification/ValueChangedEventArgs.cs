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
    /// Event argument for use with control value change notifications.
    /// </summary>
    public class ValueChangedEventArgs : EventArgs
    {
        /// <summary>The ID for the control whose value has changed.</summary>
        public string Id { get; private set; }

        /// <summary>The ID for the control whose value has changed.</summary>
        public object OldValue { get; private set; }

        /// <summary>The ID for the control whose value has changed.</summary>
        public object NewValue { get; private set; }

        /// <summary>
        /// Initializes a new <see cref="ValueChangedEventArgs"/> instance with the supplied values.
        /// </summary>
        /// <param name="id">Control ID that this event relates to.</param>
        /// <param name="oldValue">Old (previous) value of this control.</param>
        /// <param name="newValue">New value of this control.</param>
        public ValueChangedEventArgs(string id, object oldValue, object newValue)
        {
            Id = id;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
