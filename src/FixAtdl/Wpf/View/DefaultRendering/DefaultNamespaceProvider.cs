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
