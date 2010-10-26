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

using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace Atdl4net.Diagnostics
{
    /// <summary>
    /// This class is used to hold additional information about an exception, such as line number and line position for 
    /// errors in XML files.
    /// </summary>
    public class ExceptionInfo
    {
        private Dictionary<string, object> _data = new Dictionary<string, object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionInfo"/> class.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public ExceptionInfo(XObject obj)
        {
            if (obj is IXmlLineInfo && (obj as IXmlLineInfo).HasLineInfo())
            {
                _data["LineNumber"] = (obj as IXmlLineInfo).LineNumber;
                _data["LinePosition"] = (obj as IXmlLineInfo).LinePosition;
            }
        }

        /// <summary>
        /// Populates the exception data.
        /// </summary>
        /// <param name="data">The data.</param>
        public void PopulateExceptionData(IDictionary data)
        {
            foreach (KeyValuePair<string, object> item in _data)
                data[item.Key] = item.Value;
        }
    }
}