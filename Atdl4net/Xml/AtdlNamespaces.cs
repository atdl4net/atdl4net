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

using System.Xml.Linq;

namespace Atdl4net.Xml
{
    public class AtdlNamespaces
    {
        public static readonly XNamespace core = "http://www.fixprotocol.org/FIXatdl-1-1/Core";
        public static readonly XNamespace lay = "http://www.fixprotocol.org/FIXatdl-1-1/Layout";
        public static readonly XNamespace val = "http://www.fixprotocol.org/FIXatdl-1-1/Validation";
        public static readonly XNamespace flow = "http://www.fixprotocol.org/FIXatdl-1-1/Flow";
        public static readonly XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
    }
}
