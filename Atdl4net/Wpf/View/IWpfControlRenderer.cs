#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using Atdl4net.Model.Elements;

namespace Atdl4net.Wpf.View
{
    public interface IWpfControlRenderer<T> where T : Control_t
    {
        void Render(WpfXmlWriter writer, T control);
    }
}
