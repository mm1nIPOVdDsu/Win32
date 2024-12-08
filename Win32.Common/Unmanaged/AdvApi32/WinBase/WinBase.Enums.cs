using System;

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
                ///     The reported docking state of the computer.
                /// </summary>
                public enum DOCKINGINFO : byte
                {
                    /// <summary>
                    ///     The computer is docked.
                    /// </summary>
                    DOCKINFO_DOCKED = 0x2,
                    /// <summary>
                    ///     The computer is undocked. This flag is always set for desktop systems that cannot be undocked.
                    /// </summary>
                    DOCKINFO_UNDOCKED = 0x1,
                    /// <summary>
                    ///     If this flag is set, GetCurrentHwProfile retrieved the current docking state from information provided by the user in the
                    ///     Hardware Profiles page of the System control panel application.
                    /// </summary>
                    DOCKINFO_USER_SUPPLIED = 0x4,
                    /// <summary>
                    ///     The computer is docked, according to information provided by the user. This value is a combination of the
                    ///     DOCKINFO_USER_SUPPLIED and DOCKINFO_DOCKED flags.
                    /// </summary>
                    DOCKINFO_USER_DOCKED = 0x5,
                    /// <summary>
                    ///     The computer is undocked, according to information provided by the user. This value is a combination of the
                    ///     DOCKINFO_USER_SUPPLIED and DOCKINFO_UNDOCKED flags.
                    /// </summary>
                    DOCKINFO_USER_UNDOCKED = 0x6
                }
                /// <summary>
                ///     A bitfield that determines whether certain STARTUPINFO members are used when the process creates a window.
                /// </summary>
                /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/processthreadsapi/ns-processthreadsapi-startupinfoa">
                ///     STARTF
                /// </seealso>
                [Flags]
                public enum STARTF : uint
                {
                    /// <summary>
                    ///     Indicates that the cursor is in feedback mode for two seconds after CreateProcess is called.
                    /// </summary>
                    STARTF_FORCEONFEEDBACK = 0x00000040,
                    /// <summary>
                    ///     Indicates that the feedback cursor is forced off while the process is starting.
                    /// </summary>
                    STARTF_FORCEOFFFEEDBACK = 0x00000080,
                    /// <summary>
                    ///     Indicates that any windows created by the process cannot be pinned on the taskbar.
                    /// </summary>
                    STARTF_PREVENTPINNING = 0x00002000,
                    /// <summary>
                    ///     Indicates that the process should be run in full-screen mode, rather than in windowed mode.
                    /// </summary>
                    STARTF_RUNFULLSCREEN = 0x00000020,
                    /// <summary>
                    ///     The lpTitle member contains an AppUserModelID
                    /// </summary>
                    STARTF_TITLEISAPPID = 0x00001000,
                    /// <summary>
                    ///     The lpTitle member contains the path of the shortcut file (.lnk) that the user invoked to start this process
                    /// </summary>
                    STARTF_TITLEISLINKNAME = 0x00000800,
                    /// <summary>
                    ///     The command line came from an untrusted source.
                    /// </summary>
                    STARTF_UNTRUSTEDSOURCE = 0x00008000,
                    /// <summary>
                    ///     The dwXCountChars and dwYCountChars members contain additional information.
                    /// </summary>
                    STARTF_USECOUNTCHARS = 0x00000008,
                    /// <summary>
                    ///     The dwFillAttribute member contains additional information.
                    /// </summary>
                    STARTF_USEFILLATTRIBUTE = 0x00000010,
                    /// <summary>
                    ///     The hStdInput member contains additional information.
                    ///
                    ///     This flag cannot be used with STARTF_USESTDHANDLES.
                    /// </summary>
                    STARTF_USEHOTKEY = 0x00000200,
                    /// <summary>
                    ///     The dwX and dwY members contain additional information.
                    /// </summary>
                    STARTF_USEPOSITION = 0x00000004,
                    /// <summary>
                    ///     The wShowWindow member contains additional information.
                    /// </summary>
                    STARTF_USESHOWWINDOW = 0x00000001,
                    /// <summary>
                    ///     The dwXSize and dwYSize members contain additional information.
                    /// </summary>
                    STARTF_USESIZE = 0x00000002,
                    /// <summary>
                    ///     The hStdInput, hStdOutput, and hStdError members contain additional information.
                    /// </summary>
                    STARTF_USESTDHANDLES = 0x00000100
                }
                /// <summary>
                ///     The type of logon operation to perform.
                /// </summary>
                /// <see href="https://learn.microsoft.com/en-us/windows/win32/secauthn/logonuserexexw#parameters">LOGON32_LOGON</see>
                public enum LOGON32_LOGON : uint
                {
                    /// <summary>
                    ///     This logon type is intended for users who will be interactively using the computer, such as a user being logged on by a
                    ///     terminal server, remote shell, or similar process. This logon type has the additional expense of caching logon information
                    ///     for disconnected operations; therefore, it is inappropriate for some client/server applications, such as a mail server.
                    /// </summary>
                    LOGON32_LOGON_INTERACTIVE = 2,
                    /// <summary>
                    ///     This logon type is intended for high performance servers to authenticate plaintext passwords. The LogonUserExExW function
                    ///     does not cache credentials for this logon type.
                    /// </summary>
                    LOGON32_LOGON_NETWORK = 3,
                    /// <summary>
                    ///     This logon type is intended for batch servers, where processes may be executing on behalf of a user without their direct
                    ///     intervention. This type is also for higher performance servers that process many plaintext authentication attempts at a
                    ///     time, such as mail or web servers. The LogonUserExExW function does not cache credentials for this logon type.
                    /// </summary>
                    LOGON32_LOGON_BATCH = 4,
                    /// <summary>
                    ///     Indicates a service-type logon. The account provided must have the service privilege enabled.
                    /// </summary>
                    LOGON32_LOGON_SERVICE = 5,
                    /// <summary>
                    ///     This logon type is for GINA DLLs that log on users who will be interactively using the computer. This logon type can
                    ///     generate a unique audit record that shows when the workstation was unlocked.
                    /// </summary>
                    LOGON32_LOGON_UNLOCK = 7,
                    /// <summary>
                    ///     This logon type preserves the name and password in the authentication package, which allows the server to make connections
                    ///     to other network servers while impersonating the client. A server can accept plaintext credentials from a client, call
                    ///     LogonUserExExW, verify that the user can access the system across the network, and still communicate with other servers.
                    /// </summary>
                    LOGON32_LOGON_NETWORK_CLEARTEXT = 8,
                    /// <summary>
                    ///     This logon type allows the caller to clone its current token and specify new credentials for outbound connections. The new
                    ///     logon session has the same local identifier but uses different credentials for other network connections. This logon type
                    ///     is supported only by the LOGON32_PROVIDER_WINNT50 logon provider.
                    /// </summary>
                    LOGON32_LOGON_NEW_CREDENTIALS = 9
                }
                /// <summary>
                ///     The logon provider.
                /// </summary>
                /// <see href="https://learn.microsoft.com/en-us/windows/win32/secauthn/logonuserexexw#parameters">LOGON32_PROVIDER</see>
                public enum LOGON32_PROVIDER : uint
                {
                    /// <summary>
                    ///     Use the standard logon provider for the system. The default security provider is NTLM.
                    /// </summary>
                    LOGON32_PROVIDER_DEFAULT = 0,
                    /// <summary>
                    ///     Use the NTLM logon provider.
                    /// </summary>
                    LOGON32_PROVIDER_WINNT40 = 2,
                    /// <summary>
                    ///     Use the negotiate logon provider.
                    /// </summary>
                    LOGON32_PROVIDER_WINNT50 = 3,
                    /// <summary>
                    /// </summary>
                    LOGON32_PROVIDER_VIRTUAL = 4
                }
            }
        }
    }
}
