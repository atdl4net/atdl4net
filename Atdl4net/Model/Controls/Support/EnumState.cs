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

using System;
using System.Collections;
using System.Text;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Model.Collections;
using Atdl4net.Resources;
using Common.Logging;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Controls.Support
{
    /// <summary>
    /// Represents the state of an enumerated value within FIX, i.e., one that may have a single value, that maps to, say, Char_t, or
    /// one that may have several values, which in FIX is typed as a MultipleStringValue.
    /// </summary>
    /// <remarks>During its existence, this type has at times been a class and at other times a struct.  It has settled on being
    /// a class because its key function is to represent changing state, and mutable structs are seen to be <i>a bad thing</i>.
    /// As it is a class, the <see cref="CopyTo"/> method is included to provide a means to copy the contents in order to avoid
    /// the scenario that two independent entities share a reference to a given EnumState.</remarks>
    public class EnumState : IComparable
    {
        private const string ExceptionContext = "EnumState";

        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Model.Types");

        private readonly BitArray _enumStates;
        private readonly string[] _enumIds;
        private string _nonEnumValue;

        /// <summary>
        /// Initializes a new instance of <see cref="EnumState"/> with the supplied set of EnumID values.
        /// </summary>
        /// <param name="enumIds">Array of EnumID values.  The order of this array should match the order of the
        /// EnumPair elements within the target parameter.</param>
        public EnumState(string[] enumIds)
        {
            _enumIds = enumIds;
            _enumStates = new BitArray(_enumIds.Length);
            _nonEnumValue = null;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EnumState"/> and copies the data from the supplied EnumState.
        /// </summary>
        /// <param name="sourceState">EnumState to copy the initial state from.</param>
        public EnumState(EnumState sourceState)
        {
            if (sourceState == null)
                throw ThrowHelper.New<ArgumentException>(typeof(EnumState), "A valid EnumState value must be supplied to use this constuctor");

            _enumIds = sourceState._enumIds;
            _enumStates = new BitArray(sourceState._enumStates);
            _nonEnumValue = sourceState._nonEnumValue;
        }

        /// <summary>
        /// Makes a copy (deep clone) of this EnumState.
        /// </summary>
        /// <param name="targetState">EnumState to update.</param>
        /// <remarks>This method is provided to allow value type-like semantics for EnumState.</remarks>
        public EnumState Copy()
        {
            return new EnumState(this);
        }

        /// <summary>
        /// Updates this EnumState with the state of the supplied EnumState.
        /// </summary>
        /// <param name="source">Source EnumState to update this instance from.</param>
        /// <remarks>Implementation note: there are probably more effective ways to copy data from one BitArray to another that the one taken, but
        /// the current implementation ensures that the two EnumStates reference a common set of EnumIDs.</remarks>
        public void UpdateFrom(EnumState source)
        {
            if (source == null)
                throw ThrowHelper.New<ArgumentNullException>(this, "A valid EnumState must be supplied");

            if (_enumIds.Length != source._enumIds.Length)
                throw ThrowHelper.New<ArgumentException>(this, "Unable to update this EnumState from supplied EnumState as the number of EnumIDs was not consistent");

            _log.Debug(m => m("Updating EnumState from {0} to {1}", this.ToString(), source.ToString()));

            for (int n = 0; n < _enumIds.Length; n++)
            {
                if (_enumIds[n] == source._enumIds[n])
                    _enumStates.Set(n, source._enumStates[n]);
                else
                    throw ThrowHelper.New<ArgumentException>(this, "Mismatch between the EnumID of the source and target EnumState");
            }

            _nonEnumValue = source._nonEnumValue;
        }

        /// <summary>
        /// Determines whether the object supplied has the identical state to this instance.
        /// </summary>
        /// <param name="obj">Object to compare this object to.</param>
        /// <returns>true if the state of the supplied object is identical to this object instance; false otherwise.</returns>
        /// <remarks>This method assumes that both operands have the same set of EnumID values.</remarks>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is EnumState))
                return false;

            return _enumStates.Equals(obj) && _nonEnumValue == ((EnumState)obj)._nonEnumValue;
        }

        /// <summary>
        /// Serves as a hash function for this type.  Overridden because Equals(object) is overridden.
        /// </summary>
        /// <returns>A hash code for the current Object.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Gets/sets the boolean state of the enumerated element specified by the supplied EnumID value.
        /// </summary>
        /// <param name="enumId">EnumID to get/set the boolean state for.</param>
        /// <returns>Boolean state for the specified EnumID.</returns>
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

                        _log.Debug(m => m("EnumState state now {0}", this.ToString()));

                        return;
                    }

                throw ThrowHelper.New<ArgumentException>(this, ErrorMessages.UnrecognisedEnumIdValue, enumId);
            }
        }

        /// <summary>
        /// Gets the number of elements (EnumIDs) that this EnumState holds state for.
        /// </summary>
        public int Count { get { return _enumIds.Length; } }

        /// <summary>
        /// Determines whether the supplied EnumID value is valid for this EnumState instance.
        /// </summary>
        /// <param name="enumId">EnumID value to evaluate.</param>
        /// <returns>true if the supplied EnumID is valid for this EnumState; false otherwise.</returns>
        public bool IsValidEnumId(string enumId)
        {
            return Array.Exists(_enumIds, s => s == enumId);
        }

        /// <summary>
        /// Gets/sets the non-enum value; the non-enum value is only used by the EditableDropDownList_t type.
        /// </summary>
        public string NonEnumValue
        {
            get { return _nonEnumValue; }

            set
            {
                _enumStates.SetAll(false);

                _nonEnumValue = value;
            }
        }

        /// <summary>
        /// Gets the first EnumID that has a value of true.
        /// </summary>
        /// <returns>The first EnumID if any enumerated values are set; otherwise an empty string is returned.</returns>
        public string GetFirstSelectedEnumId()
        {
            for (int n = 0; n < _enumStates.Length; n++)
                if (_enumStates[n])
                    return _enumIds[n];

            return string.Empty;
        }

        /// <summary>
        /// Sets the state of all enumerated values to false.
        /// </summary>
        public void ClearAll()
        {
            _enumStates.SetAll(false);

            _nonEnumValue = null;
        }

        /// <summary>
        /// Loads the state of this EnumState from the supplied string in MultipleStringValue format, e.g., "EnumId_1 EnumId_2 EnumId_3".
        /// (Elements may be separated with space, semi-colon or comma.)
        /// </summary>
        /// <param name="initValues">MultipleStringValue format string containing the EnumIDs that specify the initial state for this EnumState.</param>
        /// <remarks>This method should not be confused with <see cref="FromWireValue"/>, as that method parses the supplied string for FIX wire
        /// values whereas this method parses the string for EnumIDs.</remarks>
        public void LoadInitValue(string initValues)
        {
            _log.DebugFormat("Loading EnumState with InitValue '{0}'", initValues);

            string[] initValuesArray = initValues.Split(new char[] { ';', ' ', ',' });

            ClearAll();

            foreach (string initValue in initValuesArray)
                this[initValue] = true;

            _log.Debug(m => m("EnumState is now {0}", ToString()));
        }

        /// <summary>
        /// Provides the state of this EnumState in a format ready to be sent over FIX, e.g., "A B C E".
        /// </summary>
        /// <param name="enumPairs">The EnumPairs for this parameter, to provide the mapping from EnumID values.</param>
        /// <returns>A space-separated string containing zero or more EnumPair WireValues.  If no EnumIDs are enabled,
        /// then null is returned.</returns>
        public string ToWireValue(EnumPairCollection enumPairs)
        {
            _log.Debug(m => m("Converting EnumState to WireValue; current state is {0}", ToString()));

            if (enumPairs.Count != _enumStates.Count)
                throw ThrowHelper.New<InvalidOperationException>(ExceptionContext, ErrorMessages.InconsistentEnumPairsListItemsError);

            // Override the values in the states collection if a non-enum value is supplied.  This is used to handle 
            // the unique case of the EditableDropDownList_t control.
            if (NonEnumValue != null)
                return NonEnumValue.Length > 0 ? NonEnumValue : null;

            bool hasAtLeastOneValue = false;
            StringBuilder sb = new StringBuilder();

            for (int n = 0; n < _enumStates.Length; n++)
            {
                if (_enumStates[n])
                {
                    string value = enumPairs.GetWireValueFromEnumId(_enumIds[n]);

                    // Typically {NULL} will only be used in a mutually exclusive fashion, although nothing enforces this
                    if (value != Atdl.NullValue)
                    {
                        // Only prepend a space after the first entry
                        if (hasAtLeastOneValue)
                            sb.AppendFormat(" {0}", value);
                        else
                        {
                            sb.Append(value);

                            hasAtLeastOneValue = true;
                        }
                    }
                }
            }

            _log.Debug(m => m("EnumState as WireValue is {0}", sb.ToString()));

            return hasAtLeastOneValue ? sb.ToString() : null;
        }

        /// <summary>
        /// Creates a new EnumState from the supplied set of EnumPairs and input FIX string.
        /// </summary>
        /// <param name="enumPairs">EnumPairs for this parameter.</param>
        /// <param name="multiValueString">String containing one or more FIX wire values (space-separated).</param>
        /// <returns></returns>
        public static EnumState FromWireValue(EnumPairCollection enumPairs, string multiValueString)
        {
            _log.DebugFormat("Converting WireValue '{0}' to EnumState", multiValueString);

            string[] inputValues = multiValueString.Split(new char[] { ';', ' ', ',' });

            EnumState result = new EnumState(enumPairs.EnumIds);

            foreach (string inputValue in inputValues)
            {
                string enumId;

                if (!enumPairs.TryParseWireValue(inputValue, out enumId))
                    throw ThrowHelper.New<ArgumentException>(ExceptionContext, ErrorMessages.UnrecognisedEnumIdValue, inputValue);

                result[enumId] = true;
            }

            _log.Debug(m => m("Converting EnumState from WireValue; state is {0}", result.ToString()));

            return result;
        }

        /// <summary>
        /// Provides a string representation of this EnumState.  Primarily for debugging purposes.
        /// </summary>
        /// <returns>String representation of this EnumState.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("(");

            for (int n = 0; n < _enumStates.Length; n++)
                sb.AppendFormat("{0}={1}{2}", _enumIds[n], _enumStates[n].ToString().ToLower(), n < _enumStates.Length - 1 ? ", " : string.Empty);

            if (_nonEnumValue != null)
                sb.AppendFormat(", NonEnumValue='{0}'", _nonEnumValue);

            sb.Append(")");

            return sb.ToString();
        }

        #region IComparable Members

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance 
        /// precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <remarks>IComparable is implemented by EnumState to allow its use within Edit_t processing.  Although this interface is
        /// designed to provide less than/greater than comparison, it is primarily used here for determining equality.</remarks>
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
