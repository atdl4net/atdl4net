#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;

namespace Atdl4net.Xml.Serialization
{
    public class ClassDeserializedEventArgs : EventArgs
    {
        public Type ClassType { get; private set; }
        public object ExtraInfo { get; private set; }

        public ClassDeserializedEventArgs(Type createdType, object extraInfo)
        {
            ClassType = createdType;
            ExtraInfo = extraInfo;
        }
    }
}