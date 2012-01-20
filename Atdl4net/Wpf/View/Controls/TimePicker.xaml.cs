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
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Common.Logging;

namespace Atdl4net.Wpf.View.Controls
{
    /// <summary>
    /// Represents the time picker control which is used to support the FIXatdl Clock_t control type.
    /// </summary>
    public partial class TimePicker : UserControl, INotifyPropertyChanged
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Wpf.View.Controls");

        private bool _minutesHasFocus;
        private TimeInstant _value = new TimeInstant() { IsEmpty = true };

        /// <summary>
        /// Dependency property that provides storage for this control's Time property.
        /// </summary>
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(DateTime?), typeof(TimePicker), new PropertyMetadata(new PropertyChangedCallback(OnTimeChanged)));

        /// <summary>
        /// Dependency property that provides storage for the validity state of this control.
        /// </summary>
        public static readonly DependencyProperty IsContentValidProperty =
            DependencyProperty.Register("IsContentValid", typeof(bool), typeof(TimePicker), new PropertyMetadata(true));

        /// <summary>
        /// Raised whenever a property of interest has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimePicker"/> control.
        /// </summary>
        public TimePicker()
        {
            InitializeComponent();

            _minutesHasFocus = false;
        }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>The time.</value>
        public DateTime? Time
        {
            get { return (DateTime?)this.GetValue(TimeProperty); }

            set
            {
                this.SetValue(TimeProperty, value);

                TimeInstant prevValue = _value;

                if (value == null)
                    _value.IsEmpty = true;
                else
                    _value.FromDateTime((DateTime)value);

                NotifyMinutesPropertyChanged(prevValue, _value);
                NotifyHoursPropertyChanged(prevValue, _value);
            }
        }

        /// <summary>
        /// Gets the validity state of this control.
        /// </summary>
        public bool IsContentValid
        {
            get { return (bool)GetValue(IsContentValidProperty); }
            set { SetValue(IsContentValidProperty, value); }
        }

        /// <summary>
        /// Gets or sets the minutes value. Used for typing in a value for minutes.
        /// </summary>
        /// <value>The minutes.</value>
        public string Minutes
        {
            get { return _value.IsEmpty ? string.Empty : _value.Minutes.ToString("D2"); }

            set
            {
                int minutes;

                if (int.TryParse(value, out minutes) && (minutes >= 0 && minutes <= 59))
                {
                    TimeInstant prevValue = _value;

                    _value.Minutes = minutes;

                    if (_value.IsEmpty)
                    {
                        _value.IsEmpty = false;
                        _value.Hours = 0;

                        NotifyHoursPropertyChanged(prevValue, _value);
                    }

                    NotifyMinutesPropertyChanged(prevValue, _value);

                    UpdateIsContentValid(true);
                }
                else
                    UpdateIsContentValid(false);
            }
        }

        /// <summary>
        /// Gets or sets the hours. Used for typing in a value for hours.
        /// </summary>
        /// <value>The hours.</value>
        public string Hours
        {
            get { return _value.IsEmpty ? string.Empty : _value.Hours.ToString("D2"); }

            set
            {
                int hours;

                if (int.TryParse(value, out hours) && (hours >= 0 && hours <= 23))
                {
                    TimeInstant prevValue = _value;

                    _value.Hours = hours;

                    if (_value.IsEmpty)
                    {
                        _value.IsEmpty = false;
                        _value.Minutes = 0;

                        NotifyMinutesPropertyChanged(prevValue, _value);
                    }

                    NotifyHoursPropertyChanged(prevValue, _value);

                    UpdateIsContentValid(true);
                }
                else
                    UpdateIsContentValid(false);
            }
        }

        #region Private Methods

        private static void OnTimeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            (dependencyObject as TimePicker).OnTimeChanged((DateTime?)e.OldValue, (DateTime?)e.NewValue);
        }

        private void OnTimeChanged(DateTime? oldValue, DateTime? newValue)
        {
            TimeInstant oldTime = oldValue != null ? new TimeInstant(((DateTime)oldValue).Hour, ((DateTime)oldValue).Minute) : new TimeInstant();
            
            _value = newValue != null ? new TimeInstant(((DateTime)newValue).Hour, ((DateTime)newValue).Minute) : new TimeInstant();

            NotifyMinutesPropertyChanged(oldTime, _value);
            NotifyHoursPropertyChanged(oldTime, _value);
        }

        private void upButton_Click(object sender, RoutedEventArgs e)
        {
            if (_minutesHasFocus)
                IncrementMinutes();
            else
                IncrementHours();
        }

        private void hours_GotFocus(object sender, RoutedEventArgs e)
        {
            _minutesHasFocus = false;
        }

        private void minutes_GotFocus(object sender, RoutedEventArgs e)
        {
            _minutesHasFocus = true;
        }

        private void downButton_Click(object sender, RoutedEventArgs e)
        {
            if (_minutesHasFocus)
                DecrementMinutes();
            else
                DecrementHours();
        }

        private void minutes_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Down)
                DecrementMinutes();
            else if (e.Key == Key.Up)
                IncrementMinutes();
        }

        private void hours_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
                DecrementHours();
            else if (e.Key == Key.Up)
                IncrementHours();
        }

        private void upButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                upButton_Click(sender, null);

                e.Handled = true;
            }
            else if (e.Key == Key.Down)
            {
                downButton_Click(sender, null);

                e.Handled = true;
            }
        }

        private void downButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                downButton_Click(sender, null);

                e.Handled = true;
            }
            else if (e.Key == Key.Up)
            {
                upButton_Click(sender, null);

                e.Handled = true;
            }
        }

        private void IncrementHours()
        {
            if (!IsContentValid)
                return;

            TimeInstant prevValue = _value;

            if (_value.IsEmpty)
            {
                _value = TimeInstant.StartOfDay;

                NotifyMinutesPropertyChanged(prevValue, _value);
            }

            _value.IncrementHours();

            NotifyHoursPropertyChanged(prevValue, _value);
        }

        private void IncrementMinutes()
        {
            if (!IsContentValid)
                return;

            TimeInstant prevValue = _value;

            if (_value.IsEmpty)
            {
                _value = TimeInstant.StartOfDay;

                NotifyHoursPropertyChanged(prevValue, _value);
            }

            _value.IncrementMinutes();

            NotifyMinutesPropertyChanged(prevValue, _value);
        }

        private void DecrementHours()
        {
            if (!IsContentValid)
                return;

            TimeInstant prevValue = _value;

            if (_value.IsEmpty)
            {
                _value = TimeInstant.EndOfDay;

                NotifyMinutesPropertyChanged(prevValue, _value);
            }

            _value.DecrementHours();

            NotifyHoursPropertyChanged(prevValue, _value);
        }

        private void DecrementMinutes()
        {
            if (!IsContentValid)
                return;

            TimeInstant prevValue = _value;

            if (_value.IsEmpty)
            {
                _value = TimeInstant.EndOfDay;

                NotifyHoursPropertyChanged(prevValue, _value);
            }

            _value.DecrementMinutes();

            NotifyMinutesPropertyChanged(prevValue, _value);
        }

        private void NotifyHoursPropertyChanged(TimeInstant oldValue, TimeInstant newValue)
        {
            // Notify when changed, but also when hours is zero, as that is the starting value
            if (TimeInstant.HoursAreDifferent(oldValue, newValue) || newValue.Hours == 0)
            {
                NotifyPropertyChanged("Hours");

                this.SetValue(TimeProperty, _value.ToDateTime());
            }
        }

        private void NotifyMinutesPropertyChanged(TimeInstant oldValue, TimeInstant newValue)
        {
            // Notify when changed, but also when minutes is zero, as that is the starting value
            if (TimeInstant.MinutesAreDifferent(oldValue, newValue) || newValue.Minutes == 0)
            {
                NotifyPropertyChanged("Minutes");

                this.SetValue(TimeProperty, _value.ToDateTime());
            }
        }

        private void UpdateIsContentValid(bool value)
        {
            _log.Debug(m => m("Updating IsContentValid for time picker to {0}", value.ToString().ToLower()));

            IsContentValid = value;
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;

            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
