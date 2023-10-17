#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using Atdl4net.Model.Elements.Support;

namespace Atdl4net.Model.Elements
{
    /// <summary>
    /// Represents the FIXatdl StrategyLayout element that contains the root StrategyPanel.
    /// </summary>
    public class StrategyLayout_t : IStrategyPanel
    {
        /// <summary>
        /// Gets/sets the root StrategyPanel.
        /// </summary>
        public StrategyPanel_t StrategyPanel { get; set; }
    }
}
