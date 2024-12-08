using System;
using System.Runtime.InteropServices;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <summary>
        ///     AdvApi32 interactions.
        /// </summary>
        public partial class Shared
        {
            /// <summary>
            ///     WinCrypt interactions.
            /// </summary>
            public partial class WinCrypt
            {
                /// <summary>
                ///     The CryptAcquireContext function is used to acquire a handle to a particular key container within a particular cryptographic
                ///     service provider (CSP). This returned handle is used in calls to CryptoAPI functions that use the selected CSP.
                /// </summary>
                /// <param name="providerHandle">A pointer to a handle of a CSP.</param>
                /// <param name="container">
                ///     The key container name. This is a null-terminated string that identifies the key container to the CSP. This name is
                ///     independent of the method used to store the keys. Some CSPs store their key containers internally (in hardware), some use the
                ///     system registry, and others use the file system. In most cases, when dwFlags is set to CRYPT_VERIFYCONTEXT, pszContainer must
                ///     be set to NULL. However, for hardware-based CSPs, such as a smart card CSP, can be access publicly available information in
                ///     the specified container.
                /// </param>
                /// <param name="provider">A null-terminated string that contains the name of the CSP to be used.</param>
                /// <param name="providerType">Specifies the type of provider to acquire.</param>
                /// <param name="flags">
                ///     Flag values. This parameter is usually set to zero, but some applications set one or more of the following flags.
                /// </param>
                /// <returns>If the function succeeds, the function returns nonzero (TRUE).</returns>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-cryptacquirecontexta">CryptAcquireContext</seealso>
                [Obsolete("This API is deprecated. New and existing software should start using Cryptography Next Generation APIs. Microsoft may remove this API in future releases.")]
                [DllImport(AdvApi32Dll, CharSet = CharSet.Auto, SetLastError = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool CryptAcquireContext(ref IntPtr providerHandle, string container, string provider, uint providerType, uint flags);
                /// <summary>
                ///     The CryptDestroyHash function destroys the hash object referenced by the hHash parameter. After a hash object has been
                ///     destroyed, it can no longer be used.
                /// </summary>
                /// <param name="hashHandle">The handle of the hash object to be destroyed.</param>
                /// <returns>f the function succeeds, the return value is nonzero.</returns>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-cryptdestroyhash">CryptDestroyHash</seealso>
                [Obsolete("This API is deprecated. New and existing software should start using Cryptography Next Generation APIs. Microsoft may remove this API in future releases.")]
                [DllImport(AdvApi32Dll, SetLastError = true)]
                public static extern bool CryptDestroyHash(IntPtr hashHandle);

                /// <summary>
                ///     The CryptHashData function adds data to a specified hash object. This function and CryptHashSessionKey can be called multiple
                ///     times to compute the hash of long or discontinuous data streams.
                /// </summary>
                /// <param name="hashHandle">Handle of the hash object.</param>
                /// <param name="inputData">A pointer to a buffer that contains the data to be added to the hash object.</param>
                /// <param name="dataLen">Number of bytes of data to be added. This must be zero if the CRYPT_USERDATA flag is set.</param>
                /// <param name="flags">User defined flags.</param>
                /// <returns>If the function succeeds, the return value is TRUE.</returns>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-crypthashdata">CryptHashData</seealso>
                [Obsolete("This API is deprecated. New and existing software should start using Cryptography Next Generation APIs. Microsoft may remove this API in future releases.")]
                [DllImport(AdvApi32Dll, SetLastError = true)]
                public static extern bool CryptHashData(IntPtr hashHandle, byte[] inputData, uint dataLen, uint flags);
                /// <summary>
                ///     The CryptGetHashParam function retrieves data that governs the operations of a hash object. The actual hash value can be
                ///     retrieved by using this function.
                /// </summary>
                /// <param name="hashHandle">Handle of the hash object to be queried.</param>
                /// <param name="algId">Query type.</param>
                /// <param name="outputData">A pointer to a buffer that receives the specified value data.</param>
                /// <param name="dataLen">
                ///     A pointer to a DWORD value specifying the size, in bytes, of the pbData buffer. When the function returns, the DWORD value
                ///     contains the number of bytes stored in the buffer.
                /// </param>
                /// <param name="flags">Reserved for future use and must be zero.</param>
                /// <returns>If the function succeeds, the return value is TRUE.</returns>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-cryptgethashparam">CryptGetHashParam</seealso>
                [Obsolete("This API is deprecated. New and existing software should start using Cryptography Next Generation APIs. Microsoft may remove this API in future releases.")]
                [DllImport("advapi32.dll", SetLastError = true)]
                public static extern bool CryptGetHashParam(IntPtr hashHandle, uint algId, byte[] outputData, ref uint dataLen, uint flags);
            }
        }
    }
}
