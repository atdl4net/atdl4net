using Atdl4net.Model.Controls;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;

namespace Atdl4net.Wpf.View.DefaultRendering
{
    [Export(typeof(IWpfControlRenderer<HiddenField_t>))]
    internal class HiddenFieldRenderer : IWpfControlRenderer<HiddenField_t>
    {
        public void Render(WpfXmlWriter writer, HiddenField_t control)
        {
            // Nothing to do!
        }
    }
}
