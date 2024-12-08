using System;
using System.Runtime.Versioning;

using Microsoft.Win32;

namespace Win32.Common.Services.Registry
{
    /// <summary>
    ///     Monitors for a change made to a registry key.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public interface IRegistryMonitorService : IDisposable
    {
        /// <summary>
        ///     
        /// </summary>
        bool Monitoring { get; }

        /// <summary>
        ///     An event raised when a change occurs to the defined registry key.
        /// </summary>
        event RegistryChangedHandler? Changed;
        /// <summary>
        ///     Event raised when an error occurs while monitoring for a change.
        /// </summary>
        event RegistryChangedHandler? Error;

        /// <summary>
        ///     Resumes the registry monitor.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown if <see cref="Start(RegistryHive, string, RegistryView, RegistryNotifyFilter)"/> has not been called.</exception>
        void Resume();
        /// <summary>
        ///     Starts the registry monitor.
        /// </summary>
        /// <param name="hive">The registry hive of the <paramref name="registryPath"/>.</param>
        /// <param name="registryPath">The path of a key to monitor sans the hive.</param>
        /// <param name="view">32 or 64 bit registry view. Uses the system default if no value is provided.</param>
        /// <param name="filter">The type of change to filter for. Default is a change in value.</param>
        void Start(RegistryHive hive, string registryPath, RegistryView view = RegistryView.Default, RegistryNotifyFilter filter = RegistryNotifyFilter.ValueChanged);
        /// <summary>
        ///     Stops the registry monitor.
        /// </summary>
        void Stop();
    }
}
