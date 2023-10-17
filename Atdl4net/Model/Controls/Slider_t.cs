#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using Atdl4net.Model.Controls.Support;
using Common.Logging;

namespace Atdl4net.Model.Controls
{
    /// <summary>
    /// Represents the Slider_t control element within FIXatdl.
    /// </summary>
    /// <remarks>The FIXatdl 1.1 specification is a little unclear on what a Slider_t can do.  The current Atdl4net implementation supports 
    /// selecting from a set of options (ListItems) but not selecting a numerical value.</remarks>
    public class Slider_t : ListControlBase
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Model.Controls");

        /// <summary>
        /// Initializes a new instance of <see cref="Slider_t"/> using the supplied ID.
        /// </summary>
        /// <param name="id">ID for this control.</param>
        public Slider_t(string id)
            : base(id) 
        {
            _log.Debug(m => m("New Slider_t created as control {0}", id));
        }
    }
}
