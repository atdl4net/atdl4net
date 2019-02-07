﻿#region Copyright (c) 2010-2012, Cornerstone Technology Limited. http://atdl4net.org
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
