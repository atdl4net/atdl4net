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

namespace Atdl4net.Model.Types
{
    /// <summary>
    /// 'string field representing the time represented based on ISO 8601. This is the time with a UTC offset to allow identification 
    /// of local time and timezone of that time.
    /// Format is HH:MM[:SS][Z | [ + | - hh[:mm]]] where HH = 00-23 hours, MM = 00-59 minutes, SS = 00-59 seconds, hh = 01-12 offset 
    /// hours, mm = 00-59 offset minutes.
    /// Example: 07:39Z is 07:39 UTC
    /// Example: 02:39-05 is five hours behind UTC, thus Eastern Time
    /// Example: 15:39+08 is eight hours ahead of UTC, Hong Kong/Singapore time
    /// Example: 13:09+05:30 is 5.5 hours ahead of UTC, India time'
    /// </summary>
    public class TZTimeOnly_t : TZDateTime
    {
    }
}
