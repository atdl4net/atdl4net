#region Copyright (c) 2010, Cornerstone Technology Limited. http://atdl4net.org
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
//      License as published by the Free Software Foundation, version 3.
// 
//      Atdl4net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
//      of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU Lesser General Public License for more details.
//
//      You should have received a copy of the GNU Lesser General Public License along with Atdl4net.  If not, see
//      http://www.gnu.org/licenses/.
//
#endregion

using Atdl4net.Model.Collections;
using Atdl4net.Model.Enumerations;
using Atdl4net.Utility;
using System;
using System.Collections.Specialized;

namespace Atdl4net.Model.Elements
{
    public class StrategyPanel_t : IParentable<StrategyPanel_t>, IDisposable, IStrategyPanel
    {
        private Strategy_t _owningStrategy;
        private StrategyPanel_t _owningStrategyPanel;
        private StrategyPanelCollection _strategyPanels;
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
