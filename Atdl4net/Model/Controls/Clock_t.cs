#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Globalization;
using Atdl4net.Diagnostics;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Fix;
using Atdl4net.Model.Controls.Support;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Model.Types.Support;
using Atdl4net.Resources;
using Common.Logging;

namespace Atdl4net.Model.Controls
{
    /// <summary>
    /// Represents the Clock_t control element within FIXatdl.
    /// </summary>
    public class Clock_t : InitializableControl<DateTime?>
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

        /// <summary>Defines the treatment of initValue time. 0: use initValue; 1: use current time if initValue time has passed.
        /// The default value is 0.</summary>
        public int? InitValueMode { get; set; }

        #region InitializableControl<T> Overrides

        /// <summary>
        /// Attempts to load the supplied FIX field value into this control.
        /// </summary>
        /// <param name="value">Value to set this control to.</param>
        /// <returns>true if it was possible to set the value of this control using the supplied value; false otherwise.</returns>
        protected override bool LoadDefaultFromFixValue(string value)
        {
            DateTime result;

            bool parsed = FixDateTime.TryParse(value, CultureInfo.InvariantCulture, out result);

            _value = parsed ? (DateTime?)result : null;

            return parsed;
        }

        /// <summary>
        /// Loads this control with any supplied InitValue. If InitValue is not supplied, then control value will
        /// be set to default/empty value.
        /// </summary>
        protected override void LoadDefaultFromInitValue()
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

        #endregion

        #region Control_t Overrides

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
        /// Resets this control to either a null value or for list controls, all options unselected.
        /// </summary>
        public override void Reset()
        {
            _value = null;
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
