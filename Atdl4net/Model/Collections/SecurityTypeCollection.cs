#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using Atdl4net.Model.Elements;
using System.Collections.ObjectModel;

namespace Atdl4net.Model.Collections
{
    public class SecurityTypeCollection : KeyedCollection<string, SecurityType_t>
    {
        protected override string GetKeyForItem(SecurityType_t item)
        {
            return item.Name;
        }
    }
}
