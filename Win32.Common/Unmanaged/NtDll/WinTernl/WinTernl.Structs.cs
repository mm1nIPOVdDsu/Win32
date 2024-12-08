using System;
using System.Runtime.InteropServices;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <inheritdoc/>
        public partial class NtDll
        {
            /// <summary>
            ///     WinTernl interactions.
            /// </summary>
            public partial class WinTernl
            {
                /// <summary>
                ///     Contains information about a newly created process and its primary thread.
                /// </summary>
                public struct PROCESS_BASIC_INFORMATION
                {
                    /// <summary>
                    ///     Contains the same value that GetExitCodeProcess returns. However the use of GetExitCodeProcess is preferable for clarity and safety.
                    /// </summary>
                    public IntPtr ExitStatus;
                    /// <summary>
                    ///     Points to a PEB structure.
                    /// </summary>
                    public IntPtr PebBaseAddress;
                    /// <summary>
                    ///     Can be cast to a DWORD and contains the same value that GetProcessAffinityMask returns for the lpProcessAffinityMask parameter.
                    /// </summary>
                    public IntPtr AffinityMask;
                    /// <summary>
                    ///     Contains the process priority as described in Scheduling Priorities.
                    /// </summary>
                    public IntPtr BasePriority;
                    /// <summary>
                    ///     Can be cast to a DWORD and contains a unique identifier for this process. We recommend using the GetProcessId function to retrieve this information.
                    /// </summary>
                    public UIntPtr UniqueProcessId;
                    /// <summary>
                    ///     Can be cast to a DWORD and contains a unique identifier for the parent process.
                    /// </summary>
                    public int InheritedFromUniqueProcessId;
                    /// <summary>
                    ///     The size of the PROCESS_BASIC_INFORMATION.
                    /// </summary>
                    public int Size => Marshal.SizeOf(typeof(PROCESS_BASIC_INFORMATION));
                }
            }
        }
    }
}