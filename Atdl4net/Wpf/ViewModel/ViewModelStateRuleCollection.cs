#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System.Collections.ObjectModel;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Elements;
using Atdl4net.Utility;
using Common.Logging;

namespace Atdl4net.Wpf.ViewModel
{
    public class ViewModelStateRuleCollection : Collection<StateRuleViewModel>, IBindable<ViewModelControlCollection>
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Wpf.ViewModel");

        private readonly ControlViewModel _owningControl;

        public ViewModelStateRuleCollection(ControlViewModel owningControl, StateRuleCollection stateRules)
        {
            _owningControl = owningControl;

            foreach (StateRule_t stateRule in stateRules)
            {
                StateRuleViewModel stateRuleViewModel = new StateRuleViewModel(owningControl, stateRule);

                Add(stateRuleViewModel);
            }
        }

        public void RefreshState()
        {
            if (this.Items.Count > 0 )
                _log.Debug(m=>m("Refreshing state for all state rules of control ID {0}", _owningControl.Id));

            foreach (StateRuleViewModel stateRule in this.Items)
                stateRule.RefreshState();
        }

        void IBindable<ViewModelControlCollection>.Bind(ViewModelControlCollection target)
        {
            _log.Debug(m=>m("Binding state rules for control Id={0}.", _owningControl.Id));

            foreach (IBindable<ViewModelControlCollection> item in Items)
                item.Bind(target);
        }
    }
}
