#region Copyright (c) 2010, Cornerstone Technology Limited. http://atdl4net.org
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
//      License as published by the Free Software Foundation, version 3.
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
using System;

namespace Atdl4net.Model.Controls
{
    public class Clock_t : Control_t, IDateTimeControl
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Atdl4net.Model.Controls.Clock_t">Clock_t</see> class using the supplied ID.
        /// </summary>
        /// <param name="id">ID for this control.</param>
        public Clock_t(string id)
            : base(id)
        {
            Logger.DebugFormat("New {0} created as Control[{1}] Id='{2}'.", typeof(Clock_t).Name, (this as IKeyedObject).RefKey, id);
        }

        public override void LoadDefault()
        {
            if (InitValue != null)
            {
                if (InitValueMode == 1)
                    Value = DateTime.Now > InitValue ? DateTime.Now : InitValue;
                else
                    Value = InitValue;
            }
            else
                Value = null;
        }

        #region IDateTimeControl Members

        public DateTime? Value { get; set; }

        // TODO: Implement LocalMktTz as a type.
        /// <summary>The timezone in which initValue is represented in.  Required when initValue is supplied. Applicable when 
        /// xsi:type is Clock_t.</summary>
        public string LocalMktTz { get; set; }

        /// <summary>The value used to pre-populate the GUI component when the order entry screen is initially rendered.</summary>
        public DateTime? InitValue { get; set; }

        /// <summary>Defines the treatment of initValue time. 0: use initValue; 1: use current time if initValue time has passed.
        /// The default value is 0.</summary>
        public int? InitValueMode { get; set; }

        #endregion

        public override object GetValue()
        {
            return Value;
        }

        public override void SetValue(object newValue)
        {
            if (object.Equals(newValue,Control_t.NullValue))
                Value = null;
            else
                Value = (DateTime?)newValue;
        }
    }
}
