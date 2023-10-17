#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Globalization;
using System.Linq;
using Atdl4net.Diagnostics;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Controls.Support;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Resources;
using Atdl4net.Validation;

namespace Atdl4net.Model.Types.Support
{
    /// <summary>
    /// Base class for all date and time related FIXatdl types (except MonthYear_t).
    /// </summary>
    public abstract class DateTimeTypeBase : AtdlValueType<DateTime>, IControlConvertible
    {
        /// <summary>
        /// Maximum value for this date/time type, i.e., the latest acceptable date/time.
        /// </summary>
        public DateTime? MaxValue { get; set; }

        /// <summary>
        /// Minimum value for this date/time type, i.e., the earliest acceptable date/time.
        /// </summary>
        public DateTime? MinValue { get; set; }

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
            if (value != null)
            {
                if (value == DateTime.MaxValue)
                    return new ValidationResult(ValidationResult.ResultType.Invalid, ErrorMessages.InvalidDateOrTimeValueUnknown);

                if (MaxValue != null && (DateTime)value > MaxValue)
                    return new ValidationResult(ValidationResult.ResultType.Invalid, ErrorMessages.MaxValueExceeded, value, MaxValue);

                if (MinValue != null && (DateTime)value < MinValue)
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
        /// <param name="value">Type to convert from string, cannot be null.</param>
        /// <returns>Value converted from a string if the conversion succeeded; otherwise an exception is thrown.</returns>
        protected override DateTime? ConvertFromWireValueFormat(string value)
        {
            string[] formats = GetDateTimeFormatStrings();

            DateTime result;

            if (DateTime.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out result))
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

            return _value != null ? ((DateTime)_value).ToString(format) : null;
        }

        /// <summary>
        /// Converts the supplied value to the type parameter type (DateTime?) for this class.
        /// </summary>
        /// <param name="hostParameter"><see cref="IParameter"/> that hosts this value.</param>
        /// <param name="value">Value to convert, may be null.</param>
        /// <returns>If input value is not null, returns value converted to T?; null otherwise.</returns>
        /// <remarks>Used when setting a parameter value from a control (or anything else that
        /// implements <see cref="IParameterConvertible"/>).</remarks>
        protected override DateTime? ConvertToNativeType(IParameter hostParameter, IParameterConvertible value)
        {
            return value.ToDateTime(hostParameter, CultureInfo.CurrentUICulture);
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
            DateTime? value = ConstValue ?? _value;

            return value != null ? ((DateTime)_value).ToString(GetDateTimeFormatStrings()[0]) : null;
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
            return ConstValue ?? _value;
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

        /// <summary>
        /// Gets the DateTime format strings to use when converting this date/time to a FIX string and vice versa.
        /// </summary>
        /// <returns>Format strings suitable when calling DateTime.ToString().  At least one format string will be 
        /// returned.</returns>
        /// <remarks>When converting from DateTime to string, the first member of the returned array is used.  When
        /// converting from string to DateTime, the member of the array that has the same length as the string
        /// value is used.</remarks>
        protected abstract string[] GetDateTimeFormatStrings();
    }
}
