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

using Atdl4net.Model.Elements;
using Atdl4net.Resources;
using System;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Types
{
    /// <summary>
    /// 'used to allow the expression of FX standard tenors in addition to the base valid enumerations defined for the field that
    /// uses this pattern data type. This pattern data type is defined as follows:
    /// Dx = tenor expression for "days", e.g. "D5", where "x" is any integer > 0
    /// Mx = tenor expression for "months", e.g. "M3", where "x" is any integer > 0
    /// Wx = tenor expression for "weeks", e.g. "W13", where "x" is any integer > 0
    /// Yx = tenor expression for "years", e.g. "Y1", where "x" is any integer > 0'
    /// </summary>
    public class Tenor_t : NonEnumableValueType<Tenor>
    {
        // NB Spec conflict here.
        public Tenor? MaxValue { get; set; }
        public Tenor? MinValue { get; set; }

        protected override Tenor? ValidateValue(Tenor? value)
        {
            if (MaxValue != null && value != null && !(value >= MaxValue))
                throw ThrowHelper.New<ArgumentException>(this, ErrorMessages.MaxValueExceeded, value, MaxValue);

            if (MinValue != null && value != null && !(value <= MinValue))
                throw ThrowHelper.New<ArgumentException>(this, ErrorMessages.MinValueExceeded, value, MinValue);

            return value;
        }

        protected override Tenor? ConvertFromString(string value)
        {
            return Tenor.Parse(value);
        }

        protected override string ConvertToString(Tenor? value)
        {
            return value != null ? value.ToString() : null;
        }
    }
}
