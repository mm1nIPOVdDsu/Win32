namespace Win32.Common
{

    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <summary>
        ///     Shared interactions.
        /// </summary>
        public partial class Shared
        {
            /// <summary>
            ///     
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/596a1078-e883-4972-9bbc-49e60bebca55">NTStatus</see>
            public enum NTStatus : uint
            {
                /// <summary>
                ///     
                /// </summary>
                STATUS_SUCCESS = 0x00000000,
                /// <summary>
                ///     
                /// </summary>
                STATUS_PENDING = 0x00000103,
                /// <summary>
                ///     
                /// </summary>
                STATUS_NOTIFY_CLEANUP = 0x0000010B,
                /// <summary>
                ///     
                /// </summary>
                STATUS_NOTIFY_ENUM_DIR = 0x0000010C,
                /// <summary>
                ///     
                /// </summary>
                SEC_I_CONTINUE_NEEDED = 0x00090312,
                /// <summary>
                ///     
                /// </summary>
                STATUS_OBJECT_NAME_EXISTS = 0x40000000,
                /// <summary>
                ///     
                /// </summary>
                STATUS_BUFFER_OVERFLOW = 0x80000005,
                /// <summary>
                ///     
                /// </summary>
                STATUS_NO_MORE_FILES = 0x80000006,
                /// <summary>
                ///     
                /// </summary>
                SEC_E_SECPKG_NOT_FOUND = 0x80090305,
                /// <summary>
                ///     
                /// </summary>
                SEC_E_INVALID_TOKEN = 0x80090308,
                /// <summary>
                ///     
                /// </summary>
                STATUS_NOT_IMPLEMENTED = 0xC0000002,
                /// <summary>
                ///     
                /// </summary>
                STATUS_INVALID_INFO_CLASS = 0xC0000003,
                /// <summary>
                ///     
                /// </summary>
                STATUS_INFO_LENGTH_MISMATCH = 0xC0000004,
                /// <summary>
                ///     
                /// </summary>
                STATUS_INVALID_HANDLE = 0xC0000008,
                /// <summary>
                ///     
                /// </summary>
                STATUS_INVALID_PARAMETER = 0xC000000D,
                /// <summary>
                ///     
                /// </summary>
                STATUS_NO_SUCH_DEVICE = 0xC000000E,
                /// <summary>
                ///     
                /// </summary>
                STATUS_NO_SUCH_FILE = 0xC000000F,
                /// <summary>
                ///     
                /// </summary>
                STATUS_INVALID_DEVICE_REQUEST = 0xC0000010,
                /// <summary>
                ///     
                /// </summary>
                STATUS_END_OF_FILE = 0xC0000011,
                /// <summary>
                ///     
                /// </summary>
                STATUS_MORE_PROCESSING_REQUIRED = 0xC0000016,
                /// <summary>
                ///     
                /// </summary>
                STATUS_ACCESS_DENIED = 0xC0000022, // The user is not authorized to access the resource.
                /// <summary>
                ///     
                /// </summary>
                STATUS_BUFFER_TOO_SMALL = 0xC0000023,
                /// <summary>
                ///     
                /// </summary>
                STATUS_OBJECT_NAME_INVALID = 0xC0000033,
                /// <summary>
                ///     
                /// </summary>
                STATUS_OBJECT_NAME_NOT_FOUND = 0xC0000034,
                /// <summary>
                ///     
                /// </summary>
                STATUS_OBJECT_NAME_COLLISION = 0xC0000035, // The file already exists
                /// <summary>
                ///     
                /// </summary>
                STATUS_OBJECT_PATH_INVALID = 0xC0000039,
                /// <summary>
                ///     
                /// </summary>
                STATUS_OBJECT_PATH_NOT_FOUND = 0xC000003A, // The share path does not reference a valid resource.
                /// <summary>
                ///     
                /// </summary>
                STATUS_OBJECT_PATH_SYNTAX_BAD = 0xC000003B,
                /// <summary>
                ///     
                /// </summary>
                STATUS_DATA_ERROR = 0xC000003E, // IO error
                /// <summary>
                ///     
                /// </summary>
                STATUS_SHARING_VIOLATION = 0xC0000043,
                /// <summary>
                ///     
                /// </summary>
                STATUS_FILE_LOCK_CONFLICT = 0xC0000054,
                /// <summary>
                ///     
                /// </summary>
                STATUS_LOCK_NOT_GRANTED = 0xC0000055,
                /// <summary>
                ///     
                /// </summary>
                STATUS_DELETE_PENDING = 0xC0000056,
                /// <summary>
                ///     
                /// </summary>
                STATUS_PRIVILEGE_NOT_HELD = 0xC0000061,
                /// <summary>
                ///     
                /// </summary>
                STATUS_WRONG_PASSWORD = 0xC000006A,
                /// <summary>
                ///     
                /// </summary>
                STATUS_LOGON_FAILURE = 0xC000006D, // Authentication failure.
                /// <summary>
                ///     
                /// </summary>
                STATUS_ACCOUNT_RESTRICTION = 0xC000006E, // The user has an empty password, which is not allowed
                /// <summary>
                ///     
                /// </summary>
                STATUS_INVALID_LOGON_HOURS = 0xC000006F,
                /// <summary>
                ///     
                /// </summary>
                STATUS_INVALID_WORKSTATION = 0xC0000070,
                /// <summary>
                ///     
                /// </summary>
                STATUS_PASSWORD_EXPIRED = 0xC0000071,
                /// <summary>
                ///     
                /// </summary>
                STATUS_ACCOUNT_DISABLED = 0xC0000072,
                /// <summary>
                ///     
                /// </summary>
                STATUS_RANGE_NOT_LOCKED = 0xC000007E,
                /// <summary>
                ///     
                /// </summary>
                STATUS_DISK_FULL = 0xC000007F,
                /// <summary>
                ///     
                /// </summary>
                STATUS_INSUFFICIENT_RESOURCES = 0xC000009A,
                /// <summary>
                ///     
                /// </summary>
                STATUS_MEDIA_WRITE_PROTECTED = 0xC00000A2,
                /// <summary>
                ///     
                /// </summary>
                STATUS_FILE_IS_A_DIRECTORY = 0xC00000BA,
                /// <summary>
                ///     
                /// </summary>
                STATUS_NOT_SUPPORTED = 0xC00000BB,
                /// <summary>
                ///     
                /// </summary>
                STATUS_NETWORK_NAME_DELETED = 0xC00000C9,
                /// <summary>
                ///     
                /// </summary>
                STATUS_BAD_DEVICE_TYPE = 0xC00000CB,
                /// <summary>
                ///     
                /// </summary>
                STATUS_BAD_NETWORK_NAME = 0xC00000CC,
                /// <summary>
                ///     
                /// </summary>
                STATUS_TOO_MANY_SESSIONS = 0xC00000CE,
                /// <summary>
                ///     
                /// </summary>
                STATUS_REQUEST_NOT_ACCEPTED = 0xC00000D0,
                /// <summary>
                ///     
                /// </summary>
                STATUS_DIRECTORY_NOT_EMPTY = 0xC0000101,
                /// <summary>
                ///     
                /// </summary>
                STATUS_NOT_A_DIRECTORY = 0xC0000103,
                /// <summary>
                ///     
                /// </summary>
                STATUS_TOO_MANY_OPENED_FILES = 0xC000011F,
                /// <summary>
                ///     
                /// </summary>
                STATUS_CANCELLED = 0xC0000120,
                /// <summary>
                ///     
                /// </summary>
                STATUS_CANNOT_DELETE = 0xC0000121,
                /// <summary>
                ///     
                /// </summary>
                STATUS_FILE_CLOSED = 0xC0000128,
                /// <summary>
                ///     
                /// </summary>
                STATUS_LOGON_TYPE_NOT_GRANTED = 0xC000015B,
                /// <summary>
                ///     
                /// </summary>
                STATUS_ACCOUNT_EXPIRED = 0xC0000193,
                /// <summary>
                ///     
                /// </summary>
                STATUS_FS_DRIVER_REQUIRED = 0xC000019C,
                /// <summary>
                ///     
                /// </summary>
                STATUS_USER_SESSION_DELETED = 0xC0000203,
                /// <summary>
                ///     
                /// </summary>
                STATUS_INSUFF_SERVER_RESOURCES = 0xC0000205,
                /// <summary>
                ///     
                /// </summary>
                STATUS_PASSWORD_MUST_CHANGE = 0xC0000224,
                /// <summary>
                ///     
                /// </summary>
                STATUS_NOT_FOUND = 0xC0000225,
                /// <summary>
                ///     
                /// </summary>
                STATUS_ACCOUNT_LOCKED_OUT = 0xC0000234,
                /// <summary>
                ///     
                /// </summary>
                STATUS_PATH_NOT_COVERED = 0xC0000257,
                /// <summary>
                ///     
                /// </summary>
                STATUS_NOT_A_REPARSE_POINT = 0xC0000275,

                /// <summary>
                ///     
                /// </summary>
                STATUS_INVALID_SMB = 0x00010002,        // SMB1/CIFS: A corrupt or invalid SMB request was received
                /// <summary>
                ///     
                /// </summary>
                STATUS_SMB_BAD_COMMAND = 0x00160002,    // SMB1/CIFS: An unknown SMB command code was received by the server
                /// <summary>
                ///     
                /// </summary>
                STATUS_SMB_BAD_FID = 0x00060001,        // SMB1/CIFS
                /// <summary>
                ///     
                /// </summary>
                STATUS_SMB_BAD_TID = 0x00050002,        // SMB1/CIFS
                /// <summary>
                ///     
                /// </summary>
                STATUS_OS2_INVALID_ACCESS = 0x000C0001, // SMB1/CIFS
                /// <summary>
                ///     
                /// </summary>
                STATUS_OS2_NO_MORE_SIDS = 0x00710001,   // SMB1/CIFS
                /// <summary>
                ///     
                /// </summary>
                STATUS_OS2_INVALID_LEVEL = 0x007C0001,  // SMB1/CIFS
            }

        }
    }
}
