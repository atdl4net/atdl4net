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

using Atdl4net.Fix;
using Atdl4net.Model.Types.Support;
using Atdl4net.Resources;

namespace Atdl4net.Model.Types
{
    /// <summary>
    /// 'string field representing a time/date combination representing local time with an offset to UTC to allow identification of 
    /// local time and timezone offset of that time. The representation is based on ISO 8601.
    /// Format is YYYYMMDD-HH:MM:SS[Z | [ + | - hh[:mm]]] where YYYY = 0000 to 9999, MM = 01-12, DD = 01-31 HH = 00-23 hours, 
    /// MM = 00-59 minutes, SS = 00-59 seconds, hh = 01-12 offset hours, mm = 00-59 offset minutes
    /// Example: 20060901-07:39Z is 07:39 UTC on 1st of September 2006
    /// Example: 20060901-02:39-05 is five hours behind UTC, thus Eastern Time on 1st of September 2006
    /// Example: 20060901-15:39+08 is eight hours ahead of UTC, Hong Kong/Singapore time on 1st of September 2006
    /// Example: 20060901-13:09+05:30 is 5.5 hours ahead of UTC, India time on 1st of September 2006'
    /// </summary>
    public class TZTimestamp_t : DateTimeTypeBase
    {
        private static readonly string[] _formatStrings = new string[] { FixDateTimeFormat.FixDateTimeWithTz };

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
            return HumanReadableTypeNames.TimestampType;
        }
    }
}
