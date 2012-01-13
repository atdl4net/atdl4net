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
using Atdl4net.Diagnostics;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Fix;
using Atdl4net.Model.Collections;
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
    public partial class AtdlControl : UserControl, IInitialValueProvider, INotifyPropertyChanged
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Wpf");

        private string _xaml;
        private FixFieldValueProvider _inputValues;

        public static readonly DependencyProperty DataEntryModeProperty =
            DependencyProperty.Register("DataEntryMode", typeof(DataEntryMode), typeof(AtdlControl), new FrameworkPropertyMetadata(DataEntryMode.Create));

        public static readonly DependencyProperty IsRenderingDisabledProperty =
            DependencyProperty.Register("IsRenderingDisabled", typeof(bool), typeof(AtdlControl), new FrameworkPropertyMetadata(false));

        public static readonly DependencyProperty StrategyProperty =
            DependencyProperty.Register("Strategy", typeof(Strategy_t), typeof(AtdlControl), new FrameworkPropertyMetadata(OnStrategyPropertyChanged));

        /// <summary>
        /// Initializes a new <see cref="AtdlControl"/>.
        /// </summary>
        public AtdlControl()
        {
            InitializeComponent();

            Application.Current.Resources[StrategyViewModel.ComboBoxSizerKey] = new WpfComboBoxSizer() { ExampleComboBox = new ComboBox(), InitialComboWidth = 28 };
        }

        public event EventHandler<UnhandledExceptionEventArgs> ExceptionOccurred;

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<ValidationStateChangedEventArgs> ValidationStateChanged;

        public UIElementCollection Children
        {
            get { return controlRoot.Children; }
        }

        public string CurrentXaml
        {
            get { return _xaml; }
        }

                /// <summary>
        /// Gets/sets the input FIX values to be used when populating this Strategy, using the FIX_ mechanism.
        /// </summary>
        public FixTagValuesCollection InitialValues
        {
            get { return _inputValues != null ? _inputValues.FixValues : null; }

            set
            {
                if (Strategy == null)
                    throw new NullReferenceException(ErrorMessages.NoStrategySelectedError);

                ParameterCollection parameters = Strategy.Parameters;

                _inputValues = new FixFieldValueProvider(this, parameters);

                parameters.InitializeValues(value);

                Strategy.Controls.UpdateValuesFromParameters(parameters, _inputValues);

                StrategyViewModel viewModel = Application.Current.Resources[StrategyViewModel.DataContextKey] as StrategyViewModel;

                if (viewModel != null)
                    viewModel.Controls.RefreshState();
            }
        }

        public string CustomControlRendererAssembly
        {
            get { return WpfStrategyPanelRenderer.CustomControlRenderer; }
            set { WpfStrategyPanelRenderer.CustomControlRenderer = value; }
        }

        public DataEntryMode DataEntryMode
        {
            get { return (DataEntryMode)GetValue(DataEntryModeProperty); }
            set { SetValue(DataEntryModeProperty, value); }
        }

        public bool IsRenderingDisabled
        {
            get { return (bool)GetValue(IsRenderingDisabledProperty); }
            set { SetValue(IsRenderingDisabledProperty, value); }
        }

        public Strategy_t Strategy
        {
            get { return (Strategy_t)GetValue(StrategyProperty); }
            set { SetValue(StrategyProperty, value); }
        }


        public bool IsValid
        {
            get
            {
                bool isValid = false;

                if (Strategy != null)
                {
                    IList<ValidationResult> validationResults;

                    if (Strategy.Controls.TryUpdateParameterValues(Strategy.Parameters, true, out validationResults))
                        isValid = Strategy.StrategyEdits.ValidateAll(new FixFieldValueProvider(this, Strategy.Parameters), true);
                }

                return isValid;
            }
        }

        /// <summary>
        /// Refreshes the rendering of the currently selected strategy.
        /// </summary>
        public void Refresh()
        {
            if (Strategy != null)
                Render();
        }

        /// <summary>
        /// Gets the output FIX tags and values based on the current state of the Strategy.  Note that if any StrategyEdit is
        /// invalid, then a <see cref="ValidationException"/> is thrown.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ValidationException">Thrown if any control/parameter value is invalid or any StrategyEdit is invalid.</exception>
        public FixTagValuesCollection GetOutputValues()
        {
            if (Strategy == null)
                throw new NullReferenceException(ErrorMessages.NoStrategySelectedError);

            IList<ValidationResult> validationResults;

            if (!Strategy.Controls.TryUpdateParameterValues(Strategy.Parameters, false, out validationResults))
            {
                StringBuilder sb = new StringBuilder();

                foreach (ValidationResult result in validationResults)
                    sb.AppendFormat("{0}\n", result.ErrorText);

                string errorText = sb.ToString();

                throw ThrowHelper.New<InvalidFieldValueException>(this, sb.ToString().Substring(0, errorText.Length - 1));
            }

            if (!Strategy.StrategyEdits.ValidateAll(_inputValues == null ? FixFieldValueProvider.Empty : _inputValues, false))
            {
                StringBuilder sb = new StringBuilder();

                foreach (StrategyEdit_t strategyEdit in (from se in Strategy.StrategyEdits where !se.CurrentState select se))
                    sb.AppendFormat("{0}\n", strategyEdit.ErrorMessage);

                string errorText = sb.ToString();

                throw ThrowHelper.New<ValidationException>(this, sb.ToString().Substring(0, errorText.Length - 1));
            }

            FixTagValuesCollection fixTagValues = Strategy.Parameters.GetOutputValues();

            if (Strategy.Parent != null)
            {
                fixTagValues.Add(Strategy.Parent.StrategyIdentifierTag, Strategy.WireValue);

                if (Strategy.Parent.VersionIdentifierTag != null)
                    fixTagValues.Add((FixTag)Strategy.Parent.VersionIdentifierTag, Strategy.Version);
            }

            _log.Debug(m => m("Strategy_t.GetOutputValues() returning: {0}", fixTagValues.ToString()));

            return fixTagValues;
        }


        /// <remarks>This method does not throw exceptions as this causes issues with WPF data binding.  Instead it
        /// invokes the ExceptionOccurred event handler (if registered).</remarks>
        private static void OnStrategyPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if (source != null && source is AtdlControl)
            {
                AtdlControl targetControl = (AtdlControl)source;

                try
                {
                    if (e.NewValue != null)
                        targetControl.Render();
                }
                catch (Exception ex)
                {
                    if (targetControl.ExceptionOccurred != null)
                        targetControl.ExceptionOccurred(source, new UnhandledExceptionEventArgs(ex, false));
                }
            }
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

        private void CreateViewModel()
        {
            StrategyViewModel previousViewModel = Application.Current.Resources[StrategyViewModel.DataContextKey] as StrategyViewModel;

            if (previousViewModel != null)
                previousViewModel.Controls.ValidationStateChanged -= new EventHandler<ValidationStateChangedEventArgs>(ControlsValidationStateChanged);

            StrategyViewModel newViewModel = new StrategyViewModel(Strategy, this, DataEntryMode);

            Application.Current.Resources[StrategyViewModel.DataContextKey] = newViewModel;

            newViewModel.Controls.ValidationStateChanged += new EventHandler<ValidationStateChangedEventArgs>(ControlsValidationStateChanged);
        }

        void ControlsValidationStateChanged(object sender, ValidationStateChangedEventArgs e)
        {
            NotifyValidationStateChanged(e);
        }

        private void NotifyValidationStateChanged(ValidationStateChangedEventArgs e)
        {
            EventHandler<ValidationStateChangedEventArgs> validationStateChanged = ValidationStateChanged;

            if (validationStateChanged != null)
                validationStateChanged(this, e);
        }
    }
}




