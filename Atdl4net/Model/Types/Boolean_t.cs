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
using Atdl4net.Model.Collections;
using Atdl4net.Model.Controls.Support;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Model.Types.Support;
using Atdl4net.Resources;
using Atdl4net.Validation;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Types
{
    /// <summary>
    /// Represents a char field containing one of two values: "Y" = True/Yes; "N" = False/No.'
    /// </summary>
    public class Boolean_t : AtdlValueType<bool>, IControlConvertible
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

        #region AtdlValueType<T> Overrides

        /// <summary>
        /// Validates the supplied value in terms of the parameters constraints.  This method does nothing because
        /// is not possible for a boolean value to be invalid.
        /// </summary>
        /// <param name="value">Value to validate, may be null in which case no validation is applied.</param>
        /// <returns>ValidationResult indicating whether the supplied value is valid.</returns>
        protected override ValidationResult ValidateValue(bool? value)
        {
            return ValidationResult.ValidResult;
        }

        /// <summary>
        /// Converts the supplied value from string format (as might be used on the FIX wire) into the type of the type
        /// parameter for this type.
        /// </summary>
        /// <param name="value">Type to convert from string; cannot be null as empty fields are invalid in FIX.</param>
        /// <returns>Value converted from a string.</returns>
        protected override bool? ConvertFromWireValueFormat(string value)
        {
            string trueValue = (TrueWireValue != null) ? TrueWireValue : DefaultTrueValue;
            string falseValue = (FalseWireValue != null) ? FalseWireValue : DefaultFalseValue;

            bool? result = null;

            if (value == trueValue)
                result = true;
            else if (value == falseValue)
                result = false;
            else
                throw ThrowHelper.New<ArgumentException>(this, ErrorMessages.InvalidBooleanValue, value, trueValue, falseValue);

            return result;
        }

        /// <summary>
        /// Converts the supplied value to a string, as might be used on the FIX wire.
        /// </summary>
        /// <param name="value">Value to convert, may be null.</param>
        /// <returns>If input value is not null, returns value converted to a string; null otherwise.</returns>
        protected override string ConvertToWireValueFormat(bool? value)
        {
            if (value == null)
                return null;

            bool actualValue = (bool)value;

            if ((actualValue && TrueWireValue == Atdl.NullValue) || (!actualValue && FalseWireValue == Atdl.NullValue))
                return null;

            return actualValue ? (TrueWireValue ?? DefaultTrueValue) : (FalseWireValue ?? DefaultFalseValue);
        }

        /// <summary>
        /// Converts the supplied value to the type parameter type (T?) for this class.
        /// </summary>
        /// <param name="hostParameter"><see cref="IParameter"/> that hosts this value.</param>
        /// <param name="value">Value to convert, may be null.</param>
        /// <returns>If input value is not null, returns value converted to T?; null otherwise.</returns>
        protected override bool? ConvertToNativeType(IParameter hostParameter, IParameterConvertible value)
        {
            return value.ToBoolean(hostParameter);
        }

        /// <summary>
        /// Gets the human-readable type name for use in error messages shown to the user.
        /// </summary>
        /// <returns>Human-readable type name.</returns>
        protected override string GetHumanReadableTypeName()
        {
            return HumanReadableTypeNames.BooleanType;
        }

        #endregion

        #region IControlConvertible Members

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable boolean value.
        /// </summary>
        /// <returns>One of true, false or null which is equivalent to the value of this instance.</returns>
        public bool? ToBoolean()
        {
            return _value;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent string value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A string value equivalent to the value of this instance.  May be null.</returns>
        public string ToString(IFormatProvider provider)
        {
            return _value != null ? ((bool)_value).ToString().ToLower() : null;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable decimal value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A nullable decimal equivalent to the value of this instance.</returns>
        public decimal? ToDecimal()
        {
            throw ThrowHelper.New<InvalidCastException>(this, ErrorMessages.UnsupportedParameterValueConversion, _value, "Decimal");
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable DateTime value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A nullable DateTime equivalent to the value of this instance.</returns>
        public DateTime? ToDateTime()
        {
            throw ThrowHelper.New<InvalidCastException>(this, ErrorMessages.UnsupportedParameterValueConversion, _value, "DateTime");
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent EnumState value.
        /// </summary>
        /// <returns>A valid EnumState, assuming the source value can be correctly converted.</returns>
        public EnumState ToEnumState(EnumPairCollection enumPairs)
        {
            throw ThrowHelper.New<InvalidCastException>(this, ErrorMessages.UnsupportedParameterValueConversion, _value, "Enumerated Type");
        }

        #endregion
    }
}
