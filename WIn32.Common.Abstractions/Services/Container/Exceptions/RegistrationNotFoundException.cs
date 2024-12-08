using System;

namespace Win32.Common.Services.Container
{
    /// <summary>
    ///     Exception that occurs when an interface does not resolve to any types in the unity container.
    /// </summary>
    /// <typeparam name="T">The type that was attempting resolution.</typeparam>
    [Serializable]
    public class RegistrationNotFoundException<T> : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RegistrationNotFoundException{T}"/> class.
        /// </summary>
        public RegistrationNotFoundException() { }
        /// <summary>
        ///     Initializes a new instance of the <see cref="RegistrationNotFoundException{T}"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public RegistrationNotFoundException(string message) : base(message) { }
        /// <summary>
        ///     Initializes a new instance of the <see cref="RegistrationNotFoundException{T}"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The exception's inner exception.</param>
        public RegistrationNotFoundException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        ///     The type that was getting its concrete type resolved for.
        /// </summary>
        public T? ResolveFor { get; }
    }
}
