#region Copyright (c) 2010, Cornerstone Technology Limited. http://atdl4net.org
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
//      License as published by the Free Software Foundation, version 3.
// 
//      Atdl4net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
//      of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU Lesser General Public License for more details.
//
//      You should have received a copy of the GNU Lesser General Public License along with Atdl4net.  If not, see
//      http://www.gnu.org/licenses/.
//
#endregion

using Atdl4net.Resources;
using System;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Types
{
    /// <summary>
    /// 'char field containing one of two values: "Y" = True/Yes; "N" = False/No.'
    /// </summary>
    public class Boolean_t : NonEnumableValueType<bool>
    {
        private const string DefaultTrueValue = "Y";
        private const string DefaultFalseValue = "N";

        /// <summary>Gets or sets the false wire value (for use with Boolean type parameters).<br/>
        /// <b>This attribute is targeted for deprecation.</b><br/>
        /// Defines the value with which to populate the FIX message when the boolean parameter is False. Overrides the 
        /// standard FIX boolean value of “N”. I.e. if this attribute is not provided then the order-sending application 
        /// must use “N”.<br/>
        /// If it is desired that the FIX message is not to be populated with this tag when the value of the parameter is 
        /// false, then falseWireValue should be defined as “{NULL}”.</summary>
        public string FalseWireValue { get; set; }

        /// <summary>
        /// Applicable only when xsi:type is Boolean_t.
        /// This attribute is targeted for deprecation.
        /// To achieve the same functionality, it is recommended that a Char_t or String_t type parameter be used instead 
        /// of a Boolean_t. The parameter should have two EnumPairs defined with one defining the false wire-value and the
        /// other defining the true wire-value. The parameter should be bound to a CheckBox control. The CheckBox control
        /// should define the parameters checkedEnumRef and uncheckedEnumRef to refer to the enumIDs of the parameter.
        /// See the section “A Sample FIXatdl Document” in this document for an example. (See the section “A Sample FIXatdl
        /// Document” in this document for an example. Examine the Parameter “AllowDarkPoolExec” and Control “DPOption” 
        /// for details.)
        /// The deprecated use is described as follows:
        /// Defines the value with which to populate the FIX message when the boolean parameter is True. Overrides the 
        /// standard FIX boolean value of “Y”. I.e. if this attribute is not provided then the order-sending application
        /// must use “Y”.
        /// If it is desired that the FIX message is not to be populated with this tag when the value of the parameter 
        /// is true, then trueWireValue should be defined as “{NULL}”.
        /// </summary>
        public string TrueWireValue { get; set; }

        protected override bool? ValidateValue(bool? value)
        {
            return value;
        }

        protected override bool? ConvertFromString(string value)
        {
            string trueValue = (TrueWireValue != null) ? TrueWireValue : DefaultTrueValue;
            string falseValue = (FalseWireValue != null) ? FalseWireValue : DefaultFalseValue;

            bool? result = null;

            if (value != null)
            {
                if (value == trueValue)
                    result = true;
                else if (value == falseValue)
                    result = false;
                else
                    throw ThrowHelper.New<ArgumentException>(this, ErrorMessages.InvalidBooleanValue, value, trueValue, falseValue);
            }

            return result;
        }

        protected override string ConvertToString(bool? value)
        {
            return (bool)value ? (TrueWireValue ?? DefaultTrueValue) : (FalseWireValue ?? DefaultFalseValue);
        }
    }
}
