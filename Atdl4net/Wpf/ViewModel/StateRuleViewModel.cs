#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using Atdl4net.Model;
using Atdl4net.Model.Elements;
using Atdl4net.Notification;
using Atdl4net.Utility;
using Common.Logging;

namespace Atdl4net.Wpf.ViewModel
{
    public class StateRuleViewModel : IBindable<ViewModelControlCollection>
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Wpf.ViewModel");

        private bool hasRun = false;
        private object _previousValue;
        private readonly StateRule_t _underlyingStateRule;
        private readonly ControlViewModel _owningControlViewModel;
        private readonly EditViewModel _edit;

        public StateRuleViewModel(ControlViewModel owningControl, StateRule_t stateRule)
        {
            _owningControlViewModel = owningControl;
            _underlyingStateRule = stateRule;

            if (stateRule.Edit != null)
                _edit = new EditViewModel(stateRule.Edit);
            else if (stateRule.EditRef != null)
                _edit = new EditViewModel(stateRule.EditRef);
            else
                // TODO: Proper exception please.
                throw new Exception("Neither Edit nor EditRef set!");
        }

        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEditStateChanged(object sender, StateChangedEventArgs e)
        {
            _log.Debug(m => m("StateRuleViewModel.OnEditStateChanged invoked; new Edit_t state = {0}", _edit.CurrentState));

            RefreshState();
        }

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
        public void RefreshState()
        {
            // Ignore all false states until we've seen a positive state
            if (!_edit.CurrentState && !hasRun)
                return;

            hasRun = true;

            _log.Debug(m => m("Refreshing state for StateRule_t {0}", _underlyingStateRule.ToString()));

            if (_underlyingStateRule.Visible != null)
            {
                bool visible = _edit.CurrentState ? (bool)_underlyingStateRule.Visible : !(bool)_underlyingStateRule.Visible;

                _log.Debug(m => m("Updating control's visible state to {0}", visible.ToString().ToLower()));

                _owningControlViewModel.IsVisible = visible;
            }

            if (_underlyingStateRule.Enabled != null)
            {
                bool enabled = _edit.CurrentState ? (bool)_underlyingStateRule.Enabled : !(bool)_underlyingStateRule.Enabled;

                _log.Debug(m => m("Updating control's enabled state to {0}", enabled.ToString().ToLower()));

                _owningControlViewModel.Enabled = enabled;
            }

            if (_underlyingStateRule.Value != null)
            {
                if (_edit.CurrentState)
                {
                    _log.Debug(m => m("Updating control's value from '{0}' to '{1}'", _owningControlViewModel.UiValue, _underlyingStateRule.Value));

                    _previousValue = _owningControlViewModel.UiValue;
                    _owningControlViewModel.UiValue = _underlyingStateRule.Value;
                }
                else if (_underlyingStateRule.Value == Atdl.NullValue)
                {
                    _log.Debug(m => m("Returning control's value to '{1}' (from '{0}')", _previousValue, _owningControlViewModel.UiValue));

                    _owningControlViewModel.UiValue = _previousValue;
                }
            }
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
