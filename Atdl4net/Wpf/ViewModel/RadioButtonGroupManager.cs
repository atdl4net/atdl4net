#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
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