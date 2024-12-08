using System;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <summary>
        ///     Header is used by Wireless Networks.
        /// </summary>
        public partial class WLanApi
        {
            /// <summary>
            ///     The DOT11_BSSID_LIST structure contains a list of basic service set (BSS) identifiers.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/nativewifi/dot11-bssid-list">DOT11_BSSID_LIST</see>
            public struct DOT11_BSSID_LIST
            {
                /// <summary>
                ///     A list of BSS identifiers. A BSS identifier is stored as a DOT11_MAC_ADDRESS type.
                /// </summary>
                public IntPtr BSSIDs;
                /// <summary>
                ///     An NDIS_OBJECT_HEADER structure that contains the type, version, and, size information of an NDIS structure. For most
                ///     DOT11_BSSID_LIST structures, set the Type member to NDIS_OBJECT_TYPE_DEFAULT, set the Revision member to
                ///     DOT11_BSSID_LIST_REVISION_1, and set the Size member to sizeof(DOT11_BSSID_LIST).
                /// </summary>
                public NDIS_OBJECT_HEADER Header;
                /// <summary>
                ///     The number of entries in this structure.
                /// </summary>
                public ulong uNumOfEntries;
                /// <summary>
                ///     The total number of entries supported.
                /// </summary>
                public ulong uTotalNumOfEntries;
            }
            /// <summary>
            ///     A DOT11_SSID structure contains the SSID of an interface.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/nativewifi/dot11-ssid">DOT11_SSID</see>
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public struct DOT11_SSID
            {
                /// <summary>
                ///     https://learn.microsoft.com/en-us/windows/win32/nativewifi/dot11-ssid
                /// </summary>
                public uint uSSIDLength;
                /// <summary>
                ///     The SSID. DOT11_SSID_MAX_LENGTH is set to 32.
                /// </summary>
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
                public string ucSSID;
            }
            /// <summary>
            ///     The NDIS_OBJECT_HEADER structure packages the object type, version, and size information that is required in many NDIS 6.0 structures.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/objectheader/ns-objectheader-ndis_object_header">NDIS_OBJECT_HEADER</see>
            public struct NDIS_OBJECT_HEADER
            {
                /// <summary>
                ///     The revision number of the structure. Every NDIS structure that has an NDIS_OBJECT_HEADER member has a revision number that
                ///     applies to the NDIS structure exclusively. This allows NDIS drivers to support multiple versions of the same structure. For
                ///     example, a driver can check the Revision member value at run time and use the appropriate version of the structure.
                /// </summary>
                public byte Revision;
                /// <summary>
                ///     The total size, in bytes, of the NDIS object structure that includes the NDIS_OBJECT_HEADER member. This size includes the
                ///     size of the NDIS_OBJECT_HEADER member and the other members of the structure.
                /// </summary>
                public ushort Size;
                /// <summary>
                ///     The type of NDIS object that a structure describes. Use this member to identify the type of structure in a memory dump.
                /// </summary>
                public byte Type;
            }
            /// <summary>
            ///     The WLAN_ASSOCIATION_ATTRIBUTES structure contains association attributes for a connection.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/ns-wlanapi-wlan_interface_info_list">WLAN_INTERFACE_INFO_LIST</see>
            [StructLayout(LayoutKind.Sequential)]
            public struct WLAN_ASSOCIATION_ATTRIBUTES
            {
                /// <summary>
                ///     A DOT11_SSID structure that contains the SSID of the association.
                /// </summary>
                public DOT11_SSID dot11Ssid;
                /// <summary>
                ///     A DOT11_BSS_TYPE value that specifies whether the network is infrastructure or ad hoc.
                /// </summary>
                public DOT11_BSS_TYPE dot11BssType;
                /// <summary>
                ///     A DOT11_MAC_ADDRESS that contains the BSSID of the association.
                /// </summary>
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
                public byte[] dot11Bssid;
                /// <summary>
                ///     A DOT11_PHY_TYPE value that indicates the physical type of the association.
                /// </summary>
                public DOT11_PHY_TYPE dot11PhyType;
                /// <summary>
                ///     The position of the DOT11_PHY_TYPE value in the structure containing the list of PHY types.
                /// </summary>
                public DOT11_PHY_TYPE dot11PhyIndex;
                /// <summary>
                ///     A percentage value that represents the signal quality of the network. WLAN_SIGNAL_QUALITY is of type ULONG. This member
                ///     contains a value between 0 and 100. A value of 0 implies an actual RSSI signal strength of -100 dbm. A value of 100 implies an
                ///     actual RSSI signal strength of -50 dbm. You can calculate the RSSI signal strength value for wlanSignalQuality values between
                ///     1 and 99 using linear interpolation.
                /// </summary>
                public uint wlanSignalQuality;
                /// <summary>
                ///     Contains the receiving rate of the association.
                /// </summary>
                public uint rxRate;
                /// <summary>
                ///     Contains the transmission rate of the association.
                /// </summary>
                public uint txRate;

                /// <summary>
                ///     Gets the BSSID of the associated access point.
                /// </summary>
                /// <value>The BSSID.</value>
                public PhysicalAddress Dot11Bssid
                {
                    get { return new PhysicalAddress(dot11Bssid); }
                }
            }
            /// <summary>
            ///     The WLAN_AVAILABLE_NETWORK structure contains information about an available wireless network.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/ns-wlanapi-wlan_available_network">WLAN_AVAILABLE_NETWORK</see>
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            public struct WLAN_AVAILABLE_NETWORK
            {
                /// <summary>
                ///     Contains the profile name associated with the network. If the network does not have a profile, this member will be empty. If
                ///     multiple profiles are associated with the network, there will be multiple entries with the same SSID in the visible network
                ///     list. Profile names are case-sensitive. This string must be NULL-terminated.
                /// </summary>
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
                public string strProfileName;
                /// <summary>
                ///     A <see cref="DOT11_SSID"/> structure that contains the SSID of the visible wireless network.
                /// </summary>
                public DOT11_SSID dot11Ssid;
                /// <summary>
                ///     A <see cref="DOT11_BSS_TYPE"/> value that specifies whether the network is infrastructure or ad hoc.
                /// </summary>
                public DOT11_BSS_TYPE dot11BssType;
                /// <summary>
                ///     Indicates the number of BSSIDs in the network.
                /// </summary>
                public uint uNumberOfBssids;
                /// <summary>
                ///     Indicates whether the network is connectable or not. If set to TRUE, the network is connectable, otherwise the network cannot
                ///     be connected to.
                /// </summary>
                public bool bNetworkConnectable;
                /// <summary>
                ///     A WLAN_REASON_CODE value that indicates why a network cannot be connected to. This member is only valid when
                ///     bNetworkConnectable is FALSE.
                /// </summary>
                public uint wlanNotConnectableReason;
                /// <summary>
                ///     The number of PHY types supported on available networks. The maximum value of uNumberOfPhyTypes is WLAN_MAX_PHY_TYPE_NUMBER,
                ///     which has a value of 8. If more than WLAN_MAX_PHY_TYPE_NUMBER PHY types are supported, bMorePhyTypes must be set to TRUE.
                /// </summary>
                public uint uNumberOfPhyTypes;
                /// <summary>
                ///     Contains an array of <see cref="DOT11_PHY_TYPE"/> values that represent the PHY types supported by the available networks.
                ///     When uNumberOfPhyTypes is greater than WLAN_MAX_PHY_TYPE_NUMBER, this array contains only the first WLAN_MAX_PHY_TYPE_NUMBER
                ///     PHY types.
                /// </summary>
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
                public DOT11_PHY_TYPE[] dot11PhyTypes;
                /// <summary>
                ///     Specifies if there are more than WLAN_MAX_PHY_TYPE_NUMBER PHY types supported.
                ///
                ///     When this member is set to TRUE, an application must call WlanGetNetworkBssList to get the complete list of PHY types. The
                ///     returned WLAN_BSS_LIST structure has an array of WLAN_BSS_ENTRY structures. The uPhyId member of the WLAN_BSS_ENTRY structure
                ///     contains the PHY type for an entry.
                /// </summary>
                public bool bMorePhyTypes;
                /// <summary>
                ///     A percentage value that represents the signal quality of the network. WLAN_SIGNAL_QUALITY is of type ULONG. This member
                ///     contains a value between 0 and 100. A value of 0 implies an actual RSSI signal strength of -100 dbm. A value of 100 implies an
                ///     actual RSSI signal strength of -50 dbm. You can calculate the RSSI signal strength value for wlanSignalQuality values between
                ///     1 and 99 using linear interpolation.
                /// </summary>
                public uint wlanSignalQuality;
                /// <summary>
                ///     Indicates whether security is enabled on the network. A value of TRUE indicates that security is enabled, otherwise it is not.
                /// </summary>
                public bool bSecurityEnabled;
                /// <summary>
                ///     A <see cref="DOT11_AUTH_ALGORITHM"/> value that indicates the default authentication algorithm used to join this network for
                ///     the first time.
                /// </summary>
                public DOT11_AUTH_ALGORITHM dot11DefaultAuthAlgorithm;
                /// <summary>
                ///     A <see cref="DOT11_CIPHER_ALGORITHM"/> value that indicates the default cipher algorithm to be used when joining this network.
                /// </summary>
                public DOT11_CIPHER_ALGORITHM dot11DefaultCipherAlgorithm;
                /// <summary>
                ///     Contains various flags for the network.
                /// </summary>
                public uint dwFlags;
                /// <summary>
                ///     Reserved for future use. Must be set to NULL.
                /// </summary>
                public uint dwReserved;
            }
            /// <summary>
            ///     The WLAN_AVAILABLE_NETWORK_LIST structure contains an array of information about available networks.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/ns-wlanapi-wlan_available_network_list">WLAN_AVAILABLE_NETWORK_LIST</see>
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            public struct WLAN_AVAILABLE_NETWORK_LIST
            {
                /// <summary>
                ///     Contains the number of items in the Network member.
                /// </summary>
                internal uint dwNumberOfItems;
                /// <summary>
                ///     The index of the current item. The index of the first item is 0. dwIndex must be less than dwNumberOfItems.
                /// </summary>
                internal uint dwIndex;
                /// <summary>
                ///     An array of WLAN_AVAILABLE_NETWORK structures containing interface information.
                /// </summary>
                internal WLAN_AVAILABLE_NETWORK[] wlanAvailableNetwork;

                /// <summary>
                /// </summary>
                /// <param name="ppAvailableNetworkList"></param>
                /// <exception cref="NullReferenceException"></exception>
                public WLAN_AVAILABLE_NETWORK_LIST(IntPtr ppAvailableNetworkList)
                {
                    // The first 4 bytes are the number of WLAN_AVAILABLE_NETWORK structures.
                    dwNumberOfItems = (uint)Marshal.ReadInt32(ppAvailableNetworkList);
                    // The next 4 bytes are the index of the current item in the unmanaged API.
                    dwIndex = (uint)Marshal.ReadInt32(ppAvailableNetworkList, 4);
                    // Construct the array of WLAN_AVAILABLE_NETWORK structures.
                    wlanAvailableNetwork = new WLAN_AVAILABLE_NETWORK[dwNumberOfItems];

                    for (var i = 0; i < dwNumberOfItems; i++)
                    {
                        var pWlanAvailableNetwork = new IntPtr(ppAvailableNetworkList.ToInt64() + (i * Marshal.SizeOf(typeof(WLAN_AVAILABLE_NETWORK))) + 8);
                        var networkStruct = Marshal.PtrToStructure(pWlanAvailableNetwork, typeof(WLAN_AVAILABLE_NETWORK));
                        if (networkStruct is null)
                            throw new NullReferenceException("Marshalled available network structure is null.");

                        wlanAvailableNetwork[i] = (WLAN_AVAILABLE_NETWORK)networkStruct;
                    }
                }
            }
            /// <summary>
            ///     The WLAN_CONNECTION_PARAMETERS structure specifies the parameters used when using the WlanConnect function.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/ns-wlanapi-wlan_connection_parameters">WLAN_CONNECTION_PARAMETERS</see>
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            public struct WLAN_CONNECTION_PARAMETERS
            {
                /// <summary>
                ///     A WLAN_CONNECTION_MODE value that specifies the mode of connection.
                /// </summary>
                public WLAN_CONNECTION_MODE wlanConnectionMode;
                /// <summary>
                ///     Specifies the profile being used for the connection.
                /// </summary>
                public string strProfile;
                /// <summary>
                ///     Pointer to a DOT11_SSID structure that specifies the SSID of the network to connect to. This parameter is optional. When set
                ///     to NULL, all SSIDs in the profile will be tried. This parameter must not be NULL if WLAN_CONNECTION_MODE is set to
                ///     wlan_connection_mode_discovery_secure or wlan_connection_mode_discovery_unsecure.
                /// </summary>
                public DOT11_SSID pDot11Ssid;
                /// <summary>
                ///     Pointer to a <see cref="DOT11_BSSID_LIST"/> structure that contains the list of basic service set (BSS) identifiers desired for the connection.
                /// </summary>
                public DOT11_BSSID_LIST pDesiredBssidList;
                /// <summary>
                ///     A <see cref="DOT11_BSS_TYPE"/> value that indicates the BSS type of the network. If a profile is provided, this BSS type must be the same as
                ///     the one in the profile.
                /// </summary>
                public DOT11_BSS_TYPE dot11BssType;
                /// <summary>
                ///     Connection flags.
                /// </summary>
                public uint dwFlags;
            }
            /// <summary>
            ///     The WLAN_CONNECTION_ATTRIBUTES structure defines the attributes of a wireless connection.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/ns-wlanapi-wlan_connection_attributes">WLAN_CONNECTION_ATTRIBUTES</see>
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            public struct WLAN_CONNECTION_ATTRIBUTES
            {
                /// <summary>
                /// A WLAN_INTERFACE_STATE value that indicates the state of the interface.
                /// </summary>
                public WLAN_INTERFACE_STATE isState;
                /// <summary>
                /// A WLAN_CONNECTION_MODE value that indicates the mode of the connection.
                /// </summary>
                public WLAN_CONNECTION_MODE wlanConnectionMode;
                /// <summary>
                /// The name of the profile used for the connection. Profile names are case-sensitive. This string must be NULL-terminated.
                /// </summary>
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
                public string profileName;
                /// <summary>
                /// A WLAN_ASSOCIATION_ATTRIBUTES structure that contains the attributes of the association.
                /// </summary>
                public WLAN_ASSOCIATION_ATTRIBUTES wlanAssociationAttributes;
                /// <summary>
                /// A WLAN_SECURITY_ATTRIBUTES structure that contains the security attributes of the connection.
                /// </summary>
                public WLAN_SECURITY_ATTRIBUTES wlanSecurityAttributes;
            }
            /// <summary>
            ///     The WLAN_INTERFACE_INFO structure contains information about a wireless LAN interface.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/ns-wlanapi-wlan_interface_info">WLAN_INTERFACE_INFO</see>
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            public struct WLAN_INTERFACE_INFO
            {
                /// <summary>
                ///     Contains the GUID of the interface.
                /// </summary>
                public Guid InterfaceGuid;
                /// <summary>
                ///     Contains the description of the interface.
                /// </summary>
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
                public string strInterfaceDescription;
                /// <summary>
                ///     Contains a <see cref="WLAN_INTERFACE_STATE"/> value that indicates the current state of the interface.
                /// </summary>
                public WLAN_INTERFACE_STATE isState;
            }
            /// <summary>
            ///     The WLAN_INTERFACE_INFO_LIST structure contains an array of NIC interface information.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/ns-wlanapi-wlan_interface_info_list">WLAN_INTERFACE_INFO_LIST</see>
            [StructLayout(LayoutKind.Sequential)]
            public struct WLAN_INTERFACE_INFO_LIST
            {
                /// <summary>
                ///     Contains the number of items in the InterfaceInfo member.
                /// </summary>
                public Int32 dwNumberOfItems;
                /// <summary>
                ///     The index of the current item. The index of the first item is 0. dwIndex must be less than dwNumberOfItems.
                /// </summary>
                public Int32 dwIndex;
                /// <summary>
                ///     An array of <see cref="WLAN_INTERFACE_INFO"/> structures containing interface information.
                /// </summary>
                public WLAN_INTERFACE_INFO[] InterfaceInfo;

                /// <summary>
                ///     Constructor for WLAN_INTERFACE_INFO_LIST.
                /// </summary>
                /// <remarks>Constructor is needed because the InterfaceInfo member varies based on how many adapters are in the system.</remarks>
                /// <param name="pList">The unmanaged pointer containing the list.</param>
                public WLAN_INTERFACE_INFO_LIST(IntPtr pList)
                {
                    // The first 4 bytes are the number of WLAN_INTERFACE_INFO structures.
                    dwNumberOfItems = Marshal.ReadInt32(pList, 0);
                    // The next 4 bytes are the index of the current item in the unmanaged API.
                    dwIndex = Marshal.ReadInt32(pList, 4);
                    // Construct the array of WLAN_INTERFACE_INFO structures.
                    InterfaceInfo = new WLAN_INTERFACE_INFO[dwNumberOfItems];

                    for (var i = 0; i <= dwNumberOfItems - 1; i++)
                    {
                        // The offset of the array of structures is 8 bytes past the beginning. Then, take the index and multiply it by the number of
                        // bytes in the structure. The length of the WLAN_INTERFACE_INFO structure is 532 bytes - this was determined by doing a Marshal.SizeOf(typeof(WLAN_INTERFACE_INFO))
                        var pItemList = new IntPtr(pList.ToInt64() + (i * Marshal.SizeOf(typeof(WLAN_INTERFACE_INFO))) + 8);

                        var networkStruct = Marshal.PtrToStructure(pItemList, typeof(WLAN_INTERFACE_INFO));
                        if (networkStruct is null)
                            throw new NullReferenceException("Marshalled available network structure is null.");
                        // Construct the WLAN_INTERFACE_INFO structure, marshal the unmanaged structure into it, then copy it to the array of structures.
                        InterfaceInfo[i] = (WLAN_INTERFACE_INFO)networkStruct;
                    }
                }
            }
            /// <summary>
            ///     The WLAN_PHY_RADIO_STATE structure specifies the radio state on a specific physical layer (PHY) type.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/ns-wlanapi-wlan_phy_radio_state">WLAN_PHY_RADIO_STATE</see>
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            public struct WLAN_PHY_RADIO_STATE
            {
                /// <summary>
                ///     The index of the PHY type on which the radio state is being set or queried. The WlanGetInterfaceCapability function returns a list of valid PHY types.
                /// </summary>
                public int dwPhyIndex;
                /// <summary>
                ///     A DOT11_RADIO_STATE value that indicates the software radio state.
                /// </summary>
                public DOT11_RADIO_STATE dot11SoftwareRadioState;
                /// <summary>
                ///     A DOT11_RADIO_STATE value that indicates the hardware radio state.
                /// </summary>
                public DOT11_RADIO_STATE dot11HardwareRadioState;
            }
            /// <summary>
            ///     The WLAN_PROFILE_INFO_LIST structure contains a list of wireless profile information.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/ns-wlanapi-wlan_profile_info_list">WLAN_PROFILE_INFO_LIST</see>
            [StructLayout(LayoutKind.Sequential)]
            public struct WLAN_PROFILE_INFO_LIST
            {
                /// <summary>
                ///     The number of wireless profile entries in the ProfileInfo member.
                /// </summary>
                public uint dwNumberOfItems;
                /// <summary>
                ///     The index of the current item. The index of the first item is 0. The dwIndex member must be less than the dwNumberOfItems member.
                /// </summary>
                public uint dwIndex;
                /// <summary>
                ///     An array of <see cref="WLAN_PROFILE_INFO"/> structures containing interface information. The number of items in the array is specified in the dwNumberOfItems member.
                /// </summary>
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
                public WLAN_PROFILE_INFO[] ProfileInfo;
            }
            /// <summary>
            ///     The WLAN_RADIO_STATE structure specifies the radio state on a list of physical layer (PHY) types.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/ns-wlanapi-wlan_radio_state">WLAN_RADIO_STATE</see>
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            public struct WLAN_RADIO_STATE
            {
                /// <summary>
                ///     The number of valid PHY indices in the PhyRadioState member.
                /// </summary>
                public int numberofItems;
                /// <summary>
                ///     An array of WLAN_PHY_RADIO_STATE structures that specify the radio states of a number of PHY indices. Only the first dwNumberOfPhys entries in this array are valid.
                /// </summary>
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
                private WLAN_PHY_RADIO_STATE[] phyRadioState;
                /// <summary>
                ///     An array of WLAN_PHY_RADIO_STATE structures that specify the radio states of a number of PHY indices. Only the first dwNumberOfPhys entries in this array are valid.
                /// </summary>
                public WLAN_PHY_RADIO_STATE[] PhyRadioState
                {
                    get
                    {
                        var ret = new WLAN_PHY_RADIO_STATE[numberofItems];
                        Array.Copy(phyRadioState, ret, numberofItems);
                        return ret;
                    }
                }
            }
            /// <summary>
            ///     The WLAN_PROFILE_INFO structure contains basic information about a profile.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/ns-wlanapi-wlan_profile_info">WLAN_PROFILE_INFO</see>
            public struct WLAN_PROFILE_INFO
            {
                /// <summary>
                ///     The name of the profile. This value may be the name of a domain if the profile is for provisioning. Profile names are case-sensitive. This string must be NULL-terminated.
                /// </summary>
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
                public string profileName;
                /// <summary>
                ///     A set of flags specifying settings for wireless profile. These values are defined in the Wlanapi.h header file.
                /// </summary>
                public WLAN_PROFILE_FLAGS profileFlags;
            }
            /// <summary>
            ///     The WLAN_SECURITY_ATTRIBUTES structure defines the security attributes for a wireless connection.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/ns-wlanapi-wlan_security_attributes">WLAN_SECURITY_ATTRIBUTES</see>
            [StructLayout(LayoutKind.Sequential)]
            public struct WLAN_SECURITY_ATTRIBUTES
            {
                /// <summary>
                /// Indicates whether security is enabled for this connection.
                /// </summary>
                [MarshalAs(UnmanagedType.Bool)]
                public bool bSecurityEnabled;
                /// <summary>
                ///     Indicates whether 802.1X is enabled for this connection.
                /// </summary>
                [MarshalAs(UnmanagedType.Bool)]
                public bool bOneXEnabled;
                /// <summary>
                /// A DOT11_AUTH_ALGORITHM value that identifies the authentication algorithm.
                /// </summary>
                public DOT11_AUTH_ALGORITHM dot11AuthAlgorithm;
                /// <summary>
                /// A DOT11_CIPHER_ALGORITHM value that identifies the cipher algorithm.
                /// </summary>
                public DOT11_CIPHER_ALGORITHM dot11CipherAlgorithm;
            }
        }
    }
}
