#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
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
