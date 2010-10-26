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
using Atdl4net.Model.Controls;
using Atdl4net.Model.Elements;
using Atdl4net.Model.Enumerations;
using Atdl4net.Utility;
using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace Atdl4net.Wpf.ViewModel
{
    public class ControlWrapper : INotifyPropertyChanged, INotifyValueChanged, IBindable<ViewModelControlCollection>
    {
        private bool _visible = true;
        private bool _enabled = true;
        private DataEntryMode _dataEntryMode;
        private IParameter_t _referencedParameter;
        private ViewModelStateRuleCollection _stateRules;

        protected ControlWrapper(Control_t control, IParameter_t referencedParameter, DataEntryMode mode)
        {
            UnderlyingControl = control;
            _referencedParameter = referencedParameter;
            _dataEntryMode = mode;
        }

        public static ControlWrapper Create(Strategy_t underlyingStrategy, Control_t control, DataEntryMode mode)
        {
            IParameter_t referencedParameter = null;

            if (control.ParameterRef != null)
            {
                referencedParameter = underlyingStrategy.Parameters[control.ParameterRef];

                // TODO: Fix up exception.
                if (referencedParameter == null)
                    throw new Exception("No parameter found for control");
            }

            ControlWrapper wrapper;

#if !NET_40
            if (control is RadioButton_t)
                wrapper = new RadioButtonWrapper(control as RadioButton_t, referencedParameter, mode);
            else
#endif
                if (control is IListControl)
                    wrapper = ListControlWrapper.Create(control as IListControl, referencedParameter, mode);
                else
                    wrapper = new ControlWrapper(control, referencedParameter, mode);

            wrapper._stateRules = new ViewModelStateRuleCollection(wrapper, control.StateRules);

            return wrapper;
        }

        public Control_t UnderlyingControl { get; private set; }

        public void RefreshState()
        {
            _stateRules.RefreshState();
        }

        public string Id { get { return UnderlyingControl.Id; } }

        public virtual object Value
        {
            get { return UnderlyingControl.GetValue(); }

            set
            {
                Logger.DebugFormat("ControlWrapper for Control {0} value updated to {1} (data type {2}).",
                    Id, (value is ListItem_t) ? (value as ListItem_t).EnumId : value ?? "null", value != null ? value.GetType().Name : "N/A");

                if (UnderlyingControl.GetValue() != value)
                {
                    object oldValue = UnderlyingControl.GetValue();

                    UnderlyingControl.SetValue(value);

                    NotifyValueChanged(oldValue, value);
                }
            }
        }

        public Orientation Orientation
        {
            get
            {
                if (UnderlyingControl is IOrientableControl)
                    return (UnderlyingControl as IOrientableControl).Orientation == Orientation_t.Vertical ? Orientation.Vertical : Orientation.Horizontal;

                throw new InvalidOperationException("Orientation referenced on non-orientable control.");
            }
        }

        public string ReferencedParameterName
        {
            get { return UnderlyingControl.ParameterRef; }
        }

        public virtual void Reset()
        {
            Enabled = true;
            Visible = true;
        }

        public bool Visible
        {
            get { return _visible; }

            set
            {
                if (_visible != value)
                {
                    _visible = value;

                    NotifyPropertyChanged("Visible");
                }
            }
        }

        public bool Enabled
        {
            get 
            {
                switch (_dataEntryMode)
                {
                    case DataEntryMode.Amend:
                        // If we don't have a referenced parameter, then there is no good reason to prevent the user changing the
                        // value of this control (assuming a StateRule hasn't already disabled it, of course).
                        return (_referencedParameter == null) ? _enabled : (_referencedParameter.MutableOnCxlRpl == true) && _enabled;

                    case DataEntryMode.Create:
                        return _enabled;

                    default:
                        return false;
                }
            }

            set
            {
                // No need to worry about DataEntryMode as WPF will use the getter to retrieve the current Enabled state.
                if (_enabled != value)
                {
                    _enabled = value;

                    NotifyPropertyChanged("Enabled");
                }
            }
        }

        public void UpdateParameterValue()
        {
            if (_referencedParameter == null)
                return;

            //object value = ParameterValueConvertor.Convert(_underlyingControl, _underlyingControl.Value, _referencedParameter.Type);

            //_referencedParameter.ControlValue = value;
        }

        public void UpdateFromParameterValue()
        {
            if (_referencedParameter == null)
                return;

            UnderlyingControl.SetValue(_referencedParameter.ControlValue);
        }

        public void NotifyPropertyChanged(string name)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;

            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Notify interested parties that the value of this control has changed.
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <remarks>Note that this method is not intended for databinding, rather to notify Edit_t objects that one of their
        /// values has changed.</remarks>
        protected void NotifyValueChanged(object oldValue, object newValue)
        {
            EventHandler<ValueChangedEventArgs> valueChanged = ValueChanged;

            if (valueChanged != null)
                valueChanged(this, new ValueChangedEventArgs() { NewValue = newValue, OldValue = oldValue });
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region INotifyValueChanged Members

        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        #endregion

        #region IBindable<ViewControlCollection> Members

        void IBindable<ViewModelControlCollection>.Bind(ViewModelControlCollection target)
        {
            (_stateRules as IBindable<ViewModelControlCollection>).Bind(target);
        }

        #endregion IBindable<ViewControlCollection> Members
    }
}
