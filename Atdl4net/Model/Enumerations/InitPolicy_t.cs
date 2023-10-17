#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

namespace Atdl4net.Model.Enumerations
{
    /// <summary>
    /// Describes how to initialize a control.  If the value of this attribute is undefined or equal to "UseValue" and 
    /// initValue is defined then initialize with initValue.  If the value is equal to "UseFixField" then attempt to 
    /// initialize with the value of the tag specified in initFixField. If the value is equal to "UseFixField" and it 
    /// is not possible to access the value of the specified fix tag then revert to using initValue. If the value is 
    /// equal to "UseFixField", the field is not accessible, and initValue is not defined, then do not initialize.
    /// </summary>
    public enum InitPolicy_t
    {
        /// <summary>If initValue is defined then initialize with initValue.</summary>
        UseValue,

        /// <summary>Attempt to initialize with the value of the tag specified in initFixField.</summary>
        UseFixField
    }
}
