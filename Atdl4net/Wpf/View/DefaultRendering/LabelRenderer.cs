﻿#region Copyright (c) 2010-2012, Cornerstone Technology Limited. http://atdl4net.org
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

using System.ComponentModel.Composition;
using Atdl4net.Model.Controls;
using Common.Logging;

namespace Atdl4net.Wpf.View.DefaultRendering
{
    [Export(typeof(IWpfControlRenderer<Label_t>))]
    internal class LabelRenderer : IWpfControlRenderer<Label_t>
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Wpf.View");

        public void Render(WpfXmlWriter writer, Label_t control)
        {
            string id = WpfControlRenderer.CleanName(control.Id);

            _log.Debug(m => m("Rendering control {0} of type Label_t using {1}", control.Id, this.GetType().FullName));

            using (writer.New(WpfXmlWriterTag.Label))
            {
                writer.WriteAttribute(WpfXmlWriterAttribute.Margin, "1,3,1,3");

                WpfControlRenderer.WriteGridAttribute(writer, control);

                writer.WriteAttribute(WpfXmlWriterAttribute.ToolTip, string.Format("{{Binding Path=Controls[{0}].ToolTip}}", id));
                writer.WriteAttribute(WpfXmlWriterAttribute.Content, string.Format("{{Binding Path=Controls[{0}].UiValue}}", id));
                writer.WriteAttribute(WpfXmlWriterAttribute.IsEnabled, string.Format("{{Binding Path=Controls[{0}].Enabled}}", id));
                writer.WriteAttribute(WpfXmlWriterAttribute.Visibility, string.Format("{{Binding Path=Controls[{0}].Visibility}}", id));
                writer.WriteAttribute(WpfXmlWriterAttribute.AutomationProperties_AutomationId, id);
            }
        }
    }
}
