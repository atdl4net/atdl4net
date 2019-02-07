#region Atdl4net Sample Code - License and Use
//
//   This sample code is provided as part of Atdl4net, with the intention of making it easier to get started.
//
//   Whilst Atdl4net is itself made available under either a commercial or an open-source (LGPL) license, the
//   samples provided with Atdl4net are made available for use freely by anyone that obtains a copy of
//   Atdl4net, without restriction.
//
//   For the avoidance of doubt, you are at liberty to remove this statement from any sample code that you
//   adapt for your use, but in any case the following statement still applies:
//
//   The samples for Atdl4net are distributed in the hope that they will be useful, but WITHOUT ANY WARRANTY; 
//   without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
//
#endregion

using System;
using System.Linq;
using System.Windows;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.ExampleApplication.Events;
using Atdl4net.ExampleApplication.Models;
using Atdl4net.Fix;
using Atdl4net.Notification;

namespace Atdl4net.ExampleApplication.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // Uncomment the following line to enable WPF tracing
            // PresentationTraceSources.Refresh();

            InitializeComponent();

            AtdlControl1.ExceptionOccurred += new EventHandler<UnhandledExceptionEventArgs>(AtdlControl1_ExceptionOccurred);
            AtdlControl1.ValidationStateChanged += new EventHandler<ValidationStateChangedEventArgs>(ValidityProcessor.OnValidationStateChanged);

            // The following events provide a mechanism to call methods on the AtdlControl from the ViewModel.  Although
            // this isn't ideal in terms of the MVVM separation, this is a demo app after all...
            ViewModel.UpdateFixValueRequested += new EventHandler<UpdateFixValueRequestEventArgs>(ViewModel_UpdateFixValueRequested);
            ViewModel.OutputValuesRequested += new EventHandler<OutputValuesRequestEventArgs>(ViewModel_OutputValuesRequested);
            ViewModel.ValidationStateRefreshRequested += new EventHandler<EventArgs>(ViewModel_ValidationStateRefreshRequested);
        }

        private MainViewModel ViewModel { get { return Resources["MainViewModelResource"] as MainViewModel; } }

        private ValidityProcessor ValidityProcessor { get { return Resources["ValidityProcessorResource"] as ValidityProcessor; } }

        private void ViewModel_OutputValuesRequested(object sender, OutputValuesRequestEventArgs e)
        {
            try
            {
                AtdlControl1.RefreshOutputValues();
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(this, ex.Message);
            }
            catch (InternalErrorException ex)
            {
                MessageBox.Show(this, string.Format("An exception was thrown with the message {0}\n\n" +
                    "Please ensure that you initialize the input FIX values (click the Initialize button)\nbefore requesting the output FIX message.", ex.Message));
            }
            catch (Atdl4netException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ViewModel_UpdateFixValueRequested(object sender, UpdateFixValueRequestEventArgs e)
        {
            AtdlControl1.UpdateFixValue((FixField)e.FixTag, e.Value);
        }

        private void ViewModel_ValidationStateRefreshRequested(object sender, EventArgs e)
        {
            ValidityProcessor.RefreshControlIsValid(AtdlControl1);
        }

        private void AtdlControl1_ExceptionOccurred(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(this, string.Format("An exception was thrown in a DependencyProperty event handler; details:\n\n{0}",
                (e.ExceptionObject as Exception).Message));
        }
    }
}
