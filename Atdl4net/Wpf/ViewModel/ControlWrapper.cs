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
using System.ComponentModel;
using System.Windows;
using Atdl4net.Diagnostics;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Fix;
using Atdl4net.Model.Controls.Support;
using Atdl4net.Model.Elements;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Notification;
using Atdl4net.Resources;
using Atdl4net.Utility;
using Atdl4net.Validation;
using Common.Logging;
#if !NET_40
using Atdl4net.Model.Controls;
#endif

namespace Atdl4net.Wpf.ViewModel
{
    /// <summary>
    /// Wrapper class for all FIXatdl non-list-based controls; part of the View Model for Atdl4net.
    /// </summary>
    /// <remarks>Each non-list-based control within a strategy is wrapped with a ControlWrapper, this provides the glue between the actual WPF
    /// control (TextBox, Clock, etc.) and the <see cref="Control_t"/> itself.  WPF databinding is used to link the WPF control state
    /// to its ControlWrapper. The list of FIXatdl controls that use ControlWrapper is as follows:
    /// <list type="bullet">
    /// <item><description><see cref="Atdl4net.Model.Controls.CheckBox_t"/></description></item>
    /// <item><description><see cref="Atdl4net.Model.Controls.Clock_t"/></description></item>
    /// <item><description><see cref="Atdl4net.Model.Controls.DoubleSpinner_t"/></description></item>
    /// <item><description><see cref="Atdl4net.Model.Controls.HiddenField_t"/></description></item>
    /// <item><description><see cref="Atdl4net.Model.Controls.Label_t"/></description></item>
    /// <item><description><see cref="Atdl4net.Model.Controls.RadioButton_t"/></description></item>
    /// <item><description><see cref="Atdl4net.Model.Controls.SingleSpinner_t"/></description></item>
    /// <item><description><see cref="Atdl4net.Model.Controls.TextField_t"/></description></item>
    /// </list><br/>
    /// Note that ControlWrappers do not hold value information; the value of each control is always held in the Control_t.
    /// ControlWrappers do however hold the user interface state of each control (i.e., visibility, enabled/disabled).<br/><br/>
    /// Note also that list-based controls use <see cref="ListControlWrapper"/> instead as that class handles the communication
    /// of the state of list-based WPF to the <see cref="Atdl4net.Model.Types.Support.EnumState"/> type that holds the state of each selectable item
    /// within the control.</remarks>
    public class ControlWrapper : INotifyPropertyChanged, INotifyValueChanged, INotifyValueChangeCompleted, IBindable<ViewModelControlCollection>
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Wpf.ViewModel");

        private bool _visible = true;
        private bool _enabled = true;
        private readonly DataEntryMode _dataEntryMode;
        private FixFieldValueProvider _fixFieldValues;
        private ViewModelStateRuleCollection _stateRules;
        private readonly ControlValidationState _validationState;
        private readonly IParameter _referencedParameter;

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raised whenever the value of a property (UiValue, Visibility, IsEnabled) changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region INotifyValueChanged Members

        /// <summary>
        /// Raised whenever the WPF control's value changes.  Used to communicate value change notifications to Edits
        /// within StateRules for the strategy the underlying <see cref="Control_t"/> belongs to.
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        #endregion

        #region INotifyValueChangeCompleted Members

        /// <summary>
        /// Raised when a value change has been fully processed.
        /// </summary>
        public event EventHandler<ValueChangeCompletedEventArgs> ValueChangeCompleted;

        #endregion

        /// <summary>
        /// Initializes a new ControlWrapper using the supplied <see cref="Control_t"/> as underlying control and the 
        /// supplied <see cref="IParameter"/> as referenced parameter.
        /// </summary>
        /// <param name="control">Underlying Control_t for this ControlWrapper.</param>
        /// <param name="referencedParameter">Parameter that this control relates to.  May be null.</param>
        /// <param name="mode">Data entry mode (create/amend/view).</param>
        protected ControlWrapper(Control_t control, IParameter referencedParameter, DataEntryMode mode)
        {
            UnderlyingControl = control;
            _referencedParameter = referencedParameter;
            _dataEntryMode = mode;

            _validationState = new ControlValidationState(control.Id);
        }

