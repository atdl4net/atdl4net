#region Copyright (c) 2010, Cornerstone Technology Limited. http://atdl4net.org
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
//      License as published by the Free Software Foundation, version 3.
// 
//      Atdl4net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
//      of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU Lesser General Public License for more details.
//
//      You should have received a copy of the GNU Lesser General Public License along with Atdl4net.  If not, see
//      http://www.gnu.org/licenses/.
//
#endregion

using Atdl4net.Model.Elements;
using Atdl4net.Utility;
using System.Collections.ObjectModel;

namespace Atdl4net.Model.Collections
{
    /// <summary>
    /// Collection for storing instances of Control_t.  This class is used at the StrategyPanel level.
    /// </summary>
    /// <remarks>This class maintains an index on each of the Control_t instances that are added to it.  This index is
    /// used when laying out controls on StrategyPanels</remarks>
    public class ControlCollection : ObservableCollection<Control_t>
    {
        private StrategyPanel_t _owner;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlCollection"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        public ControlCollection(StrategyPanel_t owner)
        {
            _owner = owner;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public new void Add(Control_t item)
        {
            (item as IParentable<StrategyPanel_t>).Parent = _owner;

            base.Add(item);

            item.Index = this.Count - 1;
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public new void Remove(Control_t item)
        {
            base.Remove(item);

            RefreshIndexes();
        }

        /// <summary>
        /// Refreshes the indexes.
        /// </summary>
        public void RefreshIndexes()
        {
            int n = 0;

            foreach (Control_t control in Items)
            {
                control.Index = n;

                n++;
            }
        }
    }
}
