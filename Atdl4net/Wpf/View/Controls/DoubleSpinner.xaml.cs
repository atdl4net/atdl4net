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
    /// Represents a DoubleSpinner control for WPF.
    /// </summary>
    public partial class DoubleSpinner : NumericSpinnerControlBase
    {
        private const decimal DefaultInnerIncrement = 1;
        private const decimal DefaultOuterIncrement = 0.01m;

        /// <summary>
        /// Dependency property that provides storage for the InnerIncrement property.
        /// </summary>
        public static readonly DependencyProperty InnerIncrementProperty =
            DependencyProperty.Register("InnerIncrement", typeof(decimal), typeof(DoubleSpinner), new FrameworkPropertyMetadata(DefaultInnerIncrement));

        /// <summary>
        /// Dependency property that provides storage for the OuterIncrement property.
        /// </summary>
        public static readonly DependencyProperty OuterIncrementProperty =
            DependencyProperty.Register("OuterIncrement", typeof(decimal), typeof(DoubleSpinner), new FrameworkPropertyMetadata(DefaultOuterIncrement));

        /// <summary>
        /// Initializes a new <see cref="DoubleSpinner"/> instance.
        /// </summary>
        public DoubleSpinner()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets/sets the inner increment.
        /// </summary>
        public decimal InnerIncrement
        {
            get { return (decimal)GetValue(InnerIncrementProperty); }
            set { SetValue(InnerIncrementProperty, value); }
        }

        /// <summary>
        /// Gets/sets the outer increment.
        /// </summary>
        public decimal OuterIncrement
        {
            get { return (decimal)GetValue(OuterIncrementProperty); }
            set { SetValue(OuterIncrementProperty, value); }
        }

        #region Private Methods

        private void InnerDecrementValue()
        {
            if (!IsContentValid)
                return;

            decimal value = Value ?? 0;

            value -= InnerIncrement;
            
            Value = value;
        }

        private void InnerIncrementValue()
        {
            if (!IsContentValid)
                return;

            decimal value = Value ?? 0;
            
            value += InnerIncrement;
            
            Value = value;
        }

        private void OuterDecrementValue()
        {
            if (!IsContentValid)
                return;

            decimal value = Value ?? 0;
            
            value -= OuterIncrement;
            
            Value = value;
        }

        private void OuterIncrementValue()
        {
            if (!IsContentValid)
                return;

            decimal value = Value ?? 0;
            
            value += OuterIncrement;
            
            Value = value;
        }

        private void value_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Down)
                InnerDecrementValue();
            else if (e.Key == Key.Up)
                InnerIncrementValue();
        }

        private void innerUpButton_Click(object sender, RoutedEventArgs e)
        {
            InnerIncrementValue();
        }

        private void innerDownButton_Click(object sender, RoutedEventArgs e)
        {
            InnerDecrementValue();
        }

        private void innerUpButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                innerUpButton_Click(sender, null);

                e.Handled = true;
            }
            else if (e.Key == Key.Down)
            {
                innerDownButton_Click(sender, null);

                e.Handled = true;
            }
        }

        private void innerDownButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                innerDownButton_Click(sender, null);

                e.Handled = true;
            }
            else if (e.Key == Key.Up)
            {
                innerUpButton_Click(sender, null);

                e.Handled = true;
            }
        }

        private void outerUpButton_Click(object sender, RoutedEventArgs e)
        {
            OuterIncrementValue();
        }

        private void outerDownButton_Click(object sender, RoutedEventArgs e)
        {
            OuterDecrementValue();
        }

        private void outerUpButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                outerUpButton_Click(sender, null);

                e.Handled = true;
            }
            else if (e.Key == Key.Down)
            {
                outerDownButton_Click(sender, null);

                e.Handled = true;
            }
        }

        private void outerDownButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                outerDownButton_Click(sender, null);

                e.Handled = true;
            }
            else if (e.Key == Key.Up)
            {
                outerUpButton_Click(sender, null);

                e.Handled = true;
            }
        }

        #endregion
    }
}
