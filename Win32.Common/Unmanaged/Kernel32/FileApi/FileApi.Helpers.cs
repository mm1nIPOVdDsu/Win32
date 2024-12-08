using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

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
                ///     Retrieves information about the file system and volume associated with the specified root directory.
                /// </summary>
                /// <param name="rootPathName">A string that contains the root directory of the volume to be described.</param>
                /// <returns><see cref="VOLUME_INFORMATION"/></returns>
                public static VOLUME_INFORMATION GetVolumeInformation(string rootPathName)
                {
                    var sb1 = new StringBuilder(MAX_PATH + 1);
                    var sb2 = new StringBuilder(MAX_PATH + 1);
                    var ret = GetVolumeInformation(rootPathName, sb1, sb1.Capacity, out var sn, out var cl, out var flags, sb2, sb2.Capacity);
                    if (ret is false)
                        throw new Win32Exception(Marshal.GetLastWin32Error());

                    return new VOLUME_INFORMATION()
                    {
                        Name = sb1.ToString(),
                        MaximumLength = cl,
                        Flags = flags,
                        FileSystemName = sb2.ToString(),
                        SerialNumber = sn
                    };
                }
            }
        }
    }
}
