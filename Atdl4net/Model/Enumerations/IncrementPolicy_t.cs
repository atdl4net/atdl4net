#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

namespace Atdl4net.Model.Enumerations
{
    /// <summary>
    /// Used with single spinner control, defines how to determine the increment. 
    /// </summary>
    public enum IncrementPolicy_t
    {
        /// <summary>Use value from increment attribute.</summary>
        Static,

        /// <summary>Use the round lot size of symbol.</summary>
        LotSize,

        /// <summary>Use symbol minimum tick size.</summary>
        Tick
    }
}
