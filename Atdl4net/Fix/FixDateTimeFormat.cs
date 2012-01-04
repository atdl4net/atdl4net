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

namespace Atdl4net.Fix
{
    /// <summary>
    /// Provides definitions of the different date and time formats supported by FIX in .NET DateTime.ToString()-
    /// compatible format.
    /// </summary>
    public static class FixDateTimeFormat
    {
        /// <summary>Date and time (no milliseconds).</summary>
        public readonly static string FixDateTime = "yyyyMMdd-HH:mm:ss";

        /// <summary>Date and time (with milliseconds).</summary>
        public readonly static string FixDateTimeMs = "yyyyMMdd-HH:mm:ss.fff";

        /// <summary>Time only (no milliseconds).</summary>
        public readonly static string FixTimeOnly = "HH:mm:ss";

        /// <summary>Time only (with milliseconds).</summary>
        public readonly static string FixTimeOnlyMs = "HH:mm:ss.fff";

        /// <summary>Date only.</summary>
        public readonly static string FixDateOnly = "yyyyMMdd";

        /// <summary>Date and time with appended time zone information.</summary>
        public readonly static string FixDateTimeWithTz = "yyyyMMdd-HH:mm:ssK";

        /// <summary>Date and time with appended time zone information.</summary>
        public readonly static string FixTimeOnlyWithTz = "HH:mm:ssK";

        /// <summary>
        /// Gets an array containing all the FIX date/time formats.
        /// </summary>
        public static string[] AllFormats { get { return _allFormats; } }

        private static readonly string[] _allFormats = new string[] 
        { 
            FixDateTime,
            FixDateTimeMs,
            FixTimeOnly,
            FixTimeOnlyMs,
            FixDateOnly,
            FixDateTimeWithTz,
            FixTimeOnlyWithTz
        };
    }
}
