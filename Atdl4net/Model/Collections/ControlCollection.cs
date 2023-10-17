#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
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
        private readonly StrategyPanel_t _owner;

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
