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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;
using Atdl4net.Configuration;
using Atdl4net.Diagnostics;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Fix;
using Atdl4net.Model.Elements;
using Atdl4net.Notification;
using Atdl4net.Resources;
using Atdl4net.Utility;
using Atdl4net.Wpf.View;
using Atdl4net.Wpf.ViewModel;
using Common.Logging;
using ValidationResult = Atdl4net.Validation.ValidationResult;

namespace Atdl4net.Wpf
{
    /// <summary>
    /// Custom control for rendering FIXatdl strategies.
    /// </summary>
    public partial class AtdlControl : UserControl, IInitialFixValueProvider, INotifyPropertyChanged
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Wpf");

        private string _xaml;  // Used for debugging purposes
        private bool _inputValuesSet = false;

        #region Dependency Properties

        /// <summary>
        /// Dependency property that provides storage for the data entry mode for this control.
        /// </summary>
        public static readonly DependencyProperty DataEntryModeProperty =
            DependencyProperty.Register("DataEntryMode", typeof(DataEntryMode), typeof(AtdlControl),
                new FrameworkPropertyMetadata(DataEntryMode.Create, OnDataEntryModeChanged));

        /// <summary>
        /// Dependency property that provides storage for the flag that enables and disables rendering.
        /// </summary>
        public static readonly DependencyProperty IsRenderingDisabledProperty =
            DependencyProperty.Register("IsRenderingDisabled", typeof(bool), typeof(AtdlControl),
                new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Dependency property that provides storage for the currently selected strategy for this control.
        /// </summary>
        public static readonly DependencyProperty StrategyProperty =
            DependencyProperty.Register("Strategy", typeof(Strategy_t), typeof(AtdlControl),
            new FrameworkPropertyMetadata(OnStrategyPropertyChanged));

        /// <summary>
        /// Dependency property that provides storage for the input FIX values for the currently selected strategy for this control.
        /// </summary>
        public static readonly DependencyProperty InputFixValuesProperty =
            DependencyProperty.Register("InputFixValues", typeof(FixTagValuesCollection), typeof(AtdlControl),
                new FrameworkPropertyMetadata(OnInputFixValuesChanged));

        /// <summary>
        /// Dependency property that provides storage for the output FIX values for the currently selected strategy for this control.
        /// </summary>
        public static readonly DependencyProperty OutputFixValuesProperty =
            DependencyProperty.Register("OutputFixValues", typeof(FixTagValuesCollection), typeof(AtdlControl),
                new FrameworkPropertyMetadata(FixTagValuesCollection.Empty));

        #endregion

        #region Events

        /// <summary>
        /// Raised whenever an exception occurs when setting the <see cref="Strategy"/> property.
        /// </summary>
        /// <remarks>This event is provided because when using Adl4net with data binding, some exceptions are swallowed
        /// by the WPF run-time.</remarks>
        public event EventHandler<UnhandledExceptionEventArgs> ExceptionOccurred;

        /// <summary>
        /// Raised whenever a property on this control has changed value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raised whenever the validation state of this control has changed.
        /// </summary>
        public event EventHandler<ValidationStateChangedEventArgs> ValidationStateChanged;

        /// <summary>
        /// Raised whenever the selected strategy has changed. 
        /// </summary>
        public event EventHandler<StrategyChangedEventArgs> StrategyChanged;

        #endregion

        /// <summary>
        /// Initializes a new <see cref="AtdlControl"/> instance.
        /// </summary>
        public AtdlControl()
        {
            InitializeComponent();

            Application.Current.Resources[StrategyViewModel.ComboBoxSizerKey] = new WpfComboBoxSizer() { ExampleComboBox = new ComboBox(), InitialComboWidth = 28 };
        }

        #region Properties

        /// <summary>
        /// Gets the collection of child controls for this control.
        /// </summary>
        public UIElementCollection Children { get { return controlRoot.Children; } }

        /// <summary>
        /// Gets the XAML for the currently selected strategy.  (Intended for debugging purposes only.)
        /// </summary>
        public string CurrentXaml { get { return _xaml; } }

        /// <summary>
        /// Gets/sets the name of the .NET assembly to be used to provide custom rendering of controls.
        /// </summary>
        public string CustomControlRendererAssembly
        {
            get { return WpfStrategyPanelRenderer.CustomControlRenderer; }
            set { WpfStrategyPanelRenderer.CustomControlRenderer = value; }
        }

        /// <summary>
        /// Gets/sets the data entry mode to be used (create order/amend order/view order).
        /// </summary>
        public DataEntryMode DataEntryMode
        {
            get { return (DataEntryMode)GetValue(DataEntryModeProperty); }
            set { SetValue(DataEntryModeProperty, value); }
        }

        /// <summary>
        /// Gets/sets a flag that is used to determine whether to render a strategy when it is set
        /// via the <see cref="Strategy"/> property.  This property is useful when trying to debug
        /// custom renderers.
        /// </summary>
        public bool IsRenderingDisabled
        {
            get { return (bool)GetValue(IsRenderingDisabledProperty); }
            set { SetValue(IsRenderingDisabledProperty, value); }
        }

        /// <summary>
        /// Gets/sets the currently selected strategy for this control.  Sets a new strategy causes the AtdlControl
        /// to render the FIXatdl, unless IsRenderingDisabled is set to true.
        /// </summary>
        public Strategy_t Strategy
        {
            get { return (Strategy_t)GetValue(StrategyProperty); }
            set { SetValue(StrategyProperty, value); }
        }

        /// <summary>
        /// Gets/sets the input FIX values for this control.
        /// </summary>
        public FixTagValuesCollection InputFixValues
        {
            get { return (FixTagValuesCollection)GetValue(InputFixValuesProperty); }
            set { SetValue(InputFixValuesProperty, value); }
        }

        /// <summary>
        /// Gets/sets the output FIX values for this control.
        /// </summary>
        public FixTagValuesCollection OutputFixValues
        {
            get { return (FixTagValuesCollection)GetValue(OutputFixValuesProperty); }
            set { SetValue(OutputFixValuesProperty, value); }
        }

        /// <summary>
        /// Determines whether all controls that are populated have valid values, and that all parameters therefore have valid
        /// values.
        /// </summary>
        public bool IsValid
        {
            get
            {
                bool isValid = false;

                if (Strategy != null && ViewModel != null)
                {
                    IList<ValidationResult> validationResults;

                    if (ViewModel.AreAllControlsInternallyValid &&
                        Strategy.TryUpdateParameterValuesFromControls(true, out validationResults))
                        isValid = Strategy.EvaluateAllStrategyEdits(this, true);
                }

                return isValid;
            }
        }

        /// <summary>
        /// Gets the ViewModel for this control.
        /// </summary>
        public StrategyViewModel ViewModel
        {
            get { return Application.Current != null ? Application.Current.Resources[StrategyViewModel.DataContextKey] as StrategyViewModel : null; }
            private set { Application.Current.Resources[StrategyViewModel.DataContextKey] = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Refreshes the rendering of the currently selected strategy.
        /// </summary>
        public void Refresh()
        {
            if (Strategy != null)
                Render();
        }

        /// <summary>
        /// Updates the FIX value within the input FIX fields for the currently selected strategy.
        /// </summary>
        /// <param name="fixTag">FIX tag whose value is to be updated.</param>
        /// <param name="value">New value for the FIX tag being updated.</param>
        /// <remarks>This method is used to update FIX tag values that are used within StrategyEdits using the FIX_
        /// mechanism.  Controls that are initialized using the FIX_ mechanism and parameter values within strategies
        /// are unaffected when invoking this method.</remarks>
        public void UpdateFixValue(FixField fixTag, string value)
        {
            if (InputFixValues == null)
                return;

            InputFixValues[fixTag] = value;

            if (ViewModel != null)
                ViewModel.EvaluateAffectedStrategyEdits(this, fixTag);
        }

        /// <summary>
        /// Refreshes the output FIX tags and values based on the current state of the Strategy.  Note that if any StrategyEdit is
        /// invalid, then a <see cref="ValidationException"/> is thrown.
        /// </summary>
        /// <exception cref="ValidationException">Thrown if any control/parameter value is invalid or any StrategyEdit is invalid.</exception>
        public void RefreshOutputValues()
        {
            if (Strategy == null)
                throw ThrowHelper.New<NullReferenceException>(this, ErrorMessages.NoStrategySelectedError);

            // NB Not localizable as this is a developer-level rather than end-user error
            if (!_inputValuesSet)
                throw ThrowHelper.New<InvalidOperationException>(this, ErrorMessages.UnableToInvokeMethodError,
                    "RefreshOutputValues", "The InputFixValues property must be set prior to attempting to retrieve the output FIX values");

            // Step 1: Ensure all controls have internally valid values (NB this is NOT checking the parameter validity)
            if (!ViewModel.Controls.AreAllValid)
                throw ThrowHelper.New<ValidationException>(this, ErrorMessages.OneOrMoreInvalidControlValues);

            IList<ValidationResult> validationResults;

            // Step 2: Update all the parameter values from the controls, throwing if there are any problems
            if (!Strategy.TryUpdateParameterValuesFromControls(false, out validationResults))
            {
                StringBuilder sb = new StringBuilder();

                foreach (ValidationResult result in validationResults)
                    sb.AppendFormat("{0}\n", result.ErrorText);

                string errorText = sb.ToString();

                throw ThrowHelper.New<InvalidFieldValueException>(this, sb.ToString().Substring(0, errorText.Length - 1));
            }

            // Step 3: Validate all StrategyEdits
            if (!ViewModel.EvaluateAllStrategyEdits(this))
            {
                StringBuilder sb = new StringBuilder();

                foreach (StrategyEdit_t strategyEdit in (from se in Strategy.StrategyEdits where !se.CurrentState select se))
                    sb.AppendFormat("{0}\n", strategyEdit.ErrorMessage);

                string errorText = sb.ToString();

                throw ThrowHelper.New<ValidationException>(this, sb.ToString().Substring(0, errorText.Length - 1));
            }

            FixTagValuesCollection fixTagValues = Strategy.Parameters.GetOutputValues();

            // Step 4: Add in the StrategyIdentifier and optional VersionIdentifier tags
            if (Strategy.Parent != null)
            {
                fixTagValues.Add(Strategy.Parent.StrategyIdentifierTag, Strategy.WireValue);

                if (Strategy.Parent.VersionIdentifierTag != null)
                    fixTagValues.Add((FixTag)Strategy.Parent.VersionIdentifierTag, Strategy.Version);
            }

            _log.Debug(m => m("RefreshOutputValues() yielding: {0}", fixTagValues.ToString()));

            OutputFixValues = fixTagValues;
        }

        #endregion

        #region Dependency Property Change Event Handlers

        private static void OnStrategyPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            (source as AtdlControl).OnStrategyPropertyChanged(e.NewValue as Strategy_t);
        }

        /// <remarks>This method does not throw exceptions as this causes issues with WPF data binding.  Instead it
        /// invokes the ExceptionOccurred event handler (if registered).</remarks>
        private void OnStrategyPropertyChanged(Strategy_t newStrategy)
        {
            try
            {
                if (newStrategy != null)
                {
                    _inputValuesSet = false;

                    if (Atdl4netConfiguration.Settings.Wpf.ResetStrategyOnAssignmentToControl)
                        newStrategy.Reset();

                    Render();

                    NotifyStrategyChanged(newStrategy.Name);
                }
            }
            catch (Exception ex)
            {
                NotifyExceptionOccurred(ex);
            }
        }

        private static void OnInputFixValuesChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
                (source as AtdlControl).OnInputFixValuesChanged();
        }

        private void OnInputFixValuesChanged()
        {
            if (Strategy == null)
                return;

            try
            {
                _inputValuesSet = true;

                FixFieldValueProvider fieldValueProvider = new FixFieldValueProvider(this, Strategy.Parameters);

                // First initialize all the control values from their initValue or initFixField...
                Strategy.LoadInitialControlValues(fieldValueProvider);

                // ... then load all the parameter values from the supplied FIX fields...
                Strategy.LoadParameterValues(fieldValueProvider, true);

                // ... then refresh the values of the controls from their parameters...
                Strategy.UpdateControlValuesFromParameters(fieldValueProvider);

                // ... and finally update all the state rules
                Strategy.RunAllStateRules();

                if (ViewModel !=null)
                    ViewModel.RefreshViewState();
            }
            catch (Exception ex)
            {
                NotifyExceptionOccurred(ex);
            }
        }

        private static void OnDataEntryModeChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            (source as AtdlControl).OnDataEntryModeChanged();
        }

