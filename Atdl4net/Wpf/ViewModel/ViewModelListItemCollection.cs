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

using Atdl4net.Model.Controls;
using Atdl4net.Model.Elements;
using Atdl4net.Model.Types;
using System.Collections.ObjectModel;
using System.Text;

namespace Atdl4net.Wpf.ViewModel
{
    public class ViewModelListItemCollection : KeyedCollection<string, ListItemWrapper>
    {
        bool _controlIsRadioButtonList;
        ListControlWrapper _owningControlWrapper;

        private ViewModelListItemCollection(ListControlWrapper controlWrapper, bool controlIsRadioButtonList)
        {
            _owningControlWrapper = controlWrapper;
            _controlIsRadioButtonList = controlIsRadioButtonList;
        }

        public static ViewModelListItemCollection Create(ListControlWrapper controlWrapper)
        {
            bool controlIsRadioButtonList = controlWrapper.UnderlyingControl is RadioButtonList_t;

            ViewModelListItemCollection collection = new ViewModelListItemCollection(controlWrapper, controlIsRadioButtonList);

            foreach (ListItem_t item in (controlWrapper.UnderlyingControl as IListControl).ListItems)
            {
                ListItemWrapper wrapper = new ListItemWrapper(collection, item);

                collection.Add(wrapper);
            }

            return collection;
        }

        protected override string GetKeyForItem(ListItemWrapper item)
        {
            return item.EnumId;
        }

        public bool GetValue(string enumId)
        {
            return (_owningControlWrapper.Value as EnumState)[enumId];
        }

        public void SetValue(string enumId, bool value)
        {
            EnumState newState = new EnumState(_owningControlWrapper.Value as EnumState);

            // If this is a pseudo-radio button group, then all other radio buttons must be de-selected.
            if (_controlIsRadioButtonList && value)
            {
                newState.ClearAll();

                foreach (ListItemWrapper item in this.Items)
                    if (item.EnumId != enumId)
                        item.IsSelected = false;
            }

            newState[enumId] = value;

            _owningControlWrapper.Value = newState;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (ListItemWrapper wrapper in this.Items)
                sb.AppendFormat("[{0}, {1}, {2}]\r\n", wrapper.EnumId, wrapper.UiRep, wrapper.IsSelected);

            return sb.ToString();
        }
    }
}
