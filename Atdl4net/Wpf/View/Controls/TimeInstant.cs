#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
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
