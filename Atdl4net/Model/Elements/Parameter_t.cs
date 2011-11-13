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

using Atdl4net.Diagnostics;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Fix;
using Atdl4net.Model.Collections;
using Atdl4net.Model.Enumerations;
using Atdl4net.Model.Types;
using Atdl4net.Resources;
using System;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Model.Elements
{
    /// <summary>
    /// Represents a Parameter_t type.
    /// </summary>
    /// <typeparam name="T">Valid FIXatdl type <see cref="Atdl4net.Model.Types"/></typeparam>
    /// <example>To create a parameter with underlying type Amt_t, use <c>new Parameter_t&lt;Amt_t&gt;</c>.</example>
    public class Parameter_t<T> : IParameter_t where T : IParameterType, new()
    {
        private EnumPairCollection _enumPairs = new EnumPairCollection();

        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter_t&lt;T&gt;"/> class.
        /// </summary>
        public Parameter_t(string name)
        {
            Logger.DebugFormat("New Parameter_t<{0}> created, Name='{1}'.", typeof(T).Name, name);

            Name = name;
            Type = typeof(T).Name;

            Use = Use_t.Optional;
            MutableOnCxlRpl = true;

            Value = new T();
        }

        public static Parameter_t<T> Create(string name)
        {
            Parameter_t<T> parameter = new Parameter_t<T>(name);

            parameter.Value = new T();

            return parameter;
        }

        public T Value { get; set; }

        #region IParameter_t<T> Members

        public string Type { get; set; }
        public string Name { get; set; }
        public FixTag? FixTag { get; set; }
        public Use_t Use { get; set; }
        public bool? MutableOnCxlRpl { get; set; }
        public bool? RevertOnCxlRpl { get; set; }
        public bool? DefinedByFix { get; set; }

        public EnumPairCollection EnumPairs
        {
            get { return _enumPairs; }
        }

        public object ControlValue
        {
            get { return Value.ControlValue; }

            set
            {
                Logger.DebugFormat("Setting parameter '{0}' to value '{1}'.", Name, value ?? "null");

                // Deal with special case of {NULL}.
                if (object.Equals(value, Control_t.NullValue))
                    value = null;

                Value.ControlValue = value;
            }
        }

        public string WireValue
        {
            get 
            {
                string value = Value.GetWireValue(this);

                if (Use == Use_t.Required && value == null)
                    throw ThrowHelper.New<MissingMandatoryValueException>(this, ErrorMessages.NonOptionalParameterNotSupplied, Name);

                return value;
            }
            set 
            {
                if (value == null)
                    throw ThrowHelper.New<ArgumentNullException>(this, ErrorMessages.IllegalUseOfNullError);

                Value.SetWireValue(this, value); 
            }
        }

        #endregion

        public object GetValue()
        {
            if (ControlValue is EnumState)
                return WireValue;
            else
                return ControlValue;
        }

        public bool IsFloat
        {
            get { return (Value is Float_t); }
        }

        public bool IsInteger
        {
            get { return (Value is Int_t || Value is NonZeroPositiveInteger || Value is NonNegativeInteger); }
        }

        public void Reset()
        {
            Value = new T();
        }
    }
}
