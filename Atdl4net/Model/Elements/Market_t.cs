#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
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
