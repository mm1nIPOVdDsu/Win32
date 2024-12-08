using System.Runtime.InteropServices;

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
            ///		Contains operating system version information.
            /// </summary>
            /// <remarks>
            ///     The information includes major and minor version numbers, a build number, a platform identifier, and information about product suites and the latest Service Pack installed on the system. This structure is used with the GetVersionEx and VerifyVersionInfo functions.
            /// </remarks>
            [StructLayout(LayoutKind.Sequential)]
            public struct OSVERSIONINFOEX
            {
                /// <summary>
                ///     The size of this data structure, in bytes. Set this member to sizeof(OSVERSIONINFOEX).
                /// </summary>
                public int dwOSVersionInfoSize;
                /// <summary>
                ///     The major version number of the operating system.
                /// </summary>
                public int dwMajorVersion;
                /// <summary>
                ///     The minor version number of the operating system
                /// </summary>
                public int dwMinorVersion;
                /// <summary>
                ///     The build number of the operating system.
                /// </summary>
                public int dwBuildNumber;
                /// <summary>
                ///     The operating system platform. This member can be VER_PLATFORM_WIN32_NT (2).
                /// </summary>
                public int dwPlatformId;
                /// <summary>
                ///     A null-terminated string, such as "Service Pack 3", that indicates the latest Service Pack installed on the system. If no Service Pack has been installed, the string is empty.
                /// </summary>
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
                public string szCSDVersion;
                /// <summary>
                ///     The major version number of the latest Service Pack installed on the system. For example, for Service Pack 3, the major version number is 3. If no Service Pack has been installed, the value is zero.
                /// </summary>
                public short wServicePackMajor;
                /// <summary>
                ///     The minor version number of the latest Service Pack installed on the system. For example, for Service Pack 3, the minor version number is 0.
                /// </summary>
                public short wServicePackMinor;
                /// <summary>
                ///     A bit mask that identifies the product suites available on the system.
                /// </summary>
                public VERSION_SUITE wSuiteMask;
                /// <summary>
                ///     Any additional information about the system.
                /// </summary>
                public VERSION_PRODUCT_TYPE wProductType;
                /// <summary>
                ///     Reserved for future use.
                /// </summary>
                public byte wReserved;
            }
        }
    }
}
