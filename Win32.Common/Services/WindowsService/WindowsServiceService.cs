using System;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.ServiceProcess;

using Microsoft.Extensions.Logging;

namespace Win32.Common.Services.WindowsService
{
    /// <summary>
    ///     TODO: Summary
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class WindowsServiceService : IWindowsServiceService
    {
        private const string installUtilPath = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe";
        private readonly ILogger<WindowsServiceService> _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="WindowsServiceService"/> class.
        /// </summary>
        /// <param name="loggingService">An instance of a <see cref="ILogger{WindowsServiceService}"/>.</param>
        public WindowsServiceService(ILogger<WindowsServiceService> loggingService) => _logger = loggingService;

        /// <summary>
        ///     Installs a windows service. Cannot install a service as a user.
        /// </summary>
        /// <param name="servicePath">The path to the executable windows service.</param>
        /// <returns>True if the service is successfully installed.</returns>
        /// <exception cref="FileNotFoundException">Thrown when InstallUtil is not on the system.</exception>
        /// <exception cref="ArgumentNullException">Service path cannot be empty.</exception>
        /// <exception cref="FileNotFoundException"><paramref name="servicePath"/></exception>
        public bool InstallService(string servicePath)
        {
            if (File.Exists(installUtilPath))
                throw new FileNotFoundException($@"Cannot find '{installUtilPath}'. Application must be present on the system to install/uninstall services.");
            if (string.IsNullOrEmpty(servicePath))
                throw new ArgumentNullException(nameof(servicePath), "Service path cannot be empty.");
            if (File.Exists(servicePath) == false)
                throw new FileNotFoundException(servicePath);

            _logger.LogDebug("Using 'InstallUtil.exe' to install the service at {servicePath}.", servicePath);
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = installUtilPath,
                Arguments = $"\"{servicePath}\" /LogToConsole=true",
            });

            return true;
        }
        /// <summary>
        ///     Checks if a service exists.
        /// </summary>
        /// <param name="serviceName">The name of the service. Not the display name.</param>
        /// <returns>True if the service exists.</returns>
        /// 
        /// <exception cref="ArgumentNullException">Service name cannot be empty.</exception>
        public bool ServiceExists(string serviceName) => string.IsNullOrEmpty(serviceName)
                ? throw new ArgumentNullException(nameof(serviceName), "Service name cannot be empty.")
                : ServiceController.GetServices().Any(x => x.ServiceName == serviceName);
        /// <summary>
        ///     Checks if a service is running.
        /// </summary>
        /// <param name="serviceName">The name of the service. Not the display name.</param>
        /// <returns><see cref="ServiceStatus"/></returns>
        /// <exception cref="ArgumentNullException">Service name cannot be empty.</exception>
        public ServiceStatus ServiceStatus(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName))
                throw new ArgumentNullException(nameof(serviceName), "Service name cannot be empty.");

            using (var service = new ServiceController(serviceName))
            {
                return GetStatus(service.Status);
            }
        }
        /// <summary>
        ///     Start a stopped service.
        /// </summary>
        /// <param name="serviceName">The name of the service. Not the display name.</param>
        /// <returns>True if the service was started successfully.</returns>
        /// <exception cref="ArgumentNullException">Service name cannot be empty.</exception>
        public bool StartService(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName))
                throw new ArgumentNullException(nameof(serviceName), "Service name cannot be empty.");

            try
            {
                using (var service = new ServiceController(serviceName))
                {
                    if (service.Status == ServiceControllerStatus.Running)
                        return true;

                    service.Start();

                    var counter = 0;
                    while (service.Status != ServiceControllerStatus.Running)
                    {
                        System.Threading.Thread.Sleep(500);
                        service.Refresh();
                        counter++;

                        if (counter > 20)
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error starting service.");
                return false;
            }
        }
        /// <summary>
        ///     Stops a running service.
        /// </summary>
        /// <param name="serviceName">The name of the service. Not the display name.</param>
        /// <returns>True if the service was stopped successfully.</returns>
        /// <exception cref="ArgumentNullException">Service name cannot be empty.</exception>
        public bool StopService(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName))
                throw new ArgumentNullException(nameof(serviceName), "Service name cannot be empty.");

            try
            {
                using (var service = new ServiceController(serviceName))
                {
                    if (service.Status == ServiceControllerStatus.Stopped)
                        return true;

                    service.Stop();

                    var counter = 0;
                    while (service.Status != ServiceControllerStatus.Stopped)
                    {
                        System.Threading.Thread.Sleep(500);
                        service.Refresh();
                        counter++;

                        if (counter > 20)
                            return false;
                    }

                    return true;
                }
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error stopping service.");
                return false;
            }
        }

        /// <summary>
        ///     Uninstalls a windows service.
        /// </summary>
        /// <param name="serviceName">The name of the service.</param>
        /// <returns>True if the service is successfully installed.</returns>
        /// <exception cref="FileNotFoundException">Thrown when InstallUtil is not on the system.</exception>
        /// <exception cref="ArgumentNullException">Service name cannot be empty.</exception>
        /// <exception cref="Exception">Cannot uninstall system services.</exception>
        public bool UninstallService(string serviceName)
        {
            if (File.Exists(installUtilPath))
                throw new FileNotFoundException($@"Cannot find '{installUtilPath}'. Application must be present on the system to install/uninstall services.");
            if (string.IsNullOrEmpty(serviceName))
                throw new ArgumentNullException(nameof(serviceName), "Service name cannot be empty.");

            using (var service = new ServiceController(serviceName))
            {
                if (service.ServiceType is ServiceType.KernelDriver or ServiceType.FileSystemDriver or
                    ServiceType.Adapter or ServiceType.RecognizerDriver)
                {
                    throw new Exception("Cannot uninstall system services.");
                }
            }

            _logger.LogDebug("Using 'InstallUtil.exe' to uninstall the service {serviceName}.", serviceName);
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = installUtilPath,
                Arguments = $"/U \"{serviceName}\" /LogToConsole=true",
            });

            return true;
        }

        #region Helpers
        /// <summary>
        ///     Gets the <see cref="ServiceStatus"/> from a <see cref="ServiceControllerStatus"/>.
        /// </summary>
        /// <param name="scmStatus"><see cref="ServiceControllerStatus"/></param>
        /// <returns>The <see cref="ServiceStatus"/> of a service.</returns>
        private ServiceStatus GetStatus(ServiceControllerStatus scmStatus) => (ServiceStatus)(int)scmStatus;
        #endregion
    }
}
