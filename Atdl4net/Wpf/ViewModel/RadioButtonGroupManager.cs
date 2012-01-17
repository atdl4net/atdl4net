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

#if !NET_40
using System.Collections.Generic;

namespace Atdl4net.Wpf.ViewModel
{
    /// <summary>
    /// Manages the state of WPF radio buttons within a radio button group - needed only for .NET Framework v3.5 to 
    /// workaround a bug in that version of the Framework that allows more than one WPF RadioButton in a radio button 
    /// group to selected at a time.
    /// </summary>
    public class RadioButtonGroupManager
    {
        private readonly string _groupName;
        private readonly List<RadioButtonViewModel> _radioButtonViewModels = new List<RadioButtonViewModel>();

        /// <summary>
        /// Initializes a new instance of <see cref="RadioButtonGroupManager"/> with the specified radio button
        /// group name.
        /// </summary>
        /// <param name="groupName">Radio button group name.</param>
        public RadioButtonGroupManager(string groupName)
        {
            _groupName = groupName;
        }

        /// <summary>
        /// Gets the name of the radio button group that this RadioButtonGroupManager is managing.
        /// </summary>
        public string GroupName { get { return _groupName; } }

        /// <summary>
        /// Registers a radio button via its <see cref="RadioButtonViewModel"/> with this RadionButtonGroupManager.
        /// </summary>
        /// <param name="radioButtonViewModel">Reference to the RadioButtonViewModel to register.</param>
        public void RegisterRadioButton(RadioButtonViewModel radioButtonViewModel)
        {
            _radioButtonViewModels.Add(radioButtonViewModel);
        }

        /// <summary>
        /// Updates the states of all radio buttons within the radio button group based on the state of the
        /// radio button specified by the supplied <see cref="RadioButtonViewModel"/>.
        /// </summary>
        /// <param name="activeButtonViewModel"></param>
        public void UpdateRadioButtons(RadioButtonViewModel activeButtonViewModel)
        {
            foreach (RadioButtonViewModel radioButtonViewModel in _radioButtonViewModels)
            {
                if (radioButtonViewModel.Id != activeButtonViewModel.Id)
                    radioButtonViewModel.UiValue = false;
            }
        }
    }
}
#endif