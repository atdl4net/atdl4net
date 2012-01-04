#region Copyright (c) 2010-2012, Cornerstone Technology Limited. http://atdl4net.org
//
//   This software is released under both commercial and open-source licenses.
//
//   If you received this software under the commercial license, the terms of that license can be found in the
//   Commercial.txt file in the Licenses folder.  If you received this software under the open-source license,
//   the following applies:
//
//      This file is part of Atdl4net.
//
//      Atdl4net is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public 
//      License as published by the Free Software Foundation, either version 2.1 of the License, or (at your option) any later version.
// 
//      Atdl4net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
//      of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU Lesser General Public License for more details.
//
//      You should have received a copy of the GNU Lesser General Public License along with Atdl4net.  If not, see
//      http://www.gnu.org/licenses/.
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
