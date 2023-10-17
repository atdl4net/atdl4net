#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

namespace Atdl4net.Model.Types
{
    /// <summary>
    /// 'string field containing one or more space delimited multiple character values (e.g. |277=AV AN A| ).'
    /// </summary>
    public class MultipleStringValue_t : String_t
    {
        /// <summary>
        /// Gets or sets the invert on wire property.<br/>
        /// Applicable when: xsi:type is MultipleStringValue_t or MultipleCharValue_t.
        /// Instructs the OMS whether to perform a bitwise “not” operation on each element of these lists.</summary>
        /// <value>true to instruct the OMS to invert; false otherwise.</value>
        public bool? InvertOnWire { get; set; }
    }
}
