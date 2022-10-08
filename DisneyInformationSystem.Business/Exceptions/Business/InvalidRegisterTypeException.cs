using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace DisneyInformationSystem.Business.Exceptions.Business
{
    /// <summary>
    /// Invalid Register Type Exception
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class InvalidRegisterTypeException : DisApplicationBusinessException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRegisterTypeException"/> exception class.
        /// </summary>
        public InvalidRegisterTypeException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRegisterTypeException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public InvalidRegisterTypeException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRegisterTypeException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public InvalidRegisterTypeException(string message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRegisterTypeException"/> exception class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information instance.</param>
        /// <param name="streamingContext">the serialization streaming context instance.</param>
        protected InvalidRegisterTypeException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext) { }
    }
}