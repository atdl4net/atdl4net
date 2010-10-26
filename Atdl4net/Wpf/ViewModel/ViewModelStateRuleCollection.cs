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

using Atdl4net.Diagnostics;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Elements;
using Atdl4net.Utility;
using System.Collections.ObjectModel;

namespace Atdl4net.Wpf.ViewModel
{
    public class ViewModelStateRuleCollection : Collection<StateRuleWrapper>, IBindable<ViewModelControlCollection>
    {
        private ControlWrapper _owningControl;

        public ViewModelStateRuleCollection(ControlWrapper owningControl, StateRuleCollection stateRules)
        {
            _owningControl = owningControl;

            foreach (StateRule_t stateRule in stateRules)
            {
                StateRuleWrapper wrapper = new StateRuleWrapper(owningControl, stateRule);

                Add(wrapper);
            }
        }

        public void RefreshState()
        {
            foreach (StateRuleWrapper stateRule in this.Items)
                stateRule.RefreshState();
        }

        void IBindable<ViewModelControlCollection>.Bind(ViewModelControlCollection target)
        {
            Logger.DebugFormat("Binding state rules for control Id={0}.", _owningControl.Id);

            foreach (IBindable<ViewModelControlCollection> item in Items)
                item.Bind(target);
        }
    }
}
