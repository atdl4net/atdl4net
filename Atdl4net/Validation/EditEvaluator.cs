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
using Atdl4net.Fix;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Elements;
using Atdl4net.Resources;
using Atdl4net.Utility;
using Common.Logging;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Validation
{
    // TODO: Implement IDisposable
    public abstract class EditEvaluator<T> : IResolvable<Strategy_t, T> where T : class, IValueProvider
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Validation");

        private Edit_t<T> _edit;
        private EditRef_t<T> _editRef;

        public HashSet<string> Sources
        {
            get
            {
                if (_editRef != null)
                    return _editRef.Sources;
                else if (_edit != null)
                    return _edit.Sources;

                throw ThrowHelper.New<InvalidOperationException>(this, ErrorMessages.NeitherEditNorEditRefSetOnObject, this.GetType().Name);
            }
        }

        public bool CurrentState
        {
            get
            {
                if (_edit != null)
                    return _edit.CurrentState;
                else if (_editRef != null)
                    return _editRef.CurrentState;

                throw ThrowHelper.New<InvalidOperationException>(this, ErrorMessages.NeitherEditNorEditRefSetOnObject, this.GetType().Name);
            }
        }

        public EditRef_t<T> EditRef
        {
            get { return _editRef; }

            set
            {
                if (_edit != null)
                    throw ThrowHelper.New<InvalidOperationException>(this, ErrorMessages.BothEditAndEditRefSetOnObject, this.GetType().Name);

                _editRef = value;
            }
        }

        public Edit_t<T> Edit
        {
            get { return _edit; }

            set
            {
                if (_editRef != null)
                    throw ThrowHelper.New<InvalidOperationException>(this, ErrorMessages.BothEditAndEditRefSetOnObject, this.GetType().Name);

                _edit = value;
            }
        }

        /// <summary>
        /// Evaluates based on the current field values and any additional FIX field values that this EditEvaluator
        /// references.
        /// </summary>
        /// <param name="additionalValues">Any additional FIX field values that may be required in the Edit evaluation.</param>
        public void Evaluate(FixFieldValueProvider additionalValues)
        {
            _log.Debug(m => m("EditEvaluator evaluating state of Edit_t/EditRef_t; current state is {0}", CurrentState.ToString().ToLower()));

            if (_edit != null)
                _edit.Evaluate(additionalValues);
            else if (_editRef != null)
                _editRef.Evaluate(additionalValues);
            else
                throw ThrowHelper.New<InvalidOperationException>(this, ErrorMessages.NeitherEditNorEditRefSetOnObject, this.GetType().Name);

            _log.Debug(m => m("EditEvaluator evaluated to state {0}", CurrentState.ToString().ToLower()));
        }

        #region IResolvable<Strategy_t> Members

        /// <summary>
        /// Resolves all interdependencies e.g. edits to edit refs, control values to edits, etc.  Called once
        /// all strategies have been loaded as there may be dependencies on EditRefs at the global level.
        /// </summary>
        void IResolvable<Strategy_t, T>.Resolve(Strategy_t strategy, ISimpleDictionary<T> sourceCollection)
        {
            if (_editRef != null)
                (_editRef as IResolvable<Strategy_t, T>).Resolve(strategy, sourceCollection);
            else if (_edit != null)
                (_edit as IResolvable<Strategy_t, T>).Resolve(strategy, sourceCollection);
        }

        #endregion IResolvable<Strategy_t> Members
    }
}

