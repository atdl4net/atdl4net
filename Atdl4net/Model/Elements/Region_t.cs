#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using Atdl4net.Model.Collections;
using Atdl4net.Model.Enumerations;

namespace Atdl4net.Model.Elements
{
    public class Region_t
    {
        private CountryCollection _countries;

        public Region Name { get; set; }
        public Inclusion_t Inclusion { get; set; }

        public CountryCollection Countries 
        {
            get
            {
                // Lazy initialize as we can't use 'this' in constructor.
                if (_countries == null)
                    _countries = new CountryCollection(this);

                return _countries;
            }
        }
    }
}
