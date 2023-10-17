#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
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
