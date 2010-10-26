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
    [Export(typeof(IWpfControlRenderer<TextField_t>))]
    internal class TextFieldRenderer : IWpfControlRenderer<TextField_t>
    {
        public void Render(WpfXmlWriter writer, TextField_t control)
        {
            string id = WpfControlRenderer.CleanName(control.Id);

            WpfControlRenderer.RenderLabelledControl<TextField_t>(writer, control, (c, gridCoordinate) =>
            {
                using (writer.New(WpfXmlWriterTag.TextBox))
                {
                    writer.WriteAttribute(WpfXmlWriterAttribute.GridColumn, gridCoordinate.Column.ToString());
                    writer.WriteAttribute(WpfXmlWriterAttribute.GridRow, gridCoordinate.Row.ToString());

//                    writer.WriteAttribute(WpfXmlWriterAttribute.Margin, "2,0,2,0");
                    writer.WriteAttribute(WpfXmlWriterAttribute.Margin, "1");

                    if (!string.IsNullOrEmpty(c.Id))
                        writer.WriteAttribute(WpfXmlWriterAttribute.Name, id);

                    writer.WriteAttribute(WpfXmlWriterAttribute.Width, "120");

                    writer.WriteAttribute(WpfXmlWriterAttribute.Text, string.Format("{0}Binding Path=Controls[{1}].Value{2}", "{", id, "}"));
                    writer.WriteAttribute(WpfXmlWriterAttribute.IsEnabled, string.Format("{0}Binding Path=Controls[{1}].Enabled{2}", "{", id, "}"));
                }
            });
        }
    }
}