        /// <summary>
        /// Factory method for creating new ControlWrapper instances.
        /// </summary>
        /// <param name="underlyingStrategy"><see cref="Strategy_t"/> that this ControlWrapper's <see cref="Control_t"/> is a member of.</param>
        /// <param name="control">Underlying Control_t for this ControlWrapper.</param>
        /// <param name="mode">Data entry mode (create/amend/view).</param>
        /// <returns></returns>
        public static ControlWrapper Create(Strategy_t underlyingStrategy, Control_t control, DataEntryMode mode)
        {
            IParameter referencedParameter = null;

            if (control.ParameterRef != null)
            {
                referencedParameter = underlyingStrategy.Parameters[control.ParameterRef];

                if (referencedParameter == null)
                    throw ThrowHelper.New<ReferencedObjectNotFoundException>(ErrorMessages.UnresolvedParameterRefError, control.ParameterRef);
            }

            ControlWrapper wrapper;

#if !NET_40
            // This is to workaround a bug in .NET Framework 3.5 where it is possible for more than one radio button in a 
            // group to be checked at a time.
            if (control is RadioButton_t)
                wrapper = new RadioButtonWrapper(control as RadioButton_t, referencedParameter, mode);
            else
#endif
            if (control is ListControlBase)
                wrapper = ListControlWrapper.Create(control as ListControlBase, referencedParameter, mode);
            else
                wrapper = new ControlWrapper(control, referencedParameter, mode);

            wrapper._stateRules = new ViewModelStateRuleCollection(wrapper, control.StateRules);
            wrapper._fixFieldValues = new FixFieldValueProvider(underlyingStrategy.InputValues, underlyingStrategy.Parameters);

            return wrapper;
        }

        /// <summary>
        /// Gets the underlying <see cref="Control_t"/> that this ControlWrapper is responsible for.
        /// </summary>
        public Control_t UnderlyingControl { get; private set; }

        /// <summary>
        /// Gets the ParameterRef of this ControlWrapper's underlying <see cref="Control_t"/>.
        /// </summary>
        public string ParameterRef { get { return UnderlyingControl.ParameterRef; } }

        /// <summary>
        /// Gets the ID of this ControlWrapper's underlying <see cref="Control_t"/>.
        /// </summary>
        public string Id { get { return UnderlyingControl.Id; } }

        /// <summary>
        /// Gets the tooltip for this control.
        /// </summary>
        public string ToolTip
        {
            get
            {
                if (_validationState.CurrentState)
                    return UnderlyingControl.ToolTip;
                else
                    return _validationState.ErrorText;
            }
        }

        /// <summary>
        /// Gets/sets the user interface value for the <see cref="Control_t"/> that this ControlWrapper is responsible for.
        /// </summary>
        public virtual object UiValue
        {
            get { return UnderlyingControl.GetCurrentValue(); }

            set
            {
                _log.Debug(m => m("ControlWrapper for Control {0} value updated to '{1}' (data type {2}).",
                    Id, (value is ListItem_t) ? (value as ListItem_t).EnumId : value ?? "null", value != null ? value.GetType().Name : "N/A"));

                if (UnderlyingControl.GetCurrentValue() != value)
                {
                    object oldValue = UnderlyingControl.GetCurrentValue();

                    UnderlyingControl.SetValue(value);

                    NotifyPropertyChanged("UiValue");
                    NotifyValueChanged(oldValue, value);
                }
            }
        }

