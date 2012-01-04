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
