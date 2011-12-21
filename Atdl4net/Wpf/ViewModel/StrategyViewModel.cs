#region Copyright (c) 2010-2011, Cornerstone Technology Limited. http://atdl4net.org
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

using System;
using Atdl4net.Model.Elements;
using Atdl4net.Utility;

namespace Atdl4net.Wpf.ViewModel
{
    /// <summary>
    /// View Model that provides the link between the Atdl4net Model (<see cref="Strategy_t"/>) and the 
    /// View (<see cref="Atdl4netControl"/>).
    /// </summary>
    public class StrategyViewModel : IDisposable
    {
        private bool _disposed;

        /// <summary>
        /// Gets the set of controls (<see cref="ControlWrapper"/>s) for this strategy (<see cref="StrategyViewModel"/>).
        /// </summary>
        public ViewModelControlCollection Controls { get; private set; }

        /// <summary>
        /// Initializes a new <see cref="StrategyViewModel"/> 
        /// </summary>
        /// <param name="strategy"><see cref="Strategy_t"/> for this View Model.</param>
        /// <param name="mode">Data entry mode.</param>
        public StrategyViewModel(Strategy_t strategy, DataEntryMode mode)
        {
            Controls = new ViewModelControlCollection(strategy, mode);

            Controls.RefreshState();
        }

        #region IDisposable Members and support

        void IDisposable.Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Cleans up when this instance of <see cref="StrategyViewModel"/> is no longer required.
        /// </summary>
        /// <param name="disposing">True if this object is being disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    (Controls as IDisposable).Dispose();

                    Controls = null;
                }

                _disposed = true;
            }
        }

        #endregion
    }
}
