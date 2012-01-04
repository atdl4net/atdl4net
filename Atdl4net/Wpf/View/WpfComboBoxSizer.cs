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
