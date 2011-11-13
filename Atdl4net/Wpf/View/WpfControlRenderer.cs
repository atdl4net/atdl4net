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

using Atdl4net.Model.Controls;
using Atdl4net.Model.Elements;
using Atdl4net.Model.Enumerations;
using Atdl4net.Utility;
using System;
using System.ComponentModel.Composition;

namespace Atdl4net.Wpf.View
{
    public class WpfControlRenderer : IControl_tVisitor
    {
        private struct StandardGridCoordinates
        {
            public static readonly GridCoordinate Label = new GridCoordinate(0, 0);
            public static readonly GridCoordinate Control = new GridCoordinate(0, 1);
        }

        protected enum ListControlType
        {
            CheckBoxList,
            RadioButtonList
        }

        protected enum ComboControlType
        {
            DropDownList,
            EditableDropDownList
        }

        // TODO: // FIX THIS!
        private static WpfComboBoxSizer _comboBoxSizer;

        private readonly WpfXmlWriter _writer;

        #region Imported Control Renderers

        [Import(typeof(INamespaceProvider))]
        public INamespaceProvider NamespaceProvider { get; set; }

        [Import(typeof(IWpfControlRenderer<CheckBoxList_t>))]
        public IWpfControlRenderer<CheckBoxList_t> CheckBoxListRenderer { get; set; }

        [Import(typeof(IWpfControlRenderer<CheckBox_t>))]
        public IWpfControlRenderer<CheckBox_t> CheckBoxRenderer { get; set; }

        [Import(typeof(IWpfControlRenderer<Clock_t>))]
        public IWpfControlRenderer<Clock_t> ClockRenderer { get; set; }

        [Import(typeof(IWpfControlRenderer<DoubleSpinner_t>))]
        public IWpfControlRenderer<DoubleSpinner_t> DoubleSpinnerRenderer { get; set; }

        [Import(typeof(IWpfControlRenderer<DropDownList_t>))]
        public IWpfControlRenderer<DropDownList_t> DropDownListRenderer { get; set; }

        [Import(typeof(IWpfControlRenderer<EditableDropDownList_t>))]
        public IWpfControlRenderer<EditableDropDownList_t> EditableDropDownListRenderer { get; set; }

        [Import(typeof(IWpfControlRenderer<HiddenField_t>))]
        public IWpfControlRenderer<HiddenField_t> HiddenFieldRenderer { get; set; }

        [Import(typeof(IWpfControlRenderer<Label_t>))]
        public IWpfControlRenderer<Label_t> LabelRenderer { get; set; }

        [Import(typeof(IWpfControlRenderer<MultiSelectList_t>))]
        public IWpfControlRenderer<MultiSelectList_t> MultiSelectListRenderer { get; set; }

        [Import(typeof(IWpfControlRenderer<RadioButtonList_t>))]
        public IWpfControlRenderer<RadioButtonList_t> RadioButtonListRenderer { get; set; }

        [Import(typeof(IWpfControlRenderer<RadioButton_t>))]
        public IWpfControlRenderer<RadioButton_t> RadioButtonRenderer { get; set; }

        [Import(typeof(IWpfControlRenderer<SingleSelectList_t>))]
        public IWpfControlRenderer<SingleSelectList_t> SingleSelectListRenderer { get; set; }

        [Import(typeof(IWpfControlRenderer<SingleSpinner_t>))]
        public IWpfControlRenderer<SingleSpinner_t> SingleSpinnerRenderer { get; set; }

        [Import(typeof(IWpfControlRenderer<Slider_t>))]
        public IWpfControlRenderer<Slider_t> SliderRenderer { get; set; }

        [Import(typeof(IWpfControlRenderer<TextField_t>))]
        public IWpfControlRenderer<TextField_t> TextFieldRenderer { get; set; }

        #endregion Imported Control Renderers

        public WpfControlRenderer(WpfXmlWriter writer, WpfComboBoxSizer sizer)
        {
            _writer = writer;
            _comboBoxSizer = sizer;

            _comboBoxSizer.Clear();
        }

        public static WpfComboBoxSizer ComboBoxSizer { get { return _comboBoxSizer; } }

        public void ProcessControl(Control_t control)
        {
            control.DoVisit(this);
        }
    
        #region IControl_tVisitor Members

        public void Visit(CheckBox_t control)
        {
            CheckBoxRenderer.Render(_writer, control);
        }

        public void Visit(CheckBoxList_t control)
        {
            CheckBoxListRenderer.Render(_writer, control);
//            RenderListControl(control.ListItems, control.Orientation, ListControlType.CheckBoxList, control.Id);
        }

        public void Visit(Clock_t control)
        {
            ClockRenderer.Render(_writer, control);
        }

        public void Visit(DoubleSpinner_t control)
        {
            DoubleSpinnerRenderer.Render(_writer, control);
            //RenderLabel(control.Label, control.Id);

            //RenderInputControl(control.Id, "text", "atdl-doublespinner");
        }

        public void Visit(DropDownList_t control)
        {
            DropDownListRenderer.Render(_writer, control);
        }

        public void Visit(EditableDropDownList_t control)
        {
            EditableDropDownListRenderer.Render(_writer, control);
        }

        public void Visit(HiddenField_t control)
        {
            HiddenFieldRenderer.Render(_writer, control);
        }

        public void Visit(Label_t control)
        {
            LabelRenderer.Render(_writer, control);
        }

        public void Visit(MultiSelectList_t control)
        {
            MultiSelectListRenderer.Render(_writer, control);
//            RenderLabel(control.Label, control.Id);

//            RenderSelectControl(control.ListItems, SelectControlType.ListBox, true, control.Id);
        }

        public void Visit(RadioButton_t control)
        {
            RadioButtonRenderer.Render(_writer, control);
        }

        public void Visit(RadioButtonList_t control)
        {
            RadioButtonListRenderer.Render(_writer, control);
        }

        public void Visit(SingleSelectList_t control)
        {
            SingleSelectListRenderer.Render(_writer, control);
//            RenderLabel(control.Label, control.Id);

//            RenderSelectControl(control.ListItems, SelectControlType.ListBox, false, control.Id);
        }

        public void Visit(SingleSpinner_t control)
        {
            SingleSpinnerRenderer.Render(_writer, control);
        }

        public void Visit(Slider_t control)
        {
            SliderRenderer.Render(_writer, control);
        }

        public void Visit(TextField_t control)
        {
            TextFieldRenderer.Render(_writer, control);
        }

        public void Visit(Control_t control)
        {
            throw new NotImplementedException();
        }

        #endregion

        public delegate void ControlRenderer<T>(T control, GridCoordinate gridCoordinate);

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

        public static void WriteGridAttribute(WpfXmlWriter writer, Control_t control)
        {
            bool isVertical = ((control as IParentable<StrategyPanel_t>).Parent.Orientation == Orientation_t.Vertical);

            writer.WriteAttribute(isVertical ? WpfXmlWriterAttribute.GridRow : WpfXmlWriterAttribute.GridColumn,
                control.Index.ToString());
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

        public static string CleanName(string controlId)
        {
            return controlId;
        }
    }
}
