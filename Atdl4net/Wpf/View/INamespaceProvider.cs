#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System.Collections.Generic;

namespace Atdl4net.Wpf.View
{
    public interface INamespaceProvider
    {
        Dictionary<string, string> CustomNamespaces { get; }
    }
}
