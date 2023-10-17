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
    public class GenericTypeElementDefinition : ElementDefinition
    {
        public XName AttributeForInnerType { get; private set; }
        public string InnerTypeNamespace { get; private set; }
        public Dictionary<Type, ElementAttribute[]> InnerTypeToAttributesMap { get; private set; }

        public GenericTypeElementDefinition(XName elementName, Type outerType, XName attributeForInnerType,
            string innerTypeNamespace, ConstructorParameter[] constructorParameters, ElementAttribute[] commonAttributes,
            Dictionary<Type, ElementAttribute[]> attributeDictionary, ChildElementDefinition[] children)
            : base(elementName, outerType, constructorParameters, commonAttributes, children)
        {
            AttributeForInnerType = attributeForInnerType;
            InnerTypeNamespace = innerTypeNamespace;
            InnerTypeToAttributesMap = attributeDictionary;
        }
    }
}
