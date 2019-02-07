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
    /// 'Sequence of digits without commas or decimals and optional sign character (ASCII characters "-" and "0" - "9" ). 
    /// The sign character utilizes one byte (i.e. positive int is "99999" while negative int is "-99999"). Note that int
    /// values may contain leading zeros (e.g. "00023" = "23").'
    /// </summary>
    public class Int_t : AtdlValueType<int>, IControlConvertible
    {
        /// <summary>
        /// Gets/sets the minimum value for this parameter.<br/>
        /// Minimum value of the parameter accepted by the algorithm provider.
        /// </summary>
        /// <value>The minimum value.</value>
        public int? MinValue { get; set; }

        /// <summary>
        /// Gets/sets the maximum value for this parameter.<br/>
        /// Maximum value of the parameter accepted by the algorithm provider.
        /// </summary>
        /// <value>The maximum value.</value>
        public int? MaxValue { get; set; }

        #region AtdlValueType<T> Overrides

        /// <summary>
        /// Validates the supplied value in terms of the parameters constraints (e.g., MinValue, MaxValue, etc.).
        /// </summary>
        /// <param name="value">Value to validate, may be null in which case no validation is applied.</param>
        /// <param name="isRequired">Set to true to check that this parameter is non-null.</param>
        /// <returns>ValidationResult indicating whether the supplied value is valid.</returns>
        protected override ValidationResult ValidateValue(int? value, bool isRequired)
        {
            if (value != null)
            {
                if (MaxValue != null && (int)value > MaxValue)
                    return new ValidationResult(ValidationResult.ResultType.Invalid, ErrorMessages.MaxValueExceeded, value, MaxValue);

                if (MinValue != null && (int)value < MinValue)
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
        protected override int? ConvertFromWireValueFormat(string value)
        {
            return value != null ? (int?)Convert.ToInt32(value) : null;
        }

        /// <summary>
        /// Converts the supplied value to a string, as might be used on the FIX wire.  If the supplied value is
        /// null, this means the field is not to be included in the outgoing FIX message.
        /// </summary>
        /// <param name="value">Value to convert, may be null.</param>
        /// <returns>If input value is not null, returns value converted to a string; null otherwise.</returns>
        protected override string ConvertToWireValueFormat(int? value)
        {
            return value != null ? ((int)value).ToString(CultureInfo.InvariantCulture) : null;
        }

        /// <summary>
        /// Converts the supplied value to the type parameter type (T?) for this class.
        /// </summary>
        /// <param name="hostParameter">Parameter that this value belongs to.</param>
        /// <param name="value">Value to convert, may be null.</param>
        /// <returns>If input value is not null, returns value converted to T?; null otherwise.</returns>
        /// <remarks>Used when setting a parameter value from a control (or anything else that
        /// implements <see cref="IParameterConvertible"/>).</remarks>
        protected override int? ConvertToNativeType(IParameter hostParameter, IParameterConvertible value)
        {
            return value.ToInt32(hostParameter, CultureInfo.CurrentUICulture);
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
        public string ToString(IFormatProvider provider)
        {
            int? value = ConstValue ?? _value;

            return (value != null) ? ((int)value).ToString(provider) : null;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable decimal value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A nullable decimal equivalent to the value of this instance.</returns>
        public decimal? ToDecimal()
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
            if (_value == null)
                return new EnumState(enumPairs.EnumIds);
            else
                return EnumState.FromWireValue(enumPairs, ToString(CultureInfo.InvariantCulture));
        }

        #endregion
    }
}
