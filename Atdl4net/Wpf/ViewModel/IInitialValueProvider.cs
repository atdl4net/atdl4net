using System;
using System.Linq;
using Atdl4net.Fix;

namespace Atdl4net.Wpf.ViewModel
{
    public interface IInitialValueProvider
    {
        FixTagValuesCollection InitialValues { get; set; }
    }
}
