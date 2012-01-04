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

using Atdl4net.Diagnostics;
using Atdl4net.Model.Elements;
using Atdl4net.Model.Enumerations;
using Atdl4net.Model.Reference;
using Atdl4net.Resources;
using System;
using System.Collections.Generic;

namespace Atdl4net.Model.Collections
{
    /// <summary>
    /// Collection for storing instances of Country_t.
    /// </summary>
    public class CountryCollection : HashSet<Country_t>
    {
        Region _region;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryCollection"/> class.
        /// </summary>
        /// <param name="region">The region.</param>
        public CountryCollection(Region_t region)
        {
            _region = region.Name;
        }

        /// <summary>
        /// Adds the specified item, validating that it belongs in the region specified when this collection was created.
        /// </summary>
        /// <param name="item">The item.</param>
        public new void Add(Country_t item)
        {
            bool countryOkay = false;

            switch (_region)
            {
                case Region.AsiaPacificJapan:
                    countryOkay = Regions.AsiaPacificJapanCountries.Contains(item.CountryCode);
                    break;

                case Region.EuropeMiddleEastAfrica:
                    countryOkay = Regions.EuropeMiddleEastAfricaCountries.Contains(item.CountryCode);
                    break;

                case Region.TheAmericas:
                    countryOkay = Regions.TheAmericasCountries.Contains(item.CountryCode);
                    break;
            }

            if (!countryOkay)
                throw ThrowHelper.New<ArgumentException>(this, ErrorMessages.InvalidAttemptToAddCountryToRegion,
                    Enum.GetName(typeof(IsoCountryCode), item.CountryCode), Enum.GetName(typeof(Region), _region));

            base.Add(item);
        }
    }
}
