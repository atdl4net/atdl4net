#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System.Windows;
using System.Windows.Controls;

namespace Atdl4net.Wpf.View.Controls
{
    public class StrategyPanelFrame : Expander
    {
        public static readonly DependencyProperty BorderVisibilityProperty = DependencyProperty.Register("BorderVisibility", typeof(Visibility), typeof(StrategyPanelFrame), new FrameworkPropertyMetadata(new Visibility(), FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty CollapseButtonVisibilityProperty = DependencyProperty.Register("CollapseButtonVisibility", typeof(Visibility), typeof(StrategyPanelFrame), new FrameworkPropertyMetadata(new Visibility(), FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty HeaderVisibilityProperty = DependencyProperty.Register("HeaderVisibility", typeof(Visibility), typeof(StrategyPanelFrame), new FrameworkPropertyMetadata(new Visibility(), FrameworkPropertyMetadataOptions.AffectsRender));

        static StrategyPanelFrame()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StrategyPanelFrame), new FrameworkPropertyMetadata(typeof(StrategyPanelFrame)));
        }

        public Visibility BorderVisibility
        {
            get { return (Visibility)base.GetValue(BorderVisibilityProperty); }
            set { base.SetValue(BorderVisibilityProperty, value); }
        }

        public Visibility CollapseButtonVisibility
        {
            get { return (Visibility)base.GetValue(CollapseButtonVisibilityProperty); }
            set { base.SetValue(CollapseButtonVisibilityProperty, value); }
        }

        public Visibility HeaderVisibility
        {
            get { return (Visibility)base.GetValue(HeaderVisibilityProperty); }
            set { base.SetValue(HeaderVisibilityProperty, value); }
        }
    }
}
