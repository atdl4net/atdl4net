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
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;
using Atdl4net.Model.Elements;
using Atdl4net.Utility;
using Atdl4net.Wpf.View;
using Atdl4net.Wpf.ViewModel;
using Common.Logging;

namespace Atdl4net.Wpf
{
    public partial class AtdlControl : UserControl, INotifyPropertyChanged
    {
        private static readonly ILog _log = LogManager.GetLogger("AtdlControl");

        public static readonly DependencyProperty DataEntryModeProperty =
            DependencyProperty.Register("DataEntryMode", typeof(DataEntryMode), typeof(AtdlControl), new FrameworkPropertyMetadata(DataEntryMode.Create));
        public static readonly DependencyProperty IsRenderingDisabledProperty =
            DependencyProperty.Register("IsRenderingDisabled", typeof(bool), typeof(AtdlControl), new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty StrategyProperty =
            DependencyProperty.Register("Strategy", typeof(Strategy_t), typeof(AtdlControl), new FrameworkPropertyMetadata(OnStrategyPropertyChanged));

        public const string ComboBoxSizerKey = "atdl4netComboBoxSizerKey";
        public const string DataContextKey = "atdl4netViewModelKey";

        private string _xaml;

        public AtdlControl()
        {
            InitializeComponent();

            Application.Current.Resources[ComboBoxSizerKey] = new WpfComboBoxSizer() { ExampleComboBox = new ComboBox(), InitialComboWidth = 28 };
        }

        public event EventHandler<UnhandledExceptionEventArgs> ExceptionOccurred;
        public event PropertyChangedEventHandler PropertyChanged;

        public UIElementCollection Children
        {
            get { return controlRoot.Children; }
        }

        public string CurrentXaml
        {
            get { return _xaml; }
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
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
            WpfComboBoxSizer sizer = Application.Current.Resources[ComboBoxSizerKey] as WpfComboBoxSizer;

            sizer.Clear();

            StrategyViewModel viewModel = new StrategyViewModel(Strategy, DataEntryMode);

            Application.Current.Resources[DataContextKey] = viewModel;

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
                }
                catch (XamlParseException)
                {
                    using (StreamWriter writer = File.CreateText(Path.Combine(Path.GetTempPath(), "atdl4net_xaml.xml")))
                        writer.Write(sb.ToString());

                    throw;
                }
            }
        }
    }
}




