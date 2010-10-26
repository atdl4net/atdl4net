#region Copyright (c) 2010, Cornerstone Technology Limited. http://atdl4net.org
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
//      License as published by the Free Software Foundation, version 3.
// 
//      Atdl4net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
//      of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU Lesser General Public License for more details.
//
//      You should have received a copy of the GNU Lesser General Public License along with Atdl4net.  If not, see
//      http://www.gnu.org/licenses/.
//
#endregion

using System;
using System.Xml.Linq;

namespace Atdl4net.Xml.Serialization
{
    public class ElementDefinition
    {
        public XName ElementName { get; set; }
        public Type TargetType { get; private set; }
        public ElementAttribute[] Attributes { get; private set; }
        public ConstructorParameter[] ConstructorParameters { get; private set; }
        public ChildElementDefinition[] ChildElements { get; private set; }
        public CacheElementValueInstruction CacheElementValueInstruction { get; private set; }

        public ElementDefinition(XName elementName, Type targetType, ElementAttribute[] attributes)
            : this(elementName, targetType, null, attributes, new ChildElementDefinition[] { }, null)
        {
        }

        public ElementDefinition(XName elementName, Type targetType, ElementAttribute[] attributes, ChildElementDefinition child)
            : this(elementName, targetType, null, attributes, new ChildElementDefinition[] { child }, null)
        {
        }

        public ElementDefinition(XName elementName, Type targetType, ElementAttribute[] attributes, ChildElementDefinition[] children)
            : this(elementName, targetType, null, attributes, children, null)
        {
        }

        public ElementDefinition(XName elementName, Type targetType, ElementAttribute[] attributes, ChildElementDefinition[] children,
            CacheElementValueInstruction cacheInstruction)
            : this(elementName, targetType, null, attributes, children, cacheInstruction)
        {
        }

        public ElementDefinition(XName elementName, Type targetType, ConstructorParameter[] constructorParameters, ElementAttribute[] attributes)
            : this(elementName, targetType, constructorParameters, attributes, new ChildElementDefinition[] { }, null)
        {
        }

        public ElementDefinition(XName elementName, Type targetType, ConstructorParameter[] constructorParameters, ElementAttribute[] attributes, 
            ChildElementDefinition child)
            : this(elementName, targetType, constructorParameters, attributes, new ChildElementDefinition[] { child }, null)
        {
        }

        public ElementDefinition(XName elementName, Type targetType, ConstructorParameter[] constructorParameters,
            ElementAttribute[] attributes, ChildElementDefinition[] children)
            : this(elementName, targetType, constructorParameters, attributes, children, null)
        {
        }

        public ElementDefinition(XName elementName, Type targetType, ConstructorParameter[] constructorParameters,
            ElementAttribute[] attributes, ChildElementDefinition[] children, CacheElementValueInstruction cacheInstruction)
        {
            ElementName = elementName;
            TargetType = targetType;
            ConstructorParameters = constructorParameters;
            Attributes = attributes;
            ChildElements = children;
            CacheElementValueInstruction = cacheInstruction;
        }
    }
}