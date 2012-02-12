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
using System.Xml;

namespace Atdl4net.Wpf.View
{
    public class WpfXmlWriter
    {
        #region Supporting structs (TagInformation, AttributeInformation)

        private struct AttributeInformation
        {
            public readonly string Name;

            public AttributeInformation(string name)
            {
                Name = name;
            }
        }

        private struct TagInformation
        {
            public readonly string Name;
            public readonly string Namespace;

            public TagInformation(string name) : this(name, null) { }

            public TagInformation(string name, string ns)
            {
                Name = name;
                Namespace = ns;
            }
        }

        #endregion Supporting structs (TagInformation, AttributeInformation)

        #region WpfEnclosingTagHelper helper class

        /// <summary>
        /// Helper class to simplify writing xHTML when dealing with tags that open and eventually close (e.g., &lt;table&gt;).
        /// </summary>
        public class WpfEnclosingTagHelper : IDisposable
        {
            private WpfXmlWriter _writer;

            public WpfEnclosingTagHelper(WpfXmlWriter writer, WpfXmlWriterTag tag)
            {
                _writer = writer;

                _writer.WriteBeginTag(tag);
            }

            public WpfEnclosingTagHelper(WpfXmlWriter writer, string prefix, string localName, string ns)
            {
                _writer = writer;

                _writer.WriteBeginTag(prefix, localName, ns);
            }

            public WpfEnclosingTagHelper(WpfXmlWriter writer, string localName, string tag)
            {
                _writer = writer;

                _writer.WriteBeginTag(localName, tag);
            }

            #region IDisposable Members

            protected virtual void Dispose(bool disposing)
            {
                if (disposing && _writer != null)
                {
                    _writer.WriteEndTag();

                    _writer = null;
                }
            }

            public void Dispose()
            {
                Dispose(true);

                GC.SuppressFinalize(this);
            }

            #endregion
        }

        #endregion WpfEnclosingTagHelper helper class

        private readonly XmlWriter _writer;
        private static AttributeInformation[] _attributeInformation;
        private static TagInformation[] _tagInformation;

        static WpfXmlWriter()
        {
            RegisterTags();
            RegisterAttributes();
        }

        public WpfXmlWriter(XmlWriter writer)
        {
            _writer = writer;
        }

        public void WriteBeginTag(string localName, string tag)
        {
            _writer.WriteStartElement(tag, localName);
        }

        public void WriteBeginTag(string prefix, string localName, string ns)
        {
            _writer.WriteStartElement(prefix, localName, ns);
        }

        public void WriteBeginTag(WpfXmlWriterTag tag)
        {
            TagInformation tagInfo = _tagInformation[(int)tag];

            _writer.WriteStartElement(tagInfo.Name, tagInfo.Namespace);
        }

        public void WriteEndTag()
        {
            _writer.WriteEndElement();
        }

        public void WriteAttribute(WpfXmlWriterAttribute attribute, string value)
        {
            AttributeInformation attributeInfo = _attributeInformation[(int)attribute];

            _writer.WriteAttributeString(attributeInfo.Name, value);
        }

        public void WriteAttribute(string attribute, string value)
        {
            _writer.WriteAttributeString(attribute, value);
        }

        public void WriteNamespaceAttribute(string prefix, string uri)
        {
            _writer.WriteAttributeString("xmlns", prefix, null, uri); 
        }

        public IDisposable New(WpfXmlWriterTag tag)
        {
            return new WpfEnclosingTagHelper(this, tag);
        }

        public IDisposable New(string prefix, string tag)
        {
            return new WpfEnclosingTagHelper(this, prefix, tag);
        }

        public IDisposable New(string prefix, string localName, string ns)
        {
            return new WpfEnclosingTagHelper(this, prefix, localName, ns);
        }

