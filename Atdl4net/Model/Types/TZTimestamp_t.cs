#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
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
