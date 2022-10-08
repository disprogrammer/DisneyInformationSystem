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
    public class AdminTypeInvalidException : DisApplicationTechnicalException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdminTypeInvalidException"/> exception class.
        /// </summary>
        public AdminTypeInvalidException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminTypeInvalidException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public AdminTypeInvalidException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminTypeInvalidException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public AdminTypeInvalidException(string message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminTypeInvalidException"/> exception class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information instance.</param>
        /// <param name="streamingContext">the serialization streaming context instance.</param>
        protected AdminTypeInvalidException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext) { }
    }
}