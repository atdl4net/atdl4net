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
using System.Linq;
using System.Text;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Model.Enumerations;
using Atdl4net.Model.Types.Support;
using Atdl4net.Resources;
using Atdl4net.Utility;
using Common.Logging;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Elements
{
    /// <summary>
    /// Represents the FIXatdl type Edit_t when it occurs outside of a StateRule_t or a StrategyEdit_t element.
    /// </summary>
    public class Edit_t
    {
        public string Field { get; set; }

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
        // Use Atdl4net.Model.Validation namespace rather than Atdl4net.Model.Elements for debugging purposes
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Model.Validation");

        private bool _currentState;
        private T _fieldSource;
        private T _field2Source;
        private readonly EditEvaluatingCollection<T> _edits = new EditEvaluatingCollection<T>();
        private EditRefCollection<T> _editRefs;

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

        public EditRefCollection<T> EditRefs
        {
            get
            {
                //Lazy initialize as 'this' cannot be used in constructor
                if (_editRefs == null)
                    _editRefs = new EditRefCollection<T>(_edits);

                return _editRefs;
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
        /// Evaluates this Edit based on the current field values.
        /// </summary>
        public void Evaluate()
        {
            _log.Debug(m => m("Evaluating Edit_t {0}; current state is {1}", ToString(), _currentState.ToString().ToLower()));

            if (Operator != null)
            {
                if (Operator == Operator_t.Exist || Operator == Operator_t.NotExist)
                    _currentState = EvaluateExists();
                else
                    _currentState = EvaluateComparison();
            }
            else if (LogicOperator != null)
            {
                _edits.Evaluate();

                _currentState = _edits.CurrentState;
            }
            else
                throw ThrowHelper.New<InvalidOperationException>(this, ErrorMessages.MissingOperatorsOnEdit);

            _log.Debug(m => m("Evaluation of Edit_t {0} yielded state of {1}", ToString(), _currentState.ToString().ToLower()));
        }

        #endregion IEdit_t Members

        #region IResolvable<Strategy_t> Members

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

        private bool EvaluateExists()
        {
            object fieldValue = FieldValue;

            bool empty = fieldValue == null ||  string.IsNullOrEmpty(fieldValue as string);

            bool result = (Operator == Operator_t.Exist) ? !empty : empty;

            _log.Debug(m => m("Evaluated whether FieldValue {0} has a value; result is {1}", fieldValue, result));

            return result;
        }

        private bool EvaluateComparison()
        {
            _log.Debug(m => m("Evaluating comparison operation of Edit_t {0}", this.ToString()));

            IComparable operand1 = FieldValue as IComparable;
            IComparable operand2;

            // If we are dealing with a boolean on the left hand side, and we're comparing with a fixed value
            // rather than another field, then we need to convert the supplied fixed value from a string to a bool.
            if (Value != null)
            {
                if (FieldValue is bool)
                    operand2 = Boolean.Parse(Value);
                else if (FieldValue is decimal)
                    operand2 = Convert.ToDecimal(Field2Value);
                else if (FieldValue is int)
                    operand2 = Convert.ToInt32(Field2Value);
                else
                    operand2 = Value;
            }
            else
                operand2 = Field2Value as IComparable;

            int compareResult;

            if (FieldValue == null)
                compareResult = (operand2 == null || object.Equals(operand2, Atdl.NullValue)) ? 0 : 1;
            else
                compareResult = operand1.CompareTo(operand2);

            bool finalResult = false;

            switch (Operator)
            {
                case Operator_t.Equal:
                    finalResult = compareResult == 0;
                    break;

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

                case Operator_t.NotEqual:
                    finalResult = compareResult != 0;
                    break;
            }

            _log.Debug(m => m("Compared values '{0}' and '{1}' as part of Edit_t evaluation; result was {2}", operand1, operand2, finalResult.ToString().ToLower()));

            return finalResult;
        }
    }
}
