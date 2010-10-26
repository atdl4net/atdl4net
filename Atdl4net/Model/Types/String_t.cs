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

using Atdl4net.Resources;
using System;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Types
{
    /// <summary>
    /// 'Alpha-numeric free format strings, can include any character or punctuation except the delimiter. All String 
    /// fields are case sensitive (i.e. morstatt != Morstatt).'
    /// </summary>
    public class String_t : EnumableReferenceType<string>
    {
        /// <summary>Gets or sets the maximum length of this parameter.<br/>
        /// Applicable when xsi:type is String_t.  The maximum allowable length of the parameter.
        /// </summary>
        /// <value>The maximum length.</value>
        public int? MaxLength { get; set; }

        /// <summary>
        /// Gets or sets the minimum length of this parameter.<br/>
        /// Applicable when xsi:type is String_t.  The minimum allowable length of the parameter.
        /// </summary>
        /// <value>The minimum length.</value>
        public int? MinLength { get; set; }

        protected override string ValidateValue(string value)
        {
            if (MaxLength != null && value != null && value.Length > MaxLength)
                throw ThrowHelper.New<ArgumentOutOfRangeException>(this, ErrorMessages.MaxLengthExceeded, value, MaxLength);

            if (MinLength != null && value != null && value.Length < MinLength)
                throw ThrowHelper.New<ArgumentOutOfRangeException>(this, ErrorMessages.MinLengthExceeded, value, MinLength);

            return value;
        }

        protected override string ConvertFromString(string value)
        {
            return string.IsNullOrEmpty(value) ? null : value;
        }

        protected override string ConvertToString(string value)
        {
            return value;
        }
    }
}
