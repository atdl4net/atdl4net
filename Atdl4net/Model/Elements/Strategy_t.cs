﻿#region Copyright (c) 2010-2012, Cornerstone Technology Limited. http://atdl4net.org
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
using System.Linq;
using Atdl4net.Fix;
using Atdl4net.Model.Collections;
using Atdl4net.Utility;
using Atdl4net.Validation;
using Atdl4net.Wpf.ViewModel;

namespace Atdl4net.Model.Elements
{
    /// <summary>
    /// Represents the FIXatdl Strategy element.
    /// </summary>
    public class Strategy_t : IParentable<Strategies_t>
    {
        private readonly ReadOnlyControlCollection _controls;
        private readonly StrategyEditCollection _strategyEdits = new StrategyEditCollection();
        private readonly ParameterCollection _parameters = new ParameterCollection();
        private readonly EditCollection _edits = new EditCollection();
        private readonly MarketCollection _markets = new MarketCollection();
        private readonly RegionCollection _regions = new RegionCollection();
        private readonly SecurityTypeCollection _securityTypes = new SecurityTypeCollection();

        /// <summary>
        /// Initializes a new <see cref="Strategy_t"/> instance.
        /// </summary>
        public Strategy_t()
        {
            _controls = new ReadOnlyControlCollection(this);
        }

        /// <summary>
        /// Gets a read-only list of the controls for this Strategy.
        /// </summary>
        public ReadOnlyControlCollection Controls { get { return _controls; } }

        /// <summary>
        /// Gets/sets a description for this Strategy.
        /// </summary>
        public Description_t Description { get; set; }

        /// <summary>
        /// Gets/sets the URL of a disclosure document supplied by the algorithm provider.
        /// </summary>
        public string DisclosureDoc { get; set; }

        /// <summary>
        /// Gets the collection of Edits for this strategy.  Edits at this level are available to be used in either the
        /// StateRules and/or the StrategyEdits for this Strategy.
        /// </summary>
        public EditCollection Edits { get { return _edits; } }

        /// <summary>
        /// Gets/sets the FIX message to use when transmitting the order that this Strategy relates to. Values are taken from FIX tag 35 and
        /// may be one of "D" (NewOrder-Single), "E" (NewOrder-List), "AB" (NewOrder-Multileg), or "s" (NewOrder-Cross).
        /// </summary>
        public string FixMsgType { get; set; }

        /// <summary>
        /// Gets/sets the file path or URL of an image file or logo for this particular strategy.
        /// </summary>
        public string ImageLocation { get; set; }

        /// <summary>
        /// This element defines the markets/exchanges (by ISO 10383 MIC Code) to which the strategy is applicable. If no 
        /// Markets element is defined then the strategy is applicable for ALL markets. If a market is defined and has its 
        /// 'inclusion' attribute set to "Include", then it is implied that the strategy is applicable for ONLY that market.  
        /// If a market is defined and is set to "Exclude", then it is implied that the strategy is applicable for all 
        /// markets EXCEPT that market.<br/>
        /// Include takes precedence over Exclude - for example, if XNAS is defined and set to "Include" and XLON is defined 
        /// and set to "Exclude" then all other markets will also be excluded since the "Include" on XNAS takes precedence 
        /// over the "Exclude" on XLON.  In this example, the definition of XLON as "Exclude" is unnecessary.  Markets are used 
        /// in conjunction with regions and countries to define the scope of the strategy.  Markets take precedence over 
        /// regions and countries.  For example, if AsiaPacificJapan is defined as "Exclude" but the Fukuoka Stock Exchange 
        /// (XFKA) is defined as an included market, the strategy will be applicable for all markets in The Americas and EMEA,
        /// as well as only the Fukuoka Stock Exchange in the APAC region.
        /// </summary>
        public MarketCollection Markets { get { return _markets; } }

        /// <summary>
        /// Gets/sets the unique identifier of a Strategy. Strategy names must be unique per provider.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets/sets the tag which contains the sequence number of a particular order of a basket.
        /// </summary>
        public FixTag? OrderSequenceTag { get; set; }

        /// <summary>
        /// Gets the collection of Parameters for this Strategy.
        /// </summary>
        public ParameterCollection Parameters
        {
            get { return _parameters; }
        }

        /// <summary>
        /// Gets/sets a string that identifies the firm providing the algorithm.
        /// </summary>
        public string ProviderId { get; set; }

        /// <summary>
        /// Gets/sets a string that provides a further level of firm identification.
        /// </summary>
        public string ProviderSubId { get; set; }

        /// <summary>
        /// Gets the Regions that this Strategy pertains to.
        /// </summary>
        public RegionCollection Regions { get { return _regions; } }

        /// <summary>
        /// Gets/sets the group of Parameter elements that are intended for use with multi-leg or basket strategies.
        /// Parameters contained within a RepeatingGroup element are intended to have their tag=value pairs populated 
        /// in either the ListOrdGrp repeating group of a New Order List message or the LegOrdGrp repeating group of a 
        /// New Order Multileg message.  Parameters not contained within a RepeatingGroup element have their values 
        /// populated in the main body of a message.
        /// </summary>
        public RepeatingGroup_t RepeatingGroup { get; set; }

        /// <summary>
        /// Gets the list of security types (by SecurityType (tag 167)) for which this Strategy is valid. The absence 
        /// of any security types implies that the strategy is valid for all security types.
        /// </summary>
        public SecurityTypeCollection SecurityTypes { get { return _securityTypes; } }

