#region Atdl4net Sample Code - License and Use
//
//   This sample code is provided as part of Atdl4net, with the intention of making it easier to get started.
//
//   Whilst Atdl4net is itself made available under either a commercial or an open-source (LGPL) license, the
//   samples provided with Atdl4net are made available for use freely by anyone that obtains a copy of
//   Atdl4net, without restriction.
//
//   For the avoidance of doubt, you are at liberty to remove this statement from any sample code that you
//   adapt for your use, but in any case the following statement still applies:
//
//   The samples for Atdl4net are distributed in the hope that they will be useful, but WITHOUT ANY WARRANTY; 
//   without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
//
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using Atdl4net.Model.Elements;
using Atdl4net.Providers;

namespace Atdl4net.ExampleApplication.Models.Support
{
    /// <summary>
    /// Provides example FIXatdl strategies from embedded resources.
    /// </summary>
    public class ExampleStrategyProvider
    {
        private readonly Dictionary<string, string> _algoProviders;
        private readonly StrategyProvider _atdlStrategyProvider;

        /// <summary>
        /// Initializes a new <see cref="ExampleStrategyProvider"/>.
        /// </summary>
        public ExampleStrategyProvider()
        {
            _algoProviders = GetProviders();

            _atdlStrategyProvider = new StrategyProvider();

            LoadStrategies();
        }

        /// <summary>
        /// Gets the list of unique identifiers for the strategy providers (aka brokers).
        /// </summary>
        public IList<string> AlgorithmProviders { get { return _algoProviders.Keys.ToList<string>(); } }

        /// <summary>
        /// Gets the list of Strategy_t instances that are relevant to the specified algo provider.
        /// </summary>
        /// <param name="providerId">Algorithm provider ID.</param>
        /// <returns>List of relevant Strategy_t instances.</returns>
        public IList<Strategy_t> GetStrategiesByProvider(string providerId)
        {
            return _atdlStrategyProvider.GetStrategiesByProvider(providerId).ToList<Strategy_t>();
        }

        /// <summary>
        /// Gets a list the names of the available strategies for a given strategy provider (aka broker).
        /// </summary>
        /// <param name="providerId">Unique identifier for provider.</param>
        /// <returns><see cref="IList{string}"/> of the names of strategies for the specified provider.</returns>
        public IList<string> GetStrategyNamesForProvider(string providerId)
        {
            return (from p in _atdlStrategyProvider.GetStrategiesByProvider(providerId) 
                    select p.Name).ToList<string>();
        }

        /// <summary>
        /// Gets the named strategy that corresponds to the supplied strategy provider (aka broker).
        /// </summary>
        /// <param name="strategyIdentifier">Strategy identifier in the format ProviderId:StrategyName.</param>
        /// <returns><see cref="Strategy_t"/> that corresponds to the supplied provider identifier and name.</returns>
        public Strategy_t GetStrategy(string strategyIdentifier)
        {
            string[] strategyParts = strategyIdentifier.Split(':');

            return _atdlStrategyProvider.GetStrategyByName(strategyParts[0], strategyParts[1], true);
        }

        // Loads all the strategies from all the embedded XML resources. Also builds a list of strategy identifiers
        // in the format ProviderId:StrategyName.
        private void LoadStrategies()
        {
            foreach (KeyValuePair<string, string> algoProvider in _algoProviders)
                _atdlStrategyProvider.Load(algoProvider.Key, ThisAssembly.GetManifestResourceStream(algoProvider.Value));
        }

        // This method loops around all the strategy files we have embedded in this project and
        // pulls out the providerID value for the first strategy in the file.  This is used to identify
        // the provider thereafter.
        private Dictionary<string, string> GetProviders()
        {
            Dictionary<string, string> providers = new Dictionary<string, string>();

            foreach (string resource in GetEmbeddedResources("*.xml"))
            {
                XDocument document;

                using (XmlReader reader = XmlReader.Create(ThisAssembly.GetManifestResourceStream(resource)))
                {
                    document = XDocument.Load(reader);
                }

                XAttribute providerId = (from e in document.Descendants("{http://www.fixprotocol.org/FIXatdl-1-1/Core}Strategy") 
                                         select e).First().Attribute("providerID");

                providers.Add(providerId.Value, resource);
            }

            return providers;
        }

        // Finds all the embedded resources with names that match the supplied pattern.
        private static IList<string> GetEmbeddedResources(string pattern)
        {
            string[] resources = ThisAssembly.GetManifestResourceNames();

            return (from r in resources where r.EndsWith(pattern.Replace("*", string.Empty)) select r).ToList();
        }

        // Returns the executing assembly.
        private static Assembly ThisAssembly { get { return Assembly.GetExecutingAssembly(); } }
    }
}
