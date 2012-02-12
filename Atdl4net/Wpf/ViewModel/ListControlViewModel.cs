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
using System.Windows.Controls;
using Atdl4net.Diagnostics;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Model;
using Atdl4net.Model.Controls;
using Atdl4net.Model.Controls.Support;
using Atdl4net.Model.Elements;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Model.Enumerations;
using Atdl4net.Resources;
using Atdl4net.Utility;
using Common.Logging;

namespace Atdl4net.Wpf.ViewModel
{
    /// <summary>
    /// View model class for all FIXatdl list-based controls; part of the View Model for Atdl4net.
    /// </summary>
    /// <remarks>Each list-based control within a strategy is wrapped with a ListControlViewModel, this provides the glue between the actual WPF
    /// control (ListBox, ComboBox, etc.) and the <see cref="Control_t"/> itself.  WPF databinding is used to link the WPF control state
    /// to its ListControlViewModel.  The list of FIXatdl controls that use ListControlViewModel is as follows:
    /// <list type="bullet">
    /// <item><description><see cref="Atdl4net.Model.Controls.CheckBoxList_t"/></description></item>
    /// <item><description><see cref="Atdl4net.Model.Controls.DropDownList_t"/></description></item>
    /// <item><description><see cref="Atdl4net.Model.Controls.EditableDropDownList_t"/></description></item>
    /// <item><description><see cref="Atdl4net.Model.Controls.MultiSelectList_t"/></description></item>
    /// <item><description><see cref="Atdl4net.Model.Controls.RadioButtonList_t"/></description></item>
    /// <item><description><see cref="Atdl4net.Model.Controls.SingleSelectList_t"/></description></item>
    /// <item><description><see cref="Atdl4net.Model.Controls.Slider_t"/></description></item>
    /// </list><br/>
    /// There are two ways that a WPF control can notify the ListControlViewModel of updates to its state.
    /// <list type="number">
    /// <item><description>Via the <see cref="SelectedValue"/> property of this class.  As a rule, this applies to all list-based
    /// controls that can only have one item selected at a time (i.e., DropDownList_t, EditableDropDownList_, SingleSelectList_, Slider_t)
    /// but it does not apply to RadioButtonList_t as the custom control for this type uses a WPF Selector for its implementation.</description></item>
    /// <item><description>Via the <see cref="ListItemViewModel.IsSelected"/> property of the <see cref="ListItemViewModel"/> class.  This applies 
    /// to all list-based controls that can have more than one item selected at a time (i.e., CheckBoxList_t, MultiSelectList_t) plus
    /// RadioButtonList_t as detailed above.</description></item>
    /// </list><br/>
    /// Note that ListControlViewModels do not hold value information; the value of each control is always held in the Control_t.
    /// ListControlViewModels do however hold the user interface state of each control (i.e., visibility, enabled/disabled).<br/><br/>
    /// Note also that non-list-based controls use <see cref="ControlViewModel"/> instead.</remarks>
    public class ListControlViewModel : ControlViewModel
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Wpf.ViewModel");

        private string _controlText; // Used to support EditableDropDownList only
        private ViewModelListItemCollection _listItems;

        // Private constructor
        private ListControlViewModel(ListControlBase control, IParameter referencedParameter)
            : base(control as Control_t, referencedParameter)
        {
        }

        /// <summary>
        /// Factory method for creating new ListControlViewModel instances.
        /// </summary>
        /// <param name="control">Underlying list-based Control_t (of type <see cref="ListControlBase"/>) for this ControlViewModel.</param>
        /// <param name="referencedParameter">Parameter that the specified Control_t relates to.  May be null.</param>
        /// <param name="mode">Data entry mode (create/amend/view).</param>
        /// <returns>New instance of ListControlViewModel.</returns>
        public static ListControlViewModel Create(ListControlBase control, IParameter referencedParameter)
        {
            ListControlViewModel controlViewModel = new ListControlViewModel(control, referencedParameter);

            controlViewModel._listItems = ViewModelListItemCollection.Create(controlViewModel);

            return controlViewModel;
        }

        /// <summary>
        /// Gets the collection of <see cref="ListItem_t"/>s for this ControlViewModel's control.
        /// </summary>
        public ViewModelListItemCollection ListItems { get { return _listItems; } }

        /// <summary>
        /// Gets the orientation of this ControlViewModel's control.  (Only applicable to controls that can be presented horizontally or vertically).
        /// </summary>
        public Orientation Orientation
        {
            get
            {
                if (UnderlyingControl is IOrientableControl)
                    return (UnderlyingControl as IOrientableControl).Orientation == Orientation_t.Vertical ? Orientation.Vertical : Orientation.Horizontal;

                throw new InvalidOperationException("Orientation referenced on non-orientable control.");
            }
        }

