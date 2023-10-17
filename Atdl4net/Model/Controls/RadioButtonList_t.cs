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
    /// Represents the RadioButtonList_t control element within FIXatdl.
    /// </summary>
    public class RadioButtonList_t : ListControlBase, IOrientableControl
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Model.Controls");

        /// <summary>
        /// Initializes a new instance of <see cref="RadioButtonList_t"/> using the supplied ID.
        /// </summary>
        /// <param name="id">ID for this control.</param>
        public RadioButtonList_t(string id)
            : base(id)
        {
            _log.Debug(m => m("New RadioButtonList_t created as control {0}", id));
        }

        #region IOrientableControl Members

        /// <summary>Must be “HORIZONTAL” or “VERTICAL”. Declares the orientation of the radio buttons within a RadioButtonList
        ///  or the checkboxes within a CheckBoxList.  Applicable when xsi:type is RadioButtonList_t or CheckBoxList_t.</summary>
        public Orientation_t? Orientation { get; set; }
        
        #endregion
    }
}
