#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Atdl4net.Wpf.View.DefaultRendering
{
    [Export(typeof(Atdl4net.Wpf.View.INamespaceProvider))]
    internal class DefaultNamespaceProvider : INamespaceProvider
    {
        public const string XamlNamespaceUri = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";
        public const string XamlXNamespace = "x";
        public const string XamlXNamespaceUri = "http://schemas.microsoft.com/winfx/2006/xaml";
        public const string Atdl4netNamespace = "atdl4net";
        public const string Atdl4netNamespaceUri = "clr-namespace:Atdl4net.Wpf.View.Controls;assembly=Atdl4net";
        public const string SystemNamespace = "sys";
        public const string SystemNamespaceUri = "clr-namespace:System;assembly=mscorlib";

        private readonly Dictionary<string, string> _namespaces;

        DefaultNamespaceProvider()
        {
            _namespaces = new Dictionary<string, string>();

            _namespaces.Add(string.Empty, XamlNamespaceUri);
            _namespaces.Add(XamlXNamespace, XamlXNamespaceUri);
            _namespaces.Add(Atdl4netNamespace, Atdl4netNamespaceUri);
            _namespaces.Add(SystemNamespace, SystemNamespaceUri);
        }

        #region INamespaceProvider Members

        public Dictionary<string, string> CustomNamespaces
        {
            get { return _namespaces; }
        }

        #endregion
    }
}
