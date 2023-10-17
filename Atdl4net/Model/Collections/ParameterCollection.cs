#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
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
