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

using Atdl4net.Resources;
using Atdl4net.Model.Elements;
using System;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Types
{
    public abstract class EnumableValueType<T> : IParameterType where T : struct
    {
        public EnumState EnumState { get; set; }

        /// <summary>
        /// Gets or sets the const value for this parameter.
        /// </summary>
        /// <note type="caution">The value of a parameter that is constant and is not referred by a Control element. This value must be 
        /// sent on the wire by the order generating application.</note>
        /// <value>The const value.</value>
        public T? ConstValue { get; set; }

        /// <summary>
        /// Gets or sets the value of this field.
        /// </summary>
        /// <value>The value.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if a value is supplied that exceeds the MinValue or MaxValue for this field.</exception>
        /// <exception cref="System.InvalidOperationException">Thrown if an attempt is made to set a field that has a ConstValue supplied.</exception>
        protected T? Value;

        #region IParameterType Members

        public virtual string GetWireValue(IParameter_t hostParameter)
        {
            if (ConstValue != null)
                return ConvertToString(ConstValue);

            if (EnumState != null)
                return EnumState.ToWireValue(hostParameter.EnumPairs);

            return Value != null ? ConvertToString(ValidateValue(Value)) : null;
        }

        public virtual void SetWireValue(IParameter_t hostParameter, string value)
        {
            // When ConstValue is set, the only assignment we allow is if the supplied value is the same value as ConstValue.
            if (ConstValue != null)
            {
                if (ConvertToString(ConstValue) == value)
                    return;

                throw ThrowHelper.New<InvalidOperationException>(this, ErrorMessages.AttemptToSetConstValueParameter, ConstValue);
            }

            if (hostParameter.EnumPairs.HasValues)
            {
                Value = null;
                EnumState = EnumState.FromWireValue(hostParameter.EnumPairs, value);
            }
            else
            {
                EnumState = null;
                Value = ValidateValue(ConvertFromString(value));
            }
        }

        public virtual object ControlValue
        {
            get
            {
                if (ConstValue != null)
                    return ConstValue;
                else if (EnumState != null)
                    return EnumState;
                else
                    return Value;
            }
            set
            {
                if (ConstValue != null)
                    throw ThrowHelper.New<InvalidOperationException>(this, ErrorMessages.AttemptToSetConstValueParameter, ConstValue);

                if (value is EnumState)
                {
                    Value = null;
                    EnumState = value as EnumState;
                }
                else
                {
                    EnumState = null;
                    Value = (T?)value;
                }
            }
        }

        #endregion

        protected abstract T? ValidateValue(T? value);

        protected abstract T? ConvertFromString(string value);

        protected abstract string ConvertToString(T? value);
    }
}
