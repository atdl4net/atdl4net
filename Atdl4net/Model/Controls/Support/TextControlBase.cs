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

using System;
using System.Globalization;
using Atdl4net.Diagnostics;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Fix;
using Atdl4net.Model.Elements;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Model.Types.Support;
using Atdl4net.Resources;
using Common.Logging;

namespace Atdl4net.Model.Controls.Support
{
    /// <summary>
    /// Represents control elements within FIXatdl that can support textual information.  Applies to the following controls:
    /// <list type="bullet">
    /// <item><description><see cref="Atdl4net.Model.Controls.HiddenField_t"/></description></item>
    /// <item><description><see cref="Atdl4net.Model.Controls.Label_t"/></description></item>
    /// <item><description><see cref="Atdl4net.Model.Controls.TextField_t"/></description></item>
    /// </list>
    /// </summary>
    public abstract class TextControlBase : Control_t, IParameterConvertible
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Model.Controls");

        /// <summary>
        /// The state value for this control.
        /// </summary>
        protected string _value;

        /// <summary>
        /// Initializes a new instance of <see cref="TextControlBase"/> using the supplied ID.
        /// </summary>
        /// <param name="id">ID for this control.</param>
        protected TextControlBase(string id)
            : base(id)
        {
        }

        /// <summary>The value used to pre-populate the GUI component when the order entry screen is initially rendered.</summary>
        public string InitValue { get; set; }

        /// <summary>
        /// Loads the InitValue for this control into the control value.
        /// </summary>
        public override void LoadDefault()
        {
            SetValue(InitValue);
        }

        /// <summary>
        /// Sets the value of this control; either via a string or using the FIXatdl '{NULL}' value.
        /// </summary>
        /// <param name="newValue">Valid string or null (meaning do not send this value over FIX).
        /// May also contain the FIXatdl '{NULL}' value as a string.</param>
        public override void SetValue(object newValue)
        {
            if (newValue is string)
            {
                string value = newValue as string;

                if (value == Atdl.NullValue)
                    _value = null;
                else
                    _value = value;
            }
            else if (newValue == null)
                _value = null;
            else
                throw ThrowHelper.New<InternalErrorException>(this, InternalErrors.UnexpectedArgumentType,
                    newValue.GetType().FullName, "System.String");

            _log.Debug(m => m("Control value is now '{0}'", _value ?? "null"));
        }

        #region Control_t Overrides

        /// <summary>
        /// Sets the value of this control using the value of the supplied parameter.
        /// </summary>
        /// <param name="parameter">Parameter to set this control's value from.</param>
        public override void SetValueFromParameter(IParameter parameter)
        {
            IControlConvertible value = parameter.GetValueForControl();

            _value = value.ToString();

            _log.Debug(m => m("Text control {0} value is now {1}", Id, _value));
        }

        /// <summary>
        /// Gets the value of this control.  May be null.
        /// </summary>
        /// <returns>This control's value (as a string) or null (meaning do not send this value over FIX).</returns>
        public override object GetCurrentValue()
        {
            return _value;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable boolean value.
        /// </summary>
        /// <param name="targetParameter">Target parameter for this conversion.</param>
        /// <returns>One of true, false or null which is equivalent to the value of this instance.</returns>
        public override bool? ToBoolean(IParameter targetParameter)
        {
            if (string.IsNullOrEmpty(_value))
                return null;

            bool result;

            if (bool.TryParse(_value, out result))
                return result;

            throw ThrowHelper.New<InvalidCastException>(this, ErrorMessages.InvalidBooleanValue, _value, bool.TrueString.ToLower(), bool.FalseString.ToLower());
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable decimal value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="targetParameter">Target parameter for this conversion.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A nullable decimal equivalent to the value of this instance.</returns>
        public override decimal? ToDecimal(IParameter targetParameter, IFormatProvider provider)
        {
            decimal result = 0;

            return TryConvertToDecimal(_value, out result) ? (decimal?)result : null;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 32-bit signed integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="targetParameter">Target parameter for this conversion.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A nullable 32-bit signed integer equivalent to the value of this instance.</returns>
        public override int? ToInt32(IParameter targetParameter, IFormatProvider provider)
        {
            int result = 0;

            return TryConvertToInt(_value, out result) ? (int?)result : null;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 32-bit unsigned integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="targetParameter">Target parameter for this conversion.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A nullable 32-bit unsigned integer equivalent to the value of this instance.</returns>
        public override uint? ToUInt32(IParameter targetParameter, IFormatProvider provider)
        {
            uint result = 0;

            return TryConvertToUint(_value, out result) ? (uint?)result : null;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent char value.
        /// </summary>
        /// <param name="targetParameter">Target parameter for this conversion.</param>
        /// <returns>A nullable char value equivalent to the value of this instance.  May be null.</returns>
        public override char? ToChar(IParameter targetParameter)
        {
            char result = char.MinValue;

            return TryConvertToChar(_value, out result) ? (char?)result : null;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent string value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="targetParameter">Target parameter for this conversion.</param>
        /// <returns>A string value equivalent to the value of this instance.  May be null.</returns>
        public override string ToString(IParameter targetParameter)
        {
            return _value;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable DateTime value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="targetParameter">Target parameter for this conversion.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A nullable DateTime equivalent to the value of this instance.</returns>
        public override DateTime? ToDateTime(IParameter targetParameter, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(_value))
                return null;

            DateTime result;

            // Try loose parsing using the supplied locale, and if that fails, try all the supported FIX formats.
            if (!DateTime.TryParse(_value, provider, DateTimeStyles.AllowWhiteSpaces, out result) &&
                !DateTime.TryParseExact(_value, FixDateTimeFormat.AllFormats, provider, DateTimeStyles.AllowWhiteSpaces, out result))
                throw ThrowHelper.New<InvalidCastException>(this, ErrorMessages.InvalidDateOrTimeValue, _value);
           
            return result;
        }

        /// <summary>
        /// Indicates whether the control has enumerated state (i.e., its state is held internally in an <see cref="EnumState"/> which
        /// requires special conversion, or if instead a regular value conversion is appropriate).
        /// </summary>
        public override bool HasEnumeratedState { get { return false; } }

        #endregion
    }
}