        protected static void RegisterTags()
        {
            _tagInformation = new TagInformation[Enum.GetValues(typeof(WpfXmlWriterTag)).Length];

            _tagInformation[(int)WpfXmlWriterTag.CheckBox] = new TagInformation("CheckBox");
            _tagInformation[(int)WpfXmlWriterTag.CheckBoxList] = new TagInformation("CheckBoxList", "atdl");
            _tagInformation[(int)WpfXmlWriterTag.Clock] =  new TagInformation("Clock", "atdl");
            _tagInformation[(int)WpfXmlWriterTag.StrategyPanelFrame] = new TagInformation("StrategyPanelFrame", "atdl4net");
            _tagInformation[(int)WpfXmlWriterTag.GroupBox] = new TagInformation("GroupBox");
            _tagInformation[(int)WpfXmlWriterTag.ColumnDefinition] = new TagInformation("ColumnDefinition");
            _tagInformation[(int)WpfXmlWriterTag.ComboBox] = new TagInformation("ComboBox");
            _tagInformation[(int)WpfXmlWriterTag.DoubleSpinner] = new TagInformation("DoubleSpinner", "atdl");
            _tagInformation[(int)WpfXmlWriterTag.Grid] = new TagInformation("Grid");
            _tagInformation[(int)WpfXmlWriterTag.GridColumnDefinitions] = new TagInformation("Grid.ColumnDefinitions");
            _tagInformation[(int)WpfXmlWriterTag.GridRowDefinitions] = new TagInformation("Grid.RowDefinitions");
            _tagInformation[(int)WpfXmlWriterTag.Label] = new TagInformation("Label");
            _tagInformation[(int)WpfXmlWriterTag.ListBox] = new TagInformation("ListBox");
            _tagInformation[(int)WpfXmlWriterTag.RadioButton] = new TagInformation("RadioButton");
            _tagInformation[(int)WpfXmlWriterTag.RadioButtonList] = new TagInformation("RadioButtonList", "atdl");
            _tagInformation[(int)WpfXmlWriterTag.Rectangle] = new TagInformation("Rectangle");
            _tagInformation[(int)WpfXmlWriterTag.RowDefinition] = new TagInformation("RowDefinition");
            _tagInformation[(int)WpfXmlWriterTag.SingleSpinner] = new TagInformation("SingleSpinner", "atdl");
            _tagInformation[(int)WpfXmlWriterTag.Slider] = new TagInformation("Slider", "atdl");
            _tagInformation[(int)WpfXmlWriterTag.StackPanel] = new TagInformation("StackPanel");
            _tagInformation[(int)WpfXmlWriterTag.TextBox] = new TagInformation("TextBox");
        }

