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

using Atdl4net.Model.Elements;
using Atdl4net.Resources;
using System;
using System.Globalization;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Types
{
    /// <summary>
    /// 'float field representing a percentage (e.g. 0.05 represents 5% and 0.9525 represents 95.25%). Note the number of 
    /// decimal places may vary.'
    /// </summary>
    public class Percentage_t : Float_t
    {
        /// <summary>
        /// Applicable for xsi:type of Percentage_t. If true then percent values must be multiplied by 100 before being
        /// sent on the wire. For example, if multiplyBy100 were false then the percentage, 75%, would be sent as 0.75 
        /// on the wire. However, if multiplyBy100 were true then 75 would be sent on the wire.
        /// If not provided it should be interpreted as false.
        /// Use of this attribute is not recommended. The motivation for this attribute is to maximize compatibility 
        /// with algorithmic interfaces that are non-compliant with FIX in regard to their handling of percentages. In
        /// these cases an integer parameter should be used instead of a percentage.
        /// </summary>
        public bool? MultiplyBy100 { get; set; }

        public override void SetWireValue(IParameter_t hostParameter, string value)
        {
            base.SetWireValue(hostParameter, value);

            if (MultiplyBy100 == true)
                Value = Value / 100;
        }

        public override object ControlValue
        {
            get
            {
                if (ConstValue != null)
                    return ConstValue * 100;
                else
                    return Value * 100;
            }
            set
            {
                if (ConstValue != null)
                    throw ThrowHelper.New<InvalidOperationException>(this, ErrorMessages.AttemptToSetConstValueParameter, ConstValue);

                Value = value != null ? (decimal?)value / 100 : null;
            }
        }

        protected override string ConvertToString(decimal? value)
        {
            if (value == null)
                return null;

            decimal adjustedValue = (MultiplyBy100 == true) ? (decimal)value * 100 : (decimal)value;

            if (Precision == null)
                return adjustedValue.ToString(CultureInfo.InvariantCulture);

            string format = string.Format("F{0}", Precision);

            return adjustedValue.ToString(format, CultureInfo.InvariantCulture);
        }
    }
}
