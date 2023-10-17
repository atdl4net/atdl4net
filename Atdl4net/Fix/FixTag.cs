#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
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
        private readonly int _value;

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
