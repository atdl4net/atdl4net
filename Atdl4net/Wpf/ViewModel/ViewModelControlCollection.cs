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
using System.Collections.ObjectModel;
using System.Linq;
using Atdl4net.Model.Elements;
using Atdl4net.Notification;
using Atdl4net.Utility;
using Common.Logging;
#if !NET_40
using Atdl4net.Model.Controls;
#endif

namespace Atdl4net.Wpf.ViewModel
{
    /// <summary>
    /// Collection of <see cref="ControlViewModel"/>s, part of the Atdl4net ViewModel. 
    /// </summary>
    public class ViewModelControlCollection : KeyedCollection<string, ControlViewModel>, IDisposable
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Wpf.ViewModel");

#if !NET_40
        #region Special support for RadioButtons on .NET 3.5

        private class RadioButtonGroupManagerCollection : KeyedCollection<string, RadioButtonGroupManager>
        {
            protected override string  GetKeyForItem(RadioButtonGroupManager item)
            {
 	            return item.GroupName;
            }

            public RadioButtonGroupManager AddGroup(string groupName)
            {
                RadioButtonGroupManager groupManager = new RadioButtonGroupManager(groupName);

                Add(groupManager);

                return groupManager;
            }
        }

        private readonly RadioButtonGroupManagerCollection _radioButtonGroups = new RadioButtonGroupManagerCollection();

        #endregion
#endif

        private bool _disposed;
        private ViewModelStrategyEditCollection _strategyEdits;

        /// <summary>
        /// Raised whenever the validation state of any control changes.
        /// </summary>
        public event EventHandler<ValidationStateChangedEventArgs> ValidationStateChanged;

        /// <summary>
        /// Initializes a new <see cref="ViewModelControlCollection"/> 
        /// </summary>
        /// <param name="strategy">Strategy that the underlying controls belong to.</param>
        /// <param name="mode">Data entry mode.</param>
        public ViewModelControlCollection(Strategy_t strategy, IInitialFixValueProvider initialValueProvider)
        {
            foreach (Control_t control in strategy.Controls)
            {
                ControlViewModel controlViewModel = ControlViewModel.Create(strategy, control, initialValueProvider);

                Add(controlViewModel);

                controlViewModel.ValueChangeCompleted += new EventHandler<ValueChangeCompletedEventArgs>(ControlValueChangeCompleted);
                controlViewModel.ValidationStateChanged += new EventHandler<ValidationStateChangedEventArgs>(ControlValidationStateChanged);

                // Special treatment for radio buttons under Framework 3.5
#if !NET_40
                if (control is RadioButton_t)
                    RegisterRadioButton(control as RadioButton_t, controlViewModel as RadioButtonViewModel);
#endif
            }

            _strategyEdits = new ViewModelStrategyEditCollection(strategy.StrategyEdits, this);

            BindStateRules();
        }

        /// <summary>
        /// Determines whether all controls have internally valid state.  Note that this is not the same as whether
        /// the controls have valid parameter values.
        /// </summary>
        public bool AreAllValid { get { return this.All(cvm => (!(cvm is InvalidatableControlViewModel) || (cvm as InvalidatableControlViewModel).IsContentValid)); } }

        /// <summary>
        /// Gets the collection of StrategyEditViewModels for the currently selected strategy.
        /// </summary>
        public ViewModelStrategyEditCollection StrategyEdits { get { return _strategyEdits; } }

        /// <summary>
        /// Refreshes the state of the view (without re-evaluating) all the state rules for the selected strategy.
        /// </summary>
        public void RefreshState()
        {
            _log.Debug("Refreshing state for all controls");

            foreach (ControlViewModel controlViewModel in Items)
                controlViewModel.RefreshState();
        }

        /// <summary>
        /// Gets the unique key for the supplied item.  The key is used to allow keyed access to the collection.
        /// </summary>
        /// <param name="item">Item to determine key for.</param>
        /// <returns>Unique key for item.</returns>
        protected override string GetKeyForItem(ControlViewModel item)
        {
            return item.Id;
        }

        #region Private Methods

        private void ControlValidationStateChanged(object sender, ValidationStateChangedEventArgs e)
        {
            EventHandler<ValidationStateChangedEventArgs> validationStateChanged = ValidationStateChanged;

            if (validationStateChanged != null)
                validationStateChanged(this, e);
        }

        private void BindStateRules()
        {
            foreach (ControlViewModel controlViewModel in Items)
                (controlViewModel as IBindable<ViewModelControlCollection>).Bind(this);
        }

        private void ControlValueChangeCompleted(object sender, ValueChangeCompletedEventArgs e)
        {
            // Update the affected parameter
            if (e != null)
                e.Control.UpdateParameterValue();
        }

#if !NET_40
        private void RegisterRadioButton(RadioButton_t radioButton, RadioButtonViewModel controlViewModel)
        {
            string groupName = radioButton.RadioGroup;

            if (!string.IsNullOrEmpty(groupName))
            {
                RadioButtonGroupManager groupManager;

                if (_radioButtonGroups.Contains(groupName))
                    groupManager = _radioButtonGroups[groupName];
                else
                    groupManager = _radioButtonGroups.AddGroup(groupName);

                groupManager.RegisterRadioButton(controlViewModel);

                controlViewModel.RadioButtonGroupManager = groupManager;
            }
        }
#endif
        #endregion

        #region IDisposable Members and support

        void IDisposable.Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Cleans up when this instance of FnsPreprocessor is no longer required.
        /// </summary>
        /// <param name="disposing">True if this object is being disposed.</param>
        /// <remarks>Required because _messageProcessor is a static member and during test cases, we cannot guarantee 
        /// when the finalizer will run.  An explicit call to Dispose() at the end of each test ensures that we start 
        /// each new test with a clean object.</remarks>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    foreach (ControlViewModel controlViewModel in this)
                    {
                        controlViewModel.ValueChangeCompleted += new EventHandler<ValueChangeCompletedEventArgs>(ControlValueChangeCompleted);
                        controlViewModel.ValidationStateChanged += new EventHandler<ValidationStateChangedEventArgs>(ControlValidationStateChanged);
                    }

                    (_strategyEdits as IDisposable).Dispose();

                    _strategyEdits = null;
                }

                _disposed = true;
            }
        }

        #endregion
    }
}
