#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion
using System;
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
    /// Represents an alpha-numeric free format strings, can include any character or punctuation except the delimiter. All
    /// String fields are case sensitive (i.e. morstatt != Morstatt).
    /// </summary>
    public class String_t : AtdlReferenceType<string>, IControlConvertible
    {
        /// <summary>Gets or sets the maximum length of this parameter.<br/>
        /// Applicable when xsi:type is String_t.  The maximum allowable length of the parameter.
        /// </summary>
        /// <value>The maximum length.</value>
        public int? MaxLength { get; set; }

        /// <summary>
        /// Gets or sets the minimum length of this parameter.<br/>
        /// Applicable when xsi:type is String_t.  The minimum allowable length of the parameter.
        /// </summary>
        /// <value>The minimum length.</value>
        public int? MinLength { get; set; }

        #region AtdlReferenceType<string> Overrides

        /// <summary>
        /// Validates the supplied value in terms of the parameters constraints.
        /// </summary>
        /// <param name="value">Value to validate, may be null in which case no validation is applied.</param>
        /// <param name="isRequired">Set to true to check that this parameter is non-null.</param>
        /// <returns>ValidationResult indicating whether the supplied value is valid.</returns>
        protected override ValidationResult ValidateValue(string value, bool isRequired)
        {
            if (MaxLength != null && value != null && value.Length > MaxLength)
                return new ValidationResult(ValidationResult.ResultType.Invalid, ErrorMessages.MaxLengthExceeded, value, MaxLength);

            if (MinLength != null && value != null && value.Length < MinLength)
                return new ValidationResult(ValidationResult.ResultType.Invalid, ErrorMessages.MinLengthExceeded, value, MinLength);

            if (isRequired && string.IsNullOrEmpty(value))
                return new ValidationResult(ValidationResult.ResultType.Missing, ErrorMessages.NonOptionalParameterNotSupplied2);

            return ValidationResult.ValidResult;
        }

        /// <summary>
        /// Converts the supplied value from string format (as might be used on the FIX wire) into the type of the type
        /// parameter for this type.
        /// </summary>
        /// <param name="value">Type to convert from string; cannot be null as empty fields are invalid in FIX.</param>
        /// <returns>Value converted from a string.</returns>
        protected override string ConvertFromWireValueFormat(string value)
        {
            return value;
        }

        /// <summary>
        /// Converts the supplied value to a string, as might be used on the FIX wire.
        /// </summary>
        /// <param name="value">Value to convert, may be null.</param>
        /// <returns>If input value is not null, returns value converted to a string; null otherwise.</returns>
        protected override string ConvertToWireValueFormat(string value)
        {
            return string.IsNullOrEmpty(value) ? null : value;
        }

        /// <summary>
        /// Converts the supplied value to the type parameter type (string) for this class.
        /// </summary>
        /// <param name="hostParameter"><see cref="IParameter"/> that hosts this value.</param>
        /// <param name="value">Value to convert, may be null.</param>
        /// <returns>If input value is not null, returns value converted to string; null otherwise.</returns>
        protected override string ConvertToNativeType(IParameter hostParameter, IParameterConvertible value)
        {
            return value.ToString(hostParameter);
        }

        /// <summary>
        /// Gets the human-readable type name for use in error messages shown to the user.
        /// </summary>
        /// <returns>Human-readable type name.</returns>
        protected override string GetHumanReadableTypeName()
        {
            return HumanReadableTypeNames.StringType;
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
            return _value;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable decimal value using the specified culture-specific formatting information.
        /// </summary>
        /// <returns>A nullable decimal equivalent to the value of this instance.</returns>
        public decimal? ToDecimal()
        {
            throw ThrowHelper.New<InvalidCastException>(this, ErrorMessages.UnsupportedParameterValueConversion, _value, "Decimal");
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
                return EnumState.FromWireValue(enumPairs, _value.ToString());
        }

        #endregion
    }
}
