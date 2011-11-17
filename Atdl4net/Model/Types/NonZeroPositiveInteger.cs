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

using Atdl4net.Resources;
using System;
using System.Globalization;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Types
{
    public abstract class NonZeroPositiveInteger : NonEnumableValueType<int>
    {
        protected override int? ValidateValue(int? value)
        {
            if (value != null && (int)value <= 0)
                throw ThrowHelper.New<ArgumentOutOfRangeException>(this, ErrorMessages.NonZeroPositiveIntRequired, value);

            return value;
        }

        protected override int? ConvertFromString(string value)
        {
            return value != null ? (int?)Convert.ToInt32(value) : null;
        }

        protected override string ConvertToString(int? value)
        {
            return value != null ? ((int)value).ToString(CultureInfo.InvariantCulture) : null;
        }
    }
}
