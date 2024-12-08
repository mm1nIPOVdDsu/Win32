using System.Runtime.InteropServices;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <inheritdoc/>
        public partial class Kernel32
        {
            /// <summary>
            ///     IoApiSet interactions.
            /// </summary>
            public partial class IoApiSet
            {
                /// <summary>
                ///     Used in conjunction with the IOCTL_STORAGE_QUERY_PROPERTY control code to retrieve the storage device descriptor data for a device.
                /// </summary>
                [StructLayout(LayoutKind.Sequential)]
                public struct STORAGE_DEVICE_DESCRIPTOR
                {
                    /// <summary>
                    ///     Contains the size of this structure, in bytes. The value of this member will change as members are added to the structure.
                    /// </summary>
                    public uint Version;
                    /// <summary>
                    ///     Specifies the total size of the descriptor, in bytes, which may include vendor ID, product ID, product revision, device serial number strings and bus-specific data which are appended to the structure.
                    /// </summary>
                    public uint Size;
                    /// <summary>
                    ///     Specifies the device type as defined by the Small Computer Systems Interface (SCSI) specification.
                    /// </summary>
                    public byte DeviceType;
                    /// <summary>
                    ///     Specifies the device type modifier, if any, as defined by the SCSI specification. If no device type modifier exists, this member is zero.
                    /// </summary>
                    public byte DeviceTypeModifier;
                    /// <summary>
                    ///     Indicates when TRUE that the device's media (if any) is removable. If the device has no media, this member should be ignored. When FALSE the device's media is not removable.
                    /// </summary>
                    [MarshalAs(UnmanagedType.U1)]
                    public bool RemovableMedia;
                    /// <summary>
                    ///     Indicates when TRUE that the device supports multiple outstanding commands (SCSI tagged queuing or equivalent). When FALSE, the device does not support SCSI-tagged queuing or the equivalent.
                    /// </summary>
                    [MarshalAs(UnmanagedType.U1)]
                    public bool CommandQueueing;
                    /// <summary>
                    ///     Specifies the byte offset from the beginning of the structure to a null-terminated ASCII string that contains the device's vendor ID. If the device has no vendor ID, this member is zero.
                    /// </summary>
                    public uint VendorIdOffset;
                    /// <summary>
                    ///     Specifies the byte offset from the beginning of the structure to a null-terminated ASCII string that contains the device's product ID. If the device has no product ID, this member is zero.
                    /// </summary>
                    public uint ProductIdOffset;
                    /// <summary>
                    ///     Specifies the byte offset from the beginning of the structure to a null-terminated ASCII string that contains the device's product revision string. If the device has no product revision string, this member is zero.
                    /// </summary>
                    public uint ProductRevisionOffset;
                    /// <summary>
                    ///     Specifies the byte offset from the beginning of the structure to a null-terminated ASCII string that contains the device's serial number. If the device has no serial number, this member is zero.
                    /// </summary>
                    public uint SerialNumberOffset;
                    /// <summary>
                    ///     Specifies an enumerator value of type STORAGE_BUS_TYPE that indicates the type of bus to which the device is connected. This should be used to interpret the raw device properties at the end of this structure (if any).
                    /// </summary>
                    public STORAGE_BUS_TYPE BusType;
                    /// <summary>
                    ///     Indicates the number of bytes of bus-specific data that have been appended to this descriptor.
                    /// </summary>
                    public uint RawPropertiesLength;
                    /// <summary>
                    ///     Contains an array of length one that serves as a place holder for the first byte of the bus specific property data.
                    /// </summary>
                    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x16)]
                    public byte[] RawDeviceProperties;
                }
                /// <summary>
                ///     Indicates the properties of a storage device or adapter to retrieve as the input buffer passed to the IOCTL_STORAGE_QUERY_PROPERTY control code.
                /// </summary>
                [StructLayout(LayoutKind.Sequential)]
                public struct STORAGE_PROPERTY_QUERY
                {
                    /// <summary>
                    ///     Indicates whether the caller is requesting a device descriptor, an adapter descriptor, a write cache property, a device unique ID (DUID), or the device identifiers provided in the device's SCSI vital product data (VPD) page. For a list of the property IDs that can be assigned to this member, see STORAGE_PROPERTY_ID.
                    /// </summary>
                    public STORAGE_PROPERTY_ID PropertyId;
                    /// <summary>
                    ///     Contains flags indicating the type of query to be performed as enumerated by the STORAGE_QUERY_TYPE enumeration.
                    /// </summary>
                    public STORAGE_QUERY_TYPE QueryType;
                    /// <summary>
                    ///     Contains an array of bytes that can be used to retrieve additional parameters for specific queries.
                    /// </summary>
                    public byte[] AdditionalParameters;
                }
            }
        }
    }
}