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

namespace Atdl4net.Diagnostics.Exceptions
{
    /// <summary>
    /// Thrown if an error occurs when trying to render a strategy.  The InnerException should provide more insight into the
    /// underlying cause, if the Message value provides insufficient detail.
    /// </summary>
    [Serializable]
    public class RenderingException : Atdl4netException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenderingException"/> class.
        /// </summary>
        /// <param name="message"></param>
        public RenderingException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderingException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public RenderingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
