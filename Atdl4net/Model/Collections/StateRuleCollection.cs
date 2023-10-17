#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System.Collections.ObjectModel;
using Atdl4net.Fix;
using Atdl4net.Model.Elements;
using Atdl4net.Utility;
using Common.Logging;

namespace Atdl4net.Model.Collections
{
    public class StateRuleCollection : Collection<StateRule_t>
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Model.Collections");

        private readonly Control_t _owner;

        public StateRuleCollection(Control_t owner)
        {
            _owner = owner;
        }

        public new void Add(StateRule_t item)
        {
            (item as IParentable<Control_t>).Parent = _owner;

            base.Add(item);

            _log.Debug(m=>m("StateRule_t {0} added to StateRules for control Id {1}", item.ToString(), _owner.Id));
        }

        public void EvaluateAll()
        {
            if (this.Items.Count > 0)
                _log.Debug(m => m("Evaluating all {0} StateRule_t instances for control Id {1}", Items.Count,  _owner.Id));

            foreach (StateRule_t rule in this.Items)
                rule.Evaluate();
        }

        /// <summary>
        /// Resolves all edit refs, connects all edits to their controls.
        /// </summary>
        /// <param name="strategy"></param>
        public void ResolveAll(Strategy_t strategy)
        {
            foreach (StateRule_t rule in this.Items)
                (rule as IResolvable<Strategy_t, Control_t>).Resolve(strategy, strategy.Controls);
        }
    }
}
