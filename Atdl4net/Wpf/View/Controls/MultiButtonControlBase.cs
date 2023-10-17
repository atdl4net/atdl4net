#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Atdl4net.Wpf.View.Controls
{
    /// <summary>
    /// Base class for WPF controls that are made up of multiple buttons, i.e., CheckBoxList and RadioButtonList.
    /// </summary>
    public abstract class MultiButtonControlBase : Selector
    {
        /// <summary>
        /// Dependency property for getting and setting the CornerRadius of this control.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(MultiButtonControlBase));

        /// <summary>
        /// Dependency property for getting and setting the Orientation of this control.
        /// </summary>
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(MultiButtonControlBase));

        /// <summary>
        /// Gets/sets the corner radius of the border of this control.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <summary>
        /// Gets/sets the orientation of this control.
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
    }
}
