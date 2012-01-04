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

namespace Atdl4net.Model.Elements
{
    /// <summary>
    /// Represents the FIXatdl Description sub-element that is used to provide additional descriptive information about an element.
    /// </summary>
    public class Description_t
    {
        /// <summary>
        /// Gets/sets the content of the Description.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Implicit cast operator that enables an arbitrary string to be assigned to the Description.
        /// </summary>
        /// <param name="value">String value to be assigned to the newly created Description.</param>
        /// <returns>New Description with the supplied string as the Content.</returns>
        public static implicit operator Description_t(string value)
        {
            return new Description_t { Content = value };
        }

        /// <summary>
        ///  Implicit case operator that enables a Description to be used where a string is expected.
        /// </summary>
        /// <param name="description">Description to treated as a string.</param>
        /// <returns>Content field of this description, i.e., the descriptive text.</returns>
        public static implicit operator string(Description_t description)
        {
            return description.Content;
        }
    }
}
