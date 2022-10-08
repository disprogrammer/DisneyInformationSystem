﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace DisneyInformationSystem.Business.Exceptions.Technical
{
    /// <summary>
    /// Address Sign Not Found Exception
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class EmailAlreadyExistsException : DisApplicationTechnicalException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressSignNotFoundException"/> exception class.
        /// </summary>
        public EmailAlreadyExistsException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressSignNotFoundException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public EmailAlreadyExistsException(string message)
            : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressSignNotFoundException"/> exception class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public EmailAlreadyExistsException(string message, Exception inner)
            : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressSignNotFoundException"/> exception class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information instance.</param>
        /// <param name="streamingContext">the serialization streaming context instance.</param>
        protected EmailAlreadyExistsException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext) { }
    }
}