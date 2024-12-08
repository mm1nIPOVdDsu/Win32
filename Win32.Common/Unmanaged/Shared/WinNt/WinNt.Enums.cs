using System;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <summary>
        ///     WinNt interactions.
        /// </summary>
        public partial class WinNt
        {
            /// <summary>
            ///     Generic rights.
            /// </summary>
            /// <seealso href="https://github.com/dotnet/pinvoke/blob/db3e17e01060616ef1b79155cfe0bf4c2ea1ca26/src/Kernel32/Kernel32%2BACCESS_MASK.cs#L57">GENERIC_RIGHTS</seealso>
            [Flags]
            public enum GENERIC_RIGHTS : uint
            {
                /// <summary>
                ///     All rights.
                /// </summary>
                GENERIC_ALL = 0x10000000,
                /// <summary>
                ///     Execute right.
                /// </summary>
                GENERIC_EXECUTE = 0x20000000,
                /// <summary>
                ///     Write right.
                /// </summary>
                GENERIC_WRITE = 0x40000000,
                /// <summary>
                ///     Read right.
                /// </summary>
                GENERIC_READ = 0x80000000,
            }
            /// <summary>
            ///     Special rights.
            /// </summary>
            /// <seealso href="https://github.com/dotnet/pinvoke/blob/db3e17e01060616ef1b79155cfe0bf4c2ea1ca26/src/Kernel32/Kernel32%2BACCESS_MASK.cs#L66">SPECIAL_RIGHTS</seealso>
            [Flags]
            public enum SPECIAL_RIGHTS : uint
            {
                /// <summary>
                ///     It is used to indicate access to a system access control list (SACL). This type of access requires the calling process to have the
                ///     SE_SECURITY_NAME (Manage auditing and security log) privilege. If this flag is set in the access mask of an audit access ACE
                ///     (successful or unsuccessful access), the SACL access will be audited.
                /// </summary>
                ACCESS_SYSTEM_SECURITY = 0x01000000,
                /// <summary>
                ///     Maximum allowed.
                /// </summary>
                MAXIMUM_ALLOWED = 0x02000000,
            }
            /// <summary>
            ///     Contains the object's standard access rights.
            /// </summary>
            /// <seealso href="https://github.com/dotnet/pinvoke/blob/db3e17e01060616ef1b79155cfe0bf4c2ea1ca26/src/Kernel32/Kernel32%2BACCESS_MASK.cs#L83">STANDARD_RIGHTS</seealso>
            [Flags]
            public enum STANDARD_RIGHTS : uint
            {
                /// <summary>
                ///     Delete access.
                /// </summary>
                DELETE = 0x00010000,
                /// <summary>
                ///     Read access to the owner, group, and discretionary access control list (DACL) of the security descriptor.
                /// </summary>
                READ_CONTROL = 0x00020000,
                /// <summary>
                ///     Write access to the DACL.
                /// </summary>
                WRITE_DAC = 0x00040000,
                /// <summary>
                ///     Write access to owner.
                /// </summary>
                WRITE_OWNER = 0x00080000,
                /// <summary>
                ///     Synchronize access.
                /// </summary>
                SYNCHRONIZE = 0x00100000,
                /// <summary>
                ///     Standard set of special rights.
                /// </summary>
                STANDARD_RIGHTS_REQUIRED = 0x000F0000,
                /// <summary>
                ///     See also <see cref="READ_CONTROL"/>
                /// </summary>
                STANDARD_RIGHTS_READ = READ_CONTROL,
                /// <summary>
                ///     See also <see cref="READ_CONTROL"/>
                /// </summary>
                STANDARD_RIGHTS_WRITE = READ_CONTROL,
                /// <summary>
                ///     See also <see cref="READ_CONTROL"/>
                /// </summary>
                STANDARD_RIGHTS_EXECUTE = READ_CONTROL,
                /// <summary>
                ///     All standard rights.
                /// </summary>
                STANDARD_RIGHTS_ALL = 0x001F0000,
            }
            /// <summary>
            ///     Contains the access mask specific to the object type associated with the mask.
            /// </summary>
            /// <seealso href="https://github.com/dotnet/pinvoke/blob/db3e17e01060616ef1b79155cfe0bf4c2ea1ca26/src/Kernel32/Kernel32%2BACCESS_MASK.cs#L134">SPECIFIC_RIGHTS</seealso>
            [Flags]
            public enum SPECIFIC_RIGHTS : uint
            {
                /// <summary>
                ///     The bit mask that covers specific rights.
                /// </summary>
                SPECIFIC_RIGHTS_ALL = 0x0000FFFF,
            }
        }
    }
}
