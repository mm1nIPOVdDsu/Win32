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
            ///     Contains information about a client session on a Remote Desktop Session Host (RD Session Host) server.
            /// </summary>
            /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/wtsapi32/ns-wtsapi32-wts_session_infoa">WTS_SESSION_INFO</see>
            [StructLayout(LayoutKind.Sequential)]
            public struct WTS_SESSION_INFO
            {
                /// <summary>
                ///     Session identifier of the session.
                /// </summary>
                public uint SessionID;
                /// <summary>
                ///     Pointer to a null-terminated string that contains the WinStation name of this session.
                /// </summary>
                [MarshalAs(UnmanagedType.LPStr)]
                public string pWinStationName;
                /// <summary>
                ///     A value from the WTS_CONNECTSTATE_CLASS enumeration type that indicates the session's current connection stat
                /// </summary>
                public WTS_CONNECTSTATE_CLASS State;
            }
        }
    }
}
