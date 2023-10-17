#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Configuration;
using System.Linq;

namespace Atdl4net.Configuration
{
    public class ConfigurationSectionHandler : ConfigurationSection
    {
        [ConfigurationProperty("wpf")]
        public WpfElement Wpf
        {
            get { return (WpfElement)this["wpf"]; }
            set { this["wpf"] = value; }
        }

        public class WpfElement : ConfigurationElement
        {
            [ConfigurationProperty("resetStrategyOnAssignmentToControl", DefaultValue = true, IsRequired = false)]
            public bool ResetStrategyOnAssignmentToControl
            {
                get { return (bool)this["resetStrategyOnAssignmentToControl"]; }
                set { this["resetStrategyOnAssignmentToControl"] = value; }
            }

            [ConfigurationProperty("view")]
            public ViewElement View
            {
                get { return (ViewElement)this["view"]; }
                set { this["view"] = value; }
            }

            [ConfigurationProperty("viewModel")]
            public ViewModelElement ViewModel
            {
                get { return (ViewModelElement)this["viewModel"]; }
                set { this["viewModel"] = value; }
            }

            public class ViewElement : ConfigurationElement
            {
                [ConfigurationProperty("autoSizeDropDowns", DefaultValue = true, IsRequired = false)]
                public bool AutoSizeDropDowns
                {
                    get { return (bool)this["autoSizeDropDowns"]; }
                    set { this["autoSizeDropDowns"] = value; }
                }
            }

            public class ViewModelElement : ConfigurationElement
            {
                [ConfigurationProperty("validateOnChange", DefaultValue = false, IsRequired = false)]
                public bool AutoSizeDropDowns
                {
                    get { return (bool)this["validateOnChange"]; }
                    set { this["validateOnChange"] = value; }
                }
            }
        }
    }
}

