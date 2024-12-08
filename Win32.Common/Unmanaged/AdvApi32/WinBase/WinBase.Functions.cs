using System.Runtime.InteropServices;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <inheritdoc/>
        public partial class AdvApi32
        {
            /// <summary>
            ///     WinBase interactions.
            /// </summary>
            public partial class WinBase
            {

                /// <summary>
                ///     The name of the file or directory to be decrypted.
                /// </summary>
                /// <param name="lpFileName">The name of the file or directory to be decrypted.</param>
                /// <returns>True if successful.</returns>
                [DllImport(AdvApi32Dll, SetLastError = true, CharSet = CharSet.Unicode)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool DecryptFile(string lpFileName);
                /// <summary>
                ///     Encrypts a file or directory. All data streams in a file are encrypted. All new files created in an encrypted directory are encrypted.
                /// </summary>
                /// <param name="lpFileName">The name of the file or directory to be encrypted.</param>
                /// <returns>True if successful.</returns>
                [DllImport(AdvApi32Dll, SetLastError = true, CharSet = CharSet.Unicode)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool EncryptFile(string lpFileName);
                /// <summary>
                ///     Retrieves information about the current hardware profile for the local computer.
                /// </summary>
                /// <param name="lpHwProfileInfo">
                ///     A pointer to an <see cref="HW_PROFILE_INFO"/> structure that receives information about the current hardware profile.
                /// </param>
                /// <example>
                ///     IntPtr lHWInfoPtr = Marshal.AllocHGlobal(123); HW_PROFILE_INFO lProfile = new HW_PROFILE_INFO();
                ///     Marshal.StructureToPtr(lProfile, lHWInfoPtr,false); if (GetCurrentHwProfile(lHWInfoPtr)) { Marshal.PtrToStructure(lHWInfoPtr,
                ///     lProfile); string lText = lProfile.szHwProfileGuid.ToString(); } Marshal.FreeHGlobal(lHWInfoPtr);
                /// </example>
                /// <returns>True if successful.</returns>
                [DllImport(AdvApi32Dll, SetLastError = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool GetCurrentHwProfile(out HW_PROFILE_INFO lpHwProfileInfo);
                ///// <summary>
                /////     The LogonUserExExW function attempts to log a user on to the local computer. The local computer is the computer from which
                /////     LogonUserExExW was called.
                ///// </summary>
                ///// <remarks>
                /////     This function is not declared in a public header and has no associated import library. You must use the LoadLibrary and 
                /////     GetProcAddress functions to dynamically link to Advapi32.dll.
                ///// </remarks>
                ///// <param name="lpszUsername">
                /////     A pointer to a null-terminated string that specifies the name of the user. This is the name of the user account to log on to.
                /////     If you use the user principal name (UPN) format, the lpszDomain parameter must be NULL.
                ///// </param>
                ///// <param name="lpszDomain">
                /////     [Optional] A pointer to a null-terminated string that specifies the name of the domain or server whose account database
                /////     contains the lpszUsername account. If this parameter is NULL, the user name must be specified in UPN format. If this parameter
                /////     is ".", the function validates the account by using only the local account database.
                ///// </param>
                ///// <param name="lpszPassword">
                /////     [Optional] A pointer to a null-terminated string that specifies the plaintext password for the user account specified by
                /////     lpszUsername. When you have finished using the password, clear the password from memory by calling the SecureZeroMemory function.
                ///// </param>
                ///// <param name="dwLogonType">The <see cref="LOGON32_LOGON"/> operation to perform.</param>
                ///// <param name="dwLogonProvider">The <see cref="LOGON32_PROVIDER"/>.</param>
                ///// <param name="phTokenGroup">
                /////     [Optional] A pointer to a TOKEN_GROUPS structure that specifies a list of group SIDs that are added to the token that this
                /////     function receives upon successful logon. Any SIDs added to the token also effect group expansion. For example, if the added
                /////     SIDs are members of local groups, those groups are also added to the received access token. If this parameter is not NULL, the
                /////     caller of this function must have the SE_TCB_PRIVILEGE privilege granted and enabled.
                ///// </param>
                ///// <param name="phToken">A pointer to a handle variable that receives a handle to a token that represents the specified user.</param>
                ///// <param name="ppLogonSid">A pointer to a pointer to a SID that receives the SID of the user logged on.</param>
                ///// <param name="ppProfileBuffer">
                /////     A pointer to a pointer that receives the address of a buffer that contains the logged on user's profile.
                ///// </param>
                ///// <param name="pdwProfileLength">A pointer to a DWORD that receives the length of the profile buffer.</param>
                ///// <param name="pQuotaLimits">
                /////     A pointer to a QUOTA_LIMITS structure that receives information about the quotas for the logged on user.
                ///// </param>
                ///// <returns>If the function succeeds, the function returns nonzero.</returns>
                ///// <see href="https://learn.microsoft.com/en-us/windows/win32/secauthn/logonuserexexw">LogonUserExExW</see>
                //[DllImport(AdvApi32Dll, SetLastError = true)]
                //public static extern bool LogonUserExExW(
                //      string lpszUsername,
                //      [Optional] string lpszDomain,
                //      [Optional] string lpszPassword,
                //      LOGON32_LOGON dwLogonType,
                //      LOGON32_PROVIDER dwLogonProvider,
                //      IntPtr phTokenGroup,
                //      out IntPtr phToken,
                //      IntPtr ppLogonSid,
                //      IntPtr ppProfileBuffer,
                //      IntPtr pdwProfileLength,
                //      IntPtr pQuotaLimits);
                //[DllImport(AdvApi32Dll, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
                //[return: MarshalAs(UnmanagedType.Bool)]
                //public static extern bool LogonUserExExW(string lpszUserName, [Optional] string lpszDomain, [Optional] string lpszPassword, LogonUserType dwLogonType, LogonUserProvider dwLogonProvider,
                //    [In, Optional] in TOKEN_GROUPS pTokenGroups, out SafeHTOKEN phToken, out SafePSID ppLogonSid, out SafeLsaReturnBufferHandle ppProfileBuffer, out uint pdwProfileLength, out QUOTA_LIMITS pQuotaLimits);
            }
        }
    }
}
