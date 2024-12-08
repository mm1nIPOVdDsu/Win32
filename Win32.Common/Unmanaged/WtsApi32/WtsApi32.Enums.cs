namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <inheritdoc/>
        public partial class WtsApi32
        {
            /// <summary>
            ///     Specifies the connection state of a Remote Desktop Services session.
            /// </summary>
            /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/wtsapi32/ne-wtsapi32-wts_connectstate_class">WTS_CONNECTSTATE_CLASS</seealso>
            public enum WTS_CONNECTSTATE_CLASS
            {
                /// <summary>
                ///     A user is logged on to the WinStation. This state occurs when a user is signed in and actively connected to the device.
                /// </summary>
                WTSActive,
                /// <summary>
                ///     The WinStation is connected to the client.
                /// </summary>
                WTSConnected,
                /// <summary>
                ///     The WinStation is in the process of connecting to the client.
                /// </summary>
                WTSConnectQuery,
                /// <summary>
                ///     The WinStation is shadowing another WinStation.
                /// </summary>
                WTSShadow,
                /// <summary>
                ///     The WinStation is active but the client is disconnected.
                /// </summary>
                WTSDisconnected,
                /// <summary>
                ///     The WinStation is waiting for a client to connect.
                /// </summary>
                WTSIdle,
                /// <summary>
                ///     The WinStation is listening for a connection.
                /// </summary>
                WTSListen,
                /// <summary>
                ///     The WinStation is being reset.
                /// </summary>
                WTSReset,
                /// <summary>
                ///     The WinStation is down due to an error.
                /// </summary>
                WTSDown,
                /// <summary>
                ///     The WinStation is initializing.
                /// </summary>
                WTSInit
            }
            /// <summary>
            ///     Contains values that indicate the type of session information to retrieve in a call to the <see href="https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/nf-wtsapi32-wtsquerysessioninformationa">WTSQuerySessionInformation</see> function.
            /// </summary>
            public enum WTS_INFO_CLASS
            {
                /// <summary>
                ///     A null-terminated string that contains the name of the initial program that Remote Desktop Services runs when the user logs on.
                /// </summary>
                WTSInitialProgram,
                /// <summary>
                ///     A null-terminated string that contains the published name of the application that the session is running.
                /// </summary>
                WTSApplicationName,
                /// <summary>
                ///     A null-terminated string that contains the default directory used when launching the initial program.
                /// </summary>
                WTSWorkingDirectory,
                /// <summary>
                ///     This value is not used.
                /// </summary>
                WTSOEMId,
                /// <summary>
                ///     A ULONG value that contains the session identifier.
                /// </summary>
                WTSSessionId,
                /// <summary>
                ///     A null-terminated string that contains the name of the user associated with the session.
                /// </summary>
                WTSUserName,
                /// <summary>
                ///     A null-terminated string that contains the name of the Remote Desktop Services session.
                /// </summary>
                /// <remarks>
                ///     Despite its name, specifying this type does not return the window station name. Rather, it returns the name of 
                ///     the Remote Desktop Services session. Each Remote Desktop Services session is associated with an interactive 
                ///     window station. Because the only supported window station name for an interactive window station is
                ///     "WinSta0", each session is associated with its own "WinSta0" window station. For more information, 
                ///     see <see href="https://docs.microsoft.com/en-us/windows/desktop/winstation/window-stations">Window Stations</see>.
                /// </remarks>
                WTSWinStationName,
                /// <summary>
                ///     A null-terminated string that contains the name of the domain to which the logged-on user belongs.
                /// </summary>
                WTSDomainName,
                /// <summary>
                ///     The session's current connection state. For more information, see <see href="https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/ne-wtsapi32-wts_connectstate_class">WTS_CONNECTSTATE_CLASS</see>.
                /// </summary>
                WTSConnectState,
                /// <summary>
                ///     A ULONG value that contains the build number of the client.
                /// </summary>
                WTSClientBuildNumber,
                /// <summary>
                ///     A null-terminated string that contains the name of the client.
                /// </summary>
                WTSClientName,
                /// <summary>
                ///     A null-terminated string that contains the directory in which the client is installed.
                /// </summary>
                WTSClientDirectory,
                /// <summary>
                ///     A USHORT client-specific product identifier.
                /// </summary>
                WTSClientProductId,
                /// <summary>
                ///     A ULONG value that contains a client-specific hardware identifier. This option is reserved for future use. 
                ///     <see href="https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/nf-wtsapi32-wtsquerysessioninformationa">WTSQuerySessionInformation</see> will always return a value of 0.
                /// </summary>
                WTSClientHardwareId,
                /// <summary>
                ///     The network type and network address of the client. For more information, see <see href="https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/ns-wtsapi32-wts_client_address">WTS_CLIENT_ADDRESS</see>.
                /// </summary>
                /// <remarks>
                ///     The IP address is offset by two bytes from the start of the Address member of the <see href="https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/ns-wtsapi32-wts_client_address">WTS_CLIENT_ADDRESS</see> structure.
                /// </remarks>
                WTSClientAddress,
                /// <summary>
                ///     Information about the display resolution of the client. For more information, see <see href="https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/ns-wtsapi32-wts_client_display">WTS_CLIENT_DISPLAY</see>.
                /// </summary>
                WTSClientDisplay,
                /// <summary>
                ///     A USHORT value that specifies information about the protocol type for the session. This is one of the following values.
                /// </summary>
                /// <remarks>
                ///     0 => The console session.
                ///     1 => This value is retained for legacy purposes.
                ///     2 => The RDP protocol.
                /// </remarks>
                WTSClientProtocolType,
                /// <summary>
                ///     This value returns FALSE. If you call GetLastError to get extended error information, GetLastError returns ERROR_NOT_SUPPORTED.
                /// </summary>
                WTSIdleTime,
                /// <summary>
                ///     This value returns FALSE. If you call GetLastError to get extended error information, GetLastError returns ERROR_NOT_SUPPORTED.
                /// </summary>
                WTSLogonTime,
                /// <summary>
                ///     This value returns FALSE. If you call GetLastError to get extended error information, GetLastError returns ERROR_NOT_SUPPORTED.
                /// </summary>
                WTSIncomingBytes,
                /// <summary>
                ///     This value returns FALSE. If you call GetLastError to get extended error information, GetLastError returns ERROR_NOT_SUPPORTED.
                /// </summary>
                WTSOutgoingBytes,
                /// <summary>
                ///     This value returns FALSE. If you call GetLastError to get extended error information, GetLastError returns ERROR_NOT_SUPPORTED.
                /// </summary>
                WTSIncomingFrames,
                /// <summary>
                ///     This value returns FALSE. If you call GetLastError to get extended error information, GetLastError returns ERROR_NOT_SUPPORTED.
                /// </summary>
                WTSOutgoingFrames,
                /// <summary>
                ///     Information about a Remote Desktop Connection (RDC) client. For more information, see <see href="https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/ns-wtsapi32-wtsclienta">WTSCLIENT</see>.
                /// </summary>
                WTSClientInfo,
                /// <summary>
                ///     Information about a client session on a RD Session Host server. For more information, see <see href="https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/ns-wtsapi32-wtsinfoa">WTSINFO</see>.
                /// </summary>
                WTSSessionInfo,
                /// <summary>
                ///     Extended information about a session on a RD Session Host server. For more information, see <see href="https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/ns-wtsapi32-wtsinfoexa">WTSINFOEX</see>.
                /// </summary>
                WTSSessionInfoEx,
                /// <summary>
                ///     A <see href="https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/ns-wtsapi32-wtsconfiginfoa">WTSCONFIGINFO</see> structure that contains information about the configuration of a RD Session Host server.
                /// </summary>
                WTSConfigInfo,
                /// <summary>
                ///     This value is not supported.
                /// </summary>
                WTSValidationInfo,
                /// <summary>
                ///     A <see href="https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/ns-wtsapi32-wts_session_address">WTS_SESSION_ADDRESS</see> structure that contains the IPv4 address assigned to the session.
                /// </summary>
                /// <remarks>
                ///     If the session does not have a virtual IP address, the WTSQuerySessionInformation function returns ERROR_NOT_SUPPORTED.
                /// </remarks>
                WTSSessionAddressV4,
                /// <summary>
                ///     Determines whether the current session is a remote session.
                /// </summary>
                /// <remarks>
                ///     The <see href="https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/nf-wtsapi32-wtsquerysessioninformationa">WTSQuerySessionInformation</see> function returns a value of TRUE to indicate that the current 
                ///     session is a remote session, and FALSE to indicate that the current session is a local session. This value can only be used for the local machine, so the hServer parameter of the WTSQuerySessionInformation 
                ///     function must contain WTS_CURRENT_SERVER_HANDLE.
                /// </remarks>
                WTSIsRemoteSession
            }
        }
    }
}
