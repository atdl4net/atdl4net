#region Copyright (c) 2010-2012, Cornerstone Technology Limited. http://atdl4net.org
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

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Elements;
using Atdl4net.Model.Enumerations;
using Atdl4net.Resources;
using Atdl4net.Utility;
using Atdl4net.Wpf.View.DefaultRendering;
using Atdl4net.Wpf.ViewModel;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Wpf.View
{
    public static class WpfStrategyPanelRenderer
    {
        public const string ExceptionContext = "WpfStrategyPanelRenderer";

        public static readonly string AtdlDataContext = string.Format("{0}StaticResource {1}{2}", "{", StrategyViewModel.DataContextKey, "}");
        public static readonly string CollapsedVisibility = Enum.GetName(typeof(Visibility), Visibility.Collapsed);
        public static readonly string VisibleVisibility = Enum.GetName(typeof(Visibility), Visibility.Visible);
        private static readonly Type[] _defaultRenderers = new Type[] {
                typeof(DefaultRendering.DefaultNamespaceProvider),
                typeof(DefaultRendering.CheckBoxListRenderer),
                typeof(DefaultRendering.CheckBoxRenderer),
                typeof(DefaultRendering.ClockRenderer),
                typeof(DefaultRendering.DoubleSpinnerRenderer),
                typeof(DefaultRendering.DropDownListRenderer),
                typeof(DefaultRendering.EditableDropDownListRenderer),
                typeof(DefaultRendering.HiddenFieldRenderer),
                typeof(DefaultRendering.LabelRenderer),
                typeof(DefaultRendering.MultiSelectListRenderer),
                typeof(DefaultRendering.RadioButtonListRenderer),
                typeof(DefaultRendering.RadioButtonRenderer),
                typeof(DefaultRendering.SingleSelectListRenderer),
                typeof(DefaultRendering.SingleSpinnerRenderer),
                typeof(DefaultRendering.SliderRenderer),
                typeof(DefaultRendering.TextFieldRenderer) };

        public static string CustomControlRenderer { get; set; }

        public static void Render(Strategy_t strategy, XmlWriter writer, WpfComboBoxSizer sizer)
        {
            if (strategy.StrategyLayout == null)
                throw ThrowHelper.New<RenderingException>(ExceptionContext, ErrorMessages.NoStrategyLayoutSupplied);

            StrategyPanel_t rootPanel = strategy.StrategyLayout.StrategyPanel;

            if (rootPanel == null)
                throw ThrowHelper.New<RenderingException>(ExceptionContext, ErrorMessages.NoStrategyPanelsInStrategy);

            WpfXmlWriter wpfWriter = new WpfXmlWriter(writer);

            // TODO: Move this somewhere better
            WpfControlRenderer controlRenderer = new WpfControlRenderer(wpfWriter, sizer);

            // TODO: Move this elsewhere

            CompositionContainer defaultContainer = new CompositionContainer(new TypeCatalog(_defaultRenderers));

            if (!string.IsNullOrEmpty(CustomControlRenderer))
            {
                string applicationDirectory = (from assembly in System.AppDomain.CurrentDomain.GetAssemblies()
                                               where assembly.CodeBase.EndsWith(".exe")
                                               select System.IO.Path.GetDirectoryName(assembly.CodeBase.Replace("file:///", ""))).FirstOrDefault();

                string customControlRendererPath = Path.Combine(applicationDirectory, CustomControlRenderer);

                AssemblyCatalog overridesCatalog = new AssemblyCatalog(customControlRendererPath);

                CompositionContainer aggregateContainer = new CompositionContainer(overridesCatalog, defaultContainer);

                aggregateContainer.ComposeParts(controlRenderer);
            }
            else
                defaultContainer.ComposeParts(controlRenderer);

            int depth = 0;

            ProcessPanel(rootPanel, wpfWriter, controlRenderer, -1, ref depth);
        }

        private static void ProcessPanel(StrategyPanel_t panel, WpfXmlWriter writer, WpfControlRenderer controlRenderer, int rowOrColumn, ref int depth)
        {
            depth++;

            bool isVertical = (panel.Orientation == Orientation_t.Vertical);

            using (writer.New(DefaultNamespaceProvider.Atdl4netNamespace, "StrategyPanelFrame", DefaultNamespaceProvider.Atdl4netNamespaceUri))
            {
                writer.WriteAttribute(WpfXmlWriterAttribute.Padding, "1");
                writer.WriteAttribute(WpfXmlWriterAttribute.Margin, "1");

                WritePanelAttributes(writer, panel);

                if (rowOrColumn == -1)
                {
                    foreach (KeyValuePair<string, string> ns in controlRenderer.NamespaceProvider.CustomNamespaces)
                        writer.WriteNamespaceAttribute(ns.Key, ns.Value);
                }
                else
                    writer.WriteAttribute((panel as IParentable<StrategyPanel_t>).Parent.Orientation == Orientation_t.Vertical
                        ? WpfXmlWriterAttribute.GridRow
                        : WpfXmlWriterAttribute.GridColumn,
                        rowOrColumn.ToString());

                bool containsControls = (panel.Controls.Count > 0);

                // For grids containing a horizontal arrangement of controls, we add an empty column so we can set its width to "*"
                int childCount = containsControls 
                    ? panel.Controls.Count + (isVertical ? 0 : 1)
                    : panel.StrategyPanels.Count;

                using (writer.New(WpfXmlWriterTag.Grid))
                {
                    if (depth == 1)
                        writer.WriteAttribute(WpfXmlWriterAttribute.DataContext, AtdlDataContext);

                    using (writer.New(WpfXmlWriterTag.GridRowDefinitions))
                    {
                        int rowCount = isVertical ? childCount : 1;

                        for (int n = 0; n < rowCount; n++)
                        {
                            using (writer.New(WpfXmlWriterTag.RowDefinition))
                                    writer.WriteAttribute(WpfXmlWriterAttribute.Height, "Auto");
                        }
                    }

                    using (writer.New(WpfXmlWriterTag.GridColumnDefinitions))
                    {
                        // Special treatment for vertical panels that contain controls - put in two columns, one for the label and 
                        // one for the control itself.
                        int columnCount = isVertical ? (containsControls ? 2 : 1) : childCount;

                        for (int n = 0; n < columnCount; n++)
                        {
                            using (writer.New(WpfXmlWriterTag.ColumnDefinition))
                            {
                                if (containsControls)
                                    writer.WriteAttribute(WpfXmlWriterAttribute.Width, (n < childCount - 1) ? "Auto" : "*");
                            }
                        }
                    }

                    // Note that a StrategyPanel_t can either contain other strategy panels, or controls but NOT BOTH.
                    if (panel.StrategyPanels != null && panel.StrategyPanels.Count > 0)
                    {
                        int thisRowOrColumn = 0;

                        foreach (StrategyPanel_t childPanel in panel.StrategyPanels)
                        {
                            ProcessPanel(childPanel, writer, controlRenderer, thisRowOrColumn, ref depth);

                            thisRowOrColumn++;
                        }
                    }
                    else
                    {
                        ProcessControls(panel, controlRenderer);

                        // For horizontal strategy panels, put a dummy rectangle in the last column to trick
                        // WPF to sizing the other columns to their control size.
                        if (panel.Orientation == Orientation_t.Horizontal)
                            using (writer.New(WpfXmlWriterTag.Rectangle))
                            {
                                writer.WriteAttribute(WpfXmlWriterAttribute.GridColumn, panel.Controls.Count.ToString());
                            }
                    }
                }
            }
        }

        private static void ProcessControls(StrategyPanel_t panel, WpfControlRenderer renderer)
        {
            ControlCollection controls = panel.Controls;

            if (controls == null || controls.Count == 0)
                return;

            foreach (var control in controls)
            {
                renderer.ProcessControl(control);
            }
        }

        private static void WritePanelAttributes(WpfXmlWriter writer, StrategyPanel_t panel)
        {
            writer.WriteAttribute(WpfXmlWriterAttribute.BorderVisibility,
                (panel.Border == Border_t.Line) ? VisibleVisibility : CollapsedVisibility);
            writer.WriteAttribute(WpfXmlWriterAttribute.HeaderVisibility,
                string.IsNullOrEmpty(panel.Title) ? CollapsedVisibility : VisibleVisibility);
            writer.WriteAttribute(WpfXmlWriterAttribute.CollapseButtonVisibility,
                (panel.Collapsible == true) ? VisibleVisibility : CollapsedVisibility);
            writer.WriteAttribute(WpfXmlWriterAttribute.IsExpanded,
                (panel.Collapsible == true && panel.Collapsed == true) ? "False" : "True");

            if (!string.IsNullOrEmpty(panel.Title))
                writer.WriteAttribute(WpfXmlWriterAttribute.Header, panel.Title);
        }
    }
}
