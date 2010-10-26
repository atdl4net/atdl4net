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
using Atdl4net.Model.Validation;
using Atdl4net.Utility;

namespace Atdl4net.Model.Elements
{
    // TODO: Implement IDisposable
    public class StateRule_t : EditEvaluator<Control_t>, IParentable<Control_t>
    {
        private Control_t _owner;

        public bool? Enabled { get; set; }
        public string Value { get; set; }
        public bool? Visible { get; set; }

        public StateRule_t()
        {
            (this as IKeyedObject).RefKey = RefKeyGenerator.GetNextKey(typeof(StateRule_t));

            Logger.DebugFormat("New StateRule_t created as StateRule[{0}].", (this as IKeyedObject).RefKey);
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
