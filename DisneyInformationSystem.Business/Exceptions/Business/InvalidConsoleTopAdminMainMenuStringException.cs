using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace DisneyInformationSystem.Business.Exceptions.Business
{
    /// <summary>
    /// Invalid Console Top Admin Main Menu String Exception.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class InvalidConsoleTopAdminMainMenuStringException : DisApplicationBusinessException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidConsoleTopAdminMainMenuStringException"/> exception class.
        /// </summary>
        public InvalidConsoleTopAdminMainMenuStringException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidConsoleTopAdminMainMenuStringException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public InvalidConsoleTopAdminMainMenuStringException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidConsoleTopAdminMainMenuStringException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public InvalidConsoleTopAdminMainMenuStringException(string message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidConsoleTopAdminMainMenuStringException"/> exception class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information instance.</param>
        /// <param name="streamingContext">the serialization streaming context instance.</param>
        protected InvalidConsoleTopAdminMainMenuStringException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext) { }
    }
}