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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.ExampleApplication.Commands;
using Atdl4net.ExampleApplication.Events;
using Atdl4net.ExampleApplication.Models.Support;
using Atdl4net.Fix;
using Atdl4net.Model.Elements;
using Atdl4net.Utility;

namespace Atdl4net.ExampleApplication.Models
{
    /// <summary>
    /// View model for the example application.
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        // MVVM command support
        private ICommand _initializeCommand;
        private ICommand _updateFixCommand;
        private ICommand _getOutputValuesCommand;

        private string _selectedAlgoProvider;
        private DataEntryMode _dataEntryMode;
        private Strategy_t _selectedStrategy;
        private FixTagValuesCollection _initialFixValues;
        private FixTagValuesCollection _outputFixValues;
        private readonly ExampleStrategyProvider _exampleStrategyProvider;

        /// <summary>
        /// Raised whenever the user wants to update a single FIX field within the initial set of FIX values.
        /// </summary>
        public event EventHandler<UpdateFixValueRequestEventArgs> UpdateFixValueRequested;

        /// <summary>
        /// Raised whenever the user wants to obtain the current set of FIX output values.
        /// </summary>
        public event EventHandler<OutputValuesRequestEventArgs> OutputValuesRequested;

        /// <summary>
        /// Raised whenever the view model wants to request the view to refresh its validation state.
        /// </summary>
        public event EventHandler<EventArgs> ValidationStateRefreshRequested;

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raised to notify the view that a specific value has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        /// <summary>
        /// Initializes a new MainViewModel instance.
        /// </summary>
        public MainViewModel()
        {
            _exampleStrategyProvider = new ExampleStrategyProvider();

            // Set Create mode as the default
            _dataEntryMode = DataEntryMode.Create;
        }

        #region Command Support

        /// <summary>
        /// InitializeCommand support.
        /// </summary>
        public ICommand InitializeCommand
        {
            get
            {
                if (_initializeCommand == null)
                    _initializeCommand = new DelegateCommand(OnInitializeCommand, delegate() { return true; });

                return _initializeCommand;
            }
        }

        /// <summary>
        /// UpdateFixCommand support.
        /// </summary>
        public ICommand UpdateFixCommand
        {
            get
            {
                if (_updateFixCommand == null)
                    _updateFixCommand = new DelegateCommand(OnUpdateFixCommand, delegate() { return true; });

                return _updateFixCommand;
            }
        }

        /// <summary>
        /// GetOutputValuesCommand support.
        /// </summary>
        public ICommand GetOutputValuesCommand
        {
            get
            {
                if (_getOutputValuesCommand == null)
                    _getOutputValuesCommand = new DelegateCommand(OnGetOutputValuesCommand, delegate() { return true; });

                return _getOutputValuesCommand;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the list of unique identifiers for the strategy providers (aka brokers).
        /// </summary>
        public IList<string> AlgorithmProviders { get { return _exampleStrategyProvider.AlgorithmProviders; } }

        /// <summary>
        /// Gets/sets the selected algorithm provider.
        /// </summary>
        public string SelectedProvider
        {
            get { return _selectedAlgoProvider; }

            set
            {
                _selectedAlgoProvider = value;

                NotifyPropertyChanged("AvailableStrategies");
            }
        }

        /// <summary>
        /// Gets the set of available strategies for the selected algorithm provider.
        /// </summary>
        public IList<Strategy_t> AvailableStrategies
        {
            get
            {
                if (_selectedAlgoProvider != null)
                    return _exampleStrategyProvider.GetStrategiesByProvider(_selectedAlgoProvider);

                return null;
            }
        }

        /// <summary>
        /// Gets/sets the selected strategy.
        /// </summary>
        public Strategy_t SelectedStrategy
        {
            get { return _selectedStrategy; }

            set
            {
                _selectedStrategy = value;

                NotifyPropertyChanged("SelectedStrategy");
            }
        }

        /// <summary>
        /// Gets/sets the input values for the strategy. 
        /// </summary>
        public string InitialValuesString { get; set; }

        /// <summary>
        /// Gets the set of initial FIX fields to be used by the currently selected strategy.
        /// </summary>
        public FixTagValuesCollection InitialFixValues { get { return _initialFixValues; } }

        /// <summary>
        /// Gets/sets the tag to be used when updating a FIX value.
        /// </summary>
        public uint? UpdateFixTag { get; set; }

        /// <summary>
        /// Gets/sets the value to be used when updating a FIX value.
        /// </summary>
        public string UpdateFixValue { get; set; }

        /// <summary>
        /// Gets/sets the data entry mode for the currently selected strategy.
        /// </summary>
        public DataEntryMode DataEntryMode
        {
            get { return _dataEntryMode; }

            set
            {
                _dataEntryMode = value;

                NotifyPropertyChanged("DataEntryMode");
            }
        }

        /// <summary>
        /// Gets/sets the output FIX values for this control.
        /// </summary>
        public FixTagValuesCollection OutputFixValues
        {
            get { return _outputFixValues; }

            set
            {
                _outputFixValues = value;

                NotifyPropertyChanged("OutputFixMessage");
            }
        }

        /// <summary>
        /// Gets the output values from the currently selected strategy.
        /// </summary>
        public string OutputFixMessage { get { return _outputFixValues != null ? _outputFixValues.ToFix().Replace('\x01', '|') : string.Empty; } }

        #endregion

        private void OnInitializeCommand()
        {
            try
            {
                _initialFixValues = null;

                NotifyPropertyChanged("InitialFixValues");

                if (string.IsNullOrEmpty(InitialValuesString))
                    _initialFixValues = FixTagValuesCollection.Empty;
                else
                    _initialFixValues = new FixTagValuesCollection(InitialValuesString.Replace('|', '\x01').Trim());

                NotifyPropertyChanged("InitialFixValues");

                RequestValidationRefresh();
            }
            catch (FixParseException ex)
            {
                // Really shouldn't be throwing message boxes from the View Model, I know...
                MessageBox.Show(ex.Message);
            }
        }

        private void OnUpdateFixCommand()
        {
            if (UpdateFixTag == null)
                return;

            EventHandler<UpdateFixValueRequestEventArgs> updateFixValueRequested = UpdateFixValueRequested;

            if (updateFixValueRequested != null)
                updateFixValueRequested(this, new UpdateFixValueRequestEventArgs((uint)UpdateFixTag, UpdateFixValue));
        }

        private void OnGetOutputValuesCommand()
        {
            EventHandler<OutputValuesRequestEventArgs> outputValuesRequested = OutputValuesRequested;

            if (outputValuesRequested != null)
                outputValuesRequested(this, new OutputValuesRequestEventArgs());
        }

        private void RequestValidationRefresh()
        {
            EventHandler<EventArgs> validationStateRefreshRequested = ValidationStateRefreshRequested;

            if (validationStateRefreshRequested != null)
                validationStateRefreshRequested(this, EventArgs.Empty);
        }

        private void NotifyPropertyChanged(string name)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;

            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
