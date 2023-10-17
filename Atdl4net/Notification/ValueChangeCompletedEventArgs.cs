#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Linq;
using Atdl4net.Wpf.ViewModel;

namespace Atdl4net.Notification
{
    /// <summary>
    /// Event argument that provides information about the completion of a control value change.
    /// </summary>
    public class ValueChangeCompletedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the control that this event relates to.
        /// </summary>
        public ControlViewModel Control { get; private set; }

        /// <summary>
        /// Initializes a new <see cref="ValueChangeCompletedEventArgs"/> with the supplied ControlViewModel.
        /// </summary>
        /// <param name="control">ControlViewModel for the control that this event relates to.</param>
        public ValueChangeCompletedEventArgs(ControlViewModel control)
        {
            Control = control;
        }
    }
}
