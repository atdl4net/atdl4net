#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

namespace Atdl4net.Model.Elements
{
    /// <summary>
    /// Represents the FIXatdl Description sub-element that is used to provide additional descriptive information about an element.
    /// </summary>
    public class Description_t
    {
        /// <summary>
        /// Gets/sets the content of the Description.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Implicit cast operator that enables an arbitrary string to be assigned to the Description.
        /// </summary>
        /// <param name="value">String value to be assigned to the newly created Description.</param>
        /// <returns>New Description with the supplied string as the Content.</returns>
        public static implicit operator Description_t(string value)
        {
            return new Description_t { Content = value };
        }

        /// <summary>
        ///  Implicit case operator that enables a Description to be used where a string is expected.
        /// </summary>
        /// <param name="description">Description to treated as a string.</param>
        /// <returns>Content field of this description, i.e., the descriptive text.</returns>
        public static implicit operator string(Description_t description)
        {
            return description.Content;
        }
    }
}