        /// <summary>
        /// Sets the visible state of the WPF control that this ControlWrapper is responsible for.
        /// </summary>
        public bool IsVisible
        {
            set
            {
                if (_visible != value)
                {
                    _visible = value;

                    Visibility = value ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// Gets/sets the WPF visibility for the user interface control that this ControlWrapper is responsible for.
        /// </summary>
        public Visibility Visibility
        {
            get { return _visible ? Visibility.Visible : Visibility.Collapsed; }
            set { NotifyPropertyChanged("Visibility"); }
        }

        /// <summary>
        /// Gets/sets the WPF enabled/disabled state for the user interface control that this ControlWrapper is responsible for.
        /// </summary>
        public bool Enabled
        {
            get
            {
                switch (_dataEntryMode)
                {
                    case DataEntryMode.Amend:
                        // If we don't have a referenced parameter, then there is no good reason to prevent the user changing the
                        // value of this control (assuming a StateRule hasn't already disabled it, of course).
                        return (_referencedParameter == null) ? _enabled : (_referencedParameter.MutableOnCxlRpl == true) && _enabled;

                    case DataEntryMode.Create:
                        return _enabled;

                    default:
                        return false;
                }
            }

            set
            {
                // No need to worry about DataEntryMode as WPF will use the getter to retrieve the current Enabled state.
                if (_enabled != value)
                {
                    _enabled = value;

                    NotifyPropertyChanged("Enabled");
                }
            }
        }

        /// <summary>
        /// Resets the state of this ControlWrapper, i.e., Enabled = true, Visible = true;
        /// </summary>
        public virtual void Reset()
        {
            Enabled = true;
            IsVisible = true;
        }

        /// <summary>
        /// Refreshes the state of all StateRules for this ControlWrapper's underlying <see cref="Control_t"/>.
        /// </summary>
        public void RefreshState()
        {
            _log.Debug(m => m("Refreshing state for control with ID {0}", Id));

            _stateRules.RefreshState();
        }

        /// <summary>
        /// Indicates whether this control value is currently valid, according to the StrategyEdits
        /// attached to the parent strategy.
        /// </summary>
        public bool IsValid { get { return !_validationState.CurrentState; } }

        /// <summary>
        /// Updates the value of the parameter that the underlying <see cref="Control_t"/> relates to.  If the Control_t
        /// has no underlying parameter, then no action is taken.
        /// </summary>
        public void UpdateParameterValue()
        {
            if (_referencedParameter != null)
                _validationState.ParameterValidationResult = _referencedParameter.SetValueFromControl(UnderlyingControl);
        }

        /// <summary>
        /// Notifies any interested parties that the named property's value has changed.
        /// </summary>
        /// <param name="name">Name of property whose value has changed.</param>
        protected void NotifyPropertyChanged(string name)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;

            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Notifies interested parties that the value of the user interface control has changed.
        /// </summary>
        /// <param name="oldValue">Old value before this change.</param>
        /// <param name="newValue">New value.</param>
        /// <remarks>Note that this method is not intended for databinding, rather to notify Edit_t objects that one of their
        /// values has changed.</remarks>
        protected void NotifyValueChanged(object oldValue, object newValue)
        {
            _log.Debug(m => m("Control {0} value changed from '{1}' to '{2}'", Id, oldValue, newValue));

            EventHandler<ValueChangedEventArgs> valueChanged = ValueChanged;

            if (valueChanged != null)
                valueChanged(this, new ValueChangedEventArgs(Id, oldValue, newValue));

            OnValueChangeCompleted();
        }

        /// <summary>
        /// Notifies interested parties that a value change has been fully processed.
        /// </summary>
        /// <remarks>We have to use a separate event (as opposed to re-using the ValueChanged event)
        /// because we cannot guarantee what order event subscribers will be called, and this 
        /// notification must happen once the value change has been completely processed.</remarks>
        protected virtual void OnValueChangeCompleted()
        {
            EventHandler<ValueChangeCompletedEventArgs> valueChangeCompleted = ValueChangeCompleted;

            if (valueChangeCompleted != null)
                valueChangeCompleted(this, new ValueChangeCompletedEventArgs(this));

            _validationState.Evaluate(_fixFieldValues);

            // In case the validation state ErrorText has changed...
            NotifyPropertyChanged("ToolTip");
        }

        private void StrategyEditStateChanged(object sender, Notification.StateChangedEventArgs e)
        {
            // In case the validation state ErrorText has changed...
            NotifyPropertyChanged("UiValue");
            NotifyPropertyChanged("ToolTip");

            StrategyEditWrapper strategyEdit = sender as StrategyEditWrapper;

            _log.Debug(m => m("Control {0}: {1} {2} {3}", Id, strategyEdit.InternalId, e.OldState, e.NewState));
        }

        internal void BindStrategyEdit(StrategyEditWrapper strategyEditWrapper)
        {
            strategyEditWrapper.StateChanged += new EventHandler<StateChangedEventArgs>(StrategyEditStateChanged);

            _validationState.Add(strategyEditWrapper);
        }

        internal void UnbindStrategyEdit(StrategyEditWrapper strategyEditWrapper)
        {
            strategyEditWrapper.StateChanged -= new EventHandler<StateChangedEventArgs>(StrategyEditStateChanged);

            _validationState.Remove(strategyEditWrapper);
        }

        #region IBindable<ViewControlCollection> Members

        /// <summary>
        /// Binds this control's StateRules to the <see cref="ViewModelControlCollection"/> this ControlWrapper is a
        /// member of.
        /// </summary>
        /// <param name="target">ViewModelControlCollection this ControlWrapper is a member of.</param>
        void IBindable<ViewModelControlCollection>.Bind(ViewModelControlCollection target)
        {
            (_stateRules as IBindable<ViewModelControlCollection>).Bind(target);
        }

        #endregion
    }
}
