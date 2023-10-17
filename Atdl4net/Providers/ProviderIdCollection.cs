#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Atdl4net.Providers
{
    public class ProviderIdCollection : Collection<string>
    {
        public ProviderIdCollection()
        {
        }

        public ProviderIdCollection(IList<string> list)
            : base(list)
        {
        }
    }
}
