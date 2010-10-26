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
using Atdl4net.Fix;
using Atdl4net.Model.Elements;
using System.Collections.ObjectModel;

namespace Atdl4net.Model.Collections
{
    public class ParameterCollection : KeyedCollection<string, IParameter_t>, IDictionary<IParameter_t>
    {
        protected override string GetKeyForItem(IParameter_t parameter)
        {
            return parameter.Name;
        }

        public void InitializeValues(FixTagValuesCollection inputValues)
        {
            string value;

            foreach (IParameter_t parameter in this.Items)
            {
                if (parameter.FixTag != null && inputValues.TryGetValue((FixTag)parameter.FixTag, out value))
                    parameter.WireValue = value;
                else
                    parameter.Reset();
            }
        }

        public FixTagValuesCollection GetOutputValues()
        {
            FixTagValuesCollection output = new FixTagValuesCollection();

            foreach (IParameter_t parameter in this.Items)
            {
                if (parameter.FixTag != null && parameter.WireValue != null)
                    output.Add((FixTag)parameter.FixTag, parameter.WireValue);
            }

            return output;
        }
    }
}
