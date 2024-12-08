using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

using static Win32.Common.Unmanaged.Kernel32.MemoryApi;
using static Win32.Common.Unmanaged.Shared;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <inheritdoc/>
        public partial class Kernel32
        {
            /// <summary>
            ///     An enclave is an isolated region of code and data within the address space for an application. Only code that runs within the
            ///     enclave can access data within the same enclave.
            /// </summary>
            public partial class EnclaveApi
            {
                /// <summary>
                ///     Used by the <see cref="CallEnclave"/> function.
                /// </summary>
                /// <param name="lpThreadParameter">The thread parameter.</param>
                /// <returns>The return thread parameter.</returns>
                [UnmanagedFunctionPointer(CallingConvention.StdCall)]
                public delegate IntPtr PENCLAVE_ROUTINE(IntPtr lpThreadParameter);

                /// <summary>
                ///     Calls a function within an enclave. CallEnclave can also be called within an enclave to call a function outside of the enclave.
                /// </summary>
                /// <param name="lpRoutine">The address of the function that you want to call.</param>
                /// <param name="lpParameter">The parameter than you want to pass to the function.</param>
                /// <param name="fWaitForThread">
                ///     TRUE if the call to the specified function should block execution until an idle enclave thread becomes available when no idle
                ///     enclave thread is available. FALSE if the call to the specified function should fail when no idle enclave thread is available.
                /// </param>
                /// <param name="lpReturnValue">The return value of the function, if it is called successfully.</param>
                /// <returns>
                ///     TRUE if the specified function was called successfully; otherwise FALSE. To get extended error information, call GetLastError.
                /// </returns>
                /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/enclaveapi/nf-enclaveapi-callenclave">CallEnclave</see>
                [DllImport(VertDll, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool CallEnclave(PENCLAVE_ROUTINE lpRoutine, IntPtr lpParameter, [MarshalAs(UnmanagedType.Bool)] bool fWaitForThread, out IntPtr lpReturnValue);
                /// <summary>
                ///     Creates a new uninitialized enclave. An enclave is an isolated region of code and data within the address space for an
                ///     application. Only code that runs within the enclave can access data within the same enclave.
                /// </summary>
                /// <param name="hProcess">A handle to the process for which you want to create an enclave.</param>
                /// <param name="lpAddress">
                ///     The preferred base address of the enclave. Specify NULL to have the operating system assign the base address.
                /// </param>
                /// <param name="dwSize">
                ///     The size of the enclave that you want to create, including the size of the code that you will load into the enclave, in bytes.
                ///     VBS enclaves must be a multiple of 2 MB in size. SGX enclaves must be a power of 2 in size and must have their base aligned to
                ///     the same power of 2 as the size, with a minimum alignment of 2 MB.As an example, if the enclave is 128 MB, then its base must
                ///     be aligned to a 128 MB boundary.
                /// </param>
                /// <param name="dwInitialCommittment">The amount of memory to commit for the enclave, in bytes.</param>
                /// <param name="flEnclaveType">The architecture type of the enclave that you want to create.</param>
                /// <param name="lpEnclaveInformation">A pointer to the architecture-specific information to use to create the enclave.</param>
                /// <param name="dwInfoLength">
                ///     The length of the structure that the lpEnclaveInformation parameter points to, in bytes. For the ENCLAVE_TYPE_SGX and
                ///     ENCLAVE_TYPE_SGX2 enclave types, this value must be 4096. For the ENCLAVE_TYPE_VBS enclave type, this value must be
                ///     sizeof(ENCLAVE_CREATE_INFO_VBS), which is 36 bytes.
                /// </param>
                /// <param name="lpEnclaveError">
                ///     An optional pointer to a variable that receives an enclave error code that is architecture-specific. For the ENCLAVE_TYPE_SGX,
                ///     ENCLAVE_TYPE_SGX2 and ENCLAVE_TYPE_VBS enclave types, the lpEnclaveError parameter is not used.
                /// </param>
                /// <returns>If the function succeeds, the return value is the base address of the created enclave.</returns>
                /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/enclaveapi/nf-enclaveapi-createenclave">CreateEnclave</see>
                [DllImport(Kernel32Dll, SetLastError = true, ExactSpelling = true)]
                public static extern IntPtr CreateEnclave(IntPtr hProcess, IntPtr lpAddress, SizeT dwSize, SizeT dwInitialCommittment, ENCLAVE_TYPE flEnclaveType, in ENCLAVE_CREATE_INFO_SGX lpEnclaveInformation, uint dwInfoLength, out uint lpEnclaveError);
                /// <summary>
                ///     Deletes the specified enclave.
                /// </summary>
                /// <param name="lpAddress">The base address of the enclave that you want to delete.</param>
                /// <returns>TRUE if the enclave was deleted successfully; otherwise FALSE. To get extended error information, call GetLastError.</returns>
                /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/enclaveapi/nf-enclaveapi-deleteenclave">DeleteEnclave</see>
                [DllImport(Kernel32Dll, SetLastError = true, ExactSpelling = true)] // NOTE: Microsoft documentation shows this as using Kernel32.dll, but have seen the usage of Vertdll.dll as well.
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool DeleteEnclave(IntPtr lpAddress);
                /// <summary>
                ///     Initializes an enclave that you created and loaded with data.
                /// </summary>
                /// <param name="hProcess">A handle to the process for which the enclave was created.</param>
                /// <param name="lpAddress">Any address within the enclave.</param>
                /// <param name="lpEnclaveInformation">A pointer to architecture-specific information to use to initialize the enclave.</param>
                /// <param name="dwInfoLength">
                ///     The length of the structure that the lpEnclaveInformation parameter points to, in bytes. For the ENCLAVE_TYPE_SGX and
                ///     ENCLAVE_TYPE_SGX2 enclave types, this value must be 4096. For the ENCLAVE_TYPE_VBS enclave type, this value must be
                ///     sizeof(ENCLAVE_INIT_INFO_VBS), which is 8 bytes.
                /// </param>
                /// <param name="lpEnclaveError">An optional pointer to a variable that receives an enclave error code that is architecture-specific.</param>
                /// <returns>
                ///     If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. To get extended error
                ///     information, call GetLastError.
                /// </returns>
                /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/enclaveapi/nf-enclaveapi-initializeenclave">InitializeEnclave</see>
                [DllImport(Kernel32Dll, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool InitializeEnclave(IntPtr hProcess, IntPtr lpAddress, in ENCLAVE_INIT_INFO_SGX lpEnclaveInformation, uint dwInfoLength, ref uint lpEnclaveError);
                /// <summary>
                ///     Retrieves whether the specified type of enclave is supported.
                /// </summary>
                /// <param name="flEnclaveType">The type of enclave to check.</param>
                /// <returns>
                ///     If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. To get extended error
                ///     information, call GetLastError.
                /// </returns>
                /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/enclaveapi/nf-enclaveapi-isenclavetypesupported">IsEnclaveTypeSupported</see>
                [DllImport(Kernel32Dll, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool IsEnclaveTypeSupported(ENCLAVE_TYPE flEnclaveType);
                /// <summary>
                ///     Loads data into an uninitialized enclave that you created by calling CreateEnclave.
                /// </summary>
                /// <param name="hProcess">A handle to the process for which the enclave was created.</param>
                /// <param name="lpAddress">The address in the enclave where you want to load the data.</param>
                /// <param name="lpBuffer">A pointer to the data the you want to load into the enclave.</param>
                /// <param name="nSize">The size of the data that you want to load into the enclave, in bytes. This value must be a whole-number multiple of the page size.</param>
                /// <param name="flProtect">The memory protection to use for the pages that you want to add to the enclave.</param>
                /// <param name="lpPageInformation">A pointer to information that describes the pages that you want to add to the enclave. The lpPageInformation parameter is not used.</param>
                /// <param name="dwInfoLength">The length of the structure that the lpPageInformation parameter points to, in bytes. This value must be 0.</param>
                /// <param name="lpNumberOfBytesWritten">A pointer to a variable that receives the number of bytes that LoadEnclaveData copied into the enclave.</param>
                /// <param name="lpEnclaveError">An optional pointer to a variable that receives an enclave error code that is architecture-specific. The lpEnclaveError parameter is not used.</param>
                /// <returns>If all of the data is loaded into the enclave successfully, the return value is nonzero. Otherwise, the return value is zero. To get extended error information, call GetLastError.</returns>
                /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/enclaveapi/nf-enclaveapi-loadenclavedata">LoadEnclaveData</see>
                [DllImport(Kernel32Dll, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool LoadEnclaveData(IntPtr hProcess, IntPtr lpAddress, IntPtr lpBuffer, SizeT nSize, MEM_PROTECTION flProtect, IntPtr lpPageInformation, uint dwInfoLength, out SizeT lpNumberOfBytesWritten, out uint lpEnclaveError);
                /// <summary>
                ///     Ends the execution of the threads that are running within an enclave.
                /// </summary>
                /// <param name="lpAddress">The base address of the enclave in which to end the execution of the threads.</param>
                /// <param name="fWait">
                ///     TRUE if TerminateEnclave should not return until all of the threads in the enclave end execution. FALSE if TerminateEnclave
                ///     should return immediately.
                /// </param>
                /// <returns>TRUE if the function succeeds; otherwise FALSE. To get extended error information, call GetLastError.</returns>
                /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/enclaveapi/nf-enclaveapi-terminateenclave">TerminateEnclave</see>
                [DllImport(Kernel32Dll, SetLastError = true, ExactSpelling = true)] // NOTE: Microsoft documentation shows this as using Kernel32.dll, but have seen the usage of Vertdll.dll as well.
                [return: MarshalAs(UnmanagedType.Bool)] 
                public static extern bool TerminateEnclave(IntPtr lpAddress, [MarshalAs(UnmanagedType.Bool)] bool fWait);
            }
        }
    }
}
