#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using Atdl4net.Model.Types.Support;

namespace Atdl4net.Model.Types
{
    /// <summary>
    /// NumInGroup_t is used when describing the number of entries in a FIX repeating group.
    /// </summary>
    /// <remarks>
    /// The FIX specification (5.0 SP2) describes this type as follows:
    /// <i>'int field representing the number of entries in a repeating group. Value must be positive.'</i>
    /// </remarks>
    public class NumInGroup_t : NonNegativeIntegerTypeBase
    {
    }
}
