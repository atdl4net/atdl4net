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
using Atdl4net.Model.Controls;
using Atdl4net.Model.Elements;
using Atdl4net.Model.Types;
using Common.Logging;

namespace Atdl4net.Wpf.ViewModel
{
    public class ParameterValueConvertor
    {
        private static readonly ILog _log = LogManager.GetLogger("ViewModel");

        public static object Convert(Control_t control, object value, string targetType)
        {
            object result = null;

            _log.DebugFormat("Converting value '{0}' to target type '{1}'.", value, targetType);

            switch (targetType)
            {
                case AtdlTypeName.Amt_t:
                case AtdlTypeName.Float_t:
                case AtdlTypeName.Percentage_t: // TODO: Needs own case?
                case AtdlTypeName.Price_t:
                case AtdlTypeName.PriceOffset_t:
                case AtdlTypeName.Qty_t:
                    if (value is string)
                    {
                        if (string.IsNullOrEmpty(value as string))
                            result = null;
                        else
                            result = System.Convert.ToDecimal(value as string);
                    }
                    else if (value is decimal)
                        result = value;
                    break;

                case AtdlTypeName.Boolean_t:
                    if (value is bool)
                        result = value;
                    break;

                case AtdlTypeName.Char_t:
                    result = (bool)value ? (control as IBooleanControl).CheckedEnumRef : (control as IBooleanControl).UncheckedEnumRef;
                    break;

                case AtdlTypeName.Country_t:
                case AtdlTypeName.Currency_t:
                case AtdlTypeName.Data_t:
                case AtdlTypeName.Exchange_t:
                    break;

                case AtdlTypeName.Int_t:
                case AtdlTypeName.Length_t:
                case AtdlTypeName.NumInGroup_t:
                case AtdlTypeName.SeqNum_t:
                case AtdlTypeName.TagNum_t:
                    if (value is string)
                        result = System.Convert.ToInt32(value);
                    else if (value is int)
                        result = value;
                    break;

                case AtdlTypeName.Language_t:
                case AtdlTypeName.MultipleCharValue_t:
                case AtdlTypeName.MultipleStringValue_t:
                case AtdlTypeName.String_t:
                    if (value is ListItem_t)
                        result = (value as ListItem_t).EnumId;
                    else if (value is string)
                        result = value;
                    break;

                case AtdlTypeName.LocalMktDate_t:
                case AtdlTypeName.MonthYear_t:
                case AtdlTypeName.Tenor_t:
                case AtdlTypeName.TZTimeOnly_t:
                case AtdlTypeName.TZTimestamp_t:
                case AtdlTypeName.UTCDateOnly_t:
                case AtdlTypeName.UTCTimeOnly_t:
                case AtdlTypeName.UTCTimestamp_t:
                    if (value is DateTime)
                        return value;
                    break;
            }

            _log.DebugFormat("Converted '{0}' to '{1}' ({2}).", 
                value, result, result != null ? result.GetType().FullName : "null");

            return result;
        }
    }
}
