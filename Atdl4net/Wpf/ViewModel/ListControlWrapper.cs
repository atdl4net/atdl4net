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
using Atdl4net.Model.Controls;
using Atdl4net.Model.Elements;
using Atdl4net.Model.Types;
using Atdl4net.Utility;
using Common.Logging;

namespace Atdl4net.Wpf.ViewModel
{
    public class ListControlWrapper : ControlWrapper
    {
        private static readonly ILog _log = LogManager.GetLogger("ViewModel");

        private string _controlText; // Used to support EditableDropDownList
        private ViewModelListItemCollection _listItems;

        private ListControlWrapper(IListControl control, IParameter_t referencedParameter, DataEntryMode mode)
            : base(control as Control_t, referencedParameter, mode)
        {
        }

        public static ListControlWrapper Create(IListControl control, IParameter_t referencedParameter, DataEntryMode mode)
        {
            ListControlWrapper controlWrapper = new ListControlWrapper(control, referencedParameter, mode);

            controlWrapper._listItems = ViewModelListItemCollection.Create(controlWrapper);

            return controlWrapper;
        }

        public ViewModelListItemCollection ListItems { get { return _listItems; } }

        public string SelectedValue
        {
            get 
            {

                EnumState state = Value as EnumState;

                if (state == null || state.NonEnumValue != null)
                    return null;
                
                return state.GetFirstSelectedEnumId(); 
            }

            set
            {
                EnumState newState = Value as EnumState;

                if (newState != null)
                {
                    newState.ClearAll();

                    if (value != null)
                        newState[value] = true;
                    else
                        newState.NonEnumValue = string.Empty;

                    Value = newState;
                }
            }
        }

        public string Text
        {
            get
            {
                EnumState state = (EnumState)Value;

                if (state.NonEnumValue != null)
                    return state.NonEnumValue;

                return _controlText;
            }
            set
            {
                EnumState state = (EnumState)Value;

                if (state.NonEnumValue != null)
                {
                    state.NonEnumValue = value;

                    Value = state;
                }
                else
                    _controlText = value;
            }
        }

        public override object Value
        {
            get { return UnderlyingControl.GetValue(); }

            set
            {
                if (value != null)
                {
                    EnumState newState;

                    if (object.Equals(value, Control_t.NullValue))
                    {
                        newState = new EnumState((EnumState)UnderlyingControl.GetValue());

                        newState.ClearAll();
                    }
                    else
                        newState = (EnumState)value;

                    _log.DebugFormat("ControlWrapper for Control {0} value updated to {1}",
                        Id, newState.ToString());

                    if (!UnderlyingControl.GetValue().Equals(newState))
                    {
                        EnumState oldState = (EnumState)UnderlyingControl.GetValue();

                        UnderlyingControl.SetValue(newState);

                        NotifyValueChanged(oldState, newState);
                    }
                }
            }
        }

    }
}