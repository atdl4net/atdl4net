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
using Atdl4net.Diagnostics;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Enumerations;
using Atdl4net.Resources;
using Atdl4net.Utility;
using Common.Logging;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Elements
{
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

    public interface IValueProvider
    {
        object GetValue();
    }

    /// <summary>
    /// Represents a FIXatdl Edit_t.
    /// </summary>
    public class Edit_t<T> : IEdit_t<T>, IResolvable<Strategy_t, T>, IKeyedObject where T : class, IValueProvider
    {
        private static readonly ILog _log = LogManager.GetLogger("EditEvaluation");

        private bool _currentState;
        private T _fieldSource;
        private T _field2Source;
        private EditEvaluatingCollection<T> _edits = new EditEvaluatingCollection<T>();
        private EditRefCollection<T> _editRefs;

        public Edit_t()
        {
            (this as IKeyedObject).RefKey = RefKeyGenerator.GetNextKey(typeof(Edit_t));

            _log.DebugFormat("New Edit_t created as Edit[{0}].", (this as IKeyedObject).RefKey);
        }

        #region IEdit_t Members

        public string Field { get; set; }
        public string Field2 { get; set; }
        public string Id { get; set; }
        public Operator_t? Operator { get; set; }
        public string Value { get; set; }

        public bool CurrentState { get { return _currentState; } }

        public EditEvaluatingCollection<T> Edits { get { return _edits; } }

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

        public LogicOperator_t? LogicOperator
        {
            get { return Edits.LogicOperator; }
            set { Edits.LogicOperator = value; }
        }

        public object FieldValue
        {
            get
            {
                if (_fieldSource != null)
                    return _fieldSource.GetValue();
                else
                    throw ThrowHelper.New<InvalidOperationException>(this, "Edit attempted to access FieldValue but requisite control was not set.");
            }
        }

        public object Field2Value
        {
            get
            {
                if (_field2Source != null)
                    return _field2Source.GetValue();
                else
                    throw ThrowHelper.New<InvalidOperationException>(this, "Edit attempted to access Field2Value but requisite control was not set.");
            }
        }

        public void Evaluate()
        {
            _log.DebugFormat("Evaluating Edit[{0}]; current state = {1}.",
                (this as IKeyedObject).RefKey, _currentState);

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

            _log.DebugFormat("Evaluation of Edit[{0}] yielded {1}.",
                (this as IKeyedObject).RefKey, _currentState);
        }

        #endregion IEdit_t Members

        private bool EvaluateExists()
        {
            _log.DebugFormat("Evaluating Edit[{0}] with Operator = '{1}'; field value = '{2}'.",
                (this as IKeyedObject).RefKey, Operator.ToString(), FieldValue);

            bool result = false;

            if (FieldValue is System.String)
                result = string.IsNullOrEmpty(FieldValue as string);
            else
                result = (FieldValue != null);

            bool finalResult = (Operator == Operator_t.Exist) ? result : !result;

            return finalResult;
        }

        private bool EvaluateComparison()
        {
            _log.DebugFormat("Evaluating Edit[{0}] with Operator = '{1}'; field value = '{2}', Value = '{3}', field2 value = '{4}'.",
                (this as IKeyedObject).RefKey, Operator.ToString(), FieldValue, Value, Field2 != null ? Field2Value : "N/A");

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
                    operand2 = Value as IComparable;
            }
            else
                operand2 = Field2Value as IComparable;

            int compareResult;

            if (FieldValue == null)
                compareResult = (operand2 == null || object.Equals(operand2, Control_t.NullValue)) ? 0 : 1;
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

            return finalResult;
        }

        #region IResolvable<Strategy_t> Members

        void IResolvable<Strategy_t, T>.Resolve(Strategy_t strategy, IDictionary<T> sourceCollection)
        {
            (_edits as IResolvable<Strategy_t, T>).Resolve(strategy, sourceCollection);

            if (!string.IsNullOrEmpty(Field) && !Field.StartsWith("FIX_"))
            {
                if (sourceCollection.Contains(Field))
                    _fieldSource = sourceCollection[Field] as T;
                else
                    throw ThrowHelper.New<ReferencedObjectNotFoundException>(this, ErrorMessages.EditRefFieldControlNotFound, Field, "Field");
            }

            if (!string.IsNullOrEmpty(Field2) && !Field2.StartsWith("FIX_"))
            {
                if (sourceCollection.Contains(Field2))
                    _field2Source = sourceCollection[Field2] as T;
                else
                    throw ThrowHelper.New<ReferencedObjectNotFoundException>(this, ErrorMessages.EditRefFieldControlNotFound, Field2, "Field2");
            }
        }

        #endregion IResolvable<Strategy_t> Members

        #region IKeyedObject Members

        string IKeyedObject.RefKey { get; set; }

        #endregion IKeyedObject Members
    }
}
