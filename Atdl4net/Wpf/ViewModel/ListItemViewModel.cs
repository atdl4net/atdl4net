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

using Atdl4net.Model.Elements;
using System.ComponentModel;

namespace Atdl4net.Wpf.ViewModel
{
    /// <summary>
    /// View model class for <see cref="ListItem_t"/>, part of the View Model for Atdl4net.
    /// </summary>
    public class ListItemViewModel : INotifyPropertyChanged
    {
        private readonly ListItem_t _underlyingListItem;
        private readonly ViewModelListItemCollection _owningCollection;

        /// <summary>
        /// Initializes a new instance of <see cref="ListItemViewModel"/>, specifying the ViewModelListItemCollection 
        /// that this ListItemViewModel belongs to and the underlying <see cref="ListItem_t"/>.
        /// </summary>
        /// <param name="owningCollection">Collection of ListItemViewModels, which corresponds to the set of ListItems
        /// for a control.</param>
        /// <param name="listItem">ListItem_t that this ListItemViewModel is responsible for.</param>
        public ListItemViewModel(ViewModelListItemCollection owningCollection, ListItem_t listItem)
        {
            _owningCollection = owningCollection;
            _underlyingListItem = listItem;
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raised whenever a property of interest's state changes (specifically in this case the <see cref="IsSelected"/>
        /// property.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        /// <summary>
        /// Gets a unique identifier for this ListItem to support automated testing.
        /// </summary>
        public string AutomationId { get { return string.Format("{0}:{1}", _owningCollection.Id, EnumId); } }

        /// <summary>
        /// Gets/sets the selection state (true/false) of the ListItem that this ListItemViewModel is responsible for.
        /// </summary>
        public bool IsSelected
        {
            get { return _owningCollection.GetValue(EnumId); }

            set
            {
                if (_owningCollection.GetValue(EnumId) != value)
                {
                    _owningCollection.SetValue(EnumId, value);

                    NotifyPropertyChanged("IsSelected");
                }
            }
        }

        /// <summary>
        /// Gets the UiRep for this ListItemViewModel's underlying ListItem.
        /// </summary>
        public string UiRep { get { return _underlyingListItem.UiRep; } }

        /// <summary>
        /// Gets the EnumID for this ListItemViewModel's underlying ListItem.
        /// </summary>
        public string EnumId { get { return _underlyingListItem.EnumId; } }

        /// <summary>
        /// Gets/sets the GroupName (Relevant for RadioButtonList_t only).
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Indicates whether the control that this list item belongs to has a mandatory parameter.
        /// </summary>
        public bool IsRequiredParameter { get { return _owningCollection.IsRequiredParameter; } }

        /// <summary>
        /// Refreshes the user interface state of this list item.
        /// </summary>
        public void RefreshState()
        {
            NotifyPropertyChanged("IsSelected");
        }

        /// <summary>
        /// Provides a string representation of this ListItemViewModel, used for test automation.
        /// </summary>
        /// <returns>Text (i.e., UiRep) for this list item.</returns>
        public override string ToString()
        {
            return UiRep;
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;

            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