        private void OnDataEntryModeChanged()
        {
            if (ViewModel != null)
                ViewModel.UpdateDataEntryMode(DataEntryMode);
        }

        #endregion

        #region General Private Methods

        private void NotifyStrategyChanged(string strategyName)
        {
            EventHandler<StrategyChangedEventArgs> strategyChanged = StrategyChanged;

            if (strategyChanged != null)
                strategyChanged(this, new StrategyChangedEventArgs(strategyName));
        }

        private void NotifyPropertyChanged(string name)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;

            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void Render()
        {
            WpfComboBoxSizer sizer = Application.Current.Resources[StrategyViewModel.ComboBoxSizerKey] as WpfComboBoxSizer;

            sizer.Clear();

            CreateViewModel();

            try
            {
                ViewModel.BeginRender();

                StringBuilder sb = new StringBuilder();

                XmlWriterSettings settings = new XmlWriterSettings { ConformanceLevel = ConformanceLevel.Fragment };

                using (XmlWriter writer = XmlWriter.Create(sb, settings))
                {
                    WpfStrategyPanelRenderer.Render(Strategy, writer, sizer);
                }

                _xaml = sb.ToString();

                if (!IsRenderingDisabled)
                {
                    try
                    {
                        controlRoot.Children.Clear();

                        UIElement e = (UIElement)XamlReader.Parse(_xaml);

                        controlRoot.Children.Add(e);
                        //using (StreamWriter writer = File.CreateText(Path.Combine(Path.GetTempPath(), "atdl4net_xaml.xml")))
                        //    writer.Write(sb.ToString());
                    }
                    catch (XamlParseException ex)
                    {
                        _log.ErrorFormat("XamlParseException thrown; details: {0}", ex.Message);

                        using (StreamWriter writer = File.CreateText(Path.Combine(Path.GetTempPath(), "atdl4net_xaml.xml")))
                            writer.Write(sb.ToString());

                        throw;
                    }
                }
            }
            finally
            {
                ViewModel.EndRender();
            }
        }

