#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Linq;

namespace Atdl4net.Validation
{
    /// <summary>
    /// Represents the results of a validation operation.
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// Type of the result.
        /// </summary>
        public enum ResultType
        {
            /// <summary>The result represents a valid value.</summary>
            Valid,

            /// <summary>The result represents a missing mandatory value.</summary>
            Missing,

            /// <summary>The result represents an invalid value.</summary>
            Invalid
        }

        private readonly ResultType _validityType;
        private readonly string _errorText;

        private static readonly ValidationResult _validResult = new ValidationResult();

        /// <summary>
        /// Indicates whether the validation that this ValidationResult corresponds to is valid.
        /// </summary>
        public bool IsValid { get { return _validityType == ResultType.Valid; } }

        /// <summary>
        /// Indicates whether the result that this ValidationResult corresponds to is invalid
        /// because the field was required but not present.
        /// </summary>
        public bool IsMissing { get { return _validityType == ResultType.Missing; } }

        /// <summary>
        /// Gets the error text for this ValidationResult; used when a validation has failed.
        /// </summary>
        public string ErrorText { get { return _errorText; } }

        /// <summary>
        /// Gets a static ValidationResult instance that corresponds to a successful validation.
        /// </summary>
        public static ValidationResult ValidResult { get { return _validResult; } }

        /// <summary>
        /// Initializes a new <see cref="ValidationResult"/> instance with the supplied state,
        /// format string and optional array of arguments.
        /// </summary>
        /// <param name="isValid">Set this value to true if the validation succeeded, false otherwise.</param>
        /// <param name="format">Format string.</param>
        /// <param name="args">Optional array of arguments to apply to format string.</param>
        public ValidationResult(ResultType resultType, string format, params object[] args)
        {
            _validityType = resultType;
            _errorText = string.Format(format, args);
        }

        private ValidationResult()
        {
            _validityType = ResultType.Valid;
        }
    }
}
