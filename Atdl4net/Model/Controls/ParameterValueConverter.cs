#region Copyright (c) 2010, Cornerstone Technology Limited. http://atdl4net.org
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
//      License as published by the Free Software Foundation, version 3.
// 
//      Atdl4net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
//      of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU Lesser General Public License for more details.
//
//      You should have received a copy of the GNU Lesser General Public License along with Atdl4net.  If not, see
//      http://www.gnu.org/licenses/.
//
#endregion

using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Model.Elements;
using Atdl4net.Model.Types;
using Atdl4net.Resources;
using System;
using System.Globalization;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Controls
{
    public class ParameterValueConverter
    {
        private static readonly string ExceptionContext = typeof(ParameterValueConverter).Name;

        private ParameterValueConverter()
        {
        }

        public static object Convert(Control_t sourceControl, IParameter_t targetParameter)
        {
            if (sourceControl is IBooleanControl && !(targetParameter is Parameter_t<Boolean_t>))
            {
                IBooleanControl control = sourceControl as IBooleanControl;

                if (control.CheckedEnumRef == null || control.UncheckedEnumRef==null)
                    throw ThrowHelper.New<InvalidFieldValueException>(ExceptionContext, ErrorMessages.EnumRefNotSetOnBooleanControl);

                EnumState state = new EnumState(new string[] { control.UncheckedEnumRef, control.CheckedEnumRef});

                state[control.UncheckedEnumRef] = !control.Value;
                state[control.CheckedEnumRef] = control.Value;

                return state;
            }
            else if (sourceControl is IStringControl && targetParameter.IsNumeric)
            {
                object value = sourceControl.GetValue();

                return value != null ? (decimal?)System.Convert.ToDecimal(value, CultureInfo.CurrentCulture) : null;
            }
            else
                return sourceControl.GetValue();
        }

        public static object Convert(IParameter_t sourceParameter, Control_t targetControl)
        {
            object value = sourceParameter.ControlValue;

            if (value is EnumState && targetControl is IBooleanControl)
            {
                IBooleanControl control = targetControl as IBooleanControl;

                if (control.CheckedEnumRef == null || control.UncheckedEnumRef == null)
                    throw ThrowHelper.New<InvalidFieldValueException>(ExceptionContext, ErrorMessages.EnumRefNotSetOnBooleanControl);

                return (value as EnumState)[control.CheckedEnumRef];
            }
            else if (sourceParameter.IsNumeric && targetControl is IStringControl)
            {
                if (value != null)
                    return value is decimal ? ((decimal)value).ToString("#.#########", CultureInfo.CurrentCulture) : value.ToString();
            }

            return value;
        }
    }
}