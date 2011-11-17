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

using Atdl4net.Diagnostics;
using Atdl4net.Resources;
using System;

namespace Atdl4net.Fix
{
    /// <summary>
    /// Represents a FIX tag, a non-zero positive integer.
    /// </summary>
    public struct FixTag
    {
        int _value;

        /// <summary>
        /// Initializes a new instance of FixTag.
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if a value less than or equal to zero is supplied.</exception>
        public FixTag(int value)
        {
            if (value <= 0)
                throw ThrowHelper.New<ArgumentOutOfRangeException>(typeof(FixTag).FullName, ErrorMessages.NonZeroPositiveIntRequired, value);

            _value = value;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Int32"/> to <see cref="Atdl4net.Fix.FixTag"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator FixTag(int value)
        {
            return new FixTag(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Atdl4net.Fix.FixField"/> to <see cref="Atdl4net.Fix.FixTag"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator FixTag(FixField value)
        {
            return new FixTag((int)value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Atdl4net.Fix.FixTag"/> to <see cref="System.Int32"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator int(FixTag value)
        {
            return value._value;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Atdl4net.Fix.FixTag"/> to <see cref="Atdl4net.Fix.FixField"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator FixField(FixTag value)
        {
            return (FixField)value._value;
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
