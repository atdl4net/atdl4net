#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using Atdl4net.Model.Enumerations;

namespace Atdl4net.Model.Controls.Support
{
    public interface IOrientableControl
    {
        Orientation_t? Orientation { get; }
    }
}
