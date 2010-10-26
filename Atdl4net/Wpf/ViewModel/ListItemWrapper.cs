#region Copyright (c) 2010, Cornerstone Technology Limited. http://atdl4net.org
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
//      License as published by the Free Software Foundation, version 3.
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
    public class ListItemWrapper : INotifyPropertyChanged
    {
        private ListItem_t _underlyingListItem;
        private ViewModelListItemCollection _owningCollection;

        public ListItemWrapper(ViewModelListItemCollection owningCollection, ListItem_t listItem)
        {
            _owningCollection = owningCollection;
            _underlyingListItem = listItem;
        }

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

        public string UiRep { get { return _underlyingListItem.UiRep; } }

        public string EnumId { get { return _underlyingListItem.EnumId; } }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;

            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return string.Format("ListItemWrapper[{0}, {1}, {2}]", EnumId, UiRep, IsSelected);
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
