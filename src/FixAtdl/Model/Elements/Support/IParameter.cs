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
using Atdl4net.Fix;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Enumerations;
using Atdl4net.Model.Types.Support;
using Atdl4net.Validation;

namespace Atdl4net.Model.Elements.Support
{
    /// <summary>
    /// Common interface for all Parameter_t types.  This interface has members for all the attributes of the FIXatdl Parameter_t
    /// type that are common across all FIXatdl types (Amt_t, Boolean_t, etc.), allowing each parameter to be treated as
    /// a common type (IParameter) regardless of the type parameter used to create the <see cref="Parameter_t{T}"/> for
    /// the FIXatdl parameter.
    /// </summary>
    public interface IParameter : IValueProvider
    {
        /// <summary>
        /// Initializes this parameter's value to a newly created instance of the type parameter.  This method is used if
        /// the parameter is being used within a strategy subsequent to its creation, say for an order amendment.
        /// </summary>
        void Reset();

        /// <summary>Gets/sets the DefinedByFIX property, which indicates whether the parameter is a redefinition of a 
        /// standard FIX tag. The default value is false.</summary>
        bool? DefinedByFix { get; set; }

        /// <summary>
        /// Gets/sets the enum pairs for this parameter.  Although it doesn't necessarily make sense in all cases, all
        /// parameter types within FIXatdl may contain an EnumPairs element, so we must support it at the base level.
        /// </summary>
        /// <value>The enum pairs.</value>
        EnumPairCollection EnumPairs { get; }

        /// <summary>
        /// Indicates whether the parameter has an EnumPairs element with at least one sub-element.
        /// </summary>
        bool HasEnumPairs { get; }

        /// <summary>Gets or sets the FIX tag for this parameter, i.e., the tag that will hold the value of the 
        /// parameter. Required when parameter value is intended to be transported over the wire.  If fixTag is not 
        /// provided then the Strategies-level attribute, tag957Support, must be set to true, indicating that the 
        /// order recipient expects to receive algo parameters in the StrategyParameterGrp repeating group beginning 
        /// at tag 957.  <b>NB Atdl4net does not currently support usage of the StrategyParameterGrp element.</b></summary>
        /// <value>The FIX tag to use.</value>
        FixTag? FixTag { get; set; }

        /// <summary>Indicates whether this parameter’s value can be modified by an Order Cancel/Replace Request message.
        /// The default value for this field is true.
        /// </summary>
        bool? MutableOnCxlRpl { get; set; }

        /// <summary>The name of this parameter.</summary>
        /// <remarks>No two parameters of any strategy may have the same name. The name may be used as a unique key when referenced 
        /// from the other sub-schemas. Names must begin with an alpha character followed only by alpha-numeric characters 
        /// and must not contain whitespace characters.</remarks>
        string Name { get; set; }

        /// <summary>Indicates how to interpret those tags that were populated in an original order but are not populated in
        /// a subsequent cancel/replace of the order message. If this value is true then revert to the value of the original 
        /// order, otherwise a null value or the parameter’s default value (Control/@initValue) is to be used or if none is
        /// specified, the parameter is to be omitted.  The default value for this field is false.<br/>
        /// </summary>
        /// <remarks>Although revertOnCxlRpl and mutableOnCxlRpl might appear to be mutually exclusive, this is not strictly
        /// the case, and as the default value for mutableOnCxlRpl is 'true', it is recommended practice to explicitly include
        /// mutableOnCxlRpl="false" if the option revertOnCxlRpl="true" is set for a given parameter (assuming of course this 
        /// is the intended behaviour).</remarks>
        bool? RevertOnCxlRpl { get; set; }

        /// <summary>
        /// Gets or sets the type name of this parameter.
        /// </summary>
        /// <value>The type name (one of Amt_t, Boolean_t, Char_t, etc.).</value>
        string Type { get; set; }

        /// <summary>Indicates whether a parameter is optional or required. Valid values are "optional" and "required".
        /// The default value for this field is "optional".
        /// </summary>
        Use_t Use { get; set; }

        /// <summary>
        /// Gets this parameter's value in a form suitable for populating its respective control.
        /// </summary>
        /// <returns>An <see cref="IControlConvertible"/> that can be used to convert this parameter into the appropriate
        /// form for the target control.</returns>
        IControlConvertible GetValueForControl();

        /// <summary>
        /// Sets the value of this parameter using the Control_t that references it.  The resulting parameter value may be
        /// null if the control is not set to a value, or if it has explicitly been set via a state rule to {NULL}.
        /// </summary>
        /// <param name="control">Control to extract this parameter's value from.</param>
        ValidationResult SetValueFromControl(Control_t control);

        /// <summary>
        /// Gets/sets the wire value of this parameter.
        /// </summary>
        string WireValue { get; set; }

        /// <summary>
        /// Indicates whether this parameter has been set to a value other than null.
        /// </summary>
        bool IsSet { get; }
    }
}
