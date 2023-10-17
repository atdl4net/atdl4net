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
    public class MultiTypeElementDefinition : ElementDefinition
    {
        public XName AttributeForType { get; private set; }
        public string TypeNamespace { get; private set; }
        public Dictionary<Type, ElementAttribute[]> TypeToAttributesMap { get; private set; }

        public MultiTypeElementDefinition(XName elementName, XName attributeForType, string typeNamespace, 
            ConstructorParameter[] constructorParameters, ElementAttribute[] commonAttributes,
            Dictionary<Type, ElementAttribute[]> attributeDictionary, ChildElementDefinition[] children)
            : base(elementName, null, constructorParameters, commonAttributes, children)
        {
            AttributeForType = attributeForType;
            TypeNamespace = typeNamespace;
            TypeToAttributesMap = attributeDictionary;
        }
    }
}
