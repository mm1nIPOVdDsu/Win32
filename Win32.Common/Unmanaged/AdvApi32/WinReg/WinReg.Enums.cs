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
            ///     WinReg interactions.
            /// </summary>
            public partial class WinReg
            {
                /// <summary>
                ///     Defines a hive of the registry.
                /// </summary>
                public enum REG_HIVE : uint
                {
                    /// <summary>
                    ///     The registry's root hive.
                    /// </summary>
                    HKEY_CLASSES_ROOT = 0x80000000,
                    /// <summary>
                    ///     The registry's current user hive.
                    /// </summary>
                    HKEY_CURRENT_USER = 0x80000001,
                    /// <summary>
                    ///     The registry's local machine hive.
                    /// </summary>
                    HKEY_LOCAL_MACHINE = 0x80000002,
                    /// <summary>
                    ///     The registry's users hive.
                    /// </summary>
                    HKEY_USERS = 0x80000003,
                    /// <summary>
                    ///     The registry's performance data hive.
                    /// </summary>
                    HKEY_PERFORMANCE_DATA = 0x80000004,
                    /// <summary>
                    ///     The registry's current config hive.
                    /// </summary>
                    HKEY_CURRENT_CONFIG = 0x80000005,
                    /// <summary>
                    ///     The registry's data hive.
                    /// </summary>
                    HKEY_DYN_DATA = 0x80000006
                }
                /// <summary>
                ///     A value that indicates the changes that should be reported.
                /// </summary>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regnotifychangekeyvalue#parameters">
                ///     REG_NOTIFY_FILTER
                /// </seealso>
                [Flags]
                public enum REG_NOTIFY_FILTER : uint
                {
                    /// <summary>
                    ///     Notify the caller if a subkey is added or deleted.
                    /// </summary>
                    REG_NOTIFY_CHANGE_NAME = 0x00000001,
                    /// <summary>
                    ///     Notify the caller of changes to the attributes of the key, such as the security descriptor information.
                    /// </summary>
                    REG_NOTIFY_CHANGE_ATTRIBUTES = 0x00000002,
                    /// <summary>
                    ///     Notify the caller of changes to a value of the key. This can include adding or deleting a value, or changing an existing value.
                    /// </summary>
                    REG_NOTIFY_CHANGE_LAST_SET = 0x00000004,
                    /// <summary>
                    ///     Notify the caller of changes to the security descriptor of the key.
                    /// </summary>
                    REG_NOTIFY_CHANGE_SECURITY = 0x00000008,
                    /// <summary>
                    ///     Indicates that the lifetime of the registration must not be tied to the lifetime of the thread issuing the
                    ///     RegNotifyChangeKeyValue call.
                    /// </summary>
                    REG_NOTIFY_THREAD_AGNOSTIC = 0x10000000
                }
                /// <summary>
                ///     Access rights for registry key objects.
                /// </summary>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/sysinfo/registry-key-security-and-access-rights">KEY_ACCESS_RIGHTS</seealso>
                public enum KEY_ACCESS_RIGHTS : uint
                {
                    /// <summary>
                    ///     Combines the STANDARD_RIGHTS_REQUIRED, KEY_QUERY_VALUE, KEY_SET_VALUE, KEY_CREATE_SUB_KEY, KEY_ENUMERATE_SUB_KEYS,
                    ///     KEY_NOTIFY, and KEY_CREATE_LINK access rights.
                    /// </summary>
                    KEY_ALL_ACCESS = 0xF003F,
                    /// <summary>
                    ///     Reserved for system use.
                    /// </summary>
                    KEY_CREATE_LINK = 0x0020,
                    /// <summary>
                    ///     Required to create a subkey of a registry key.
                    /// </summary>
                    KEY_CREATE_SUB_KEY = 0x0004,
                    /// <summary>
                    ///     Required to enumerate the subkeys of a registry key.
                    /// </summary>
                    KEY_ENUMERATE_SUB_KEYS = 0x0008,
                    /// <summary>
                    ///     Equivalent to KEY_READ.
                    /// </summary>
                    KEY_EXECUTE = 0x20019,
                    /// <summary>
                    ///     Required to request change notifications for a registry key or for subkeys of a registry key.
                    /// </summary>
                    KEY_NOTIFY = 0x0010,
                    /// <summary>
                    ///     Required to query the values of a registry key.
                    /// </summary>
                    KEY_QUERY_VALUE = 0x0001,
                    /// <summary>
                    ///     Combines the STANDARD_RIGHTS_READ, KEY_QUERY_VALUE, KEY_ENUMERATE_SUB_KEYS, and KEY_NOTIFY values.
                    /// </summary>
                    KEY_READ = 0x20019,
                    /// <summary>
                    ///     Required to create, delete, or set a registry value.
                    /// </summary>
                    KEY_SET_VALUE = 0x0002,
                    /// <summary>
                    ///     Indicates that an application on 64-bit Windows should operate on the 32-bit registry view. This flag is ignored by 32-bit Windows.
                    /// </summary>
                    KEY_WOW64_32KEY = 0x0200,
                    /// <summary>
                    ///     Indicates that an application on 64-bit Windows should operate on the 64-bit registry view. This flag is ignored by 32-bit Windows.
                    /// </summary>
                    KEY_WOW64_64KEY = 0x0100,
                    /// <summary>
                    ///     Combines the STANDARD_RIGHTS_WRITE, KEY_SET_VALUE, and KEY_CREATE_SUB_KEY access rights.
                    /// </summary>
                    KEY_WRITE = 0x20006

                }
                /// <summary>
                ///     
                /// </summary>
                [Flags]
                public enum SECURITY_INFORMATION : uint
                {
                    OWNER_SECURITY_INFORMATION = 0x00000001,
                    GROUP_SECURITY_INFORMATION = 0x00000002,
                    DACL_SECURITY_INFORMATION = 0x00000004,
                    SACL_SECURITY_INFORMATION = 0x00000008,
                    UNPROTECTED_SACL_SECURITY_INFORMATION = 0x10000000,
                    UNPROTECTED_DACL_SECURITY_INFORMATION = 0x20000000,
                    PROTECTED_SACL_SECURITY_INFORMATION = 0x40000000,
                    PROTECTED_DACL_SECURITY_INFORMATION = 0x80000000
                }
            }
        }
    }
}
