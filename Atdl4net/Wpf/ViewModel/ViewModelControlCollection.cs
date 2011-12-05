#region Copyright (c) 2010-2011, Cornerstone Technology Limited. http://atdl4net.org
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

using Atdl4net.Model.Controls;
using Atdl4net.Model.Elements;
using Atdl4net.Utility;
using System.Collections.ObjectModel;

namespace Atdl4net.Wpf.ViewModel
{
    public class ViewModelControlCollection : KeyedCollection<string, ControlWrapper>
    {
#if !NET_40
        #region Special support for RadioButtons on .NET 3.5

        private class RadioButtonGroupManagerCollection : KeyedCollection<string, RadioButtonGroupManager>
        {
            protected override string  GetKeyForItem(RadioButtonGroupManager item)
            {
 	            return item.GroupName;
            }

            public RadioButtonGroupManager AddGroup(string groupName)
            {
                RadioButtonGroupManager groupManager = new RadioButtonGroupManager(groupName);

                Add(groupManager);

                return groupManager;
            }
        }

        private RadioButtonGroupManagerCollection _radioButtonGroups = new RadioButtonGroupManagerCollection();

        #endregion
#endif

        private readonly Strategy_t _underlyingStrategy;

        public ViewModelControlCollection(Strategy_t strategy, DataEntryMode mode)
        {
            _underlyingStrategy = strategy;

            foreach (Control_t control in strategy.Controls)
            {
                ControlWrapper controlWrapper = ControlWrapper.Create(strategy, control, mode);

                Add(controlWrapper);
#if !NET_40
                if (control is RadioButton_t)
                    RegisterRadioButton(control as RadioButton_t, controlWrapper as RadioButtonWrapper);
#endif
            }
        }

        public void Bind()
        {
            foreach (ControlWrapper controlWrapper in Items)
                (controlWrapper as IBindable<ViewModelControlCollection>).Bind(this);
        }

        public void RefreshState()
        {
            foreach (ControlWrapper controlWrapper in Items)
                controlWrapper.RefreshState();
        }

        protected override string GetKeyForItem(ControlWrapper item)
        {
            return item.Id;
        }

        public void UpdateParameterValues()
        {
            foreach (ControlWrapper controlWrapper in Items)
            {
                controlWrapper.UpdateParameterValue();
            }
        }

        public void UpdateControlsFromParameters()
        {
            foreach (ControlWrapper controlWrapper in Items)
            {
                controlWrapper.UpdateFromParameterValue();
            }
        }

#if !NET_40
        private void RegisterRadioButton(RadioButton_t radioButton, RadioButtonWrapper controlWrapper)
        {
            string groupName = radioButton.RadioGroup;

            if (!string.IsNullOrEmpty(groupName))
            {
                RadioButtonGroupManager groupManager;

                if (_radioButtonGroups.Contains(groupName))
                    groupManager = _radioButtonGroups[groupName];
                else
                    groupManager = _radioButtonGroups.AddGroup(groupName);

                groupManager.RegisterRadioButton(controlWrapper);

                controlWrapper.RadioButtonGroupManager = groupManager;
            }
        }
#endif
    }
}
