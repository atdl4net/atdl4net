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
using System.Linq;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Elements;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Model.Types;
using Common.Logging;

namespace Atdl4net.Fix
{
    /// <summary>
    /// Provides access to initial values for FIXatdl controls based on a set of input FIX fields.
    /// </summary>
    public class FixFieldValueProvider
    {
        private static readonly FixFieldValueProvider _emptyProvider = new FixFieldValueProvider(null, null);
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Fix");

        private readonly FixTagValuesCollection _fixValues;
        private readonly ParameterCollection _parameters;

        /// <summary>
        /// Initializes a new <see cref="FixFieldValueProvider"/> instance using the supplied set of input 
        /// values and parameters.
        /// </summary>
        /// <param name="fixValues">Input FIX fields to use.</param>
        /// <param name="parameters">Parameters to use.</param>
        public FixFieldValueProvider(FixTagValuesCollection fixValues, ParameterCollection parameters)
        {
            _fixValues = fixValues;
            _parameters = parameters;
        }

        /// <summary>
        /// Gets a static instance of an empty provider.
        /// </summary>
        public static FixFieldValueProvider Empty { get { return _emptyProvider; } }

        /// <summary>
        /// Gets the parameters for this value provider.
        /// </summary>
        public ParameterCollection Parameters { get { return _parameters; } }

        /// <summary>
        /// Gets the FIX values collection for this value provider.
        /// </summary>
        public FixTagValuesCollection FixValues { get { return _fixValues; } }

        /// <summary>
        /// Attempts to get the value of the specified FIX field (in FIX_ format), returning the value as a string.
        /// In the case of enumerated fields, the output parameter contains the EnumID, assuming a valid lookup was
        /// possible.
        /// </summary>
        /// <param name="fixField">FIX field value to retrieve, in FIX_ format.</param>
        /// <param name="targetParameterName">Target parameter for this field value.  May be null.</param>
        /// <param name="value">Contains the value of the FIX field if it could successfully be retrieved.</param>
        /// <returns>true if the field could be retrieved; false otherwise.</returns>
        public bool TryGetValue(string fixField, string targetParameterName, out string value)
        {
            string result = null;

            bool retrieved = TryGetValue(fixField, out result);

            if (retrieved && !string.IsNullOrEmpty(targetParameterName) && _parameters.Contains(targetParameterName))
            {
                IParameter parameter = _parameters[targetParameterName];

                if (parameter.HasEnumPairs)
                {
                    string wireValue = result;

                    _log.Debug(m => m("Attempting to find EnumID for FIX field {0} using parameter {1} with wire value '{2}'",
                        fixField, targetParameterName, wireValue));

                    retrieved = parameter.EnumPairs.TryParseWireValue(wireValue, out result);
                }
                else if (parameter is Parameter_t<Percentage_t>)
                    ProcessPercentageValue(parameter as Parameter_t<Percentage_t>, ref result);

                _log.Debug(m => m("FIX enumerated value lookup for field {0} returning {1}; value = '{2}'", fixField,
                    retrieved.ToString().ToLower(), retrieved ? result : "N/A"));
            }

            value = result;

            return retrieved;
        }

        /// <summary>
        /// Attempts to get the value of the specified FIX field (in FIX_ format), returning the value as a string.
        /// In the case of enumerated fields, the output parameter contains the EnumID, assuming a valid lookup was
        /// possible.
        /// </summary>
        /// <param name="fixField">FIX field value to retrieve, in FIX_ format.</param>
        /// <param name="value">Contains the value of the FIX field if it could successfully be retrieved.</param>
        /// <returns>true if the field could be retrieved; false otherwise.</returns>
        public bool TryGetValue(string fixField, out string value)
        {
            string result = null;

            bool retrieved = _fixValues != null && _fixValues.TryGetValue(fixField, out result);
            
            _log.Debug(m => m("FIX value lookup for field {0} returning {1}; value = '{2}'", fixField,
                retrieved.ToString().ToLower(), retrieved ? result : "N/A"));

            value = retrieved ? result : null;

            return retrieved;
        }

        private void ProcessPercentageValue(Parameter_t<Percentage_t> parameter, ref string value)
        {
            bool adjustmentNeeded = parameter.Value.MultiplyBy100 != true;

            if (adjustmentNeeded)
            {
                decimal decimalValue;

                if (decimal.TryParse(value, out decimalValue))
                    value = (decimalValue * 100).ToString("0.####");
                else
                    value = string.Empty;
            }
        }
    }
}