        /// <summary>
        /// Gets/sets the prefix portion of a URL used to access the order or draft at the target 
        /// e.g. https://xyz.com/algo/dashboard?SenderCompID= - an OMS can append to this the specific SenderCompID 
        /// string, an ampersand "ClOrdID=" and the specific ClOrdID-string. Trader hits this full URL to communicate 
        /// regarding the order or draft.  See additional documentation.
        /// </summary>
        public string SentOrderLink { get; set; }

        /// <summary>
        /// Gets the collection of <see cref="StrategyEdit_t">StrategyEdits</see> for validating the output of this Strategy.
        /// </summary>
        public StrategyEditCollection StrategyEdits { get { return _strategyEdits; } }

        /// <summary>
        /// Gets/sets the StrategyLayout for this Strategy, which itself contains the root StrategyPanel for displaying
        /// the Strategy.
        /// </summary>
        public StrategyLayout_t StrategyLayout { get; set; }

        /// <summary>
        /// Gets/sets a field that denotes number of repeating legs; used when msgType is AB.
        /// </summary>
        public NumInGroup? TotalLegs { get; set; }

        /// <summary>
        /// Gets/sets a field that denotes the number of repeating orders in a NewOrder-List message or a basket of 
        /// NewOrder-Single messages.
        /// </summary>
        public NumInGroup? TotalOrders { get; set; }

        /// <summary>
        /// Gets/sets the name of the strategy as rendered in the user interface (UI). If not provided then the "name" attribute should 
        /// be used. (This is the value rendered on the UI when the user is presented with a choice of algorithms.)
        /// </summary>
        public string UiRep { get; set; }

        /// <summary>
        /// Gets/sets information to facilitate version control
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets/sets the value used to identify the algorithm. The tag referred to by <see cref="Strategies_t.StrategyIdentifierTag"/>
        /// at the Strategies level within the FIXatdl file will be set to this value.
        /// </summary>
        public string WireValue { get; set; }

        /// <summary>
        /// Loads the initial control values, either from the control's initValue attribute, or using the
        /// FIX_ mechanism in conjunction with the supplied initial FIX values.
        /// </summary>
        /// <param name="controlInitValueProvider">Initial value provider that provides access to the initial FIX field values,
        /// needed to allow control values to be initialised using the FIX_ mechanism .</param>
        public void LoadInitialControlValues(FixFieldValueProvider controlInitValueProvider)
        {
            Controls.LoadDefaults(controlInitValueProvider);
        }

        /// <summary>
        /// Updates the values of each control within this strategy from its respective parameter.
        /// </summary>
        /// <param name="controlInitValueProvider">Initial value provider that provides access to the initial FIX field values,
        /// needed to allow control values to be initialised using the FIX_ mechanism .</param>
        public void UpdateControlValuesFromParameters(FixFieldValueProvider controlInitValueProvider)
        {
            Controls.UpdateValuesFromParameters(Parameters);
        }

        /// <summary>
        /// Loads this strategy's parameters with the supplied FIX values.
        /// </summary>
        /// <param name="controlInitValueProvider"><see cref="FixFieldValueProvider"/> providing the FIX values to initialize from.</param>
        /// <param name="resetExistingValues">Set to true if each parameter value is to be reset if its value is specified in
        /// inputValues; set to false to leave the parameter value unchanged.</param>
        public void LoadParameterValues(FixFieldValueProvider controlInitValueProvider, bool resetExistingValues)
        {
            Parameters.LoadInitialValues(controlInitValueProvider.FixValues, resetExistingValues);
        }

        /// <summary>
        /// Evaluate all the <see cref="StrategyEdit_t">StrategyEdit</see>s for this strategy.
        /// </summary>
        /// <param name="inputValueProvider">Provider that providers access to any additional FIX field values that may 
        /// be required in the Edit evaluation.</param>
        /// <param name="shortCircuit">If true, this method returns as soon as any error is found; if false, all StrategyEdits
        /// are evaluated before the method returns.</param>
        public bool EvaluateAllStrategyEdits(IInitialFixValueProvider inputValueProvider, bool shortCircuit)
        {
            FixFieldValueProvider additionalValues = inputValueProvider == null ?
                FixFieldValueProvider.Empty : new FixFieldValueProvider(inputValueProvider, Parameters);

            return _strategyEdits.EvaluateAll(additionalValues, shortCircuit);
        }

        /// <summary>
        /// Attempts to updates the parameter values from the controls in this strategy.
        /// </summary>
        /// <param name="shortCircuit">If true, this method returns as soon as any error is found; if false, an attempt is made to update all parameter
        /// values before the method returns.</param>
        /// <param name="validationResults">If one or more validations fail, this parameter contains a list of ValidationResults; null otherwise.</param>
        public bool TryUpdateParameterValuesFromControls(bool shortCircuit, out IList<ValidationResult> validationResults)
        {
            return Controls.TryUpdateParameterValues(Parameters, shortCircuit, out validationResults);
        }

        /// <summary>
        /// Resets all the parameters and controls within this strategy.
        /// </summary>
        public void Reset()
        {
            Parameters.ResetAll();
            Controls.ResetAll();
        }


        #region IParentable<Strategies_t> Members

        public Strategies_t Parent { get; set; }

        #endregion
    }
}
