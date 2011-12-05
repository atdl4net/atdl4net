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
using System.Globalization;
using Atdl4net.Diagnostics;
using Atdl4net.Resources;

namespace Atdl4net.Model.Types
{
    /// <summary>
    /// 'float field representing a percentage (e.g. 0.05 represents 5% and 0.9525 represents 95.25%). Note the number of 
    /// decimal places may vary.'
    /// </summary>
    public class Percentage_t : Float_t
    {
        /// <summary>
        /// Applicable for xsi:type of Percentage_t. If true then percent values must be multiplied by 100 before being
        /// sent on the wire. For example, if multiplyBy100 were false then the percentage, 75%, would be sent as 0.75 
        /// on the wire. However, if multiplyBy100 were true then 75 would be sent on the wire.
        /// If not provided it should be interpreted as false.
        /// Use of this attribute is not recommended. The motivation for this attribute is to maximize compatibility 
        /// with algorithmic interfaces that are non-compliant with FIX in regard to their handling of percentages. In
        /// these cases an integer parameter should be used instead of a percentage.
        /// </summary>
        public bool? MultiplyBy100 { get; set; }

        /// <summary>
        /// Validates the supplied value in terms of the parameters constraints (e.g., MinValue, MaxValue, etc.).
        /// </summary>
        /// <param name="value">Value to validate, may be null in which case no validation is applied.</param>
        /// <returns>Value passed in is returned if it is valid; otherwise an appropriate exception is thrown.</returns>
        protected override decimal? ValidateValue(decimal? value)
        {
            if (value != null)
            {
                decimal wireValue = (MultiplyBy100 != true) ? (decimal)value / 100 : (decimal)value;

                if (MaxValue != null && wireValue > MaxValue)
                    throw ThrowHelper.New<ArgumentOutOfRangeException>(this, ErrorMessages.MaxValueExceeded, wireValue, MaxValue);

                if (MinValue != null && wireValue < MinValue)
                    throw ThrowHelper.New<ArgumentOutOfRangeException>(this, ErrorMessages.MinValueExceeded, wireValue, MinValue);
            }

            return value;
        }

        /// <summary>
        /// Converts the supplied value from string format (as might be used on the FIX wire) into the type of the type
        /// parameter for this type.  This implementation adjusts for the fact that percentage values are typically
        /// shown as whole numbers (5, 10, 15) on the user interface but sent over the FIX wire as decimals (0.05, 0.1, 0.15).
        /// </summary>
        /// <param name="value">Type to convert from string; cannot be null as empty fields are invalid in FIX.</param>
        /// <returns>Value converted from a string.</returns>
        protected override decimal? ConvertFromWireValueFormat(string value)
        {
            decimal? decimalValue = base.ConvertFromWireValueFormat(value);

            return (MultiplyBy100 != true) ? (decimal)decimalValue : (decimal)decimalValue * 100;
        }

        /// <summary>
        /// Converts the supplied value to a string, as might be used on the FIX wire.  If the supplied value is
        /// null, this means the field is not to be included in the outgoing FIX message.  This implementation adjusts for the 
        /// fact that percentage values are typically shown as whole numbers (5, 10, 15) on the user interface but sent over 
        /// the FIX wire as decimals (0.05, 0.1, 0.15).
        /// </summary>
        /// <param name="value">Value to convert, may be null.</param>
        /// <returns>If input value is not null, returns value converted to a string; null otherwise.</returns>
        protected override string ConvertToWireValueFormat(decimal? value)
        {
            if (value == null)
                return null;

            decimal adjustedValue = (MultiplyBy100 != true) ? (decimal)value / 100 : (decimal)value;

            if (Precision == null)
                return adjustedValue.ToString(CultureInfo.InvariantCulture);
            else
            {
                string format = string.Format("F{0}", Precision);

                return adjustedValue.ToString(format, CultureInfo.InvariantCulture);
            }
        }
    }
}
