using System;
using System.Runtime.InteropServices;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <inheritdoc/>
        public partial class Kernel32
        {
            /// <summary>
            ///     ProcessThreadsApi interactions.
            /// </summary>
            public partial class ProcessThreadsApi
            {
                /// <summary>
                ///     Retrieves a pseudo handle for the current process.
                /// </summary>
                /// <returns>The return value is a pseudo handle to the current process.</returns>
                [DllImport(Kernel32Dll, SetLastError = true)]
                public static extern IntPtr GetCurrentProcess();
                /// <summary>
                ///     Retrieves the process identifier of the specified process.
                /// </summary>
                /// <param name="hProcess">A handle to the process. The handle must have the PROCESS_QUERY_INFORMATION or PROCESS_QUERY_LIMITED_INFORMATION access right.</param>
                /// <returns>If the function succeeds, the return value is the process identifier.</returns>
                [DllImport(Kernel32Dll, SetLastError = true)]
                public static extern int GetProcessId(IntPtr hProcess);
                /// <summary>
                ///     Opens an existing local process object.
                /// </summary>
                /// <param name="processAccess">The access to the process object. This access right is checked against the security descriptor for the process. This parameter can be one or more of the process access rights.</param>
                /// <param name="bInheritHandle">If this value is TRUE, processes created by this process will inherit the handle. Otherwise, the processes do not inherit this handle.</param>
                /// <param name="processId">The identifier of the local process to be opened.</param>
                /// <returns>If the function succeeds, the return value is an open handle to the specified process.</returns>
                [DllImport(Kernel32Dll, SetLastError = true)]
                public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, uint processId);
                /// <summary>
                ///     Retrieves the Remote Desktop Services session associated with a specified process.
                /// </summary>
                /// <param name="dwProcessId">Specifies a process identifier. Use the GetCurrentProcessId function to retrieve the process identifier for the current process.</param>
                /// <param name="pSessionId">Pointer to a variable that receives the identifier of the Remote Desktop Services session under which the specified process is running. To retrieve the identifier of the session currently attached to the console, use the WTSGetActiveConsoleSessionId function.</param>
                /// <returns>If the function succeeds, the return value is a nonzero value.</returns>
                [DllImport(Kernel32Dll, SetLastError = true)]
                public static extern bool ProcessIdToSessionId(uint dwProcessId, out uint pSessionId);
            }
        }
    }
}
