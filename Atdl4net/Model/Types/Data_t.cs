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

using Atdl4net.Model.Elements;
using Atdl4net.Resources;
using System;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Types
{
    /// <summary>
    /// 'string field containing raw data with no format or content restrictions. Data fields are always immediately preceded
    /// by a length field. The length field should specify the number of bytes of the value of the data field (up to but not 
    /// including the terminating SOH).
    /// Caution: the value of one of these fields may contain the delimiter (SOH) character. Note that the value specified for
    /// this field should be followed by the delimiter (SOH) character as all fields are terminated with an "SOH".'
    /// </summary>
    public class Data_t : EnumableReferenceType<char[]>
    {
        public int? MaxLength { get; set; }
        public int? MinLength { get; set; }

        protected override char[] ValidateValue(char[] value)
        {
            if (MaxLength != null && value != null && value.Length > MaxLength)
                throw ThrowHelper.New<ArgumentOutOfRangeException>(this, ErrorMessages.MaxLengthExceeded, value, MaxLength);

            if (MinLength != null && value != null && value.Length < MinLength)
                throw ThrowHelper.New<ArgumentOutOfRangeException>(this, ErrorMessages.MinLengthExceeded, value, MinLength);

            return value;
        }

        protected override char[] ConvertFromString(string value)
        {
            return value != null ? value.ToCharArray() : null;
        }

        protected override string ConvertToString(char[] value)
        {
            return value != null ? value.ToString() : null;
        }
    }
}
