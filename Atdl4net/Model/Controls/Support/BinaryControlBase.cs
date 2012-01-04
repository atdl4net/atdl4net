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
using Atdl4net.Diagnostics;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Model.Types.Support;
using Atdl4net.Resources;
using Common.Logging;

namespace Atdl4net.Model.Controls.Support
{
    /// <summary>
    /// Represents control elements within FIXatdl that can support one of two states (<see cref="CheckBox_t"/>, <see cref="RadioButton_t"/>).
    /// </summary>
    public abstract class BinaryControlBase : InitializableControl<bool?>
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Model.Controls");

        /// <summary>
        /// The state value for this control.
        /// </summary>
        protected bool? _value;

        /// <summary>
        /// Initializes a new instance of <see cref="BinaryControlBase"/> using the supplied ID.
        /// </summary>
        /// <param name="id">ID for this control.</param>
        protected BinaryControlBase(string id)
            : base(id)
        {
        }

        /// <summary>Output EnumID if checked/selected.  Applicable when xsi:type is CheckBox_t or RadioButton_t.</summary>
        public string CheckedEnumRef { get; set; }

        /// <summary>Output EnumID if unchecked/not selected.  Applicable when xsi:type is CheckBox_t or RadioButton_t.</summary>
        public string UncheckedEnumRef { get; set; }

        #region InitializableControl<T> Overrides

