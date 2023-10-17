#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
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
        /// references.  Used for evaluating Edits in the context of StrategyEdits.
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

        /// <summary>
        /// Evaluates based on the current field values.  Used for evaluating Edits in the context of StateRules.
        /// </summary>
        public void Evaluate()
        {
            _log.Debug(m => m("EditEvaluator evaluating state of Edit_t/EditRef_t; current state is {0}", CurrentState.ToString().ToLower()));

            if (_edit != null)
                _edit.Evaluate();
            else if (_editRef != null)
                _editRef.Evaluate();
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

