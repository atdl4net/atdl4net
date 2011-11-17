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

using Atdl4net.Resources;
using System;
using System.Globalization;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Types
{
    public abstract class UTCDateTime : NonEnumableValueType<DateTime>
    {
        public const string FixTimeFormat = "yyyyMMdd-HH:mm:ss";
        public const string FixTimeFormatMs = "yyyyMMdd-HH:mm:ss.fff";

        public DateTime? MaxValue { get; set; }
        public DateTime? MinValue { get; set; }

        protected override DateTime? ValidateValue(DateTime? value)
        {
            if (MaxValue != null && value != null && (DateTime)value > MaxValue)
                throw ThrowHelper.New<ArgumentOutOfRangeException>(this, ErrorMessages.MaxValueExceeded, ((DateTime)value).ToShortTimeString(), MaxValue);

            if (MinValue != null && value != null && (DateTime)value < MinValue)
                throw ThrowHelper.New<ArgumentOutOfRangeException>(this, ErrorMessages.MinValueExceeded, ((DateTime)value).ToShortTimeString(), MinValue);

            return value;
        }

        protected override DateTime? ConvertFromString(string value)
        {
            string format;

            if (value == null)
                return null;

            if (value.Length == FixTimeFormat.Length)
                format = FixTimeFormat;
            else if (value.Length == FixTimeFormatMs.Length)
                format = FixTimeFormatMs;
            else
                throw ThrowHelper.New<ArgumentException>(this, ErrorMessages.InvalidFIXTimeFormat, value);

            try
            {
                return DateTime.ParseExact(value, format, CultureInfo.InvariantCulture);
            }
            catch (FormatException ex)
            {
                throw ThrowHelper.New<ArgumentException>(this, ex, ErrorMessages.InvalidFIXTimeFormat, value);
            }
        }

        protected override string ConvertToString(DateTime? value)
        {
            return value != null ? ((DateTime)value).ToString(FixTimeFormat) : null;
        }
    }
}
