#region Copyright (c) 2010-2011, Cornerstone Technology Limited. http://atdl4net.org
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

namespace Atdl4net.Model.Types
{
    /// <summary>
    /// 'string field representing Time/date combination represented in UTC (Universal Time Coordinated, also known as "GMT") 
    /// in either YYYYMMDD-HH:MM:SS (whole seconds) or YYYYMMDD-HH:MM:SS.sss (milliseconds) format, colons, dash, and period required.
    /// Valid values:
    /// * YYYY = 0000-9999, MM = 01-12, DD = 01-31, HH = 00-23, MM = 00-59, SS = 00-60 (60 only if UTC leap second) (without milliseconds).
    /// * YYYY = 0000-9999, MM = 01-12, DD = 01-31, HH = 00-23, MM = 00-59, SS = 00-60 (60 only if UTC leap second), sss=000-999 (indicating 
    /// milliseconds).
    /// Leap Seconds: Note that UTC includes corrections for leap seconds, which are inserted to account for slowing of the rotation of the
    /// earth. Leap second insertion is declared by the International Earth Rotation Service (IERS) and has, since 1972, only occurred on the
    /// night of Dec. 31 or Jun 30. The IERS considers March 31 and September 30 as secondary dates for leap second insertion, but has never
    /// utilized these dates. During a leap second insertion, a UTCTimestamp field may read "19981231-23:59:59", "19981231-23:59:60", 
    /// "19990101-00:00:00". (see http://tycho.usno.navy.mil/leapsec.html)'
    /// </summary>
    public class UTCTimestamp_t : DateTimeTypeBase
    {
        /// <summary>Gets or sets the local market timezone.<br/>
        /// Describes the time zone without indicating whether daylight savings is in effect. Valid values are taken from 
        /// names in the Olson time zone database. All are of the form Area/Location, where Area is the name of a continent 
        /// or ocean, and Location is the name of a specific location within that region. E.g. Americas/Chicago.
        /// Applicable when xsi:type is UTCTimestamp_t.</summary>
        /// <value>The local market timezone.</value>
        public string LocalMktTz { get; set; }

        private static readonly string[] _formatStrings = new string[] { FixDateTimeFormat.FixDateTime, FixDateTimeFormat.FixDateTimeMs };

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
    }
}
