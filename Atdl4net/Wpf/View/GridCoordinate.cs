#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

namespace Atdl4net.Wpf.View
{
    public struct GridCoordinate
    {
        public int Row;
        public int Column;

        public GridCoordinate(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}