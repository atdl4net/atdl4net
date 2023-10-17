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
    /// 'used to allow the expression of FX standard tenors in addition to the base valid enumerations defined for the field that
    /// uses this pattern data type. This pattern data type is defined as follows:
    /// Dx = tenor expression for "days", e.g. "D5", where "x" is any integer > 0
    /// Mx = tenor expression for "months", e.g. "M3", where "x" is any integer > 0
    /// Wx = tenor expression for "weeks", e.g. "W13", where "x" is any integer > 0
    /// Yx = tenor expression for "years", e.g. "Y1", where "x" is any integer > 0'
    /// </summary>
    public class Tenor_t : AtdlValueType<Tenor>, IControlConvertible
    {
        /// <summary>
        /// Gets/sets the maximum value of this parameter.
        /// </summary>
        public Tenor? MaxValue { get; set; }

        /// <summary>
        /// Gets/sets the minimum value of this parameter.
        /// </summary>
        public Tenor? MinValue { get; set; }

        #region AtdlValueType<T> Overrides

        /// <summary>
        /// Validates the supplied value in terms of the parameters constraints (e.g., MinValue, MaxValue, etc.).
        /// </summary>
        /// <param name="value">Value to validate, may be null in which case no validation is applied.</param>
        /// <param name="isRequired">Set to true to check that this parameter is non-null.</param>
        /// <returns>ValidationResult indicating whether the supplied value is valid.</returns>
        protected override ValidationResult ValidateValue(Tenor? value, bool isRequired)
        {
            if (MaxValue != null && value != null && !(value >= MaxValue))
                return new ValidationResult(ValidationResult.ResultType.Invalid, ErrorMessages.MaxValueExceeded, value, MaxValue);

            if (MinValue != null && value != null && !(value <= MinValue))
                return new ValidationResult(ValidationResult.ResultType.Invalid, ErrorMessages.MinValueExceeded, value, MinValue);

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
        protected override Tenor? ConvertFromWireValueFormat(string value)
        {
            return Tenor.Parse(value);
        }

        /// <summary>
        /// Converts the supplied value to a string, as might be used on the FIX wire.  If the supplied value is
        /// null, this means the field is not to be included in the outgoing FIX message.
        /// </summary>
        /// <param name="value">Value to convert, may be null.</param>
        /// <returns>If input value is not null, returns value converted to a string; null otherwise.</returns>
        protected override string ConvertToWireValueFormat(Tenor? value)
        {
            return value != null ? value.ToString() : null;
        }

        /// <summary>
        /// Converts the supplied value to the type parameter type (Tenor?) for this class.
        /// </summary>
        /// <param name="hostParameter"><see cref="IParameter"/> that hosts this value.</param>
        /// <param name="value">Value to convert, may be null.</param>
        /// <returns>If input value is not null, returns value converted to T?; null otherwise.</returns>
        /// <remarks>Used when setting a parameter value from a control (or anything else that
        /// implements <see cref="IParameterConvertible"/>).</remarks>
        protected override Tenor? ConvertToNativeType(IParameter hostParameter, IParameterConvertible value)
        {
            string tenor = value.ToString(hostParameter);

            return tenor != null ? (Tenor?)Tenor.Parse(tenor) : null;
        }

        /// <summary>
        /// Gets the human-readable type name for use in error messages shown to the user.
        /// </summary>
        /// <returns>Human-readable type name.</returns>
        protected override string GetHumanReadableTypeName()
        {
            return HumanReadableTypeNames.TenorType;
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
            Tenor? value = ConstValue ?? _value;

            return (value != null) ? ((Tenor)value).ToString() : null;
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
        public EnumState ToEnumState(EnumPairCollection enumPairs)
        {
            if (_value == null)
                return new EnumState(enumPairs.EnumIds);
            else
                return EnumState.FromWireValue(enumPairs, ToString(null));
        }

        #endregion
    }
}
