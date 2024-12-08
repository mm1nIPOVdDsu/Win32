using System;
using System.IO;

namespace Win32.Common.Services.WindowsService
{
    /// <summary>
    ///     TODO: Summary
    /// </summary>
    public interface IWindowsServiceService : IServiceBase
    {
        /// <summary>
        ///     Installs a windows service. Cannot install a service as a user.
        /// </summary>
        /// <param name="servicePath">The path to the executable windows service.</param>
        /// <returns>True if the service is successfully installed.</returns>
        /// <exception cref="FileNotFoundException">Thrown when InstallUtil is not on the system.</exception>
        /// <exception cref="ArgumentNullException">Service path cannot be empty.</exception>
        /// <exception cref="FileNotFoundException"><paramref name="servicePath"/></exception>
        bool InstallService(string servicePath);
        /// <summary>
        ///     Checks if a service exists.
        /// </summary>
        /// <param name="serviceName">The name of the service. Not the display name.</param>
        /// <returns>True if the service exists.</returns>
        /// 
        /// <exception cref="ArgumentNullException">Service name cannot be empty.</exception>
        bool ServiceExists(string serviceName);
        /// <summary>
        ///     Checks if a service is running.
        /// </summary>
        /// <param name="serviceName">The name of the service. Not the display name.</param>
        /// <returns><see cref="ServiceStatus"/></returns>
        /// <exception cref="ArgumentNullException">Service name cannot be empty.</exception>
        ServiceStatus ServiceStatus(string serviceName);
        /// <summary>
        ///     Start a stopped service.
        /// </summary>
        /// <param name="serviceName">The name of the service. Not the display name.</param>
        /// <returns>True if the service was started successfully.</returns>
        /// <exception cref="ArgumentNullException">Service name cannot be empty.</exception>
        bool StartService(string serviceName);
        /// <summary>
        ///     Stops a running service.
        /// </summary>
        /// <param name="serviceName">The name of the service. Not the display name.</param>
        /// <returns>True if the service was stopped successfully.</returns>
        /// <exception cref="ArgumentNullException">Service name cannot be empty.</exception>
        bool StopService(string serviceName);
        /// <summary>
        ///     Uninstalls a windows service.
        /// </summary>
        /// <param name="serviceName">The name of the service.</param>
        /// <returns>True if the service is successfully installed.</returns>
        /// <exception cref="FileNotFoundException">Thrown when InstallUtil is not on the system.</exception>
        /// <exception cref="ArgumentNullException">Service name cannot be empty.</exception>
        /// <exception cref="Exception">Cannot uninstall system services.</exception>
        bool UninstallService(string serviceName);
    }
}
