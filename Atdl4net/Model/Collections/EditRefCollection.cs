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

using Atdl4net.Model.Elements;
using System.Collections.ObjectModel;

namespace Atdl4net.Model.Collections
{
    /// <summary>
    /// Collection used to store typed instances of EditRef_t.
    /// </summary>
    /// <typeparam name="T">Type.</typeparam>
    public class EditRefCollection<T> : KeyedCollection<string, EditRef_t<T>> where T : class, IValueProvider
    {
        private EditEvaluatingCollection<T> _evaluatingCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditRefCollection&lt;T&gt;"/> class.
        /// </summary>
        public EditRefCollection()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditRefCollection&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="evaluatingCollection">The evaluating collection.</param>
        public EditRefCollection(EditEvaluatingCollection<T> evaluatingCollection)
        {
            _evaluatingCollection = evaluatingCollection;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public new void Add(EditRef_t<T> item)
        {
            if (_evaluatingCollection != null)
                _evaluatingCollection.Add(item);

            base.Add(item);
        }

        /// <summary>
        /// Determines whether an EditRef with the specified ID is in the collection.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// 	<c>true</c> if [has edit ref] [the specified id]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasEditRef(string id)
        {
            return this.Contains(id);
        }

        protected override string GetKeyForItem(EditRef_t<T> item)
        {
            return item.Id;
        }
    }
}
