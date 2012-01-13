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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Fix;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Model.Enumerations;
using Atdl4net.Resources;
using Atdl4net.Utility;
using Atdl4net.Validation;
using Common.Logging;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Elements
{
    /// <summary>
    /// Represents the FIXatdl type Edit_t when it occurs outside of a StateRule_t or a StrategyEdit_t element.
    /// </summary>
    public class Edit_t
    {
        /// <summary>
        /// Gets/sets the first field name for comparison. When the edit is used within a StateRule, this field 
        /// must refer to the ID of a Control. When the edit is used within a StrategyEdit, this field must refer 
        /// to either the name of a parameter or a standard FIX field name. When referring to a standard FIX tag
        /// then the name must be pre-pended with the string "FIX_", e.g. "FIX_OrderQty". Required the Operator is 
        /// not null.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Gets/sets the optional second field name for comparison. When the edit is used within a StateRule, this field 
        /// must refer to the ID of a Control. When the edit is used within a StrategyEdit, this field must refer 
        /// to either the name of a parameter or a standard FIX field name. When referring to a standard FIX tag
        /// then the name must be pre-pended with the string "FIX_", e.g. "FIX_OrderQty".
        /// </summary>
        public string Field2 { get; set; }

        public string Id { get; set; }

        public Operator_t? Operator { get; set; }

        public LogicOperator_t? LogicOperator { get; set; }

        public string Value { get; set; }

        public EditCollection Edits { get; private set; }

        public Edit_t()
        {
            Edits = new EditCollection();
        }
    }

    /// <summary>
    /// Represents a FIXatdl Edit_t when implemented within a StateRule_t or StrategyEdit_t element.
    /// </summary>
    public class Edit_t<T> : IEdit<T>, IResolvable<Strategy_t, T> where T : class, IValueProvider
    {
        // Use Atdl4net.Validation namespace rather than Atdl4net.Model.Elements for debugging purposes
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Validation");
        private static readonly bool isPartOfStrategyEdit = typeof(T) == typeof(IParameter);

        private bool _currentState;
        private T _fieldSource;
        private T _field2Source;
        private readonly EditEvaluatingCollection<T> _edits;
        private readonly EditRefCollection<T> _editRefs;

        /// <summary>
        /// Initializes a new <see cref="Edit{T}"/> instance.
        /// </summary>
        public Edit_t()
        {
            _edits = new EditEvaluatingCollection<T>();
            _editRefs = new EditRefCollection<T>(_edits);

            // For StrategyEdits, we want to start with the assumption that the current state of this
            // Edit is true (i.e., valid) before it has been evaluated
            _currentState = isPartOfStrategyEdit ? true : false;
        }

        /// <summary>
        /// Provides a string representation of this Edit_t, primarily for debugging purposes.
        /// </summary>
        /// <returns>String representation of this Edit_t.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("(");

            if (Id != null)
                sb.AppendFormat("Id=\"{0}\", ", Id);

            if (LogicOperator != null)
                sb.AppendFormat("LogicOperator=\"{0}\", ", LogicOperator);

            if (Field != null)
                sb.AppendFormat("Field=\"{0}\", ", Field);

            if (Operator != null)
                sb.AppendFormat("Operator=\"{0}\", ", Operator);

            if (Value != null)
                sb.AppendFormat("Value=\"{0}\", ", Value);

            if (Field2 != null)
                sb.AppendFormat("Field2=\"{0}\", ", Field2);

            // Convert to string so we can remove trailing ', '
            string text = sb.ToString();

            return string.Format("{0})", text.Substring(0, text.Length - 2));
        }

        /// <summary>
        /// Gets the collection of EditRefs for this Edit.
        /// </summary>
        public EditRefCollection<T> EditRefs { get { return _editRefs; } }

        /// <summary>
        /// Gets the set of sources for this Edit and its children.  As source is non-null Field or Field2 value.
        /// </summary>
        public HashSet<string> Sources
        {
            get
            {
                HashSet<string> sources = new HashSet<string>();

                if (Operator != null)
                {
                    sources.Add(Field);

                    if (Field2 != null)
                        sources.Add(Field2);
                }
                else
                    foreach (string source in _edits.Sources)
                        sources.Add(source);

                return sources;
            }
        }

        #region IEdit_t Members

        /// <summary>
        /// Gets/sets the name of field to be used as left hand side of the evaluation.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Gets/sets the name of second (optional) field, to be used as the right hand side of the evaluation.
        /// </summary>
        public string Field2 { get; set; }

        /// <summary>
        /// Gets/sets the optional ID for this Edit.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets/sets the optional operator - used when comparing two values.
        /// </summary>
        public Operator_t? Operator { get; set; }

        /// <summary>
        /// Gets/sets the optional fixed value to be used as the right hand side of the evaluation.
        /// </summary>
        /// <remarks>From the spec:<br/><br/>"When Edit is a descendant of a StateRule element, Value refers to the 
        /// value of the control referred by Field. If the control referred by Field has enumerated values then Value 
        /// refers to the enumID of one of the control's ListItem elements.<br/>
        /// When Edit is a descendant of a StrategyEdit element, Value refers to the wireValue of the parameter 
        /// referred by Field."</remarks>
        public string Value { get; set; }

        /// <summary>
        /// Gets the current state of this Edit based on the most recent evaluation.
        /// </summary>
        public bool CurrentState { get { return _currentState; } }

        /// <summary>
        /// Gets the collection of child Edits.  May be empty, unless LogicOperator is non-null.
        /// </summary>
        public EditEvaluatingCollection<T> Edits { get { return _edits; } }

        /// <summary>
        /// Gets/sets the optional logical operator - used when combining two or more Edits.
        /// </summary>
        public LogicOperator_t? LogicOperator
        {
            get { return Edits.LogicOperator; }
            set { Edits.LogicOperator = value; }
        }

        /// <summary>
        /// Gets the current value of the field pointed to by the Field property.
        /// </summary>
        public object FieldValue
        {
            get
            {
                if (_fieldSource != null)
                    return _fieldSource.GetCurrentValue();
                else
                    throw ThrowHelper.New<InvalidOperationException>(this, "Edit attempted to access FieldValue but requisite control was not set.");
            }
        }

        /// <summary>
        /// Gets the current value of the field pointed to by the Field2 property.
        /// </summary>
        public object Field2Value
        {
            get
            {
                if (_field2Source != null)
                    return _field2Source.GetCurrentValue();
                else
                    throw ThrowHelper.New<InvalidOperationException>(this, "Edit attempted to access Field2Value but requisite control was not set.");
            }
        }

        /// <summary>
        /// Evaluates this Edit based on the current field values and any supplied FIX field values.
        /// </summary>
        /// <param name="additionalValues">Any additional FIX field values that may be required in the Edit evaluation.</param>
        public void Evaluate(FixFieldValueProvider additionalValues)
        {
            _log.Debug(m => m("Evaluating Edit_t {0}; current state is {1}", ToString(), _currentState.ToString().ToLower()));

            if (Operator != null)
            {
                object lhs = GetLhsValue(additionalValues);

                switch (Operator)
                {
                    case Operator_t.Exist:
                    case Operator_t.NotExist:
                        _currentState = EvaluateExists(lhs);
                        break;

                    case Operator_t.Equal:
                    case Operator_t.NotEqual:
                        _currentState = EvaluateEquality(lhs, GetRhsValue(additionalValues, lhs));
                        break;

                    default:
                        _currentState = EvaluateInequalityComparison(lhs as IComparable, GetRhsValue(additionalValues, lhs) as IComparable);
                        break;
                }
            }
            else if (LogicOperator != null)
            {
                _edits.Evaluate(additionalValues);

                _currentState = _edits.CurrentState;
            }
            else
                throw ThrowHelper.New<InvalidOperationException>(this, ErrorMessages.MissingOperatorsOnEdit);

            _log.Debug(m => m("Evaluation of Edit_t {0} yielded state of {1}", ToString(), _currentState.ToString().ToLower()));
        }

        #endregion IEdit_t Members

        private bool EvaluateExists(object value)
        {
            bool checkingForExist = Operator == Operator_t.Exist;

            bool empty = value == null || (value as string == string.Empty);

            bool result = checkingForExist ? !empty : empty;

            _log.Debug(m => m("Evaluated whether Field {0} {1} a value; result is {2} (value was '{3}')", Field,
                checkingForExist ? "has" : "does not have", result.ToString().ToLower(), empty ? "N/A" : value));

            return result;
        }

        private bool EvaluateEquality(object lhs, object rhs)
        {
            _log.Debug(m => m("Comparing values operand1={0}, operand2={1} for equality with operator {2}", lhs, rhs, Operator));

            CheckForUnsupportedComparisons(lhs, rhs);

            bool equal;

            if (lhs == null)
                equal = rhs == null || (rhs as string) == Atdl.NullValue;
            else
            {
                IComparable comparableLhs = lhs as IComparable;
                IComparable comparableRhs = rhs as IComparable;

                if (comparableLhs != null && comparableRhs != null)
                    equal = comparableLhs.CompareTo(comparableRhs) ==0;
                else
                    equal = lhs.Equals(rhs);
            }

            bool finalResult = Operator == Operator_t.Equal ? equal : !equal;

            _log.Debug(m => m("Result of equality comparison = {0}", finalResult.ToString().ToLower()));

            return finalResult;
        }

        private bool EvaluateInequalityComparison(IComparable lhs, IComparable rhs)
        {
            _log.Debug(m => m("Comparing values lhs='{0}', rhs='{1}' for inequality with operator {2}", lhs, rhs, Operator));

            // It's not clear what the right thing is to do with a null LHS and an inequality operator
            // so we return false anyway
            if (lhs == null)
            {
                _log.Debug("Left hand side of inequality comparison is null so returning false");

                return false;
            }

            int compareResult = lhs.CompareTo(rhs);

            bool finalResult = false;

            switch (Operator)
            {
                case Operator_t.GreaterThan:
                    finalResult = compareResult > 0;
                    break;

                case Operator_t.GreaterThanOrEqual:
                    finalResult = compareResult >= 0;
                    break;

                case Operator_t.LessThan:
                    finalResult = compareResult < 0;
                    break;

                case Operator_t.LessThanOrEqual:
                    finalResult = compareResult <= 0;
                    break;
            }

            _log.Debug(m => m("Compared values '{0}' and '{1}' as part of Edit_t evaluation; result was {2}",
                lhs, rhs, finalResult.ToString().ToLower()));

            return finalResult;
        }

        private object GetLhsValue(FixFieldValueProvider additionalValues)
        {
            if (Field.StartsWith("FIX_"))
                return GetFixFieldValue(additionalValues, Field);

            object result;
            object fieldValue = FieldValue;

            // If the field value can be converted into a number, most likely it should be treated as one
            // for comparison purposes
            if (fieldValue is string)
            {
                decimal number;

                if (decimal.TryParse(fieldValue as string, out number))
                    result = number;
                else
                    result = fieldValue;
            }
            else
                result = fieldValue;

            return result;
        }

        private object GetRhsValue(FixFieldValueProvider additionalValues, object lhs)
        {
            if (Value != null)
                return EditValueConverter.ConvertToComparableType(lhs, Value);

            if (Field2 != null)
            {
                if (Field2.StartsWith("FIX_"))
                    return GetFixFieldValue(additionalValues, Field2);

                return Field2Value;
            }

            return null;
        }

        private void CheckForUnsupportedComparisons(object lhs, object rhs)
        {
            // We don't currently support comparisons for type 'Data_t' which is represented by a char[].
            if (lhs is char[])
                throw ThrowHelper.New<InvalidOperationException>(this, ErrorMessages.UnsupportedComparisonOperation, Value, new string(lhs as char[]));

            if (rhs is char[])
                throw ThrowHelper.New<InvalidOperationException>(this, ErrorMessages.UnsupportedComparisonOperation, Value, new string(rhs as char[]));
        }

        private static object GetFixFieldValue(FixFieldValueProvider additionalValues, string fixField)
        {
            object result;
            string value;

            bool gotValue = additionalValues.TryGetValue(fixField, out value);

            // If the FIX value can be converted into a number, most likely it should be treated as one
            // for comparison purposes
            if (gotValue)
            {
                decimal number;

                if (decimal.TryParse(value, out number))
                    result = number;
                else
                    result = value;
            }
            else
                result = null;

            _log.Debug(m => m("Looked up FIX field {0} for comparison; field was {1}, value={2}",
                fixField, gotValue ? "found" : "not found", gotValue ? result : "N/A"));

            return result;
        }

        #region IResolvable<Strategy_t> Members

        /// <summary>
        /// Resolves all interdependencies e.g. edits to edit refs, control values to edits, etc.  Called once
        /// all strategies have been loaded as there may be dependencies on EditRefs at the global level.
        /// </summary>
        void IResolvable<Strategy_t, T>.Resolve(Strategy_t strategy, ISimpleDictionary<T> sourceCollection)
        {
            (_edits as IResolvable<Strategy_t, T>).Resolve(strategy, sourceCollection);

            if (!string.IsNullOrEmpty(Field) && !Field.StartsWith("FIX_"))
            {
                if (sourceCollection.Contains(Field))
                    _fieldSource = sourceCollection[Field];
                else
                    throw ThrowHelper.New<ReferencedObjectNotFoundException>(this, ErrorMessages.EditRefFieldControlNotFound, Field, "Field");
            }

            if (!string.IsNullOrEmpty(Field2) && !Field2.StartsWith("FIX_"))
            {
                if (sourceCollection.Contains(Field2))
                    _field2Source = sourceCollection[Field2];
                else
                    throw ThrowHelper.New<ReferencedObjectNotFoundException>(this, ErrorMessages.EditRefFieldControlNotFound, Field2, "Field2");
            }
        }

        #endregion IResolvable<Strategy_t> Members
    }
}
