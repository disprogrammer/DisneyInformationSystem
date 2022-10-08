using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace DisneyInformationSystem.Business.Exceptions.Technical
{
    /// <summary>
    /// Phone Number Invalid Exception
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class PhoneNumberInvalidException : DisApplicationTechnicalException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumberInvalidException"/> exception class.
        /// </summary>
        public PhoneNumberInvalidException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumberInvalidException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public PhoneNumberInvalidException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumberInvalidException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public PhoneNumberInvalidException(string message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumberInvalidException"/> exception class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information instance.</param>
        /// <param name="streamingContext">the serialization streaming context instance.</param>
        protected PhoneNumberInvalidException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext) { }
    }
}