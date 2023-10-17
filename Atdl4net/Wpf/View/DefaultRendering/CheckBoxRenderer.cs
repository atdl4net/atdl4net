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
    [Export(typeof(IWpfControlRenderer<CheckBox_t>))]
    internal class CheckBoxRenderer : IWpfControlRenderer<CheckBox_t>
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Wpf.View");

        public void Render(WpfXmlWriter writer, CheckBox_t control)
        {
            string id = WpfControlRenderer.CleanName(control.Id);

            _log.Debug(m => m("Rendering control {0} of type CheckBox_t using {1}", control.Id, this.GetType().FullName));

            using (writer.New(WpfXmlWriterTag.CheckBox))
            {
                WpfControlRenderer.WriteGridAttribute(writer, control);

//                writer.WriteAttribute(WpfXmlWriterAttribute.Margin, "1,5,2,0");
                writer.WriteAttribute(WpfXmlWriterAttribute.Margin, "1,8,2,3");

                if (!string.IsNullOrEmpty(control.Label))
                    writer.WriteAttribute(WpfXmlWriterAttribute.Content, control.Label);

                if (!string.IsNullOrEmpty(control.Id))
                    writer.WriteAttribute(WpfXmlWriterAttribute.Name, id);

                writer.WriteAttribute(WpfXmlWriterAttribute.DataContext, string.Format("{{Binding Path=Controls[{0}]}}", id));

                writer.WriteAttribute(WpfXmlWriterAttribute.ToolTip, "{Binding Path=ToolTip, Mode=OneWay}");
                writer.WriteAttribute(WpfXmlWriterAttribute.IsChecked, "{Binding Path=UiValue, Mode=TwoWay}");
                writer.WriteAttribute(WpfXmlWriterAttribute.IsEnabled, "{Binding Path=Enabled, Mode=OneWay}");
                writer.WriteAttribute(WpfXmlWriterAttribute.Visibility, "{Binding Path=Visibility, Mode=OneWay}");
            }
        }
    }
}
