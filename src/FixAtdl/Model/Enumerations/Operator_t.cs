﻿#region Copyright (c) 2010-2012, Cornerstone Technology Limited. http://atdl4net.org
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
