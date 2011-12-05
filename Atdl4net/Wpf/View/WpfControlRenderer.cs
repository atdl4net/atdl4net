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

using System;
using System.ComponentModel.Composition;
using Atdl4net.Model.Controls;
using Atdl4net.Model.Controls.Support;
using Atdl4net.Model.Elements;
using Atdl4net.Model.Enumerations;
using Atdl4net.Utility;

namespace Atdl4net.Wpf.View
{
    /// <summary>
    /// Provides XAML rendering of FIXatdl controls.
    /// </summary>
    public class WpfControlRenderer : IControlVisitor
    {
        private struct StandardGridCoordinates
        {
            public static readonly GridCoordinate Label = new GridCoordinate(0, 0);
            public static readonly GridCoordinate Control = new GridCoordinate(0, 1);
        }

        /// <summary>
        /// Enumeration for controls that use lists of other controls (radio buttons, check boxes).
        /// </summary>
        protected enum ListControlType
        {
            /// <summary>CheckBoxList.</summary>
            CheckBoxList,

            /// <summary>RadioButtonList.</summary>
            RadioButtonList
        }

        /// <summary>
        /// Enumeration for controls that use ComboBoxes.
        /// </summary>
        protected enum ComboControlType
        {
            /// <summary>DropDownList.</summary>
            DropDownList,

            /// <summary>EditableDropDownList</summary>
            EditableDropDownList
        }

        // TODO: // FIX THIS!
        private static WpfComboBoxSizer _comboBoxSizer;

        private readonly WpfXmlWriter _writer;

        #region Imported Control Renderers

        /// <summary>Implement NamespaceProvider in order to provide custom XAML namespaces (typically
        /// needed to support use of custom controls).</summary>
        [Import(typeof(INamespaceProvider))]
        public INamespaceProvider NamespaceProvider { get; set; }

        /// <summary>Implement CheckBoxListRenderer to provide custom rendering of CheckBoxList_t.</summary>
        [Import(typeof(IWpfControlRenderer<CheckBoxList_t>))]
        public IWpfControlRenderer<CheckBoxList_t> CheckBoxListRenderer { get; set; }

        /// <summary>Implement CheckBoxRenderer to provide custom rendering of CheckBox_t.</summary>
        [Import(typeof(IWpfControlRenderer<CheckBox_t>))]
        public IWpfControlRenderer<CheckBox_t> CheckBoxRenderer { get; set; }

        /// <summary>Implement ClockRenderer to provide custom rendering of Clock_t.</summary>
        [Import(typeof(IWpfControlRenderer<Clock_t>))]
        public IWpfControlRenderer<Clock_t> ClockRenderer { get; set; }

        /// <summary>Implement DoubleSpinnerRenderer to provide custom rendering of DoubleSpinner_t.</summary>
        [Import(typeof(IWpfControlRenderer<DoubleSpinner_t>))]
        public IWpfControlRenderer<DoubleSpinner_t> DoubleSpinnerRenderer { get; set; }

        /// <summary>Implement DropDownListRenderer to provide custom rendering of DropDownList_t.</summary>
        [Import(typeof(IWpfControlRenderer<DropDownList_t>))]
        public IWpfControlRenderer<DropDownList_t> DropDownListRenderer { get; set; }

        /// <summary>Implement EditableDropDownListRenderer to provide custom rendering of EditableDropDownList_t.</summary>
        [Import(typeof(IWpfControlRenderer<EditableDropDownList_t>))]
        public IWpfControlRenderer<EditableDropDownList_t> EditableDropDownListRenderer { get; set; }

        /// <summary>Implement HiddenFieldRenderer to provide custom rendering of HiddenField_t.</summary>
        [Import(typeof(IWpfControlRenderer<HiddenField_t>))]
        public IWpfControlRenderer<HiddenField_t> HiddenFieldRenderer { get; set; }

        /// <summary>Implement LabelRenderer to provide custom rendering of Label_t.</summary>
        [Import(typeof(IWpfControlRenderer<Label_t>))]
        public IWpfControlRenderer<Label_t> LabelRenderer { get; set; }

