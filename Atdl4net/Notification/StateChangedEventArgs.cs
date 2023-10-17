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
    /// Event argument that provides state change information.
    /// </summary>
    public class StateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the old (previous) state that this event relates to.
        /// </summary>
        public bool OldState { get; private set; }

        /// <summary>
        /// Gets the new state that this event relates to.
        /// </summary>
        public bool NewState { get; private set; }

        /// <summary>
        /// Initializes a <see cref=" StateChangedEventArgs"/> with the supplied state values.
        /// </summary>
        /// <param name="oldState">Old (previous) state.</param>
        /// <param name="newState">New state.</param>
        public StateChangedEventArgs(bool oldState, bool newState)
        {
            OldState = oldState;
            NewState = newState;
        }
    }
}
