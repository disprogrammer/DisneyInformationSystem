using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace DisneyInformationSystem.Business.Exceptions.Business
{
    /// <summary>
    /// Invalid Console Main Menu String Exception.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class InvalidConsoleMainMenuStringException : DisApplicationBusinessException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidConsoleMainMenuStringException"/> exception class.
        /// </summary>
        public InvalidConsoleMainMenuStringException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidConsoleMainMenuStringException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public InvalidConsoleMainMenuStringException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidConsoleMainMenuStringException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public InvalidConsoleMainMenuStringException(string message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidConsoleMainMenuStringException"/> exception class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information instance.</param>
        /// <param name="streamingContext">the serialization streaming context instance.</param>
        protected InvalidConsoleMainMenuStringException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext) { }
    }
}