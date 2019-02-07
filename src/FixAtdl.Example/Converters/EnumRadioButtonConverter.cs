#region Atdl4net Sample Code - License and Use
//
//   This sample code is provided as part of Atdl4net, with the intention of making it easier to get started.
//
//   Whilst Atdl4net is itself made available under either a commercial or an open-source (LGPL) license, the
//   samples provided with Atdl4net are made available for use freely by anyone that obtains a copy of
//   Atdl4net, without restriction.
//
//   For the avoidance of doubt, you are at liberty to remove this statement from any sample code that you
//   adapt for your use, but in any case the following statement still applies:
//
//   The samples for Atdl4net are distributed in the hope that they will be useful, but WITHOUT ANY WARRANTY; 
//   without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
//
#endregion

using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Atdl4net.ExampleApplication.Converters
{
    /// <summary>
    /// Converter for converting between a set of radio buttons and an enum type.
    /// </summary>
    public class EnumRadioButtonConverter : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns Nothing, the valid null value is used.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string parameterString = parameter as string;

            if (parameterString == null)
                return DependencyProperty.UnsetValue;

            if (!Enum.IsDefined(value.GetType(), value))
                return DependencyProperty.UnsetValue;

            object parameterValue = Enum.Parse(value.GetType(), parameterString);

            return parameterValue.Equals(value);
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns Nothing, the valid null value is used.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string parameterString = parameter as string;

            if (parameterString == null || value.Equals(false)) 
                return Binding.DoNothing;

            return Enum.Parse(targetType, parameterString);
        }

        #endregion
    }
}
