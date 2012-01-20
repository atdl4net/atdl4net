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
using System.Text;
using Atdl4net.Model.Controls;
using Atdl4net.Model.Controls.Support;
using Atdl4net.Model.Elements;

namespace Atdl4net.Wpf.ViewModel
{
    /// <summary>
    /// Collection of <see cref="ListItemViewModel"/>s that correspond to a set of <see cref="ListItem_t"/>s for a given 
    /// control.  This is part of the View Model for Atdl4net.
    /// </summary>
    public class ViewModelListItemCollection : KeyedCollection<string, ListItemViewModel>
    {
        private readonly bool  _controlIsRadioButtonList;
        private readonly ListControlViewModel _owningControlViewModel;

        private ViewModelListItemCollection(ListControlViewModel controlViewModel, bool controlIsRadioButtonList)
        {
            _owningControlViewModel = controlViewModel;
            _controlIsRadioButtonList = controlIsRadioButtonList;
        }

        /// <summary>
        /// Factory method for creating new ViewModelListItemCollection instances, based on the supplied <see cref="ListControlViewModel"/>.
        /// </summary>
        /// <param name="controlViewModel">ListControlViewModel that represents the list-based control that contains the set
        /// of <see cref="ListItem_t"/>s that this ViewModelListItemCollection relates to.</param>
        /// <returns>A new ViewModelListItemCollection instance.  May be an empty collection.</returns>
        public static ViewModelListItemCollection Create(ListControlViewModel controlViewModel)
        {
            bool controlIsRadioButtonList = controlViewModel.UnderlyingControl is RadioButtonList_t;

            ViewModelListItemCollection collection = new ViewModelListItemCollection(controlViewModel, controlIsRadioButtonList);

            // Make a unique group name for use with RadioButtonLists to ensure individual radio buttons are mutually exclusive.
            string groupName = Guid.NewGuid().ToString();

            foreach (ListItem_t item in (controlViewModel.UnderlyingControl as ListControlBase).ListItems)
            {
                ListItemViewModel listItem = new ListItemViewModel(collection, item);

                if (controlIsRadioButtonList)
                    listItem.GroupName = groupName;

                collection.Add(listItem);
            }

            return collection;
        }

        /// <summary>
        /// Indicates whether the control that this collection of list items belongs to has a mandatory parameter.
        /// </summary>
        public bool IsRequiredParameter { get { return _owningControlViewModel.IsRequiredParameter; } }

        /// <summary>
        /// Gets the ID of the control that this set of ListItems belong to.
        /// </summary>
        public string Id { get { return _owningControlViewModel.Id; } }

        /// <summary>
        /// Attempts to get the EnumID for the supplied UiRep.
        /// </summary>
        /// <param name="uiRep">UiRep to search for.</param>
        /// <param name="enumId">EnumID that is returned if a valid match is found.</param>
        /// <returns>True if the supplied UiRep matched one of the ListItems in this collection; false otherwise.</returns>
        public bool TryGetEnumIdByUiRep(string uiRep, out string enumId)
        {
            enumId = null;

            foreach (ListItemViewModel listItem in this)
            {
                if (listItem.UiRep == uiRep)
                {
                    enumId = listItem.EnumId;

                    break;
                }
            }

            return enumId != null;
        }

        /// <summary>
        /// Gets the EnumID from the supplied <see cref="ListItemViewModel"/>; this acts as the key into this collection.
        /// </summary>
        /// <param name="item">ListItemViewModel to retrieve the key from.</param>
        /// <returns>EnumID for the supplied ListItemViewModel.</returns>
        protected override string GetKeyForItem(ListItemViewModel item)
        {
            return item.EnumId;
        }

        /// <summary>
        /// Gets the (zero-based) index of the first EnumID that has a value of true.
        /// </summary>
        /// <returns>The index of the first EnumID if any enumerated values are set; -1 otherwise.</returns>
        public int GetFirstSelectedEnumIdIndex()
        {
            if (_owningControlViewModel.UiValue == null)
                return -1;

            return (_owningControlViewModel.UiValue as EnumState).GetFirstSelectedEnumIdIndex();
        }

        /// <summary>
        /// Gets the (zero-based) index of the supplied EnumID.
        /// </summary>
        /// <returns>The index of the supplied EnumID if that matches a valid enumerated value identifier; -1 otherwise.</returns>
        public int GetIndexOfEnumId(string enumId)
        {
            if (_owningControlViewModel.UiValue == null)
                return -1;

            return (_owningControlViewModel.UiValue as EnumState).GetIndexOfEnumId(enumId);
        }

        /// <summary>
        /// Gets the current state (true/false) of the ListItem specified by the supplied EnumID.
        /// </summary>
        /// <param name="enumId">EnumID to get the state for.</param>
        /// <returns>State of the ListItem for the given EnumID.</returns>
        public bool GetValue(string enumId)
        {
            if (_owningControlViewModel.UiValue == null)
                return false;
            
            return (_owningControlViewModel.UiValue as EnumState)[enumId];
        }


        /// <summary>
        /// Sets the state (true/false) of the ListItem specified by the supplied EnumID.
        /// </summary>
        /// <param name="enumId">EnumID to set the state for.</param>
        /// <param name="value">New state value for the specified ListItem.</param>
        public void SetValue(string enumId, bool value)
        {
            EnumState state = _owningControlViewModel.UiValue as EnumState;

            // If this is a pseudo-radio button group, and a radio button has been selected (value = true),
            // then all other radio buttons must be de-selected.
            if (_controlIsRadioButtonList && value)
            {
                state.ClearAll();

                // Loop round notifying each element that it is now de-selected
                foreach (ListItemViewModel item in this.Items)
                    if (item.EnumId != enumId)
                        item.IsSelected = false;
            }

            state[enumId] = value;

            _owningControlViewModel.UiValue = state;
        }

        /// <summary>
        /// Refreshes the user interface state of each list item within this collection.
        /// </summary>
        public void RefreshState()
        {
            foreach (ListItemViewModel listItem in this)
                listItem.RefreshState();
        }

        /// <summary>
        /// Gets a string representation of this <see cref="ViewModelListItemCollection"/>, primarily for debugging purposes.
        /// </summary>
        /// <returns>String representation of this instance.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (ListItemViewModel listItem in this.Items)
                sb.AppendFormat("[{0}, {1}, {2}]", listItem.EnumId, listItem.UiRep, listItem.IsSelected);

            return sb.ToString();
        }
    }
}
