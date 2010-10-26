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
using System.ComponentModel.Composition.Primitives;

namespace Atdl4net.Diagnostics
{
    /// <summary>
    /// <para>This class provides the default implementation of the IExternalLogger interface used
    /// throughout Atdl4net to collect debugging, tracing and logging information.</para>
    /// <para>If no other implementation of IExternalLogger is made available, then this implementation
    /// is used.  All default logging is done via the System.Diagnostics facilities.</para>
    /// </summary>
    [Export(typeof(IExternalLogger))]
    public class DefaultLogger : IExternalLogger
    {
        #region IExternalLogger Members
        /// <summary>
        /// Writes the supplied text to the debug output.
        /// </summary>
        /// <param name="message">Message to write.</param>
        public void Debug(string message)
        {
            // System.Diagnostics.Debug.WriteLine(message);
        }

        /// <summary>
        /// Writes a formatted message to the debug output using the supplied format string.
        /// </summary>
        /// <param name="format">Format string to use to format the message.</param>
        /// <param name="args">One or more arguments to be inserted into the formatted message.</param>
        public void DebugFormat(string format, params object[] args)
        {
            // System.Diagnostics.Debug.WriteLine(string.Format(format, args));
        }

        /// <summary>
        /// Writes a formatted message to the debug output using the supplied format string.
        /// </summary>
        /// <param name="format">Format string to use to format the message.</param>
        /// <param name="args">Argument to be inserted into the formatted message.</param>
        public void DebugFormat(string format, object arg)
        {
            // System.Diagnostics.Debug.WriteLine(string.Format(format, arg));
        }

        #endregion
    }
}
