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

using System.Collections.ObjectModel;
using Atdl4net.Model.Elements;
using Atdl4net.Model.Enumerations;
using Atdl4net.Model.Reference;

namespace Atdl4net.Model.Collections
{
    public class RegionCollection : KeyedCollection<Region, Region_t>
    {
        protected override Region GetKeyForItem(Region_t region)
        {
            return region.Name;
        }

        /// <summary>
        /// Gets a bitmask of the applicable regions for a given strategy.
        /// </summary>
        /// <returns>A bitmask of applicable Regions.</returns>
        /// <remarks>If no region information is provided, Atdl4net assumes that the strategy is applicable to all regions.</remarks>
        public Region GetApplicableRegions()
        {
            if (Count == 0)
                return Region.All;
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

        /// <summary>
        /// Determines whether a given strategy is applicable for a specific country.
        /// </summary>
        /// <param name="country">Country to check for.</param>
        /// <returns>true if the strategy is applicable for the specified country; false otherwise.</returns>
        public bool IsApplicableTo(IsoCountryCode country)
        {
            Region applicableRegions = GetApplicableRegions();

            Region targetRegion = Regions.GetRegionForCountry(country);

            return (applicableRegions & targetRegion) != 0;
        }
    }
}
