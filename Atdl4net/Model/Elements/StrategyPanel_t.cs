#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Collections.Specialized;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Elements.Support;
using Atdl4net.Model.Enumerations;
using Atdl4net.Utility;

namespace Atdl4net.Model.Elements
{
    public class StrategyPanel_t : IParentable<StrategyPanel_t>, IDisposable, IStrategyPanel
    {
        private readonly Strategy_t _owningStrategy;
        private StrategyPanel_t _owningStrategyPanel;
        private readonly StrategyPanelCollection _strategyPanels;
        private ControlCollection _controls;

        public Border_t? Border { get; set; }
        public bool? Collapsed { get; set; }
        public bool? Collapsible { get; set; }
        public string Color { get; set; }
        public Orientation_t? Orientation { get; set; }
        public string Title { get; set; }

        // Single parameter constructor needed for root StrategyPanel_t.
        public StrategyPanel_t(Strategy_t owner) : this(owner, null) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="parent">; null if this StrategyPanel_t does not have a parent (for example, because it is the
        /// immediate descendent of a StrategyLayout_t.</param>
        /// <remarks></remarks>
        public StrategyPanel_t(Strategy_t owningStrategy, IStrategyPanel parent)
        {
            _owningStrategy = owningStrategy;
            _owningStrategyPanel = parent as StrategyPanel_t;

            // Set defaults
            Collapsed = true;
            Collapsible = false;

            _strategyPanels = new StrategyPanelCollection();
        }

        public Strategy_t OwningStrategy { get { return _owningStrategy; } }

        public StrategyPanelCollection StrategyPanels { get { return _strategyPanels; } }

        public ControlCollection Controls
        {
            get
            {
                // Lazy initialisation as we can't use 'this' pointer in constructor.
                if ( _controls == null)
                {
                    _controls = new ControlCollection(this);

                    // Provide a mechanism for the Controls collection of the Strategy_t (as opposed to the StrategyPanel_t) to be
                    // notified as controls are added to and removed from this StrategyPanel_t.
                    _controls.CollectionChanged += new NotifyCollectionChangedEventHandler(_owningStrategy.Controls.SourceCollectionChanged);
                }

                return _controls;
            }
        }

        #region IDisposable Members

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _owningStrategy != null)
            {
                Controls.CollectionChanged -= new NotifyCollectionChangedEventHandler(_owningStrategy.Controls.SourceCollectionChanged);
            }
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        #endregion

        #region IParentable<StrategyPanel_t> Members

        StrategyPanel_t IParentable<StrategyPanel_t>.Parent
        {
            get { return _owningStrategyPanel; }
            set { _owningStrategyPanel = value; }
        }

        #endregion
    }
}
