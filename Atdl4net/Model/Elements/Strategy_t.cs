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
using Atdl4net.Fix;
using Atdl4net.Model.Collections;
using Atdl4net.Utility;
using Common.Logging;

namespace Atdl4net.Model.Elements
{
    public class Strategy_t : IParentable<Strategies_t>, IKeyedObject
    {
        private static readonly ILog _log = LogManager.GetLogger("Model");

        private readonly ParameterCollection _parameters = new ParameterCollection();
        private ReadOnlyControlCollection _controls;
        private EditCollection _edits = new EditCollection();
        private FixTagValuesCollection _inputValues;
        private MarketCollection _markets = new MarketCollection();
        private string _name;
        private RegionCollection _regions = new RegionCollection();
        private SecurityTypeCollection _securityTypes = new SecurityTypeCollection();
        private StrategyEditCollection _strategyEdits;

        public Strategy_t()
        {
            (this as IKeyedObject).RefKey = RefKeyGenerator.GetNextKey(typeof(Strategy_t));

            _log.DebugFormat("New Strategy_t created as Strategy[{0}].", (this as IKeyedObject).RefKey);
        }

        public ReadOnlyControlCollection Controls
        {
            get
            {
                // Lazy initialise as 'this' cannot be used in constructor.
                if (_controls == null)
                    _controls = new ReadOnlyControlCollection(this);

                return _controls;
            }
        }

        public Description_t Description { get; set; }

        public string DisclosureDoc { get; set; }

        public EditCollection Edits
        {
            get
            {
                return _edits;
            }
        }

        public string FixMsgType { get; set; }

        public string ImageLocation { get; set; }

        public FixTagValuesCollection InputValues 
        {
            get { return _inputValues; }
            set
            {
                _inputValues = value;

                Parameters.InitializeValues(_inputValues);

                Controls.UpdateValuesFromParameters(Parameters);
            }
        }

        public MarketCollection Markets
        {
            get { return _markets; }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _log.DebugFormat("Strategy[{0}] Name='{1}'.", (this as IKeyedObject).RefKey, value);

                _name = value;
            }
        }

        public FixTag? OrderSequenceTag { get; set; }

        public FixTagValuesCollection GetOutputValues()
        {
            Controls.UpdateParameterValues(Parameters);

            StrategyEdits.ValidateAll();

            return Parameters.GetOutputValues();
        }

        public ParameterCollection Parameters
        {
            get { return _parameters; }
        }

        public string ProviderId { get; set; }

        public string ProviderSubId { get; set; }

        public RegionCollection Regions
        {
            get { return _regions; }
        }

        public RepeatingGroup_t RepeatingGroup { get; set; }

        public SecurityTypeCollection SecurityTypes
        {
            get { return _securityTypes; }
        }

        public string SentOrderLink { get; set; }

        public StrategyEditCollection StrategyEdits
        {
            get
            {
                // Lazy initialise as 'this' cannot be used in constructor.
                if (_strategyEdits == null)
                    _strategyEdits = new StrategyEditCollection(this);

                return _strategyEdits;
            }
        }

        public StrategyLayout_t StrategyLayout { get; set; }

        public NumInGroup? TotalLegs { get; set; }

        public NumInGroup? TotalOrders { get; set; }

        public string UiRep { get; set; }

        public string Version { get; set; }

        public string WireValue { get; set; }

        #region IParentable<Strategies_t> Members

        public Strategies_t Parent { get; set; }

        #endregion

        #region IKeyedObject Members

        string IKeyedObject.RefKey { get; set; }

        #endregion IKeyedObject Members
    }
}
