using System;
using System.Collections.Generic;
using System.Runtime.Versioning;

namespace Win32.Common.Services.Registry
{
    /// <summary>
    ///     Service for interacting with the machine's registry.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public interface IRegistryService : IServiceBase
    {
        /// <summary>
        ///     Checks if the current user has write permission to the provided registry key (folder).
        /// </summary>
        /// <param name="root">The <see cref="RegistryRoot"/> the key resides under.</param>
        /// <param name="path">The path to the registry key (folder).</param>
        /// <param name="bitness">The x86 or x64 view of the registry. Default is x64.</param>
        /// <returns>True if the current user has write permission.</returns>
        public bool CanWrite(RegistryRoot root, string path, RegistryBitness bitness = RegistryBitness.x64) => CanWrite(root, path, Environment.UserName, bitness);
        /// <summary>
        ///     Checks if given user has write permission to the provided registry key (folder).
        /// </summary>
        /// <remarks>Check on user name uses "IdentityReference.Value.Contains" to check for the user name in the permissions set.</remarks>
        /// <param name="root">The <see cref="RegistryRoot"/> the key resides under.</param>
        /// <param name="path">The path to the registry key (folder).</param>
        /// <param name="username">The full user name to check the permission for (Environment.UserName). Case sensitive.</param>
        /// <param name="bitness">The x86 or x64 view of the registry. Default is x64.</param>
        /// <returns>True if the user has write permission.</returns>
        public bool CanWrite(RegistryRoot root, string path, string username, RegistryBitness bitness = RegistryBitness.x64);
        /// <summary>
        ///     Determines if a key exists.
        /// </summary>
        /// <param name="root">The <see cref="RegistryRoot"/> of the registry key.</param>
        /// <param name="path">The path to the key without the registry root prefix.</param>
        /// <param name="keyName">The name of the key to check for existence.</param>
        /// <param name="bitness">The x86 or x64 view of the registry. Default is x64.</param>
        /// <returns>True if the key exists.</returns>
        public bool KeyExists(RegistryRoot root, string path, string keyName, RegistryBitness bitness = RegistryBitness.x64);
        /// <summary>
        ///     Gets the value of a registry key.
        /// </summary>
        /// <param name="root">The <see cref="RegistryRoot"/> of the registry key.</param>
        /// <param name="path">The path to the key without the registry root prefix.</param>
        /// <param name="keyName">The name of the key to get the value of.</param>
        /// <param name="bitness">The x86 or x64 view of the registry. Default is x64.</param>
        /// <returns>The value of the registry key as an object.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="path"/> or <paramref name="root"/> is null or empty.</exception>
        public object? GetKeyValue(RegistryRoot root, string path, string keyName, RegistryBitness bitness = RegistryBitness.x64);
        /// <summary>
        ///     Gets the value of a registry key.
        /// </summary>
        /// <typeparam name="T">The type of the registry value.</typeparam>
        /// <param name="root">The <see cref="RegistryRoot"/> of the registry key.</param>
        /// <param name="path">The path to the key without the registry root prefix.</param>
        /// <param name="keyName">The name of the key to get the value of.</param>
        /// <param name="bitness">The x86 or x64 view of the registry. Default is x64.</param>
        /// <returns>The value of the registry key as an object.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="path"/> or <paramref name="root"/> is null or empty.</exception>
        public T? GetKeyValue<T>(RegistryRoot root, string path, string keyName, RegistryBitness bitness = RegistryBitness.x64);
        /// <summary>
        ///     Gets all the child key values.
        /// </summary>
        /// <param name="root">The <see cref="RegistryRoot"/> of the registry key.</param>
        /// <param name="path">The path to the key without the registry root prefix.</param>
        /// <param name="bitness">The x86 or x64 view of the registry. Default is x64.</param>
        /// <returns>A <see cref="Array"/> of registry key value names.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="path"/> is null or empty.</exception>
        /// <exception cref="Exception">Thrown if <paramref name="path"/> cannot be found.</exception>
        public string[] GetSubKeyNames(RegistryRoot root, string path, RegistryBitness bitness = RegistryBitness.x64);
        /// <summary>
        ///     Gets all child keys and and values.
        /// </summary>
        /// <param name="root">The <see cref="RegistryRoot"/> of the registry key.</param>
        /// <param name="path">The path to the key without the registry root prefix.</param>
        /// <param name="bitness">The x86 or x64 view of the registry. Default is x64.</param>
        /// <returns>A <see cref="Dictionary{TKey, TValue}"/> of string keys and values.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="path"/> is null or empty.</exception>
        /// <exception cref="Exception">Thrown if <paramref name="path"/> cannot be found.</exception>
        public Dictionary<string, object?> GetSubKeyNamesAndValues(RegistryRoot root, string path, RegistryBitness bitness = RegistryBitness.x64);
        /// <summary>
        ///     Sets the value of a registry key.
        /// </summary>
        /// <param name="root">The <see cref="RegistryRoot"/> of the registry key.</param>
        /// <param name="path">The path to the key without the registry root prefix.</param>
        /// <param name="keyName">The name of the key to set the value of.</param>
        /// <param name="keyValue">The value of the registry key.</param>
        /// <param name="bitness">The x86 or x64 view of the registry. Default is x64.</param>
        /// <returns>True if successful.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="path"/>, <paramref name="keyName"/>, or <paramref name="keyValue"/> are null or empty.</exception>
        public bool SetKeyValue(RegistryRoot root, string path, string keyName, object keyValue, RegistryBitness bitness = RegistryBitness.x64);
    }
}
