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

using Atdl4net.Fix;
using Atdl4net.Model.Collections;
using Atdl4net.Utility;

namespace Atdl4net.Model.Elements
{
    /// <summary>
    /// ...
    /// </summary>
    public class Strategies_t
    {
        private StrategyCollection _strategies;
        private EditCollection _edits = new EditCollection();

        /// <summary>Indicates whether a new strategy can be chosen during a Cancel/Replace.</summary>
        public bool? ChangeStrategyOnCxlRpl { get; set; }

        public Description_t Description { get; set; }

        /// <summary>The tag within the FIX order message to be populated with a boolean ('Y'/'N') indicating
        /// whether the order is a draft.</summary>
        public FixTag? DraftFlagIdentifierTag { get; set; }

        /// <summary>Filepath or URL of an image file or logo of the algo providing firm.</summary>
        public string ImageLocation { get; set; }

        /// <summary>The tag within the FIX order message to be populated with a value identifying the chosen strategy.
        /// E.g. if strategyIdentifierTag is 5001 and the chosen strategy is identified by the value 'VWAP' then the 
        /// FIX order message would contain the tag-value pair 5001=VWAP.</summary>
        public FixTag StrategyIdentifierTag { get; set; }

        /// <summary>The tag within the FIX order message to be populated with a value identifying the version of a chosen
        /// strategy. For example, if versionIdentifierTag is 5002 and the version of the chosen strategy is '2.01' then 
        /// the FIX order message would contain the tag-value pair 5001=2.01</summary>
        public FixTag? VersionIdentifierTag { get; set; }

        /// <summary>Indicates whether the order recipient can receive algorithmic parameters in the StrategyParametersGrp
        /// component block, a repeating group starting at tag 957. If this mode of parameter transport is not supported 
        /// then the fixTag attribute of all Parameter elements is required.</summary>
        /// <remarks>Default value: false.</remarks>
        public bool? Tag957Support { get; set; }

        public StrategyCollection Strategies 
        { 
            get 
            {
                // Use lazy initialisation as it is not possible to use 'this' in class initialisation.
                if (_strategies == null)
                    _strategies = new StrategyCollection(this);

                return _strategies; 
            }
        }

        public EditCollection Edits { get { return _edits; } }

        /// <summary>
        /// Resolve all interdependencies e.g. edits to edit refs, control values to edits, etc.
        /// </summary>
        public void ResolveAll()
        {
            foreach (Strategy_t strategy in Strategies)
            {
                foreach (Control_t control in strategy.Controls)
                    control.StateRules.ResolveAll(strategy);

                foreach (StrategyEdit_t strategyEdit in strategy.StrategyEdits)
                    (strategyEdit as IResolvable<Strategy_t, IParameter_t>).Resolve(strategy, strategy.Parameters);
            }
        }
    }
}
