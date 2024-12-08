using System;
using System.Runtime.InteropServices;

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
            ///     Contains extended information about a drive's partitions.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winioctl/ns-winioctl-drive_layout_information_ex">DRIVE_LAYOUT_INFORMATION_EX</see>
            [StructLayout(LayoutKind.Sequential)]
            public struct DRIVE_LAYOUT_INFORMATION_EX
            {
                /// <summary>
                ///     The style of the partitions on the drive enumerated by the <see cref="PARTITION_STYLE"/> enumeration.
                /// </summary>
                public PARTITION_STYLE PartitionStyle;
                /// <summary>
                ///     The number of partitions on the drive. On hard disks with the MBR layout, this value will always be a multiple of 4. Any partitions that are actually unused will have a partition type of PARTITION_ENTRY_UNUSED (0) set in the PartitionType member of the PARTITION_INFORMATION_MBR structure of the Mbr member of the <see cref="PARTITION_INFORMATION_EX"/> structure of the PartitionEntry member of this structure.
                /// </summary>
                public int PartitionCount;
                /// <summary>
                ///     <see cref="DRIVE_LAYOUT_INFORMATION_UNION"/>.
                /// </summary>
                public DRIVE_LAYOUT_INFORMATION_UNION DriveLayoutInformaiton;
                /// <summary>
                ///     A variable-sized array of <see cref="PARTITION_INFORMATION_EX"/> structures, one structure for each partition on the drive.
                /// </summary>
                [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 0x16)]
                public PARTITION_INFORMATION_EX[] PartitionEntry;
            }
            /// <summary>
            ///     Contains information about a drive's GUID partition table (GPT) partitions.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winioctl/ns-winioctl-drive_layout_information_gpt">DRIVE_LAYOUT_INFORMATION_GPT</see>
            [StructLayout(LayoutKind.Sequential)]
            public struct DRIVE_LAYOUT_INFORMATION_GPT
            {
                /// <summary>
                ///     The GUID of the disk.
                /// </summary>
                public Guid DiskId;
                /// <summary>
                ///     The starting byte offset of the first usable block.
                /// </summary>
                public Int64 StartingUsableOffset;
                /// <summary>
                ///     The size of the usable blocks on the disk, in bytes.
                /// </summary>
                public Int64 UsableLength;
                /// <summary>
                ///     The maximum number of partitions that can be defined in the usable block.
                /// </summary>
                public ulong MaxPartitionCount;
            }
            /// <summary>
            ///     Provides information about a drive's master boot record (MBR) partitions.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winioctl/ns-winioctl-drive_layout_information_mbr">DRIVE_LAYOUT_INFORMATION_MBR</see>
            [StructLayout(LayoutKind.Sequential)]
            public struct DRIVE_LAYOUT_INFORMATION_MBR
            {
                /// <summary>
                ///     The signature of the drive.
                /// </summary>
                public Int32 Signature;
            }
            /// <summary>
            ///     A union used by other Drive Layout structures.
            /// </summary>
            [StructLayout(LayoutKind.Explicit)]
            public struct DRIVE_LAYOUT_INFORMATION_UNION
            {
                /// <summary>
                ///     A <see cref="DRIVE_LAYOUT_INFORMATION_MBR"/> structure containing information about the master boot record type partitioning on the drive.
                /// </summary>
                [FieldOffset(0)]
                public DRIVE_LAYOUT_INFORMATION_MBR Mbr;
                /// <summary>
                ///     A <see cref="DRIVE_LAYOUT_INFORMATION_GPT"/> structure containing information about the GUID disk partition type partitioning on the drive.
                /// </summary>
                [FieldOffset(0)]
                public DRIVE_LAYOUT_INFORMATION_GPT Gpt;
            }
            /// <summary>
            ///     Contains information about a disk partition.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winioctl/ns-winioctl-partition_information">PARTITION_INFORMATION</see>
            [StructLayout(LayoutKind.Sequential)]
            public struct PARTITION_INFORMATION
            {
                /// <summary>
                ///     The starting offset of the partition.
                /// </summary>
                public long StartingOffset;
                /// <summary>
                ///     The length of the partition, in bytes.
                /// </summary>
                public long PartitionLength;
                /// <summary>
                ///     The number of hidden sectors in the partition.
                /// </summary>
                public int HiddenSectors;
                /// <summary>
                ///     The number of the partition (1-based).
                /// </summary>
                public int PartitionNumber;
                /// <summary>
                ///     The type of partition. For a list of values, see Disk Partition Types.
                /// </summary>
                public byte PartitionType;
                /// <summary>
                ///     If this member is TRUE, the partition is bootable.
                /// </summary>
                [MarshalAs(UnmanagedType.I1)]
                public bool BootIndicator;
                /// <summary>
                ///     If this member is TRUE, the partition is of a recognized type.
                /// </summary>
                [MarshalAs(UnmanagedType.I1)]
                public bool RecognizedPartition;
                /// <summary>
                ///     If this member is TRUE, the partition information has changed.
                /// </summary>
                [MarshalAs(UnmanagedType.I1)]
                public bool RewritePartition;
            }
            /// <summary>
            ///     Contains partition information for standard AT-style master boot record (MBR) and Extensible Firmware Interface (EFI) disks.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winioctl/ns-winioctl-partition_information_ex">PARTITION_INFORMATION_EX</see>
            [StructLayout(LayoutKind.Sequential)]
            public struct PARTITION_INFORMATION_EX
            {
                /// <summary>
                ///     The format of the partition. For a list of values, <see cref="PARTITION_STYLE"/>.
                /// </summary>
                public PARTITION_STYLE PartitionStyle;
                /// <summary>
                ///     The starting offset of the partition.
                /// </summary>
                public long StartingOffset;
                /// <summary>
                ///     The size of the partition, in bytes.
                /// </summary>
                public long PartitionLength;
                /// <summary>
                ///     The number of the partition (1-based).
                /// </summary>
                public int PartitionNumber;
                /// <summary>
                /// If this member is TRUE, the partition is rewritable. The value of this parameter should be set to TRUE.
                /// </summary>
                public bool RewritePartition;
                /// <summary>
                ///     <see cref="PARTITION_INFORMATION_UNION"/>.
                /// </summary>
                public PARTITION_INFORMATION_UNION DriveLayoutInformaiton;
            }
            /// <summary>
            ///     Contains GUID partition table (GPT) partition information.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winioctl/ns-winioctl-partition_information_gpt">PARTITION_INFORMATION_GPT</see>
            [StructLayout(LayoutKind.Sequential)]
            public struct PARTITION_INFORMATION_GPT
            {
                /// <summary>
                ///     A GUID that identifies the partition type. Must be ebd0a0a2-b9e5-4433-87c0-68b6b72699c7.
                /// </summary>
                /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winioctl/ns-winioctl-partition_information_gpt#members"/>
                public Guid PartitionType;
                /// <summary>
                ///     The GUID of the partition.
                /// </summary>
                public Guid PartitionId;
                /// <summary>
                ///     The Extensible Firmware Interface (EFI) attributes
                /// </summary>
                public GPT_ATTRIBUTES Attributes;
                /// <summary>
                ///     A wide-character string that describes the partition.
                /// </summary>
                public char[] Name;
            }
            /// <summary>
            ///     Contains partition information specific to master boot record (MBR) disks.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/winioctl/ns-winioctl-partition_information_mbr">PARTITION_INFORMATION_MBR</see>
            [StructLayout(LayoutKind.Sequential)]
            public struct PARTITION_INFORMATION_MBR
            {
                /// <summary>
                ///     The type of partition.
                /// </summary>
                /// <remarks>For a list of all values, see <see href="https://learn.microsoft.com/en-us/windows/win32/fileio/disk-partition-types">Disk Parition Types</see>.</remarks>
                public PARTITION_TYPE PartitionType;
                /// <summary>
                ///     If the member is TRUE, the partition is a boot partition.
                /// </summary>
                public bool BootIndicator;
                /// <summary>
                ///     If this member is TRUE, the partition is of a recognized type
                /// </summary>
                public bool RecognizedPartition;
                /// <summary>
                ///     The number of hidden sectors to be allocated when the partition table is created.
                /// </summary>
                public Int32 HiddenSectors;
            }
            /// <summary>
            ///     A union used by other Drive Layout structures.
            /// </summary>
            [StructLayout(LayoutKind.Explicit)]
            public struct PARTITION_INFORMATION_UNION
            {
                /// <summary>
                ///     Information about a disk partition.
                /// </summary>
                [FieldOffset(0)]
                public PARTITION_INFORMATION_MBR Mbr;
                /// <summary>
                ///     GUID partition table (GPT) partition information.
                /// </summary>
                [FieldOffset(8)]
                public PARTITION_INFORMATION_GPT Gpt;
            }
        }
    }
}