        /// <summary>Implement MultiSelectListRenderer to provide custom rendering of MultiSelectList_t.</summary>
        [Import(typeof(IWpfControlRenderer<MultiSelectList_t>))]
        public IWpfControlRenderer<MultiSelectList_t> MultiSelectListRenderer { get; set; }

        /// <summary>Implement RadioButtonListRenderer to provide custom rendering of RadioButtonList_t.</summary>
        [Import(typeof(IWpfControlRenderer<RadioButtonList_t>))]
        public IWpfControlRenderer<RadioButtonList_t> RadioButtonListRenderer { get; set; }

        /// <summary>Implement RadioButtonRenderer to provide custom rendering of RadioButton_t.</summary>
        [Import(typeof(IWpfControlRenderer<RadioButton_t>))]
        public IWpfControlRenderer<RadioButton_t> RadioButtonRenderer { get; set; }

        /// <summary>Implement SingleSelectListRenderer to provide custom rendering of SingleSelectList_t.</summary>
        [Import(typeof(IWpfControlRenderer<SingleSelectList_t>))]
        public IWpfControlRenderer<SingleSelectList_t> SingleSelectListRenderer { get; set; }

        /// <summary>Implement SingleSpinnerRenderer to provide custom rendering of SingleSpinner_t.</summary>
        [Import(typeof(IWpfControlRenderer<SingleSpinner_t>))]
        public IWpfControlRenderer<SingleSpinner_t> SingleSpinnerRenderer { get; set; }

        /// <summary>Implement SliderRenderer to provide custom rendering of Slider_t.</summary>
        [Import(typeof(IWpfControlRenderer<Slider_t>))]
        public IWpfControlRenderer<Slider_t> SliderRenderer { get; set; }

        /// <summary>Implement TextFieldRenderer to provide custom rendering of TextField_t.</summary>
        [Import(typeof(IWpfControlRenderer<TextField_t>))]
        public IWpfControlRenderer<TextField_t> TextFieldRenderer { get; set; }

        #endregion Imported Control Renderers

        /// <summary>
        /// Initializes a new WpfControlRenderer.
        /// </summary>
        /// <param name="writer">WpfXmlWriter to use for writing the output XAML.</param>
        /// <param name="sizer">Combo box sizer.</param>
        public WpfControlRenderer(WpfXmlWriter writer, WpfComboBoxSizer sizer)
        {
            _writer = writer;
            _comboBoxSizer = sizer;

            _comboBoxSizer.Clear();
        }

        /// <summary>
        /// Gets the ComboBoxSizer - a type that knows how to size comboboxes to their widest member.
        /// </summary>
        public static WpfComboBoxSizer ComboBoxSizer { get { return _comboBoxSizer; } }

        /// <summary>
        /// Processes each control, i.e., renders the XAML for the supplied control.
        /// </summary>
        /// <param name="control">Control to generate XAML for.</param>
        public void ProcessControl(Control_t control)
        {
            control.DoVisit(this);
        }
    
        #region IControl_tVisitor Members

        /// <summary>
        /// Renders the supplied CheckBox_t as XAML.
        /// </summary>
        /// <param name="control">CheckBox_t to render.</param>
        public void Visit(CheckBox_t control)
        {
            CheckBoxRenderer.Render(_writer, control);
        }

        /// <summary>
        /// Renders the supplied CheckBoxList_t as XAML.
        /// </summary>
        /// <param name="control">CheckBoxList_t to render.</param>
        public void Visit(CheckBoxList_t control)
        {
            CheckBoxListRenderer.Render(_writer, control);
        }

        /// <summary>
        /// Renders the supplied Clock_t as XAML.
        /// </summary>
        /// <param name="control">Clock_t to render.</param>
        public void Visit(Clock_t control)
        {
            ClockRenderer.Render(_writer, control);
        }

        /// <summary>
        /// Renders the supplied DoubleSpinner_t as XAML.
        /// </summary>
        /// <param name="control">DoubleSpinner_t to render.</param>
        public void Visit(DoubleSpinner_t control)
        {
            DoubleSpinnerRenderer.Render(_writer, control);
        }

