#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using Atdl4net.Diagnostics;
using Atdl4net.Fix;
using Atdl4net.Model.Elements;
using Atdl4net.Notification;
using Atdl4net.Resources;

namespace Atdl4net.Wpf.ViewModel
{
    /// <summary>
    /// View model class for <see cref="StrategyEdit_t">StrategyEdit</see>s.
    /// </summary>
    public class StrategyEditViewModel
    {
        private readonly StrategyEdit_t _underlyingStrategyEdit;

        /// <summary>
        /// Initializes a new <see cref="StrategyEditViewModel"/> with the supplied underlying <see cref="StrategyEdit_t"/>.
        /// </summary>
        /// <param name="underlyingStrategyEdit"><see cref="StrategyEdit_t"/> that this view model is responsible for.  May not be null.</param>
        public StrategyEditViewModel(StrategyEdit_t underlyingStrategyEdit)
        {
            if (underlyingStrategyEdit == null)
                throw ThrowHelper.New<ArgumentNullException>(this, InternalErrors.UnexpectedNullReference, "underlyingStrategyEdit", "StrategyEdit_t");

            _underlyingStrategyEdit = underlyingStrategyEdit;
        }

        /// <summary>
        /// Raised whenever the state of the underlying <see cref="StrategyEdit_t"/> changes.
        /// </summary>
        public event EventHandler<StateChangedEventArgs> StateChanged;

        /// <summary>
        /// Gets the set of sources for the data to be evaluated as part of the underlying StrategyEdit.
        /// </summary>
        public HashSet<string> Sources { get { return _underlyingStrategyEdit.Sources; } }

        /// <summary>
        ///  Gets the internal ID for the underlying StrategyEdit; used to support lookups when applying the results of 
        ///  validations to controls.
        /// </summary>
        public string InternalId { get { return _underlyingStrategyEdit.InternalId; } }

        /// <summary>
        /// Gets the current state of the underlying StrategyEdit.
        /// </summary>
        public bool CurrentState { get { return _underlyingStrategyEdit.CurrentState; } }

        /// <summary>
        /// Gets the error message for the underlying StrategyEdit.
        /// </summary>
        public string ErrorMessage { get { return _underlyingStrategyEdit.ErrorMessage; } }

        /// <summary>
        /// Evaluates the underlying <see cref="StrategyEdit_t"/> and notifies any interested parties of change
        /// of state.
        /// </summary>
        /// <returns>State of this StrategyEdit after the evaluation.</returns>
        /// <param name="additionalValues">Any additional FIX field values that may be required in the Edit evaluation.</param>
        public bool Evaluate(FixFieldValueProvider additionalValues)
        {
            bool oldState = _underlyingStrategyEdit.CurrentState;

            _underlyingStrategyEdit.Evaluate(additionalValues);

            bool newState = _underlyingStrategyEdit.CurrentState;

            if (oldState != newState)
                NotifyStateChanged(oldState, newState);

            return newState;
        }

        /// <summary>
        /// Binds the supplied set of ControlViewModels to this StrategyEditViewModel.
        /// </summary>
        /// <param name="controlViewModels"><see cref="IEnumerable<ControlViewModel>"/> containing the set of ControlViewModels
        /// that this StrategyEdit sources its data from.</param>
        public void Bind(IEnumerable<ControlViewModel> controlViewModels)
        {
            foreach (ControlViewModel controlViewModel in controlViewModels)
                controlViewModel.BindStrategyEdit(this);
        }

        /// <summary>
        /// Binds the supplied set of ControlViewModels to this StrategyEditViewModel.
        /// </summary>
        /// <param name="controlViewModels"><see cref="IEnumerable<ControlViewModel>"/> containing the set of ControlViewModels
        /// that this StrategyEdit sources its data from.</param>
        public void Unbind(IEnumerable<ControlViewModel> controlViewModels)
        {
            foreach (ControlViewModel controlViewModel in controlViewModels)
                controlViewModel.UnbindStrategyEdit(this);
        }

        private void NotifyStateChanged(bool oldState, bool newState)
        {
            EventHandler<StateChangedEventArgs> stateChanged = StateChanged;

            if (stateChanged != null)
                stateChanged(this, new StateChangedEventArgs(oldState, newState));
        }
    }
}
