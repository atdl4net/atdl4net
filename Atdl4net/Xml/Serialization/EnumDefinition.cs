#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Collections.Generic;

namespace Atdl4net.Xml.Serialization
{
    public class EnumDefinition
    {
        public Type EnumType { get; private set; }
        public Dictionary<string, Enum> TextValues { get; private set; }

        public EnumDefinition(Type enumType, Dictionary<string, Enum> textValues)
        {
            EnumType = enumType;
            TextValues = textValues;
        }
    }
}