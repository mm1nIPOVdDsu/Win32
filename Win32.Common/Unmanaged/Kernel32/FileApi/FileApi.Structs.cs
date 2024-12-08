using System.Diagnostics.Contracts;

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
                ///     Information about a volume.
                /// </summary>
                public struct VOLUME_INFORMATION
                {
                    /// <summary>
                    ///     The root directory of the volume to be described.
                    /// </summary>
                    public string Name;
                    /// <summary>
                    ///     The maximum length, in characters, of a file name component that a specified file system supports.
                    /// </summary>
                    public uint MaximumLength;
                    /// <summary>
                    ///     The <see cref="FileSystemFlags"/> associated with the specified file system.
                    /// </summary>
                    public FileSystemFlags Flags;
                    /// <summary>
                    ///     The name of the file system, for example, the FAT file system or the NTFS file system.
                    /// </summary>
                    public string FileSystemName;
                    /// <summary>
                    ///     The volume serial number.
                    /// </summary>
                    public uint SerialNumber;
                }
            }
        }
    }
}
