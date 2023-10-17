#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using Atdl4net.Resources;
using Atdl4net.Validation;

namespace Atdl4net.Model.Types.Support
{
    /// <summary>
    /// Abstract base class for FIXatdl types that require positive integers greater than zero.
    /// </summary>
    public abstract class NonZeroPositiveIntegerTypeBase : NonNegativeIntegerTypeBase
    {
        /// <summary>
        /// Validates the supplied value in terms of the parameters constraints (e.g., MinValue, MaxValue, etc.).
        /// </summary>
        /// <param name="value">Value to validate, may be null in which case no validation is applied.</param>
        /// <param name="isRequired">Set to true to check that this parameter is non-null.</param>
        /// <returns>ValidationResult indicating whether the supplied value is valid.</returns>
        protected override ValidationResult ValidateValue(uint? value, bool isRequired)
        {
            if (value != null && (uint)value < 1)
                return new ValidationResult(ValidationResult.ResultType.Invalid, ErrorMessages.NonZeroPositiveIntRequired, value);

            if (isRequired && value == null)
                return new ValidationResult(ValidationResult.ResultType.Missing, ErrorMessages.NonOptionalParameterNotSupplied2);

            return ValidationResult.ValidResult;
        }
    }
}
