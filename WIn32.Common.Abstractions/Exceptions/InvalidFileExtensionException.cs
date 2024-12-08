using System;
using System.Runtime.Serialization;

namespace Win32.Common.Abstractions.Exceptions
{
    /// <summary>
    ///     An exception thrown when a file extension is invalid.
    /// </summary>
    public class InvalidFileExtensionException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidFileExtensionException"/> class.
        /// </summary>
        public InvalidFileExtensionException() : base() { }
        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidFileExtensionException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidFileExtensionException(string? message) : base(message) { }
        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidFileExtensionException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public InvalidFileExtensionException(string? message, Exception? innerException) : base(message, innerException) { }
        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidFileExtensionException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="info"/> is null.</exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException">Thrown if the class name is null or <see cref="System.Exception.HResult"/> is zero (0).</exception>
        public InvalidFileExtensionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
