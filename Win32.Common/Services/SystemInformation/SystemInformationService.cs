using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

using Win32.Common.Services.Registry;

using Microsoft.Extensions.Logging;

using static Win32.Common.Unmanaged.Kernel32.MemoryApi;
using static Win32.Common.Unmanaged.User32;
using static Win32.Common.Unmanaged.User32.WinUser;

namespace Win32.Common.Services.SystemInformation
{
    /// <summary>
    ///     Service for getting machine system information.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class SystemInformationService : ISystemInformationService
    {
        private const string ACTIVE_HOURS_KEY_PATH = @"SOFTWARE\Microsoft\WindowsUpdate\UX\Settings\";
        private const string SMART_ACTIVE_HOURS_STATE_KEY = "SmartActiveHoursState";
        private const string SMART_ACTIVE_HOURS_START_KEY = "SmartActiveHoursStart";
        private const string SMART_ACTIVE_HOURS_END_KEY = "SmartActiveHoursEnd";
        private const string ACTIVE_HOURS_START_KEY = "ActiveHoursStart";
        private const string ACTIVE_HOURS_END_KEY = "ActiveHoursEnd";

        private readonly ILogger<SystemInformationService> _logger;
        private readonly IRegistryService _registryService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SystemInformationService"/> class.
        /// </summary>
        /// <param name="logger">An instance of a <see cref="ILogger{SystemInformationService}"/>.</param>
        /// <param name="registryService">An instance of a <see cref="IRegistryService"/>.</param>
        public SystemInformationService(ILogger<SystemInformationService> logger, IRegistryService registryService)
        {
            _registryService = registryService;
            _logger = logger;
        }

        /// <summary>
        ///     Gets the active hours start and end times.
        /// </summary>
        /// <returns><see cref="Tuple{T1, T2}"/>.</returns>
        public (int StartTime, int EndTime) GetActiveHours()
        {
            _logger.LogInformation("Getting Windows Active Hours.");

            // NOTE: If the key does not exist, the default type of T will be returned.
            _logger.LogDebug("Getting smart active hours state from the registry.");
            var activeHoursState = _registryService.GetKeyValue<int>(RegistryRoot.LocalMachine, ACTIVE_HOURS_KEY_PATH, SMART_ACTIVE_HOURS_STATE_KEY);

            _logger.LogDebug("Smart active hours feature is {state}.", activeHoursState);
            string? startHourKey;
            string? endHourKey;
            if (activeHoursState == 1)
            {
                _logger.LogDebug("Setting registry keys to smart active hours keys.");
                startHourKey = SMART_ACTIVE_HOURS_START_KEY;
                endHourKey = SMART_ACTIVE_HOURS_END_KEY;
            }
            else
            {
                _logger.LogDebug("Setting registry keys to manual active hours keys.");
                startHourKey = ACTIVE_HOURS_START_KEY;
                endHourKey = ACTIVE_HOURS_END_KEY;
            }

            _logger.LogDebug("Getting active hours start and end time.");
            var activeHoursBegin = _registryService.GetKeyValue<int>(RegistryRoot.LocalMachine, ACTIVE_HOURS_KEY_PATH, startHourKey);
            var activeHoursEnd = _registryService.GetKeyValue<int>(RegistryRoot.LocalMachine, ACTIVE_HOURS_KEY_PATH, endHourKey);

            _logger.LogDebug("Active hour starts at {ActiveHoursBegin} and ends at {ActiveHoursEnd}.", activeHoursBegin, activeHoursEnd);
            return (StartTime: activeHoursBegin, EndTime: activeHoursEnd);
        }
        /// <summary>
        ///     Gets the enabled state of Windows Active Hours feature.
        /// </summary>
        /// <returns>The <see cref="EnabledStatus"/> of Active Hours.</returns>
        public EnabledStatus GetActiveHoursStatus()
        {
            _logger.LogInformation("Getting status of Windows Active Hours.");
            try
            {
                var (StartTime, EndTime) = GetActiveHours();
                return EndTime == 0 && StartTime == 0 ? EnabledStatus.Disabled : EnabledStatus.Enabled;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active hours status.");
                return EnabledStatus.Unknown;
            }
        }
        /// <summary>
        ///     Gets the CPU ID of the device's processor.
        /// </summary>
        /// <returns>The string CPU ID information.</returns>
        public string GetCpuId()
        {
            var sn = new byte[8];
            _logger.LogDebug("Executing CPU ID macro.");
            return !ExecuteCode(ref sn)
                ? "ND"
                : string.Format("{0}{1}", BitConverter.ToUInt32(sn, 4).ToString("X8"), BitConverter.ToUInt32(sn, 0).ToString("X8"));
        }
        /// <summary>
        ///     Gets the <see cref="DateTime"/> of the last user input.
        /// </summary>
        /// <returns>The <see cref="DateTime"/> of the last input received by the user.</returns>
        public DateTime GetLastUserInput() 
        {
            var lastInputInfo = new WinUser.LASTINPUTINFO();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
            lastInputInfo.dwTime = 0;

            _logger.LogDebug("Getting last activity ticks.");
            return WinUser.GetLastInputInfo(ref lastInputInfo)
                ? DateTime.Now.AddMilliseconds((Environment.TickCount - lastInputInfo.dwTime) * -1)
                : DateTime.Now;
        }

        /// <summary>
        ///     Executes code to retrieve CPU ID from the processor.
        /// </summary>
        /// <param name="result">Raw CPU ID info.</param>
        /// <returns>True if successful.</returns>
        private bool ExecuteCode(ref byte[] result)
        {
            /* The opcodes below implement a C function with the signature:
             * __stdcall CpuIdWindowProc(hWnd, Msg, wParam, lParam);
             * with wParam interpreted as an 8 byte unsigned character buffer.
             * */
            var code_x86 = new byte[]
            {
                0x55,                      /* push ebp */
                0x89, 0xe5,                /* mov  ebp, esp */
                0x57,                      /* push edi */
                0x8b, 0x7d, 0x10,          /* mov  edi, [ebp+0x10] */
                0x6a, 0x01,                /* push 0x1 */
                0x58,                      /* pop  eax */
                0x53,                      /* push ebx */
                0x0f, 0xa2,                /* cpuid    */
                0x89, 0x07,                /* mov  [edi], eax */
                0x89, 0x57, 0x04,          /* mov  [edi+0x4], edx */
                0x5b,                      /* pop  ebx */
                0x5f,                      /* pop  edi */
                0x89, 0xec,                /* mov  esp, ebp */
                0x5d,                      /* pop  ebp */
                0xc2, 0x10, 0x00,          /* ret  0x10 */
            };
            var code_x64 = new byte[] 
            {
                0x53,                                     /* push rbx */
                0x48, 0xc7, 0xc0, 0x01, 0x00, 0x00, 0x00, /* mov rax, 0x1 */
                0x0f, 0xa2,                               /* cpuid */
                0x41, 0x89, 0x00,                         /* mov [r8], eax */
                0x41, 0x89, 0x50, 0x04,                   /* mov [r8+0x4], edx */
                0x5b,                                     /* pop rbx */
                0xc3,                                     /* ret */
            };

            var code = IsX64Process() ? code_x64 : code_x86;
            var ptr = new IntPtr(code.Length);

            _logger.LogDebug("Changing protection level of assembly block.");
            if (!VirtualProtect(code, ptr, MEM_PROTECTION.PAGE_EXECUTE_READWRITE, out var _))
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());

            ptr = new IntPtr(result.Length);

            _logger.LogDebug("Passing message to procedure.");
            return CallWindowProcW(code, IntPtr.Zero, 0, result, ptr) != IntPtr.Zero;
        }
        /// <summary>
        ///     Determines if the process is an x86 or x64 process.
        /// </summary>
        /// <returns>True if the process is x64</returns>
        private static bool IsX64Process() => IntPtr.Size == 8;
    }
}
