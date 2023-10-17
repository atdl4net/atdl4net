#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using Atdl4net.Model.Collections;

namespace Atdl4net.Utility
{
    public interface IResolvable<Thost, Tvaluesource>
    {
        void Resolve(Thost host, ISimpleDictionary<Tvaluesource> sourceCollection);
    }
}
