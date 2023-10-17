#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Linq;
using Atdl4net.Fix;

namespace Atdl4net.Wpf.ViewModel
{
    /// <summary>
    /// Interface that classes must implement in order to be able to provide initial FIX values to consumers.
    /// </summary>
    public interface IInitialFixValueProvider
    {
        /// <summary>
        /// Gets the set of initial values to be used.
        /// </summary>
        FixTagValuesCollection InputFixValues { get; }
    }
}
