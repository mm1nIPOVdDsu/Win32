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
            ///     An enclave is an isolated region of code and data within the address space for an application. Only code that runs within the
            ///     enclave can access data within the same enclave.
            /// </summary>
            public partial class EnclaveApi
            {
                /// <summary>
                ///     Contains architecture-specific information to use to initialize an enclave when the enclave type is ENCLAVE_TYPE_SGX or
                ///     ENCLAVE_TYPE_SGX2, which specifies an enclave for the Intel Software Guard Extensions (SGX) architecture extension.
                /// </summary>
                /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-enclave_init_info_sgx">ENCLAVE_INIT_INFO_SGX</see>
                [StructLayout(LayoutKind.Sequential)]
                public struct ENCLAVE_INIT_INFO_SGX
                {
                    /// <summary>
                    ///     The enclave signature structure ( <c>SIGSTRUCT</c>) to use to initialize the enclave. This structure specifies information
                    ///     about the enclave from the enclave signer.
                    /// </summary>
                    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1808)]
                    public byte[] SigStruct;
                    /// <summary>
                    ///     Not used.
                    /// </summary>
                    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 240)]
                    public byte[] Reserved1;
                    /// <summary>
                    ///     The EINIT token structure ( <c>EINITTOKEN</c>) to use to initialize the enclave. The initialization operation uses this
                    ///     structure to verify that the enclave has permission to start.
                    /// </summary>
                    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 304)]
                    public byte[] EInitToken;
                    /// <summary>
                    ///     Not used.
                    /// </summary>
                    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1744)]
                    public byte[] Reserved2;
                }
                /// <summary>
                ///     Contains architecture-specific information to use to create an enclave when the enclave type is ENCLAVE_TYPE_SGX or
                ///     ENCLAVE_TYPE_SGX2, which specifies an enclave for one of the Intel Software Guard Extensions (SGX) architecture extensions.
                /// </summary>
                /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-enclave_create_info_sgx">ENCLAVE_CREATE_INFO_SGX</see>
                public struct ENCLAVE_CREATE_INFO_SGX
                {
                    /// <summary>
                    ///     The SGX enclave control structure ( <c>SECS</c>) to use to create the enclave.
                    /// </summary>
                    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4096)]
                    public byte[] Secs;
                }
            }
        }
    }
}
