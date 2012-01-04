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
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Fix;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Controls.Support;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Model.Enumerations;
using Atdl4net.Model.Types.Support;
using Atdl4net.Resources;
using Atdl4net.Validation;
using Common.Logging;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Elements
{
    /// <summary>
    /// Represents a Parameter_t type.
    /// </summary>
    /// <typeparam name="T">Valid FIXatdl type <see cref="Atdl4net.Model.Types"/></typeparam>
    /// <example>To create a parameter with underlying type Amt_t, use <c>new Parameter_t&lt;Amt_t&gt;</c>.</example>
    public class Parameter_t<T> : IParameter where T : IParameterType, new()
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Model.Elements");

        private readonly EnumPairCollection _enumPairs = new EnumPairCollection();

        /// <summary>
        /// The underlying value of this parameter.
        /// </summary>
        protected T _value;

        /// <summary>
        /// Creates a new instance of <see cref="Parameter_t{T}"/>.
        /// </summary>
        /// <param name="name">Name of this parameter.  See <see cref="Parameter{T}.Name"/> for constraints on parameter names.</param>
        public Parameter_t(string name)
        {
            _log.DebugFormat("New Parameter_t<{0}> created, Name='{1}'.", typeof(T).Name, name);

            Name = name;
            Type = typeof(T).Name;

            // Set FIXatdl defaults
            Use = Use_t.Optional;
            MutableOnCxlRpl = true;

            _value = new T();
        }

        /// <summary>
        /// Value accessor.  <b>This property is intended to be used for deserialization purposes only.</b>
        /// </summary>
        public T Value { get { return _value; } }

        #region IParameter Members

        /// <summary>
        /// Initializes this parameter's value to a newly created instance of the type parameter.  This method is used if
        /// the parameter is being used within a strategy subsequent to its creation, say for an order amendment.
        /// </summary>
        public void Reset()
        {
            _log.DebugFormat("Parameter_t<{0}> {1} reset.", typeof(T).Name, Name);

            _value = new T();
        }

        /// <summary>Gets/sets the DefinedByFIX property, which indicates whether the parameter is a redefinition of a 
        /// standard FIX tag. The default value is false.</summary>
        public bool? DefinedByFix { get; set; }

        /// <summary>
        /// Gets/sets the enum pairs for this parameter.  Although it doesn't necessarily make sense in all cases, all
        /// parameter types within FIXatdl may contain an EnumPairs element, so we must support it at the base level.
        /// </summary>
        /// <value>The enum pairs.  Will be an empty collection if no enum pairs were present in the parameter definition.</value>
        public EnumPairCollection EnumPairs { get { return _enumPairs; } }

        /// <summary>
        /// Indicates whether the parameter has an EnumPairs element with at least one sub-element.
        /// </summary>
        public bool HasEnumPairs { get { return _enumPairs.Count != 0; } }

        /// <summary>Gets or sets the FIX tag for this parameter, i.e., the tag that will hold the value of the 
        /// parameter. Required when parameter value is intended to be transported over the wire.  If fixTag is not 
        /// provided then the Strategies-level attribute, tag957Support, must be set to true, indicating that the 
        /// order recipient expects to receive algo parameters in the StrategyParameterGrp repeating group beginning 
        /// at tag 957.  <b>NB Atdl4net does not currently support usage of the StrategyParameterGrp element.</b></summary>
        /// <value>The FIX tag to use.</value>
        public FixTag? FixTag { get; set; }

        /// <summary>Indicates whether this parameter’s value can be modified by an Order Cancel/Replace Request message.
        /// The default value for this field is true.
        /// </summary>
        public bool? MutableOnCxlRpl { get; set; }

        /// <summary>The name of this parameter.</summary>
        /// <remarks>No two parameters of any strategy may have the same name. The name may be used as a unique key when referenced 
        /// from the other sub-schemas. Names must begin with an alpha character followed only by alpha-numeric characters 
        /// and must not contain whitespace characters.</remarks>
        public string Name { get; set; }

        /// <summary>Indicates how to interpret those tags that were populated in an original order but are not populated in
        /// a subsequent cancel/replace of the order message. If this value is true then revert to the value of the original 
        /// order, otherwise a null value or the parameter’s default value (Control/@initValue) is to be used or if none is
        /// specified, the parameter is to be omitted.  The default value for this field is false.<br/>
        /// </summary>
        /// <remarks>Although revertOnCxlRpl and mutableOnCxlRpl might appear to be mutually exclusive, this is not strictly
        /// the case, and as the default value for mutableOnCxlRpl is 'true', it is recommended practice to explicitly include
        /// mutableOnCxlRpl="false" if the option revertOnCxlRpl="true" is set for a given parameter (assuming of course this 
        /// is the intended behaviour).</remarks>
        public bool? RevertOnCxlRpl { get; set; }

        /// <summary>
        /// Gets or sets the type name of this parameter.
        /// </summary>
        /// <value>The type name (one of Amt_t, Boolean_t, Char_t, etc.).</value>
        public string Type { get; set; }

        /// <summary>Indicates whether a parameter is optional or required. Valid values are "optional" and "required".
        /// The default value for this field is "optional".
        /// </summary>
        public Use_t Use { get; set; }

        /// <summary>
        /// Gets the value of this parameter as seen by the Control_t that references it.  May be null if the 
        /// parameter has no value, for example if it has explicitly been set via a state rule to {NULL}.
        /// </summary>
        /// <remarks>An <see cref="IControlConvertible"/> is returned enabling the parameter value to be converted into any 
        /// desired type, provided that the underlying value supports that type.</remarks>
        public IControlConvertible GetValueForControl()
        {
            return _value.GetValueForControl(this);
        }

        /// <summary>
        /// Sets the value of this parameter using the Control_t that references it.  The resulting parameter value may be
        /// null if the control is not set to a value, or if it has explicitly been set via a state rule to {NULL}.
        /// </summary>
        /// <param name="control">Control to extract this parameter's new value from.</param>
        public ValidationResult SetValueFromControl(Control_t control)
        {
            IParameterConvertible value = control.GetValueForParameter();

            try
            {
                return _value.SetValueFromControl(this, value);
            }
            catch (Atdl4netException ex)
            {
                throw ThrowHelper.Rethrow(this, ex, ErrorMessages.UnsuccessfulSetParameterOperation, Name, control.Id, ex.Message);
            }
        }

        /// <summary>
        /// Gets/sets the wire value of this parameter.
        /// </summary>
        public string WireValue
        {
            get { return _value.GetWireValue(this); }

            set
            {
                // Wire value of null is not allowed (as it is equivalent to writing FIX tag=<SOH>)
                if (value == null)
                    throw ThrowHelper.New<ArgumentNullException>(this, ErrorMessages.IllegalUseOfNullError);

                _value.SetWireValue(this, value);
            }
        }

        #endregion

        #region IValueProvider Members

        /// <summary>
        /// Gets the current value of this parameter.  Used only for StrategyEdit_t evaluation.
        /// </summary>
        /// <returns>Current value of this parameter as an object.</returns>
        public object GetCurrentValue()
        {
            return _value.GetNativeValue(true);
        }

        #endregion
    }
}
