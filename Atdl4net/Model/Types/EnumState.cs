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
using Atdl4net.Diagnostics;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Elements;
using Atdl4net.Resources;
using System;
using System.Collections;
using System.Text;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Types
{
    public class EnumState : IComparable
    {
        private const string ExceptionContext = "EnumState";

        private BitArray _enumStates;
        private string[] _enumIds;
        private string _nonEnumValue;

        public EnumState(string[] enumIds)
        {
            _enumIds = enumIds;
            _enumStates = new BitArray(_enumIds.Length);
        }

        public EnumState(EnumState sourceState)
        {
            _enumIds = sourceState._enumIds;
            _enumStates = new BitArray(sourceState._enumStates);
        }


        // NB Assumes that _enumIds == obj._enumIds.
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is EnumState))
                return false;

            return _enumStates.Equals(obj) && _nonEnumValue == ((EnumState)obj)._nonEnumValue;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool this[string enumId]
        {
            get
            {
                for (int n = 0; n < _enumIds.Length; n++)
                    if (_enumIds[n] == enumId)
                        return _enumStates[n];

                throw ThrowHelper.New<ArgumentException>(this, ErrorMessages.UnrecognisedEnumIdValue, enumId);
            }

            set
            {
                for (int n = 0; n < _enumIds.Length; n++)
                    if (_enumIds[n] == enumId)
                    {
                        _enumStates[n] = value;

                        return;
                    }

                throw ThrowHelper.New<ArgumentException>(this, ErrorMessages.UnrecognisedEnumIdValue, enumId);
            }
        }

        public int Count
        {
            get { return _enumIds.Length; }
        }

        public bool IsValidEnumId(string enumId)
        {
            return Array.Exists(_enumIds, s => s == enumId);
        }

        public string NonEnumValue
        {
            get { return _nonEnumValue; }

            set
            {
                _enumStates.SetAll(false);

                _nonEnumValue = value;
            }
        }

        public string GetFirstSelectedEnumId()
        {
            for (int n = 0; n < _enumStates.Length; n++)
                if (_enumStates[n])
                    return _enumIds[n];

            return string.Empty;
        }

        public void ClearAll()
        {
            _enumStates.SetAll(false);

            _nonEnumValue = null;
        }

        public void LoadInitValue(string initValues)
        {
            string[] initValuesArray = initValues.Split(new char[] { ';' });

            ClearAll();

            foreach (string initValue in initValuesArray)
                this[initValue] = true;
        }

        public string ToWireValue(EnumPairCollection enumPairs)
        {
            if (enumPairs.Count != _enumStates.Count)
                throw ThrowHelper.New<InvalidOperationException>(ExceptionContext, ErrorMessages.InconsistentEnumPairsListItemsError);

            // Override the values in the states collection if a non-enum value is supplied.  This is used to handle 
            // the EditableDropDownList_t control.
            if (NonEnumValue != null)
                return NonEnumValue.Length > 0 ? NonEnumValue : null;

            bool hasValue = false;
            StringBuilder sb = new StringBuilder();

            for (int n = 0; n < _enumStates.Length; n++)
                if (_enumStates[n])
                {
                    string value = enumPairs.GetWireValueFromEnumId(_enumIds[n]);

                    if (value != Control_t.NullValue)
                    {
                        if (hasValue)
                            sb.AppendFormat(" {0}", value);
                        else
                        {
                            sb.Append(value);

                            hasValue = true;
                        }
                    }
                }

            string result = sb.ToString();

            return result.Length > 0 ? result : null;
        }

        public static EnumState FromWireValue(EnumPairCollection enumPairs, string multiValueString)
        {
            string[] inputValues = multiValueString.Split(new char[] { ' ' });

            EnumState result = new EnumState(enumPairs.EnumIds);

            foreach (string inputValue in inputValues)
            {
                string enumId;

                if (!enumPairs.TryParseWireValue(inputValue, out enumId))
                    throw ThrowHelper.New<ArgumentException>(ExceptionContext, ErrorMessages.UnrecognisedEnumIdValue, inputValue);

                result[enumId] = true;
            }

            return result;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("(");

            for (int n = 0; n < _enumStates.Length; n++)
                sb.AppendFormat("{0}={1}{2}", _enumIds[n], _enumStates[n], n < _enumStates.Length - 1 ? ", " : string.Empty);

            if (_nonEnumValue != null)
                sb.AppendFormat(", NonEnumValue='{0}'", _nonEnumValue);

            sb.Append(")");

            return sb.ToString();
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is string)
            {
                string enumId = (string)obj;

                if (!IsValidEnumId(enumId))
                    throw ThrowHelper.New<InvalidFieldValueException>(this, ErrorMessages.UnrecognisedEnumIdValue, enumId);

                return this[enumId] ? 0 : -1;
            }
            else if (obj is EnumState)
            {
                EnumState enumState = (EnumState)obj;

                return this.Equals(enumState) ? 0 : -1;
            }
            else
                throw ThrowHelper.New<ArgumentException>(this, ErrorMessages.CompareValueFailure, this.ToString(), obj.GetType().FullName);
        }

        #endregion
    }
}
