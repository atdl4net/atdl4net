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

using System.Windows;
using System.Windows.Input;

namespace Atdl4net.Wpf.View.Controls
{
    /// <summary>
    /// Represents a SingleSpinner control for WPF.
    /// </summary>
    public partial class SingleSpinner : NumericSpinnerControlBase
    {
        private const decimal DefaultIncrement = 1;

        /// <summary>
        /// Dependency property that provides storage for the Increment property of this control.
        /// </summary>
        public static readonly DependencyProperty IncrementProperty =
            DependencyProperty.Register("Increment", typeof(decimal), typeof(SingleSpinner), new FrameworkPropertyMetadata(DefaultIncrement));

        /// <summary>
        /// Initializes a new <see cref="SingleSpinner"/> instance.
        /// </summary>
        public SingleSpinner()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets/sets the Increment property of this control.
        /// </summary>
        public decimal Increment
        {
            get { return (decimal)GetValue(IncrementProperty); }
            set { SetValue(IncrementProperty, value); }
        }

        #region Private Methods

        private void DecrementValue()
        {
            if (!IsContentValid)
                return;

            decimal value = Value ?? 0;

            value -= Increment;

            Value = value;
        }

        private void IncrementValue()
        {
            if (!IsContentValid)
                return;

            decimal value = Value ?? 0;

            value += Increment;

            Value = value;
        }

        private void upButton_Click(object sender, RoutedEventArgs e)
        {
            IncrementValue();
        }

        private void downButton_Click(object sender, RoutedEventArgs e)
        {
            DecrementValue();
        }

        private void value_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Down)
                DecrementValue();
            else if (e.Key == Key.Up)
                IncrementValue();
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

        #endregion
    }
}
