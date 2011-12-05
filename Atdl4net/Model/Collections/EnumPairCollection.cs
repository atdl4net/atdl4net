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

using Atdl4net.Model.Elements;
using Atdl4net.Resources;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Collections
{
    /// <summary>
    /// Collection used to represent a set of EnumPairs.
    /// </summary>
    public class EnumPairCollection : KeyedCollection<string, EnumPair_t>
    {
        protected override string GetKeyForItem(EnumPair_t item)
        {
            return item.EnumId;
        }

        /// <summary>
        /// Gets the full set of EnumIds.
        /// </summary>
        /// <value>The enum ids.</value>
        public string[] EnumIds
        {
            get { return (from item in Items select item.EnumId).ToArray<string>(); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has values.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has values; otherwise, <c>false</c>.
        /// </value>
        public bool HasValues
        {
            get { return Count > 0; }
        }

        /// <summary>
        /// Gets the wire value from enum id.
        /// </summary>
        /// <param name="enumId">The enum id.</param>
        /// <returns></returns>
        public string GetWireValueFromEnumId(string enumId)
        {
            EnumPair_t enumPair = this[enumId];

            if (enumPair == null)
                throw ThrowHelper.New<InvalidOperationException>(this, ErrorMessages.EnumerationNotFound, enumId);

            return enumPair.WireValue;
        }

        /// <summary>
        /// Tries to parse the supplied wire value.
        /// </summary>
        /// <param name="wireValue">The wire value.</param>
        /// <param name="enumId">The enum id.</param>
        /// <returns>true if the values could be parsed; false otherwise.</returns>
        public bool TryParseWireValue(string wireValue, out string enumId)
        {
            enumId = null;
            string testValue = (wireValue != null) ? wireValue : Atdl.NullValue;

            foreach (EnumPair_t enumPair in this)
            {
                if (enumPair.WireValue == testValue)
                {
                    enumId = enumPair.EnumId;

                    return true;
                }
            }

            return false;
        }
    }
}
