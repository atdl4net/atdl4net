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

using Atdl4net.Diagnostics;
using Atdl4net.Resources;
using System;

namespace Atdl4net.Utility
{
    /// <summary>
    /// Provides extension methods for System.String.
    /// </summary>
    public static class StringExtensions
    {
        private static readonly string ExceptionContext = typeof(StringExtensions).FullName;

        /// <summary>
        /// Gets the string representation of this enumerated type value.
        /// </summary>
        /// <typeparam name="T">Type of enum.</typeparam>
        /// <param name="value">Value to convert to the supplied enum type.</param>
        /// <returns>A valid enumerated value if the conversion was possible; an exception is thrown otherwise.</returns>
        public static T ParseAsEnum<T>(this string value) where T : struct
        {
            if (string.IsNullOrEmpty(value))
                throw ThrowHelper.New<ArgumentNullException>(ExceptionContext, ErrorMessages.NullOrEmptyStringEnumParseFailure, typeof(T).Name);

            T result;

            if (!typeof(T).IsEnum)
                throw ThrowHelper.New<InvalidOperationException>(ExceptionContext, InternalErrors.InvalidUseOfParseAsEnum);

#if NET_40
            if (!Enum.TryParse<T>(value, true, out result))
                throw ThrowHelper.New<ArgumentException>(ExceptionContext, ErrorMessages.InvalidValueEnumParseFailure, value, typeof(T).Name);
#else
            try
            {
                result = (T)Enum.Parse(typeof(T), value, true);
            }
            catch (ArgumentException ex)
            {
                // We don't Rethrow here as we want the error message (that may be shown to the user) to be consistent between the .NET 3.5 
                // and .NET 4.0 implementations.
                throw ThrowHelper.New<ArgumentException>(ExceptionContext, ex, ErrorMessages.InvalidValueEnumParseFailure, value, typeof(T).Name);
            }
#endif

            return result;
        }
    }
}
