using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Atdl4net.Wpf.View.Controls
{
    /// <summary>
    /// Represents the time picker control which is used to support the FIXatdl Clock_t control type.
    /// </summary>
    public partial class TimePicker : UserControl, INotifyPropertyChanged
    {
        private bool _minutesHasFocus;
        private TimeInstant _value = new TimeInstant() { IsEmpty = true };

        /// <summary>
        /// Underlying dependency property for this control's Time property.
        /// </summary>
        public static readonly DependencyProperty TimeProperty = 
            DependencyProperty.Register("Time", typeof(DateTime?), typeof(TimePicker), new PropertyMetadata(new PropertyChangedCallback(OnTimeChanged)));

        /// <summary>
        /// Initializes a new instance of the TimePicker control.
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

        private static void OnTimeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            TimePicker target = dependencyObject as TimePicker;

            DateTime? oldValue = (DateTime?)e.OldValue;
            DateTime? newValue = (DateTime?)e.NewValue;

            TimeInstant oldTime = oldValue != null ? new TimeInstant(((DateTime)oldValue).Hour, ((DateTime)oldValue).Minute) : new TimeInstant();
            target._value = newValue != null ? new TimeInstant(((DateTime)newValue).Hour, ((DateTime)newValue).Minute) : new TimeInstant();

            target.NotifyMinutesPropertyChanged(oldTime, target._value);
            target.NotifyHoursPropertyChanged(oldTime, target._value);
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

        /// <summary>
        /// Gets or sets the minutes value. Used for typing in a value for minutes.
        /// </summary>
        /// <value>The minutes.</value>
        public string Minutes
        {
            get { return _value.IsEmpty ? string.Empty : _value.Minutes.ToString("D2"); }

            set
            {
                try
                {
                    int minutes = Convert.ToInt32(value);

                    if (minutes >= 0 && minutes <= 59)
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
                    }
                }
                catch (FormatException)
                {
                }
            }
        }


        /// <summary>
        /// Gets or sets the hours. Used for typing in a value for hours.
        /// </summary>
        /// <value>The hours.</value>
        public string Hours
        {
            get { return _value.IsEmpty ? string.Empty : _value.Hours.ToString(); }

            set
            {
                try
                {
                    int hours = Convert.ToInt32(value);

                    if (hours >= 0 && hours <= 23)
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
                    }
                }
                catch (FormatException)
                {
                }
            }
        }

        private void IncrementHours()
        {
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
            PropertyChangedEventHandler propertyChanged = PropertyChanged;

            if (propertyChanged != null && TimeInstant.HoursAreDifferent(oldValue, newValue))
            {
                propertyChanged(this, new PropertyChangedEventArgs("Hours"));

                this.SetValue(TimeProperty, _value.ToDateTime());
            }
        }

        private void NotifyMinutesPropertyChanged(TimeInstant oldValue, TimeInstant newValue)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;

            if (propertyChanged != null && TimeInstant.MinutesAreDifferent(oldValue, newValue))
            {
                propertyChanged(this, new PropertyChangedEventArgs("Minutes"));

                this.SetValue(TimeProperty, _value.ToDateTime());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
