#region Copyright (c) 2010-2011, Cornerstone Technology Limited. http://atdl4net.org
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

using Atdl4net.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Fix
{
    /// <summary>
    /// Represents a FIX message.
    /// </summary>
    public class FixMessage : Dictionary<FixField, string>
    {
        /// <summary>Field separator.</summary>
        public static readonly char SOH = '\x01';

        /// <summary>Field/value separator.</summary>
        public static readonly char Separator = '=';

        /// <summary>
        /// Initializes a new instance of <see cref="FixMessage"/>.
        /// </summary>
        public FixMessage()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="FixMessage"/> using the supplied FIX message.
        /// </summary>
        /// <param name="rawMessage">The FIX message to parse.</param>
        /// <remarks>The current implementation of this class does NOT support repeating blocks.</remarks>
        public FixMessage(string rawMessage)
        {
            if (string.IsNullOrEmpty(rawMessage))
                throw ThrowHelper.New<ArgumentException>(this, ErrorMessages.UnableToParseFixMessageEmpty);

            string[] nameValuePairs = rawMessage.Split(new char[] { SOH }, StringSplitOptions.RemoveEmptyEntries);

            if (nameValuePairs.Length == 0)
                throw ThrowHelper.New<ArgumentException>(this, ErrorMessages.UnableToParseFixMessageInvalidContent, rawMessage);

            char[] separator = new char[] { Separator };

            string tagText = string.Empty;
            string valueText = string.Empty;

            try
            {
                foreach (string nameValuePair in nameValuePairs)
                {
                    string[] parts = nameValuePair.Split(separator);

                    if (parts.Length != 2)
                        throw ThrowHelper.New<ArgumentException>(this, ErrorMessages.UnableToParseFixMessageInvalidContent, nameValuePair);

                    tagText = parts[0];
                    valueText = parts[1];

                    int tag = Convert.ToInt32(tagText);

                    Add((FixField)tag, parts[1]);
                }
            }
            catch (FormatException fe)
            {
                throw ThrowHelper.New<ArgumentException>(this, fe, ErrorMessages.UnableToParseFixMessageInvalidFormat, tagText, valueText, fe.Message);
            }
        }

        /// <summary>
        /// Gets the complete set of fix fields for this message.
        /// </summary>
        /// <value>The fix fields.</value>
        public ICollection<FixField> FixFields
        {
            get { return this.Keys; }
        }

        /// <summary>
        /// Provides the string representation of this FixMessage.
        /// </summary>
        /// <returns>String representation of this message.</returns>
        public string ToFix()
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<FixField, string> item in this)
            {
                sb.AppendFormat("{0}{1}{2}{3}", ((uint)item.Key).ToString(CultureInfo.InvariantCulture), Separator, item.Value, SOH);
            }

            return sb.ToString();
        }
    }
}
