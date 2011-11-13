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
using Atdl4net.Utility;
using System;

namespace Atdl4net.Wpf.ViewModel
{
    public class StateRuleWrapper : IBindable<ViewModelControlCollection>
    {
        private StateRule_t _underlyingStateRule;
        private ControlWrapper _owningControl;
        private EditWrapper _edit;

        public StateRuleWrapper(ControlWrapper owningControl, StateRule_t stateRule)
        {
            _owningControl = owningControl;
            _underlyingStateRule = stateRule;

            if (stateRule.Edit != null)
                _edit = new EditWrapper(stateRule.Edit);
            else if (stateRule.EditRef != null)
                _edit = new EditWrapper(stateRule.EditRef);
            else
                // TODO: Proper exception please.
                throw new Exception("Neither Edit nor EditRef set!");
        }

        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>From the spec: "... the action of a flow-control rule is performed when the condition it describes is true. This differs 
        /// from validation rules, where the action of “raising an error” occurs when the condition is false."
        /// 
        /// In terms of behaviours, the spec says the following:
        /// 
        /// "i.   A StateRule that changes the “enabled” property of a control to X when its condition becomes true, will implicitly cause the
        /// “enabled” property of the control to change to NOT(X) when its condition becomes false, where X is Boolean.
        /// ii.  A StateRule that changes the “visible” property of a control to X when its condition becomes true, will implicitly cause the
        /// “visible” property of the control to change to NOT(X) when its condition becomes false, where X is Boolean. 
        /// iii. A StateRule that changes the value of a control when its condition becomes true will cause no action to take place when its 
        /// condition becomes false. Provided the vale expressed in the StateRule element is not the special token “{NULL}”.
        /// iv.	 A StateRule that changes the value of a control to “{NULL}” when its condition becomes true will cause the control’s value to
        /// revert back to its previous non-{NULL} value or its initial value."
        /// </remarks>
        private void OnEditStateChanged(object sender, StateChangedEventArgs e)
        {
            RefreshState();

            if (_underlyingStateRule.Value != null)
            {
                _owningControl.Value = _underlyingStateRule.Value;
            }
        }

        public void RefreshState()
        {
            if (_underlyingStateRule.Visible != null)
                _owningControl.IsVisible = _edit.CurrentState ? (bool)_underlyingStateRule.Visible : !(bool)_underlyingStateRule.Visible;

            if (_underlyingStateRule.Enabled != null)
                _owningControl.Enabled = _edit.CurrentState ? (bool)_underlyingStateRule.Enabled : !(bool)_underlyingStateRule.Enabled;
        }

        #region IBindable<ViewControlCollection> Members

        public void Bind(ViewModelControlCollection target)
        {
            (_edit as IBindable<ViewModelControlCollection>).Bind(target);

            _edit.StateChanged += new EventHandler<StateChangedEventArgs>(OnEditStateChanged);
        }

        #endregion IBindable<ViewControlCollection> Members
    }
}
