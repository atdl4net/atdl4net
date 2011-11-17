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
using Atdl4net.Diagnostics;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Elements;
using Atdl4net.Resources;
using Atdl4net.Utility;
using Common.Logging;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Validation
{
    // TODO: Implement IDisposable
    public class EditEvaluator<T> : IResolvable<Strategy_t, T>, IKeyedObject where T : class, IValueProvider
    {
        private static readonly ILog _log = LogManager.GetLogger("EditEvaluation");

        private const string ObjectDescription = "collection of Edits";

        private Edit_t<T> _edit;
        private EditRef_t<T> _editRef;

        public bool CurrentState
        {
            get
            {
                if (_edit != null)
                    return _edit.CurrentState;
                else if (_editRef != null)
                    return _editRef.CurrentState;

                throw ThrowHelper.New<InvalidOperationException>(this, ErrorMessages.NeitherEditNorEditRefSetOnObject, ObjectDescription);
            }
        }

        public EditRef_t<T> EditRef
        {
            get { return _editRef; }

            set
            {
                if (_edit != null)
                    throw ThrowHelper.New<InvalidOperationException>(this, ErrorMessages.BothEditAndEditRefSetOnObject, ObjectDescription);

                _editRef = value;

                _log.DebugFormat("EditRef[{0}] associated with EditEvaluator[{1}].",
                    (value as IKeyedObject).RefKey, (this as IKeyedObject).RefKey);
            }
        }

        public Edit_t<T> Edit
        {
            get { return _edit; }

            set
            {
                if (_editRef != null)
                    throw ThrowHelper.New<InvalidOperationException>(this, ErrorMessages.BothEditAndEditRefSetOnObject, ObjectDescription);

                _edit = value;

                _log.DebugFormat("Edit[{0}] associated with EditEvaluator[{1}].",
                    (value as IKeyedObject).RefKey, (this as IKeyedObject).RefKey);
            }
        }

        public void Evaluate()
        {
            if (_edit != null)
                _edit.Evaluate();
            else if (_editRef != null)
                _editRef.Evaluate();
            else
                throw ThrowHelper.New<InvalidOperationException>(this, ErrorMessages.NeitherEditNorEditRefSetOnObject, ObjectDescription);
        }

        #region IResolvable<Strategy_t> Members

        void IResolvable<Strategy_t, T>.Resolve(Strategy_t strategy, IDictionary<T> sourceCollection)
        {
            if (_editRef != null)
                (_editRef as IResolvable<Strategy_t, T>).Resolve(strategy, sourceCollection);
            else if (_edit != null)
                (_edit as IResolvable<Strategy_t, T>).Resolve(strategy, sourceCollection);
        }

        #endregion IResolvable<Strategy_t> Members

        #region IKeyedObject Members

        string IKeyedObject.RefKey { get; set; }

        #endregion IKeyedObject Members
    }
}