        /// <summary>
        /// Renders the supplied DropDownList_t as XAML.
        /// </summary>
        /// <param name="control">DropDownList_t to render.</param>
        public void Visit(DropDownList_t control)
        {
            DropDownListRenderer.Render(_writer, control);
        }

        /// <summary>
        /// Renders the supplied EditableDropDownList_t as XAML.
        /// </summary>
        /// <param name="control">EditableDropDownList_t to render.</param>
        public void Visit(EditableDropDownList_t control)
        {
            EditableDropDownListRenderer.Render(_writer, control);
        }

        /// <summary>
        /// Renders the supplied HiddenField_t as XAML.
        /// </summary>
        /// <param name="control">HiddenField_t to render.</param>
        public void Visit(HiddenField_t control)
        {
            HiddenFieldRenderer.Render(_writer, control);
        }

        /// <summary>
        /// Renders the supplied Label_t as XAML.
        /// </summary>
        /// <param name="control">Label_t to render.</param>
        public void Visit(Label_t control)
        {
            LabelRenderer.Render(_writer, control);
        }

        /// <summary>
        /// Renders the supplied MultiSelectList_t as XAML.
        /// </summary>
        /// <param name="control">MultiSelectList_t to render.</param>
        public void Visit(MultiSelectList_t control)
        {
            MultiSelectListRenderer.Render(_writer, control);
        }

        /// <summary>
        /// Renders the supplied RadioButton_t as XAML.
        /// </summary>
        /// <param name="control">RadioButton_t to render.</param>
        public void Visit(RadioButton_t control)
        {
            RadioButtonRenderer.Render(_writer, control);
        }

        /// <summary>
        /// Renders the supplied RadioButtonList_t as XAML.
        /// </summary>
        /// <param name="control">RadioButtonList_t to render.</param>
        public void Visit(RadioButtonList_t control)
        {
            RadioButtonListRenderer.Render(_writer, control);
        }

        /// <summary>
        /// Renders the supplied SingleSelectList_t as XAML.
        /// </summary>
        /// <param name="control">SingleSelectList_t to render.</param>
        public void Visit(SingleSelectList_t control)
        {
            SingleSelectListRenderer.Render(_writer, control);
        }

        /// <summary>
        /// Renders the supplied SingleSpinner_t as XAML.
        /// </summary>
        /// <param name="control">SingleSpinner_t to render.</param>
        public void Visit(SingleSpinner_t control)
        {
            SingleSpinnerRenderer.Render(_writer, control);
        }

        /// <summary>
        /// Renders the supplied Slider_t as XAML.
        /// </summary>
        /// <param name="control">Slider_t to render.</param>
        public void Visit(Slider_t control)
        {
            SliderRenderer.Render(_writer, control);
        }

        /// <summary>
        /// Renders the supplied TextField_t as XAML.
        /// </summary>
        /// <param name="control">TextField_t to render.</param>
        public void Visit(TextField_t control)
        {
            TextFieldRenderer.Render(_writer, control);
        }

        /// <summary>
        /// Non-specific control render method - this method should never get called.
        /// </summary>
        /// <param name="control">Control to render.</param>
        public void Visit(Control_t control)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Delegate that is used to call each control renderer.
        /// </summary>
        /// <typeparam name="T">Type of control.</typeparam>
        /// <param name="control">Control to be rendered.</param>
        /// <param name="gridCoordinate">Place in the grid for this control.</param>
        public delegate void ControlRenderer<T>(T control, GridCoordinate gridCoordinate);

