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
using Atdl4net.Fix;
using Atdl4net.Model.Types.Support;
using Atdl4net.Resources;

namespace Atdl4net.Model.Types
{
    /// <summary>
    /// 'string field representing Time-only represented in UTC (Universal Time Coordinated, also known as "GMT") in either HH:MM:SS 
    /// (whole seconds) or HH:MM:SS.sss (milliseconds) format, colons, and period required. This special-purpose field is paired with 
    /// UTCDateOnly to form a proper UTCTimestamp for bandwidth-sensitive messages.
    /// Valid values:
    /// HH = 00-23, MM = 00-60 (60 only if UTC leap second), SS = 00-59. (without milliseconds)
    /// HH = 00-23, MM = 00-59, SS = 00-60 (60 only if UTC leap second), sss=000-999 (indicating milliseconds).'
    /// </summary>
    public class UTCTimeOnly_t : DateTimeTypeBase
    {
        private static readonly string[] _formatStrings = new string[] { FixDateTimeFormat.FixTimeOnly, FixDateTimeFormat.FixTimeOnlyMs };

        /// <summary>
        /// Gets the DateTime format strings to use when converting this date/time to a FIX string and vice versa.
        /// </summary>
        /// <returns>Format strings suitable when calling DateTime.ToString().</returns>
        /// <remarks>When converting from DateTime to string, the first member of the returned array is used.  When
        /// converting from string to DateTime, the member of the array that has the same length as the string
        /// value is used.</remarks>
        protected override string[] GetDateTimeFormatStrings()
        {
            return _formatStrings;
        }

        /// <summary>
        /// Gets the human-readable type name for use in error messages shown to the user.
        /// </summary>
        /// <returns>Human-readable type name.</returns>
        protected override string GetHumanReadableTypeName()
        {
            return HumanReadableTypeNames.TimeType;
        }
    }
}
