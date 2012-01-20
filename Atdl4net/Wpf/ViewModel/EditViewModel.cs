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
using System.Linq;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Fix;
using Atdl4net.Model.Elements;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Notification;
using Atdl4net.Resources;
using Atdl4net.Utility;
using Common.Logging;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Wpf.ViewModel
{
    // TODO: Implement IDisposable
    public class EditViewModel : INotifyStateChanged, IBindable<ViewModelControlCollection>
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Wpf.ViewModel");

        private readonly IEdit<Control_t> _underlyingEdit;
        private readonly ViewModelEditCollection _edits;

        public EditViewModel(IEdit<Control_t> underlyingEdit)
        {
            _underlyingEdit = underlyingEdit;

            if (underlyingEdit.Edits.Count > 0)
            {
                _edits = new ViewModelEditCollection(underlyingEdit.Edits);

                _edits.StateChanged += new EventHandler<StateChangedEventArgs>(OnEditsStateChanged);
            }
        }

        public bool CurrentState { get { return _underlyingEdit.CurrentState; } }

        #region INotifyStateChanged Members

        public event EventHandler<StateChangedEventArgs> StateChanged;

        #endregion INotifyStateChanged Members

        #region Event handlers

        private void OnFieldValueChanged(object sender, ValueChangedEventArgs e)
        {
            _log.Debug(m => m("EditViewModel.OnFieldValueChanged invoked; value changed from '{0}' to '{1}'", e.OldValue, e.NewValue));

            bool previousState = _underlyingEdit.CurrentState;

            _underlyingEdit.Evaluate(FixFieldValueProvider.Empty);

            NotifyStateChange(e.Id, previousState, _underlyingEdit.CurrentState);
        }

        private void OnField2ValueChanged(object sender, ValueChangedEventArgs e)
        {
            _log.Debug(m => m("EditViewModel.OnField2ValueChanged invoked; value changed from '{0}' to '{1}'", e.OldValue, e.NewValue));

            bool previousState = _underlyingEdit.CurrentState;

            _underlyingEdit.Evaluate(FixFieldValueProvider.Empty);

            NotifyStateChange(e.Id, previousState, _underlyingEdit.CurrentState);
        }

        private void OnEditsStateChanged(object sender, StateChangedEventArgs e)
        {
            bool previousState = _underlyingEdit.CurrentState;

            _underlyingEdit.Evaluate(FixFieldValueProvider.Empty);

            NotifyStateChange("Edits", previousState, _underlyingEdit.CurrentState);
        }

        #endregion

        private void NotifyStateChange(string sourceOfChange, bool oldState, bool newState)
        {
            if (oldState != newState)
            {
                _log.Debug(m=>m("Edit_t state changed by {0}; old state = {1}, new state = {2}", sourceOfChange, oldState.ToString().ToLower(), newState.ToString().ToLower()));

                EventHandler<StateChangedEventArgs> stateChanged = StateChanged;

                if (stateChanged != null)
                    stateChanged(this, new StateChangedEventArgs(newState, oldState));
            }
        }

        #region IBindable<ViewControlCollection> Members

        // TODO: Provide a means to unbind (probably through dispose)
        void IBindable<ViewModelControlCollection>.Bind(ViewModelControlCollection controls)
        {
            if (_edits != null && _edits.Count > 0)
            {
                (_edits as IBindable<ViewModelControlCollection>).Bind(controls);
            }
            else
            {
                if (!string.IsNullOrEmpty(_underlyingEdit.Field))
                {
                    if (controls.Contains(_underlyingEdit.Field))
                    {
                        ControlViewModel targetControl = controls[_underlyingEdit.Field];

                        (targetControl as INotifyValueChanged).ValueChanged += new EventHandler<ValueChangedEventArgs>(OnFieldValueChanged);
                    }
                    else
                        throw ThrowHelper.New<ReferencedObjectNotFoundException>(this, ErrorMessages.EditRefFieldControlNotFound, _underlyingEdit.Field, "Field");
                }

                if (!string.IsNullOrEmpty(_underlyingEdit.Field2))
                {
                    if (controls.Contains(_underlyingEdit.Field2))
                    {
                        ControlViewModel targetControl = controls[_underlyingEdit.Field2];

                        (targetControl as INotifyValueChanged).ValueChanged += new EventHandler<ValueChangedEventArgs>(OnField2ValueChanged);
                    }
                    else
                        throw ThrowHelper.New<ReferencedObjectNotFoundException>(this, ErrorMessages.EditRefFieldControlNotFound, _underlyingEdit.Field2, "Field2");
                }
            }
        }

        #endregion IBindable<Strategy_t> Members
    }
}
