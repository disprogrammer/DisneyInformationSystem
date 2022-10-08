using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace DisneyInformationSystem.Business.Exceptions.Business
{
    /// <summary>
    /// Invalid Sign In Type Exception
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class InvalidSignInTypeException : DisApplicationBusinessException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSignInTypeException"/> exception class.
        /// </summary>
        public InvalidSignInTypeException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSignInTypeException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public InvalidSignInTypeException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSignInTypeException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public InvalidSignInTypeException(string message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSignInTypeException"/> exception class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information instance.</param>
        /// <param name="streamingContext">the serialization streaming context instance.</param>
        protected InvalidSignInTypeException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext) { }
    }
}