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

namespace Atdl4net.Wpf.View.Controls
{
    internal struct TimeInstant
    {
        public static readonly TimeInstant StartOfDay = new TimeInstant(0, 0);
        public static readonly TimeInstant EndOfDay = new TimeInstant(23, 59);

        public int Minutes;
        public int Hours;

        public bool IsEmpty { get; set; }

        public TimeInstant(int hours, int minutes)
            : this()
        {
            IsEmpty = false;
            Hours = hours;
            Minutes = minutes;
        }

        public void IncrementMinutes()
        {
            if (Minutes < 59)
                Minutes++;
            else
                Minutes = 0;
        }

        public void DecrementMinutes()
        {
            if (Minutes > 0)
                Minutes--;
            else
                Minutes = 59;
        }

        public void IncrementHours()
        {
            if (Hours < 23)
                Hours++;
            else
                Hours = 0;
        }

        public void DecrementHours()
        {
            if (Hours > 0)
                Hours--;
            else
                Hours = 23;
        }

        public static bool operator <(TimeInstant lhs, TimeInstant rhs)
        {
            return (lhs.Hours < rhs.Hours) || (lhs.Hours == rhs.Hours && lhs.Minutes <= rhs.Minutes);
        }

        public static bool operator >(TimeInstant lhs, TimeInstant rhs)
        {
            return (lhs.Hours > rhs.Hours) || (lhs.Hours == rhs.Hours && lhs.Minutes >= rhs.Minutes);
        }

        public DateTime? ToDateTime()
        {
            if (IsEmpty)
                return null;

            DateTime today = DateTime.Now;

            return new DateTime(today.Year, today.Month, today.Day, Hours, Minutes, 0);
        }

        public void FromDateTime(DateTime? value)
        {
            if (value == null)
                IsEmpty = true;
            else
            {
                IsEmpty = false;
                Hours = ((DateTime)value).Hour;
                Minutes = ((DateTime)value).Minute;
            }
        }

        public static bool MinutesAreDifferent(TimeInstant lhs, TimeInstant rhs)
        {
            if (lhs.IsEmpty ^ rhs.IsEmpty)
                return true;

            if (lhs.IsEmpty && rhs.IsEmpty)
                return false;

            return (lhs.Minutes != rhs.Minutes);
        }

        public static bool HoursAreDifferent(TimeInstant lhs, TimeInstant rhs)
        {
            if (lhs.IsEmpty ^ rhs.IsEmpty)
                return true;

            if (lhs.IsEmpty && rhs.IsEmpty)
                return false;

            return (lhs.Hours != rhs.Hours);
        }
    }
}
