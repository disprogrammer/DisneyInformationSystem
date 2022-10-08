using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace DisneyInformationSystem.Business.Exceptions.Technical
{
    /// <summary>
    /// Invalid Property Type Exception.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class InvalidPropertyTypeException : DisApplicationTechnicalException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPropertyTypeException"/> exception class.
        /// </summary>
        public InvalidPropertyTypeException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPropertyTypeException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public InvalidPropertyTypeException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPropertyTypeException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public InvalidPropertyTypeException(string message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPropertyTypeException"/> exception class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information instance.</param>
        /// <param name="streamingContext">the serialization streaming context instance.</param>
        protected InvalidPropertyTypeException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext) { }
    }
}