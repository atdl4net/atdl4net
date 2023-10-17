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
    /// Represents the TextField_t control element within FIXatdl.
    /// </summary>
    public class TextField_t : TextControlBase
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Model.Controls");

        /// <summary>
        /// Initializes a new instance of <see cref="TextField_t"/> using the supplied ID.
        /// </summary>
        /// <param name="id">ID for this control.</param>
        public TextField_t(string id)
            : base(id)
        {
            _log.Debug(m => m("New TextField_t created as control {0}", id));
        }
    }
}
