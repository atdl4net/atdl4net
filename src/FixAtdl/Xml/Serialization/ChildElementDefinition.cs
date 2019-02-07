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