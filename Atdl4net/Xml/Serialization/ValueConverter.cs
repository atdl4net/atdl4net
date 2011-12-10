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

using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Fix;
using Atdl4net.Resources;
using System;
using System.Globalization;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Xml.Serialization
{
    public class ValueConverter
    {
        public static readonly string ExceptionContext = typeof(ValueConverter).Name;

        public static T ConvertTo<T>(string value)
        {
            return (T)ConvertTo(value, typeof(T));
        }

        public static object ConvertTo(string value, System.Type targetType)
        {
            switch (targetType.FullName)
            {
                case "System.String":
                    return value;

                case "System.Char":
                    if (value.Length == 1)
                        return Convert.ToChar(value);
                    else
                        throw ThrowHelper.New<InvalidFieldValueException>(ExceptionContext, ErrorMessages.InvalidCharValue, value);

                case "System.Boolean":
                    return bool.Parse(value);

                case "System.Int32":
                    return Convert.ToInt32(value, CultureInfo.InvariantCulture);

                case "System.Decimal":
                    return Convert.ToDecimal(value, CultureInfo.InvariantCulture);

                case "System.DateTime":
                    {
                        DateTime result;

                        if (!FixDateTime.TryParse(value, CultureInfo.InvariantCulture, out result))
                            throw ThrowHelper.New<InvalidFieldValueException>(ExceptionContext, ErrorMessages.InvalidDateOrTimeValue, value);

                        return result;
                    }

                case "Atdl4net.Fix.FixTag":
                    return new FixTag(Convert.ToInt32(value, CultureInfo.InvariantCulture));

                default:
                    if (targetType.FullName.StartsWith("Atdl4net.Model.Controls.InitValue"))
                        return value;
                    else
                        throw ThrowHelper.New<InternalErrorException>(ExceptionContext, InternalErrors.UnrecognisedAttributeType, targetType.FullName);
            }
        }
    }
}
