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

using System.ComponentModel.Composition;
using System.Diagnostics;

namespace Atdl4net.Diagnostics
{
    /// <summary>
    /// Class that provides library-wide static access to the logger.
    /// </summary>
    public class Logger
    {
        private static IExternalLogger _logger;

        [Import(typeof(Atdl4net.Diagnostics.IExternalLogger))]
        private IExternalLogger Log { set { _logger = value; } }

        /// <summary>
        /// Writes the specified debug message.
        /// </summary>
        /// <param name="message">The message.</param>
        [Conditional("DEBUG")]
        public static void Debug(string message)
        {
            if (_logger != null)
                _logger.Debug(message);
        }

        /// <summary>
        /// /// Writes the specified debug message using the supplied format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg">The arg.</param>
        [Conditional("DEBUG")]
        public static void DebugFormat(string format, object arg)
        {
            if (_logger != null)
                _logger.DebugFormat(format, arg);
        }

        /// <summary>
        /// /// Writes the specified debug message using the supplied format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        [Conditional("DEBUG")]
        public static void DebugFormat(string format, params object[] args)
        {
            if (_logger != null)
                _logger.DebugFormat(format, args);
        }

        /// <summary>
        /// Gets a value indicating whether this instance is initialised.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is initialised; otherwise, <c>false</c>.
        /// </value>
        public static bool IsInitialised { get { return _logger != null; } }
    }
}
