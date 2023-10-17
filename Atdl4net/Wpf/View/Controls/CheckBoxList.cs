#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System.Windows;

namespace Atdl4net.Wpf.View.Controls
{
    public class CheckBoxList : MultiButtonControlBase
    {
        static CheckBoxList()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckBoxList), new FrameworkPropertyMetadata(typeof(CheckBoxList)));
        }
    }
}
