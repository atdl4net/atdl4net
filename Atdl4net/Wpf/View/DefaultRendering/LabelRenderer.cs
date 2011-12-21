using Atdl4net.Model.Controls;
using System.ComponentModel.Composition;

namespace Atdl4net.Wpf.View.DefaultRendering
{
    [Export(typeof(IWpfControlRenderer<Label_t>))]
    internal class LabelRenderer : IWpfControlRenderer<Label_t>
    {
        public void Render(WpfXmlWriter writer, Label_t control)
        {
            if (!string.IsNullOrEmpty(control.Label))
            {
                using (writer.New(WpfXmlWriterTag.Label))
                {
                    WpfControlRenderer.WriteGridAttribute(writer, control);

                    writer.WriteAttribute(WpfXmlWriterAttribute.ToolTip, string.Format("{0}Binding Path=Controls[{1}].ToolTip{2}", "{", control.Id, "}"));
                    writer.WriteAttribute(WpfXmlWriterAttribute.Content, control.Label);
                }
            }
        }
    }
}
