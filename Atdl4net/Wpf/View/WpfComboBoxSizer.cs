#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using Atdl4net.Model.Collections;
using Atdl4net.Model.Elements;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Atdl4net.Wpf.View
{
    public class WpfComboBoxSizer : DependencyObject
    {
        private Typeface _exampleTypeface;
        private double _exampleFontSize;
        private readonly Dictionary<string, double> _desiredSizeTable = new Dictionary<string, double>();

        public ComboBox ExampleComboBox { get; set; }
        public double InitialComboWidth { get; set; }

        public void RegisterComboBox(string name, ListItemCollection listItems)
        {
            if (_exampleTypeface == null)
            {
                _exampleTypeface = new Typeface(ExampleComboBox.FontFamily, 
                    ExampleComboBox.FontStyle, ExampleComboBox.FontWeight, ExampleComboBox.FontStretch);
                _exampleFontSize = ExampleComboBox.FontSize;
            }           
            
            double desiredWidth = 0;

            foreach (ListItem_t item in listItems)
            {
                FormattedText text = new FormattedText(item.UiRep,
                    CultureInfo.CurrentCulture, FlowDirection.LeftToRight, _exampleTypeface, _exampleFontSize, Brushes.Black);

                desiredWidth = Math.Max(text.Width, desiredWidth);
            }

            _desiredSizeTable[name] = InitialComboWidth + desiredWidth;
        }

        public void Clear()
        {
            _desiredSizeTable.Clear();
        }

        public double this[string controlId]
        {
            get { return _desiredSizeTable[controlId]; }
        }
    }
}
