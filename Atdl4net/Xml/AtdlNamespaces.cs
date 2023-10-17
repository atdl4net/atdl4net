#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
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
