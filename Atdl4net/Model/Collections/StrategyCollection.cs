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
using System.Collections.ObjectModel;

namespace Atdl4net.Model.Collections
{
    public class StrategyCollection : KeyedCollection<string, Strategy_t>
    {
        private Strategies_t _owner;

        public StrategyCollection(Strategies_t owner)
        {
            _owner = owner;
        }

        protected override string GetKeyForItem(Strategy_t strategy)
        {
            return strategy.Name;
        }

        public new void Add(Strategy_t item)
        {
            item.Parent = _owner;

            base.Add(item);
        }
    }
}
