using System;
using System.Runtime.InteropServices;
using System.Text;

using static Unmanaged.Kernel32;

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
                ///     Creates or opens a file or I/O device.
                /// </summary>
                /// <remarks>
                ///     The most commonly used I/O devices are as follows: file, file stream, directory, physical disk, volume, console buffer, tape drive, 
                ///     communications resource, mailslot, and pipe. The function returns a handle that can be used to access the file or device for 
                ///     various types of I/O depending on the file or device and the flags and attributes specified.
                /// </remarks>
                /// <param name="lpFileName">The name of the file or device to be created or opened. You may use either forward slashes (/) or backslashes (\) in this name.</param>
                /// <param name="dwDesiredAccess">The requested access to the file or device, which can be summarized as read, write, both or neither zero).</param>
                /// <param name="dwShareMode">The requested sharing mode of the file or device, which can be read, write, both, delete, all of these, or none.</param>
                /// <param name="lpSecurityAttributes">A pointer to a SECURITY_ATTRIBUTES structure that contains two separate but related data members: an optional security descriptor, and a Boolean value that determines whether the returned handle can be inherited by child processes.</param>
                /// <param name="dwCreationDisposition">An action to take on a file or device that exists or does not exist.</param>
                /// <param name="dwFlagsAndAttributes">The file or device attributes and flags, FILE_ATTRIBUTE_NORMAL being the most common default value for files.</param>
                /// <param name="hTemplateFile">A valid handle to a template file with the GENERIC_READ access right. The template file supplies file attributes and extended attributes for the file that is being created.</param>
                /// <returns>If the function succeeds, the return value is an open handle to the specified file, device, named pipe, or mail slot.</returns>
                [DllImport(Kernel32Dll, EntryPoint = "CreateFileW", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
                public static extern IntPtr CreateFileW(string lpFileName, UInt32 dwDesiredAccess, UInt32 dwShareMode, IntPtr lpSecurityAttributes, UInt32 dwCreationDisposition, UInt32 dwFlagsAndAttributes, IntPtr hTemplateFile);
                /// <summary>
                ///     Retrieves the name of a volume on a computer. FindFirstVolume is used to begin scanning the volumes of a computer.
                /// </summary>
                /// <param name="lpszVolumeName">A pointer to a buffer that receives a null-terminated string that specifies a volume GUID path for the first volume that is found.</param>
                /// <param name="cchBufferLength">The length of the buffer to receive the volume GUID path, in TCHARs.</param>
                /// <returns>If the function succeeds, the return value is a search handle used in a subsequent call to the FindNextVolume and FindVolumeClose functions.</returns>
                [DllImport(Kernel32Dll, SetLastError = true)]
                public static extern FindVolumeSafeHandle FindFirstVolume([Out] StringBuilder lpszVolumeName, uint cchBufferLength);
                /// <summary>
                ///     Continues a volume search started by a call to the FindFirstVolume function. FindNextVolume finds one volume per call.
                /// </summary>
                /// <param name="hFindVolume">The volume search handle returned by a previous call to the FindFirstVolume function.</param>
                /// <param name="lpszVolumeName">A pointer to a string that receives the volume GUID path that is found.</param>
                /// <param name="cchBufferLength">The length of the buffer that receives the volume GUID path, in TCHARs.</param>
                /// <returns>If the function succeeds, the return value is nonzero.</returns>
                [DllImport(Kernel32Dll, SetLastError = true)]
                public static extern bool FindNextVolume(FindVolumeSafeHandle hFindVolume, [Out] StringBuilder lpszVolumeName, uint cchBufferLength);
                /// <summary>
                ///     Closes the specified volume search handle. The FindFirstVolume and FindNextVolume functions use this search handle to locate volumes.
                /// </summary>
                /// <param name="hFindVolume">Closes the specified volume search handle. The FindFirstVolume and FindNextVolume functions use this search handle to locate volumes.</param>
                /// <returns>If the function succeeds, the return value is nonzero.</returns>
                [DllImport(Kernel32Dll, SetLastError = true)]
                public static extern bool FindVolumeClose(IntPtr hFindVolume);
                /// <summary>
                ///     Retrieves information about the file system and volume associated with the specified root directory.
                /// </summary>
                /// <param name="lpRootPathName">A pointer to a string that contains the root directory of the volume to be described.</param>
                /// <param name="lpVolumeNameBuffer">A pointer to a buffer that receives the name of a specified volume. The buffer size is specified by the nVolumeNameSize parameter.</param>
                /// <param name="nVolumeNameSize">The length of a volume name buffer, in TCHARs. The maximum buffer size is MAX_PATH+1.</param>
                /// <param name="lpVolumeSerialNumber">A pointer to a variable that receives the volume serial number. This parameter can be NULL if the serial number is not required.</param>
                /// <param name="lpMaximumComponentLength">A pointer to a variable that receives the maximum length, in TCHARs, of a file name component that a specified file system supports.</param>
                /// <param name="lpFileSystemFlags">A pointer <see cref="FileSystemFlags"/> flags associated with the specified file system.</param>
                /// <param name="lpFileSystemNameBuffer">A pointer to a buffer that receives the name of the file system, for example, the FAT file system or the NTFS file system.</param>
                /// <param name="nFileSystemNameSize">The length of the file system name buffer, in TCHARs. The maximum buffer size is MAX_PATH+1.</param>
                /// <returns>True if successful.</returns>
                [DllImport(Kernel32Dll, SetLastError = true, CharSet = CharSet.Unicode)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool GetVolumeInformation(string lpRootPathName, StringBuilder lpVolumeNameBuffer, int nVolumeNameSize, out uint lpVolumeSerialNumber, out uint lpMaximumComponentLength, out FileSystemFlags lpFileSystemFlags, StringBuilder lpFileSystemNameBuffer, int nFileSystemNameSize);
            }
        }
    }
}
