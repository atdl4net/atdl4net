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
    /// 'string field representing month of a year. An optional day of the month can be appended or an optional week code.
    /// Valid formats:  YYYYMM
    ///                 YYYYMMDD
    ///                 YYYYMMWW
    /// Valid values:   YYYY = 0000-9999; MM = 01-12; DD = 01-31; WW = w1, w2, w3, w4, w5.'
    /// </summary>
    public class MonthYear_t : EnumableValueType<MonthYear>
    {
        public MonthYear? MaxValue { get; set; }
        public MonthYear? MinValue { get; set; }

        protected override MonthYear? ValidateValue(MonthYear? value)
        {
            if (MaxValue != null && !(value >= MaxValue))
                throw ThrowHelper.New<ArgumentException>(this, ErrorMessages.MaxValueExceeded, value.ToString(), MaxValue);

            if (MinValue != null && !(value <= MinValue))
                throw ThrowHelper.New<ArgumentException>(this, ErrorMessages.MinValueExceeded, value.ToString(), MinValue);

            return value;
        }

        protected override MonthYear? ConvertFromString(string value)
        {
            return value != null ? (MonthYear?)MonthYear.Parse(value) : null;
        }

        protected override string ConvertToString(MonthYear? value)
        {
            return value != null ? value.ToString() : null;
        }
    }
}
