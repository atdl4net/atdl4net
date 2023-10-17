#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Linq;

namespace Atdl4net.Validation
{
    /// <summary>
    /// Minimal interface that objects can support in order to make available a current value.
    /// </summary>
    public interface IValueProvider
    {
        /// <summary>
        /// Gets the current value of this object.
        /// </summary>
        /// <returns>Object's current value.</returns>
        object GetCurrentValue();
    }
}
