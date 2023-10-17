#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
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
