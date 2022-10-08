using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace DisneyInformationSystem.Business.Exceptions.Business
{
    /// <summary>
    /// Invalid Password Exception
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class InvalidPasswordException : DisApplicationBusinessException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPasswordException"/> exception class.
        /// </summary>
        public InvalidPasswordException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPasswordException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public InvalidPasswordException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPasswordException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public InvalidPasswordException(string message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPasswordException"/> exception class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information instance.</param>
        /// <param name="streamingContext">the serialization streaming context instance.</param>
        protected InvalidPasswordException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext) { }
    }
}