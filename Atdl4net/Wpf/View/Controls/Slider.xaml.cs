#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Atdl4net.Wpf.ViewModel;

namespace Atdl4net.Wpf.View.Controls
{
    /// <summary>
    /// Represents a FIXatdl slider control for WPF.
    /// </summary>
    /// <remarks>This control is implemented using a standard WPF Slider and a series of TextBlocks, positioned
    /// using the measuring algorithm in the private UpdateListItems method.  An alternative implementation using 
    /// WPF UniformGrid was tried, but the custom algorithm was found to give better layout results.</remarks>
    public partial class Slider : UserControl, INotifyPropertyChanged
    {
        private bool _selectedIndexChangeInProgress = false;

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(ViewModelListItemCollection), typeof(Slider), new FrameworkPropertyMetadata(OnListItemsChanged));

        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(string), typeof(Slider), new FrameworkPropertyMetadata(OnSelectedValueChanged));

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public Slider()
        {
            InitializeComponent();
        }

        public ViewModelListItemCollection ItemsSource
        {
            get { return (ViewModelListItemCollection)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public string SelectedValue
        {
            get { return (string)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        public int SelectedIndex
        {
            get
            {
                if (ItemsSource != null)
                    return ItemsSource.GetFirstSelectedEnumIdIndex();

                return -1;
            }

            set
            {
                try
                {
                    _selectedIndexChangeInProgress = true;

                    if (ItemsSource != null && value >= 0 && value < ItemsSource.Count)
                        SelectedValue = ItemsSource[value].EnumId;
                }
                finally
                {
                    _selectedIndexChangeInProgress = false;
                }

            }
        }

        private static void OnListItemsChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            (dependencyObject as Slider).LayoutControl(e.NewValue as ViewModelListItemCollection);
        }

        private static void OnSelectedValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            (dependencyObject as Slider).OnSelectedValueChanged(e.NewValue as string);
        }

        private void OnSelectedValueChanged(string enumId)
        {
            if (!_selectedIndexChangeInProgress && ItemsSource != null)
            {
                SelectedIndex = ItemsSource.GetIndexOfEnumId(enumId);

                NotifyPropertyChanged("SelectedIndex");
            }
        }

        private void NotifyPropertyChanged(string name)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;

            if (propertyChanged !=null)
                propertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void LayoutControl(ViewModelListItemCollection items)
        {
            double desiredWidth = 0;

            int numItems = items.Count;

            Typeface _typeface = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);

            double[] widths = new double[items.Count];

            double adjacentWidthDiff = double.MaxValue;

            for (int n = 0; n < items.Count; n++)
            {
                FormattedText text = new FormattedText(items[n].UiRep,
                    CultureInfo.CurrentCulture, FlowDirection.LeftToRight, _typeface, FontSize, Brushes.Black);

                widths[n] = text.Width;
                desiredWidth = Math.Max(text.Width, desiredWidth);

                if (n > 0)
                    adjacentWidthDiff = Math.Min(adjacentWidthDiff, Math.Abs(widths[n] - widths[n - 1]));
            }

            // The value '10' was determined by trial-and-error, to give a balance between too much gap between
            // adjacent labels, and too little when two adjacent labels are both long.
            double internalMargin = Math.Max(10 - adjacentWidthDiff, 1);

            double spacing = desiredWidth + internalMargin;

            labelArea.Children.Clear();

            for (int n = 0; n < items.Count; n++)
            {
                double offset = 0;

                if (n > 0)
                    offset = widths[0] / 2 + spacing * n - widths[n] / 2;

                labelArea.Children.Add(new Label() { Content = items[n].UiRep, Margin = new Thickness(offset, 0, 0, 0) });
            }

            sliderControl.Width = spacing * (numItems - 1) + internalMargin + 10;
            sliderControl.Maximum = numItems - 1;
        }
    }
}
