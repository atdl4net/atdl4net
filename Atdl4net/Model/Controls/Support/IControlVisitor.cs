#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Linq;
using Atdl4net.Model.Elements;

namespace Atdl4net.Model.Controls.Support
{
    /// <summary>
    /// Interface to support visitor pattern.
    /// </summary>
    public interface IControlVisitor
    {
        /// <summary>
        /// Method that the visitor will call based on the type of the control parameter.
        /// </summary>
        /// <param name="control">Control to process as part of this visitor pattern.</param>
        void Visit(Control_t control);
    }
}
