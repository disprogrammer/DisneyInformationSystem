using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace DisneyInformationSystem.Business.Exceptions.Technical
{
    /// <summary>
    /// Hashed Password Not Supported Exception.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class HashedPasswordNotSupportedException : DisApplicationTechnicalException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HashedPasswordNotSupportedException"/> exception class.
        /// </summary>
        public HashedPasswordNotSupportedException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HashedPasswordNotSupportedException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public HashedPasswordNotSupportedException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HashedPasswordNotSupportedException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public HashedPasswordNotSupportedException(string message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HashedPasswordNotSupportedException"/> exception class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information instance.</param>
        /// <param name="streamingContext">the serialization streaming context instance.</param>
        protected HashedPasswordNotSupportedException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext) { }
    }
}