#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System.ComponentModel.Composition;
using Atdl4net.Configuration;
using Atdl4net.Model.Controls;
using Atdl4net.Wpf.ViewModel;
using Common.Logging;

namespace Atdl4net.Wpf.View.DefaultRendering
{
    [Export(typeof(IWpfControlRenderer<EditableDropDownList_t>))]
    internal class EditableDropDownListRenderer : IWpfControlRenderer<EditableDropDownList_t>
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Wpf.View");

        public void Render(WpfXmlWriter writer, EditableDropDownList_t control)
        {
            string id = WpfControlRenderer.CleanName(control.Id);

            _log.Debug(m => m("Rendering control {0} of type EditableDropDownList_t using {1}", control.Id, this.GetType().FullName));

            if (Atdl4netConfiguration.Settings.Wpf.View.AutoSizeDropDowns)
                WpfControlRenderer.ComboBoxSizer.RegisterComboBox(id, control.ListItems);

            WpfControlRenderer.RenderLabelledControl<EditableDropDownList_t>(writer, control, (c, gridCoordinate) =>
            {
                using (writer.New(WpfXmlWriterTag.ComboBox))
                {
                    writer.WriteAttribute(WpfXmlWriterAttribute.GridColumn, gridCoordinate.Column.ToString());
                    writer.WriteAttribute(WpfXmlWriterAttribute.GridRow, gridCoordinate.Row.ToString());

                    writer.WriteAttribute(WpfXmlWriterAttribute.Margin, "1,3,1,3");

                    if (Atdl4netConfiguration.Settings.Wpf.View.AutoSizeDropDowns)
                        writer.WriteAttribute(WpfXmlWriterAttribute.Width,
                            string.Format("{{Binding Source={{StaticResource {0}}},Path=[{1}]}}", StrategyViewModel.ComboBoxSizerKey, id));

                    writer.WriteAttribute(WpfXmlWriterAttribute.IsEditable, "True");

                    if (!string.IsNullOrEmpty(id))
                        writer.WriteAttribute(WpfXmlWriterAttribute.Name, id);

                    writer.WriteAttribute(WpfXmlWriterAttribute.HorizontalAlignment, "Left");

                    writer.WriteAttribute(WpfXmlWriterAttribute.DataContext, string.Format("{{Binding Path=Controls[{0}]}}", id));

                    writer.WriteAttribute(WpfXmlWriterAttribute.ToolTip, "{Binding Path=ToolTip, Mode=OneWay}");
                    writer.WriteAttribute(WpfXmlWriterAttribute.ItemsSource, "{Binding Path=ListItems}");
                    writer.WriteAttribute(WpfXmlWriterAttribute.SelectedValue, "{Binding Path=SelectedValue}");
                    writer.WriteAttribute(WpfXmlWriterAttribute.SelectedValuePath, "EnumId");
                    writer.WriteAttribute(WpfXmlWriterAttribute.DisplayMemberPath, "UiRep");
                    writer.WriteAttribute(WpfXmlWriterAttribute.Text, "{Binding Path=Text, Mode=TwoWay}");
                    writer.WriteAttribute(WpfXmlWriterAttribute.IsEnabled, "{Binding Path=Enabled, Mode=OneWay}");
                    writer.WriteAttribute(WpfXmlWriterAttribute.Visibility, "{Binding Path=Visibility, Mode=OneWay}");
                }
            });
        }
    }
}
