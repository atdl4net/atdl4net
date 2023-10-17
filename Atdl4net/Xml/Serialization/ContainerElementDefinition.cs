#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Xml.Linq;

namespace Atdl4net.Xml.Serialization
{
    public class ContainerElementDefinition : ElementDefinition
    {
        public ElementDefinition ChildDefinition { get; private set; }

        public ContainerElementDefinition(XName elementName, ElementDefinition childDefinition)
            : base(elementName, null, null, null, null, null)
        {
            ChildDefinition = childDefinition;
        }
    }
}
