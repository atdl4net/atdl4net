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
using System.Globalization;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Types
{
    public struct MonthYear
    {
        private const string ExceptionContext = "MonthYear";

        ushort Year;
        ushort Month;
        ushort? Day;
        ushort? Week;

        public static bool operator ==(MonthYear lhs, MonthYear rhs)
        {
            return lhs.Year == rhs.Year && lhs.Month == rhs.Month && lhs.Day == rhs.Day && lhs.Month == rhs.Month;
        }

        public static bool operator !=(MonthYear lhs, MonthYear rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return (MonthYear)obj == this;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator <=(MonthYear lhs, MonthYear rhs)
        {
            if (lhs.Year > rhs.Year)
                return false;

            if (lhs.Year < rhs.Year)
                return true;

            // Years equal...
            if (lhs.Month > rhs.Month)
                return false;

            if (lhs.Month < rhs.Month)
                return true;

            // Years and months equal...
            if (lhs.Day == null && rhs.Day == null && lhs.Week == null && lhs.Week == null)
                return true;

            if (lhs.Day != null && rhs.Day != null)
                return lhs.Day <= rhs.Day;

            if (lhs.Week != null && lhs.Week != null)
                return lhs.Week <= rhs.Week;

            throw ThrowHelper.New<NotSupportedException>(ExceptionContext, ErrorMessages.UnsupportedComparisonOperation, lhs, rhs);
        }

        public static bool operator >=(MonthYear lhs, MonthYear rhs)
        {
            if (lhs.Year < rhs.Year)
                return false;

            if (lhs.Year > rhs.Year)
                return true;

            // Years equal...
            if (lhs.Month < rhs.Month)
                return false;

            if (lhs.Month > rhs.Month)
                return true;

            // Years and months equal...
            if (lhs.Day == null && rhs.Day == null && lhs.Week == null && lhs.Week == null)
                return true;

            if (lhs.Day != null && rhs.Day != null)
                return lhs.Day >= rhs.Day;

            if (lhs.Week != null && lhs.Week != null)
                return lhs.Week >= rhs.Week;

            throw ThrowHelper.New<NotSupportedException>(ExceptionContext, ErrorMessages.UnsupportedComparisonOperation, lhs, rhs);
        }

        public static MonthYear Parse(string value)
        {
            MonthYear result = new MonthYear();

            bool suffixValid = false;

            // Note: This is probably better done with a regex at some point.
            if (value.Length == 8)
            {
                string suffix = value.Substring(6, 2);

                if (suffix[0] == 'w')
                    result.Week = ValidateRange(suffix[1].ToString(), 1, 5);
                else
                    result.Day = ValidateRange(suffix, 1, 31);

                suffixValid = true;
            }

            if (value.Length == 6 || (value.Length == 8 && suffixValid))
            {
                result.Year = ValidateRange(value.Substring(0, 4), 0, 9999);
                result.Month = ValidateRange(value.Substring(4, 2), 1, 12);

                return result;
            }

            throw ThrowHelper.New<ArgumentException>(ExceptionContext, ErrorMessages.InvalidMonthYearValue, value);
        }

        private static ushort ValidateRange(string value, int lowerBound, int upperBound)
        {
            try
            {
                ushort numValue = Convert.ToUInt16(value);

                if (numValue >= lowerBound && numValue <= upperBound)
                    return numValue;

                throw ThrowHelper.New<ArgumentException>(ExceptionContext, ErrorMessages.InvalidMonthYearValue, value);
            }
            catch (FormatException ex)
            {
                throw ThrowHelper.New<ArgumentException>(ExceptionContext, ex, ErrorMessages.InvalidMonthYearValue, value);
            }
        }

        public override string ToString()
        {
            string suffix = Week != null ? string.Format("w{0}", Week) : (Day != null ? string.Format("{0:00}", Day) : string.Empty);

            return string.Format("{0:0000}{1:00}{2}", Year, Month, suffix);
        }
    }
}