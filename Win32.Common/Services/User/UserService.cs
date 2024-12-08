using System;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Principal;

using Microsoft.Extensions.Logging;

using static Win32.Common.Unmanaged.AdvApi32.ProcessThreadsApi;
using static Win32.Common.Unmanaged.Kernel32.HandleApi;
using static Win32.Common.Unmanaged.User32;

namespace Win32.Common.Services.User
{
    /// <summary>
    ///     Service for getting information about a user.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class UserService : IUserService
    {
        private readonly ILogger _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="logger">An instance of <see cref="ILogger{UserService}"/>.</param>
        public UserService(ILogger<UserService> logger) => _logger = logger;

        /// <summary>
        ///     Gets the user context of a process.
        /// </summary>
        /// <param name="processId">The Id of the process to get a <see cref="WindowsIdentity"/> from.</param>
        /// <returns>The <see cref="WindowsIdentity"/> that owns the process.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public WindowsIdentity GetCurrentUserFromProcess(int processId)
        {
            if (processId <= 0)
                throw new ArgumentException("Process id must be greater than 0.");

            var processHandle = IntPtr.Zero;
            try
            {
                _logger.LogInformation("Getting a Windows Identity from process {processId}.", processId);
                _logger.LogDebug("Getting process by name.");
                var explorerProcess = Process.GetProcessById(processId);
                if (explorerProcess is null)
                    throw new Exception("A user has not logged onto the system.");

                _logger.LogDebug("Opening process token.");
                OpenProcessToken(explorerProcess.Handle, 8, ref processHandle);

                _logger.LogDebug("Returning identity from process handle.");
                return new WindowsIdentity(processHandle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not get a Windows Identity from {processId}.", processId);
                throw;
            }
            finally
            {
                if (processHandle != IntPtr.Zero)
                {
                    _logger.LogDebug("Closing handle to process {processId}.", processId);
                    CloseHandle(processHandle);
                }
            }
        }
        /// <summary>
        ///     Gets the user context of a process.
        /// </summary>
        /// <param name="processName">The name of the process to get a <see cref="WindowsIdentity"/> from.</param>
        /// <returns>The <see cref="WindowsIdentity"/> that owns the process.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public WindowsIdentity GetCurrentUserFromProcess(string processName)
        {
            if (string.IsNullOrEmpty(processName))
                throw new ArgumentNullException(nameof(processName));

            var processHandle = IntPtr.Zero;
            try
            {
                _logger.LogInformation("Getting a Windows Identity from the process {processName}.", processName);
                _logger.LogDebug("Getting process by name.");
                var explorerProcess = Process.GetProcessesByName(processName).FirstOrDefault();
                if (explorerProcess is null)
                    throw new Exception("A user has not logged onto the system.");

                _logger.LogDebug("Opening process token.");
                OpenProcessToken(explorerProcess.Handle, 8, ref processHandle);

                _logger.LogDebug("Returning identity from process handle.");
                return new WindowsIdentity(processHandle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not get a Windows Identity from {processName}.", processName);
                throw;
            }
            finally
            {
                if (processHandle != IntPtr.Zero)
                {
                    CloseHandle(processHandle);
                }
            }
        }
        /// <summary>
        ///     Gets the current user profile path.
        /// </summary>
        /// <returns>The user profile path.</returns>
        public string? GetCurrentUserProfilePath()
        {
            using (var identity = GetCurrentUserFromProcess("explorer"))
            {
                if (identity.User is null)
                    throw new NullReferenceException(nameof(identity.User));

                var keyPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileList\" + identity.User.Value;

                var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keyPath, false);
                if (key is null)
                    throw new Exception("Could not open the key {keyPath}.");

                var profilePath = key.GetValue("ProfileImagePath") as string;

                return profilePath;
            }
        }
        /// <summary>
        ///     Gets the time of last user activity.
        /// </summary>
        /// <returns></returns>
        public DateTime GetLastActiveTime()
        {
            try
            {
                _logger.LogInformation("Getting the current user's last active time.");
                var lastInputInfo = new WinUser.LASTINPUTINFO();
                lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
                lastInputInfo.dwTime = 0;

                var lastInputTime = DateTime.Now;
                if (WinUser.GetLastInputInfo(ref lastInputInfo))
                    lastInputTime = DateTime.Now.AddMilliseconds((Environment.TickCount - lastInputInfo.dwTime) * -1);

                _logger.LogDebug("User's last input time was {lastInputTime}", lastInputTime);
                return lastInputTime;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting the user's last active time.");
                return DateTime.Now;
            }
        }
        /// <summary>
        ///     Gets the Unique User ID from the system.
        /// </summary>
        /// <returns>The unique user ID as a GUID.</returns>
        public string GetUUID()
        {
            var process = new Process();
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                FileName = "CMD.exe",
                Arguments = "/C wmic csproduct get UUID",
                CreateNoWindow = true
            };
            process.StartInfo = startInfo;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;

            _logger.LogDebug("Starting process to get the UUID.");
            process.Start();

            _logger.LogDebug("Waiting for process to finish.");
            process.WaitForExit();

            var output = process.StandardOutput.ReadToEnd().Replace("UUID", "").Trim();

            _logger.LogDebug($"Returning a value the UUID {output}.");
            return output;
        }
        /// <summary>
        ///     Checks if the currently logged on user has Administrator rights.
        /// </summary>
        /// <remarks>
        ///     This method is very slow and should only be used when trying to determine if the current user is in the Administrative group.
        /// </remarks>
        /// <returns>True if the user has administrator rights.</returns>
        public bool IsAdministrator()
        {
            try
            {
                _logger.LogInformation("Checking if current user is in Administrator group.");
                var groups = UserPrincipal.Current.GetAuthorizationGroups();
                return groups.Any(x => x.Name == "Administrators");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user role.");
                throw;
            }
        }
    }
}
