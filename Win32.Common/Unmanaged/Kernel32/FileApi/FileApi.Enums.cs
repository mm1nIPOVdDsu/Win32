using System;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <inheritdoc/>
        public partial class Kernel32
        {
            /// <summary>
            ///     FileApi interactions.
            /// </summary>
            public partial class FileApi
            {
                /// <summary>
                ///     Flags that may be passed to the
                /// </summary>
                [Flags]
                public enum FileSystemFlags : uint
                {
                    /// <summary>
                    ///     The specified volume supports case-sensitive file names.
                    /// </summary>
                    FILE_CASE_SENSITIVE_SEARCH = 0x00000001,
                    /// <summary>
                    ///     The specified volume supports preserved case of file names when it places a name on disk.
                    /// </summary>
                    FILE_CASE_PRESERVED_NAMES = 0x00000002,
                    /// <summary>
                    ///     The specified volume supports Unicode in file names as they appear on disk.
                    /// </summary>
                    FILE_UNICODE_ON_DISK = 0x00000004,
                    /// <summary>
                    ///     The specified volume preserves and enforces access control lists (ACL). For example, the NTFS file system preserves and
                    ///     enforces ACLs, and the FAT file system does not.
                    /// </summary>
                    FILE_PERSISTENT_ACLS = 0x00000008,
                    /// <summary>
                    ///     The specified volume supports file-based compression.
                    /// </summary>
                    FILE_FILE_COMPRESSION = 0x00000010,
                    /// <summary>
                    ///     The specified volume supports disk quotas.
                    /// </summary>
                    FILE_VOLUME_QUOTAS = 0x00000020,
                    /// <summary>
                    ///     The specified volume supports sparse files.
                    /// </summary>
                    FILE_SUPPORTS_SPARSE_FILES = 0x00000040,
                    /// <summary>
                    ///     The specified volume supports re-parse points.
                    /// </summary>
                    FILE_SUPPORTS_REPARSE_POINTS = 0x00000080,
                    /// <summary>
                    ///     The file system supports remote storage.
                    /// </summary>
                    FILE_SUPPORTS_REMOTE_STORAGE = 0x00000100,
                    /// <summary>
                    ///     The file system returns information that describes additional actions taken during cleanup, such as deleting the file.
                    /// </summary>
                    FILE_RETURNS_CLEANUP_RESULT_INFO = 0x00000200,
                    /// <summary>
                    ///     The file system supports POSIX-style delete and rename operations.
                    /// </summary>
                    FILE_SUPPORTS_POSIX_UNLINK_RENAME = 0x00000400,
                    /// <summary>
                    ///     The specified volume is a compressed volume.
                    /// </summary>
                    FILE_VOLUME_IS_COMPRESSED = 0x00008000,
                    /// <summary>
                    ///     The specified volume supports object identifiers.
                    /// </summary>
                    FILE_SUPPORTS_OBJECT_IDS = 0x00010000,
                    /// <summary>
                    ///     The specified volume supports the Encrypted File System (EFS). For more information, see
                    /// </summary>
                    FILE_SUPPORTS_ENCRYPTION = 0x00020000,
                    /// <summary>
                    ///     The specified volume supports named streams.
                    /// </summary>
                    FILE_NAMED_STREAMS = 0x00040000,
                    /// <summary>
                    ///     The specified volume is read-only.
                    /// </summary>
                    FILE_READ_ONLY_VOLUME = 0x00080000,
                    /// <summary>
                    ///     The specified volume supports a single sequential write.
                    /// </summary>
                    FILE_SEQUENTIAL_WRITE_ONCE = 0x00100000,
                    /// <summary>
                    ///     The specified volume supports transactions. For more information, see
                    /// </summary>
                    FILE_SUPPORTS_TRANSACTIONS = 0x00200000,
                    /// <summary>
                    ///     The specified volume supports hard links. For more information, see
                    /// </summary>
                    /// <remarks>
                    ///     Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP: This value is not supported until Windows
                    ///     Server 2008 R2 and Windows 7.
                    /// </remarks>
                    FILE_SUPPORTS_HARD_LINKS = 0x00400000,
                    /// <summary>
                    ///     The specified volume supports extended attributes. An extended attribute is a piece of application-specific metadata that an
                    ///     application can associate with a file and is not part of the file's data.
                    /// </summary>
                    /// <remarks>
                    ///     Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP: This value is not supported until Windows
                    ///     Server 2008 R2 and Windows 7.
                    /// </remarks>
                    FILE_SUPPORTS_EXTENDED_ATTRIBUTES = 0x00800000,
                    /// <summary>
                    ///     The file system supports open by FileID. For more information, see FILE_ID_BOTH_DIR_INFO.
                    /// </summary>
                    /// <remarks>
                    ///     Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP: This value is not supported until Windows
                    ///     Server 2008 R2 and Windows 7.
                    /// </remarks>
                    FILE_SUPPORTS_OPEN_BY_FILE_ID = 0x01000000,
                    /// <summary>
                    ///     The specified volume supports update sequence number (USN) journals. For more information, see
                    /// </summary>
                    /// <remarks>
                    ///     Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP: This value is not supported until Windows
                    ///     Server 2008 R2 and Windows 7.
                    /// </remarks>
                    FILE_SUPPORTS_USN_JOURNAL = 0x02000000,
                    /// <summary>
                    ///     The specified volume supports integrity streams.
                    /// </summary>
                    FILE_SUPPORTS_INTEGRITY_STREAMS = 0x04000000,
                    /// <summary>
                    ///     The specified volume supports block refcounting.
                    /// </summary>
                    FILE_SUPPORTS_BLOCK_REFCOUNTING = 0x08000000,
                    /// <summary>
                    ///     The specified volume supports sparse VDL.
                    /// </summary>
                    FILE_SUPPORTS_SPARSE_VDL = 0x10000000,
                    /// <summary>
                    ///     The specified volume is a direct access (DAX) volume.
                    /// </summary>
                    /// <remarks>
                    ///     This flag was introduced in Windows 10, version 1607.
                    /// </remarks>
                    FILE_DAX_VOLUME = 0x20000000,
                    /// <summary>
                    ///     The specified volume supports ghosting.
                    /// </summary>
                    FILE_SUPPORTS_GHOSTING = 0x40000000
                }
            }
        }
    }
}
