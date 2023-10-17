#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

namespace Atdl4net.Utility
{
    /// <summary>
    /// Data entry mode.
    /// </summary>
    public enum DataEntryMode
    {
        /// <summary>
        /// View only mode - fields cannot be changed.
        /// </summary>
        View,

        /// <summary>
        /// Create order mode.
        /// </summary>
        Create,

        /// <summary>
        /// Amend order mode.
        /// </summary>
        Amend
    }
}