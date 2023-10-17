#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System.Collections.Generic;
using Atdl4net.Fix;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Enumerations;

namespace Atdl4net.Model.Elements.Support
{
    // 
    /// <summary>
    /// Interface to allow both Edits and EditRefs to be put in the Edit collections.  The type parameter
    /// specifies whether the Edit relates to a StateRule or to a StrategyEdit.
    /// </summary>
    /// <typeparam name="T">One of <see cref="Control_t"/> or <see cref="Parameter_t"/>.</typeparam>
    public interface IEdit<T>
    {
        /// <summary>
        /// Gets/sets the name of field to be used as left hand side of the evaluation.
        /// </summary>
        string Field { get; set; }

        /// <summary>
        /// Gets/sets the name of second (optional) field, to be used as the right hand side of the evaluation.
        /// </summary>
        string Field2 { get; set; }

        /// <summary>
        /// Gets/sets the optional ID for this Edit.
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Gets/sets the optional operator - used when comparing two values.
        /// </summary>
        Operator_t? Operator { get; set; }

        /// <summary>
        /// Gets/sets the optional logical operator - used when combining two or more Edits.
        /// </summary>
        LogicOperator_t? LogicOperator { get; set; }

        /// <summary>
        /// Gets/sets the optional fixed value to be used as the right hand side of the evaluation.
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// Gets the collection of child Edits.  May be empty, unless LogicOperator is non-null.
        /// </summary>
        EditEvaluatingCollection<T> Edits { get; }

        /// <summary>
        /// Gets the current state of this Edit based on the most recent evaluation.
        /// </summary>
        bool CurrentState { get; }

        /// <summary>
        /// Gets the current value of the field pointed to by the Field property.
        /// </summary>
        object FieldValue { get; }

        /// <summary>
        /// Gets the current value of the field pointed to by the Field2 property.
        /// </summary>
        object Field2Value { get; }

        /// <summary>
        /// Evaluates this Edit based on the current field values and any additional FIX field values that this
        /// Edit references.
        /// </summary>
        /// <param name="additionalValues">Any additional FIX field values that may be required in the Edit evaluation.</param>
        void Evaluate(FixFieldValueProvider additionalValues);

        /// <summary>
        /// Gets the set of sources for the data to be evaluated as part of this Edit.
        /// </summary>
        HashSet<string> Sources { get; }
    }
}
