#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Linq;
using Atdl4net.Fix;
using Atdl4net.Model.Elements;
using Atdl4net.Model.Enumerations;
using Common.Logging;

namespace Atdl4net.Model.Controls.Support
{
    /// <summary>
    /// Generic base class for all controls.  Provides the ability to initialize controls based on the state of the InitPolicy, 
    /// InitFixField and InitValue attributes.
    /// </summary>
    /// <typeparam name="T">Specified the type of the InitValue.  Note that this may not be the same as the type that the
    /// control uses to store its data, for example InitValue for list controls is of type string whereas this type of
    /// control uses EnumState to store its state.</typeparam>
    public abstract class InitializableControl<T> : Control_t
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Model.Controls");

        /// <summary>
        /// Initializes a new <see cref="InitializableControl"/> instance with the specified identifier as id.
        /// </summary>
        /// <param name="id">Id of this control.</param>
        protected InitializableControl(string id)
            :base(id)
        {
        }

        /// <summary>The value used to pre-populate the GUI component when the order entry screen is initially rendered.</summary>
        public T InitValue { get; set; }

        /// <summary>
        /// Loads the initial value for this control based on the InitPolicy, InitFixField and InitValue attributes.
        /// </summary>
        /// <param name="controlInitValueProvider">Value provider for initializing control values from InitFixField.</param>
        /// <remarks>The spec states: 'If the value of the initPolicy attribute is undefined or equal to "UseValue" and the initValue attribute is 
        /// defined then initialize with initValue.  If the value is equal to "UseFixField" then attempt to initialize with the value of 
        /// the tag specified in the initFixField attribute. If the value is equal to "UseFixField" and it is not possible to access the 
        /// value of the specified fix tag then revert to using initValue. If the value is equal to "UseFixField", the field is not accessible,
        /// and initValue is not defined, then do not initialize.<br/br>
        /// Note that it is possible to initialize an enumerated control (e.g., DropDownList_t) from a FIX_ value.  In this case, it must
        /// be possible to retrieve a valid EnumID for the supplied FIX wire value.  The target parameter is used to translate the wire value
        /// into </remarks>
        public override void LoadInitValue(FixFieldValueProvider controlInitValueProvider)
        {
            // If UseFixField, then attempt to initialize with FIX field...
            if (InitPolicy == InitPolicy_t.UseFixField)
            {
                _log.Debug(m => m("Attempting to initialize control {0} from FIX field...", Id));

                if (!string.IsNullOrEmpty(InitFixField))
                {
                    string value;

                    if (controlInitValueProvider.TryGetValue(InitFixField, ParameterRef, out value))
                    {
                        if (LoadDefaultFromFixValue(value))
                        {
                            _log.Debug(m => m("Control {0} successfully initialized with value '{1}' from FIX field {2}", Id, value, InitFixField));

                            return;
                        }
                    }
                    else
                        _log.WarnFormat("Unable to initialize control {0} with FIX field {1} as no valid value was found", Id, InitFixField);
                }
                else
                    _log.WarnFormat("Unable to initialize control {0} with initPolicy = UseFixField as no valid initFixField was supplied", Id);
            }

            _log.Debug(m => m("Initializing control {0} with InitValue '{1}'...", Id, InitValue != null ? InitValue.ToString() : "null"));

            // Unable to initialize with FIX field so let's try using InitValue.  If InitValue is null, then control value will
            // be set to default/empty value.
            LoadDefaultFromInitValue();
        }

        /// <summary>
        /// Attempts to load the supplied FIX field value into this control.
        /// </summary>
        /// <param name="value">Value to set this control to.</param>
        /// <returns>true if it was possible to set the value of this control using the supplied value; false otherwise.</returns>
        protected abstract bool LoadDefaultFromFixValue(string value);

        /// <summary>
        /// Loads this control with any supplied InitValue. If InitValue is not supplied, then control value will
        /// be set to default/empty value.
        /// </summary>
        protected abstract void LoadDefaultFromInitValue();
    }
}