        /// <summary>
        /// Renders the supplied "labelled control", i.e., control which has a label. 
        /// </summary>
        /// <typeparam name="T">Type of control.</typeparam>
        /// <param name="writer">WpfXmlWriter to use to write the XAML.</param>
        /// <param name="control">Control to render alongside label.</param>
        /// <param name="controlRenderer">Control renderer to use to render the control.</param>
        public static void RenderLabelledControl<T>(WpfXmlWriter writer, T control, ControlRenderer<T> controlRenderer) where T : Control_t
        {
            bool isVertical = ((control as IParentable<StrategyPanel_t>).Parent.Orientation == Orientation_t.Vertical);

            // If this is a vertical StrategyPanel, we don't bother with a containing Grid - this provides nice alignment of labels and controls
            if (isVertical)
            {
                RenderControlLabel(writer, control, new GridCoordinate(control.Index, 0));

                controlRenderer(control, new GridCoordinate(control.Index, 1));
            }
            else
            {
                using (writer.New(WpfXmlWriterTag.Grid))
                {
                    writer.WriteAttribute(WpfXmlWriterAttribute.GridColumn, control.Index.ToString());

                    using (writer.New(WpfXmlWriterTag.GridColumnDefinitions))
                    {
                        for (int n = 0; n < 2; n++)
                        {
                            using (writer.New(WpfXmlWriterTag.ColumnDefinition))
                                writer.WriteAttribute(WpfXmlWriterAttribute.Width, "Auto");
                        }
                    }

                    using (writer.New(WpfXmlWriterTag.GridRowDefinitions))
                    {
                        using (writer.New(WpfXmlWriterTag.RowDefinition))
                            writer.WriteAttribute(WpfXmlWriterAttribute.Height, "Auto");
                    }

                    RenderControlLabel(writer, control, StandardGridCoordinates.Label);

                    controlRenderer(control, StandardGridCoordinates.Control);
                }
            }
        }

        /// <summary>
        /// Writes the Grid.Row or Grid.Column attribute as appropriate for this control based on orientation of
        /// the panel this control is a member of.
        /// </summary>
        /// <param name="writer">WpfXmlWriter to use to write the XAML to.</param>
        /// <param name="control">Control.</param>
        public static void WriteGridAttribute(WpfXmlWriter writer, Control_t control)
        {
            bool isVertical = ((control as IParentable<StrategyPanel_t>).Parent.Orientation == Orientation_t.Vertical);

            writer.WriteAttribute(isVertical ? WpfXmlWriterAttribute.GridRow : WpfXmlWriterAttribute.GridColumn,
                control.Index.ToString());
        }

        /// <summary>
        /// Gets a "cleaned up" version of the control ID so it can be used in XAML.
        /// </summary>
        /// <param name="controlId">ID of the Control_t.</param>
        /// <returns>Cleaned up control ID.</returns>
        /// <remarks>Currently this method does nothing, but could be adjusted if a FIXatdl file uses
        /// characters that are not allowed within XAML.</remarks>
        public static string CleanName(string controlId)
        {
            return controlId;
        }

        private static void RenderControlLabel(WpfXmlWriter writer, Control_t control, GridCoordinate gridCoordinate)
        {
            string label = control.Label;
            string forControl = control.Id;

            // Assumes that a control label will always be in the first cell of a 2 x 1 grid.
            if (!string.IsNullOrEmpty(label))
            {
                using (writer.New(WpfXmlWriterTag.Label))
                {
                    writer.WriteAttribute(WpfXmlWriterAttribute.GridColumn, gridCoordinate.Column.ToString());
                    writer.WriteAttribute(WpfXmlWriterAttribute.GridRow, gridCoordinate.Row.ToString());

//                    writer.WriteAttribute(WpfXmlWriterAttribute.Margin, "2,0,2,0");

                    if (!string.IsNullOrEmpty(forControl))
                    {
                        writer.WriteAttribute(WpfXmlWriterAttribute.Target, string.Format("{0}Binding ElementName={1}{2}", "{", CleanName(forControl), "}"));
                        writer.WriteAttribute(WpfXmlWriterAttribute.IsEnabled, string.Format("{0}Binding Path=Controls[{1}].Enabled{2}", "{", CleanName(forControl), "}"));
                        writer.WriteAttribute(WpfXmlWriterAttribute.Visibility, string.Format("{0}Binding Path=Controls[{1}].Visibility{2}", "{", CleanName(forControl), "}"));
                    }

                    writer.WriteAttribute(WpfXmlWriterAttribute.Content, label);

                }
            }
        }
    }
}
