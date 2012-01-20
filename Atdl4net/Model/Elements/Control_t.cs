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
using Atdl4net.Fix;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Controls.Support;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Model.Enumerations;
using Atdl4net.Resources;
using Atdl4net.Utility;
using Atdl4net.Validation;

namespace Atdl4net.Model.Elements
{
    /// <summary>
    /// Base class for all concrete <see cref="Control_t"/> types.
    /// </summary>
    public abstract class Control_t : IParentable<StrategyPanel_t>, IValueProvider, IParameterConvertible
    {
        private StrategyPanel_t _owner;
        private StateRuleCollection _stateRules;

        /// <summary>
        /// Initializes a new <see cref="Control_t"/> instance with the specified identifier as id.
        /// </summary>
        /// <param name="id">Id of this control.</param>
        protected Control_t(string id)
        {
            Id = id;
        }

        #region Control_t Attributes

        // NB InitValue is not defined at this level because its data type depends on the type of control.

        /// <summary>For implementing systems that support saving order templates or pre-populated orders for basket trading/list
        ///  trading this attribute specifies that the control should be disabled when the order screen is going to be saved as a
        ///  template and not actually used to place an order.</summary>
        public bool? DisableForTemplate { get; set; }

        /// <summary>Unique identifier of this control. No two controls of the same strategy can have the same ID.</summary>
        public string Id { get; set; }

        /// <summary>Zero-based index for this control within a StrategyPanel_t.  For example, if a StrategyPanel_t has three controls,
        /// the first would have index of 0, the second 1 and the third 2.</summary>
        public int Index { get; set; }

        /// <summary>Indicates the initialization value is to be taken from this standard FIX field. Format: "FIX_" + FIXFieldName. 
        /// E.g. "FIX_OrderQty".  Required when initPolicy=”UseFixField”.</summary>
        public string InitFixField { get; set; }

        /// <summary>Describes how to initialize the control.  If the value of this attribute is undefined or equal to "UseValue" and
        ///  initValue is defined then initialize with initValue.  If the value is equal to "UseFixField" then attempt to initialize 
        /// with the value of the tag specified in initFixField. If the value is equal to "UseFixField" and it is not possible to 
        /// access the value of the specified fix tag then revert to using initValue. If the value is equal to "UseFixField", the 
        /// field is not accessible, and initValue is not defined, then do not initialize.</summary>
        public InitPolicy_t? InitPolicy { get; set; }

        /// <summary>A title for this control which may be displayed.</summary>
        public string Label { get; set; }

        /// <summary>The name of the parameter for which this control gives the visual representation. A parameter with this name 
        /// must be defined within the same strategy as this control.</summary>
        /// <remarks>The <see cref="ReferencedParameter"/> property provides access to the parameter instance itself, whilst
        /// this property provides access to the name of the parameter.</remarks>
        public string ParameterRef { get; set; }

        /// <summary>Tool tip text for rendered GUI objects rendered for the parameter.</summary>
        public string ToolTip { get; set; }

        #endregion

        /// <summary>
        /// Sets the value of this control; either with a value of the appropriate type, or using the FIXatdl '{NULL}' 
        /// value.  This method is either called indirectly from the user interface, or by a StateRule.
        /// </summary>
        /// <param name="newValue">Either a valid instance of the appropriate type or null (meaning do not send this 
        /// value over FIX).  May also contain the FIXatdl '{NULL}' value as a string.</param>
        public abstract void SetValue(object newValue);

        /// <summary>
        /// Resets this control to either a null value or for list controls, all options unselected.
        /// </summary>
        public abstract void Reset();

        /// <summary>
        /// Provides access to the value of this control using the <see cref="IParameterConvertible"/> interface which
        /// means that this control's value can be converted into a form that is appropriate for the target parameter.
        /// </summary>
        /// <returns>An <see cref="IParameterConvertible"/> object through which the control's value can be accessed.</returns>
        public virtual IParameterConvertible GetValueForParameter()
        {
            return this as IParameterConvertible;
        }

