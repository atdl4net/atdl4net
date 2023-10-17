#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

namespace Atdl4net.Xml.Serialization
{
    /// <summary>
    /// Represents a deserialization instruction that provides a mechanism to cache an element within the input stream,
    /// so that it can be used elsewhere in the deserialization process.
    /// </summary>
    public class CacheElementValueInstruction
    {
        /// <summary>
        /// Gets the key for this instruction.
        /// </summary>
        public string CacheKey { get; private set; }

        /// <summary>
        /// Initializes a new 
        /// </summary>
        /// <param name="cacheKey"></param>
        public CacheElementValueInstruction(string cacheKey)
        {
            CacheKey = cacheKey;
        }
    }
}