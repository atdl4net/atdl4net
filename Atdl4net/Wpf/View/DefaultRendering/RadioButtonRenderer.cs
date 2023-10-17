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
    [Export(typeof(IWpfControlRenderer<RadioButton_t>))]
    internal class RadioButtonRenderer : IWpfControlRenderer<RadioButton_t>
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Wpf.View");

        public void Render(WpfXmlWriter writer, RadioButton_t control)
        {
            string id = WpfControlRenderer.CleanName(control.Id);

            _log.Debug(m => m("Rendering control {0} of type RadioButton_t using {1}", control.Id, this.GetType().FullName));

            using (writer.New(WpfXmlWriterTag.RadioButton))
            {
                WpfControlRenderer.WriteGridAttribute(writer, control);

                writer.WriteAttribute(WpfXmlWriterAttribute.Margin, "1,8,4,3");

                if (!string.IsNullOrEmpty(control.Label))
                    writer.WriteAttribute(WpfXmlWriterAttribute.Content, control.Label);

                if (!string.IsNullOrEmpty(control.Id))
                    writer.WriteAttribute(WpfXmlWriterAttribute.Name, id);

                // For .NET 4.0 we can rely on GroupName, but for .NET 3.5 we have to provide our own mechanism to 
                // ensure that only one radio button is enabled at a time
#if NET_40
                if (!string.IsNullOrEmpty(control.RadioGroup))
                    writer.WriteAttribute(WpfXmlWriterAttribute.GroupName, WpfControlRenderer.CleanName(control.RadioGroup));
#endif

                writer.WriteAttribute(WpfXmlWriterAttribute.DataContext, string.Format("{{Binding Path=Controls[{0}]}}", id));

                writer.WriteAttribute(WpfXmlWriterAttribute.ToolTip, "{Binding Path=ToolTip, Mode=OneWay}");
                writer.WriteAttribute(WpfXmlWriterAttribute.IsChecked, "{Binding Path=UiValue, Mode=TwoWay}");
                writer.WriteAttribute(WpfXmlWriterAttribute.IsEnabled, "{Binding Path=Enabled, Mode=OneWay}");
                writer.WriteAttribute(WpfXmlWriterAttribute.Visibility, "{Binding Path=Visibility, Mode=OneWay}");
            }
        }
    }
}
