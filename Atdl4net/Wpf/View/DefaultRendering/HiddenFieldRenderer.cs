#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System.ComponentModel.Composition;
using Atdl4net.Model.Controls;
using Common.Logging;

namespace Atdl4net.Wpf.View.DefaultRendering
{
    [Export(typeof(IWpfControlRenderer<HiddenField_t>))]
    internal class HiddenFieldRenderer : IWpfControlRenderer<HiddenField_t>
    {
        private static readonly ILog _log = LogManager.GetLogger("Atdl4net.Wpf.View");

        public void Render(WpfXmlWriter writer, HiddenField_t control)
        {
            _log.Debug(m => m("Rendering control {0} of type HiddenField_t using {1}", control.Id, this.GetType().FullName));
        }
    }
}