        /// <summary>
        /// Gets the collection of <see cref="StateRule_t"/>s for this control.
        /// </summary>
        public StateRuleCollection StateRules
        {
            get
            {
                // Perform lazy initialisation as we can't use 'this' in constructor.
                if (_stateRules == null)
                    _stateRules = new StateRuleCollection(this);

                return _stateRules;
            }
        }

        /// <summary>
        /// Adds support for the visitor pattern, enabling the appropriate Visit() method to be called on the visitor
        /// depending on its concrete type.
        /// </summary>
        /// <param name="visitor">Visitor.</param>
        public void DoVisit(IControlVisitor visitor)
        {
            ModelUtils.VisitHelper(typeof(IControlVisitor), visitor, this);
        }

        #region IValueProvider Members

        /// <summary>
        /// Gets the current value of this control, for use in Edits as part of StateRules but also used internally in the
        /// View Model.
        /// </summary>
        /// <returns>Current value of this control; may be null.</returns>
        public abstract object GetCurrentValue();

        #endregion

        #region Abstract Methods that all Controls must implement

        /// <summary>
        /// Sets the value of this control using the value of the supplied parameter.
        /// </summary>
        /// <param name="parameter">Parameter to set this control's value from.</param>
        public abstract void SetValueFromParameter(IParameter parameter);

        /// <summary>
        /// Loads the initial value for this control based on the InitPolicy, InitFixField and InitValue attributes.
        /// </summary>
        /// <param name="controlInitValueProvider">Value provider for initializing control values from InitFixField.</param>
        /// <remarks>The spec states: 'If the value of the initPolicy attribute is undefined or equal to "UseValue" and the initValue attribute is 
        /// defined then initialize with initValue.  If the value is equal to "UseFixField" then attempt to initialize with the value of 
        /// the tag specified in the initFixField attribute. If the value is equal to "UseFixField" and it is not possible to access the 
        /// value of the specified fix tag then revert to using initValue. If the value is equal to "UseFixField", the field is not accessible,
        /// and initValue is not defined, then do not initialize.</remarks>
        public abstract void LoadInitValue(FixFieldValueProvider controlInitValueProvider);

        #endregion

        #region IParentable<StrategyPanel_t> Members

        StrategyPanel_t IParentable<StrategyPanel_t>.Parent
        {
            get { return _owner; }
            set { _owner = value; }
        }

        #endregion

        #region IParameterConvertible Members

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable boolean value.
        /// </summary>
        /// <returns>One of true, false or null which is equivalent to the value of this instance.</returns>
        public abstract bool? ToBoolean(IParameter targetParameter);

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable decimal value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A nullable decimal equivalent to the value of this instance.</returns>
        public abstract decimal? ToDecimal(IParameter targetParameter, IFormatProvider provider);

        /// <summary>
        /// Converts the value of this instance to an equivalent 32-bit signed integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A nullable 32-bit signed integer equivalent to the value of this instance.</returns>
        public abstract int? ToInt32(IParameter targetParameter, IFormatProvider provider);

        /// <summary>
        /// Converts the value of this instance to an equivalent 32-bit unsigned integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A nullable 32-bit unsigned integer equivalent to the value of this instance.</returns>
        public abstract uint? ToUInt32(IParameter targetParameter, IFormatProvider provider);

        /// <summary>
        /// Converts the value of this instance to an equivalent char value.
        /// </summary>
        /// <returns>A nullable char value equivalent to the value of this instance.  May be null.</returns>
        public abstract char? ToChar(IParameter targetParameter);

        /// <summary>
        /// Converts the value of this instance to an equivalent string value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A string value equivalent to the value of this instance.  May be null.</returns>
        public abstract string ToString(IParameter targetParameter);

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable DateTime value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A nullable DateTime equivalent to the value of this instance.</returns>
        public abstract DateTime? ToDateTime(IParameter targetParameter, IFormatProvider provider);