        /// <summary>
        /// Gets/sets the SelectedValue property - typically this is the EnumID of the selected ListItem.  Only called from the
        /// user interface.  Note that the setter for SelectedValue is effectively mutually exclusive; the state of all other 
        /// EnumIDs must be set to false.
        /// </summary>
        /// <remarks>The output of property's getter isn't very meaningful for controls that allow more than one item
        /// to be selected at the same time, as it can only return a single value.</remarks>
        public string SelectedValue
        {
            get
            {
                EnumState state = UiValue as EnumState;

                if (state == null || state.NonEnumValue != null)
                    return null;

                return state.GetFirstSelectedEnumId();
            }

            set
            {
                if (value != null)
                {
                    // GetValue returns a copy of the current control state
                    EnumState state = UnderlyingControl.GetCurrentValue() as EnumState;

                    if (state == null)
                        throw ThrowHelper.New<InternalErrorException>(this, InternalErrors.UnexpectedNullReference, "state (from UnderlyingControl.GetCurrentValue())",
                            "Atdl4net.Model.Types.Support.EnumState");

                    _log.Debug(m => m("SelectedValue changed; updating ListControlViewModel from {0} with EnumID {1}", state.ToString(), value));

                    state.ClearAll();

                    state[value] = true;

                    UiValue = state;
                }
            }
        }

        /// <summary>
        /// Gets/sets the Text property - this is only used by EditableDropDownList to specify the text that is typed 
        /// directly into that type of control.
        /// </summary>
        public string Text
        {
            get
            {
                EnumState state = UiValue as EnumState;

                // If we have a valid non-enum value, then return it
                if (state != null)
                {
                    if (state.NonEnumValue != null)
                        return state.NonEnumValue;

                    string enumId = state.GetFirstSelectedEnumId();

                    if (_listItems.Contains(enumId))
                        return _listItems[enumId].UiRep;

                    return string.Empty;
                }

                // ... otherwise use the private temporary storage
                return _controlText;
            }

            set
            {
                // This method is invoked when the user types in any test (EditableDropDownList only) but also
                // when an item is selected, as that causes the text portion of the combo box to be updated.
                // If the latter, then we don't want to invoke another update to the UiValue.
                EnumState state = UiValue as EnumState;

                string enumId = null;

                // If we have a valid EnumState, then update that
                if (state != null)
                {
                    // If this is equivalent to a valid EnumID and the EnumID isn't set, then update; for all
                    // other cases, update anyway.
                    if (!_listItems.TryGetEnumIdByUiRep(value, out enumId) || !state[enumId])
                        UiValue = value;
                }
                else
                    // ... otherwise use the private temporary storage
                    _controlText = value;
            }
        }

        /// <summary>
        /// Sets the user interface value for this control.  Called in response to a change in the SelectedValue
        /// property, but for also by StateRules in response to a change in state that includes the value attribute.
        /// </summary>
        public override object UiValue
        {
            set
            {
                // GetValue returns a copy of the current control state
                EnumState oldState = UnderlyingControl.GetCurrentValue() as EnumState;
                EnumState state = null;

                if (oldState == null)
                    throw ThrowHelper.New<InternalErrorException>(this, InternalErrors.UnexpectedNullReference, "state (from UnderlyingControl.GetValue())", 
                        "Atdl4net.Model.Types.Support.EnumState");

                bool isString = value is string;
                bool isEnumState = value is EnumState;

                _log.Debug(m => m("Updating UiValue of ListControlViewModel for control type {0}", UnderlyingControl.GetType().Name));

                // Either we have been passed a string, which is either arbitrary text (EditableDropDownList only) or
                // is the FIXatdl {NULL} value or we've been passed an EnumID value via a StateRule...
                if (isString)
                {
                    state = oldState.Copy();

                    string newValue = value as string;

                    if (state != null)
                    {
                        _log.Debug(m => m("Updating UiValue of ListControlViewModel from {0} with value {1}", state.ToString(), newValue));

                        if (newValue == Atdl.NullValue)
                            state.ClearAll();
                        else
                        {
                            // Special treatment for EditableDropDownList_t...
                            if (UnderlyingControl is EditableDropDownList_t && !state.IsValidEnumId(newValue))
                                state.NonEnumValue = newValue;
                            else
                                state[newValue] = true;
                        }
                    }
                }
                // ... or we are being passed an EnumState...
                else if (isEnumState)
                {
                    state = value as EnumState;

                    _log.Debug(m => m("Updating UiValue of ListControlViewModel from {0} to {1}", oldState.ToString(), state.ToString()));
                }
                // ... or we're being cleared by a parameter (TODO: verify this is to be expected)....
                else if (value == null)
                {
                    state = oldState.Copy();

                    state.ClearAll();
                }
                // ... or we've got something we plainly don't recognise
                else
                    throw ThrowHelper.New<InternalErrorException>(this, InternalErrors.UnexpectedArgumentType, 
                        value.GetType().FullName, "System.String, Atdl4net.Model.Types.Support.EnumState");

                if (!oldState.Equals(state))
                {
                    // SetValue copies the state we are passing in
                    UnderlyingControl.SetValue(state);

                    NotifyValueChanged(oldState, state);
                }
            }
        }

        /// <summary>
        /// Refreshes the state of all StateRules for this ListControlViewModel's underlying <see cref="Control_t"/>.
        /// </summary>
        public override void RefreshState()
        {
            base.RefreshState();

            _log.Debug(m => m("Refreshing SelectedValue and ListItems state for list control with ID {0}", Id));

            _listItems.RefreshState();

            NotifyPropertyChanged("SelectedValue");
            NotifyPropertyChanged("Text");
        }
    }
}