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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Atdl4net.Utility
{
    public static class ModelUtils
    {
        private static readonly IEnumerable<Type> _types;
        private static readonly Dictionary<string, MethodInfo> _methodInfoCache = new Dictionary<string, MethodInfo>();

        static ModelUtils()
        {
            _types = from t in Assembly.GetExecutingAssembly().GetTypes()
                     where (t.IsClass && t.Namespace == "Atdl4net.Model.Types") && !t.IsAbstract 
                     select t;
        }

        public static bool VisitHelper(Type visitorType, object visitor, object target)
        {
            Type targetParamType = target.GetType();
            string searchString = string.Format("{0}:{1}", visitorType.FullName, targetParamType.FullName);

            MethodInfo methodInfo = null;

            lock (_methodInfoCache)
            {
                if (!_methodInfoCache.TryGetValue(searchString, out methodInfo))
                {
                    Type[] types = new Type[] { targetParamType };

                    methodInfo = visitor.GetType().GetMethod("Visit", types);

                    if (methodInfo == null)
                        return false;

                    _methodInfoCache.Add(searchString, methodInfo);
                }
            }

            methodInfo.Invoke(visitor, new object[] { target });

            return true;
        }

        // TODO: Move this somewhere better.
        public static System.Type GetTypeFromName(string typeName)
        {
            return _types.FirstOrDefault(t => t.Name == typeName);
        }
    }
}
