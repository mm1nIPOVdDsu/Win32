using System;

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
            ///     The DOT11_AUTH_ALGORITHM enumerated type defines a wireless LAN authentication algorithm.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/nativewifi/dot11-auth-algorithm">DOT11_AUTH_ALGORITHM</see>
            public enum DOT11_AUTH_ALGORITHM : uint
            {
                /// <summary>
                ///     Specifies an IEEE 802.11 Open System authentication algorithm.
                /// </summary>
                DOT11_AUTH_ALGO_80211_OPEN = 1,
                /// <summary>
                ///     Specifies an 802.11 Shared Key authentication algorithm that requires the use of a pre-shared Wired Equivalent Privacy (WEP)
                ///     key for the 802.11 authentication.
                /// </summary>
                DOT11_AUTH_ALGO_80211_SHARED_KEY = 2,
                /// <summary>
                ///     Specifies a WiFi Protected Access (WPA) algorithm. IEEE 802.1X port authentication is performed by the supplicant,
                ///     authenticator, and authentication server. Cipher keys are dynamically derived through the authentication process.
                /// </summary>
                DOT11_AUTH_ALGO_WPA = 3,
                /// <summary>
                ///     Specifies a WPA algorithm that uses pre-shared keys (PSK). IEEE 802.1X port authentication is performed by the supplicant and
                ///     authenticator. Cipher keys are dynamically derived through a pre-shared key that is used on both the supplicant and authenticator.
                /// </summary>
                DOT11_AUTH_ALGO_WPA_PSK = 4,
                /// <summary>
                ///     This value is not supported.
                /// </summary>
                DOT11_AUTH_ALGO_WPA_NONE = 5,
                /// <summary>
                ///     Specifies an 802.11i Robust Security Network Association (RSNA) algorithm. WPA2 is one such algorithm. IEEE 802.1X port
                ///     authentication is performed by the supplicant, authenticator, and authentication server. Cipher keys are dynamically derived
                ///     through the authentication process.
                /// </summary>
                DOT11_AUTH_ALGO_RSNA = 6,
                /// <summary>
                ///     Specifies an 802.11i RSNA algorithm that uses PSK. IEEE 802.1X port authentication is performed by the supplicant and
                ///     authenticator. Cipher keys are dynamically derived through a pre-shared key that is used on both the supplicant and authenticator.
                /// </summary>
                DOT11_AUTH_ALGO_RSNA_PSK = 7,
                /// <summary>
                ///     Indicates the start of the range that specifies proprietary authentication algorithms that are developed by an IHV.
                /// </summary>
                DOT11_AUTH_ALGO_IHV_START = 0x80000000,
                /// <summary>
                ///     Indicates the end of the range that specifies proprietary authentication algorithms that are developed by an IHV.
                /// </summary>
                DOT11_AUTH_ALGO_IHV_END = 0xffffffff
            }
            /// <summary>
            ///     The DOT11_BSS_TYPE enumerated type defines a basic service set (BSS) network type.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/nativewifi/dot11-bss-type">DOT11_BSS_TYPE</see>
            public enum DOT11_BSS_TYPE : uint
            {
                ///<summary>
                ///     Specifies an infrastructure BSS network.
                ///</summary>
                dot11_BSS_type_infrastructure = 1,
                ///<summary>
                ///     Specifies an independent BSS (IBSS) network.
                ///</summary>
                dot11_BSS_type_independent = 2,
                ///<summary>
                ///     Specifies either infrastructure or IBSS network.
                ///</summary>
                dot11_BSS_type_any = 3,
            }
            /// <summary>
            ///     The DOT11_CIPHER_ALGORITHM enumerated type defines a cipher algorithm for data encryption and decryption.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/nativewifi/dot11-cipher-algorithm">DOT11_CIPHER_ALGORITHM</see>
            public enum DOT11_CIPHER_ALGORITHM : uint
            {
                /// <summary>
                ///     Specifies that no cipher algorithm is enabled or supported.
                /// </summary>
                DOT11_CIPHER_ALGO_NONE = 0x00,
                /// <summary>
                ///     Specifies a Wired Equivalent Privacy (WEP) algorithm, which is the RC4-based algorithm that is specified in the 802.11-1999
                ///     standard. This enumerator specifies the WEP cipher algorithm with a 40-bit cipher key.
                /// </summary>
                DOT11_CIPHER_ALGO_WEP40 = 0x01,
                /// <summary>
                ///     Specifies a Temporal Key Integrity Protocol (TKIP) algorithm, which is the RC4-based cipher suite that is based on the
                ///     algorithms that are defined in the WPA specification and IEEE 802.11i-2004 standard. This cipher also uses the Michael Message
                ///     Integrity Code (MIC) algorithm for forgery protection.
                /// </summary>
                DOT11_CIPHER_ALGO_TKIP = 0x02,
                /// <summary>
                ///     Specifies an AES-CCMP algorithm, as specified in the IEEE 802.11i-2004 standard and RFC 3610. Advanced Encryption Standard
                ///     (AES) is the encryption algorithm defined in FIPS PUB 197.
                /// </summary>
                DOT11_CIPHER_ALGO_CCMP = 0x04,
                /// <summary>
                ///     Specifies a WEP cipher algorithm with a 104-bit cipher key.
                /// </summary>
                DOT11_CIPHER_ALGO_WEP104 = 0x05,
                /// <summary>
                ///     Specifies a WiFi Protected Access (WPA) Use Group Key cipher suite. For more information about the Use Group Key cipher suite,
                ///     refer to Clause 7.3.2.25.1 of the IEEE 802.11i-2004 standard.
                /// </summary>
                DOT11_CIPHER_ALGO_WPA_USE_GROUP = 0x100,
                /// <summary>
                ///     Specifies a Robust Security Network (RSN) Use Group Key cipher suite. For more information about the Use Group Key cipher
                ///     suite, refer to Clause 7.3.2.25.1 of the IEEE 802.11i-2004 standard.
                /// </summary>
                DOT11_CIPHER_ALGO_RSN_USE_GROUP = 0x100,
                /// <summary>
                ///     Specifies a WEP cipher algorithm with a cipher key of any length.
                /// </summary>
                DOT11_CIPHER_ALGO_WEP = 0x101,
                /// <summary>
                ///     Specifies the start of the range that is used to define proprietary cipher algorithms that are developed by an independent
                ///     hardware vendor (IHV).
                /// </summary>
                DOT11_CIPHER_ALGO_IHV_START = 0x80000000,
                /// <summary>
                ///     Specifies the end of the range that is used to define proprietary cipher algorithms that are developed by an IHV.
                /// </summary>
                DOT11_CIPHER_ALGO_IHV_END = 0xffffffff,
            }
            /// <summary>
            ///     A bitmask of the miniport driver's current operation modes.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/windot11/ns-windot11-_dot11_current_operation_mode">DOT11_OPERATION_MODE</see>
            public enum DOT11_OPERATION_MODE : uint
            {
                /// <summary>
                ///     Specifies that the miniport driver support is unknown.
                /// </summary>
                DOT11_OPERATION_MODE_UNKNOWN = 0x00000000,
                /// <summary>
                ///     Specifies that the miniport driver supports the Station (STA) operation mode.
                /// </summary>
                DOT11_OPERATION_MODE_STATION = 0x00000001,
                /// <summary>
                ///     Specifies that the miniport driver supports the Extensible Access Point (ExtAP) operation mode.
                /// </summary>
                DOT11_OPERATION_MODE_EXTENSIBLE_AP = 0x00000002,
                /// <summary>
                ///     Specifies that the miniport driver supports the Extensible Station (ExtSTA) operation mode.
                /// </summary>
                DOT11_OPERATION_MODE_EXTENSIBLE_STATION = 0x00000004,
                /// <summary>
                ///     Specifies that the miniport driver supports the Network Monitor (NetMon) operation mode.
                /// </summary>
                DOT11_OPERATION_MODE_NETWORK_MONITOR = 0x80000000,
                /// <summary>
                ///     Specifies that the miniport driver supports the WiFi Direct Device operation mode. This mode is available starting in Windows 8.
                /// </summary>
                DOT11_OPERATION_MODE_WFD_DEVICE,
                /// <summary>
                ///     Specifies that the miniport driver supports the WiFi Direct Group Owner operation mode.This mode is available starting in
                ///     Windows 8.
                /// </summary>
                DOT11_OPERATION_MODE_WFD_GROUP_OWNER,
                /// <summary>
                ///     Specifies that the miniport driver supports the WiFi Direct Client operation mode. This mode is available starting in Windows 8.
                /// </summary>
                DOT11_OPERATION_MODE_WFD_CLIENT
            }
            /// <summary>
            ///     The DOT11_PHY_TYPE enumeration defines an 802.11 PHY and media type.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/nativewifi/dot11-phy-type">DOT11_PHY_TYPE</see>
            public enum DOT11_PHY_TYPE : uint
            {
                /// <summary>
                ///     Specifies an unknown or uninitialized PHY type.
                /// </summary>
                DOT11_PHY_TYPE_UNKNOWN = 0,
                /// <summary>
                ///     Specifies any PHY type.
                /// </summary>
                DOT11_PHY_TYPE_ANY = DOT11_PHY_TYPE_UNKNOWN,
                /// <summary>
                ///     Specifies a frequency-hopping spread-spectrum (FHSS) PHY. Bluetooth devices can use FHSS or an adaptation of FHSS.
                /// </summary>
                DOT11_PHY_TYPE_FHSS = 1,
                /// <summary>
                ///     Specifies a direct sequence spread spectrum (DSSS) PHY type.
                /// </summary>
                DOT11_PHY_TYPE_DSSS = 2,
                /// <summary>
                ///     Specifies an infrared (IR) baseband PHY type.
                /// </summary>
                DOT11_PHY_TYPE_IRBASEBAND = 3,
                /// <summary>
                ///     Specifies an orthogonal frequency division multiplexing (OFDM) PHY type. 802.11a devices can use OFDM.
                /// </summary>
                DOT11_PHY_TYPE_OFDM = 4,
                /// <summary>
                ///     Specifies a high-rate DSSS (HRDSSS) PHY type.
                /// </summary>
                DOT11_PHY_TYPE_HRDSSS = 5,
                /// <summary>
                ///     Specifies an extended rate PHY type (ERP). 802.11g devices can use ERP.
                /// </summary>
                DOT11_PHY_TYPE_ERP = 6,
                /// <summary>
                ///     Specifies the 802.11n PHY type.
                /// </summary>
                DOT11_PHY_TYPE_HT = 7,
                /// <summary>
                ///     Specifies the 802.11ac PHY type. This is the very high throughput PHY type specified in IEEE 802.11ac.
                /// </summary>
                DOT11_PHY_TYPE_VHT = 8,
                /// <summary>
                ///     Specifies the start of the range that is used to define PHY types that are developed by an independent hardware vendor (IHV).
                /// </summary>
                DOT11_PHY_TYPE_IHV_START = 0x80000000,
                /// <summary>
                ///     Specifies the start of the range that is used to define PHY types that are developed by an independent hardware vendor (IHV).
                /// </summary>
                DOT11_PHY_TYPE_IHV_END = 0xffffffff,
            }
            /// <summary>
            ///     Defines the radio state of a wireless connection.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/ne-wlanapi-dot11_radio_state-r1">DOT11_RADIO_STATE</see>
            public enum DOT11_RADIO_STATE : uint
            {
                /// <summary>
                ///     Radio state is unknown.
                /// </summary>
                Unknown = 0,
                /// <summary>
                ///     Radio state is off.
                /// </summary>
                On,
                /// <summary>
                ///     Radio state is on.
                /// </summary>
                Off
            }
            /// <summary>
            ///     The access mask of the all-user profile.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/nf-wlanapi-wlangetprofile">WLAN_ACCESS_MASK</see>
            public enum WLAN_ACCESS_MASK : uint
            {
                /// <summary>
                ///     The user can view the contents of the profile.
                /// </summary>
                WLAN_READ_ACCESS,
                /// <summary>
                ///     The user has read access, and the user can also connect to and disconnect from a network using the profile. If a user has
                ///     WLAN_EXECUTE_ACCESS, then the user also has WLAN_READ_ACCESS.
                /// </summary>
                WLAN_EXECUTE_ACCESS,
                /// <summary>
                ///     The user has execute access and the user can also modify the content of the profile or delete the profile. If a user has
                ///     WLAN_WRITE_ACCESS, then the user also has WLAN_EXECUTE_ACCESS and WLAN_READ_ACCESS.
                /// </summary>
                WLAN_WRITE_ACCESS
            }
            /// <summary>
            ///     A set of flags that control the type of networks returned in the list.
            /// </summary>
            /// <see href="">WLAN_AVAILABLE_NETWORK_INCLUDE</see>
            public enum WLAN_AVAILABLE_NETWORK_INCLUDE : uint
            {
                /// <summary>
                ///     Include all ad hoc network profiles in the available network list, including profiles that are not visible.
                /// </summary>
                WLAN_AVAILABLE_NETWORK_INCLUDE_ALL_ADHOC_PROFILES = 0x00000001,
                /// <summary>
                ///     Include all hidden network profiles in the available network list, including profiles that are not visible.
                /// </summary>
                WLAN_AVAILABLE_NETWORK_INCLUDE_ALL_MANUAL_HIDDEN_PROFILES = 0x00000002
            }
            /// <summary>
            ///     Connection flags.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/ns-wlanapi-wlan_connection_parameters">WLAN_CONNECTION_FLAGS</see>
            [Flags]
            public enum WLAN_CONNECTION_FLAGS : uint
            {
                /// <summary>
                ///     Connect to the destination network even if the destination is a hidden network. A hidden network does not broadcast its SSID.
                ///     Do not use this flag if the destination network is an ad-hoc network.If the profile specified by strProfile is not NULL, then
                ///     this flag is ignored and the nonBroadcast profile element determines whether to connect to a hidden network.
                /// </summary>
                WLAN_CONNECTION_HIDDEN_NETWORK = 0x00000001,
                /// <summary>
                ///     Do not form an ad-hoc network. Only join an ad-hoc network if the network already exists. Do not use this flag if the
                ///     destination network is an infrastructure network.
                /// </summary>
                WLAN_CONNECTION_ADHOC_JOIN_ONLY = 0x00000002,
                /// <summary>
                ///     Do not form an ad-hoc network. Only join an ad-hoc network if the network already exists. Do not use this flag if the
                ///     destination network is an infrastructure network.
                /// </summary>
                WLAN_CONNECTION_IGNORE_PRIVACY_BIT = 0x00000004,
                /// <summary>
                ///     Exempt EAPOL traffic from encryption and decryption. This flag is used when an application must send EAPOL traffic over an
                ///     infrastructure network that uses Open authentication and WEP encryption. This flag must not be used to connect to networks
                ///     that require 802.1X authentication. This flag is only valid when wlaConnectionMode is set to
                ///     WLAN_CONNECTION_MODE_TEMPORARY_PROFILE. Avoid using this flag whenever possible.
                /// </summary>
                WLAN_CONNECTION_EAPOL_PASSTHROUGH = 0x00000008,
                /// <summary>
                ///     Automatically persist discovery profile on successful connection completion. This flag is only valid for
                ///     WLAN_CONNECTION_MODE_DISCOVERY_SECURE or WLAN_CONNECTION_MODE_DISCOVERY_UNSECURE. The profile will be saved as an all user
                ///     profile, with the name generated from the SSID using WlanUtf8SsidToDisplayName. If there is already a profile with the same
                ///     name, a number will be appended to the end of the profile name. The profile will be saved with manual connection mode, unless
                ///     WLAN_CONNECTION_PERSIST_DISCOVERY_PROFILE_CONNECTION_MODE_AUTO is also specified.
                /// </summary>
                WLAN_CONNECTION_PERSIST_DISCOVERY_PROFILE = 0x00000010,
                /// <summary>
                ///     To be used in conjunction with WLAN_CONNECTION_PERSIST_DISCOVERY_PROFILE. The discovery profile will be persisted with
                ///     automatic connection mode.
                /// </summary>
                WLAN_CONNECTION_PERSIST_DISCOVERY_PROFILE_CONNECTION_MODE_AUTO = 0x00000020,
                /// <summary>
                ///     To be used in conjunction with WLAN_CONNECTION_PERSIST_DISCOVERY_PROFILE. The discovery profile will be persisted with
                ///     automatic connection mode.
                /// </summary>
                WLAN_CONNECTION_PERSIST_DISCOVERY_PROFILE_OVERWRITE_EXISTING = 0x00000040
            }
            /// <summary>
            ///     The WLAN_CONNECTION_MODE enumerated type defines the mode of connection.Windows XP with SP3 and Wireless LAN API for Windows XP
            ///     with SP2: Only the WLAN_CONNECTION_MODE_PROFILE value is supported.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/ne-wlanapi-wlan_connection_mode">WLAN_CONNECTION_MODE</see>
            public enum WLAN_CONNECTION_MODE : uint
            {
                /// <summary>
                ///     A profile will be used to make the connection.
                /// </summary>
                WLAN_CONNECTION_MODE_PROFILE = 0,
                /// <summary>
                ///     A temporary profile will be used to make the connection.
                /// </summary>
                WLAN_CONNECTION_MODE_TEMPORARY_PROFILE,
                /// <summary>
                ///     Secure discovery will be used to make the connection.
                /// </summary>
                WLAN_CONNECTION_MODE_DISCOVERY_SECURE,
                /// <summary>
                ///     Unsecure discovery will be used to make the connection.
                /// </summary>
                WLAN_CONNECTION_MODE_DISCOVERY_UNSECURE,
                /// <summary>
                ///     The connection is initiated by the wireless service automatically using a persistent profile.
                /// </summary>
                WLAN_CONNECTION_MODE_AUTO,
                /// <summary>
                ///     Not used.
                /// </summary>
                WLAN_CONNECTION_MODE_INVALID,
            }
            /// <summary>
            ///     The WLAN_INTERFACE_STATE enumerated type indicates the state of an interface.
            /// </summary>
            /// <see href="">WLAN_INTERFACE_STATE</see>
            public enum WLAN_INTERFACE_STATE : uint
            {
                /// <summary>
                ///     The interface is not ready.
                /// </summary>
                WLAN_INTERFACE_STATE_NOT_READY = 0,
                /// <summary>
                ///     The interface is connected.
                /// </summary>
                WLAN_INTERFACE_STATE_CONNECTED = 1,
                /// <summary>
                ///     A wireless ad-hoc network was formed.
                /// </summary>
                WLAN_INTERFACE_STATE_AD_HOC_NETWORK_FORMED = 2,
                /// <summary>
                ///     The interface is disconnecting.
                /// </summary>
                WLAN_INTERFACE_STATE_DISCONNECTING = 3,
                /// <summary>
                ///     The interface is disconnected.
                /// </summary>
                WLAN_INTERFACE_STATE_DISCONNECTED = 4,
                /// <summary>
                ///     The interface is associating.
                /// </summary>
                WLAN_INTERFACE_STATE_ASSOCIATING = 5,
                /// <summary>
                ///     The interface is discovering.
                /// </summary>
                WLAN_INTERFACE_STATE_DISCOVERING = 6,
                /// <summary>
                ///     The interface is authenticating.
                /// </summary>
                WLAN_INTERFACE_STATE_AUTHENTICATING = 7,
            }
            /// <summary>
            ///     The WLAN_INTF_OPCODE enumerated type defines various opcodes used to set and query parameters on a wireless interface.
            /// </summary>
            public enum WLAN_INTF_OPCODE : uint
            {
                /// <summary>
                ///     Opcode used to set or query whether auto config is started.
                /// </summary>
                WLAN_INTF_OPCODE_AUTOCONF_START,
                /// <summary>
                ///     Opcode used to set or query whether auto config is enabled.
                /// </summary>
                WLAN_INTF_OPCODE_AUTOCONF_ENABLED = 1,
                /// <summary>
                ///     Opcode used to set or query whether background scan is enabled.
                /// </summary>
                WLAN_INTF_OPCODE_BACKGROUND_SCAN_ENABLED,
                /// <summary>
                ///     Opcode used to set or query the media streaming mode of the driver.
                /// </summary>
                WLAN_INTF_OPCODE_MEDIA_STREAMING_MODE,
                /// <summary>
                ///     Opcode used to set or query the radio state.
                /// </summary>
                WLAN_INTF_OPCODE_RADIO_STATE,
                /// <summary>
                ///     Opcode used to set or query the BSS type of the interface.
                /// </summary>
                WLAN_INTF_OPCODE_BSS_TYPE,
                /// <summary>
                ///     Opcode used to query the state of the interface.
                /// </summary>
                WLAN_INTF_OPCODE_INTERFACE_TYPE,
                /// <summary>
                ///     Opcode used to query information about the current connection of the interface.
                /// </summary>
                WLAN_INTF_OPCODE_CURRENT_CONNECTION,
                /// <summary>
                ///     Opcode used to query the current channel on which the wireless interface is operating.
                /// </summary>
                WLAN_INTF_OPCODE_CHANNEL_NUMBER,
                /// <summary>
                ///     Opcode used to query the supported authenticate/cipher pairs for infrastructure mode.
                /// </summary>
                WLAN_INTF_OPCODE_SUPPORTED_INFRASTRUCTURE_AUTH_CIPHER_PAIRS,
                /// <summary>
                ///     Opcode used to query the supported authenticate/cipher pairs for ad hoc mode.
                /// </summary>
                WLAN_INTF_OPCODE_SUPPORTED_ADHOC_AUTH_CIPHER_PAIRS,
                /// <summary>
                ///     Opcode used to query the list of supported country or region strings.
                /// </summary>
                WLAN_INTF_OPCODE_SUPPORTED_COUNTRY_OR_REGION_STRING_LIST,
                /// <summary>
                ///     Opcode used to set or query the current operation mode of the wireless interface.
                /// </summary>
                WLAN_INTF_OPCODE_CURRENT_OPERATION_MODE,
                /// <summary>
                ///     TODO
                /// </summary>
                WLAN_INTF_OPCODE_SUPPORTED_SAFE_MODE,
                /// <summary>
                ///     TODO
                /// </summary>
                WLAN_INTF_OPCODE_CERTIFIED_SAFE_MODE,
                /// <summary>
                ///     TODO
                /// </summary>
                WLAN_INTF_OPCODE_HOSTED_NETWORK_CAPABLE,
                /// <summary>
                ///     TODO
                /// </summary>
                WLAN_INTF_OPCODE_MANAGEMENT_FRAME_PROTECTION_CAPABLE,
                /// <summary>
                ///     TODO
                /// </summary>
                WLAN_INTF_OPCODE_SECONDARY_STA_INTERFACES,
                /// <summary>
                ///     TODO
                /// </summary>
                WLAN_INTF_OPCODE_SECONDARY_STA_SYNCHRONIZED_CONNECTIONS,
                /// <summary>
                ///     TODO
                /// </summary>
                WLAN_INTF_OPCODE_AUTOCONF_END = 0x0fffffff,
                /// <summary>
                ///     TODO
                /// </summary>
                WLAN_INTF_OPCODE_MSM_START = 0x10000100,
                /// <summary>
                ///     Opcode used to query driver statistics.
                /// </summary>
                WLAN_INTF_OPCODE_STATISTICS = 0x10000101,
                /// <summary>
                ///     Opcode used to query the received signal strength.
                /// </summary>
                WLAN_INTF_OPCODE_RSSI,
                /// <summary>
                ///     TODO
                /// </summary>
                WLAN_INTF_OPCODE_MSM_END = 0x1fffffff,
                /// <summary>
                ///     TODO
                /// </summary>
                WLAN_INTF_OPCODE_SECURITY_START = 0x20010000,
                /// <summary>
                ///     TODO
                /// </summary>
                WLAN_INTF_OPCODE_SECURITY_END = 0x2fffffff,
                /// <summary>
                ///     TODO
                /// </summary>
                WLAN_INTF_OPCODE_IHV_START = 0x30000000,
                /// <summary>
                ///     TODO
                /// </summary>
                WLAN_INTF_OPCODE_IHV_END = 0x3fffffff
            }
            /// <summary>
            ///     The WLAN_OPCODE_VALUE_TYPE enumeration specifies the origin of automatic configuration (auto config) settings.
            /// </summary>
            public enum WLAN_OPCODE_VALUE_TYPE : uint
            {
                /// <summary>
                ///     The auto config settings were queried, but the origin of the settings was not determined.
                /// </summary>
                WLAN_OPCODE_VALUE_TYPE_QUERY_ONLY = 0,
                /// <summary>
                ///     The auto config settings were set by group policy.
                /// </summary>
                WLAN_OPCODE_VALUE_TYPE_SET_BY_GROUP_POLICY = 1,
                /// <summary>
                ///     The auto config settings were set by the user.
                /// </summary>
                WLAN_OPCODE_VALUE_TYPE_SET_BY_USER = 2,
                /// <summary>
                ///     The auto config settings are invalid.
                /// </summary>
                WLAN_OPCODE_VALUE_TYPE_INVALID = 3
            }
            /// <summary>
            ///     Provides additional information about a request.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/nf-wlanapi-wlangetprofile">WLAN_PROFILE_FLAGS</see>
            [Flags]
            public enum WLAN_PROFILE_FLAGS : uint
            {
                /// <summary>
                ///     On input, this flag indicates that the caller wants to retrieve the plain text key from a wireless profile. If the calling
                ///     thread has the required permissions, the WlanGetProfile function returns the plain text key in the keyMaterial element of the
                ///     profile returned in the buffer pointed to by the pstrProfileXml parameter.
                /// </summary>
                WLAN_PROFILE_GET_PLAINTEXT_KEY = 0x00000000,
                /// <summary>
                ///     On output when the WlanGetProfile call is successful, this flag indicates that this profile was created by group policy. A
                ///     group policy profile is read-only. Neither the content nor the preference order of the profile can be changed.
                /// </summary>
                WLAN_PROFILE_GROUP_POLICY = 0x00000001,
                /// <summary>
                ///     On output when the WlanGetProfile call is successful, this flag indicates that the profile is a user profile for the specific
                ///     user in whose context the calling thread resides. If not set, this profile is an all-user profile.
                /// </summary>
                WLAN_PROFILE_USER = 0x00000002
            }
            /// <summary>
            ///     The WLAN_REASON_CODE type indicates the reason a WLAN operation has failed.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/nativewifi/wlan-reason-code">WLAN_REASON_CODE</see>
            public enum WLAN_REASON_CODE : uint
            {
                /// <summary>
                ///     The operation succeeds.
                /// </summary>
                WLAN_REASON_CODE_SUCCESS = 0x00,
                /// <summary>
                ///     The reason for failure is unknown.
                /// </summary>
                WLAN_REASON_CODE_UNKNOWN,
                /// <summary>
                ///     The wireless network is not compatible.
                /// </summary>
                WLAN_REASON_CODE_NETWORK_NOT_COMPATIBLE,
                /// <summary>
                ///     The wireless network profile is not compatible.
                /// </summary>
                WLAN_REASON_CODE_PROFILE_NOT_COMPATIBLE,
                /// <summary>
                ///     The profile specifies no auto connection.
                /// </summary>
                WLAN_REASON_CODE_NO_AUTO_CONNECTION,
                /// <summary>
                ///     The wireless network is not visible.
                /// </summary>
                WLAN_REASON_CODE_NOT_VISIBLE,
                /// <summary>
                ///     The wireless network is blocked by group policy.
                /// </summary>
                WLAN_REASON_CODE_GP_DENIED,
                /// <summary>
                ///     The wireless network is blocked by the user.
                /// </summary>
                WLAN_REASON_CODE_USER_DENIED,
                /// <summary>
                ///     The basic service set (BSS) type is not allowed on this wireless adapter.
                /// </summary>
                WLAN_REASON_CODE_BSS_TYPE_NOT_ALLOWED,
                /// <summary>
                ///     The wireless network is in the failed list.
                /// </summary>
                WLAN_REASON_CODE_IN_FAILED_LIST,
                /// <summary>
                ///     The wireless network is in the blocked list.
                /// </summary>
                WLAN_REASON_CODE_IN_BLOCKED_LIST,
                /// <summary>
                ///     The size of the service set identifiers (SSID) list exceeds the maximum size supported by the adapter.
                /// </summary>
                WLAN_REASON_CODE_SSID_LIST_TOO_LONG,
                /// <summary>
                ///     The Media Specific Module (MSM) connect call fails.
                /// </summary>
                WLAN_REASON_CODE_CONNECT_CALL_FAIL,
                /// <summary>
                ///     The MSM scan call fails.
                /// </summary>
                WLAN_REASON_CODE_SCAN_CALL_FAIL,
                /// <summary>
                ///     The specified network is not available.This reason code is also used when there is a mismatch between capabilities specified
                ///     in an XML profile and interface and/or network capabilities. For example, if a profile specifies the use of WPA2 when the NIC
                ///     only supports WPA, then this error code is returned. Also, if a profile specifies the use of FIPS mode when the NIC does not
                ///     support FIPS mode, then this error code is returned.
                /// </summary>
                WLAN_REASON_CODE_NETWORK_NOT_AVAILABLE,
                /// <summary>
                ///     The profile was changed or deleted before the connection was established.
                /// </summary>
                WLAN_REASON_CODE_PROFILE_CHANGED_OR_DELETED,
                /// <summary>
                ///     The profile key does not match the network key.
                /// </summary>
                WLAN_REASON_CODE_KEY_MISMATCH,
                /// <summary>
                ///     The user is not responding.
                /// </summary>
                WLAN_REASON_CODE_USER_NOT_RESPOND,
                /// <summary>
                ///     An application tried to apply a wireless Hosted Network profile to a physical wireless network adapter using
                ///     the WlanSetProfile function, rather than to a virtual device.
                /// </summary>
                WLAN_REASON_CODE_AP_PROFILE_NOT_ALLOWED_FOR_CLIENT,
                /// <summary>
                ///     An application tried to apply a wireless Hosted Network profile to a physical wireless network adapter using
                ///     the WlanSetProfile function, rather than to a virtual device.
                /// </summary>
                WLAN_REASON_CODE_AP_PROFILE_NOT_ALLOWED,
                /// <summary>
                ///     The profile invalid according to the schema.
                /// </summary>
                WLAN_REASON_CODE_INVALID_PROFILE_SCHEMA,
                /// <summary>
                ///     The WLANProfile element is missing.
                /// </summary>
                WLAN_REASON_CODE_PROFILE_MISSING,
                /// <summary>
                ///     The name of the profile is invalid.
                /// </summary>
                WLAN_REASON_CODE_INVALID_PROFILE_NAME,
                /// <summary>
                ///     The type of the profile is invalid.
                /// </summary>
                WLAN_REASON_CODE_INVALID_PROFILE_TYPE,
                /// <summary>
                ///     The PHY type is invalid.
                /// </summary>
                WLAN_REASON_CODE_INVALID_PHY_TYPE,
                /// <summary>
                ///     The MSM security settings are missing.
                /// </summary>
                WLAN_REASON_CODE_MSM_SECURITY_MISSING,
                /// <summary>
                ///     The independent hardware vendor (IHV) security settings are missing.
                /// </summary>
                WLAN_REASON_CODE_IHV_SECURITY_NOT_SUPPORTED,
                /// <summary>
                ///     The IHV profile OUI did not match with the adapter OUI.
                /// </summary>
                WLAN_REASON_CODE_IHV_OUI_MISMATCH,
                /// <summary>
                ///     The IHV OUI settings are missing.
                /// </summary>
                WLAN_REASON_CODE_IHV_OUI_MISSING,
                /// <summary>
                ///     The IHV security settings are missing.
                /// </summary>
                WLAN_REASON_CODE_IHV_SETTINGS_MISSING,
                /// <summary>
                ///     An application tried to apply an IHV profile on an adapter that does not support IHV connectivity settings.
                /// </summary>
                WLAN_REASON_CODE_IHV_CONNECTIVITY_NOT_SUPPORTED,
                /// <summary>
                ///     The security settings conflict.
                /// </summary>
                WLAN_REASON_CODE_CONFLICT_SECURITY,
                /// <summary>
                ///     The security settings are missing.
                /// </summary>
                WLAN_REASON_CODE_SECURITY_MISSING,
                /// <summary>
                ///     The BSS type is not valid.
                /// </summary>
                WLAN_REASON_CODE_INVALID_BSS_TYPE,
                /// <summary>
                ///     Automatic connection cannot be set for an ad hoc network.
                /// </summary>
                WLAN_REASON_CODE_INVALID_ADHOC_CONNECTION_MODE,
                /// <summary>
                ///     Non-broadcast cannot be set for an ad hoc network.
                /// </summary>
                WLAN_REASON_CODE_NON_BROADCAST_SET_FOR_ADHOC,
                /// <summary>
                ///     Auto-switch cannot be set for an ad hoc network.
                /// </summary>
                WLAN_REASON_CODE_AUTO_SWITCH_SET_FOR_ADHOC,
                /// <summary>
                ///     Auto-switch cannot be set for a manual connection profile.
                /// </summary>
                WLAN_REASON_CODE_AUTO_SWITCH_SET_FOR_MANUAL_CONNECTION,
                /// <summary>
                ///     The IHV 802.1X security settings are missing.
                /// </summary>
                WLAN_REASON_CODE_IHV_SECURITY_ONEX_MISSING,
                /// <summary>
                ///     The SSID in the profile is invalid or missing.
                /// </summary>
                WLAN_REASON_CODE_PROFILE_SSID_INVALID,
                /// <summary>
                ///     Too many SSIDs were specified in the profile.
                /// </summary>
                WLAN_REASON_CODE_TOO_MANY_SSID,
                /// <summary>
                ///     An application tried to apply a wireless Hosted Network profile to a physical network adapter NIC using
                ///     the WlanSetProfile function, and specified an unacceptable value for the maximum number of clients allowed.
                /// </summary>
                WLAN_REASON_CODE_BAD_MAX_NUMBER_OF_CLIENTS_FOR_AP,
                /// <summary>
                ///     The channel specified is invalid.
                /// </summary>
                WLAN_REASON_CODE_INVALID_CHANNEL,
                /// <summary>
                /// </summary>
                WLAN_REASON_CODE_OPERATION_MODE_NOT_SUPPORTED,
                /// <summary>
                ///     An internal operating system error occurred with the wireless Hosted Network.
                /// </summary>
                WLAN_REASON_CODE_AUTO_AP_PROFILE_NOT_ALLOWED,
                /// <summary>
                /// </summary>
                WLAN_REASON_CODE_AUTO_CONNECTION_NOT_ALLOWED,
                /// <summary>
                ///     The security settings are not supported by the operating system.
                /// </summary>
                WLAN_REASON_CODE_UNSUPPORTED_SECURITY_SET_BY_OS,
                /// <summary>
                ///     The security settings are not supported.
                /// </summary>
                WLAN_REASON_CODE_UNSUPPORTED_SECURITY_SET,
                /// <summary>
                ///     The BSS type does not match.
                /// </summary>
                WLAN_REASON_CODE_BSS_TYPE_UNMATCH,
                /// <summary>
                ///     The PHY type does not match.
                /// </summary>
                WLAN_REASON_CODE_PHY_TYPE_UNMATCH,
                /// <summary>
                ///     The data rate does not match.
                /// </summary>
                WLAN_REASON_CODE_DATARATE_UNMATCH,
                /// <summary>
                ///     User has canceled the operation.
                /// </summary>
                WLAN_REASON_CODE_USER_CANCELLED,
                /// <summary>
                ///     Driver disconnected while associating.
                /// </summary>
                WLAN_REASON_CODE_ASSOCIATION_FAILURE,
                /// <summary>
                ///     Association timed out.
                /// </summary>
                WLAN_REASON_CODE_ASSOCIATION_TIMEOUT,
                /// <summary>
                ///     Pre-association security failure.
                /// </summary>
                WLAN_REASON_CODE_PRE_SECURITY_FAILURE,
                /// <summary>
                ///     Failed to start security after association.
                /// </summary>
                WLAN_REASON_CODE_START_SECURITY_FAILURE,
                /// <summary>
                ///     Security ends up with failure.
                /// </summary>
                WLAN_REASON_CODE_SECURITY_FAILURE,
                /// <summary>
                ///     Security operation times out.
                /// </summary>
                WLAN_REASON_CODE_SECURITY_TIMEOUT,
                /// <summary>
                ///     Driver disconnected while roaming.
                /// </summary>
                WLAN_REASON_CODE_ROAMING_FAILURE,
                /// <summary>
                ///     Failed to start security for roaming.
                /// </summary>
                WLAN_REASON_CODE_ROAMING_SECURITY_FAILURE,
                /// <summary>
                ///     Failed to start security for ad hoc peer.
                /// </summary>
                WLAN_REASON_CODE_ADHOC_SECURITY_FAILURE,
                /// <summary>
                ///     Driver disconnected.
                /// </summary>
                WLAN_REASON_CODE_DRIVER_DISCONNECTED,
                /// <summary>
                ///     Driver failed to perform some operations.
                /// </summary>
                WLAN_REASON_CODE_DRIVER_OPERATION_FAILURE,
                /// <summary>
                ///     The IHV service is not available.
                /// </summary>
                WLAN_REASON_CODE_IHV_NOT_AVAILABLE,
                /// <summary>
                ///     The response from the IHV service timed out.
                /// </summary>
                WLAN_REASON_CODE_IHV_NOT_RESPONDING,
                /// <summary>
                ///     Timed out waiting for the driver to disconnect.
                /// </summary>
                WLAN_REASON_CODE_DISCONNECT_TIMEOUT,
                /// <summary>
                ///     An internal error prevented the operation from being completed.
                /// </summary>
                WLAN_REASON_CODE_INTERNAL_FAILURE,
                /// <summary>
                ///     A user interaction request timed out.
                /// </summary>
                WLAN_REASON_CODE_UI_REQUEST_TIMEOUT,
                /// <summary>
                ///     Roaming too often. Post security was not completed after 5 attempts.
                /// </summary>
                WLAN_REASON_CODE_TOO_MANY_SECURITY_ATTEMPTS,
                /// <summary>
                ///     An internal operating system error occurred that resulted in a failure to start the wireless Hosted Network.
                /// </summary>
                WLAN_REASON_CODE_AP_STARTING_FAILURE,
                /// <summary>
                ///     Key index specified is not valid.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_INVALID_KEY_INDEX,
                /// <summary>
                ///     Key required, PSK present.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_PSK_PRESENT,
                /// <summary>
                ///     Invalid key length.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_KEY_LENGTH,
                /// <summary>
                ///     Invalid PSK length.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_PSK_LENGTH,
                /// <summary>
                ///     No authentication/cipher pairs specified.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_NO_AUTH_CIPHER_SPECIFIED,
                /// <summary>
                ///     Too many authentication/cipher pairs specified.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_TOO_MANY_AUTH_CIPHER_SPECIFIED,
                /// <summary>
                ///     Profile contains duplicate authentication/cipher pair.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_DUPLICATE_AUTH_CIPHER,
                /// <summary>
                ///     Profile raw data is invalid.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_RAWDATA_INVALID,
                /// <summary>
                ///     Invalid authentication/cipher combination.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_INVALID_AUTH_CIPHER,
                /// <summary>
                ///     802.1X disabled when it is required to be enabled.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_ONEX_DISABLED,
                /// <summary>
                ///     802.1X enabled when it is required to be disabled.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_ONEX_ENABLED,
                /// <summary>
                ///     Invalid PMK cache mode.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_INVALID_PMKCACHE_MODE,
                /// <summary>
                ///     Invalid PMK cache size.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_INVALID_PMKCACHE_SIZE,
                /// <summary>
                ///     Invalid PMK cache TTL.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_INVALID_PMKCACHE_TTL,
                /// <summary>
                ///     Invalid pre-authentication mode.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_INVALID_PREAUTH_MODE,
                /// <summary>
                ///     Invalid pre-authentication throttle.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_INVALID_PREAUTH_THROTTLE,
                /// <summary>
                ///     Pre-authentication enabled when PMK cache is disabled.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_PREAUTH_ONLY_ENABLED,
                /// <summary>
                ///     Capability matching failed at network.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_CAPABILITY_NETWORK,
                /// <summary>
                ///     Capability matching failed at NIC.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_CAPABILITY_NIC,
                /// <summary>
                ///     Capability matching failed at profile.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_CAPABILITY_PROFILE,
                /// <summary>
                ///     Network does not support specified capability type.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_CAPABILITY_DISCOVERY,
                /// <summary>
                ///     Paraphrase contains invalid character.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_PASSPHRASE_CHAR,
                /// <summary>
                ///     Key material contains invalid character.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_KEYMATERIAL_CHAR,
                /// <summary>
                ///     The key type specified does not match the key material.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_WRONG_KEYTYPE,
                /// <summary>
                ///     A mixed cell is suspected. The AP is not signaling that it is compatible with a privacy-enabled profile.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_MIXED_CELL,
                /// <summary>
                ///     The number of authentication timers or the number of timeouts specified in the profile is invalid.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_AUTH_TIMERS_INVALID,
                /// <summary>
                ///     The group key update interval specified in the profile is invalid.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_INVALID_GKEY_INTV,
                /// <summary>
                ///     A "transition network" is suspected. Legacy 802.11 security is used for the next authentication attempt.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_TRANSITION_NETWORK,
                /// <summary>
                ///     The key contains characters that are not in the ASCII character set.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_KEY_UNMAPPED_CHAR,
                /// <summary>
                ///     Capability matching failed because the network does not support the authentication method in the profile.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_CAPABILITY_PROFILE_AUTH,
                /// <summary>
                ///     Capability matching failed because the network does not support the cipher algorithm in the profile.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_CAPABILITY_PROFILE_CIPHER,
                /// <summary>
                ///     FIPS 140-2 mode value in the profile is invalid.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_SAFE_MODE,
                /// <summary>
                ///     Profile requires FIPS 140-2 mode, which is not supported by network interface card (NIC).
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_CAPABILITY_PROFILE_SAFE_MODE_NIC,
                /// <summary>
                ///     Profile requires FIPS 140-2 mode, which is not supported by network.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_CAPABILITY_PROFILE_SAFE_MODE_NW,
                /// <summary>
                ///     Profile specifies an unsupported authentication ,mechanism.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_UNSUPPORTED_AUTH,
                /// <summary>
                ///     Profile specifies an unsupported cipher.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PROFILE_UNSUPPORTED_CIPHER,
                /// <summary>
                ///     Failed to queue the user interface request.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_UI_REQUEST_FAILURE,
                /// <summary>
                ///     The wireless LAN requires Management Frame Protection (MFP) and the network interface does not support MFP. For more
                ///     information, see the IEEE 802.11w amendment to the 802.11 standard.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_CAPABILITY_MFP_NW_NIC,
                /// <summary>
                ///     802.1X authentication did not start within configured time.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_AUTH_START_TIMEOUT,
                /// <summary>
                ///     802.1X authentication did not complete within configured time.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_AUTH_SUCCESS_TIMEOUT,
                /// <summary>
                ///     Dynamic key exchange did not start within configured time.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_KEY_START_TIMEOUT,
                /// <summary>
                ///     Dynamic key exchange did not complete within configured time.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_KEY_SUCCESS_TIMEOUT,
                /// <summary>
                ///     Message 3 of 4-way handshake has no key data.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_M3_MISSING_KEY_DATA,
                /// <summary>
                ///     Message 3 of 4-way handshake has no IE.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_M3_MISSING_IE,
                /// <summary>
                ///     Message 3 of 4-way handshake has no GRP key.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_M3_MISSING_GRP_KEY,
                /// <summary>
                ///     Matching security capabilities of IE in M3 failed.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PR_IE_MATCHING,
                /// <summary>
                ///     Matching security capabilities of secondary IE in M3 failed.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_SEC_IE_MATCHING,
                /// <summary>
                ///     Required a pairwise key but access point (AP) configured only group keys.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_NO_PAIRWISE_KEY,
                /// <summary>
                ///     Message 1 of group key handshake has no key data.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_G1_MISSING_KEY_DATA,
                /// <summary>
                ///     Message 1 of group key handshake has no group key.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_G1_MISSING_GRP_KEY,
                /// <summary>
                ///     AP reset secure bit after connection was secured.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PEER_INDICATED_INSECURE,
                /// <summary>
                ///     802.1X indicated that there is no authenticator, but the profile requires one.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_NO_AUTHENTICATOR,
                /// <summary>
                ///     Plumbing settings to NIC failed.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_NIC_FAILURE,
                /// <summary>
                ///     Operation was canceled by a caller.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_CANCELLED,
                /// <summary>
                ///     Entered key format is not in a valid format.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_KEY_FORMAT,
                /// <summary>
                ///     A security downgrade was detected.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_DOWNGRADE_DETECTED,
                /// <summary>
                ///     A PSK mismatch is suspected.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_PSK_MISMATCH_SUSPECTED,
                /// <summary>
                ///     There was a forced failure because the connection method was not secure.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_FORCED_FAILURE,
                /// <summary>
                ///     Message 3 of 4 way handshake contains too many RSN IE (RSN).
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_M3_TOO_MANY_RSNIE,
                /// <summary>
                ///     Message 2 of 4 way handshake has no key data (RSN Ad-hoc).
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_M2_MISSING_KEY_DATA,
                /// <summary>
                ///     Message 2 of 4 way handshake has no IE (RSN Ad-hoc).
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_M2_MISSING_IE,
                /// <summary>
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_AUTH_WCN_COMPLETED,
                /// <summary>
                ///     The security UI request failed because the request could not be queued or because the user canceled the request.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_SECURITY_UI_FAILURE,
                /// <summary>
                ///     Message 3 of 4 way handshake has no Management Group Key (RSN).
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_M3_MISSING_MGMT_GRP_KEY,
                /// <summary>
                ///     Message 1 of group key handshake has no group management key.
                /// </summary>
                WLAN_REASON_CODE_MSMSEC_G1_MISSING_MGMT_GRP_KEY,
                /// <summary>
                ///     No user is available for 802.1X authentication. This error can occur when machine authentication is disabled and no user is
                ///     logged on to the machine.
                /// </summary>
                ONEX_UNABLE_TO_IDENTIFY_USER,
                /// <summary>
                ///     The 802.1X identity could not be found.
                /// </summary>
                ONEX_IDENTITY_NOT_FOUND,
                /// <summary>
                ///     Authentication could only be completed through the user interface and this interface could not be displayed.
                /// </summary>
                ONEX_UI_DISABLED,
                /// <summary>
                ///     The EAP authentication failed.
                /// </summary>
                ONEX_EAP_FAILURE_RECEIVED,
                /// <summary>
                ///     The 802.1X authenticator went away from the network.
                /// </summary>
                ONEX_AUTHENTICATOR_NO_LONGER_PRESENT,
                /// <summary>
                ///     The version of the OneX profile supplied is not supported.
                /// </summary>
                ONEX_PROFILE_VERSION_NOT_SUPPORTED,
                /// <summary>
                ///     The OneX profile has an invalid length.
                /// </summary>
                ONEX_PROFILE_INVALID_LENGTH,
                /// <summary>
                ///     The EAP type specified in the OneX profile(possibly supplied by the EAPType element) is not allowed.
                /// </summary>
                ONEX_PROFILE_DISALLOWED_EAP_TYPE,
                /// <summary>
                ///     The EAP Type specified in the OneX profile (possibly supplied by the EAPType element) is invalid, or one of the EAP flags
                ///     (possibly supplied in the EAPConfig element) is invalid.
                /// </summary>
                ONEX_PROFILE_INVALID_EAP_TYPE_OR_FLAG,
                /// <summary>
                ///     The supplicant flags (possibly supplied in the EAPConfig element) in the OneX profile are invalid.
                /// </summary>
                ONEX_PROFILE_INVALID_ONEX_FLAGS,
                /// <summary>
                ///     A timer specified in the OneX profile (possibly supplied by the heldPeriod, authPeriod, or startPeriod element) is invalid.
                /// </summary>
                ONEX_PROFILE_INVALID_TIMER_VALUE,
                /// <summary>
                ///     The supplicant mode specified in the OneX profile (possibly supplied by the supplicantMode element) is invalid.
                /// </summary>
                ONEX_PROFILE_INVALID_SUPPLICANT_MODE,
                /// <summary>
                ///     The authentication mode specified in the OneX profile (possibly supplied by the authMode element) is invalid.
                /// </summary>
                ONEX_PROFILE_INVALID_AUTH_MODE,
                /// <summary>
                ///     The connection properties specified in the OneX profile (possibly supplied by the EAPConfig element) are invalid.
                /// </summary>
                ONEX_PROFILE_INVALID_EAP_CONNECTION_PROPERTIES,
            }
        }
    }
}