using System;

namespace Win32.Common.Services.Container
{
    /// <summary>
    ///     Allows the abstraction of Unity
    /// </summary>
    public class Registration
    {
        /// <summary>
        ///     The name of the registration.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        ///     The interface that is registered.
        /// </summary>
        public Type? RegisteredType { get; set; } = null;
        /// <summary>
        ///     The concrete type that the RegisteredType is mapped to.
        /// </summary>
        public Type? MappedToType { get; set; } = null;
    }
}
