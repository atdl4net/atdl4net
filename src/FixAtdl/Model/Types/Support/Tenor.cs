#region Copyright (c) 2010-2012, Cornerstone Technology Limited. http://atdl4net.org
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

namespace Atdl4net.Model.Types.Support
{
    /// <summary>
    /// Represents the Tenor type within FIX/FIXatdl.
    /// </summary>
    public struct Tenor : IComparable
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

        /// <summary>
        /// Serves as a hash function for this type.  Overridden because Equals(object) is overridden.
        /// </summary>
        /// <returns>A hash code for the current Object.</returns>
        /// <remarks>The value 251 is used here because it is a prime number, helpful for generating unique hash values.</remarks>
        public override int GetHashCode()
        {
            return (Offset * 251) + (int)TenorType;
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

            throw ThrowHelper.New<ArgumentException>(ExceptionContext, ErrorMessages.InvalidTenorValue, value);
        }

        public override string ToString()
        {
            switch (TenorType)
            {
                case TenorTypeValue.Day:
                    return string.Format("D{0}", Offset);

                case TenorTypeValue.Week:
                    return string.Format("W{0}", Offset);

                case TenorTypeValue.Month:
                    return string.Format("M{0}", Offset);

                default:
                    return string.Format("Y{0}", Offset);
            }
        }

        #region IComparable Members

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates 
        /// whether the current instance precedes, follows, or occurs in the same position in the sort order as the 
        /// other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings:
        /// <list type="bullet">
        /// <item><description>Less than zero - this instance precedes obj in the sort order.</description></item></list>
        /// <item><description></description>Zero - this instance occurs in the same position in the sort order as obj.</item></list>
        /// <item><description>Greater than zero - this instance follows obj in the sort order.</description></item></list>
        /// </returns>
        public int CompareTo(object obj)
        {
            // Null references are by definition less than the current instance.
            if (obj == null)
                return 1;

            if (!(obj is Tenor))
                throw ThrowHelper.New<ArgumentException>(this, InternalErrors.UnexpectedArgumentType, obj.GetType().FullName, this.GetType().FullName);

            Tenor rhs = (Tenor)obj;

            if (rhs == this)
                return 0;

            return rhs <= this ? 1 : -1;
        }

        #endregion
    }
}