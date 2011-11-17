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
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;

namespace Atdl4net.Wpf.View.DefaultRendering
{
    [Export(typeof(IWpfControlRenderer<RadioButton_t>))]
    internal class RadioButtonRenderer : IWpfControlRenderer<RadioButton_t>
    {
        public void Render(WpfXmlWriter writer, RadioButton_t control)
        {
            string id = WpfControlRenderer.CleanName(control.Id);

            using (writer.New(WpfXmlWriterTag.RadioButton))
            {
                WpfControlRenderer.WriteGridAttribute(writer, control);

                writer.WriteAttribute(WpfXmlWriterAttribute.Margin, "1,5,4,1");

                if (!string.IsNullOrEmpty(control.Label))
                    writer.WriteAttribute(WpfXmlWriterAttribute.Content, control.Label);

                if (!string.IsNullOrEmpty(control.Id))
                    writer.WriteAttribute(WpfXmlWriterAttribute.Name, id);
#if NET_40
                if (!string.IsNullOrEmpty(control.RadioGroup))
                    writer.WriteAttribute(WpfXmlWriterAttribute.GroupName, WpfControlRenderer.CleanName(control.RadioGroup));
#endif
                writer.WriteAttribute(WpfXmlWriterAttribute.IsChecked, string.Format("{0}Binding Path=Controls[{1}].Value{2}", "{", id, "}"));
            }
        }
    }
}
