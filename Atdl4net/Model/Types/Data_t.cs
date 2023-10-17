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
    /// Represents a string field containing raw data with no format or content restrictions. Data fields are always immediately preceded
    /// by a length field. The length field should specify the number of bytes of the value of the data field (up to but not 
    /// including the terminating SOH).
    /// Caution: the value of one of these fields may contain the delimiter (SOH) character. Note that the value specified for
    /// this field should be followed by the delimiter (SOH) character as all fields are terminated with an "SOH".
    /// </summary>
    /// <remarks>As there is no way within FIXatdl of associating one field with another to provide the length, Data_t fields
    /// do not appear to be particularly useful; they are implemented within Atdl4net for completeness.</remarks>
    public class Data_t : AtdlReferenceType<char[]>, IControlConvertible
    {
        /// <summary>
        /// Gets/sets the maximum length of this field.
        /// </summary>
        public int? MaxLength { get; set; }

        /// <summary>
        /// Gets/sets the minimum length of this field.
        /// </summary>
        public int? MinLength { get; set; }

        #region AtdlValueType<T> Overrides

        /// <summary>
        /// Validates the supplied value in terms of the parameters constraints.
        /// </summary>
        /// <param name="value">Value to validate, may be null in which case no validation is applied.</param>
        /// <param name="isRequired">Set to true to check that this parameter is non-null.</param>
        /// <returns>ValidationResult indicating whether the supplied value is valid.</returns>
        protected override ValidationResult ValidateValue(char[] value, bool isRequired)
        {
            if (MaxLength != null && value != null && value.Length > MaxLength)
                return new ValidationResult(ValidationResult.ResultType.Invalid, ErrorMessages.MaxLengthExceeded, value, MaxLength);

            if (MinLength != null && value != null && value.Length < MinLength)
                return new ValidationResult(ValidationResult.ResultType.Invalid, ErrorMessages.MinLengthExceeded, value, MinLength);

            if (isRequired && value == null)
                return new ValidationResult(ValidationResult.ResultType.Missing, ErrorMessages.NonOptionalParameterNotSupplied2);

            return ValidationResult.ValidResult;
        }

        /// <summary>
        /// Converts the supplied value from string format (as might be used on the FIX wire) into the type of the type
        /// parameter for this type.
        /// </summary>
        /// <param name="value">Type to convert from string; cannot be null as empty fields are invalid in FIX.</param>
        /// <returns>Value converted from a string.</returns>
        protected override char[] ConvertFromWireValueFormat(string value)
        {
            return value != null ? value.ToCharArray() : null;
        }

        /// <summary>
        /// Converts the supplied value to a string, as might be used on the FIX wire.
        /// </summary>
        /// <param name="value">Value to convert, may be null.</param>
        /// <returns>If input value is not null, returns value converted to a string; null otherwise.</returns>
        protected override string ConvertToWireValueFormat(char[] value)
        {
            return value != null ? new string(value) : null;
        }

        /// <summary>
        /// Converts the supplied value to the type parameter type (T?) for this class.
        /// </summary>
        /// <param name="hostParameter">Parameter that this value belongs to.</param>
        /// <param name="value">Value to convert, may be null.</param>
        /// <returns>If input value is not null, returns value converted to T?; null otherwise.</returns>
        protected override char[] ConvertToNativeType(IParameter hostParameter, IParameterConvertible value)
        {
            string result = value != null ? value.ToString(hostParameter) : null;

            return result != null ? result.ToCharArray() : null;
        }

        /// <summary>
        /// Gets the human-readable type name for use in error messages shown to the user.
        /// </summary>
        /// <returns>Human-readable type name.</returns>
        protected override string GetHumanReadableTypeName()
        {
            return HumanReadableTypeNames.DataType;
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
            char[] value = ConstValue ?? _value;

            return value != null ? new string(value) : null;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable decimal value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
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
        /// <remarks>This method converts the enum value to a string, looks up the EnumID from the supplied
        /// EnumPairCollection and then returns a new EnumState.  This method may be a little slow for
        /// very large enumerations.</remarks>
        public EnumState ToEnumState(EnumPairCollection enumPairs)
        {
            throw ThrowHelper.New<InvalidCastException>(this, ErrorMessages.UnsupportedParameterValueConversion, _value, "Enumerated Type");
        }

        #endregion
    }
}
