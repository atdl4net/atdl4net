#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Resources;
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
                throw ThrowHelper.New<FixParseException>(this, ErrorMessages.UnableToParseFixMessageEmpty);

            string[] nameValuePairs = rawMessage.Split(new char[] { SOH }, StringSplitOptions.RemoveEmptyEntries);

            if (nameValuePairs.Length == 0)
                throw ThrowHelper.New<FixParseException>(this, ErrorMessages.UnableToParseFixMessageInvalidContent, rawMessage);

            char[] separator = new char[] { Separator };

            string tagText = string.Empty;
            string valueText = string.Empty;

            try
            {
                foreach (string nameValuePair in nameValuePairs)
                {
                    string[] parts = nameValuePair.Split(separator);

                    if (parts.Length != 2)
                        throw ThrowHelper.New<FixParseException>(this, ErrorMessages.UnableToParseFixMessageInvalidContent, nameValuePair);

                    tagText = parts[0];
                    valueText = parts[1];

                    int tag = Convert.ToInt32(tagText);

                    Add((FixField)tag, parts[1]);
                }
            }
            catch (FormatException fe)
            {
                throw ThrowHelper.New<FixParseException>(this, fe, ErrorMessages.UnableToParseFixMessageInvalidFormat, tagText, valueText, fe.Message);
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
