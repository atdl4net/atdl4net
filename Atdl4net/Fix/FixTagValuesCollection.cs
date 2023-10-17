#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System.Collections.Generic;
using Atdl4net.Utility;

namespace Atdl4net.Fix
{
    public class FixTagValuesCollection : IEnumerable<KeyValuePair<FixField, string>>
    {
        private static readonly FixTagValuesCollection _empty = new FixTagValuesCollection();
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
        }

        public static FixTagValuesCollection Empty { get { return _empty; } }

        public string this[FixField fixField]
        {
            get { return _message[fixField]; }
            set { _message[fixField] = value; }
        }

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