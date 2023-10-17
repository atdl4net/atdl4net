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
    /// Represents the CheckBox_t control element within FIXatdl.
    /// </summary>
    public class CheckBox_t : BinaryControlBase
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Model.Controls");

        /// <summary>
        /// Initializes a new instance of <see cref="CheckBox_t"/> using the supplied ID.
        /// </summary>
        /// <param name="id">ID for this control.</param>
        public CheckBox_t(string id)
            : base(id)
        {
            _log.Debug(m => m("New CheckBox_t created as control {0}", id));
        }
    }
}
