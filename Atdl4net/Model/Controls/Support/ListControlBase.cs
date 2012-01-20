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
    /// Base class for the subset of FIXatdl controls that allow ListItems.
    /// </summary>
    /// <remarks>The following controls support ListItems:
    /// <list type="bullet">
    /// <item><description>CheckBoxList_t</description></item>
    /// <item><description>DropDownList_t</description></item>
    /// <item><description>EditableDropDownList_t</description></item>
    /// <item><description>MultiSelectList_t</description></item>
    /// <item><description>RadioButtonList_t</description></item>
    /// <item><description>SingleSelectList_t</description></item>
    /// <item><description>Slider_t</description></item>
    /// </list>
    /// </remarks>
    public abstract class ListControlBase : InitializableControl<string>
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Model.Controls");

        /// <summary>
        /// EnumState for this control which provides storage of the state of each ListItem.
        /// </summary>
        protected EnumState _value;

        /// <summary>
        /// The ListItems for this control; will be empty if no ListItem sub-elements are present.
        /// </summary>
        protected readonly ListItemCollection _listItems = new ListItemCollection();

        /// <summary>
        /// Indicates whether the EnumState value for this control can be set to a value other than one of the enumerated
        /// values.  (This property is present to support editable drop-down list controls.)
        /// </summary>
        protected virtual bool IsNonEnumValueAllowed { get { return false; } }

        /// <summary>
        /// Initializes the base Control_t class with the supplied control identifier.
        /// </summary>
        /// <param name="id">ID for this control.</param>
        protected ListControlBase(string id)
            : base(id)
        {
        }

        #region InitializableControl<T> Overrides

        /// <summary>
        /// Attempts to load the supplied FIX field value into this control.
        /// </summary>
        /// <param name="value">Value to set this control to.</param>
        /// <returns>true if it was possible to set the value of this control using the supplied value; false otherwise.</returns>
        /// <remarks>Although the method name might suggest that value is a FIX wire value, for list controls, this
        /// parameter is in fact an enumID.</remarks>
        protected override bool LoadDefaultFromFixValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            _value = new EnumState(ListItems.EnumIds);

            _value.LoadInitValue(value, IsNonEnumValueAllowed);

            return true;
        }

        /// <summary>
        /// Loads this control with any supplied InitValue. If InitValue is not supplied, then control value will
        /// be set to default/empty value.
        /// </summary>
        protected override void LoadDefaultFromInitValue()
        {
            _value = new EnumState(ListItems.EnumIds);

            if (InitValue != null)
                _value.LoadInitValue(InitValue, IsNonEnumValueAllowed);
        }

        #endregion

        #region Control_t Overrides

        /// <summary>
        /// Gets the value of this control as an EnumState.
        /// </summary>
        /// <returns>EnumState that reflects which checkboxes are selected.</returns>
        public override object GetCurrentValue()
        {
            return _value;
        }

        /// <summary>
        /// Sets the value of this control; either via an EnumState, or using the FIXatdl '{NULL}' value.
        /// </summary>
        /// <param name="newValue">Value that reflects which checkboxes are selected.</param>
        public override void SetValue(object newValue)
        {
            if (_value == null)
                throw ThrowHelper.New<InternalErrorException>(this, InternalErrors.UnexpectedNullReference, "_value",
                    "Atdl4net.Model.Types.Support.EnumState");

            if (newValue == null || newValue as string == Atdl.NullValue)
                _value.ClearAll();
            else
                _value.UpdateFrom(newValue as EnumState);
        }

        /// <summary>
        /// Resets this control to either a null value or for list controls, all options unselected.
        /// </summary>
        public override void Reset()
        {
            if (_value != null)
                _value.ClearAll();
        }

        /// <summary>
        /// Gets the collection of ListItems for this control.
        /// </summary>
        public ListItemCollection ListItems { get { return _listItems; } }

        /// <summary>
        /// Indicates whether this control has one or more ListItems.
        /// </summary>
        public bool HasListItems { get { return _listItems.HasItems; } }

        /// <summary>
        /// Sets the value of this control using the value of the supplied parameter.
        /// </summary>
        /// <param name="parameter">Parameter to set this control's value from.</param>
        public override void SetValueFromParameter(IParameter parameter)
        {
            IControlConvertible value = parameter.GetValueForControl();

            _value = value.ToEnumState(parameter.EnumPairs);

            _log.Debug(m => m("List control {0} value is now {1}", Id, _value.ToString()));
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable boolean value.
        /// </summary>
        /// <returns>One of true, false or null which is equivalent to the value of this instance.</returns>
        public override bool? ToBoolean(IParameter targetParameter)
        {
            throw ThrowHelper.New<InvalidCastException>(this, ErrorMessages.UnsupportedControlValueConversion, _value, "Boolean", Id);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable decimal value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A nullable decimal equivalent to the value of this instance.</returns>
        public override decimal? ToDecimal(IParameter targetParameter, IFormatProvider provider)
        {
            decimal result = 0;
            string wireValue = ToString(targetParameter);

            return TryConvertToDecimal(wireValue, out result) ? (decimal?)result : null;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 32-bit signed integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A nullable 32-bit signed integer equivalent to the value of this instance.</returns>
        public override int? ToInt32(IParameter targetParameter, IFormatProvider provider)
        {
            int result = 0;
            string wireValue = ToString(targetParameter);

            return TryConvertToInt(wireValue, out result) ? (int?)result : null;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 32-bit unsigned integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A nullable 32-bit unsigned integer equivalent to the value of this instance.</returns>
        public override uint? ToUInt32(IParameter targetParameter, IFormatProvider provider)
        {
            uint result = 0;
            string wireValue = ToString(targetParameter);

            return TryConvertToUint(wireValue, out result) ? (uint?)result : null;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent char value.
        /// </summary>
        /// <returns>A nullable char value equivalent to the value of this instance.  May be null.</returns>
        public override char? ToChar(IParameter targetParameter)
        {
            string wireValue = ToString(targetParameter);

            char result = char.MinValue;

            return TryConvertToChar(wireValue, out result) ? (char?)result : null;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent string value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A string value equivalent to the value of this instance.  May be null.</returns>
        public override string ToString(IParameter targetParameter)
        {
            if (_value == null)
                throw ThrowHelper.New<InternalErrorException>(this, InternalErrors.UnexpectedNullReference, "_value", this.GetType().Name);

            try
            {
                return _value.ToWireValue(targetParameter.EnumPairs);
            }
            catch (InvalidOperationException ex)
            {
                throw ThrowHelper.Rethrow(this, ex, ErrorMessages.UnsuccessfulSetParameterOperation, targetParameter.Name, Id, ex.Message);
            }
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable DateTime value using the specified culture-specific formatting information.
        /// </summary>
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
        public override bool HasEnumeratedState { get { return _value != null; } }

        #endregion
    }
}
