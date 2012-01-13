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
using Atdl4net.Model.Collections;
using Atdl4net.Model.Elements;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Notification;
using Atdl4net.Utility;

namespace Atdl4net.Wpf.ViewModel
{
    // TODO: Set up CollectionBase<T> for classes that override Add etc.
    // TODO: Make the collection implement IDisposable so you can clean up eventhandlers etc.
    // TODO: Write about threading model in the documentation
    public class ViewModelEditCollection : Collection<EditViewModel>, INotifyStateChanged, IBindable<ViewModelControlCollection>
    {
        public ViewModelEditCollection(EditEvaluatingCollection<Control_t> underlyingCollection)
        {
            foreach (IEdit<Control_t> item in underlyingCollection)
            {
                EditViewModel editViewModel = new EditViewModel(item);

                editViewModel.StateChanged  += new EventHandler<StateChangedEventArgs>(OnMemberStateChanged);

                Add(editViewModel);
            }
        }

        private void OnMemberStateChanged(object sender, StateChangedEventArgs e)
        {
            NotifyStateChange(e.OldState, e.NewState);
        }

        private void NotifyStateChange(bool oldState, bool newState)
        {
            if (oldState != newState)
            {
                EventHandler<StateChangedEventArgs> stateChanged = StateChanged;

                if (stateChanged != null)
                    stateChanged(this, new StateChangedEventArgs(newState, oldState));
            }
        }
        
        #region INotifyStateChanged Members

        public event EventHandler<StateChangedEventArgs> StateChanged;

        #endregion

        #region IBindable<ViewControlCollection> Members

        void IBindable<ViewModelControlCollection>.Bind(ViewModelControlCollection target)
        {
            foreach (IBindable<ViewModelControlCollection> item in Items)
                item.Bind(target);
        }

        #endregion IBindable<ViewControlCollection> Members
    }
}
