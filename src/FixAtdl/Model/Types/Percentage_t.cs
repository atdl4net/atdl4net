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
using Atdl4net.Model.Controls.Support;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Resources;
using Atdl4net.Validation;

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

        #region AtdlValueType<T> Overrides

        /// <summary>
        /// Validates the supplied value in terms of the parameters constraints (e.g., MinValue, MaxValue, etc.).
        /// </summary>
        /// <param name="value">Value to validate, may be null in which case no validation is applied.</param>
        /// <param name="isRequired">Set to true to check that this parameter is non-null.</param>
        /// <returns>ValidationResult indicating whether the supplied value is valid.</returns>
        protected override ValidationResult ValidateValue(decimal? value, bool isRequired)
        {
            if (value != null)
            {
                int adjustmentFactor = (MultiplyBy100 == true) ? 1 : 100;

                if (MaxValue != null && (decimal)value > MaxValue)
                    return new ValidationResult(ValidationResult.ResultType.Invalid, ErrorMessages.MaxValueExceeded,
                        RemoveTrailingZeroes(value * adjustmentFactor), RemoveTrailingZeroes(MaxValue * adjustmentFactor));

                if (MinValue != null && (decimal)value < MinValue)
                    return new ValidationResult(ValidationResult.ResultType.Invalid, ErrorMessages.MinValueExceeded,
                        RemoveTrailingZeroes(value * adjustmentFactor), RemoveTrailingZeroes(MinValue * adjustmentFactor));
            }
            else if (isRequired)
                return new ValidationResult(ValidationResult.ResultType.Missing, ErrorMessages.NonOptionalParameterNotSupplied2);

            return ValidationResult.ValidResult;
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

            return (MultiplyBy100 == true) ? (decimal)decimalValue / 100 : (decimal)decimalValue;
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

            decimal adjustedValue = (MultiplyBy100 == true) ? (decimal)RemoveTrailingZeroes(value * 100) : (decimal)value;

            if (Precision == null)
                return adjustedValue.ToString(CultureInfo.InvariantCulture);
            else
                return ((decimal)(Round(adjustedValue, (int)Precision))).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the supplied value to the type parameter type (T?) for this class.
        /// </summary>
        /// <param name="hostParameter">Parameter that this value belongs to.</param>
        /// <param name="value">Value to convert, may be null.</param>
        /// <returns>If input value is not null, returns value converted to T?; null otherwise.</returns>
        /// <remarks>Used when setting a parameter value from a control (or anything else that
        /// implements <see cref="IParameterConvertible"/>).<br/><br/>
        /// Unlike all other (non-enumerated) control/parameter relationships, Percentage_t does not have a 
        /// one-to-one mapping with its associated control value as the control will typically contain a user-oriented 
        /// format (e.g., 25) when the parameter must contain the true value (i.e., 0.25, assuming multiplyBy100 
        /// is not set to true).</remarks>
        protected override decimal? ConvertToNativeType(IParameter hostParameter, IParameterConvertible value)
        {
            decimal? convertedValue = value.ToDecimal(hostParameter, CultureInfo.CurrentUICulture);

            return (convertedValue != null) ? convertedValue /= 100 : null;
        }

        /// <summary>
        /// Gets the value of this parameter type in its native (i.e., raw) form, such as int, char, string, etc. 
        /// </summary>
        /// <param name="applyWireValueFormat">If set to true, the value returned is adjusted to be in the 'format'
        /// it would be if sent on the FIX wire.  In this case, we have to apply both Precision and the MultiplyBy100
        /// flag.</param>
        /// <returns>Native parameter value.</returns>
        public override object GetNativeValue(bool applyWireValueFormat)
        {
            decimal? value = ConstValue != null ? ((MultiplyBy100 == true) ? ConstValue / 100 : ConstValue) : _value;

            if (value != null && applyWireValueFormat)
            {
                decimal adjustedValue = (MultiplyBy100 == true) ? (decimal)RemoveTrailingZeroes(value * 100) : (decimal)value;

                if (Precision != null)
                    return Math.Round(adjustedValue, (int)Precision, MidpointRounding.AwayFromZero);
                else
                    return adjustedValue;
            }
            else
                return value;
        }

        #endregion

        #region IControlConvertible Members

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable decimal value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A nullable decimal equivalent to the value of this instance.</returns>
        public override decimal? ToDecimal()
        {
            decimal? value = ConstValue ?? _value;

            if (value == null || MultiplyBy100 == true)
                return value;

            return RemoveTrailingZeroes(value * 100);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent string value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A string value equivalent to the value of this instance.  May be null.</returns>
        public override string ToString(IFormatProvider provider)
        {
            decimal? value = ToDecimal();

            return (value != null) ? ((decimal)value).ToString(provider) : null;
        }

        #endregion

        private static decimal? RemoveTrailingZeroes(decimal? value)
        {
            if (value == null)
                return null;

            // We use this slightly ugly manipulation to remove the trailing zeroes that multiplication by 100 produces
            return decimal.Parse(((decimal)value).ToString("G29"));
        }
    }
}
