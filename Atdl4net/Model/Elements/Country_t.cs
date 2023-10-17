#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using Atdl4net.Model.Enumerations;
using Atdl4net.Model.Reference;

namespace Atdl4net.Model.Elements
{
    public class Country_t
    {
        public IsoCountryCode CountryCode { get; set; }

        public Inclusion_t Inclusion { get; set; }
    }
}
