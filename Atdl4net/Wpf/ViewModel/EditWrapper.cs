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

using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Model.Elements;
using Atdl4net.Resources;
using Atdl4net.Utility;
using System;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Wpf.ViewModel
{
    public class EditWrapper : INotifyStateChanged, IBindable<ViewModelControlCollection>
    {
        private IEdit_t<Control_t> _underlyingEdit;
        private ViewModelEditCollection _edits;

        public EditWrapper(IEdit_t<Control_t> underlyingEdit)
        {
            _underlyingEdit = underlyingEdit;

            _edits = new ViewModelEditCollection(underlyingEdit.Edits);

            _edits.StateChanged += new EventHandler<StateChangedEventArgs>(OnEditsStateChanged);
        }

        public bool CurrentState { get { return _underlyingEdit.CurrentState; } }

        #region Event handlers

        private void OnFieldValueChanged(object sender, ValueChangedEventArgs e)
        {
            bool previousState = _underlyingEdit.CurrentState;

            _underlyingEdit.Evaluate();

            NotifyStateChange(previousState, _underlyingEdit.CurrentState);
        }

        private void OnField2ValueChanged(object sender, ValueChangedEventArgs e)
        {
            bool previousState = _underlyingEdit.CurrentState;

            _underlyingEdit.Evaluate();

            NotifyStateChange(previousState, _underlyingEdit.CurrentState);
        }

        private void OnEditsStateChanged(object sender, StateChangedEventArgs e)
        {
            bool previousState = _underlyingEdit.CurrentState;

            _underlyingEdit.Evaluate();

            NotifyStateChange(previousState, _underlyingEdit.CurrentState);
        }

        #endregion Event handlers

        private void NotifyStateChange(bool oldState, bool newState)
        {
            if (oldState != newState)
            {
                EventHandler<StateChangedEventArgs> stateChanged = StateChanged;

                if (stateChanged != null)
                    stateChanged(this, new StateChangedEventArgs() { NewState = newState, OldState = oldState });
            }
        }

        #region IBindable<ViewControlCollection> Members

        // TODO: Provide a means to unbind (probably through dispose)
        void IBindable<ViewModelControlCollection>.Bind(ViewModelControlCollection target)
        {
            if (_edits.Count > 0)
            {
                (_edits as IBindable<ViewModelControlCollection>).Bind(target);
            }
            else
            {
                if (!string.IsNullOrEmpty(_underlyingEdit.Field))
                {
                    if (target.Contains(_underlyingEdit.Field))
                    {
                        ControlWrapper targetControl = target[_underlyingEdit.Field];

                        (targetControl as INotifyValueChanged).ValueChanged += new EventHandler<ValueChangedEventArgs>(OnFieldValueChanged);
                    }
                    else
                        throw ThrowHelper.New<ReferencedObjectNotFoundException>(this, ErrorMessages.EditRefFieldControlNotFound, _underlyingEdit.Field, "Field");
                }

                if (!string.IsNullOrEmpty(_underlyingEdit.Field2))
                {
                    if (target.Contains(_underlyingEdit.Field2))
                    {
                        ControlWrapper targetControl = target[_underlyingEdit.Field2];

                        (targetControl as INotifyValueChanged).ValueChanged += new EventHandler<ValueChangedEventArgs>(OnField2ValueChanged);
                    }
                    else
                        throw ThrowHelper.New<ReferencedObjectNotFoundException>(this, ErrorMessages.EditRefFieldControlNotFound, _underlyingEdit.Field2, "Field2");
                }
            }
        }

        #endregion IBindable<Strategy_t> Members

        #region INotifyStateChanged Members

        public event EventHandler<StateChangedEventArgs> StateChanged;

        #endregion INotifyStateChanged Members
    }
}
