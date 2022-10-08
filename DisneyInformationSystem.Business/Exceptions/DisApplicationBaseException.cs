using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace DisneyInformationSystem.Business.Exceptions
{
    /// <summary>
    /// The base exception for the DIS Application.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Serializable]
    public abstract class DisApplicationBaseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisApplicationBaseException"/> exception class.
        /// </summary>
        protected DisApplicationBaseException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisApplicationBaseException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        protected DisApplicationBaseException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisApplicationBaseException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        protected DisApplicationBaseException(string message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisApplicationBaseException"/> exception class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information instance.</param>
        /// <param name="streamingContext">the serialization streaming context instance.</param>
        protected DisApplicationBaseException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext) { }
    }
}