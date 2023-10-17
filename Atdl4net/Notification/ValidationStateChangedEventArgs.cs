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
    /// Event argument that provides information about a change in validation state.
    /// </summary>
    public class ValidationStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the validity - true if valid; false otherwise.
        /// </summary>
        public bool IsValid { get; private set; }

        /// <summary>
        /// Gets the Id of the affected control.
        /// </summary>
        public string ControlId { get; private set; }

        /// <summary>
        /// Initializes a new <see cref="ValidationStateChangedEventArgs"/> instance.
        /// </summary>
        /// <param name="isValid">Validation state - true if valid; false otherwise.</param>
        /// <param name="controlId">Id of the affected control.</param>
        public ValidationStateChangedEventArgs(string controlId, bool isValid)
        {
            ControlId = controlId;
            IsValid = isValid;
        }
    }
}
