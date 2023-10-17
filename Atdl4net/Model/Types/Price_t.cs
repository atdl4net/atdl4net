#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

namespace Atdl4net.Model.Types
{
    /// <summary>
    /// 'float field representing a price. Note the number of decimal places may vary. For certain asset classes prices 
    /// may be negative values. For example, prices for options strategies can be negative under certain market conditions. 
    /// Refer to Volume 7: FIX Usage by Product for asset classes that support negative price values.'
    /// </summary>
    public class Price_t : Float_t
    {
    }
}
