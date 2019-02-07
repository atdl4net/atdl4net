#region Copyright (c) 2010-2012, Cornerstone Technology Limited. http://atdl4net.org
//
//   This software is released under both commercial and open-source licenses.
//
//   If you received this software under the commercial license, the terms of that license can be found in the
//   Commercial.txt file in the Licenses folder.  If you received this software under the open-source license,
//   the following applies:
//
//      This file is part of Atdl4net.
//
//      Atdl4net is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public 
//      License as published by the Free Software Foundation, either version 2.1 of the License, or (at your option) any later version.
// 
//      Atdl4net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
//      of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU Lesser General Public License for more details.
//
//      You should have received a copy of the GNU Lesser General Public License along with Atdl4net.  If not, see
//      http://www.gnu.org/licenses/.
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
