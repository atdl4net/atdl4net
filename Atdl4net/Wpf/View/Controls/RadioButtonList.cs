#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion
using System;
using System.Windows;

namespace Atdl4net.Wpf.View.Controls
{
    /// <summary>
    /// Represents a RadioButtonList WPF control.
    /// </summary>
    public class RadioButtonList : MultiButtonControlBase
    {
        /// <summary>
        /// Static constructor for RadioButtonList; overrides the style.
        /// </summary>
        static RadioButtonList()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RadioButtonList), new FrameworkPropertyMetadata(typeof(RadioButtonList)));
        }
    }
}
