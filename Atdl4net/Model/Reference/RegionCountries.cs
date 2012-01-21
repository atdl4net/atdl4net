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

using System.Collections.Generic;
using Atdl4net.Model.Enumerations;

namespace Atdl4net.Model.Reference
{
    /// <summary>
    /// Represents the Regions supported by FIXatdl.
    /// </summary>
    public static class Regions
    {
        /// <summary>
        /// Gets the FIXatdl region for the supplied country.
        /// </summary>
        /// <param name="country">ISO country code to determine the region for.</param>
        /// <returns>Applicable region, or Region.None if none is applicable.</returns>
        public static Region GetRegionForCountry(IsoCountryCode country)
        {
            if (TheAmericasCountries.Contains(country))
                return Region.TheAmericas;

            if (EuropeMiddleEastAfricaCountries.Contains(country))
                return Region.EuropeMiddleEastAfrica;

            if (AsiaPacificJapanCountries.Contains(country))
                return Region.AsiaPacificJapan;

            return Region.None;
        }

        /// <summary>
        /// Provides the set of ISO country codes that are in The Americas.
        /// </summary>
        public static readonly HashSet<IsoCountryCode> TheAmericasCountries = new HashSet<IsoCountryCode> 
        { 
            IsoCountryCode.AI,
            IsoCountryCode.AG,
            IsoCountryCode.AR,
            IsoCountryCode.AW,
            IsoCountryCode.BS,
            IsoCountryCode.BB,
            IsoCountryCode.BZ,
            IsoCountryCode.BM,
            IsoCountryCode.BO,
            IsoCountryCode.BR,
            IsoCountryCode.CA,
            IsoCountryCode.KY,
            IsoCountryCode.CL,
            IsoCountryCode.CO,
            IsoCountryCode.CR,
            IsoCountryCode.CU,
            IsoCountryCode.DM,
            IsoCountryCode.DO,
            IsoCountryCode.EC,
            IsoCountryCode.SV,
            IsoCountryCode.FK,
            IsoCountryCode.GD,
            IsoCountryCode.GP,
            IsoCountryCode.GT,
            IsoCountryCode.GY,
            IsoCountryCode.HT,
            IsoCountryCode.HN,
            IsoCountryCode.JM,
            IsoCountryCode.MQ,
            IsoCountryCode.MX,
            IsoCountryCode.MS,
            IsoCountryCode.AN,
            IsoCountryCode.NI,
            IsoCountryCode.PA,
            IsoCountryCode.PY,
            IsoCountryCode.PE,
            IsoCountryCode.PR,
            IsoCountryCode.BL,
            IsoCountryCode.KN,
            IsoCountryCode.LC,
            IsoCountryCode.MF,
            IsoCountryCode.PM,
            IsoCountryCode.VC,
            IsoCountryCode.TT,
            IsoCountryCode.TC,
            IsoCountryCode.US,
            IsoCountryCode.UY,
            IsoCountryCode.VG,
            IsoCountryCode.VI,
            IsoCountryCode.VE,
        };

