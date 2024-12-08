using System;
using System.Runtime.InteropServices;

using static Win32.Common.Unmanaged.Shared;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <inheritdoc/>
        public partial class AdvApi32
        {
            /// <summary>
            ///     WinReg interactions.
            /// </summary>
            public partial class WinReg
            {
                /// <summary>
                ///     The SECURITY_INFORMATION data type identifies the object-related security information being set or queried.
                /// </summary>
                /// <see href="https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/ntifs/ns-ntifs-_security_descriptor">SECURITY_DESCRIPTOR</see>
                [StructLayoutAttribute(LayoutKind.Sequential)]
                public struct SECURITY_DESCRIPTOR
                {
                    /// <summary>
                    ///     Specifies the revision level of the security descriptor.
                    /// </summary>
                    public byte Revision;
                    /// <summary>
                    ///     Specifies a zero byte of padding that aligns the Revision member on a 16-bit boundary.
                    /// </summary>
                    public byte Size;
                    /// <summary>
                    ///     The control information of security descriptor. For more information, see <see cref="SECURITY_DESCRIPTOR_CONTROL">SECURITY_DESCRIPTOR_CONTROL</see>.
                    /// </summary>
                    public short Control;
                    /// <summary>
                    ///     A pointer to an owner security identifier.
                    /// </summary>
                    /// <remarks>This member might be invalid. You should use RtlGetOwnerSecurityDescriptor to get an owner security identifier.</remarks>
                    public IntPtr Owner;
                    /// <summary>
                    ///     A pointer to a primary group security identifier.
                    /// </summary>
                    /// <remarks>This member might be invalid. You should use RtlGetGroupSecurityDescriptor to get this member.</remarks>
                    public IntPtr Group;
                    /// <summary>
                    ///     A pointer to a system access control list (SACL).
                    /// </summary>
                    /// <remarks>This member might be invalid. You should use RtlGetSaclSecurityDescriptor to get this member.</remarks>
                    public IntPtr Sacl;
                    /// <summary>
                    ///     A pointer to a discretionary access control list (DACL).
                    /// </summary>
                    /// <remarks>This member might be invalid. You should use RtlGetDaclSecurityDescriptor to get this member.</remarks>
                    public IntPtr Dacl;
                }
            }
        }
    }
}