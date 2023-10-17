#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Xml.Linq;

namespace Atdl4net.Xml.Serialization
{
    public class ChildElementDefinition
    {
        public ElementDefinition ElementDefinition { get; private set; }
        public XName ContainerElementName { get; private set; }
        public string ContainerProperty { get; private set; }
        public Type ContainerPropertyType { get; private set; }
        public object ContainerMethod { get; private set; }

        public ChildElementDefinition(ContainerElementDefinition containerElementDefinition, string containerProperty, Type containerPropertyType, StandardContainerMethod containerMethod)
        {
            ContainerElementName = containerElementDefinition.ElementName;
            ElementDefinition = containerElementDefinition.ChildDefinition;
            ContainerProperty = containerProperty;
            ContainerPropertyType = containerPropertyType;
            ContainerMethod = containerMethod;
        }

        public ChildElementDefinition(ElementDefinition elementDefinition, string containerProperty, Type containerPropertyType, StandardContainerMethod containerMethod)
        {
            ElementDefinition = elementDefinition;
            ContainerProperty = containerProperty;
            ContainerPropertyType = containerPropertyType;
            ContainerMethod = containerMethod;
        }

        public ChildElementDefinition(ElementDefinition elementDefinition, string containerProperty, Type containerPropertyType, string containerMethod)
        {
            ElementDefinition = elementDefinition;
            ContainerProperty = containerProperty;
            ContainerPropertyType = containerPropertyType;
            ContainerMethod = containerMethod;
        }
    }
}