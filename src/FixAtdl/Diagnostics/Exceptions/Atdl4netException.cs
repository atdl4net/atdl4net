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

using System;
using System.Runtime.Serialization;

namespace Atdl4net.Diagnostics.Exceptions
{
    /// <summary>Provides a base exception class for all Atdl4net custom exceptions.</summary>
    [Serializable]
    public class Atdl4netException : System.Exception
    {
        /// <summary>Initializes a new instance of the Atdl4netException class; for serialization purposes only.</summary>
        protected Atdl4netException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>Initializes a new instance of the Atdl4netException class with a specified error message.</summary>
        public Atdl4netException(string message)
            : base(message)
        {
        }

        /// <summary>Initializes a new instance of the Atdl4netException class with a specified error message and a reference to the inner exception that is the
        /// cause of this exception.</summary>
        public Atdl4netException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
