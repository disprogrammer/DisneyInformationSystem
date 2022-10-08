using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace DisneyInformationSystem.Business.Exceptions.Business
{
    /// <summary>
    /// Email Not Found Exception
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class EmailNotFoundException : DisApplicationBusinessException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailNotFoundException"/> exception class.
        /// </summary>
        public EmailNotFoundException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailNotFoundException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public EmailNotFoundException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailNotFoundException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public EmailNotFoundException(string message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailNotFoundException"/> exception class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information instance.</param>
        /// <param name="streamingContext">the serialization streaming context instance.</param>
        protected EmailNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext) { }
    }
}