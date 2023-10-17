#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Configuration;
using System.Linq;
using Common.Logging;
using ConfigurationSectionHandler = Atdl4net.Configuration.ConfigurationSectionHandler;

namespace Atdl4net.Configuration
{
    /// <summary>
    /// Provides access to the configuration settings for Atdl4net.
    /// </summary>
    public class Atdl4netConfiguration
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net");

        private static readonly ConfigurationSectionHandler _settings;

        /// <summary>
        /// Gets the configurations settings. 
        /// </summary>
        public static ConfigurationSectionHandler Settings { get { return _settings; } }

        /// <summary>
        /// Reads the configuration settings from the Atdl4net section of the application configuration file.
        /// </summary>
        static Atdl4netConfiguration()
        {
            try
            {
                _settings = (ConfigurationSectionHandler)System.Configuration.ConfigurationManager.GetSection("atdl4net/settings");
            }
            catch (ConfigurationErrorsException ex)
            {
                _log.ErrorFormat("Error reading configuration data; details: {0}", ex.Message);

                throw;
            }
        }
    }
}
