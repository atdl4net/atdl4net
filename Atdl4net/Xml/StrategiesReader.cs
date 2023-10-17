#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
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
