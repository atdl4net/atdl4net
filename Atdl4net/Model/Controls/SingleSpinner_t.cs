#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using Atdl4net.Model.Controls.Support;
using Atdl4net.Model.Enumerations;
using Common.Logging;

namespace Atdl4net.Model.Controls
{
    /// <summary>
    /// Represents the SingleSpinner_t control element within FIXatdl.
    /// </summary>
    public class SingleSpinner_t : NumericControlBase
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Model.Controls");

        /// <summary>
        /// Initializes a new instance of <see cref="SingleSpinner_t"/> using the supplied ID.
        /// </summary>
        /// <param name="id">ID for this control.</param>
        public SingleSpinner_t(string id)
            : base(id)
        {
            _log.Debug(m => m("New SingleSpinner_t created as control {0}", id));
        }

        /// <summary>Limits the granularity of a spinner control. Useful in spinner objects to enforce odd-lot and sub-penny
        ///  restrictions.  Applicable when xsi:type is SingleSpinner_t or Slider_t.</summary>
        public decimal? Increment { get; set; }

        /// <summary>For single spinner control, defines how to determine the increment. Applicable when xsi:type is SingleSpinner_t.</summary>
        public IncrementPolicy_t? IncrementPolicy { get; set; }
    }
}
