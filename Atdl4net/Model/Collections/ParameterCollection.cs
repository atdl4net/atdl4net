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

using System.Collections.ObjectModel;
using Atdl4net.Fix;
using Atdl4net.Model.Elements.Support;

namespace Atdl4net.Model.Collections
{
    /// <summary>
    /// Collection class for FIXatdl parameters, keyed on parameter name.
    /// </summary>
    public class ParameterCollection : KeyedCollection<string, IParameter>, ISimpleDictionary<IParameter>
    {
        /// <summary>
        /// Gets the key for the supplied item.
        /// </summary>
        /// <param name="parameter">Parameter to get key (name) for.</param>
        /// <returns>Key (name of parameter).</returns>
        protected override string GetKeyForItem(IParameter parameter)
        {
            return parameter.Name;
        }

        /// <summary>
        /// Loads this set of parameters with the supplied FIX values.
        /// </summary>
        /// <param name="initialValues"><see cref="FixTagValuesCollection"/> containing the FIX values to initialize from.</param>
        /// <param name="resetNonSuppliedParameters">Set to true if each parameter value is to be reset if a corresponding value is 
        /// not specified in inputValues; set to false to leave the parameter value unchanged.</param>
        public void LoadInitialValues(FixTagValuesCollection initialValues, bool resetNonSuppliedParameters)
        {
            string value;

            foreach (IParameter parameter in this.Items)
            {
                if (parameter.FixTag != null && initialValues.TryGetValue((FixTag)parameter.FixTag, out value))
                    parameter.WireValue = value;
                else if (resetNonSuppliedParameters)
                    parameter.Reset();
            }
        }

        /// <summary>
        /// Gets the complete set of FIX values for the strategy that this parameter collection belongs to.
        /// </summary>
        /// <returns><see cref="FixTagValuesCollection"/> that contains the FIX values corresponding to this set of
        /// parameters.</returns>
        public FixTagValuesCollection GetOutputValues()
        {
            FixTagValuesCollection output = new FixTagValuesCollection();

            foreach (IParameter parameter in this.Items)
            {
                if (parameter.FixTag != null && parameter.WireValue != null)
                    output.Add((FixTag)parameter.FixTag, parameter.WireValue);
            }

            return output;
        }

        /// <summary>
        /// Resets each parameter value to an empty value.
        /// </summary>
        public void ResetAll()
        {
            foreach (IParameter parameter in this.Items)
                parameter.Reset();
        }
    }
}
