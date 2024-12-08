using System;
using System.Runtime.InteropServices;

using Microsoft.Win32.SafeHandles;

using static Win32.Common.Unmanaged.AdvApi32.WinBase;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <inheritdoc/>
        public partial class AdvApi32
        {
            /// <summary>
            ///     SecurityBaseApi interactions.
            /// </summary>
            public partial class SecurityBaseApi
            {
                /// <summary>
                ///     The DuplicateTokenEx function creates a new access token that duplicates an existing token. This function can create either a
                ///     primary token or an impersonation token.
                /// </summary>
                /// <param name="ExistingTokenHandle">A handle to an access token opened with TOKEN_DUPLICATE access.</param>
                /// <param name="dwDesiredAccess">Specifies the requested access rights for the new token.</param>
                /// <param name="lpTokenAttributes">
                ///     A pointer to a SECURITY_ATTRIBUTES structure that specifies a security descriptor for the new token and determines whether
                ///     child processes can inherit the token.
                /// </param>
                /// <param name="ImpersonationLevel">
                ///     Specifies a value from the SECURITY_IMPERSONATION_LEVEL enumeration that indicates the impersonation level of the new token.
                /// </param>
                /// <param name="TokenType">Specifies one of the following values from the TOKEN_TYPE enumeration.</param>
                /// <param name="phNewToken">A pointer to a HANDLE variable that receives the new token.</param>
                /// <returns>If the function succeeds, the function returns a nonzero value.</returns>
                /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/securitybaseapi/nf-securitybaseapi-duplicatetokenex">
                ///     DuplicateTokenEx
                /// </seealso>
                [DllImport(AdvApi32Dll, EntryPoint = "DuplicateTokenEx")]
                public static extern bool DuplicateTokenEx(
                    IntPtr ExistingTokenHandle,
                    uint dwDesiredAccess,
                    IntPtr lpTokenAttributes,
                    int ImpersonationLevel,
                    int TokenType,
                    ref IntPtr phNewToken);
                /// <summary>
                ///     The DuplicateTokenEx function creates a new access token that duplicates an existing token. This function can create either a
                ///     primary token or an impersonation token.
                /// </summary>
                /// <param name="ExistingTokenHandle">A handle to an access token opened with TOKEN_DUPLICATE access.</param>
                /// <param name="dwDesiredAccess">Specifies the requested access rights for the new token.</param>
                /// <param name="lpTokenAttributes">
                ///     A pointer to a SECURITY_ATTRIBUTES structure that specifies a security descriptor for the new token and determines whether
                ///     child processes can inherit the token.
                /// </param>
                /// <param name="ImpersonationLevel">
                ///     Specifies a value from the SECURITY_IMPERSONATION_LEVEL enumeration that indicates the impersonation level of the new token.
                /// </param>
                /// <param name="TokenType">Specifies one of the following values from the TOKEN_TYPE enumeration.</param>
                /// <param name="phNewToken">A pointer to a HANDLE variable that receives the new token.</param>
                /// <returns>If the function succeeds, the function returns a nonzero value.</returns>
                /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/securitybaseapi/nf-securitybaseapi-duplicatetokenex">
                ///     DuplicateTokenEx
                /// </seealso>
                [DllImport(AdvApi32Dll, EntryPoint = "DuplicateTokenEx")]
                public static extern bool DuplicateTokenEx(
                    IntPtr ExistingTokenHandle,
                    uint dwDesiredAccess,
                    ref SECURITY_ATTRIBUTES lpTokenAttributes,
                    int ImpersonationLevel,
                    int TokenType,
                    ref IntPtr phNewToken);
                /// <summary>
                ///     The DuplicateTokenEx function creates a new access token that duplicates an existing token. This function can create either a
                ///     primary token or an impersonation token.
                /// </summary>
                /// <param name="ExistingTokenHandle">A handle to an access token opened with TOKEN_DUPLICATE access.</param>
                /// <param name="dwDesiredAccess">Specifies the requested access rights for the new token.</param>
                /// <param name="lpTokenAttributes">
                ///     A pointer to a SECURITY_ATTRIBUTES structure that specifies a security descriptor for the new token and determines whether
                ///     child processes can inherit the token.
                /// </param>
                /// <param name="ImpersonationLevel">
                ///     Specifies a value from the SECURITY_IMPERSONATION_LEVEL enumeration that indicates the impersonation level of the new token.
                /// </param>
                /// <param name="TokenType">Specifies one of the following values from the TOKEN_TYPE enumeration.</param>
                /// <param name="phNewToken">A pointer to a HANDLE variable that receives the new token.</param>
                /// <returns>If the function succeeds, the function returns a nonzero value.</returns>
                /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/securitybaseapi/nf-securitybaseapi-duplicatetokenex">
                ///     DuplicateTokenEx
                /// </seealso>
                [DllImport(AdvApi32Dll, EntryPoint = "DuplicateTokenEx")]
                public static extern bool DuplicateTokenEx(
                    IntPtr ExistingTokenHandle,
                    uint dwDesiredAccess,
                    ref SECURITY_ATTRIBUTES lpTokenAttributes,
                    int ImpersonationLevel,
                    int TokenType,
                    ref SafeAccessTokenHandle phNewToken);
                ///// <summary>
                /////     The GetLengthSid function returns the length, in bytes, of a valid security identifier (SID).
                ///// </summary>
                ///// <param name="pSid">A pointer to the SID structure whose length is returned. The structure is assumed to be valid.</param>
                ///// <returns>
                /////     If the SID structure is valid, the return value is the length, in bytes, of the SID structure.
                /////     <para>
                /////         If the SID structure is not valid, the return value is undefined. Before calling GetLengthSid, pass the SID to the
                /////         IsValidSid function to verify that the SID is valid.
                /////     </para>
                ///// </returns>
                //[DllImport(AdvApi32Dll, ExactSpelling = true, SetLastError = true)]
                //public static extern int GetLengthSid(PSID pSid);
                ///// <summary>
                /////     The IsValidSid function validates a security identifier (SID) by verifying that the revision number is within a known range,
                /////     and that the number of sub-authorities is less than the maximum.
                ///// </summary>
                ///// <param name="pSid">A pointer to the SID structure to validate. This parameter cannot be NULL.</param>
                ///// <returns>
                /////     If the SID structure is valid, the return value is nonzero. If the SID structure is not valid, the return value is zero. There
                /////     is no extended error information for this function; do not call GetLastError.
                ///// </returns>
                //[DllImport(AdvApi32Dll, ExactSpelling = true)]
                //[return: MarshalAs(UnmanagedType.Bool)]
                //public static extern bool IsValidSid(PSID pSid);
            }
        }
    }
}