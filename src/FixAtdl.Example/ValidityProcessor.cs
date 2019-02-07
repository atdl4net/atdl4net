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
using System.ComponentModel;
using System.Linq;
using Atdl4net.Notification;
using Atdl4net.Wpf;

namespace Atdl4net.ExampleApplication
{
    /// <summary>
    /// Helper class that communicates the validity state of the strategy to the 'Strategy may be submitted' CheckBox.
    /// </summary>
    public class ValidityProcessor : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raised whenever the value of a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        /// <summary>
        /// Gets the validity state.
        /// </summary>
        public bool IsValid { get; private set; }

        /// <summary>
        /// Event handler for AtdlControl validation event.
        /// </summary>
        /// <param name="sender">AtdlControl that raised this event.</param>
        /// <param name="e">Event argument.</param>
        public void OnValidationStateChanged(object sender, ValidationStateChangedEventArgs e)
        {
            AtdlControl control = sender as AtdlControl;

            if (control == null)
                return;

            if (!e.IsValid)
            {
                IsValid = false;

                NotifyPropertyChanged("IsValid");
            }
            else
                RefreshControlIsValid(control);
        }

        /// <summary>
        /// Refreshes the validity state of the supplied control.
        /// </summary>
        /// <param name="source"></param>
        public void RefreshControlIsValid(AtdlControl source)
        {
            IsValid = source.IsValid;

            NotifyPropertyChanged("IsValid");
        }

        private void NotifyPropertyChanged(string name)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;

            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
