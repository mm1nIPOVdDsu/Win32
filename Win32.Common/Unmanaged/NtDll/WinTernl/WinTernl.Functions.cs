using System;
using System.IO;
using System.Runtime.InteropServices;

//using static Win32.Common.Unmanaged.Shared;

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
                ///     Retrieves information about the specified process.
                /// </summary>
                /// <param name="processHandle">A handle to the process for which information is to be retrieved.</param>
                /// <param name="processInformationClass">The type of process information to be retrieved. </param>
                /// <param name="processInformation">A pointer to a buffer supplied by the calling application into which the function writes the requested information.</param>
                /// <param name="processInformationLength">The size of the buffer pointed to by the ProcessInformation parameter, in bytes.</param>
                /// <param name="returnLength">A pointer to a variable in which the function returns the size of the requested information. If the function was successful, this is the size of the information written to the buffer pointed to by the ProcessInformation parameter (if the buffer was too small, this is the minimum size of buffer needed to receive the information successfully).</param>
                /// <returns>The function returns an NTSTATUS success or error code.</returns>
                [DllImport(NtDllDll, SetLastError = true)]
                public static extern UInt32 NtQueryInformationProcess(IntPtr processHandle, UInt32 processInformationClass, ref PROCESS_BASIC_INFORMATION processInformation, int processInformationLength, ref UInt32 returnLength);

                //[DllImport(NtDllDll, ExactSpelling = true, SetLastError = false)]
                //private static extern NTStatus NtCreateFile(out IntPtr handle, uint desiredAccess, ref OBJECT_ATTRIBUTES objectAttributes, out IO_STATUS_BLOCK ioStatusBlock, ref long allocationSize, FileAttributes fileAttributes, ShareAccess shareAccess, CreateDisposition createDisposition, CreateOptions createOptions, IntPtr eaBuffer, uint eaLength);
            }
        }
    }
}