using System;

namespace Win32.Common.Services.Registry
{
    /// <summary>
    ///     The type of registry change to filter for.
    /// </summary>
    [Flags]
    public enum RegistryNotifyFilter : uint
    {
        /// <summary>
        ///     Notify the caller if a subkey is added or deleted.
        /// </summary>
        AddedOrDeleted = 0x00000001,
        /// <summary>
        ///     Notify the caller of changes to the attributes of the key, such as the security descriptor information.
        /// </summary>
        AttributeChanged = 0x00000002,
        /// <summary>
        ///     Notify the caller of changes to a value of the key. This can include adding or deleting a value, or changing an existing value.
        /// </summary>
        ValueChanged = 0x00000004,
        /// <summary>
        ///     Notify the caller of changes to the security descriptor of the key.
        /// </summary>
        SecurityChanged = 0x00000008,
    }
}
