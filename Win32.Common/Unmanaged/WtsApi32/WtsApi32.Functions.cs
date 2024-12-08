using System;
using System.Runtime.InteropServices;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <inheritdoc/>
        public partial class WtsApi32
        {
            /// <summary>
            ///     Retrieves a list of sessions on a Remote Desktop Session Host (RD Session Host) server.
            /// </summary>
            /// <param name="hServer">A handle to the RD Session Host server.</param>
            /// <param name="Reserved">This parameter is reserved. It must be zero.</param>
            /// <param name="Version">The version of the enumeration request. This parameter must be 1.</param>
            /// <param name="ppSessionInfo">A pointer to an array of WTS_SESSION_INFO structures that represent the retrieved sessions.</param>
            /// <param name="pCount">A pointer to the number of WTS_SESSION_INFO structures returned in the ppSessionInfo parameter.</param>
            /// <returns>Returns zero if this function fails. If this function succeeds, a nonzero value is returned.</returns>
            /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wtsapi32/nf-wtsapi32-wtsquerysessioninformationa">WTSEnumerateSessions</seealso>
            [DllImport(WtsApi32Dll, SetLastError = true)]
            public static extern int WTSEnumerateSessions(
                IntPtr hServer,
                int Reserved,
                int Version,
                ref IntPtr ppSessionInfo,
                ref int pCount);
            /// <summary>
            ///     Frees memory allocated by a Remote Desktop Services function.
            /// </summary>
            /// <param name="pMemory">Pointer to the memory to free.</param>
            /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wtsapi32/nf-wtsapi32-wtsfreememory">WTSFreeMemory</seealso>
            [DllImport(WtsApi32Dll)]
            public static extern void WTSFreeMemory(IntPtr pMemory);
            /// <summary>
            ///     Retrieves session information for the specified session on the specified Remote Desktop Session Host (RD Session Host) server. It
            ///     can be used to query session information on local and remote RD Session Host servers.
            /// </summary>
            /// <param name="hServer">
            ///     A handle to an RD Session Host server. Specify a handle opened by the WTSOpenServer function, or specify WTS_CURRENT_SERVER_HANDLE
            ///     to indicate the RD Session Host server on which your application is running.
            /// </param>
            /// <param name="sessionId">
            ///     A Remote Desktop Services session identifier. To indicate the session in which the calling application is running (or the current
            ///     session) specify WTS_CURRENT_SESSION. Only specify WTS_CURRENT_SESSION when obtaining session information on the local server. If
            ///     WTS_CURRENT_SESSION is specified when querying session information on a remote server, the returned session information will be
            ///     inconsistent. Do not use the returned data.
            /// </param>
            /// <param name="wtsInfoClass">
            ///     A value of the <see cref="WTS_INFO_CLASS"/> enumeration that indicates the type of session information to retrieve in a call to
            ///     the WTSQuerySessionInformation function.
            /// </param>
            /// <param name="ppBuffer">
            ///     A pointer to a variable that receives a pointer to the requested information. The format and contents of the data depend on the
            ///     information class specified in the WTSInfoClass parameter. To free the returned buffer, call the WTSFreeMemory function.
            /// </param>
            /// <param name="pBytesReturned">A pointer to a variable that receives the size, in bytes, of the data returned in ppBuffer.</param>
            /// <returns>If the function succeeds, the return value is a nonzero value.</returns>
            /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wtsapi32/nf-wtsapi32-wtsquerysessioninformationa">WTSQuerySessionInformation</seealso>
            [DllImport(WtsApi32Dll)]
            public static extern bool WTSQuerySessionInformation(IntPtr hServer, uint sessionId, WTS_INFO_CLASS wtsInfoClass, out IntPtr ppBuffer, out uint pBytesReturned);
            /// <summary>
            ///     Obtains the primary access token of the logged-on user specified by the session ID. To call this function successfully, the
            ///     calling application must be running within the context of the LocalSystem account and have the SE_TCB_NAME privilege.
            /// </summary>
            /// <param name="SessionId">
            ///     A Remote Desktop Services session identifier. Any program running in the context of a service will have a session identifier of
            ///     zero (0).
            /// </param>
            /// <param name="phToken">If the function succeeds, receives a pointer to the token handle for the logged-on user.</param>
            /// <returns>
            ///     If the function succeeds, the return value is a nonzero value, and the phToken parameter points to the primary token of the user.
            /// </returns>
            /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/wtsapi32/nf-wtsapi32-wtsqueryusertoken">WTSQueryUserToken</seealso>
            [DllImport(WtsApi32Dll)]
            public static extern uint WTSQueryUserToken(uint SessionId, ref IntPtr phToken);
        }
    }
}
