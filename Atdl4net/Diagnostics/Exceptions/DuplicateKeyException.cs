#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;

namespace Atdl4net.Diagnostics.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an item is added to a collection but there is already an item with the same key in the collection.
    /// </summary>
    [Serializable]
    public class DuplicateKeyException : Atdl4netException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateKeyException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        public DuplicateKeyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateKeyException"/> class with a specified error message and a
        /// reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the innerException 
        /// parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block 
        /// that handles the inner exception. </param>
        public DuplicateKeyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
