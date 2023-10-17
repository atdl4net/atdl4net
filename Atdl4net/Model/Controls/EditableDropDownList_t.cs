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
    /// Represents the EditableDropDownList_t control element within FIXatdl.
    /// </summary>
    public class EditableDropDownList_t : ListControlBase
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Model.Controls");

        /// <summary>
        /// Initializes a new instance of <see cref="EditableDropDownList_t"/> using the supplied ID.
        /// </summary>
        /// <param name="id">ID for this control.</param>
        public EditableDropDownList_t(string id)
            : base(id)
        {
            _log.Debug(m => m("New EditableDropDownList_t created as control {0}", id));
        }

        /// <summary>
        /// Indicates whether the EnumState value for this control can be set to a value other than one of the enumerated
        /// values.
        /// </summary>
        protected override bool IsNonEnumValueAllowed { get { return true; } }
    }
}
