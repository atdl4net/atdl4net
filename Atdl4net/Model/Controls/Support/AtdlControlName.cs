#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

namespace Atdl4net.Model.Controls.Support
{
    /// <summary>
    /// Helper class containing the names of all the valid FIXatdl control types.
    /// </summary>
    /// <remarks>NB We use const here rather than static readonly so the values can be used in a switch statement.</remarks>
    public class AtdlControlName
    {
        /// <summary>CheckBox_t type.</summary>
        public const string CheckBox_t = "CheckBox_t";

        /// <summary>CheckBoxList_t type.</summary>
        public const string CheckBoxList_t = "CheckBoxList_t";
        
        /// <summary>Clock_t type.</summary>
        public const string Clock_t = "Clock_t";
        
        /// <summary>DoubleSpinner_t type.</summary>
        public const string DoubleSpinner_t = "DoubleSpinner_t";
        
        /// <summary>DropDownList_t type.</summary>
        public const string DropDownList_t = "DropDownList_t";
        
        /// <summary>EditableDropDownList_t type.</summary>
        public const string EditableDropDownList_t = "EditableDropDownList_t";
        
        /// <summary>HiddenField_t type.</summary>
        public const string HiddenField_t = "HiddenField_t";
        
        /// <summary>Label_t type.</summary>
        public const string Label_t = "Label_t";
        
        /// <summary>MultiSelectList_t type.</summary>
        public const string MultiSelectList_t = "MultiSelectList_t";
        
        /// <summary>RadioButton_t type.</summary>
        public const string RadioButton_t = "RadioButton_t";
        
        /// <summary>RadioButtonList_t type.</summary>
        public const string RadioButtonList_t = "RadioButtonList_t";
        
        /// <summary>SingleSelectList_t type.</summary>
        public const string SingleSelectList_t = "SingleSelectList_t";
        
        /// <summary>SingleSpinner_t type.</summary>
        public const string SingleSpinner_t = "SingleSpinner_t";
        
        /// <summary>Slider_t type.</summary>
        public const string Slider_t = "Slider_t";
        
        /// <summary>TextField_t type.</summary>
        public const string TextField_t = "TextField_t";
    }
}
