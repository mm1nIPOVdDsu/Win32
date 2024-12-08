using System;

namespace Win32.Common.Services.Container
{
    /// <summary>
    ///     Exception that occurs when multiple registrations are found in the unity container.
    /// </summary>
    /// <typeparam name="T">The type that was attempting resolution.</typeparam>
    [Serializable]
    public class MultipleRegistrationsFoundException<T> : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MultipleRegistrationsFoundException{T}"/> class.
        /// </summary>
        public MultipleRegistrationsFoundException() : base($"Cannot resolve from a single type when multiple registrations for {typeof(T)} are found.") { }
        /// <summary>
        ///     Initializes a new instance of the <see cref="MultipleRegistrationsFoundException{T}"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public MultipleRegistrationsFoundException(string message) : base(message) { }
        /// <summary>
        ///     Initializes a new instance of the <see cref="MultipleRegistrationsFoundException{T}"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The exception's inner exception.</param>
        public MultipleRegistrationsFoundException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        ///     The type that was getting its concrete type resolved for.
        /// </summary>
        public T? ResolveFor { get; }
    }
}
