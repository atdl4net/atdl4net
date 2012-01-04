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

using System.Collections.Generic;
using Atdl4net.Fix;
using Atdl4net.Utility;

namespace Atdl4net.Model.Collections
{
    public class FixTagValuesCollection : IEnumerable<KeyValuePair<FixField, string>>
    {
        private readonly FixMessage _message;

        public FixTagValuesCollection()
        {
            _message = new FixMessage();
        }

        public FixTagValuesCollection(string fixMessage)
            : this(new FixMessage(fixMessage))
        {
        }

        public FixTagValuesCollection(FixMessage message)
        {
            _message = message;
            //foreach (FixField field in message.FixFields)
            //    this[field] = message[field];
        }

        /// <summary>
        /// FIX_ ...
        /// </summary>
        /// <param name="fixField"></param>
        /// <returns>.</returns>
        public string this[string fixField]
        {
            get
            {
                FixField field = fixField.ParseAsEnum<FixField>();

                return _message[field];
            }

            set
            {
                FixField field = fixField.ParseAsEnum<FixField>();

                _message[field] = value;
            }
        }

        public bool TryGetValue(string fixField, out string value)
        {
            FixField field = fixField.ParseAsEnum<FixField>();

            return _message.TryGetValue(field, out value);
        }

        public bool TryGetValue(FixTag tag, out string value)
        {
            FixField field = tag;

            return _message.TryGetValue(field, out value);
        }

        public void Add(FixTag tag, string value)
        {
            _message.Add(tag, value);
        }

        public string ToFix()
        {
            return _message.ToFix();
        }

        public override string ToString()
        {
            return ToFix().Replace("\x01", " | ");
        }

        public IEnumerator<KeyValuePair<FixField, string>> GetEnumerator()
        {
            return _message.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _message.GetEnumerator();
        }
    }
}