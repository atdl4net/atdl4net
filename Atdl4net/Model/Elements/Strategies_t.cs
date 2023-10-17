﻿#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System.Collections.Generic;
using Atdl4net.Fix;
using Atdl4net.Model.Collections;

namespace Atdl4net.Model.Elements
{
    /// <summary>
    /// Represents the Strategies element within a FIXatdl file.
    /// </summary>
    public class Strategies_t : IEnumerable<Strategy_t>
    {
        private readonly StrategyCollection _strategies;
        private readonly EditCollection _edits = new EditCollection();

        /// <summary>
        /// Initializes a new <see cref="Strategies_t"/> instance.
        /// </summary>
        public Strategies_t()
        {
            _strategies = new StrategyCollection(this);
        }

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

        /// <summary>
        /// Gets the number of <see cref="Strategy_t"/> instances in this collection.
        /// </summary>
        public int Count { get { return _strategies.Count; } }

        /// <summary>
        /// Gets the strategy identified by the supplied name.
        /// </summary>
        /// <param name="name">Name of strategy.</param>
        /// <returns><see cref="Strategy_t"/> that has the specified name.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the specified name does not match a valid strategy.</exception>
        public Strategy_t this[string name] { get { return _strategies[name]; } }

        /// <summary>
        /// Provides access to the underlying collection of strategies - used primarily for deserialization purposes.
        /// </summary>
        public StrategyCollection Strategies { get { return _strategies; } }

        /// <summary>
        /// Gets the global <see cref="Edit_t">Edits</see> for this collection of strategies.
        /// </summary>
        public EditCollection Edits { get { return _edits; } }

        /// <summary>
        /// Resolves all interdependencies e.g. edits to edit refs, control values to edits, etc.  Called once
        /// all strategies have been loaded as there may be dependencies on EditRefs at the global level.
        /// </summary>
        public void ResolveAll()
        {
            foreach (Strategy_t strategy in this)
            {
                strategy.Controls.ResolveAll();

                strategy.StrategyEdits.ResolveAll(strategy);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through all the <see cref="Strategy_t"/> instances in this collection.
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
        public IEnumerator<Strategy_t> GetEnumerator()
        {
            return _strategies.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through all the <see cref="Strategy_t"/> instances in this collection.
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _strategies.GetEnumerator();
        }
    }
}