        protected static void RegisterAttributes()
        {
            _attributeInformation = new AttributeInformation[Enum.GetValues(typeof(WpfXmlWriterAttribute)).Length];

            _attributeInformation[(int)WpfXmlWriterAttribute.AutomationProperties_AutomationId] = new AttributeInformation("AutomationProperties.AutomationId");
            _attributeInformation[(int)WpfXmlWriterAttribute.BorderThickness] = new AttributeInformation("BorderThickness");
            _attributeInformation[(int)WpfXmlWriterAttribute.BorderVisibility] = new AttributeInformation("BorderVisibility");
            _attributeInformation[(int)WpfXmlWriterAttribute.CollapseButtonVisibility] = new AttributeInformation("CollapseButtonVisibility");
            _attributeInformation[(int)WpfXmlWriterAttribute.Content] = new AttributeInformation("Content");
            _attributeInformation[(int)WpfXmlWriterAttribute.DataContext] = new AttributeInformation("DataContext");
            _attributeInformation[(int)WpfXmlWriterAttribute.DisplayMemberPath] = new AttributeInformation("DisplayMemberPath");
            _attributeInformation[(int)WpfXmlWriterAttribute.GridColumn] = new AttributeInformation("Grid.Column");
            _attributeInformation[(int)WpfXmlWriterAttribute.GridRow] = new AttributeInformation("Grid.Row");
            _attributeInformation[(int)WpfXmlWriterAttribute.GroupName] = new AttributeInformation("GroupName");
            _attributeInformation[(int)WpfXmlWriterAttribute.Header] = new AttributeInformation("Header");
            _attributeInformation[(int)WpfXmlWriterAttribute.HeaderVisibility] = new AttributeInformation("HeaderVisibility");
            _attributeInformation[(int)WpfXmlWriterAttribute.Height] = new AttributeInformation("Height");
            _attributeInformation[(int)WpfXmlWriterAttribute.HorizontalAlignment] = new AttributeInformation("HorizontalAlignment");
            _attributeInformation[(int)WpfXmlWriterAttribute.Increment] = new AttributeInformation("Increment");
            _attributeInformation[(int)WpfXmlWriterAttribute.InnerIncrement] = new AttributeInformation("InnerIncrement");
            _attributeInformation[(int)WpfXmlWriterAttribute.IsChecked] = new AttributeInformation("IsChecked");
            _attributeInformation[(int)WpfXmlWriterAttribute.IsContentValid] = new AttributeInformation("IsContentValid");
            _attributeInformation[(int)WpfXmlWriterAttribute.IsEditable] = new AttributeInformation("IsEditable");
            _attributeInformation[(int)WpfXmlWriterAttribute.IsEnabled] = new AttributeInformation("IsEnabled");
            _attributeInformation[(int)WpfXmlWriterAttribute.IsExpanded] = new AttributeInformation("IsExpanded");
            _attributeInformation[(int)WpfXmlWriterAttribute.ItemContainerStyle] = new AttributeInformation("ItemContainerStyle");
            _attributeInformation[(int)WpfXmlWriterAttribute.ItemsSource] = new AttributeInformation("ItemsSource");
            _attributeInformation[(int)WpfXmlWriterAttribute.Margin] = new AttributeInformation("Margin");
            _attributeInformation[(int)WpfXmlWriterAttribute.Name] = new AttributeInformation("Name");
            _attributeInformation[(int)WpfXmlWriterAttribute.Orientation] = new AttributeInformation("Orientation");
            _attributeInformation[(int)WpfXmlWriterAttribute.OuterIncrement] = new AttributeInformation("OuterIncrement");
            _attributeInformation[(int)WpfXmlWriterAttribute.Padding] = new AttributeInformation("Padding");
            _attributeInformation[(int)WpfXmlWriterAttribute.SelectedItem] = new AttributeInformation("SelectedItem");
            _attributeInformation[(int)WpfXmlWriterAttribute.SelectedValue] = new AttributeInformation("SelectedValue");
            _attributeInformation[(int)WpfXmlWriterAttribute.SelectedValuePath] = new AttributeInformation("SelectedValuePath");
            _attributeInformation[(int)WpfXmlWriterAttribute.SelectionMode] = new AttributeInformation("SelectionMode");
            _attributeInformation[(int)WpfXmlWriterAttribute.Target] = new AttributeInformation("Target");
            _attributeInformation[(int)WpfXmlWriterAttribute.Text] = new AttributeInformation("Text");
            _attributeInformation[(int)WpfXmlWriterAttribute.ToolTip] = new AttributeInformation("ToolTip");
            _attributeInformation[(int)WpfXmlWriterAttribute.Time] = new AttributeInformation("Time");
            _attributeInformation[(int)WpfXmlWriterAttribute.Value] = new AttributeInformation("Value");
            _attributeInformation[(int)WpfXmlWriterAttribute.VirtualizingStackPanel_IsVirtualizing] = new AttributeInformation("VirtualizingStackPanel.IsVirtualizing");
            _attributeInformation[(int)WpfXmlWriterAttribute.Visibility] = new AttributeInformation("Visibility");
            _attributeInformation[(int)WpfXmlWriterAttribute.Width] = new AttributeInformation("Width");
        }
    }
}
