#region Copyright (c) 2010-2011, Cornerstone Technology Limited. http://atdl4net.org
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

using Atdl4net.Diagnostics;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Elements;
using Atdl4net.Model.Enumerations;
using Atdl4net.Model.Types;
using Common.Logging;

namespace Atdl4net.Model.Controls
{
    public class RadioButtonList_t : Control_t, IListControl, IOrientableControl
    {
        private static readonly ILog _log = LogManager.GetLogger("Model");

        /// <summary>
        /// Initialises a new instance of the <see cref="Atdl4net.Model.Controls.RadioButtonList_t">RadioButtonList_t</see> class using the supplied ID.
        /// </summary>
        /// <param name="id">ID for this control.</param>
        public RadioButtonList_t(string id)
            : base(id)
        {
            _log.DebugFormat("New {0} created as Control[{1}] Id='{2}'.", typeof(RadioButtonList_t).Name, (this as IKeyedObject).RefKey, id);

            ListItems = new ListItemCollection();
        }

        public override void LoadDefault()
        {
            Value = new EnumState(ListItems.EnumIds);

            if (InitValue != null)
                Value.LoadInitValue(InitValue);
        }

        #region IListControl Members

        public EnumState Value { get; set; }

        /// <summary>The value used to pre-populate the GUI component when the order entry screen is initially rendered.</summary>
        public string InitValue { get; set; }

        public bool HasListItems { get { return ListItems.HasItems; } }

        public ListItemCollection ListItems { get; private set; }

        #endregion

        #region IOrientableControl Members

        /// <summary>Must be “HORIZONTAL” or “VERTICAL”. Declares the orientation of the radio buttons within a RadioButtonList
        ///  or the checkboxes within a CheckBoxList.  Applicable when xsi:type is RadioButtonList_t or CheckBoxList_t.</summary>
        public Orientation_t? Orientation { get; set; }
        
        #endregion

        public override object GetValue()
        {
            return Value;
        }

        public override void SetValue(object newValue)
        {
            if (object.Equals(newValue, Control_t.NullValue))
                Value.ClearAll();
            else
                Value = (EnumState)newValue;
        }
    }
}
