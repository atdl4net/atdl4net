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

using Atdl4net.Model.Controls;
using Atdl4net.Wpf.View.Controls;
using System.ComponentModel.Composition;

namespace Atdl4net.Wpf.View.DefaultRendering
{
    [Export(typeof(IWpfControlRenderer<DoubleSpinner_t>))]
    internal class DoubleSpinnerRenderer : IWpfControlRenderer<DoubleSpinner_t>
    {
        public void Render(WpfXmlWriter writer, DoubleSpinner_t control)
        {
            string id = WpfControlRenderer.CleanName(control.Id);

            WpfControlRenderer.RenderLabelledControl<DoubleSpinner_t>(writer, control, (c, gridCoordinate) =>
            {
                using (writer.New(DefaultNamespaceProvider.Atdl4netNamespaceUri, typeof(DoubleSpinner).Name))
                {
                    writer.WriteAttribute(WpfXmlWriterAttribute.GridColumn, gridCoordinate.Column.ToString());
                    writer.WriteAttribute(WpfXmlWriterAttribute.GridRow, gridCoordinate.Row.ToString());

                    if (!string.IsNullOrEmpty(c.Id))
                        writer.WriteAttribute(WpfXmlWriterAttribute.Name, id);

                    writer.WriteAttribute(WpfXmlWriterAttribute.Value, string.Format("{0}Binding Path=Controls[{1}].UiValue,Mode=TwoWay,TargetNullValue={{x:Static sys:String.Empty}},StringFormat=\\{{0:D\\}}{2}", "{", id, "}"));
                    writer.WriteAttribute(WpfXmlWriterAttribute.InnerIncrement, string.Format("{0}Binding Path=Controls[{1}].UnderlyingControl.InnerIncrement, Mode=OneWay{2}", "{", id, "}"));
                    writer.WriteAttribute(WpfXmlWriterAttribute.OuterIncrement, string.Format("{0}Binding Path=Controls[{1}].UnderlyingControl.OuterIncrement, Mode=OneWay{2}", "{", id, "}"));
                    writer.WriteAttribute(WpfXmlWriterAttribute.IsEnabled, string.Format("{0}Binding Path=Controls[{1}].Enabled{2}", "{", id, "}"));
                    writer.WriteAttribute(WpfXmlWriterAttribute.Visibility, string.Format("{0}Binding Path=Controls[{1}].Visibility{2}", "{", id, "}"));

                    writer.WriteAttribute(WpfXmlWriterAttribute.Width, "120");
                }
            });
        }
    }
}
