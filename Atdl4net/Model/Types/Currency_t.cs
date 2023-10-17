#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using Atdl4net.Model.Controls.Support;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Model.Reference;
using Atdl4net.Model.Types.Support;
using Atdl4net.Resources;
using Atdl4net.Utility;
using Atdl4net.Validation;

namespace Atdl4net.Model.Types
{
    /// <summary>
    /// 'string field representing a currency type using ISO 4217 Currency code (3 character) values (see Appendix 6-A).'
    /// </summary>
    public class Currency_t : EnumTypeBase<IsoCurrencyCode>
    {
        #region AtdlValueType<T> Overrides

        /// <summary>
        /// Validates the supplied value in terms of the parameters constraints.  This method does nothing because
        /// is not possible for an IsoCurrencyCode value to be invalid.
        /// </summary>
        /// <param name="value">Value to validate, may be null in which case no validation is applied.</param>
        /// <param name="isRequired">Set to true to check that this parameter is non-null.</param>
        /// <returns>ValidationResult indicating whether the supplied value is valid.</returns>
        protected override ValidationResult ValidateValue(IsoCurrencyCode? value, bool isRequired)
        {
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
        protected override IsoCurrencyCode? ConvertFromWireValueFormat(string value)
        {
            return (IsoCurrencyCode?)value.ParseAsEnum<IsoCurrencyCode>();
        }

        /// <summary>
        /// Converts the supplied value to a string, as might be used on the FIX wire.
        /// </summary>
        /// <param name="value">Value to convert, may be null.</param>
        /// <returns>If input value is not null, returns value converted to a string; null otherwise.</returns>
        protected override string ConvertToWireValueFormat(IsoCurrencyCode? value)
        {
            return value != null ? Enum.GetName(typeof(IsoCurrencyCode), value) : null;
        }

        /// <summary>
        /// Converts the supplied value to the type parameter type (T?) for this class.
        /// </summary>
        /// <param name="hostParameter"><see cref="IParameter"/> that hosts this value.</param>
        /// <param name="value">Value to convert, may be null.</param>
        /// <returns>If input value is not null, returns value converted to T?; null otherwise.</returns>
        /// <remarks>Used when setting a parameter value from a control (or anything else that
        /// implements <see cref="IParameterConvertible"/>).</remarks>
        protected override IsoCurrencyCode? ConvertToNativeType(IParameter hostParameter, IParameterConvertible value)
        {
            string wireValue = value.ToString(hostParameter);

            return !string.IsNullOrEmpty(wireValue) ? ConvertFromWireValueFormat(wireValue) : null;
        }

        /// <summary>
        /// Gets the human-readable type name for use in error messages shown to the user.
        /// </summary>
        /// <returns>Human-readable type name.</returns>
        protected override string GetHumanReadableTypeName()
        {
            return HumanReadableTypeNames.CurrencyType;
        }

        #endregion
    }
}
