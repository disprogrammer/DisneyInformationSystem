using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace DisneyInformationSystem.Business.Exceptions.Technical
{
    /// <summary>
    /// Admin Type Invalid Exception
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class AcronymInvalidException : DisApplicationTechnicalException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AcronymInvalidException"/> exception class.
        /// </summary>
        public AcronymInvalidException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AcronymInvalidException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public AcronymInvalidException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AcronymInvalidException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public AcronymInvalidException(string message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AcronymInvalidException"/> exception class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information instance.</param>
        /// <param name="streamingContext">the serialization streaming context instance.</param>
        protected AcronymInvalidException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext) { }
    }
}