        /// <summary>
        /// Provides the set of ISO country codes that are in Europe, the Middle East and Africa.
        /// </summary>
        public static readonly HashSet<IsoCountryCode> EuropeMiddleEastAfricaCountries = new HashSet<IsoCountryCode>()
        {
            IsoCountryCode.AD,
            IsoCountryCode.AE,
            IsoCountryCode.AL,
            IsoCountryCode.AM,
            IsoCountryCode.AO,
            IsoCountryCode.AT,
            IsoCountryCode.AX,
            IsoCountryCode.AZ,
            IsoCountryCode.BA,
            IsoCountryCode.BE,
            IsoCountryCode.BF,
            IsoCountryCode.BG,
            IsoCountryCode.BH,
            IsoCountryCode.BI,
            IsoCountryCode.BJ,
            IsoCountryCode.BW,
            IsoCountryCode.BY,
            IsoCountryCode.CD,
            IsoCountryCode.CF,
            IsoCountryCode.CG,
            IsoCountryCode.CH,
            IsoCountryCode.CI,
            IsoCountryCode.CM,
            IsoCountryCode.CV,
            IsoCountryCode.CY,
            IsoCountryCode.CZ,
            IsoCountryCode.DE,
            IsoCountryCode.DJ,
            IsoCountryCode.DK,
            IsoCountryCode.DZ,
            IsoCountryCode.EE,
            IsoCountryCode.EG,
            IsoCountryCode.EH,
            IsoCountryCode.ER,
            IsoCountryCode.ES,
            IsoCountryCode.ET,
            IsoCountryCode.FI,
            IsoCountryCode.FO,
            IsoCountryCode.FR,
            IsoCountryCode.GA,
            IsoCountryCode.GB,
            IsoCountryCode.GE,
            IsoCountryCode.GF,
            IsoCountryCode.GG,
            IsoCountryCode.GH,
            IsoCountryCode.GI,
            IsoCountryCode.GL,
            IsoCountryCode.GM,
            IsoCountryCode.GN,
            IsoCountryCode.GQ,
            IsoCountryCode.GR,
            IsoCountryCode.GS,
            IsoCountryCode.GW,
            IsoCountryCode.HR,
            IsoCountryCode.HU,
            IsoCountryCode.IE,
            IsoCountryCode.IL,
            IsoCountryCode.IM,
            IsoCountryCode.IQ,
            IsoCountryCode.IR,
            IsoCountryCode.IS,
            IsoCountryCode.IT,
            IsoCountryCode.JE,
            IsoCountryCode.JO,
            IsoCountryCode.KE,
            IsoCountryCode.KM,
            IsoCountryCode.KW,
            IsoCountryCode.LB,
            IsoCountryCode.LI,
            IsoCountryCode.LK,
            IsoCountryCode.LR,
            IsoCountryCode.LS,
            IsoCountryCode.LT,
            IsoCountryCode.LU,
            IsoCountryCode.LV,
            IsoCountryCode.LY,
            IsoCountryCode.MA,
            IsoCountryCode.MC,
            IsoCountryCode.MD,
            IsoCountryCode.ME,
            IsoCountryCode.MG,
            IsoCountryCode.MK,
            IsoCountryCode.ML,
            IsoCountryCode.MR,
            IsoCountryCode.MT,
            IsoCountryCode.MU,
            IsoCountryCode.MW,
            IsoCountryCode.MZ,
            IsoCountryCode.NA,
            IsoCountryCode.NE,
            IsoCountryCode.NG,
            IsoCountryCode.NL,
            IsoCountryCode.NO,
            IsoCountryCode.OM,
            IsoCountryCode.PL,
            IsoCountryCode.PN,
            IsoCountryCode.PS,
            IsoCountryCode.PT,
            IsoCountryCode.QA,
            IsoCountryCode.RE,
            IsoCountryCode.RO,
            IsoCountryCode.RS,
            IsoCountryCode.RU,
            IsoCountryCode.RW,
            IsoCountryCode.SA,
            IsoCountryCode.SC,
            IsoCountryCode.SD,
            IsoCountryCode.SE,
            IsoCountryCode.SH,
            IsoCountryCode.SI,
            IsoCountryCode.SJ,
            IsoCountryCode.SK,
            IsoCountryCode.SL,
            IsoCountryCode.SM,
            IsoCountryCode.SN,
            IsoCountryCode.SO,
            IsoCountryCode.SR,
            IsoCountryCode.ST,
            IsoCountryCode.SY,
            IsoCountryCode.SZ,
            IsoCountryCode.TD,
            IsoCountryCode.TG,
            IsoCountryCode.TN,
            IsoCountryCode.TR,
            IsoCountryCode.TZ,
            IsoCountryCode.UA,
            IsoCountryCode.UG,
            IsoCountryCode.VA,
            IsoCountryCode.YE,
            IsoCountryCode.YT,
            IsoCountryCode.ZA,
            IsoCountryCode.ZM,
            IsoCountryCode.ZW
        };

        /// <summary>
        /// Provides the set of ISO country codes that are in the Asia Pacific and Japan region.
        /// </summary>
        public static readonly HashSet<IsoCountryCode> AsiaPacificJapanCountries = new HashSet<IsoCountryCode>()
        {
            IsoCountryCode.AF, 
            IsoCountryCode.AS, 
            IsoCountryCode.AU, 
            IsoCountryCode.BD, 
            IsoCountryCode.BN, 
            IsoCountryCode.BT, 
            IsoCountryCode.CC, 
            IsoCountryCode.CK, 
            IsoCountryCode.CN, 
            IsoCountryCode.CX, 
            IsoCountryCode.FJ, 
            IsoCountryCode.FM, 
            IsoCountryCode.GU, 
            IsoCountryCode.HK, 
            IsoCountryCode.ID, 
            IsoCountryCode.IN, 
            IsoCountryCode.IO, 
            IsoCountryCode.JP, 
            IsoCountryCode.KG, 
            IsoCountryCode.KH, 
            IsoCountryCode.KI, 
            IsoCountryCode.KP, 
            IsoCountryCode.KR, 
            IsoCountryCode.KZ, 
            IsoCountryCode.LA, 
            IsoCountryCode.MH, 
            IsoCountryCode.MM, 
            IsoCountryCode.MN, 
            IsoCountryCode.MO, 
            IsoCountryCode.MP, 
            IsoCountryCode.MV, 
            IsoCountryCode.MY, 
            IsoCountryCode.NC, 
            IsoCountryCode.NF, 
            IsoCountryCode.NP, 
            IsoCountryCode.NR, 
            IsoCountryCode.NU, 
            IsoCountryCode.NZ, 
            IsoCountryCode.PF, 
            IsoCountryCode.PG, 
            IsoCountryCode.PH, 
            IsoCountryCode.PK, 
            IsoCountryCode.PW, 
            IsoCountryCode.SB, 
            IsoCountryCode.SG, 
            IsoCountryCode.TH, 
            IsoCountryCode.TJ, 
            IsoCountryCode.TK, 
            IsoCountryCode.TL, 
            IsoCountryCode.TM, 
            IsoCountryCode.TO, 
            IsoCountryCode.TV, 
            IsoCountryCode.TW, 
            IsoCountryCode.UM, 
            IsoCountryCode.UZ, 
            IsoCountryCode.VN, 
            IsoCountryCode.VU, 
            IsoCountryCode.WF, 
            IsoCountryCode.WS
        };
    }
}
