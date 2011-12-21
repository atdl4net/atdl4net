#region Copyright (c) 2010-2011, Cornerstone Technology Limited. http://atdl4net.org
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
using System.Linq;

namespace Atdl4net.Validation
{
    /// <summary>
    /// Represents the results of a validation operation.
    /// </summary>
    public class ValidationResult
    {
        private readonly bool _isValid;
        private readonly string _errorText;

        private static readonly ValidationResult _validResult = new ValidationResult();

        /// <summary>
        /// Indicates whether the validation that this ValidationResult corresponds to is valid.
        /// </summary>
        public bool IsValid { get { return _isValid; } }

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
        public ValidationResult(bool isValid, string format, params object[] args)
        {
            _isValid = isValid;
            _errorText = string.Format(format, args);
        }

        private ValidationResult()
        {
            _isValid = true;
        }
    }
}
