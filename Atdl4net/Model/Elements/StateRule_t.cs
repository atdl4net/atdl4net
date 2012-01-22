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

using System.Text;
using Atdl4net.Utility;
using Atdl4net.Validation;

namespace Atdl4net.Model.Elements
{
    // TODO: Implement IDisposable
    public class StateRule_t : EditEvaluator<Control_t>, IParentable<Control_t>
    {
        private Control_t _owner;

        /// <summary>
        /// Enabled state for this state rule.
        /// </summary>
        public bool? Enabled { get; set; }

        /// <summary>
        /// Value attribute for this state rule.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Visible state for this state rule.
        /// </summary>
        public bool? Visible { get; set; }

        /// <summary>
        /// Provides a string representation of this StateRule_t, primarily for debugging purposes.
        /// </summary>
        /// <returns>String representation in the format (control_id, enabled_value_if_set, value_value_if_set, visible_value_if_set).</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("(Control.ID=\"{0}\"", _owner.Id);

            if (Enabled != null)
                sb.AppendFormat(", enabled=\"{0}\"", Enabled.ToString().ToLower());
 
            if (Value != null)
                sb.AppendFormat(", value=\"{0}\"", Value);

            if (Visible != null)
                sb.AppendFormat(", visible=\"{0}\"", Visible.ToString().ToLower());

            sb.Append(")");

            return sb.ToString();
        }

        #region IParentable<Control_t> Members

        Control_t IParentable<Control_t>.Parent
        {
            get { return _owner; }
            set { _owner = value; }
        }

        #endregion IParentable<Control_t> Members
    }
}
