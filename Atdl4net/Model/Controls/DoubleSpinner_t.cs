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
    /// Represents the DoubleSpinner_t control element within FIXatdl.
    /// </summary>
    public class DoubleSpinner_t : NumericControlBase
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Model.Controls");

        /// <summary>
        /// Initializes a new instance of <see cref="DoubleSpinner_t"/> using the supplied ID.
        /// </summary>
        /// <param name="id">ID for this control.</param>
        public DoubleSpinner_t(string id)
            : base(id)
        {
            _log.Debug(m => m("New DoubleSpinner_t created as control {0}", id));
        }

        /// <summary>Limits the granularity of the inner spinner of a double spinner control. Useful in spinner objects to enforce
        ///  odd-lot and sub-penny restrictions.  Applicable when xsi:type is DoubleSpinner_t.</summary>
        public decimal? InnerIncrement { get; set; }

        /// <summary>For double spinner control, defines how to determine the increment for the inner set of spinners. Applicable 
        /// when xsi:type is DoubleSpinner_t only.</summary>
        public IncrementPolicy_t? InnerIncrementPolicy { get; set; }

        /// <summary>Limits the granularity of the outer spinner of a double spinner control. Useful in spinner objects to enforce
        ///  odd-lot and sub-penny restrictions.  Applicable when xsi:type is DoubleSpinner_t.</summary>
        public decimal? OuterIncrement { get; set; }

        /// <summary>For double spinner control, defines how to determine the increment for the outer set of spinners. Applicable 
        /// when xsi:type is DoubleSpinner_t only.</summary>
        public IncrementPolicy_t? OuterIncrementPolicy { get; set; }
    }
}
