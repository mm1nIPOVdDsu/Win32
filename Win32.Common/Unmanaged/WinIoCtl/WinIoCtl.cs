namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {

        /// <summary>
        ///     WinIoCtl interactions.
        /// </summary>
        public partial class WinIoCtl
        {
            public const int GENERIC_READ = -2147483648;
            public const int GENERIC_WRITE = 1073741824;
            public const int OPEN_EXISTING = 3;

            public const int FILE_SHARE_READ = 0x0000000001;
            public const int FILE_SHARE_WRITE = 0x0000000002;

            public const int IOCTL_DISK_UPDATE_PROPERTIES = 0x70140;
            public const int IOCTL_DISK_SET_DRIVE_LAYOUT_EX = 0x7C054;
        }
    }
}
