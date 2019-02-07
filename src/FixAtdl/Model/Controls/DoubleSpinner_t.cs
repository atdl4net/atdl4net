#region Copyright (c) 2010-2012, Cornerstone Technology Limited. http://atdl4net.org
//
//   This software is released under both commercial and open-source licenses.
//
//   If you received this software under the commercial license, the terms of that license can be found in the
//   Commercial.txt file in the Licenses folder.  If you received this software under the open-source license,
//   the following applies:
//
//      This file is part of Atdl4net.
//
//      Atdl4net is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public 
//      License as published by the Free Software Foundation, either version 2.1 of the License, or (at your option) any later version.
// 
//      Atdl4net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
//      of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU Lesser General Public License for more details.
//
//      You should have received a copy of the GNU Lesser General Public License along with Atdl4net.  If not, see
//      http://www.gnu.org/licenses/.
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
