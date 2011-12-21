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
