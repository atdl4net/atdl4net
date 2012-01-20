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
