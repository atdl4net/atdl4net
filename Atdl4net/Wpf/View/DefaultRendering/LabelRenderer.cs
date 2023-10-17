#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
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
