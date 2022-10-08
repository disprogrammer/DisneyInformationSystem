using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace DisneyInformationSystem.Business.Exceptions.Business
{
    /// <summary>
    /// Invalid Yes No Exception
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class InvalidYesNoException : DisApplicationBusinessException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidYesNoException"/> exception class.
        /// </summary>
        public InvalidYesNoException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidYesNoException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public InvalidYesNoException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidYesNoException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public InvalidYesNoException(string message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidYesNoException"/> exception class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information instance.</param>
        /// <param name="streamingContext">the serialization streaming context instance.</param>
        protected InvalidYesNoException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext) { }
    }
}