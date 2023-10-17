#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
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
