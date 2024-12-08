using System;

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
            /// <summary>
            ///     The valid partition types that are used by disk drivers.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/fileio/disk-partition-types">PARTITION_TYPE</see>
            public enum PARTITION_TYPE : byte
            {
                /// <summary>
                ///     An unused entry partition.
                /// </summary>
                PARTITION_ENTRY_UNUSED = 0x00,
                /// <summary>
                ///     An extended partition.
                /// </summary>
                PARTITION_EXTENDED = 0x05,
                /// <summary>
                ///     A FAT12 file system partition.
                /// </summary>
                PARTITION_FAT_12 = 0x01,
                /// <summary>
                ///     A FAT16 file system partition.
                /// </summary>
                PARTITION_FAT_16 = 0x04,
                /// <summary>
                ///     A FAT32 file system partition.
                /// </summary>
                PARTITION_FAT32 = 0x0B,
                /// <summary>
                ///     An IFS partition.
                /// </summary>
                PARTITION_IFS = 0x07,
                /// <summary>
                ///     A logical disk manager (LDM) partition.
                /// </summary>
                PARTITION_LDM = 0x42,
                /// <summary>
                ///     An NTFT partition.
                /// </summary>
                PARTITION_NTFT = 0x80,
                /// <summary>
                ///     A valid NTFT partition.
                /// </summary>
                /// <remarks>The high bit of a partition type code indicates that a partition is part of an NTFT mirror or striped array.</remarks>
                VALID_NTFT = 0xC0,
            }
            /// <summary>
            ///     Represents the format of a partition.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winioctl/ne-winioctl-partition_style">PARTITION_STYLE</see>
            public enum PARTITION_STYLE
            {
                /// <summary>
                ///     Master boot record (MBR) format. This corresponds to standard AT-style MBR partitions.
                /// </summary>
                PARTITION_STYLE_MBR = 0,
                /// <summary>
                ///     GUID Partition Table (GPT) format.
                /// </summary>
                PARTITION_STYLE_GPT = 1,
                /// <summary>
                ///     Partition not formatted in either of the recognized formats—MBR or GPT.
                /// </summary>
                PARTITION_STYLE_RAW = 2,
            }
            /// <summary>
            ///     The Extensible Firmware Interface (EFI) attributes of a partition.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winioctl/ns-winioctl-partition_information_gpt">GPT_ATTRIBUTES</see>
            [Flags]
            public enum GPT_ATTRIBUTES : ulong
            {
                /// <summary>
                ///     If this attribute is set, the partition is required by a computer to function properly.
                /// </summary>
                GPT_ATTRIBUTE_PLATFORM_REQUIRED = 0x0000000000000001,
                /// <summary>
                ///     If this attribute is set, the partition does not receive a drive letter by default when the disk is moved to another computer
                ///     or when the disk is seen for the first time by a computer.
                /// </summary>
                GPT_BASIC_DATA_ATTRIBUTE_NO_DRIVE_LETTER = 0x8000000000000000,
                /// <summary>
                ///     If this attribute is set, the partition is not detected by the Mount Manager.
                /// </summary>
                GPT_BASIC_DATA_ATTRIBUTE_HIDDEN = 0x4000000000000000,
                /// <summary>
                ///     If this attribute is set, the partition is a shadow copy of another partition. VSS uses this attribute. This attribute is an
                ///     indication for file system filter driver-based software (such as antivirus programs) to avoid attaching to the volume.
                /// </summary>
                GPT_BASIC_DATA_ATTRIBUTE_SHADOW_COPY = 0x2000000000000000,
                /// <summary>
                ///     If this attribute is set, the partition is read-only.
                /// </summary>
                GPT_BASIC_DATA_ATTRIBUTE_READ_ONLY = 0x1000000000000000
            }
        }
    }
}
