using System;
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
            ///     SysInfoApi interactions.
            /// </summary>
            public partial class SysInfoApi
            {
                /// <summary>
                ///     Contains information about the current computer system. This includes the architecture and type of the processor, the number of processors in the system, the page size, and other such information.
                /// </summary>
                [StructLayout(LayoutKind.Sequential)]
                public struct SYSTEM_INFO 
                {
                    /// <summary>
                    ///     The processor architecture of the installed operating system.
                    /// </summary>
                    public PROCESSOR_ARCHITECTURE wProcessorArchitecture;
                    /// <summary>
                    ///     This member is reserved for future use.
                    /// </summary>
                    public ushort wReserved;
                    /// <summary>
                    ///     The page size and the granularity of page protection and commitment. This is the page size used by the VirtualAlloc function.
                    /// </summary>
                    public uint dwPageSize;
                    /// <summary>
                    ///     A pointer to the lowest memory address accessible to applications and dynamic-link libraries (DLLs).
                    /// </summary>
                    public IntPtr lpMinimumApplicationAddress;
                    /// <summary>
                    ///     A pointer to the highest memory address accessible to applications and DLLs.
                    /// </summary>
                    public IntPtr lpMaximumApplicationAddress;
                    /// <summary>
                    ///     A mask representing the set of processors configured into the system. Bit 0 is processor 0; bit 31 is processor 31.
                    /// </summary>
                    public UIntPtr dwActiveProcessorMask;
                    /// <summary>
                    ///     The number of logical processors in the current group. To retrieve this value, use the GetLogicalProcessorInformation function.
                    /// </summary>
                    public uint dwNumberOfProcessors;
                    /// <summary>
                    ///     An obsolete member that is retained for compatibility. Use the wProcessorArchitecture, wProcessorLevel, and wProcessorRevision members to determine the type of processor.
                    /// </summary>
                    public uint dwProcessorType;
                    /// <summary>
                    ///     The granularity for the starting address at which virtual memory can be allocated. For more information, see VirtualAlloc.
                    /// </summary>
                    public uint dwAllocationGranularity;
                    /// <summary>
                    ///     The architecture-dependent processor level. It should be used only for display purposes. 
                    /// </summary>
                    public ushort wProcessorLevel;
                    /// <summary>
                    ///     The architecture-dependent processor revision.
                    /// </summary>
                    public ushort wProcessorRevision;
                };
                [StructLayout(LayoutKind.Sequential)]
                public struct SMBIOS_HEADER
                {
                    public byte Type;
                    public byte Length;
                    public UInt16 Handle;
                }
                [StructLayout(LayoutKind.Sequential)]
                public struct SMBIOS_SYSTEM_INFORMATION
                {
                    public SMBIOS_HEADER Header;
                    public byte Manufacturer;
                    public byte ProductName;
                    public byte Version;
                    public byte SN;
                    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] // NOTE: No idea if this will work
                    public byte[] UUID;
                    public byte WakeUpType;
                    public byte SKUNumber;
                    public byte Family;
                }

                /// <summary>
                ///     Contents of the raw SMBIOS firmware table.
                /// </summary>
                // https://learn.microsoft.com/en-us/dotnet/standard/native-interop/best-practices
                //[StructLayout(LayoutKind.Explicit, Pack = 0)]
                [StructLayout(LayoutKind.Sequential)]
                public struct RawSMBIOSData
                {
                    //[FieldOffset(0)]
                    [MarshalAs(UnmanagedType.I1)]
                    public Byte Used20CallingMethod;
                    //[FieldOffset(1)]
                    [MarshalAs(UnmanagedType.I1)]
                    public Byte SMBIOSMajorVersion;
                    //[FieldOffset(2)]
                    [MarshalAs(UnmanagedType.I1)]
                    public Byte SMBIOSMinorVersion;
                    //[FieldOffset(3)]
                    [MarshalAs(UnmanagedType.I1)]
                    public Byte DmiRevision;
                    //[FieldOffset(4)]
                    public uint Length;
                    //[MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)]
                    //public byte[] SMBIOSTableData;
                    //[MarshalAs(UnmanagedType.LPArray, UnmanagedType.ByValArray)]
                    //[FieldOffset(7)]
                    public IntPtr SMBIOSTableData;
                }

                public class AcpiTable
                {
                    const Int32 AcpiTableHeaderLength = 36;

                    public AcpiTable(byte[] data)
                    {
                        if (data.Length < AcpiTableHeaderLength)
                            throw new ArgumentException("Invalid ACPI data.");
                        
                    }

                    public byte[] RawData { get; private set; }

                    public string Signature { get; private set; }
                    public uint Length { get; private set; }
                    public byte Revision { get; private set; }
                    public byte Checksum { get; private set; }
                    public bool ChecksumIsValid { get; private set; }
                    public string OemId { get; private set; }
                    public string OemTableId { get; private set; }
                    public uint OemRevision { get; private set; }
                    public string CreatorId { get; private set; }
                    public uint CreatorRevision { get; private set; }
                    public byte[] Payload { get; private set; }

                    public Byte GetPayloadByte(Int32 index)
                    {
                        return this.Payload[index];
                    }

                    public UInt16 GetPayloadUInt16(Int32 index)
                    {
                        return BitConverter.ToUInt16(this.Payload, index);
                    }

                    public UInt32 GetPayloadUInt32(Int32 index)
                    {
                        return BitConverter.ToUInt32(this.Payload, index);
                    }

                    public UInt64 GetPayloadUInt64(Int32 index)
                    {
                        return BitConverter.ToUInt64(this.Payload, index);
                    }

                    public String GetPayloadString(Int32 index, Int32 length)
                    {
                        return Encoding.ASCII.GetString(this.Payload, index, length);
                    }

                    private static Boolean ValidateChecksum(Byte[] data)
                    {
                        Byte sum = 0;

                        for (var i = 0; i < data.Length; i++)
                        {
                            sum += data[i];
                        }

                        return 0 == sum;
                    }
                }
            }
        }
    }
}
