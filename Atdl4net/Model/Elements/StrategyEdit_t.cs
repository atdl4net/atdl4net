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
using Atdl4net.Model.Elements.Support;
using Atdl4net.Validation;

namespace Atdl4net.Model.Elements
{
    /// <summary>
    /// Represents the FIXatdl StrategyEdit element, which is a definition of a validation rule. A StrategyEdit element must 
    /// contain an Edit element as a child. The boolean expression described by the Edit element is an assertion, 
    /// i.e., validation succeeds if the condition described by the Edit is true and fails when the condition described by 
    /// the Edit element is false. In the case where validation fails, the error message, supplied by the errorMsg attribute 
    /// of the StrategyEdit, may be displayed to an OMS user or logged.
    /// </summary>
    public class StrategyEdit_t : EditEvaluator<IParameter>
    {
        private readonly string _internalId = Guid.NewGuid().ToString();

        /// <summary>
        ///  Gets the internal ID for this StrategyEdit; used to support lookups when applying the results of validations to
        ///  controls.
        /// </summary>
        public string InternalId { get { return _internalId; } }

        /// <summary>
        /// Gets/sets the error message to display when the boolean expression defined by StrategyEdit/Edit evaluates to False.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
