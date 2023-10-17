#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

namespace Atdl4net.Model.Enumerations
{
    /// <summary>
    /// FIXatdl Operator type.
    /// </summary>
    public enum Operator_t
    {
        /// <summary>Exists i.e., the user has entered a value</summary>
        Exist,

        /// <summary>Not exists i.e., the user has not entered a value</summary>
        NotExist,

        /// <summary>Equal</summary>
        Equal,

        /// <summary>Less than</summary>
        LessThan,

        /// <summary>Greater than</summary>
        GreaterThan,

        /// <summary>Not equal</summary>
        NotEqual,

        /// <summary>Less than or equal</summary>
        LessThanOrEqual,

        /// <summary>Greater than or equal</summary>
        GreaterThanOrEqual
    }
}
