#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

namespace Atdl4net.Utility
{
    public interface IParentable<T>
    {
        T Parent { get; set; }
    }
}
