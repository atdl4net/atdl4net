#region Copyright (c) 2010-2011, Cornerstone Technology Limited. http://atdl4net.org
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

