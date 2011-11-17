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

using Atdl4net.Diagnostics;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Controls;
using Atdl4net.Model.Enumerations;
using Atdl4net.Model.Types;
using Atdl4net.Utility;
using System;

namespace Atdl4net.Model.Elements
{
    /// <summary>
    /// Base class for all concrete <see cref="Control_t{T}">Control&lt;T&gt;</see> types.
    /// </summary>
    public abstract class Control_t : IParentable<StrategyPanel_t>, IKeyedObject, IValueProvider
    {
        public const string NullValue = "{NULL}";

        private StrategyPanel_t _owner;
        private StateRuleCollection _stateRules;

        protected IParameter_t ReferencedParameter { get; set; }

        /// <summary>For implementing systems that support saving order templates or pre-populated orders for basket trading/list
        ///  trading this attribute specifies that the control should be disabled when the order screen is going to be saved as a
        ///  template and not actually used to place an order.</summary>
        public bool? DisableForTemplate { get; set; }

        /// <summary>Unique identifier of this control. No two controls of the same strategy can have the same ID.</summary>
        public string Id { get; set; }

        /// <summary>Zero-based index for this control within a StrategyPanel_t.  For example, if a StrategyPanel_t has three controls,
        /// the first would have index of 0, the second 1 and the third 2.</summary>
        public int Index { get; set; }

        /// <summary>Indicates the initialization value is to be taken from this standard FIX field. Format: "FIX_" + FIXFieldName. 
        /// E.g. "FIX_OrderQty".  Required when initPolicy=”UseFixField”.</summary>
        public string InitFixField { get; set; }

        /// <summary>Describes how to initialize the control.  If the value of this attribute is undefined or equal to "UseValue" and
        ///  initValue is defined then initialize with initValue.  If the value is equal to "UseFixField" then attempt to initialize 
        /// with the value of the tag specified in initFixField. If the value is equal to "UseFixField" and it is not possible to 
        /// access the value of the specified fix tag then revert to using initValue. If the value is equal to "UseFixField", the 
        /// field is not accessible, and initValue is not defined, then do not initialize.</summary>
        public InitPolicy_t? InitPolicy { get; set; }

        /// <summary>A title for this control which may be displayed.</summary>
        public string Label { get; set; }

        /// <summary>The name of the parameter for which this control gives the visual representation. A parameter with this name 
        /// must be defined within the same strategy as this control.</summary>
        public string ParameterRef { get; set; }

        /// <summary>Tool tip text for rendered GUI objects rendered for the parameter.</summary>
        public string Tooltip { get; set; }

        /// <summary>Indicates the type of GUI control that should be rendered on the screen.  Absence of this attribute may indicate
        ///  that the parameter should not be visible to the user and the parameter’s initValue should be used to populate the FIX 
        /// message.</summary>
        //public string Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        protected Control_t(string id)
        {
            Id = id;

            (this as IKeyedObject).RefKey = RefKeyGenerator.GetNextKey(typeof(Control_t));
        }

        public abstract object GetValue();

        public abstract void SetValue(object newValue);

        public StateRuleCollection StateRules
        {
            get
            {
                // Perform lazy initialisation as we can't use 'this' in constructor.
                if (_stateRules == null)
                    _stateRules = new StateRuleCollection(this);

                return _stateRules;
            }
        }

        public abstract void LoadDefault();

        /// <summary>
        /// Adds support for the visitor pattern, enabling ...
        /// </summary>
        /// <param name="visitor"></param>
        public void DoVisit(IControl_tVisitor visitor)
        {
            ModelUtils.VisitHelper(visitor, this);
        }

        protected FixTagValuesCollection GetInputValues()
        {
            if (_owner != null && _owner.OwningStrategy != null)
                return _owner.OwningStrategy.InputValues;
            else
                return null;
        }

        #region IParentable<StrategyPanel_t> Members

        StrategyPanel_t IParentable<StrategyPanel_t>.Parent
        {
            get { return _owner; }
            set { _owner = value; }
        }

        #endregion

        #region IKeyedObject Members

        string IKeyedObject.RefKey { get; set; }

        #endregion IKeyedObject Members
    }

    /// <summary>
    /// Interface to support visitor pattern.
    /// (Thanks to Brad Wilson - http://www.agileprogrammer.com/dotnetguy/articles/ReflectionVisitor.aspx - for this tip.)
    /// </summary>
    public interface IControl_tVisitor
    {
        void Visit(Control_t control);
    }
}
