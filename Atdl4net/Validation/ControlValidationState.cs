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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atdl4net.Fix;
using Atdl4net.Wpf.ViewModel;
using Common.Logging;

namespace Atdl4net.Validation
{
    /// <summary>
    /// Used to store the validation state for a control.
    /// </summary>
    public class ControlValidationState
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Validation");

        private bool _currentState;
        private readonly string _controlId;
        private readonly List<StrategyEditViewModel> _strategyEdits = new List<StrategyEditViewModel>();
        private ValidationResult _parameterValidationResult;

        /// <summary>
        /// Initializes a new <see cref="ControlValidationState"/>.
        /// </summary>
        /// <param name="controlId"></param>
        public ControlValidationState(string controlId)
        {
            _controlId = controlId;
            _currentState = true;
        }

        /// <summary>
        /// Gets the current state of this set of StrategyEdits.
        /// </summary>
        public bool CurrentState { get { return _currentState; } }

        /// <summary>
        /// Used to hold the results obtained from the parameter set and validation operation.
        /// </summary>
        public ValidationResult ParameterValidationResult { set { _parameterValidationResult = value; } }

        /// <summary>
        /// Adds the supplied StrategyEditViewModel to this <see cref="ControlValidationState"/>.
        /// </summary>
        /// <param name="strategyEdit"><see cref="StrategyEditViewModel"/> to add to this <see cref="ControlValidationState"/>.</param>
        public void Add(StrategyEditViewModel strategyEdit)
        {
            _strategyEdits.Add(strategyEdit);
        }

        /// <summary>
        /// Removes the supplied StrategyEditViewModel from this <see cref="ControlValidationState"/>.
        /// </summary>
        /// <param name="strategyEdit"><see cref="StrategyEditViewModel"/> to remove from this <see cref="ControlValidationState"/>.</param>
        public void Remove(StrategyEditViewModel strategyEdit)
        {
            _strategyEdits.Remove(strategyEdit);
        }

        /// <summary>
        /// Evaluates all the <see cref="StrategyEdit_t"/>s for this control.
        /// </summary>
        /// <param name="additionalValues">Any additional FIX field values that may be required in the Edit evaluation.</param>
        public void Evaluate(FixFieldValueProvider additionalValues)
        {
            _log.Debug(m => m("Evaluating ValidationState for control {0}, CurrentState = {1}", _controlId, _currentState.ToString().ToLower()));

            // Evaluating the StrategyEdits may give us meaningless information if the parameter value
            // didn't validate, but we go ahead and do it anyway because failing to do leaves us in an
            // indeterminate state from this value change.
            _currentState = _parameterValidationResult == null || _parameterValidationResult.IsValid;

            foreach (StrategyEditViewModel strategyEdit in _strategyEdits)
                _currentState &= strategyEdit.Evaluate(additionalValues);

            _log.Debug(m => m("Evaluated ValidationState for control {0}, CurrentState = {1}", _controlId, _currentState.ToString().ToLower()));
        }

        /// <summary>
        /// Gets the error messages for all <see cref="StrategyEdit_t"/>s that evaluate to false.
        /// </summary>
        public string ErrorText
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                IEnumerable<StrategyEditViewModel> strategyEditsInError = from s in _strategyEdits where !s.CurrentState select s;

                int count = strategyEditsInError.Count();

                if (_parameterValidationResult != null && !_parameterValidationResult.IsValid)
                {
                    sb.Append(_parameterValidationResult.ErrorText);

                    if (count > 0)
                        sb.AppendLine();
                }

                foreach (StrategyEditViewModel strategyEdit in (from s in _strategyEdits where !s.CurrentState select s))
                {
                    sb.Append(strategyEdit.ErrorMessage);

                    if (--count > 0)
                        sb.AppendLine();
                }

                _log.Debug(m => m("ValidationState for control {0} = '{1}'", _controlId, sb.ToString()));

                return sb.ToString();
            }
        }
    }
}