        private void CreateViewModel()
        {
            StrategyViewModel previousViewModel = ViewModel;

            if (previousViewModel != null)
                previousViewModel.Controls.ValidationStateChanged -= new EventHandler<ValidationStateChangedEventArgs>(ControlsValidationStateChanged);

            StrategyViewModel newViewModel = new StrategyViewModel(Strategy, this);

            Application.Current.Resources[StrategyViewModel.DataContextKey] = newViewModel;

            newViewModel.Controls.ValidationStateChanged += new EventHandler<ValidationStateChangedEventArgs>(ControlsValidationStateChanged);
        }

        private void ControlsValidationStateChanged(object sender, ValidationStateChangedEventArgs e)
        {
            NotifyValidationStateChanged(e);
        }

        private void NotifyValidationStateChanged(ValidationStateChangedEventArgs e)
        {
            _log.Debug(m => m("AtdlControl notifying event listeners that validation state for control {0} is now {1}", e.ControlId, e.IsValid));

            EventHandler<ValidationStateChangedEventArgs> validationStateChanged = ValidationStateChanged;

            if (validationStateChanged != null)
                validationStateChanged(this, e);
        }

        private void NotifyExceptionOccurred(Exception ex)
        {
            EventHandler<UnhandledExceptionEventArgs> exceptionOccurred = ExceptionOccurred;

            if (exceptionOccurred != null)
                exceptionOccurred(this, new UnhandledExceptionEventArgs(ex, false));
        }

        #endregion
    }
}




