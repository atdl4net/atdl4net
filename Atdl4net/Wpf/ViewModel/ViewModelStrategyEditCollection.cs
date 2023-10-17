#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Collections.ObjectModel;
using System.Linq;
using Atdl4net.Fix;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Elements;

namespace Atdl4net.Wpf.ViewModel
{
    /// <summary>
    /// Collection of <see cref="StrategyEditViewModel"/>s, part of the Atdl4net ViewModel.
    /// </summary>
    public class ViewModelStrategyEditCollection : Collection<StrategyEditViewModel>, IDisposable
    {
        private bool _disposed;
        private ViewModelControlCollection _controls;

        /// <summary>
        /// Initializes a new <see cref="ViewModelStrategyEditCollection"/>.
        /// </summary>
        /// <param name="underlyingStrategyEdits">Set of <see cref="StrategyEdit_t"/>s that this collection is responsible for.</param>
        /// <param name="controls">Collection of controls for the strategy this <see cref="ViewModelStrategyEditCollection"/>
        /// corresponds to.</param>
        public ViewModelStrategyEditCollection(StrategyEditCollection underlyingStrategyEdits, ViewModelControlCollection controls)
        {
            foreach (StrategyEdit_t strategyEdit in underlyingStrategyEdits) 
            {
                _controls = controls;

                StrategyEditViewModel strategyEditViewModel = new StrategyEditViewModel(strategyEdit);

                Add(strategyEditViewModel);

                strategyEditViewModel.Bind(from c in _controls 
                                           where c.ParameterRef != null && strategyEdit.Sources.Contains(c.ParameterRef) 
                                           select c);
            }
        }

        /// <summary>
        /// Evaluates all the underlying <see cref="StrategyEdit_t"/>s and notifies any interested parties of change
        /// of state.
        /// </summary>
        /// <param name="inputValueProvider">Provider that providers access to any additional FIX field values that may 
        /// be required in the Edit evaluation.</param>
        /// <returns>Summary state of all StrategyEdits after the evaluation.</returns>
        public bool EvaluateAll(FixFieldValueProvider inputValueProvider)
        {
            bool overallState = true;

            foreach (StrategyEditViewModel strategyEdit in this)
                overallState &= strategyEdit.Evaluate(inputValueProvider);

            return overallState;
        }

        /// <summary>
        /// Evaluates all the underlying <see cref="StrategyEdit_t"/>s and notifies any interested parties of change
        /// of state.
        /// </summary>
        /// <param name="inputValueProvider">Provider that providers access to any additional FIX field values that may 
        /// be required in the Edit evaluation.</param>
        /// <returns>Summary state of all StrategyEdits after the evaluation.</returns>
        public bool EvaluateAffected(FixFieldValueProvider inputValueProvider, FixField fixField)
        {
            bool overallState = true;

            foreach (StrategyEditViewModel strategyEdit in this)
                if (strategyEdit.Sources.Contains(Enum.GetName(typeof(FixField), fixField)))
                    overallState &= strategyEdit.Evaluate(inputValueProvider);

            return overallState;
        }

        #region IDisposable Members and support

        void IDisposable.Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Cleans up when this <see cref="ViewModelStrategyEditCollection"/> is no longer required.
        /// </summary>
        /// <param name="disposing">True if this object is being disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    foreach (StrategyEditViewModel strategyEdit in this)
                    {
                        foreach (ControlViewModel control in _controls)
                        {
                            string targetParameter = control.UnderlyingControl.ParameterRef;

                            if (strategyEdit.Sources.Contains(targetParameter))
                                control.UnbindStrategyEdit(strategyEdit);
                        }
                    }

                    _controls = null;
                }

                _disposed = true;
            }
        }

        #endregion
    }
}
