#region Copyright (c) 2010, Cornerstone Technology Limited. http://atdl4net.org
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
//      License as published by the Free Software Foundation, version 3.
// 
//      Atdl4net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
//      of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU Lesser General Public License for more details.
//
//      You should have received a copy of the GNU Lesser General Public License along with Atdl4net.  If not, see
//      http://www.gnu.org/licenses/.
//
#endregion

using Atdl4net.Model.Reference;
using Atdl4net.Utility;
using System;

namespace Atdl4net.Model.Types
{
    /// <summary>
    /// 'string field representing a currency type using ISO 4217 Currency code (3 character) values (see Appendix 6-A).'
    /// </summary>
    public class Currency_t : EnumableValueType<IsoCurrencyCode>
    {
        protected override IsoCurrencyCode? ValidateValue(IsoCurrencyCode? value)
        {
            return value;
        }

        protected override IsoCurrencyCode? ConvertFromString(string value)
        {
            return value != null ? (IsoCurrencyCode?)(value.ParseAsEnum<IsoCurrencyCode>()) : null;
        }

        protected override string ConvertToString(IsoCurrencyCode? value)
        {
            return value != null ? Enum.GetName(typeof(IsoCurrencyCode), value) : null;
        }
    }
}
