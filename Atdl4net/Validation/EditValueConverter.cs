#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Globalization;
using System.Linq;
using Atdl4net.Diagnostics;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Fix;
using Atdl4net.Model.Reference;
using Atdl4net.Model.Types.Support;
using Atdl4net.Resources;
using Atdl4net.Utility;

namespace Atdl4net.Validation
{
    /// <summary>
    /// Provides value conversion for <see cref="Edit_t"/> evaluation.
    /// </summary>
    public static class EditValueConverter
    {
        private static readonly string ExceptionContext = typeof(EditValueConverter).FullName;

        /// <summary>
        /// Attempts to convert the second parameter value to a comparable type of the first parameter.
        /// </summary>
        /// <param name="typeInstanceToMatch">Instance of a the target comparable type.</param>
        /// <param name="value">Value to convert.</param>
        /// <returns>Converted value as an <see cref="IComparable"/>.</returns>
        /// <exception cref="InvalidCastException">Thrown if the value cannot be converted to the target type.</exception>
        /// <exception cref="FormatException">Thrown if the value cannot be converted into a valid numeric type.</exception>
        public static IComparable ConvertToComparableType(object typeInstanceToMatch, string value)
        {
            // If we don't have a valid type to convert to, then best leave the value alone.
            if (typeInstanceToMatch == null)
                return value;

            string type = typeInstanceToMatch.GetType().FullName;

            switch (type)
            {
                case "System.Decimal":
                    return Convert.ToDecimal(value);

                case "System.Boolean":
                    return ConvertToBool(value);

                case "System.Int32":
                    return Convert.ToInt32(value);

                case "System.UInt32":
                    return Convert.ToUInt32(value);

                case "System.Char":
                    return Convert.ToChar(value);

                case "System.DateTime":
                    return FixDateTime.Parse(value, CultureInfo.InvariantCulture);

                case "System.String":
                    return value;

                case "Atdl4net.Model.Reference.IsoCountryCode":
                    return value.ParseAsEnum<IsoCountryCode>();

                case "Atdl4net.Model.Reference.IsoCurrencyCode":
                    return value.ParseAsEnum<IsoCurrencyCode>();

                case "Atdl4net.Model.Reference.IsoLanguageCode":
                    return value.ParseAsEnum<IsoLanguageCode>();

                case "Atdl4net.Model.Types.Support.MonthYear":
                    return MonthYear.Parse(value);

                case "Atdl4net.Model.Types.Support.Tenor":
                    return Tenor.Parse(value);

                case "Atdl4net.Model.Controls.Support.EnumState":
                    return value;

                default:
                    throw ThrowHelper.New<InvalidCastException>(ExceptionContext, ErrorMessages.DataConversionError1, value, type);
            }
        }

        private static bool ConvertToBool(string value)
        {
            if (value == null)
                throw ThrowHelper.New<InvalidFieldValueException>(ExceptionContext, ErrorMessages.IllegalUseOfNullError);

            switch (value.ToUpper())
            {
                case "N":
                    return false;

                case "Y":
                    return true;

                default:
                    return Boolean.Parse(value);
            }
        }
    }
}
