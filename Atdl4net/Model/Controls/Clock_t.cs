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
using Atdl4net.Diagnostics;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Fix;
using Atdl4net.Model.Elements;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Model.Types.Support;
using Atdl4net.Resources;
using Common.Logging;

namespace Atdl4net.Model.Controls
{
    /// <summary>
    /// Represents the Clock_t control element within FIXatdl.
    /// </summary>
    public class Clock_t : Control_t
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Model.Controls");

        private DateTime? _value;

        /// <summary>
        /// Initializes a new instance of <see cref="Clock_t"/> using the supplied ID.
        /// </summary>
        /// <param name="id">ID for this control.</param>
        public Clock_t(string id)
            : base(id)
        {
            _log.Debug(m => m("New Clock_t created as control {0}", id));
        }

        // TODO: Implement LocalMktTz as a type.
        /// <summary>The timezone in which initValue is represented in.  Required when initValue is supplied. Applicable when 
        /// xsi:type is Clock_t.</summary>
        public string LocalMktTz { get; set; }

        /// <summary>The value used to pre-populate the GUI component when the order entry screen is initially rendered.</summary>
        public DateTime? InitValue { get; set; }

        /// <summary>Defines the treatment of initValue time. 0: use initValue; 1: use current time if initValue time has passed.
        /// The default value is 0.</summary>
        public int? InitValueMode { get; set; }

        #region Control_t Overrides

        /// <summary>
        /// Loads the InitValue for this control into the control value.
        /// </summary>
        public override void LoadDefault()
        {
            if (InitValue != null)
            {
                if (InitValueMode == 1)
                    _value = DateTime.Now > InitValue ? DateTime.Now : InitValue;
                else
                    _value = InitValue;
            }
            else
                _value = null;
        }

        /// <summary>
        /// Sets the value of this control using the value of the supplied parameter.
        /// </summary>
        /// <param name="parameter">Parameter to set this control's value from.</param>
        public override void SetValueFromParameter(IParameter parameter)
        {
            IControlConvertible value = parameter.GetValueForControl();

            _value = value.ToDateTime();

            _log.Debug(m => m("Clock_t control {0} value is now {1}", _value));
        }

        /// <summary>
        /// Sets the value of this control; either via a DateTime, or using the FIXatdl '{NULL}' value.  This method
        /// is either called indirectly from the user interface, or by a StateRule.
        /// </summary>
        /// <param name="newValue">Either a valid DateTime or null (meaning do not send this value over FIX).
        /// May also contain the FIXatdl '{NULL}' value as a string.</param>
        public override void SetValue(object newValue)
        {
            bool isString = newValue is string;
            bool isDateTime = newValue is DateTime;

            if (isString)
            {
                string value = newValue as string;

                if (value == Atdl.NullValue)
                    _value = null;
                else
                    throw ThrowHelper.New<InvalidFieldValueException>(this, ErrorMessages.InitControlValueError,
                        Id, string.Format("'{0}' is not a valid value for this control", value));
            }
            else if (isDateTime || newValue == null)
            {
                _value = (DateTime?)newValue;
            }
            else
                throw ThrowHelper.New<InternalErrorException>(this, InternalErrors.UnexpectedArgumentType,
                    newValue.GetType().FullName, "System.String, System.DateTime");
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable boolean value.
        /// </summary>
        /// <param name="targetParameter">Target parameter for this conversion.</param>
        /// <returns>One of true, false or null which is equivalent to the value of this instance.</returns>
        public override bool? ToBoolean(IParameter targetParameter)
        {
            throw ThrowHelper.New<InvalidCastException>(this, ErrorMessages.UnsupportedControlValueConversion, _value, "Boolean", Id);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable decimal value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="targetParameter">Target parameter for this conversion.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A nullable decimal equivalent to the value of this instance.</returns>
        public override decimal? ToDecimal(IParameter targetParameter, IFormatProvider provider)
        {
            throw ThrowHelper.New<InvalidCastException>(this, ErrorMessages.UnsupportedControlValueConversion, _value, "Decimal", Id);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 32-bit signed integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="targetParameter">Target parameter for this conversion.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A nullable 32-bit signed integer equivalent to the value of this instance.</returns>
        public override int? ToInt32(IParameter targetParameter, IFormatProvider provider)
        {
            throw ThrowHelper.New<InvalidCastException>(this, ErrorMessages.UnsupportedControlValueConversion, _value, "Int32", Id);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 32-bit unsigned integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="targetParameter">Target parameter for this conversion.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A nullable 32-bit unsigned integer equivalent to the value of this instance.</returns>
        public override uint? ToUInt32(IParameter targetParameter, IFormatProvider provider)
        {
            throw ThrowHelper.New<InvalidCastException>(this, ErrorMessages.UnsupportedControlValueConversion, _value, "UInt32", Id);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent char value.
        /// </summary>
        /// <param name="targetParameter">Target parameter for this conversion.</param>
        /// <returns>A nullable char value equivalent to the value of this instance.  May be null.</returns>
        public override char? ToChar(IParameter targetParameter)
        {
            throw ThrowHelper.New<InvalidCastException>(this, ErrorMessages.UnsupportedControlValueConversion, _value, "Char", Id);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent string value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="targetParameter">Target parameter for this conversion.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A string value equivalent to the value of this instance in the format YYYYMMDD-HH:MM:SS.  May be null.</returns>
        public override string ToString(IParameter targetParameter)
        {
            return _value != null ? ((DateTime)_value).ToString(FixDateTimeFormat.FixDateTime) : null;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable DateTime value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="targetParameter">Target parameter for this conversion.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A nullable DateTime equivalent to the value of this instance.</returns>
        public override DateTime? ToDateTime(IParameter targetParameter, IFormatProvider provider)
        {
            return _value;
        }

        /// <summary>
        /// Indicates whether the control has enumerated state (i.e., its state is held internally in an <see cref="EnumState"/> which
        /// requires special conversion, or if instead a regular value conversion is appropriate.
        /// </summary>
        public override bool HasEnumeratedState { get { return false; } }

        #endregion

        #region IValueProvider Members

        /// <summary>
        /// Gets the current value of this control, for use in Edits as part of StateRules.
        /// </summary>
        /// <returns>Either a valid DateTime or null (meaning do not send this value over FIX).</returns>
        public override object GetCurrentValue()
        {
            return _value;
        }

        #endregion
    }
}
