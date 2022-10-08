using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace DisneyInformationSystem.Business.Exceptions.Business
{
    /// <summary>
    /// Invalid Console Explore Menu String Exception.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class InvalidConsoleExploreMenuStringException : DisApplicationBusinessException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidConsoleExploreMenuStringException"/> exception class.
        /// </summary>
        public InvalidConsoleExploreMenuStringException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidConsoleExploreMenuStringException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public InvalidConsoleExploreMenuStringException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidConsoleExploreMenuStringException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public InvalidConsoleExploreMenuStringException(string message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidConsoleExploreMenuStringException"/> exception class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information instance.</param>
        /// <param name="streamingContext">the serialization streaming context instance.</param>
        protected InvalidConsoleExploreMenuStringException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext) { }
    }
}