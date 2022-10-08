using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace DisneyInformationSystem.Business.Exceptions.Business
{
    /// <summary>
    /// The base exception for all technical exceptions in the Dis Application.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Serializable]
    public abstract class DisApplicationBusinessException : DisApplicationBaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisApplicationBusinessException"/> exception class.
        /// </summary>
        protected DisApplicationBusinessException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisApplicationBusinessException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        protected DisApplicationBusinessException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisApplicationBusinessException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        protected DisApplicationBusinessException(string message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisApplicationBusinessException"/> exception class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information instance.</param>
        /// <param name="streamingContext">the serialization streaming context instance.</param>
        protected DisApplicationBusinessException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext) { }
    }
}