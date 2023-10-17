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
    /// Represents the SingleSelectList_t control element within FIXatdl.
    /// </summary>
    public class SingleSelectList_t : ListControlBase
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Model.Controls");

        /// <summary>
        /// Initializes a new instance of <see cref="SingleSelectList_t"/> using the supplied ID.
        /// </summary>
        /// <param name="id">ID for this control.</param>
        public SingleSelectList_t(string id)
            : base(id)
        {
            _log.Debug(m => m("New SingleSelectList_t created as control {0}", id));
        }
    }
}
