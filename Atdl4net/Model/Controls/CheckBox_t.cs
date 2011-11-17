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
using Atdl4net.Model.Elements;
using Common.Logging;

namespace Atdl4net.Model.Controls
{
    public class CheckBox_t : Control_t, IBooleanControl
    {
        private static readonly ILog _log = LogManager.GetLogger("Model");

        /// <summary>
        /// Initialises a new instance of the <see cref="Atdl4net.Model.Controls.CheckBox_t">CheckBox_t</see> class using the supplied ID.
        /// </summary>
        /// <param name="id">ID for this control.</param>
        public CheckBox_t(string id)
            : base(id)
        {
            _log.DebugFormat("New {0} created as Control[{1}] Id='{2}'.", typeof(CheckBox_t).Name, (this as IKeyedObject).RefKey, id);
        }

        public override void LoadDefault()
        {
            if (InitValue != null)
                Value = InitValue.GetValue(GetInputValues());
        }

        #region IBooleanControl Members

        public bool Value { get; set; }

        /// <summary>The value used to pre-populate the GUI component when the order entry screen is initially rendered.</summary>
        public InitValue<bool> InitValue { get; set; }

        /// <summary>Output enumID if checked/selected.  Applicable when xsi:type is CheckBox_t or RadioButton_t.</summary>
        public string CheckedEnumRef { get; set; }

        /// <summary>Output enumID if unchecked/not selected.  Applicable when xsi:type is CheckBox_t or RadioButton_t.</summary>
        public string UncheckedEnumRef { get; set; }

        #endregion IBooleanControl Members

        public override object GetValue()
        {
            return Value;
        }

        public override void SetValue(object newValue)
        {
            if (object.Equals(newValue, Control_t.NullValue))
                Value = false;
            else
                Value = (bool)newValue;
        }
    }
}
