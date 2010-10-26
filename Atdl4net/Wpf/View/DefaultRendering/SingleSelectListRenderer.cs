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
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;

namespace Atdl4net.Wpf.View.DefaultRendering
{
    [Export(typeof(IWpfControlRenderer<SingleSelectList_t>))]
    internal class SingleSelectListRenderer : IWpfControlRenderer<SingleSelectList_t>
    {
        public void Render(WpfXmlWriter writer, SingleSelectList_t control)
        {
            string id = WpfControlRenderer.CleanName(control.Id);

            if (GlobalSettings.View.Wpf.AutosizeDropdowns)
                WpfControlRenderer.ComboBoxSizer.RegisterComboBox(id, control.ListItems);

            WpfControlRenderer.RenderLabelledControl<SingleSelectList_t>(writer, control, (c, gridCoordinate) =>
            {
                using (writer.New(WpfXmlWriterTag.ListBox))
                {
                    writer.WriteAttribute(WpfXmlWriterAttribute.GridColumn, gridCoordinate.Column.ToString());
                    writer.WriteAttribute(WpfXmlWriterAttribute.GridRow, gridCoordinate.Row.ToString());

                    writer.WriteAttribute(WpfXmlWriterAttribute.Margin, "2");

                    if (!string.IsNullOrEmpty(id))
                        writer.WriteAttribute(WpfXmlWriterAttribute.Name, id);

                    writer.WriteAttribute(WpfXmlWriterAttribute.ItemsSource, string.Format("{0}Binding Path=Controls[{1}].ListItems{2}", "{", id, "}"));
                    writer.WriteAttribute(WpfXmlWriterAttribute.SelectedValue, string.Format("{0}Binding Path=Controls[{1}].SelectedValue{2}", "{", id, "}"));
                    writer.WriteAttribute(WpfXmlWriterAttribute.SelectedValuePath, string.Format("EnumId", "{", id, "}"));
                    writer.WriteAttribute(WpfXmlWriterAttribute.DisplayMemberPath, "UiRep");
                }
            });
        }
    }
}
