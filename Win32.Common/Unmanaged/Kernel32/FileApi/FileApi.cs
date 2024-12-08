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
                public const UInt32 FILE_SHARE_READ = 0x00000001;
                public const UInt32 FILE_SHARE_WRITE = 0x00000002;
                public const UInt32 OPEN_EXISTING = 0x00000003;
                public const UInt32 FILE_ATTRIBUTE_NORMAL = 0x80;
                public const UInt32 ERROR_ACCESS_DENIED = 5;
                public const UInt32 ATTACH_PARRENT = 0xFFFFFFFF;
            }
        }
    }
}