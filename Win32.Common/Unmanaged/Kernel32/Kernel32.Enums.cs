namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <summary>
        ///     Kernel32 interactions.
        /// </summary>
        public partial class Kernel32
        {
            /// <summary>
            ///     The SECURITY_IMPERSONATION_LEVEL enumeration contains values that specify security impersonation levels. Security impersonation levels govern the degree to which a server process can act on behalf of a client process.
            /// </summary>
            /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/winnt/ne-winnt-security_impersonation_level">SECURITY_IMPERSONATION_LEVEL</seealso>
            public enum SECURITY_IMPERSONATION_LEVEL
            {
                /// <summary>
                ///     The server cannot impersonate or identify the client.
                /// </summary>
                SecurityAnonymous = 0,
                /// <summary>
                ///     The server can get the identity and privileges of the client, but cannot impersonate the client.
                /// </summary>
                SecurityIdentification = 1,
                /// <summary>
                ///     The server can impersonate the client's security context on the local system.
                /// </summary>
                SecurityImpersonation = 2,
                /// <summary>
                ///     The server can impersonate the client's security context on remote systems.
                /// </summary>
                SecurityDelegation = 3,
            }
            /// <summary>
            ///     The TOKEN_TYPE enumeration contains values that differentiate between a primary token and an impersonation token.
            /// </summary>
            public enum TOKEN_TYPE
            {
                /// <summary>
                ///     Indicates a primary token.
                /// </summary>
                TokenPrimary = 1,
                /// <summary>
                ///     Indicates an impersonation token.
                /// </summary>
                TokenImpersonation = 2
            }
            /// <summary>
            ///     Any additional information about the system.
            /// </summary>
            public enum VERSION_PRODUCT_TYPE
            {
                /// <summary>
                ///     The operating system is Windows 8, Windows 7, Windows Vista, Windows XP Professional, Windows XP Home Edition, or Windows 2000 Professional.
                /// </summary>
                VER_NT_WORKSTATION = 0x0000001,
                /// <summary>
                ///     The system is a domain controller and the operating system is Windows Server 2012 , Windows Server 2008 R2, Windows Server 2008, Windows Server 2003, or Windows 2000 Server.
                /// </summary>
                VER_NT_DOMAIN_CONTROLLER = 0x0000002,
                /// <summary>
                ///     The operating system is Windows Server 2012, Windows Server 2008 R2, Windows Server 2008, Windows Server 2003, or Windows 2000 Server.
                ///     Note that a server that is also a domain controller is reported as VER_NT_DOMAIN_CONTROLLER, not VER_NT_SERVER.
                /// </summary>
                VER_NT_SERVER = 0x0000003
            }
            /// <summary>
            ///     Product version information.
            /// </summary>
            public enum VERSION_SUITE : int
            {
                /// <summary>
                ///     Microsoft BackOffice components are installed.
                /// </summary>
                VER_SUITE_BACKOFFICE = 0x00000004,
                /// <summary>
                ///     Windows Server 2003, Web Edition is installed.
                /// </summary>
                VER_SUITE_BLADE = 0x00000400,
                /// <summary>
                ///     Windows Server 2003, Compute Cluster Edition is installed.
                /// </summary>
                VER_SUITE_COMPUTE_SERVER = 0x00004000,
                /// <summary>
                ///     Windows Server 2008 Datacenter, Windows Server 2003, Datacenter Edition, or Windows 2000 Datacenter Server is installed.
                /// </summary>
                VER_SUITE_DATACENTER = 0x00000080,
                /// <summary>
                ///     Windows Server 2008 Enterprise, Windows Server 2003, Enterprise Edition, or Windows 2000 Advanced Server is installed. Refer to the Remarks section for more information about this bit flag.
                /// </summary>
                VER_SUITE_ENTERPRISE = 0x00000002,
                /// <summary>
                ///     Windows XP Embedded is installed.
                /// </summary>
                VER_SUITE_EMBEDDEDNT = 0x00000040,
                /// <summary>
                ///     Windows Vista Home Premium, Windows Vista Home Basic, or Windows XP Home Edition is installed.
                /// </summary>
                VER_SUITE_PERSONAL = 0x00000200,
                /// <summary>
                ///     Remote Desktop is supported, but only one interactive session is supported. This value is set unless the system is running in application server mode.
                /// </summary>
                VER_SUITE_SINGLEUSERTS = 0x00000100,
                /// <summary>
                ///     Microsoft Small Business Server was once installed on the system, but may have been upgraded to another version of Windows. Refer to the Remarks section for more information about this bit flag.
                /// </summary>
                VER_SUITE_SMALLBUSINESS = 0x00000001,
                /// <summary>
                ///     Microsoft Small Business Server is installed with the restrictive client license in force. Refer to the Remarks section for more information about this bit flag.
                /// </summary>
                VER_SUITE_SMALLBUSINESS_RESTRICTED = 0x00000020,
                /// <summary>
                ///     Windows Storage Server 2003 R2 or Windows Storage Server 2003is installed.
                /// </summary>
                VER_SUITE_STORAGE_SERVER = 0x00002000,
                /// <summary>
                ///     Terminal Services is installed. This value is always set.
                ///     If VER_SUITE_TERMINAL is set but VER_SUITE_SINGLEUSERTS is not set, the system is running in application server mode.
                /// </summary>
                VER_SUITE_TERMINAL = 0x00000010,
                /// <summary>
                ///     Windows Home Server is installed.
                /// </summary>
                VER_SUITE_WH_SERVER = 0x00008000,
                /// <summary>
                ///     AppServer mode is enabled.
                /// </summary>
                VER_SUITE_MULTIUSERTS = 0x00020000
            }
        }
    }
}
