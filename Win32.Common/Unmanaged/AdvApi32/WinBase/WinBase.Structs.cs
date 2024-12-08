using System;
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
                ///     Contains information about a hardware profile.
                /// </summary>
                [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
                public struct HW_PROFILE_INFO
                {
                    /// <summary>
                    ///     The reported docking state of the computer.
                    /// </summary>
                    public DOCKINGINFO dwDockInfo;
                    /// <summary>
                    ///     The globally unique identifier (GUID) string for the current hardware profile. The string returned by GetCurrentHwProfile encloses the GUID in curly braces.
                    /// </summary>
                    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 39)]
                    public string szHwProfileGuid;
                    /// <summary>
                    ///     The display name for the current hardware profile.
                    /// </summary>
                    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
                    public string szHwProfileName;
                }
                /// <summary>
                ///     The SECURITY_ATTRIBUTES structure contains the security descriptor for an object and specifies whether the handle retrieved by specifying this structure is inheritable.
                /// </summary>
                /// <seealso href="https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/aa379560(v=vs.85)">SECURITY_ATTRIBUTES</seealso>
                [StructLayout(LayoutKind.Sequential)]
                public struct SECURITY_ATTRIBUTES
                {
                    /// <summary>
                    ///     The size, in bytes, of this structure. Set this value to the size of the SECURITY_ATTRIBUTES structure.
                    /// </summary>
                    public int Length;
                    /// <summary>
                    ///     A pointer to a SECURITY_DESCRIPTOR structure that controls access to the object. If the value of this member is NULL, the object is assigned the default security descriptor associated with the access token of the calling process.
                    /// </summary>
                    public IntPtr lpSecurityDescriptor;
                    /// <summary>
                    ///     A Boolean value that specifies whether the returned handle is inherited when a new process is created. If this member is TRUE, the new process inherits the handle.
                    /// </summary>
                    public bool bInheritHandle;
                }
            }
        }
    }
}
