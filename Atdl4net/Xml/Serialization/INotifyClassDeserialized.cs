#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;

namespace Atdl4net.Xml.Serialization
{
    public interface INotifyClassDeserialized
    {
        event EventHandler<ClassDeserializedEventArgs> ClassDeserialized;
    }
}
