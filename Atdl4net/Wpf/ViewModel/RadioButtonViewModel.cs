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
using Atdl4net.Model.Controls;
using Atdl4net.Model.Elements.Support;

namespace Atdl4net.Wpf.ViewModel
{
    /// <summary>
    /// View model class for <see cref="RadioButton_t"/> - needed only for .NET Framework v3.5 to workaround a bug
    /// in that version of the Framework that allows more than one WPF RadioButton in a radio button group to 
    /// selected at a time.
    /// </summary>
    public class RadioButtonViewModel : ControlViewModel
    {
        /// <summary>
        /// Gets/sets the <see cref="RadioButtonGroupManager"/> that is used to manage the state of all the
        /// RadioButtons within a given radio button group.
        /// </summary>
        public RadioButtonGroupManager RadioButtonGroupManager { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="RadioButtonViewModel"/>.
        /// </summary>
        /// <param name="control">Underlying <see cref="RadioButton_t"/> for this RadioButtonViewModel.</param>
        /// <param name="referencedParameter">Parameter that the RadioButton_t refers to.</param>
        public RadioButtonViewModel(RadioButton_t control, IParameter referencedParameter) 
            :base(control, referencedParameter)
        {
        }

        /// <summary>
        /// Gets/sets the user interface control value for the underlying <see cref="RadioButton_t"/>.
        /// </summary>
        public override object UiValue
        {
            get { return base.UiValue; }
            
            set
            {
                base.UiValue = value;

                if ((bool)value)
                {
                    if (RadioButtonGroupManager != null)
                        RadioButtonGroupManager.UpdateRadioButtons(this);
                }
                else
                    NotifyRadioButtonCleared();
            }
        }

        private void NotifyRadioButtonCleared()
        {
            base.NotifyPropertyChanged("Value");
        }
    }
}
#endif
