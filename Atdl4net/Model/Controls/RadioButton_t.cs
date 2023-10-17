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
    /// Represents the RadioButton_t control element within FIXatdl.
    /// </summary>
    public class RadioButton_t : BinaryControlBase
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Model.Controls");

        /// <summary>
        /// Initializes a new instance of <see cref="RadioButton_t"/> using the supplied ID.
        /// </summary>
        /// <param name="id">ID for this control.</param>
        public RadioButton_t(string id)
            : base(id)
        {
            _log.Debug(m => m("New RadioButton_t created as control {0}", id));
        }

        /// <summary>Identifies a common group name used by a set of RadioButton_t among which only one radio button 
        /// may be selected at a time.  Applicable when xsi:type is RadioButton_t.</summary>
        public string RadioGroup { get; set; }
    }
}