        /// <summary>
        /// Loads the supplied FIX field value into this control.
        /// </summary>
        /// <param name="value">Value to set this control to.</param>
        protected override bool LoadDefaultFromFixValue(string value)
        {
            try
            {
                SetValue(value);

                return true;
            }
            catch (InvalidFieldValueException ex)
            {
                _log.ErrorFormat("Unable to set control {0} from FIX field value '{1}'; reason: {2}", Id, value, ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Loads this control with any supplied InitValue. If InitValue is not supplied, then control value will
        /// be set to default/empty value.
        /// </summary>
        protected override void LoadDefaultFromInitValue()
        {
            // Binary controls should have a default state of off if no InitValue is specified.
            if (InitValue != null)
                SetValue(InitValue);
            else
                _value = false;
        }

        #endregion

        #region Control_t Overrides

        /// <summary>
        /// Gets the value of this control.  May be null.
        /// </summary>
        /// <returns>One of three states, true, false or null (meaning do not send this value over FIX).</returns>
        public override object GetCurrentValue()
        {
            return _value;
        }

        /// <summary>
        /// Sets the value of this control using the value of the supplied parameter.
        /// </summary>
        /// <param name="parameter">Parameter to set this control's value from.</param>
        public override void SetValueFromParameter(IParameter parameter)
        {
            IControlConvertible value = parameter.GetValueForControl();

            // Special treatment needed here as we can't just assume that the source parameter has enumerated values, but if it
            // does, then we can't use ToBoolean as that won't map the wire values correctly to the state of this control.
            if (HasEnumeratedState)
            {
                if (parameter.HasEnumPairs)
                {
                    EnumState state = value.ToEnumState(parameter.EnumPairs);

                    if (state[CheckedEnumRef])
                        _value = true;
                    else if (state[UncheckedEnumRef])
                        _value = false;
                    else
                        _value = null;
                }
                else
                    throw ThrowHelper.New<InconsistentStrategyException>(this, ErrorMessages.InconsistentEnumPairsListItemsError);
            }
            else
                _value = value.ToBoolean();

            _log.Debug(m => m("Binary control {0} value is now {1}", _value));
        }

        /// <summary>
        /// Sets the value of this control; either via a boolean, or using the FIXatdl '{NULL}' value.
        /// </summary>
        /// <param name="newValue">One of three bool? states, true, false or null (meaning do not send this value over FIX).
        /// May also contain the FIXatdl '{NULL}' value as a string, or either of the CheckedEnumRef or
        /// UncheckedEnumRef string values.</param>
        public override void SetValue(object newValue)
        {
            bool isString = newValue is string;
            bool isBool = newValue is bool;

            _log.Debug(m => m("Setting binary control {0} using value {1} (of type {2})", Id, newValue, newValue != null ? newValue.GetType().Name : "N/A"));

            // Strictly this is a bit of a hack as the right thing to do when implementing CheckBoxes and RadioButtons is
            // to enforce the use of boolean inputs.  However as atdl4j supports setting of these controls' state via
            // EnumIDs in the InitValue, guess we better do the same...
            if (isString)
            {
                string value = newValue as string;

                if (value == Atdl.NullValue)
                    _value = null;
                else if (value == CheckedEnumRef)
                    _value = true;
                else if (value == UncheckedEnumRef)
                    _value = false;
                else
                    throw ThrowHelper.New<InvalidFieldValueException>(this, ErrorMessages.InitControlValueError,
                        Id, string.Format("'{0}' is not a valid value for this control", value));
            }
            else if (isBool)
            {
                _value = (bool?)newValue;
            }
            else if (newValue == null)
                _value = null;
            else
                throw ThrowHelper.New<InternalErrorException>(this, InternalErrors.UnexpectedArgumentType,
                    newValue.GetType().FullName, "System.String, System.Boolean");

            _log.Debug(m => m("Binary control value is now {0}", _value != null ? _value.ToString().ToLower() : "null"));
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable boolean value.
        /// </summary>
        /// <param name="targetParameter">Target parameter for this conversion.</param>
        /// <returns>One of true, false or null which is equivalent to the value of this instance.</returns>
        public override bool? ToBoolean(IParameter targetParameter)
        {
            return _value;
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
            string wireValue = _value != null ? ToString(targetParameter) : null;

            int result = 0;

            return TryConvertToInt(wireValue, out result) ? (int?)result : null;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 32-bit unsigned integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="targetParameter">Target parameter for this conversion.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A nullable 32-bit unsigned integer equivalent to the value of this instance.</returns>
        public override uint? ToUInt32(IParameter targetParameter, IFormatProvider provider)
        {
            string wireValue = _value != null ? ToString(targetParameter) : null;

            uint result = 0;

            return TryConvertToUint(wireValue, out result) ? (uint?)result : null;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent char value.
        /// </summary>
        /// <param name="targetParameter">Target parameter for this conversion.</param>
        /// <returns>A nullable char value equivalent to the value of this instance.  May be null.</returns>
        public override char? ToChar(IParameter targetParameter)
        {
            string wireValue = _value != null ? ToString(targetParameter) : null;

            char result = char.MinValue;

            return TryConvertToChar(wireValue, out result) ? (char?)result : null;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent string value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="targetParameter">Target parameter for this conversion.</param>
        /// <returns>A string value equivalent to the value of this instance.  May be null.</returns>
        public override string ToString(IParameter targetParameter)
        {
            if (_value != null && HasEnumeratedState)
            {
                EnumPairCollection enumPairs = targetParameter.EnumPairs;

                string value = (bool)_value ? enumPairs.GetWireValueFromEnumId(CheckedEnumRef) : enumPairs.GetWireValueFromEnumId(UncheckedEnumRef);

                // It is possible for '{NULL}' to be provided as one of the enum wire values, so we have to act accordingly
                return value != Atdl.NullValue ? value : null;
            }
            else
                return _value != null ? _value.ToString().ToLower() : null;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable DateTime value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="targetParameter">Target parameter for this conversion.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A nullable DateTime equivalent to the value of this instance.</returns>
        public override DateTime? ToDateTime(IParameter targetParameter, IFormatProvider provider)
        {
            throw ThrowHelper.New<InvalidCastException>(this, ErrorMessages.UnsupportedControlValueConversion, _value, "DateTime", Id);
        }

        /// <summary>
        /// Indicates whether the control has enumerated state (i.e., its state is held internally in an <see cref="EnumState"/> which
        /// requires special conversion, or if instead a regular value conversion is appropriate).
        /// </summary>
        public override bool HasEnumeratedState { get { return CheckedEnumRef != null && UncheckedEnumRef != null; } }

        #endregion
    }
}
