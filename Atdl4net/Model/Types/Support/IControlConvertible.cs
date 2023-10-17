#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Linq;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Controls.Support;

namespace Atdl4net.Model.Types.Support
{
    /// <summary>
    /// Interface that all parameter types (Amt_t, Boolean_t, etc.) must implement such that their values can be converted to a form accepted by
    /// <see cref="Control_t"/> instances.
    /// </summary>
    /// <remarks>If the parameter's value cannot be converted to the target type, then an <see cref="InvalidCastException"/>
    /// is thrown.<br/br/>
    /// (Atdl4net doesn't use <see cref="System.IConvertible"/> as that does not support the conversion to
    /// and from Nullable types.)</remarks>
    public interface IControlConvertible
    {
        /// <summary>
        /// Converts the value of this instance to an equivalent nullable boolean value.
        /// </summary>
        /// <returns>One of true, false or null which is equivalent to the value of this instance.</returns>
        bool? ToBoolean();

        /// <summary>
        /// Converts the value of this instance to an equivalent string value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>A string value equivalent to the value of this instance.  May be null.</returns>
        string ToString(IFormatProvider provider);

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable decimal value using the specified culture-specific formatting information.
        /// </summary>
        /// <returns>A nullable decimal equivalent to the value of this instance.</returns>
        decimal? ToDecimal();

        /// <summary>
        /// Converts the value of this instance to an equivalent nullable DateTime value.
        /// </summary>
        /// <returns>A nullable DateTime equivalent to the value of this instance.</returns>
        DateTime? ToDateTime();

        /// <summary>
        /// Converts the value of this instance to an equivalent EnumState value.
        /// </summary>
        /// <returns>A valid EnumState, assuming the source value can be correctly converted.</returns>
        EnumState ToEnumState(EnumPairCollection enumPairs);
    }
}
