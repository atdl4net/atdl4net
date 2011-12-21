#region Copyright (c) 2010-2011, Cornerstone Technology Limited. http://atdl4net.org
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
using System.Windows.Controls;
using Atdl4net.Model.Controls;
#endif

namespace Atdl4net.Wpf.ViewModel
{
    /// <summary>
    /// Collection of <see cref="ControlWrapper"/>s, part of the Atdl4net ViewModel. 
    /// </summary>
    public class ViewModelControlCollection : KeyedCollection<string, ControlWrapper>, IDisposable
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

        private RadioButtonGroupManagerCollection _radioButtonGroups = new RadioButtonGroupManagerCollection();

        #endregion
#endif

        private bool _disposed;
        private Strategy_t _underlyingStrategy;
        private ViewModelStrategyEditCollection _strategyEdits;

        /// <summary>
        /// Initializes a new <see cref="ViewModelControlCollection"/> 
        /// </summary>
        /// <param name="strategy">Strategy that the underlying controls belong to.</param>
        /// <param name="mode">Data entry mode.</param>
        public ViewModelControlCollection(Strategy_t strategy, DataEntryMode mode)
        {
            _underlyingStrategy = strategy;

            foreach (Control_t control in strategy.Controls)
            {
                ControlWrapper controlWrapper = ControlWrapper.Create(strategy, control, mode);

                Add(controlWrapper);

                controlWrapper.ValueChangeCompleted += new EventHandler<ValueChangeCompletedEventArgs>(ControlValueChangeCompleted);

// Special treatment for radio buttons under Framework 3.5
#if !NET_40
                if (control is RadioButton_t)
                    RegisterRadioButton(control as RadioButton_t, controlWrapper as RadioButtonWrapper);
#endif
                //string targetParam = control.ParameterRef;

                //if (targetParam != null)
                //    _strategyEdits.Add(targetParam, 
                //        from s in strategy.StrategyEdits where s.Sources.Contains(targetParam) select s);
            }

            _strategyEdits = new ViewModelStrategyEditCollection(strategy.StrategyEdits, this);

            BindStateRules();
        }

        private void BindStateRules()
        {
            foreach (ControlWrapper controlWrapper in Items)
                (controlWrapper as IBindable<ViewModelControlCollection>).Bind(this);
        }

        public void RefreshState()
        {
            foreach (ControlWrapper controlWrapper in Items)
                controlWrapper.RefreshState();
        }

        protected override string GetKeyForItem(ControlWrapper item)
        {
            return item.Id;
        }

        private void ControlValueChangeCompleted(object sender, ValueChangeCompletedEventArgs e)
        {
            if (e != null)
            {
                // Update the affected parameter
                e.Control.UpdateParameterValue();

                //_strategyEdits.EvaluateAffected(e.Control.ParameterRef);

                //string parameter = e.Control.ParameterRef;

                //var results = _strategyEdits[parameter].Evaluate();

                ////StrategyEditC

                ////ValidationResult[] results = _underlyingStrategy.ValidateAffectedStrategyEdits(parameter);

                //ValidationResult[] results = _underlyingStrategy.ValidateStrategyEdits();

                //ApplyValidationsToControls(results);

                //foreach (var result in results)
                //    _log.Debug(m => m("Control {0} value changed | result = {1} | {2}", e.Control.Id, result.IsValid, result.ErrorContent));
            }
        }

//        private void ApplyValidationsToControls(ValidationResult[] results)
//        {
//            foreach (var control in this)
//            {
//                string param = control.UnderlyingControl.ParameterRef;

//                if (param != null)
//                {
////                    var resultsForControl = results.GetResultsByParam(param);



//                }
//            }
//        }

#if !NET_40
        private void RegisterRadioButton(RadioButton_t radioButton, RadioButtonWrapper controlWrapper)
        {
            string groupName = radioButton.RadioGroup;

            if (!string.IsNullOrEmpty(groupName))
            {
                RadioButtonGroupManager groupManager;

                if (_radioButtonGroups.Contains(groupName))
                    groupManager = _radioButtonGroups[groupName];
                else
                    groupManager = _radioButtonGroups.AddGroup(groupName);

                groupManager.RegisterRadioButton(controlWrapper);

                controlWrapper.RadioButtonGroupManager = groupManager;
            }
        }
#endif

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
                    (_strategyEdits as IDisposable).Dispose();

                    _strategyEdits = null;
                    _underlyingStrategy = null;
                }

                _disposed = true;
            }
        }

        #endregion
    }
}
