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
using Atdl4net.Model.Enumerations;
using System.Collections.ObjectModel;

namespace Atdl4net.Model.Collections
{
    public class RegionCollection : KeyedCollection<Region, Region_t>
    {
        protected override Region GetKeyForItem(Region_t region)
        {
            return region.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>If no region information is provided, Atdl4net assumes that the strategy is applicable to all regions.</remarks>
        public Region GetApplicableRegions()
        {
            if (Count == 0)
                return Region.AsiaPacificJapan | Region.EuropeMiddleEastAfrica | Region.TheAmericas;
            else
            {
                Region applicableRegions = Region.None;

                foreach (Region_t region in Items)
                {
                    if (region.Inclusion == Inclusion_t.Include)
                        applicableRegions |= region.Name;
                }

                return applicableRegions;
            }
        }
    }
}
