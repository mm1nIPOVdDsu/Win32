using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Principal;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Win32.SafeHandles;

using static Win32.Common.Unmanaged.AdvApi32.ProcessThreadsApi;
using static Win32.Common.Unmanaged.AdvApi32.SecurityBaseApi;
using static Win32.Common.Unmanaged.AdvApi32.WinBase;
using static Win32.Common.Unmanaged.Kernel32;
using static Win32.Common.Unmanaged.Kernel32.HandleApi;
using static Win32.Common.Unmanaged.Kernel32.ProcessThreadsApi;
using static Win32.Common.Unmanaged.WtsApi32;

namespace Win32.Common.Services.Identity
{
    /// <summary>
    ///     Service for managing user identity.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class IdentityService : IIdentityService
    {
        private const uint MAXIMUM_ALLOWED = 0x2000000;
        private const int TOKEN_DUPLICATE = 0x0002;

        private readonly object _impersonationLock = new();
        private readonly ILogger<IdentityService> _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="IdentityService"/> class.
        /// </summary>
        /// <param name="logger">An instance of a <see cref="ILogger{IdentityService}"/>.</param>
        public IdentityService(ILogger<IdentityService> logger) => _logger = logger;

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
        ///     Impersonates the active user.
        /// </summary>
        /// <returns>True if impersonation was successful.</returns>
        public bool Impersonate()
        {
            try
            {
                // get token for explorer process (a user process)
                var activeUserInfo = GetActiveSessionInfo();
                if (activeUserInfo is null)
                    return false;

                var hUserTokenDup = GetDuplicateTokenForProcess("explorer", activeUserInfo.SessionId);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        ///     Runs code in the context of the user.
        /// </summary>
        /// <param name="action">The action that will run in the user context.</param>
        public void RunAsUser(Action action)
        {
            lock (_impersonationLock)
            {
                var activeUserInfo = GetActiveSessionInfo();
                if (activeUserInfo is null)
                    return;

                _logger.LogDebug("Getting duplicate token.");
                var hUserTokenDup = GetDuplicateTokenForProcess("explorer", activeUserInfo.SessionId);
                if (hUserTokenDup is null)
                    return;

                _logger.LogDebug("Running action as the currently logged on user.");
                WindowsIdentity.RunImpersonated(hUserTokenDup, action);
            }
        }
        /// <summary>
        ///     Runs code in the context of the user.
        /// </summary>
        /// <param name="func">The asynchronous function that will run in the user context.</param>
        public void RunAsUserAsync(Func<Task> func)
        {
            lock (_impersonationLock)
            {
                var activeUserInfo = GetActiveSessionInfo();
                if (activeUserInfo is null)
                    return;

                _logger.LogDebug("Getting duplicate token.");
                var hUserTokenDup = GetDuplicateTokenForProcess("explorer", activeUserInfo.SessionId);

                if (hUserTokenDup is null)
                    return;

                _logger.LogDebug("Running function as the currently logged on user.");
                WindowsIdentity.RunImpersonatedAsync(hUserTokenDup, func).ContinueWith(result =>
                {
                    _logger.LogDebug("Function finished with result {status}.", result.Status);
                    if (result.Exception is not null && result.Status is TaskStatus.Faulted)
                        throw result.Exception;
                });
            }
        }
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        private WindowsIdentity GetWindowsIdentityFromHandle(Process process)
        {
            var processHandle = IntPtr.Zero;
            try
            {
                if (process is null)
                    throw new Exception("A user has not logged onto the system.");

                _logger.LogDebug("Opening process token.");
                // need to create an enum from the desired access argument
                OpenProcessToken(process.Handle, 8, ref processHandle);

                _logger.LogDebug("Returning identity from process handle.");
                return new WindowsIdentity(processHandle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not get a Windows Identity from {processName}.", process.ProcessName);
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
        ///     Duplicates a token for a user based upon the process
        /// </summary>
        /// <param name="processName">The process to get a duplicate token</param>
        /// <param name="sessionId"></param>
        /// <returns>A duplicate token</returns>
        private SafeAccessTokenHandle? GetDuplicateTokenForProcess(string processName, uint sessionId)
        {
            var _hPToken = IntPtr.Zero;
            var hProcess = IntPtr.Zero;
            var hUserTokenDup = IntPtr.Zero;

            try
            {
                // obtain the currently active session id; every logged on user in the system has a unique session id
                //var dwSessionId = WTSGetActiveConsoleSessionId();

                // obtain the process id of the windows log-on process that is running within the currently active session
                _logger.LogDebug("Getting the process named {processName}.", processName);
                var processes = Process.GetProcessesByName(processName);
                var processForSession = processes.FirstOrDefault(p => (uint)p.SessionId == sessionId);

                if (processForSession == null)
                {
                    _logger.LogWarning("Could not find any processes named {processName}.", processName);
                    return null;
                }

                _logger.LogDebug("Opening the process {processName}.", processName);
                hProcess = OpenProcess(MAXIMUM_ALLOWED, false, (uint)processForSession.Id);

                _logger.LogDebug("Opening process token for {processName}.", processName);
                if (!OpenProcessToken(hProcess, TOKEN_DUPLICATE, ref _hPToken))
                {
                    _logger.LogWarning("Could not open token for the process {processName}.", processName);
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                // Security attribute structure used in DuplicateTokenEx and CreateProcessAsUser
                // I would prefer to not have to use a security attribute variable and to just
                // simply pass null and inherit (by default) the security attributes
                // of the existing token. However, in C# structures are value types and therefore
                // cannot be assigned the null value.
                var sa = new SECURITY_ATTRIBUTES();
                sa.Length = Marshal.SizeOf(sa);

                _logger.LogDebug("Attempting to duplicate the token for the process {processName}.", processName);
                if (!DuplicateTokenEx(_hPToken, MAXIMUM_ALLOWED, ref sa, (int)SECURITY_IMPERSONATION_LEVEL.SecurityIdentification, (int)TOKEN_TYPE.TokenPrimary, ref hUserTokenDup))
                {
                    _logger.LogWarning("Could not duplicate the token for the process {processName}.", processName);
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                _logger.LogDebug("Returning a duplicate token for the process {processName}.", processName);
                return new SafeAccessTokenHandle(hUserTokenDup);

                //return hUserTokenDup;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating duplicate token for {processName}", processName);
                return null;
            }
            finally
            {
                if (_hPToken != IntPtr.Zero)
                    CloseHandle(_hPToken);
                if (hProcess != IntPtr.Zero)
                    CloseHandle(hProcess);
                if (hUserTokenDup != IntPtr.Zero)
                    CloseHandle(hUserTokenDup);
            }
        }
        /// <summary>
        ///     Gets the active user session.
        /// </summary>
        /// <returns><see cref="UserSessionInfo"/></returns>
        private UserSessionInfo? GetActiveSessionInfo()
        {
            var pServer = IntPtr.Zero;
            var pSessionInfo = IntPtr.Zero;
            var sessionCount = 0;

            try
            {
                var returnValue = WTSEnumerateSessions(pServer, 0, 1, ref pSessionInfo, ref sessionCount);
                var dataSize = Marshal.SizeOf(typeof(WTS_SESSION_INFO));
                var currentSession = pSessionInfo;
                if (returnValue == 0)
                    return null;

                //Go to all sessions
                for (var i = 0; i < sessionCount; i++)
                {
                    var pAddress = IntPtr.Zero;

                    try
                    {
                        var ptr = Marshal.PtrToStructure(currentSession, typeof(WTS_SESSION_INFO));
                        if (ptr is null)
                            return null;

                        var sessionInfo = (WTS_SESSION_INFO)ptr;
                        if (!sessionInfo.State.ToString().Contains("Active"))
                            continue;

                        //Get the User Name of the Terminal Services User
                        if (!WTSQuerySessionInformation(pServer, sessionInfo.SessionID, WTS_INFO_CLASS.WTSUserName, out pAddress, out var iReturned))
                            continue;

                        return new UserSessionInfo()
                        {
                            SessionId = sessionInfo.SessionID,
                            UserName = Marshal.PtrToStringAnsi(pAddress)
                        };
                    }
                    finally
                    {
                        currentSession += dataSize;
                        WTSFreeMemory(pAddress);
                        WTSFreeMemory(pServer);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active session information.");
            }
            finally
            {
                WTSFreeMemory(pSessionInfo);
                WTSFreeMemory(pServer);
            }

            return null;
        }
    }
}