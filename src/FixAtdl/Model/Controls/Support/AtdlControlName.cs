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
