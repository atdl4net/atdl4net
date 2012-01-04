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

using Atdl4net.Model.Enumerations;

namespace Atdl4net.Model.Elements
{
    /// <summary>
    /// Represents a FIXatdl Market_t.<br/>
    /// Defines a particular market using a market identifier code (MIC). An attribute, inclusion, determines whether the market 
    /// should be included or excluded from the list of markets created by the patterned element, Markets.
    /// </summary>
    public class Market_t
    {
        /// <summary>
        /// Indicates whether this market should be included or excluded from encompassing list. 
        /// Valid values: are “Include”, “Exclude”.
        /// </summary>
        public Inclusion_t Inclusion { get; set; }

        /// <summary>
        /// String representing a market or exchange - ISO 10383 Market Identifier Code (MIC).
        /// </summary>
        public string MICCode { get; set; }
    }
}