        /// <summary>
        /// Indicates whether the control has enumerated state (i.e., its state is held internally in an <see cref="EnumState"/> which
        /// requires special conversion, or if instead a regular value conversion is appropriate).
        /// </summary>
        public abstract bool HasEnumeratedState { get; }

        #endregion

        #region Conversion Utilities

        /// <summary>
        /// Attempts to convert the supplied value to an integer, provided that the value is non-null and of non-zero length.
        /// </summary>
        /// <param name="value">Value to attempt to convert.</param>
        /// <param name="result">Valid int value if the conversion was possible; zero otherwise.</param>
        /// <returns>True if the value was non-null and of non-zero length and the conversion succeeded; false otherwise.</returns>
        /// <exception cref="InvalidCastException">Thrown if the supplied value was non-null and of non-zero length and the conversion was unsuccessful.</exception>
        protected bool TryConvertToInt(string value, out int result)
        {
            result = 0;
            bool hasValue = !string.IsNullOrEmpty(value);

            if (hasValue && !int.TryParse(value, out result))
                throw ThrowHelper.New<InvalidCastException>(this, ErrorMessages.InvalidNumericValue, value);

            return hasValue;
        }

        /// <summary>
        /// Attempts to convert the supplied value to an unsigned integer, provided that the value is non-null and of non-zero length.
        /// </summary>
        /// <param name="value">Value to attempt to convert.</param>
        /// <param name="result">Valid uint value if the conversion was possible; zero otherwise.</param>
        /// <returns>True if the value was non-null and of non-zero length and the conversion succeeded; false otherwise.</returns>
        /// <exception cref="InvalidCastException">Thrown if the supplied value was non-null and of non-zero length and the conversion was unsuccessful.</exception>
        protected bool TryConvertToUint(string value, out uint result)
        {
            result = 0;
            bool hasValue = !string.IsNullOrEmpty(value);

            if (hasValue && !uint.TryParse(value, out result))
                throw ThrowHelper.New<InvalidCastException>(this, ErrorMessages.InvalidNumericValue, value);

            return hasValue;
        }

        /// <summary>
        /// Attempts to convert the supplied value to an decimal, provided that the value is non-null and of non-zero length.
        /// </summary>
        /// <param name="value">Value to attempt to convert.</param>
        /// <param name="result">Valid decimal value if the conversion was possible; zero otherwise.</param>
        /// <returns>True if the value was non-null and of non-zero length and the conversion succeeded; false otherwise.</returns>
        /// <exception cref="InvalidCastException">Thrown if the supplied value was non-null and of non-zero length and the conversion was unsuccessful.</exception>
        protected bool TryConvertToDecimal(string value, out decimal result)
        {
            result = 0;
            bool hasValue = !string.IsNullOrEmpty(value);

            if (hasValue && !decimal.TryParse(value, out result))
                throw ThrowHelper.New<InvalidCastException>(this, ErrorMessages.InvalidNumericValue, value);

            return hasValue;
        }

        /// <summary>
        /// Attempts to convert the supplied value to a char, provided that the value is non-null and of non-zero length.
        /// </summary>
        /// <param name="value">Value to attempt to convert.</param>
        /// <param name="result">Valid char value if the conversion was possible; Char.MinValue otherwise.</param>
        /// <returns>True if the value was non-null and of non-zero length and the conversion succeeded; false otherwise.</returns>
        /// <exception cref="InvalidCastException">Thrown if the supplied value was non-null and of non-zero length and the conversion was unsuccessful.</exception>
        protected bool TryConvertToChar(string value, out char result)
        {
            bool hasValue = !string.IsNullOrEmpty(value);

            if (hasValue && value.Length != 1)
                throw ThrowHelper.New<InvalidCastException>(this, ErrorMessages.InvalidCharValue, value);

            result = hasValue ? value[0] : char.MinValue;

            return hasValue;
        }

        #endregion
    }
}
