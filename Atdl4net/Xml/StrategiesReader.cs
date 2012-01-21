#region Copyright (c) 2010-2012, Cornerstone Technology Limited. http://atdl4net.org
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
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Model.Elements;
using Atdl4net.Notification;
using Atdl4net.Resources;
using Atdl4net.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using Common.Logging;

#if !NET_40
using System.Xml;
#endif

namespace Atdl4net.Xml
{
    public class StrategiesReader: INotifyStrategyLoaded
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Xml.Serialization");

        public event System.EventHandler<StrategyLoadedEventArgs> StrategyLoaded;

        public Strategies_t Load(string path)
        {
            _log.DebugFormat("Attempting to load strategies from file '{0}'.", path);

            XDocument document = XDocument.Load(path, LoadOptions.SetLineInfo | LoadOptions.PreserveWhitespace);

            Strategies_t strategies = LoadStrategies(document);

            _log.DebugFormat("{0} strategies loaded from file '{1}'.", strategies.Count, path);

            return strategies;
        }

        public Strategies_t Load(Stream stream)
        {
            _log.Debug("Attempting to load strategies from stream.");

            XDocument document;
#if NET_40
            document = XDocument.Load(stream, LoadOptions.SetLineInfo | LoadOptions.PreserveWhitespace);
#else
            using (XmlReader reader = XmlReader.Create(stream))
            {
                document = XDocument.Load(reader, LoadOptions.SetLineInfo | LoadOptions.PreserveWhitespace);
            }
#endif
            Strategies_t strategies = LoadStrategies(document);

            _log.DebugFormat("{0} strategies loaded from stream.", strategies.Count);

            return strategies;
        }

        private Strategies_t LoadStrategies(XDocument document)
        {
            XElement element = document.Element(AtdlNamespaces.core + "Strategies");

            if (element == null)
                throw ThrowHelper.New<Atdl4netException>(this, ErrorMessages.StrategiesLoadFailure);

            ElementFactory factory = new ElementFactory(SchemaDefinitions.Strategies_t, typeof(Strategy_t));

            factory.ClassDeserialized += new System.EventHandler<ClassDeserializedEventArgs>(OnStrategyDeserialized);

            Strategies_t strategies = (Strategies_t)factory.DeserializeElement(element);

            strategies.ResolveAll();

            return strategies;
        }

        private void OnStrategyDeserialized(object sender, ClassDeserializedEventArgs args)
        {
            NotifyStrategyLoaded(0,0,(args.ExtraInfo as Strategy_t).Name);
        }

        #region INotifyStrategyLoad Members and Support Methods

        private void NotifyStrategyLoaded(int index, int total, string strategyName)
        {
            System.EventHandler<StrategyLoadedEventArgs> strategyLoaded = StrategyLoaded;

            if (strategyLoaded != null)
                strategyLoaded(this, new StrategyLoadedEventArgs(index, total, strategyName));
        }

        #endregion
    }
}
