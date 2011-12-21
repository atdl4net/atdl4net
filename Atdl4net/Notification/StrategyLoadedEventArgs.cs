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
using System.Linq;

namespace Atdl4net.Notification
{
    /// <summary>
    /// Event argument that provides strategy load information.
    /// </summary>
    public class StrategyLoadedEventArgs : EventArgs
    {
        /// <summary>
        /// Index number of this strategy within the strategy file.
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// Total number of strategies within the strategy file being loaded.
        /// </summary>
        public int Total { get; private set; }

        /// <summary>
        /// Name of the loaded strategy.
        /// </summary>
        public string StrategyName { get; private set; }

        /// <summary>
        /// Initializes a new <see cref="StrategyLoadedEventArgs"/> instance.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="total"></param>
        /// <param name="strategyName"></param>
        public StrategyLoadedEventArgs(int index, int total, string strategyName)
        {
            Index = index;
            Total = total;
            StrategyName = strategyName;
        }
    }
}
