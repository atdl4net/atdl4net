#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
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
