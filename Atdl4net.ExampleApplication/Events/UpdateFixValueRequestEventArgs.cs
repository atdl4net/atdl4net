#region Atdl4net Sample Code - License and Use
//
//   This sample code is provided as part of Atdl4net, with the intention of making it easier to get started.
//
//   Whilst Atdl4net is itself made available under either a commercial or an open-source (LGPL) license, the
//   samples provided with Atdl4net are made available for use freely by anyone that obtains a copy of
//   Atdl4net, without restriction.
//
//   For the avoidance of doubt, you are at liberty to remove this statement from any sample code that you
//   adapt for your use, but in any case the following statement still applies:
//
//   The samples for Atdl4net are distributed in the hope that they will be useful, but WITHOUT ANY WARRANTY; 
//   without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
//
#endregion

using System;
using System.Linq;

namespace Atdl4net.ExampleApplication.Events
{
    /// <summary>
    /// Event argument used to communicate the change of value for a specific FIX tag.
    /// </summary>
    public class UpdateFixValueRequestEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the FIX tag whose value is being updated.
        /// </summary>
        public uint FixTag { get; private set; }

        /// <summary>
        /// Gets the new value for the FIX tag that is being updated.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Initializes a new <see cref="UpdateFixValueRequestEventArgs"/> with the supplied FIX tag and value.
        /// </summary>
        /// <param name="fixTag">FIX tag.</param>
        /// <param name="value">New value for this FIX tag.</param>
        public UpdateFixValueRequestEventArgs(uint fixTag, string value)
        {
            FixTag = fixTag;
            Value = value;
        }
    }
}
