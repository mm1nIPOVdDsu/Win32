using System;
using System.Runtime.Versioning;
using System.Security.Principal;

namespace Win32.Common.Services.User
{
    /// <summary>
    ///     TODO: Summary
    /// </summary>
    [SupportedOSPlatform("windows")]
    public interface IUserService : IServiceBase
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
        ///     Gets the current user profile path.
        /// </summary>
        /// <returns>The user profile path.</returns>
        string? GetCurrentUserProfilePath();
        /// <summary>
        ///     Gets the time of last user activity.
        /// </summary>
        /// <returns></returns>
        DateTime GetLastActiveTime();
        /// <summary>
        ///     Gets the Unique User ID from the system.
        /// </summary>
        /// <returns>The unique user ID as a GUID.</returns>
        string GetUUID();
        /// <summary>
        ///     Checks if the currently logged on user has Administrator rights.
        /// </summary>
        /// <returns>True if the user has administrator rights.</returns>
        bool IsAdministrator();
    }
}
