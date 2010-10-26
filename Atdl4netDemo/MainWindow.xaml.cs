using Atdl4net.Model.Collections;
using Atdl4net.Model.Elements;
using Atdl4net.Providers;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

// NOTE: This app deliberately does not use data binding, so all the connections are explicit and visible.
// Limited error checking is performed to keep the code short.  

namespace Atdl4netDemo
{
    /// <summary>
    /// </summary>
    public partial class MainWindow : Window
    {
        private Strategy_t _currentStrategy;
        private StrategiesReaderProvider _strategyProvider = new StrategiesReaderProvider();

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event handler for the 'Load' button to load a new FIXatdl file.
        /// </summary>
        private void loadFileButtonClick(object sender, RoutedEventArgs e)
        {
            FileProviderContext context = GetContext();

            if (context != null)
            {
                // Call simple helper method (immediately below) to load the file.
                if (LoadStrategy(context))
                {
                    // We need at least one strategy to work with...
                    if (_strategyProvider.AllStrategies.Count > 0)
                    {
                        // As we have some strategies, set up the 'Select strategy' drop-down to offer
                        // up all strategies (this is an obvious candidate for data binding).
                        strategySelectionComboBox.DisplayMemberPath = "StrategyName";

                        strategySelectionComboBox.ItemsSource = _strategyProvider.AllStrategies;

                        strategySelectionComboBox.IsEnabled = true;

                        MessageBox.Show(string.Format("{0} strategies available - choose a strategy of interest from the 'Select strategy' drop-down.", 
                            _strategyProvider.AllStrategies.Count));
                    }
                    else
                    {
                        // No strategies found - better tell the user.
                        strategySelectionComboBox.IsEnabled = false;

                        MessageBox.Show("Sorry - no strategies found in the selected file.");
                    }
                }
            }
        }

        /// <summary>
        /// Simple helper method to load a FIXatdl strategy file into the provider.
        /// </summary>
        /// <remarks>Catching System.Exception isn't normally good form, but this saves working out
        /// which exceptions to handle in this simple demo.  Comment applies below too.</remarks>
        private bool LoadStrategy(FileProviderContext context)
        {
            try
            {
                _strategyProvider.Load(context);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Event handler for the 'Select strategy' drop-down selection changed.
        /// </summary>
        private void strategySelectionComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // We need a strategy to work with; return if none selected.
            if (e.AddedItems.Count == 0)
                return;

            // Get the information container class back from the drop-down.
            StrategyInfo info = (StrategyInfo)e.AddedItems[0];

            // Use the key (unique value across all strategies for all strategy providers) to retrieve
            // the selected strategy.
            _currentStrategy = _strategyProvider[info.StrategyKey];

            // Load the default values into the strategy (this may be automated in the future).
            _currentStrategy.Controls.LoadDefaults();

            // Tell the AtdlControl to render this strategy.
            atdlControl.Strategy = _currentStrategy;
        }

        /// <summary>
        /// Helper method to get a FileProviderContext (a simple container class with the provider id and
        /// strategy file (using a FileInfo object) in it).  Gets the context for the file specified
        /// in the file path text box.
        /// </summary>
        private FileProviderContext GetContext()
        {
            try
            {
                // Trim off double-quotes in case path got pasted from Explorer
                string filePath = filePathTextBox.Text.TrimStart(new char[] { '"' }).TrimEnd(new char[] { '"' });

                FileInfo file = new FileInfo(filePath);

                return new FileProviderContext(file, file.Name);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);

                return null;
            }
        }

        /// <summary>
        /// Event handler for the little square button to load the FIX message.
        /// </summary>
        private void loadFixButtonClick(object sender, RoutedEventArgs e)
        {
            if (_currentStrategy == null)
            {
                MessageBox.Show("Please select a strategy first.");

                return;
            }

            try
            {
                // Replace all vertical bars with SOH characters (easier to type)
                string fixMessage = fixMessageTextBox.Text.Replace('|', '\x01');

                // Load the selected strategy with the contents of the message
                _currentStrategy.InputValues = new FixTagValuesCollection(fixMessage);

                // Refresh the AtdlControl's Strategy property
                atdlControl.Strategy = null;
                atdlControl.Strategy = _currentStrategy;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Event handler for the 'Get FIX' button.
        /// </summary>
        private void getFixButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the current output values from the strategy
                FixTagValuesCollection output = _currentStrategy.GetOutputValues();

                // Replace SOH with '|' for easier viewing
                outputFixMessage.Text = output.ToFix().Replace('\x01', '|');
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
