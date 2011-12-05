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
using Atdl4net.Model.Controls.Support;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Model.Reference;
using Atdl4net.Model.Types.Support;
using Atdl4net.Utility;

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
        /// <returns>Value passed in.</returns>
        protected override IsoCurrencyCode? ValidateValue(IsoCurrencyCode? value)
        {
            return value;
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
        protected override IsoCurrencyCode? ConvertToNativeType(IParameter hostParameter, IParameterConvertible value)
        {
            string wireValue = value.ToString(hostParameter);

            return !string.IsNullOrEmpty(wireValue) ? ConvertFromWireValueFormat(wireValue) : null;
        }

        #endregion

    }
}
