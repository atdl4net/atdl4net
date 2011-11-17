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
    /// <summary>
    /// 'Sequence of digits with optional decimal point and sign character (ASCII characters "-", "0" - "9" and "."); the
    /// absence of the decimal point within the string will be interpreted as the float representation of an integer value.
    /// All float fields must accommodate up to fifteen significant digits. The number of decimal places used should be a
    /// factor of business/market needs and mutual agreement between counterparties. Note that float values may contain
    /// leading zeros (e.g. "00023.23" = "23.23") and may contain or omit trailing zeros after the decimal point 
    /// (e.g. "23.0" = "23.0000" = "23" = "23.").
    /// Note that fields which are derived from float may contain negative values unless explicitly specified otherwise.'
    /// </summary>
    public class Float_t : NonEnumableValueType<decimal>
    {
        /// <summary>
        /// Gets or sets the maximum value for this parameter.<br/>
        /// Maximum value of the parameter accepted by the algorithm provider.
        /// </summary>
        /// <value>The maximum value.</value>
        public decimal? MaxValue { get; set; }

        /// <summary>
        /// Gets or sets the minimum value for this parameter.<br/>
        /// Minimum value of the parameter accepted by the algorithm provider.
        /// </summary>
        /// <value>The minimum value.</value>
        public decimal? MinValue { get; set; }

        public int? Precision { get; set; }

        protected override decimal? ValidateValue(decimal? value)
        {
            if (MaxValue != null && value != null && (decimal)value > MaxValue)
                throw ThrowHelper.New<ArgumentOutOfRangeException>(this, ErrorMessages.MaxValueExceeded, value, MaxValue);

            if (MinValue != null && value != null && (decimal)value < MinValue)
                throw ThrowHelper.New<ArgumentOutOfRangeException>(this, ErrorMessages.MinValueExceeded, value, MinValue);

            return value;
        }

        protected override decimal? ConvertFromString(string value)
        {
            return Convert.ToDecimal(value, CultureInfo.InvariantCulture);
        }

        protected override string ConvertToString(decimal? value)
        {
            if (value == null)
                return null;

            if (Precision == null)
                return ((decimal)value).ToString(CultureInfo.InvariantCulture);

            string format = string.Format("F{0}", Precision);

            return ((decimal)value).ToString(format, CultureInfo.InvariantCulture);
        }
    }
}
