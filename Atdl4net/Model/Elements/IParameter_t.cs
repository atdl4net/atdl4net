#region Copyright (c) 2010-2011, Cornerstone Technology Limited. http://atdl4net.org
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
using Atdl4net.Fix;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Enumerations;

namespace Atdl4net.Model.Elements
{
    /// <summary>
    /// Common interface for all Parameter_t types.
    /// </summary>
    public interface IParameter_t : IValueProvider
    {
        /// <summary>Gets or sets the DefinedByFIX property.<br/>
        /// Indicates whether the parameter is a redefinition of a standard FIX tag. The default value is False.</summary>
        bool? DefinedByFix { get; set; }

        /// <summary>
        /// Gets or sets the enum pair for this parameter.
        /// </summary>
        /// <value>The enum pair.</value>
        EnumPairCollection EnumPairs { get; }

        /// <summary>Gets or sets the FIX tag.<br/>
        /// The tag that will hold the value of the parameter. Required when parameter value is intended to be transported 
        /// over the wire.  If fixTag is not provided then the Strategies-level attribute, tag957Support, must be set to 
        /// true, indicating that the order recipient expects to receive algo parameters in the StrategyParameterGrp 
        /// repeating group beginning at tag 957.</summary>
        /// <value>The FIX tag to use.</value>
        FixTag? FixTag { get; set; }

        /// <summary></summary>
        bool? MutableOnCxlRpl { get; set; }

        /// <summary></summary>
        string Name { get; set; }

        /// <summary></summary>
        bool? RevertOnCxlRpl { get; set; }

        /// <summary>
        /// Gets or sets the type name of this parameter.
        /// </summary>
        /// <value>The type name.</value>
        string Type { get; set; }

        /// <summary></summary>
        Use_t Use { get; set; }

        object ControlValue { get; set; }

        string WireValue { get; set; }

        bool IsFloat { get; }

        bool IsInteger { get; }

        void Reset();
    }
}
