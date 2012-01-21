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
using Atdl4net.Model.Collections;
using Atdl4net.Model.Controls.Support;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Model.Types.Support;
using Atdl4net.Resources;
using Atdl4net.Validation;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Types
{
    /// <summary>
    /// Represents a sequence of digits with optional decimal point and sign character (ASCII characters "-", "0" - "9" and "."); the
    /// absence of the decimal point within the string will be interpreted as the float representation of an integer value.
    /// All float fields must accommodate up to fifteen significant digits. The number of decimal places used should be a
    /// factor of business/market needs and mutual agreement between counterparties. Note that float values may contain
    /// leading zeros (e.g. "00023.23" = "23.23") and may contain or omit trailing zeros after the decimal point 
    /// (e.g. "23.0" = "23.0000" = "23" = "23.").
    /// Note that fields which are derived from float may contain negative values unless explicitly specified otherwise.'
    /// </summary>
    public class Float_t : AtdlValueType<decimal>, IControlConvertible
    {
        /// <summary>
        /// Gets/sets the maximum value for this parameter.<br/>
        /// Maximum value of the parameter accepted by the algorithm provider.
        /// </summary>
        /// <value>The maximum value.</value>
        public decimal? MaxValue { get; set; }

        /// <summary>
        /// Gets/sets the minimum value for this parameter.<br/>
        /// Minimum value of the parameter accepted by the algorithm provider.
        /// </summary>
        /// <value>The minimum value.</value>
        public decimal? MinValue { get; set; }

        /// <summary>
        /// Gets/sets the precision of this value, taken as the number of digits to the right of the decimal point in 
        /// which to round when populating the FIX message. Lack of this attribute indicates that the value entered by 
        /// the user should be taken as-is without rounding.
        /// </summary>
        public int? Precision { get; set; }

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
                if (MaxValue != null && (decimal)value > MaxValue)
                    return new ValidationResult(ValidationResult.ResultType.Invalid, ErrorMessages.MaxValueExceeded, value, MaxValue);

                if (MinValue != null && (decimal)value < MinValue)
                    return new ValidationResult(ValidationResult.ResultType.Invalid, ErrorMessages.MinValueExceeded, value, MinValue);
            }
            else if (isRequired)
                return new ValidationResult(ValidationResult.ResultType.Missing, ErrorMessages.NonOptionalParameterNotSupplied2);

            return ValidationResult.ValidResult;
        }

        /// <summary>
        /// Converts the supplied value from string format (as might be used on the FIX wire) into the type of the type
        /// parameter for this type.
        /// </summary>
        /// <param name="value">Type to convert from string; cannot be null as empty fields are invalid in FIX.</param>
        /// <returns>Value converted from a string.</returns>
        protected override decimal? ConvertFromWireValueFormat(string value)
        {
            return Convert.ToDecimal(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the supplied value to a string, as might be used on the FIX wire.  If the supplied value is
        /// null, this means the field is not to be included in the outgoing FIX message.
        /// </summary>
        /// <param name="value">Value to convert, may be null.</param>
        /// <returns>If input value is not null, returns value converted to a string; null otherwise.</returns>
        protected override string ConvertToWireValueFormat(decimal? value)
        {
            if (value == null)
                return null;

            if (Precision == null)
                return ((decimal)value).ToString(CultureInfo.InvariantCulture);
            else
                return ((decimal)Round(value, (int)Precision)).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the supplied value to the type parameter type (T?) for this class.
        /// </summary>
        /// <param name="hostParameter">Parameter that this value belongs to.</param>
        /// <param name="value">Value to convert, may be null.</param>
        /// <returns>If input value is not null, returns value converted to T?; null otherwise.</returns>
        /// <remarks>Used when setting a parameter value from a control (or anything else that
        /// implements <see cref="IParameterConvertible"/>).</remarks>
        protected override decimal? ConvertToNativeType(IParameter hostParameter, IParameterConvertible value)
        {
            return value.ToDecimal(hostParameter, CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Gets the value of this parameter type in its native (i.e., raw) form, such as int, char, string, etc. 
        /// </summary>
        /// <param name="applyWireValueFormat">If set to true, the value returned is adjusted to be in the 'format'
        /// it would be if sent on the FIX wire.  For example, for Float_t parameters, setting this value to true
        /// would cause the Precision attribute setting to be applied.</param>
        /// <returns>Native parameter value.</returns>
        public override object GetNativeValue(bool applyWireValueFormat)
        {
            decimal? value = ConstValue != null ? ConstValue : _value;

            if (value != null && applyWireValueFormat && Precision != null)
                return Round(value, (int)Precision);
            else
                return value;
        }

        /// <summary>
        /// Gets the human-readable type name for use in error messages shown to the user.
        /// </summary>
        /// <returns>Human-readable type name.</returns>
        protected override string GetHumanReadableTypeName()
        {
            return HumanReadableTypeNames.NumericType;
        }

        #endregion

        /// <summary>
        /// Rounds the supplied value to the specified number of decimal places.
        /// </summary>
        /// <param name="value">Value to be rounded. May be null.</param>
        /// <param name="precision">Number of places to round to.</param>
        /// <returns>If the supplied value is non-null, the rounded value is returned; otherwise returns null.</returns>
        protected decimal? Round(decimal? value, int precision)
        {
            return value != null ? (decimal?)(Math.Round((decimal)value, precision, MidpointRounding.AwayFromZero)) : null;
        }

        #region IControlConvertible Members

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable boolean value.
        /// </summary>
        /// <returns>One of true, false or null which is equivalent to the value of this instance.</returns>
        public bool? ToBoolean()
        {
            throw ThrowHelper.New<InvalidCastException>(this, ErrorMessages.UnsupportedParameterValueConversion, _value, "Boolean");
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent string value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A string value equivalent to the value of this instance.  May be null.</returns>
        public virtual string ToString(IFormatProvider provider)
        {
            decimal? value = ConstValue ?? _value;

            return (value != null) ? ((decimal)value).ToString(provider) : null;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable decimal value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A nullable decimal equivalent to the value of this instance.</returns>
        public virtual decimal? ToDecimal()
        {
            return ConstValue ?? _value;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable DateTime value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A nullable DateTime equivalent to the value of this instance.</returns>
        public DateTime? ToDateTime()
        {
            throw ThrowHelper.New<InvalidCastException>(this, ErrorMessages.UnsupportedParameterValueConversion, _value, "DateTime");
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent EnumState value.
        /// </summary>
        /// <returns>A valid EnumState, assuming the source value can be correctly converted.</returns>
        public EnumState ToEnumState(EnumPairCollection enumPairs)
        {
            throw ThrowHelper.New<InvalidCastException>(this, ErrorMessages.UnsupportedParameterValueConversion, _value, "EnumState");
        }

        #endregion
    }
}
