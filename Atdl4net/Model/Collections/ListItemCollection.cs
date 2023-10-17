#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Model.Elements;
using Atdl4net.Resources;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Collections
{
    public class ListItemCollection : KeyedCollection<string, ListItem_t>
    {
        public void CopyFrom(List<ListItem_t> items)
        {
            foreach (ListItem_t item in items)
                Add(item);
        }

        public new void Add(ListItem_t item)
        {
            try
            {
                base.Add(item);
            }
            catch (ArgumentException ex)
            {
                throw ThrowHelper.New<DuplicateKeyException>(this, ex, ErrorMessages.AttemptToAddDuplicateKey,
                    item.EnumId, "ListItems");
            }
        }

        public string[] EnumIds
        {
            get { return (from item in Items select item.EnumId).ToArray<string>(); }
        }

        public bool HasItems
        {
            get { return Count > 0; }
        }

        protected override string GetKeyForItem(ListItem_t item)
        {
            return item.EnumId;
        }
    }
}
