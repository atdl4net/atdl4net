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
