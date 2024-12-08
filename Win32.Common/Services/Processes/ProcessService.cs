using System;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Security.Principal;

using Microsoft.Extensions.Logging;

using static Win32.Common.Unmanaged.AdvApi32.ProcessThreadsApi;
using static Win32.Common.Unmanaged.Kernel32.HandleApi;

namespace Win32.Common.Services.Processes
{
    /// <summary>
    ///     Service for interacting with processes.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class ProcessService : IProcessService
    {
        private readonly ILogger<ProcessService> _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ProcessService"/> class.
        /// </summary>
        /// <param name="logger">An instance of a <see cref="ILogger{ProcessService}"/>.</param>
        public ProcessService(ILogger<ProcessService> logger)
        {
            if (logger is null)
                throw new ArgumentNullException(nameof(logger));

            _logger = logger;
        }

        /// <summary>
        ///     Determines if a process is running with admin rights.
        /// </summary>
        /// <param name="processId">The id of the process.</param>
        /// <returns>True if the process is running with admin rights.</returns>
        public bool IsAdminGroupMember(int processId)
        {
            var hToken = IntPtr.Zero;
            try
            {
                /* Alternate method
                    using (var user = WindowsIdentity.GetCurrent())
                    {
                        if (user.Groups is null)
                            return false;

                        return user.Groups.Any(x => x.Value == "BUILTIN\\Administrators");
                    }
                 */
                _logger.LogInformation("Attempting to get process membership.");
                _logger.LogDebug("Getting process token.");
                OpenProcessToken(Process.GetProcessById(processId).Handle, 8u, ref hToken);
                var owner = new WindowsIdentity(hToken).Owner;
                if (owner is null)
                    throw new Exception("Cannot get the owner of the process.");

                _logger.LogDebug("Getting process owner security group.");
                return owner.IsWellKnown(WellKnownSidType.BuiltinAdministratorsSid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting process group membership.");
                throw;
            }
            finally
            {
                if (hToken != IntPtr.Zero)
                    CloseHandle(hToken);
            }
        }
    }
}
