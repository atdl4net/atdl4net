#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System.ComponentModel.Composition;
using Atdl4net.Model.Controls;
using Atdl4net.Wpf.View.Controls;
using Common.Logging;

namespace Atdl4net.Wpf.View.DefaultRendering
{
    [Export(typeof(IWpfControlRenderer<SingleSpinner_t>))]
    internal class SingleSpinnerRenderer : IWpfControlRenderer<SingleSpinner_t>
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Wpf.View");

        public void Render(WpfXmlWriter writer, SingleSpinner_t control)
        {
            string id = WpfControlRenderer.CleanName(control.Id);

            _log.Debug(m => m("Rendering control {0} of type SingleSpinner_t using {1}", control.Id, this.GetType().FullName));

            WpfControlRenderer.RenderLabelledControl<SingleSpinner_t>(writer, control, (c, gridCoordinate) =>
            {
                using (writer.New(DefaultNamespaceProvider.Atdl4netNamespaceUri, typeof(SingleSpinner).Name))
                {
                    writer.WriteAttribute(WpfXmlWriterAttribute.GridColumn, gridCoordinate.Column.ToString());
                    writer.WriteAttribute(WpfXmlWriterAttribute.GridRow, gridCoordinate.Row.ToString());

                    writer.WriteAttribute(WpfXmlWriterAttribute.Margin, "1,3,3,3");

                    writer.WriteAttribute(WpfXmlWriterAttribute.Width, "120");
                    writer.WriteAttribute(WpfXmlWriterAttribute.HorizontalAlignment, "Left");

                    if (!string.IsNullOrEmpty(c.Id))
                        writer.WriteAttribute(WpfXmlWriterAttribute.Name, id);

                    writer.WriteAttribute(WpfXmlWriterAttribute.DataContext, string.Format("{{Binding Path=Controls[{0}]}}", id));

                    writer.WriteAttribute(WpfXmlWriterAttribute.ToolTip, "{Binding Path=ToolTip, Mode=OneWay}");
                    writer.WriteAttribute(WpfXmlWriterAttribute.Value, "{Binding Path=UiValue, Mode=TwoWay, TargetNullValue={x:Static sys:String.Empty}}");
                    writer.WriteAttribute(WpfXmlWriterAttribute.Increment, "{Binding Path=UnderlyingControl.Increment, Mode=OneWay, TargetNullValue=1}");
                    writer.WriteAttribute(WpfXmlWriterAttribute.IsEnabled, "{Binding Path=Enabled, Mode=OneWay}");
                    writer.WriteAttribute(WpfXmlWriterAttribute.Visibility, "{Binding Path=Visibility, Mode=OneWay}");
                    writer.WriteAttribute(WpfXmlWriterAttribute.IsContentValid, "{Binding Path=IsContentValid, Mode=OneWayToSource}");
                }
            });
        }
    }
}
