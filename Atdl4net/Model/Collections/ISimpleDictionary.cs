#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

namespace Atdl4net.Model.Collections
{
    /// <summary>
    /// Accessor interface that simple dictionary collections must implement.  Lookup is via string-based key.
    /// </summary>
    /// <typeparam name="T">Type of item that this SimpleDictionary provides access to.</typeparam>
    public interface ISimpleDictionary<T>
    {
        /// <summary>
        /// Gets the item specified by the supplied key.
        /// </summary>
        /// <param name="key">String-based key for the desired item.</param>
        /// <returns>Item whose key matches the supplied value.  Behaviour is undefined if the key cannot be found.</returns>
        T this[string key] { get; }

        /// <summary>
        /// Indicates whether an item with the supplied key is present in the collection.
        /// </summary>
        /// <param name="key">String-based key to check for.</param>
        /// <returns>True if an item with the supplied key is present; false otherwise.</returns>
        bool Contains(string key);
    }
}
