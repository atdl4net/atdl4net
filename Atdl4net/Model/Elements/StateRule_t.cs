#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
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
