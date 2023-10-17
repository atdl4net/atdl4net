#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Atdl4net.Xml.Serialization
{
    public enum Required
    {
        Mandatory,
        Optional
    }

    public class ElementAttribute
    {
        public XName XmlName { get; private set; }
        public string Property { get; private set; }
        public Type Type { get; private set; }
        public Dictionary<string, Enum> EnumValues { get; private set; }
        public Required Required { get; private set; }

        public ElementAttribute(string xmlName, string property, Type type, Required required)
        {
            XmlName = xmlName;
            Property = property;
            Required = required;
            Type = type;
        }

        public ElementAttribute(string xmlName, string property, EnumDefinition enumDefinition, Required required)
        {
            XmlName = xmlName;
            Property = property;
            Required = required;
            Type = enumDefinition.EnumType;
            EnumValues = enumDefinition.TextValues;
        }
    }
}