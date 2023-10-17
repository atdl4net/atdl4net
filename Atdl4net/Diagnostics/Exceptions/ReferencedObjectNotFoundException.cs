#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;

namespace Atdl4net.Diagnostics.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a reference from one object to another cannot be resolved; for example, if a 
    /// control has a parameterRef value but there is no corresponding parameter with that name.
    /// </summary>
    [Serializable]
    public class ReferencedObjectNotFoundException : Atdl4netException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferencedObjectNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ReferencedObjectNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferencedObjectNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ReferencedObjectNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
