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

using Atdl4net.Diagnostics;
using Atdl4net.Resources;
using System;

namespace Atdl4net.Fix
{
    /// <summary>
    /// Represents a FIX NumInGroup value, the number of elements in a repeating block.
    /// </summary>
    public struct NumInGroup
    {
        private readonly int _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="NumInGroup"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public NumInGroup(int value)
        {
            if (value < 0)
                throw ThrowHelper.New<ArgumentOutOfRangeException>(typeof(NumInGroup).FullName, ErrorMessages.NonNegativeIntRequired, value);

            _value = value;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Int32"/> to <see cref="Atdl4net.Fix.NumInGroup"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator NumInGroup(int value)
        {
            return new NumInGroup(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Atdl4net.Fix.NumInGroup"/> to <see cref="System.Int32"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator int(NumInGroup value)
        {
            return value._value;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return _value.ToString();
        }
    }
}
