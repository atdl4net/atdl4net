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
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Common.Logging;

namespace Atdl4net.Wpf.View.Controls
{
    /// <summary>
    /// Provides the base class for numeric spinner controls.
    /// </summary>
    public class NumericSpinnerControlBase : UserControl
    {
        /// <summary>
        /// Logger for this class.
        /// </summary>
        protected static readonly ILog _log = LogManager.GetLogger("Atdl4net.Wpf.View.Controls");

        private bool _textChangeInProgress;

        /// <summary>
        /// Dependency property that provides storage for the InnerIncrement property.
        /// </summary>
        /// <summary>
        /// Dependency property that provides storage for the output value of this control.  May be null.
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(decimal?), typeof(NumericSpinnerControlBase), new FrameworkPropertyMetadata(OnValuePropertyChanged));

        /// <summary>
        /// Dependency property that provides storage for the current text for this control.  May or may not be a valid number.
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(NumericSpinnerControlBase), new FrameworkPropertyMetadata(OnTextPropertyChanged));

        /// <summary>
        /// Dependency property that provides storage for the current format provider for this control.  By default, the culture invariant format provider is used.
        /// </summary>
        public static readonly DependencyProperty FormatProviderProperty =
            DependencyProperty.Register("FormatProvider", typeof(IFormatProvider), typeof(NumericSpinnerControlBase), new PropertyMetadata(CultureInfo.InvariantCulture));

        /// <summary>
        /// Dependency property that provides storage for the validity state of this control.
        /// </summary>
        public static readonly DependencyProperty IsContentValidProperty =
            DependencyProperty.Register("IsContentValid", typeof(bool), typeof(NumericSpinnerControlBase), new PropertyMetadata(true));

        /// <summary>
        /// Initializes a new <see cref="NumericSpinnerControlBase"/> instance.
        /// </summary>
        public NumericSpinnerControlBase()
        {
            _textChangeInProgress = false;
        }

        /// <summary>
        /// Gets/sets the output value of this control.
        /// </summary>
        public decimal? Value
        {
            get { return (decimal?)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Gets/sets the format provider for this control.
        /// </summary>
        public IFormatProvider FormatProvider
        {
            get { return (IFormatProvider)GetValue(FormatProviderProperty); }
            set { SetValue(FormatProviderProperty, value); }
        }

        /// <summary>
        /// Gets/sets the text value of this control.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets the validity state of this control.
        /// </summary>
        public bool IsContentValid
        {
            get { return (bool)GetValue(IsContentValidProperty); }
            set { SetValue(IsContentValidProperty, value); }
        }

        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            _log.Debug(m => m("Value property changed for numeric spinner control; value is now {0}", e.NewValue ?? "null"));

            (d as NumericSpinnerControlBase).OnValuePropertyChanged((decimal?)e.NewValue);
        }

        private void OnValuePropertyChanged(decimal? newValue)
        {
            if (!_textChangeInProgress)
            {
                if (newValue == null)
                    Text = string.Empty;
                else
                    Text = ((decimal)newValue).ToString(FormatProvider);
            }
        }

        private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as NumericSpinnerControlBase).OnTextPropertyChanged(e.NewValue as string);
        }

        private void OnTextPropertyChanged(string newValue)
        {
            try
            {
                _textChangeInProgress = true;

                _log.Debug(m => m("Text property changed for numeric spinner control; value is now '{0}'", newValue));

                decimal decimalValue;

                if (string.IsNullOrEmpty(newValue))
                {
                    Value = null;

                    UpdateIsContentValid(true);
                }
                else
                {
                    if (decimal.TryParse(newValue, out decimalValue))
                    {
                        Value = decimalValue;

                        UpdateIsContentValid(true);
                    }
                    else
                    {
                        Value = null;

                        UpdateIsContentValid(false);
                    }
                }
            }
            finally
            {
                _textChangeInProgress = false;
            }
        }

        private void UpdateIsContentValid(bool value)
        {
            _log.Debug(m => m("Updating IsContentValid for numeric spinner to {0}", value.ToString().ToLower()));

            IsContentValid = value;
        }
    }
}
