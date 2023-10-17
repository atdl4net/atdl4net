#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using Atdl4net.Diagnostics;
using Atdl4net.Resources;
using System;

namespace Atdl4net.Model.Elements
{
    public class ListItem_t : IComparable
    {
        public string EnumId { get; set; }
        public string UiRep { get; set; }
        public bool IsSelected { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is string)
                return (this.EnumId).CompareTo(obj as string);
            else
                throw ThrowHelper.New<InvalidOperationException>(this, ErrorMessages.CompareValueFailure, "ListItem_t", obj.GetType().FullName);
        }

        public override string ToString()
        {
            return UiRep;
        }
    }
}
