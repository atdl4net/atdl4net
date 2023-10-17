#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Runtime.Serialization;

namespace Atdl4net.Diagnostics.Exceptions
{
    /// <summary>Provides a base exception class for all Atdl4net custom exceptions.</summary>
    [Serializable]
    public class Atdl4netException : System.Exception
    {
        /// <summary>Initializes a new instance of the Atdl4netException class; for serialization purposes only.</summary>
        protected Atdl4netException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>Initializes a new instance of the Atdl4netException class with a specified error message.</summary>
        public Atdl4netException(string message)
            : base(message)
        {
        }

        /// <summary>Initializes a new instance of the Atdl4netException class with a specified error message and a reference to the inner exception that is the
        /// cause of this exception.</summary>
        public Atdl4netException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
