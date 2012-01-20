#region Copyright (c) 2010-2012, Cornerstone Technology Limited. http://atdl4net.org
//
//   This software is released under both commercial and open-source licenses.
//
//   If you received this software under the commercial license, the terms of that license can be found in the
//   Commercial.txt file in the Licenses folder.  If you received this software under the open-source license,
//   the following applies:
//
//      This file is part of Atdl4net.
//
//      Atdl4net is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public 
//      License as published by the Free Software Foundation, either version 2.1 of the License, or (at your option) any later version.
// 
//      Atdl4net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
//      of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU Lesser General Public License for more details.
//
//      You should have received a copy of the GNU Lesser General Public License along with Atdl4net.  If not, see
//      http://www.gnu.org/licenses/.
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
