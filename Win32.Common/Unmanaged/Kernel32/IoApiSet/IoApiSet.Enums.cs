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
                ///     Specifies the various types of storage buses.
                /// </summary>
                public enum STORAGE_BUS_TYPE
                {
                    /// <summary>
                    ///     Unknown bus type.
                    /// </summary>
                    BusTypeUnknown = 0x00,
                    /// <summary>
                    ///     SCSI bus.
                    /// </summary>
                    BusTypeScsi = 0x1,
                    /// <summary>
                    ///     ATAPI bus.
                    /// </summary>
                    BusTypeAtapi = 0x2,
                    /// <summary>
                    ///     ATA bus.
                    /// </summary>
                    BusTypeAta = 0x3,
                    /// <summary>
                    ///     IEEE-1394 bus.
                    /// </summary>
                    BusType1394 = 0x4,
                    /// <summary>
                    ///     SSA bus.
                    /// </summary>
                    BusTypeSsa = 0x5,
                    /// <summary>
                    ///     Fibre Channel bus.
                    /// </summary>
                    BusTypeFibre = 0x6,
                    /// <summary>
                    ///     USB bus.
                    /// </summary>
                    BusTypeUsb = 0x7,
                    /// <summary>
                    ///     RAID bus.
                    /// </summary>
                    BusTypeRAID = 0x8,
                    /// <summary>
                    ///     iSCSI bus.
                    /// </summary>
                    BusTypeiScsi = 0x9,
                    /// <summary>
                    ///     Serial Attached SCSI (SAS) bus.
                    /// </summary>
                    BusTypeSas = 0xA,
                    /// <summary>
                    ///     SATA bus.
                    /// </summary>
                    BusTypeSata = 0xB,
                    /// <summary>
                    ///     SD bus.
                    /// </summary>
                    BusTypeSd = 0xC,
                    /// <summary>
                    ///     MMC bus.
                    /// </summary>
                    BusTypeMmc = 0xD,
                    /// <summary>
                    ///     Virtual bus.
                    /// </summary>
                    BusTypeVirtual = 0xE,
                    /// <summary>
                    ///     File backed virtual bus.
                    /// </summary>
                    BusTypeFileBackedVirtual = 0xF,
                    /// <summary>
                    ///     Spaces bus.
                    /// </summary>
                    BusTypeSpaces = 0x10,
                    /// <summary>
                    ///     NVME bus.
                    /// </summary>
                    BusTypeNvme = 0x11,
                    /// <summary>
                    ///     SCM bus.
                    /// </summary>
                    BusTypeSCM = 0x12,
                    /// <summary>
                    ///     UFS bus.
                    /// </summary>
                    BusTypeUfs = 0x13,
                    /// <summary>
                    ///     Max bus.
                    /// </summary>
                    BusTypeMax = 0x14,
                    /// <summary>
                    ///     Reserved for future use.
                    /// </summary>
                    BusTypeMaxReserved = 0x7F
                }
                /// <summary>
                ///     Enumerates the possible values of the PropertyId member of the STORAGE_PROPERTY_QUERY structure passed as input to the IOCTL_STORAGE_QUERY_PROPERTY request to retrieve the properties of a storage device or adapter.
                /// </summary>
                public enum STORAGE_PROPERTY_ID
                {
                    /// <summary>
                    ///     Indicates that the caller is querying for the device descriptor, STORAGE_DEVICE_DESCRIPTOR.
                    /// </summary>
                    StorageDeviceProperty = 0,
                    /// <summary>
                    ///     Indicates that the caller is querying for the adapter descriptor, STORAGE_ADAPTER_DESCRIPTOR.
                    /// </summary>
                    StorageAdapterProperty = 1,
                    /// <summary>
                    ///     Indicates that the caller is querying for the device identifiers provided with the SCSI vital product data pages. Data is returned using the STORAGE_DEVICE_ID_DESCRIPTOR structure.
                    /// </summary>
                    StorageDeviceIdProperty = 2,
                    /// <summary>
                    ///     Intended for driver usage. Indicates that the caller is querying for the unique device identifiers. Data is returned using the STORAGE_DEVICE_UNIQUE_IDENTIFIER structure (see the storduid.h header in the DDK).
                    /// </summary>
                    StorageDeviceUniqueIdProperty = 3,
                    /// <summary>
                    ///     Indicates that the caller is querying for the write cache property. Data is returned using the STORAGE_WRITE_CACHE_PROPERTY structure.
                    /// </summary>
                    StorageDeviceWriteCacheProperty = 4,
                    /// <summary>
                    ///     Reserved for system use.
                    /// </summary>
                    StorageMiniportProperty = 5,
                    /// <summary>
                    ///     Indicates that the caller is querying for the access alignment descriptor, STORAGE_ACCESS_ALIGNMENT_DESCRIPTOR.
                    /// </summary>
                    StorageAccessAlignmentProperty = 6,
                    /// <summary>
                    ///     Indicates that the caller is querying for the seek penalty descriptor, DEVICE_SEEK_PENALTY_DESCRIPTOR.
                    /// </summary>
                    StorageDeviceSeekPenaltyProperty = 7,
                    /// <summary>
                    ///     Indicates that the caller is querying for the trim descriptor, DEVICE_TRIM_DESCRIPTOR.
                    /// </summary>
                    StorageDeviceTrimProperty = 8,
                    /// <summary>
                    ///     Reserved for system use.
                    /// </summary>
                    StorageDeviceWriteAggregationProperty = 9,
                    /// <summary>
                    ///     Reserved for system use.
                    /// </summary>
                    StorageDeviceDeviceTelemetryProperty = 10, // 0xA
                    /// <summary>
                    ///     Indicates that the caller is querying for the logical block provisioning property. Data is returned using the DEVICE_LB_PROVISIONING_DESCRIPTOR structure.
                    /// </summary>
                    StorageDeviceLBProvisioningProperty = 11, // 0xB
                    /// <summary>
                    ///     Indicates that the caller is querying for the device power descriptor. Data is returned using the DEVICE_POWER_DESCRIPTOR structure.
                    /// </summary>
                    StorageDevicePowerProperty = 12, // 0xC
                    /// <summary>
                    ///     Indicates that the caller is querying for the copy offload parameters property. Data is returned using the DEVICE_COPY_OFFLOAD_DESCRIPTOR structure.
                    /// </summary>
                    StorageDeviceCopyOffloadProperty = 13, // 0xD
                    /// <summary>
                    ///     Reserved for system use.
                    /// </summary>
                    StorageDeviceResiliencyProperty = 14, // 0xE
                    /// <summary>
                    ///     Indicates that the caller is querying for the medium product type. Data is returned using the STORAGE_MEDIUM_PRODUCT_TYPE_DESCRIPTOR structure.
                    /// </summary>
                    StorageDeviceMediumProductType = 15,
                    /// <summary>
                    ///     Indicates that the caller is querying for RPMB support and properties. Data is returned using the STORAGE_RPMB_DESCRIPTOR structure.
                    /// </summary>
                    StorageAdapterRpmbProperty = 16,
                    /// <summary>
                    ///     Provides info on the storage adapter encryption capabilities. This is currently supported on UFS (Universal Flash Storage) adapters.
                    /// </summary>
                    StorageAdapterCryptoProperty = 17,
                    /// <summary>
                    ///     Indicates that the caller is querying for the device I/O capability property. Data is returned using the DEVICE_IO_CAPABILITY_DESCRIPTOR structure.
                    /// </summary>
                    StorageDeviceIoCapabilityProperty = 48,
                    /// <summary>
                    ///     Indicates that the caller is querying for protocol-specific data from the adapter. Data is returned using the STORAGE_PROTOCOL_DATA_DESCRIPTOR structure. See the remarks for more info.
                    /// </summary>
                    StorageAdapterProtocolSpecificProperty = 49,
                    /// <summary>
                    ///     Indicates that the caller is querying for protocol-specific data from the device. Data is returned using the STORAGE_PROTOCOL_DATA_DESCRIPTOR structure. See the remarks for more info.
                    /// </summary>
                    StorageDeviceProtocolSpecificProperty = 50,
                    /// <summary>
                    ///     Indicates that the caller is querying temperature data from the adapter. Data is returned using the STORAGE_TEMPERATURE_DATA_DESCRIPTOR structure.
                    /// </summary>
                    StorageAdapterTemperatureProperty = 51,
                    /// <summary>
                    ///     Indicates that the caller is querying for temperature data from the device. Data is returned using the STORAGE_TEMPERATURE_DATA_DESCRIPTOR structure.
                    /// </summary>
                    StorageDeviceTemperatureProperty = 52,
                    /// <summary>
                    ///     Indicates that the caller is querying for topology information from the adapter. Data is returned using the STORAGE_PHYSICAL_TOPOLOGY_DESCRIPTOR structure.
                    /// </summary>
                    StorageAdapterPhysicalTopologyProperty = 53,
                    /// <summary>
                    ///     Indicates that the caller is querying for topology information from the device. Data is returned using the STORAGE_PHYSICAL_TOPOLOGY_DESCRIPTOR structure.
                    /// </summary>
                    StorageDevicePhysicalTopologyProperty = 54,
                    /// <summary>
                    ///     Reserved for future use.
                    /// </summary>
                    StorageDeviceAttributesProperty = 55,
                    /// <summary>
                    ///     Provides health information about the storage device (specifically for the persistent memory stack).
                    /// </summary>
                    StorageDeviceManagementStatus = 56,
                    /// <summary>
                    ///     Indicates that the caller is querying for the adapter serial number. Data is returned using the STORAGE_ADAPTER_SERIAL_NUMBER structure.
                    /// </summary>
                    StorageAdapterSerialNumberProperty = 57,
                    /// <summary>
                    ///     Reserved for system use.
                    /// </summary>
                    StorageDeviceLocationProperty = 58,
                    /// <summary>
                    ///     Provides the non-uniform memory access (NUMA) node of the storage device.
                    /// </summary>
                    StorageDeviceNumaProperty = 59,
                    /// <summary>
                    ///     Reserved for system use.
                    /// </summary>
                    StorageDeviceZonedDeviceProperty = 60,
                    /// <summary>
                    ///     Provides the unsafe shutdown count value used to determine if the device data might have been lost during a power loss event (specifically for the persistent memory stack).
                    /// </summary>
                    StorageDeviceUnsafeShutdownCount = 61,
                    /// <summary>
                    ///     Provides info on how many bytes have been read/write from a solid-state drive (SSD). This property is supported only for Non-Volatile Memory Express (NVMe) devices that implement a certain NVMe feature.
                    /// </summary>
                    StorageDeviceEnduranceProperty = 62,
                    /// <summary>
                    ///     Provides info on the state of the LED associated with a storage device. This is a server-oriented feature.
                    /// </summary>
                    StorageDeviceLedStateProperty = 63,
                    /// <summary>
                    ///     Reserved for system use.
                    /// </summary>
                    StorageDeviceSelfEncryptionProperty = 64,
                    /// <summary>
                    ///     Provides identification info for a storage device that can be physically replaced with a Field Replacement Unit (FRU).
                    /// </summary>
                    StorageFruIdProperty = 65
                }
                /// <summary>
                ///     Used by the STORAGE_PROPERTY_QUERY structure passed to the IOCTL_STORAGE_QUERY_PROPERTY control code to indicate what information is returned about a property of a storage device or adapter.
                /// </summary>
                public enum STORAGE_QUERY_TYPE
                {
                    /// <summary>
                    ///     Instructs the driver to return an appropriate descriptor.
                    /// </summary>
                    PropertyStandardQuery = 0,
                    /// <summary>
                    ///     Instructs the driver to report whether the descriptor is supported.
                    /// </summary>
                    PropertyExistsQuery = 1,
                    /// <summary>
                    ///     Used to retrieve a mask of writeable fields in the descriptor. Not currently supported. Do not use.
                    /// </summary>
                    PropertyMaskQuery = 2,
                    /// <summary>
                    ///     Specifies the upper limit of the list of query types. This is used to validate the query type.
                    /// </summary>
                    PropertyQueryMaxDefined = 3
                }
            }
        }
    }
}