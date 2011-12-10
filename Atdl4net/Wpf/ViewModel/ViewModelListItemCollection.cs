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
using System.Text;
using Atdl4net.Model.Controls;
using Atdl4net.Model.Controls.Support;
using Atdl4net.Model.Elements;

namespace Atdl4net.Wpf.ViewModel
{
    /// <summary>
    /// Collection of <see cref="ListItemWrapper"/>s that correspond to a set of <see cref="ListItem_t"/>s for a given 
    /// control.  This is part of the View Model for Atdl4net.
    /// </summary>
    public class ViewModelListItemCollection : KeyedCollection<string, ListItemWrapper>
    {
        private readonly bool  _controlIsRadioButtonList;
        private readonly ListControlWrapper _owningControlWrapper;

        private ViewModelListItemCollection(ListControlWrapper controlWrapper, bool controlIsRadioButtonList)
        {
            _owningControlWrapper = controlWrapper;
            _controlIsRadioButtonList = controlIsRadioButtonList;
        }

        /// <summary>
        /// Factory method for creating new ViewModelListItemCollection instances, based on the supplied <see cref="ListControlWrapper"/>.
        /// </summary>
        /// <param name="controlWrapper">ListControlWrapper that represents the list-based control that contains the set
        /// of <see cref="ListItem_t"/>s that this ViewModelListItemCollection relates to.</param>
        /// <returns>A new ViewModelListItemCollection instance.  May be an empty collection.</returns>
        public static ViewModelListItemCollection Create(ListControlWrapper controlWrapper)
        {
            bool controlIsRadioButtonList = controlWrapper.UnderlyingControl is RadioButtonList_t;

            ViewModelListItemCollection collection = new ViewModelListItemCollection(controlWrapper, controlIsRadioButtonList);

            // Make a unique group name for use with RadioButtonLists to ensure individual radio buttons are mutually exclusive.
            string groupName = Guid.NewGuid().ToString();

            foreach (ListItem_t item in (controlWrapper.UnderlyingControl as ListControlBase).ListItems)
            {
                ListItemWrapper wrapper = new ListItemWrapper(collection, item);

                if (controlIsRadioButtonList)
                    wrapper.GroupName = groupName;

                collection.Add(wrapper);
            }

            return collection;
        }

        /// <summary>
        /// Gets the ID of the control that this set of ListItems belong to.
        /// </summary>
        public string Id { get { return _owningControlWrapper.Id; } }

        /// <summary>
        /// Attempts to get the EnumID for the supplied UiRep.
        /// </summary>
        /// <param name="uiRep">UiRep to search for.</param>
        /// <param name="enumId">EnumID that is returned if a valid match is found.</param>
        /// <returns>True if the supplied UiRep matched one of the ListItems in this collection; false otherwise.</returns>
        public bool TryGetEnumIdByUiRep(string uiRep, out string enumId)
        {
            enumId = null;

            foreach (ListItemWrapper wrapper in this)
            {
                if (wrapper.UiRep == uiRep)
                {
                    enumId = wrapper.EnumId;

                    break;
                }
            }

            return enumId != null;
        }

        /// <summary>
        /// Gets the EnumID from the supplied <see cref="ListItemWrapper"/>; this acts as the key into this collection.
        /// </summary>
        /// <param name="item">ListItemWrapper to retrieve the key from.</param>
        /// <returns>EnumID for the supplied ListItemWrapper.</returns>
        protected override string GetKeyForItem(ListItemWrapper item)
        {
            return item.EnumId;
        }

        /// <summary>
        /// Gets the current state (true/false) of the ListItem specified by the supplied EnumID.
        /// </summary>
        /// <param name="enumId">EnumID to get the state for.</param>
        /// <returns>State of the ListItem for the given EnumID.</returns>
        public bool GetValue(string enumId)
        {
            if (_owningControlWrapper.UiValue == null)
                return false;
            
            return (_owningControlWrapper.UiValue as EnumState)[enumId];
        }


        /// <summary>
        /// Sets the state (true/false) of the ListItem specified by the supplied EnumID.
        /// </summary>
        /// <param name="enumId">EnumID to set the state for.</param>
        /// <param name="value">New state value for the specified ListItem.</param>
        public void SetValue(string enumId, bool value)
        {
            EnumState state = _owningControlWrapper.UiValue as EnumState;

            // If this is a pseudo-radio button group, and a radio button has been selected (value = true),
            // then all other radio buttons must be de-selected.
            if (_controlIsRadioButtonList && value)
            {
                state.ClearAll();

                // Loop round notifying each element that it is now de-selected
                foreach (ListItemWrapper item in this.Items)
                    if (item.EnumId != enumId)
                        item.IsSelected = false;
            }

            state[enumId] = value;

            _owningControlWrapper.UiValue = state;
        }

        /// <summary>
        /// Gets a string representation of this <see cref="ViewModelListItemCollection"/>, primarily for debugging purposes.
        /// </summary>
        /// <returns>String representation of this instance.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (ListItemWrapper wrapper in this.Items)
                sb.AppendFormat("[{0}, {1}, {2}]", wrapper.EnumId, wrapper.UiRep, wrapper.IsSelected);

            return sb.ToString();
        }
    }
}
