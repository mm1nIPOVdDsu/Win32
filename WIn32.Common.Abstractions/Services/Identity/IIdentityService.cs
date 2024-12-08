using System;
using System.Runtime.Versioning;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Win32.Common.Services.Identity
{
    /// <summary>
    ///     Service for managing user identity.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public interface IIdentityService
    {
        /// <summary>
        ///     Gets the user context of a process.
        /// </summary>
        /// <param name="processId">The Id of the process to get a <see cref="WindowsIdentity"/> from.</param>
        /// <returns>The <see cref="WindowsIdentity"/> that owns the process.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        WindowsIdentity GetCurrentUserFromProcess(int processId);
        /// <summary>
        ///     Gets the user context of a process.
        /// </summary>
        /// <param name="processName">The name of the process to get a <see cref="WindowsIdentity"/> from.</param>
        /// <returns>The <see cref="WindowsIdentity"/> that owns the process.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        WindowsIdentity GetCurrentUserFromProcess(string processName);
        /// <summary>
        ///     Impersonates the active user.
        /// </summary>
        /// <returns>True if impersonation was successful.</returns>
        bool Impersonate();
        /// <summary>
        ///     Runs code in the context of the user.
        /// </summary>
        /// <param name="action">The action that will run in the user context.</param>
        void RunAsUser(Action action);
        /// <summary>
        ///     Runs code in the context of the user.
        /// </summary>
        /// <param name="func">The asynchronous function that will run in the user context.</param>
        void RunAsUserAsync(Func<Task> func);
    }
}
