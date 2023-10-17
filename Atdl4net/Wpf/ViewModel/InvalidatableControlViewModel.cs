#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Linq;
using Atdl4net.Model.Controls;
using Atdl4net.Model.Elements;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Resources;
using Atdl4net.Utility;
using Atdl4net.Validation;

namespace Atdl4net.Wpf.ViewModel
{
    /// <summary>
    /// Represents a control view model where the control itself can be in an invalid state.  Examples of this type of
    /// control include numerical spinners and clock controls, because it is possible for the user to type a non-numeric
    /// value directly into the control.
    /// </summary>
    public class InvalidatableControlViewModel : ControlViewModel
    {
        private static readonly ValidationResult _invalidResult = new ValidationResult(ValidationResult.ResultType.Invalid, ErrorMessages.InvalidControlValueError);

        /// <summary>
        /// Initializes a new <see cref="InvalidatableControlViewModel"/> instance.
        /// </summary>
        /// <param name="control">Control that this control view model corresponds to.</param>
        /// <param name="referencedParameter">Parameter for this control.  May be null.</param>
        private InvalidatableControlViewModel(Control_t control, IParameter referencedParameter)
            : base(control, referencedParameter)
        {
        }

        /// <summary>
        /// Gets/sets the validity state of this control.
        /// </summary>
        public bool IsContentValid
        {
            get { return _validationState.ControlValidationResult == null || _validationState.ControlValidationResult.IsValid; }

            set
            {
                if (_renderInProgress)
                    return;

                _validationState.ControlValidationResult = value ? ValidationResult.ValidResult : _invalidResult;

                NotifyValidationStateChanged(_validationState.CurrentState);

                RefreshUiState(false);
            }
        }

        /// <summary>
        /// Factory method for creating new InvalidatableControlViewModel instances.
        /// </summary>
        /// <param name="control">Underlying Control_t for this ControlViewModel.</param>
        /// <param name="referencedParameter">Parameter that the specified Control_t relates to.  May be null.</param>
        /// <returns>New instance of InvalidatableControlViewModel.</returns>
        public static InvalidatableControlViewModel Create(Control_t control, IParameter referencedParameter)
        {
            return new InvalidatableControlViewModel(control, referencedParameter);
        }

        /// <summary>
        /// Determines whether the supplied control is an invalidatable control.
        /// </summary>
        /// <param name="control">Control_t to check.</param>
        /// <returns>true if the control is invalidatable; false otherwise.</returns>
        public static bool IsInvalidatable(Control_t control)
        {
            return (control is SingleSpinner_t || control is DoubleSpinner_t || control is Clock_t);
        }
    }
}
