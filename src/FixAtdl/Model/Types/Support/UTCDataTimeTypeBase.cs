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
using System.Globalization;
using System.Linq;
using Atdl4net.Diagnostics;
using Atdl4net.Resources;
using Atdl4net.Validation;

namespace Atdl4net.Model.Types.Support
{
    /// <summary>
    /// Base class for all date and time related UTC-prefixed FIXatdl types.
    /// </summary>
    public abstract class UTCDateTimeTypeBase : DateTimeTypeBase
    {
        #region AtdlReferenceType<string> Overrides

        /// <summary>
        /// Validates the supplied value in terms of the parameters constraints (e.g., MinValue, MaxValue, etc.).
        /// </summary>
        /// <param name="value">Value to validate, may be null in which case no validation is applied.</param>
        /// <param name="isRequired">Set to true to check that this parameter is non-null.</param>
        /// <returns>ValidationResult indicating whether the supplied value is valid.</returns>
        /// <remarks>DateTime.MaxValue (a date and time at the end of the year 9999) is used to indicate an invalid date or time.</remarks>
        protected override ValidationResult ValidateValue(DateTime? value, bool isRequired)
        {
            return base.ValidateValue(GetAdjustedValue(value), isRequired);
        }

        /// <summary>
        /// Converts the supplied value from string format (as might be used on the FIX wire) into the type of the type
        /// parameter for this type.  
        /// </summary>
        /// <param name="value">Type to convert from string, cannot be null.</param>
        /// <returns>Value converted from a string if the conversion succeeded; otherwise an exception is thrown.</returns>
        protected override DateTime? ConvertFromWireValueFormat(string value)
        {
            string[] formats = GetDateTimeFormatStrings();

            DateTime result;

            // Unless instructed otherwise, DateTime.TryParseExact returns a DateTime with Kind = Local
            if (DateTime.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeUniversal, out result))
                return result;

            throw ThrowHelper.New<InvalidCastException>(this, ErrorMessages.InvalidDateOrTimeValue, value);
        }

        /// <summary>
        /// Converts the supplied value to a string, as might be used on the FIX wire.
        /// </summary>
        /// <param name="value">Value to convert, may be null.</param>
        /// <returns>If input value is not null, returns value converted to a string; null otherwise.</returns>
        protected override string ConvertToWireValueFormat(DateTime? value)
        {
            string format = GetDateTimeFormatStrings()[0];
            DateTime? adjustedValue = GetAdjustedValue(value);

            return adjustedValue != null ? ((DateTime)adjustedValue).ToString(format) : null;
        }

        #endregion

        private DateTime? GetAdjustedValue(DateTime? value)
        {
            return value != null ? (DateTime?)((DateTime)value).ToUniversalTime() : null;
        }
    }
}
