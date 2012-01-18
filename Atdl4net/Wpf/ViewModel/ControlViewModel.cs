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
using Atdl4net.Model.Enumerations;
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
    /// View model class for all FIXatdl non-list-based controls; part of the View Model for Atdl4net.
    /// </summary>
    /// <remarks>Each non-list-based control within a strategy is wrapped with a ControlViewModel, this provides the glue between the actual WPF
    /// control (TextBox, Clock, etc.) and the <see cref="Control_t"/> itself.  WPF databinding is used to link the WPF control state
    /// to its ControlViewModel. The list of FIXatdl controls that use ControlViewModel is as follows:
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
    /// Note that ControlViewModels do not hold value information; the value of each control is always held in the Control_t.
    /// ControlViewModels do however hold the user interface state of each control (i.e., visibility, enabled/disabled).<br/><br/>
    /// Note also that list-based controls use <see cref="ListControlViewModel"/> instead as that class handles the communication
    /// of the state of list-based WPF to the <see cref="Atdl4net.Model.Types.Support.EnumState"/> type that holds the state of each selectable item
    /// within the control.</remarks>
    public class ControlViewModel : INotifyPropertyChanged, INotifyValueChanged, INotifyValueChangeCompleted, IBindable<ViewModelControlCollection>
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Wpf.ViewModel");

        private bool _currentlyEvaluating = false;
        private bool _visible = true;
        private bool _enabled = true;
        private bool _firstValidationStateChangeNotification = true;
        private readonly DataEntryMode _dataEntryMode;
        private FixFieldValueProvider _fixFieldValues;
        private ViewModelStateRuleCollection _stateRules;
        private readonly IParameter _referencedParameter;

        /// <summary>
        /// Holds the validation state of this control view model.
        /// </summary>
        protected readonly ControlValidationState _validationState;

        /// <summary>
        /// Raised whenever the value of a property (UiValue, Visibility, IsEnabled) changes.
        /// </summary>
        /// <remarks>Implemented as part of INotifyPropertyChanged.</remarks>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raised whenever the WPF control's value changes.  Used to communicate value change notifications to Edits
        /// within StateRules for the strategy the underlying <see cref="Control_t"/> belongs to.
        /// </summary>
        /// <remarks>Implemented as part of INotifyValueChanged.</remarks>
        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        /// <summary>
        /// Raised when a value change has been fully processed.
        /// </summary>
        /// <remarks>Implemented as part of INotifyValueChangeCompleted.</remarks>
        public event EventHandler<ValueChangeCompletedEventArgs> ValueChangeCompleted;

        /// <summary>
        /// Raised whenever the validation state of this control changes.
        /// </summary>
        public event EventHandler<ValidationStateChangedEventArgs> ValidationStateChanged;

        /// <summary>
        /// Initializes a new ControlViewModel using the supplied <see cref="Control_t"/> as underlying control and the 
        /// supplied <see cref="IParameter"/> as referenced parameter.
        /// </summary>
        /// <param name="control">Underlying Control_t for this ControlViewModel.</param>
        /// <param name="referencedParameter">Parameter that this control relates to.  May be null.</param>
        /// <param name="mode">Data entry mode (create/amend/view).</param>
        protected ControlViewModel(Control_t control, IParameter referencedParameter, DataEntryMode mode)
        {
            UnderlyingControl = control;
            _referencedParameter = referencedParameter;
            _dataEntryMode = mode;

            _validationState = new ControlValidationState(control.Id);
        }

        /// <summary>
        /// Factory method for creating new ControlViewModel instances.
        /// </summary>
        /// <param name="underlyingStrategy"><see cref="Strategy_t"/> that this ControlViewModel's <see cref="Control_t"/> is a member of.</param>
        /// <param name="control">Underlying Control_t for this ControlViewModel.</param>
        /// <param name="mode">Data entry mode (create/amend/view).</param>
        /// <returns></returns>
        public static ControlViewModel Create(Strategy_t underlyingStrategy, Control_t control, IInputValueProvider initialValueProvider, DataEntryMode mode)
        {
            IParameter referencedParameter = null;

            if (control.ParameterRef != null)
            {
                referencedParameter = underlyingStrategy.Parameters[control.ParameterRef];

                if (referencedParameter == null)
                    throw ThrowHelper.New<ReferencedObjectNotFoundException>(ErrorMessages.UnresolvedParameterRefError, control.ParameterRef);
            }

            ControlViewModel controlViewModel;

#if !NET_40
            // This is to workaround a bug in .NET Framework 3.5 where it is possible for more than one radio button in a 
            // group to be checked at a time.
            if (control is RadioButton_t)
                controlViewModel = new RadioButtonViewModel(control as RadioButton_t, referencedParameter, mode);
            else
#endif
            if (control is ListControlBase)
                controlViewModel = ListControlViewModel.Create(control as ListControlBase, referencedParameter, mode);
            else if (InvalidatableControlViewModel.IsInvalidatable(control))
                controlViewModel = InvalidatableControlViewModel.Create(control, referencedParameter, mode);
            else
                controlViewModel = new ControlViewModel(control, referencedParameter, mode);

            controlViewModel._stateRules = new ViewModelStateRuleCollection(controlViewModel, control.StateRules);
            controlViewModel._fixFieldValues = new FixFieldValueProvider(initialValueProvider, underlyingStrategy.Parameters);

            return controlViewModel;
        }

        /// <summary>
        /// Indicates whether the parameter this control references is required or optional.  False if this control has no parameter.
        /// </summary>
        public bool IsRequiredParameter { get { return _referencedParameter != null ? _referencedParameter.Use == Use_t.Required : false; } }

        /// <summary>
        /// Gets the underlying <see cref="Control_t"/> that this ControlViewModel is responsible for.
        /// </summary>
        public Control_t UnderlyingControl { get; private set; }

        /// <summary>
        /// Gets the ParameterRef of this ControlViewModel's underlying <see cref="Control_t"/>.
        /// </summary>
        public string ParameterRef { get { return UnderlyingControl.ParameterRef; } }

        /// <summary>
        /// Gets the ID of this ControlViewModel's underlying <see cref="Control_t"/>.
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
        /// Gets/sets the user interface value for the <see cref="Control_t"/> that this ControlViewModel is responsible for.
        /// </summary>
        public virtual object UiValue
        {
            get { return UnderlyingControl.GetCurrentValue(); }

            set
            {
                _log.Debug(m => m("ControlViewModel for Control {0} value updated to '{1}' (data type {2}).",
                    Id, (value is ListItem_t) ? (value as ListItem_t).EnumId : value ?? "null", value != null ? value.GetType().Name : "N/A"));

                if (UnderlyingControl.GetCurrentValue() != value)
                {
                    object oldValue = UnderlyingControl.GetCurrentValue();

                    UnderlyingControl.SetValue(value);

                    NotifyValueChanged(oldValue, value);
                }
            }
        }

        /// <summary>
        /// Sets the visible state of the WPF control that this ControlViewModel is responsible for.
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
        /// Gets/sets the WPF visibility for the user interface control that this ControlViewModel is responsible for.
        /// </summary>
        public Visibility Visibility
        {
            get { return _visible ? Visibility.Visible : Visibility.Collapsed; }
            set { NotifyPropertyChanged("Visibility"); }
        }

        /// <summary>
        /// Gets/sets the WPF enabled/disabled state for the user interface control that this ControlViewModel is responsible for.
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
        /// Indicates whether this control value is currently valid, according to the StrategyEdits
        /// attached to the parent strategy and the control validations.
        /// </summary>
        public bool IsValid { get { return _validationState.CurrentState; } }

        /// <summary>
        /// Resets the state of this ControlViewModel, i.e., Enabled = true, Visible = true;
        /// </summary>
        public virtual void Reset()
        {
            Enabled = true;
            IsVisible = true;
        }

        /// <summary>
        /// Refreshes the state of all StateRules for this ControlViewModel's underlying <see cref="Control_t"/> and
        /// all control values (in terms of data binding).
        /// </summary>
        /// <param name="reevaluateStateRules">Set to true to force the control's state rules to be re-evaluated; to false otherwise.</param>
        public virtual void RefreshState(bool reevaluateStateRules)
        {
            _log.Debug(m => m("Refreshing state for control with ID {0}", Id));

            if (reevaluateStateRules)
                _stateRules.RefreshState();

            if (_referencedParameter != null && _referencedParameter.IsSet)
                NotifyValueChanged(null, UnderlyingControl.GetCurrentValue());
            else
                RefreshUiState(true);
        }

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
        /// <remarks>Note that the ValueChanged event is not intended for databinding, rather to notify Edit_t objects that one of their
        /// values has changed.</remarks>
        protected void NotifyValueChanged(object oldValue, object newValue)
        {
            _log.Debug(m => m("Control {0} value changed from '{1}' to '{2}'", Id, oldValue, newValue));

            NotifyPropertyChanged("UiValue");

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
            try
            {
                _currentlyEvaluating = true;

                bool previousState = _validationState.CurrentState;

                _log.Debug(m => m("Processing value change completed; validation state ahead of evaluation is {0}", previousState.ToString().ToLower()));

                EventHandler<ValueChangeCompletedEventArgs> valueChangeCompleted = ValueChangeCompleted;

                if (valueChangeCompleted != null)
                    valueChangeCompleted(this, new ValueChangeCompletedEventArgs(this));

                _validationState.Evaluate(_fixFieldValues);

                bool newState = _validationState.CurrentState;

                // We always notify the first state change notification even if the state hasn't changed from the default value (true)
                if (_firstValidationStateChangeNotification || previousState != newState)
                {
                    _log.Debug(m => m("Notifying validation state change; was {0}, now is {1}", previousState.ToString().ToLower(), newState.ToString().ToLower()));

                    _firstValidationStateChangeNotification = false;

                    NotifyValidationStateChanged(newState);
                }

                RefreshUiState(false);
            }
            finally
            {
                _currentlyEvaluating = false;
            }
        }

        /// <summary>
        /// Updates the state of the user interface in terms of the validity, tooltip and optionally value.
        /// </summary>
        /// <param name="includeValue"></param>
        protected void RefreshUiState(bool includeValue)
        {
            NotifyPropertyChanged("IsValid");
            NotifyPropertyChanged("ToolTip");

            if (includeValue)
                NotifyPropertyChanged("UiValue");
        }

        /// <summary>
        /// Notifies interested parties that the validation state of this control has changed.
        /// </summary>
        /// <param name="isValid">true if the control is now valid; false otherwise.</param>
        protected void NotifyValidationStateChanged(bool isValid)
        {
            EventHandler<ValidationStateChangedEventArgs> validationStateChanged = ValidationStateChanged;

            if (validationStateChanged != null)
                validationStateChanged(this, new ValidationStateChangedEventArgs(Id, isValid));
        }

        private void StrategyEditStateChanged(object sender, Notification.StateChangedEventArgs e)
        {
            StrategyEditViewModel strategyEdit = sender as StrategyEditViewModel;

            _log.Debug(m => m("StrategyEdit ({0}) state changed for control {1}; was {2}, now is {3}",
                strategyEdit.InternalId, Id, e.OldState.ToString().ToLower(), e.NewState.ToString().ToLower()));

            // To save the property changed notifications being raised multiple times during and evaluation,
            // ignore this event if that is what we're doing.  (The primary purpose of this event is to handle
            // the scenario where another control is changing value.)
            if (!_currentlyEvaluating)
                RefreshUiState(false);
            else
                _log.Debug("Ignoring StrategyEdit change notification as this control's value change is currently being processed");
        }

        internal void BindStrategyEdit(StrategyEditViewModel strategyEditViewModel)
        {
            strategyEditViewModel.StateChanged += new EventHandler<StateChangedEventArgs>(StrategyEditStateChanged);

            _validationState.Add(strategyEditViewModel);
        }

        internal void UnbindStrategyEdit(StrategyEditViewModel strategyEditViewModel)
        {
            strategyEditViewModel.StateChanged -= new EventHandler<StateChangedEventArgs>(StrategyEditStateChanged);

            _validationState.Remove(strategyEditViewModel);
        }

        #region IBindable<ViewControlCollection> Members

        /// <summary>
        /// Binds this control's StateRules to the <see cref="ViewModelControlCollection"/> this ControlViewModel is a
        /// member of.
        /// </summary>
        /// <param name="target">ViewModelControlCollection this ControlViewModel is a member of.</param>
        void IBindable<ViewModelControlCollection>.Bind(ViewModelControlCollection target)
        {
            (_stateRules as IBindable<ViewModelControlCollection>).Bind(target);
        }

        #endregion
    }
}
