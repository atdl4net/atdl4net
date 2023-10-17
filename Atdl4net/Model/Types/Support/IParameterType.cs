#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using Atdl4net.Model.Controls.Support;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Validation;

namespace Atdl4net.Model.Types.Support
{
    /// <summary>
    /// Interface that all FIXatdl parameter types (Amt_t, Boolean_t, etc.) must support.
    /// </summary>
    public interface IParameterType
    {
        /// <summary>
        /// Gets the value of this parameter as seen by the Control_t that references it.  May be null if the 
        /// parameter has no value, for example if it has explicitly been set via a state rule to {NULL}.
        /// </summary>
        /// <param name="hostParameter"><see cref="IParameter"/> that hosts the value.</param>
        /// <remarks>An <see cref="IControlConvertible"/> is returned enabling the parameter value to be converted into any 
        /// desired type, provided that the underlying value supports that type.</remarks>
        IControlConvertible GetValueForControl(IParameter hostParameter);

        /// <summary>
        /// Sets the value of this parameter as seen by the Control_t that references it.
        /// </summary>
        /// <param name="hostParameter"><see cref="IParameter"/> that hosts the value.</param>
        /// <param name="value">Control value that implements <see cref="IParameterConvertible"/>.</param>
        /// <remarks>An <see cref="IParameterConvertible"/> is passed in enabling the control value to be converted into any 
        /// desired type, provided that the value supports conversion to that type.</remarks>
        ValidationResult SetValueFromControl(IParameter hostParameter, IParameterConvertible value);

        /// <summary>
        /// Sets the wire value for this parameter.  This method is typically used to initialise the parameter through the
        /// InitValue mechanism, but may also be used to initialise the parameter when doing order amendments.
        /// </summary>
        /// <param name="hostParameter"><see cref="Parameter_t{T}"/> that is hosting this type.  Parameters in Atdl4net are
        /// represented by means of the generic Parameter_t type with the appropriate type parameter, for example, Parameter_t&lt;Amt_t&gt;.</param>
        /// <param name="value">New wire value (all wire values in Atdl4net are strings).</param>
        void SetWireValue(IParameter hostParameter, string value);

        /// <summary>
        /// Gets the wire value for this parameter.  This method is used to retrieve the value of the parameter that should
        /// be transmitted over FIX.
        /// </summary>
        /// <param name="hostParameter"><see cref="Parameter_t{T}"/> that is hosting this type.  Parameters in Atdl4net are
        /// represented by means of the generic Parameter_t type with the appropriate type parameter, for example, Parameter_t&lt;Amt_t&gt;.</param>
        /// <returns>The parameter's current wire value (all wire values in Atdl4net are strings).</returns>
        string GetWireValue(IParameter hostParameter);

        /// <summary>
        /// Gets the value of this parameter type in its native (i.e., raw) form, such as int, char, string, etc. 
        /// </summary>
        /// <param name="applyWireValueFormat">If set to true, the value returned is adjusted to be in the 'format'
        /// it would be if sent on the FIX wire.  For example, for Float_t parameters, setting this value to true
        /// would cause the Precision attribute setting to be applied.</param>
        /// <returns>Native parameter value.</returns>
        object GetNativeValue(bool applyWireValueFormat);

        /// <summary>
        /// Gets the human-readable name of this type.
        /// </summary>
        string HumanReadableTypeName { get; }

        /// <summary>
        /// Resets this parameter value to its default state.
        /// </summary>
        void Reset();

        /// <summary>
        /// Indicates whether this parameter value has been set to a value other than null.
        /// </summary>
        bool IsSet { get; }
    }
}
