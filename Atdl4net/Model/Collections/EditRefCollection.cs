#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System.Collections.ObjectModel;
using Atdl4net.Model.Elements;
using Atdl4net.Validation;

namespace Atdl4net.Model.Collections
{
    /// <summary>
    /// Collection used to store typed instances of EditRef_t.
    /// </summary>
    /// <typeparam name="T">Type.</typeparam>
    public class EditRefCollection<T> : KeyedCollection<string, EditRef_t<T>> where T : class, IValueProvider
    {
        private readonly EditEvaluatingCollection<T> _evaluatingCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditRefCollection{T}"/> class.
        /// </summary>
        public EditRefCollection()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditRefCollection{T}"/> class.
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

        /// <summary>
        /// Gets the key for items in this collection, i.e., the Edit_t ID.
        /// </summary>
        /// <param name="item">EditRef_t.</param>
        /// <returns>Edit_t ID.</returns>
        protected override string GetKeyForItem(EditRef_t<T> item)
        {
            return item.Id;
        }
    }
}
