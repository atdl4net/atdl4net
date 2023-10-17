#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

namespace Atdl4net.Model.Elements
{
    /// <summary>
    /// Represents a FIXatdl EnumPair_t.<br/>
    /// Defines a legal value of a parameter in the form of a wire value. A Parameter element will have an EnumPair element for each
    /// enumerated value which the parameter can take.
    /// </summary>
    public class EnumPair_t
    {
        /// <summary>A unique identifier of an enumPair element per parameter.</summary>
        public string EnumId { get; set; }

        /// <summary>The corresponding value that is used to populate the FIX message.</summary>
        public string WireValue { get; set; }
    }
}
