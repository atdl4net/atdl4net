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

namespace Atdl4net.Model.Collections
{
    /// <summary>
    /// Accessor interface that simple dictionary collections must implement.  Lookup is via string-based key.
    /// </summary>
    /// <typeparam name="T">Type of item that this SimpleDictionary provides access to.</typeparam>
    public interface ISimpleDictionary<T>
    {
        /// <summary>
        /// Gets the item specified by the supplied key.
        /// </summary>
        /// <param name="key">String-based key for the desired item.</param>
        /// <returns>Item whose key matches the supplied value.  Behaviour is undefined if the key cannot be found.</returns>
        T this[string key] { get; }

        /// <summary>
        /// Indicates whether an item with the supplied key is present in the collection.
        /// </summary>
        /// <param name="key">String-based key to check for.</param>
        /// <returns>True if an item with the supplied key is present; false otherwise.</returns>
        bool Contains(string key);
    }
}
