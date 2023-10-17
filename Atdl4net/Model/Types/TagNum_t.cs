#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using Atdl4net.Model.Types.Support;

namespace Atdl4net.Model.Types
{
    /// <summary>
    /// 'int field representing a field's tag number when using FIX "Tag=Value" syntax. Value must be positive and may not 
    /// contain leading zeros.'
    /// </summary>
    public class TagNum_t : NonZeroPositiveIntegerTypeBase
    {
    }
}
