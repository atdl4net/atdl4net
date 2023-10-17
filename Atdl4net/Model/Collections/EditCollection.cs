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
    /// Collection used for storing instances of Edit_t, keyed on Edit ID.  This collection is used at the root Strategies_t and Strategy_t level.
    /// </summary>
    public class EditCollection : KeyedCollection<string, Edit_t>
    {
        /// <summary>
        /// Determines whether an Edit with the specified ID is present.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// 	<c>true</c> if the specified id has edit; otherwise, <c>false</c>.
        /// </returns>
        public bool HasEdit(string id)
        {
            return this.Contains(id);
        }

        protected override string GetKeyForItem(Edit_t item)
        {
            return item.Id;
        }

        /// <summary>
        /// Clones the Edit with the specified id.  Used to handle EditRefs.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Id">The id.</param>
        /// <returns></returns>
        public Edit_t<T> Clone<T>(string Id) where T : class, IValueProvider
        {
            Edit_t sourceEdit = this[Id];

            return Copy<T>(sourceEdit); 
        }

        /// <summary>
        /// Recursively copies an edit from this collection.  Used to handle EditRefs.
        /// </summary>
        /// <param name="source">Instance of Edit_t to be copied.</param>
        /// <returns>Copy of source Edit_t instance.</returns>
        private Edit_t<T> Copy<T>(Edit_t source) where T : class, IValueProvider
        {
            Edit_t<T> target = new Edit_t<T>() { Field = source.Field, Field2 = source.Field2, LogicOperator = source.LogicOperator, Operator = source.Operator, Value = source.Value };

            foreach (Edit_t child in source.Edits)
                target.Edits.Add(Copy<T>(child));

            return target;
        }
    }
}
