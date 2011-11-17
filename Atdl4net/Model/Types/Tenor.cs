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
using Atdl4net.Resources;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Types
{
    public struct Tenor
    {
        private enum TenorTypeValue
        {
            Invalid = 0,
            Day,
            Week,
            Month,
            Year
        }

        private const string ExceptionContext = "Tenor";

        private int Offset;
        private TenorTypeValue TenorType;

        public static bool operator <=(Tenor lhs, Tenor rhs)
        {
            if (lhs.TenorType != rhs.TenorType)
                throw ThrowHelper.New<NotSupportedException>(ExceptionContext, ErrorMessages.UnsupportedComparisonOperation, lhs, rhs);

            return lhs.Offset <= rhs.Offset;
        }

        public static bool operator >=(Tenor lhs, Tenor rhs)
        {
            if (lhs.TenorType != rhs.TenorType)
                throw ThrowHelper.New<NotSupportedException>(ExceptionContext, ErrorMessages.UnsupportedComparisonOperation, lhs, rhs);

            return lhs.Offset >= rhs.Offset;
        }

        public static bool operator ==(Tenor lhs, Tenor rhs)
        {
            return lhs.Offset == rhs.Offset && lhs.TenorType == rhs.TenorType;
        }

        public static bool operator !=(Tenor lhs, Tenor rhs)
        {
            return lhs.Offset != rhs.Offset || lhs.TenorType != rhs.TenorType;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Tenor))
                return false;

            return this == (Tenor)obj;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static Tenor Parse(string value)
        {
            Tenor result = new Tenor();

            if (value.Length >= 2)
            {
                switch (value[0])
                {
                    case 'D':
                        result.TenorType = TenorTypeValue.Day;
                        break;

                    case 'W':
                        result.TenorType = TenorTypeValue.Week;
                        break;

                    case 'M':
                        result.TenorType = TenorTypeValue.Month;
                        break;

                    case 'Y':
                        result.TenorType = TenorTypeValue.Year;
                        break;
                }

                string number = value.Substring(1);

                try
                {
                    result.Offset = Convert.ToInt32(number);

                    if (result.TenorType != TenorTypeValue.Invalid)
                        return result;
                }
                catch (FormatException ex)
                {
                    throw ThrowHelper.New<ArgumentException>(ExceptionContext, ex, ErrorMessages.InvalidTenorValue, value);
                }
            }

            throw ThrowHelper.New<ArgumentException>(ExceptionContext, ErrorMessages.InvalidMonthYearValue, value);
        }

        public override string ToString()
        {
            switch (TenorType)
            {
                case TenorTypeValue.Day:
                    return string.Format("D{1}", Offset);

                case TenorTypeValue.Week:
                    return string.Format("W{1}", Offset);

                case TenorTypeValue.Month:
                    return string.Format("M{1}", Offset);

                default:
                    return string.Format("Y{1}", Offset);
            }
        }
    }
}