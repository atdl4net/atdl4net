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
using Atdl4net.Fix;
using Atdl4net.Model.Elements;
using Atdl4net.Utility;

namespace Atdl4net.Wpf.ViewModel
{
    /// <summary>
    /// View Model that provides the link between the Atdl4net Model (<see cref="Strategy_t"/>) and the 
    /// View (<see cref="Atdl4netControl"/>).
    /// </summary>
    public class StrategyViewModel : IDisposable
    {
        public const string ComboBoxSizerKey = "atdl4netComboBoxSizerKey";
        public const string DataContextKey = "atdl4netViewModelKey";

        private bool _disposed;
        private readonly Strategy_t _underlyingStrategy;

        /// <summary>
        /// Gets the set of controls (<see cref="ControlViewModel"/>s) for this strategy (<see cref="StrategyViewModel"/>).
        /// </summary>
        public ViewModelControlCollection Controls { get; private set; }

        /// <summary>
        /// Gets the set of strategy edits (<see cref="StrategyEditViewModel"/>s) for this strategy (<see cref="StrategyViewModel"/>).
        /// </summary>
        public ViewModelStrategyEditCollection StrategyEdits { get; private set; }

        /// <summary>
        /// Determines whether all controls within the strategy have internally valid state.  Note that this is 
        /// not the same as whether the controls have valid parameter values.
        /// </summary>
        public bool AreAllControlsInternallyValid { get { return Controls.AreAllValid; } }

        /// <summary>
        /// Initializes a new <see cref="StrategyViewModel"/> 
        /// </summary>
        /// <param name="strategy"><see cref="Strategy_t"/> for this View Model.</param>
        public StrategyViewModel(Strategy_t strategy, IInitialFixValueProvider initialValueProvider)
        {
            _underlyingStrategy = strategy;

            Controls = new ViewModelControlCollection(strategy, initialValueProvider);
            StrategyEdits = Controls.StrategyEdits;
        }

        /// <summary>
        /// Evaluates all strategy edits within the currently selected strategy.
        /// </summary>
        /// <param name="inputValueProvider">Provider that providers access to any additional FIX field values that may 
        /// be required in the Edit evaluation.</param>
        /// <returns>Summary state of all StrategyEdits after the evaluation.</returns>
        public bool EvaluateAllStrategyEdits(IInitialFixValueProvider inputValueProvider)
        {
            FixFieldValueProvider additionalValues = inputValueProvider == null ?
                FixFieldValueProvider.Empty : new FixFieldValueProvider(inputValueProvider, _underlyingStrategy.Parameters);

            return StrategyEdits.EvaluateAll(additionalValues);
        }

        /// <summary>
        /// Evaluates all strategy edits within the currently selected strategy.
        /// </summary>
        /// <param name="inputValueProvider">Provider that providers access to any additional FIX field values that may 
        /// be required in the Edit evaluation.</param>
        /// <returns>Summary state of all StrategyEdits after the evaluation.</returns>
        public bool EvaluateAffectedStrategyEdits(IInitialFixValueProvider inputValueProvider, FixField updatedField)
        {
            FixFieldValueProvider additionalValues = inputValueProvider == null ?
                FixFieldValueProvider.Empty : new FixFieldValueProvider(inputValueProvider, _underlyingStrategy.Parameters);

            bool result = StrategyEdits.EvaluateAffected(additionalValues, updatedField);

            foreach (ControlViewModel control in Controls)
                control.OnValueChangeCompleted();

            return result;
        }

        /// <summary>
        /// Used to inform the view model that rendering is about to begin.
        /// </summary>
        public void BeginRender()
        {
            foreach (ControlViewModel control in Controls)
                control.IsRenderInProgress = true;
        }

        /// <summary>
        /// Used to inform the view model that rendering has finished.
        /// </summary>
        public void EndRender()
        {
            foreach (ControlViewModel cvm in Controls)
                cvm.IsRenderInProgress = false;
        }

        /// <summary>
        /// Updates the data entry mode for all control view models.
        /// </summary>
        /// <param name="dataEntryMode">New DataEntryMode.</param>
        public void UpdateDataEntryMode(DataEntryMode dataEntryMode)
        {
            foreach (ControlViewModel cvm in Controls)
                cvm.DataEntryMode = dataEntryMode;
        }

        /// <summary>
        /// Refreshes the state of the view for all the state rules for the selected strategy.  Note that this method does
        /// not cause the state rules to be re-evaluated.
        /// </summary>
        public void RefreshViewState()
        {
            Controls.RefreshState();
        }

        #region IDisposable Members and support

        void IDisposable.Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Cleans up when this instance of <see cref="StrategyViewModel"/> is no longer required.
        /// </summary>
        /// <param name="disposing">True if this object is being disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    (Controls as IDisposable).Dispose();

                    Controls = null;
                }

                _disposed = true;
            }
        }

        #endregion
    }
}
