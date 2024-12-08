using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Security.AccessControl;
using System.Security.Principal;

using Microsoft.Extensions.Logging;
using Microsoft.Win32;

namespace Win32.Common.Services.Registry
{
    /// <summary>
    ///     Service for interacting with the machine's registry.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class RegistryService : IRegistryService
    {
        private readonly ILogger<RegistryService> _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RegistryService"/> class.
        /// </summary>
        /// <param name="logger">An instance of a <see cref="ILogger{RegistryService}"/>.</param>
        public RegistryService(ILogger<RegistryService> logger) => _logger = logger;

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
        public bool CanWrite(RegistryRoot root, string path, string username, RegistryBitness bitness = RegistryBitness.x64)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            try
            {
                _logger.LogDebug($"Normalizing the provided registry path.");
                var normalizedPath = NormalizePath(path);

                _logger.LogDebug("Opening {root} registry hive.", root);
                using (var rootNode = GetRootNode(root, bitness))
                {
                    _logger.LogDebug("Opening sub-key {root}.", path);
                    using (var child = rootNode.OpenSubKey(path))
                    {
                        if (child is null)
                            throw new Exception($"The key {root}\\{path} does not exist.");

                        _logger.LogDebug("Getting access control for registry key.");
                        var access = child.GetAccessControl(AccessControlSections.Access);
                        var rules = access.GetAccessRules(true, true, typeof(NTAccount)).Cast<RegistryAccessRule>();

                        _logger.LogDebug("Checking if user has write permission.");
                        return rules.Any(x => x.IdentityReference.Value.Contains(username) && x.AccessControlType == AccessControlType.Allow &&
                                x.RegistryRights == (RegistryRights.CreateSubKey | RegistryRights.FullControl | RegistryRights.SetValue | RegistryRights.WriteKey));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking registry permission.");
                return false;
            }
        }
        /// <summary>
        ///     Determines if a key exists.
        /// </summary>
        /// <param name="root">The <see cref="RegistryRoot"/> of the registry key.</param>
        /// <param name="path">The path to the key without the registry root prefix.</param>
        /// <param name="keyName">The name of the key to check for existence.</param>
        /// <param name="bitness">The x86 or x64 view of the registry. Default is x64.</param>
        /// <returns>True if the key exists.</returns>
        public bool KeyExists(RegistryRoot root, string path, string keyName, RegistryBitness bitness = RegistryBitness.x64)
        {
            _logger.LogDebug("Determining if the key {keyPath}.", $"{GetRootNodeString(root)}\\{path}\\{keyName}");
            return GetKeyValue(root, path, keyName, bitness) is not null;
        }
        /// <summary>
        ///     Gets the value of a registry key.
        /// </summary>
        /// <param name="root">The <see cref="RegistryRoot"/> of the registry key.</param>
        /// <param name="path">The path to the key without the registry root prefix.</param>
        /// <param name="keyName">The name of the key to get the value of.</param>
        /// <param name="bitness">The x86 or x64 view of the registry. Default is x64.</param>
        /// <returns>The value of the registry key as an object.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="path"/> or <paramref name="root"/> is null or empty.</exception>
        public object? GetKeyValue(RegistryRoot root, string path, string keyName, RegistryBitness bitness = RegistryBitness.x64)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrEmpty(keyName))
                throw new ArgumentNullException(nameof(keyName));

            _logger.LogDebug($"Normalizing the provided registry path.");
            path = path.Trim();
            keyName = keyName.Trim();
            if (path[0] == '\\')
                path = path.Remove(0, 1);
            if (path.EndsWith('\\'))
                path = path.Remove(path.Length - 1);
            var keyPath = $"{GetRootNodeString(root)}\\{path}";

            _logger.LogDebug("Getting the value of {keyPath}\\{keyName}.", keyPath, keyName);
            var value = Microsoft.Win32.Registry.GetValue(keyPath, keyName, null);

            _logger.LogDebug("Returning {value}.", value);
            return value;
        }
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
        public T? GetKeyValue<T>(RegistryRoot root, string path, string keyName, RegistryBitness bitness = RegistryBitness.x64)
        {
            var value = GetKeyValue(root, path, keyName, bitness);
            return value is null ? default : (T)value;
        }
        /// <summary>
        ///     Gets all the child key values.
        /// </summary>
        /// <param name="root">The <see cref="RegistryRoot"/> of the registry key.</param>
        /// <param name="path">The path to the key without the registry root prefix.</param>
        /// <param name="bitness">The x86 or x64 view of the registry. Default is x64.</param>
        /// <returns>A <see cref="Array"/> of registry key value names.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="path"/> is null or empty.</exception>
        /// <exception cref="Exception">Thrown if <paramref name="path"/> cannot be found.</exception>
        public string[] GetSubKeyNames(RegistryRoot root, string path, RegistryBitness bitness = RegistryBitness.x64)
        {
            if(string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            using (var registryKey = GetRootNode(root, bitness).OpenSubKey(path))
            {
                if (registryKey is null)
                    throw new Exception($"The registry path '{path}' cannot be found.");

                return registryKey.GetValueNames();
            }
        }
        /// <summary>
        ///     Gets all child keys and and values.
        /// </summary>
        /// <param name="root">The <see cref="RegistryRoot"/> of the registry key.</param>
        /// <param name="path">The path to the key without the registry root prefix.</param>
        /// <param name="bitness">The x86 or x64 view of the registry. Default is x64.</param>
        /// <returns>A <see cref="Dictionary{TKey, TValue}"/> of string keys and values.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="path"/> is null or empty.</exception>
        /// <exception cref="Exception">Thrown if <paramref name="path"/> cannot be found.</exception>
        public Dictionary<string, object?> GetSubKeyNamesAndValues(RegistryRoot root, string path, RegistryBitness bitness = RegistryBitness.x64)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            using (var registryKey = GetRootNode(root, bitness).OpenSubKey(path))
            {
                if (registryKey is null)
                    throw new Exception($"The registry path '{path}' cannot be found.");

                var keyNames = registryKey.GetValueNames();
                var response = new Dictionary<string, object?>();
                foreach(var keyName in keyNames)
                {
                    // TODO: If there is a need to get the value kind, use the following:
                    //var kind = registryKey.GetValueKind(keyName);
                    var value = registryKey.GetValue(keyName);
                    response.Add(keyName, value);
                }

                return response;
            }
        }
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
        public bool SetKeyValue(RegistryRoot root, string path, string keyName, object keyValue, RegistryBitness bitness = RegistryBitness.x64)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrEmpty(keyName))
                throw new ArgumentNullException(nameof(keyName));
            if (keyValue is null)
                throw new ArgumentNullException(nameof(keyValue));

            Microsoft.Win32.Registry.SetValue($"{GetRootNodeString(root)}\\{path}", keyName, keyValue);
            return true;
        }

        /// <summary>
        ///     Normalizes the path of the registry key to set it up for use.
        /// </summary>
        /// <param name="path">The unnormalized path.</param>
        /// <returns>A normalized path.</returns>
        private string NormalizePath(string path)
        {
            path = path.Trim();
            if (path[0] == '\\')
                path = path.Remove(0, 1);
            if (path.EndsWith('\\'))
                path = path.Remove(path.Length - 1);
            return path;
        }
        /// <summary>
        ///     Gets the string representation of the <see cref="RegistryRoot"/> enum.
        /// </summary>
        /// <param name="root"><see cref="RegistryRoot"/></param>
        /// <returns>The registry root node as a string.</returns>
        private string GetRootNodeString(RegistryRoot root)
        {
            _logger.LogDebug("Getting string root for {root}.", root);
            switch (root)
            {
                case RegistryRoot.ClassesRoot:
                    return "HKEY_CLASSES_ROOT";
                case RegistryRoot.CurrentUser:
                    return "HKEY_CURRENT_USER";
                case RegistryRoot.LocalMachine:
                    return "HKEY_LOCAL_MACHINE";
                case RegistryRoot.Users:
                    return "HKEY_USERS";
                default:
                    return "HKEY_CURRENT_USER";
            }
        }
        /// <summary>
        ///     Gets the <see cref="Microsoft.Win32.Registry"/> root from the passed in <see cref="RegistryRoot"/>.
        /// </summary>
        /// <param name="root">The <see cref="RegistryRoot"/>.</param>
        /// <param name="bitness">32 or 64 bit registry.</param>
        /// <returns>The <see cref="Microsoft.Win32.Registry"/> root node.</returns>
        private RegistryKey GetRootNode(RegistryRoot root, RegistryBitness bitness)
        {
            var regRoot = RegistryHive.CurrentUser;
            switch (root)
            {
                case RegistryRoot.ClassesRoot:
                    regRoot = RegistryHive.ClassesRoot;
                    break;
                case RegistryRoot.CurrentUser:
                    regRoot = RegistryHive.CurrentUser;
                    break;
                case RegistryRoot.LocalMachine:
                    regRoot = RegistryHive.LocalMachine;
                    break;
                case RegistryRoot.Users:
                    regRoot = RegistryHive.Users;
                    break;
            }

            var regView = bitness == RegistryBitness.x64 ? RegistryView.Registry64 : RegistryView.Registry32;
            return RegistryKey.OpenBaseKey(regRoot, regView);
        }
    }
}
