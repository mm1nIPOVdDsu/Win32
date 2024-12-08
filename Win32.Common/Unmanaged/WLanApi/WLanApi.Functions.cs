using System;
using System.Runtime.InteropServices;
using System.Text;

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
            ///     The WlanCloseHandle function closes a connection to the server.
            /// </summary>
            /// <param name="hClientHandle">
            ///     The client's session handle, which identifies the connection to be closed. This handle was obtained by a previous call to the
            ///     <see cref="WlanOpenHandle"/> function.
            /// </param>
            /// <param name="pReserved">Reserved for future use. Set this parameter to NULL.</param>
            /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/nf-wlanapi-wlanclosehandle">WlanCloseHandle</see>
            [DllImport(WLanApiDll, EntryPoint = "WlanCloseHandle", SetLastError = true)]
            public static extern uint WlanCloseHandle([In] IntPtr hClientHandle, IntPtr pReserved);
            /// <summary>
            ///     The WlanConnect function attempts to connect to a specific network.
            /// </summary>
            /// <param name="hClientHandle">The client's session handle, returned by a previous call to the <see cref="WlanOpenHandle"/> function.</param>
            /// <param name="pInterfaceGuid">The GUID of the interface to use for the connection.</param>
            /// <param name="pConnectionParameters">
            ///     Pointer to a WLAN_CONNECTION_PARAMETERS structure that specifies the connection type, mode, network profile, SSID that identifies
            ///     the network, and other parameters.
            /// </param>
            /// <param name="pReserved">Reserved for future use. Must be set to NULL.</param>
            /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/nf-wlanapi-wlanconnect">WlanConnect</see>
            [DllImport(WLanApiDll, SetLastError = true)]
            public static extern uint WlanConnect(IntPtr hClientHandle, ref Guid pInterfaceGuid, ref WLAN_CONNECTION_PARAMETERS pConnectionParameters, IntPtr pReserved);
            /// <summary>
            ///     The WlanDisconnect function disconnects an interface from its current network.
            /// </summary>
            /// <param name="hClientHandle">The client's session handle, obtained by a previous call to the <see cref="WlanOpenHandle"/> function.</param>
            /// <param name="pInterfaceGuid">The GUID of the interface to be disconnected.</param>
            /// <param name="pReserved">Reserved for future use. Must be set to NULL.</param>
            /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/nf-wlanapi-wlandisconnect">WlanDisconnect</see>
            [DllImport(WLanApiDll, SetLastError = true)]
            public static extern uint WlanDisconnect(IntPtr hClientHandle, ref Guid pInterfaceGuid, IntPtr pReserved);
            /// <summary>
            ///     The WlanEnumInterfaces function enumerates all of the wireless LAN interfaces currently enabled on the local computer.
            /// </summary>
            /// <param name="hClientHandle">The client's session handle, obtained by a previous call to the <see cref="WlanOpenHandle"/> function.</param>
            /// <param name="pReserved">Reserved for future use. This parameter must be set to NULL.</param>
            /// <param name="ppInterfaceList">
            ///     A pointer to storage for a pointer to receive the returned list of wireless LAN interfaces in a WLAN_INTERFACE_INFO_LIST
            ///     structure. The buffer for the WLAN_INTERFACE_INFO_LIST returned is allocated by the <see cref="WlanEnumInterfaces"/> function if the call succeeds.
            /// </param>
            /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/nf-wlanapi-wlanenuminterfaces">WlanEnumInterfaces</see>
            [DllImport(WLanApiDll, EntryPoint = "WlanEnumInterfaces", SetLastError = true)]
            public static extern uint WlanEnumInterfaces([In] IntPtr hClientHandle, IntPtr pReserved, ref IntPtr ppInterfaceList);
            /// <summary>
            ///     The WlanFreeMemory function frees memory. Any memory returned from Native Wifi functions must be freed.
            /// </summary>
            /// <param name="pMemory">Pointer to the memory to be freed.</param>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/nf-wlanapi-wlanfreememory">WlanFreeMemory</see>
            [DllImport(WLanApiDll, EntryPoint = "WlanFreeMemory")]
            public static extern void WlanFreeMemory([In] IntPtr pMemory);
            /// <summary>
            ///     The WlanGetAvailableNetworkList function retrieves the list of available networks on a wireless LAN interface.
            /// </summary>
            /// <param name="hClientHandle">The client's session handle, obtained by a previous call to the WlanOpenHandle function.</param>
            /// <param name="pInterfaceGuid">A pointer to the GUID of the wireless LAN interface to be queried.</param>
            /// <param name="dwFlags">A set of flags that control the type of networks returned in the list.</param>
            /// <param name="pReserved">Reserved for future use. This parameter must be set to NULL.</param>
            /// <param name="ppAvailableNetworkList">
            ///     A pointer to storage for a pointer to receive the returned list of visible networks in a WLAN_AVAILABLE_NETWORK_LIST structure.
            /// </param>
            /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/nf-wlanapi-wlangetavailablenetworklist">WlanGetAvailableNetworkList</see>
            [DllImport(WLanApiDll, SetLastError = true)]
            public static extern uint WlanGetAvailableNetworkList(IntPtr hClientHandle, ref Guid pInterfaceGuid, WLAN_AVAILABLE_NETWORK_INCLUDE dwFlags, IntPtr pReserved, ref IntPtr ppAvailableNetworkList);
            /// <summary>
            ///     The WlanGetProfile function retrieves all information about a specified wireless profile.
            /// </summary>
            /// <param name="hClientHandle">The client's session handle, obtained by a previous call to the WlanOpenHandle function.</param>
            /// <param name="pInterfaceGuid">The GUID of the wireless interface.</param>
            /// <param name="strProfileName">
            ///     The name of the profile. Profile names are case-sensitive. This string must be NULL-terminated. The maximum length of the profile
            ///     name is 255 characters. This means that the maximum length of this string, including the NULL terminator, is 256 characters.
            /// </param>
            /// <param name="pReserved">Reserved for future use. Must be set to NULL.</param>
            /// <param name="pstrProfileXml">
            ///     A string that is the XML representation of the queried profile. There is no predefined maximum string length.
            /// </param>
            /// <param name="pdwFlags">
            ///     On input, a pointer to the address location used to provide additional information about the request. If this parameter is NULL on
            ///     input, then no information on profile flags will be returned. On output, a pointer to the address location used to receive profile flags.
            /// </param>
            /// <param name="pdwGrantedAccess"></param>
            /// <returns></returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/nf-wlanapi-wlangetprofile">WlanGetProfile</see>
            [DllImport(WLanApiDll, SetLastError = true)]
            public static extern uint WlanGetProfile(
                IntPtr hClientHandle, 
                [MarshalAs(UnmanagedType.LPStruct), In] Guid pInterfaceGuid, 
                [MarshalAs(UnmanagedType.LPWStr)] string strProfileName, 
                IntPtr pReserved, 
                [MarshalAs(UnmanagedType.LPWStr)] out string pstrProfileXml, 
                ref WLAN_PROFILE_FLAGS pdwFlags, 
                out WLAN_ACCESS_MASK pdwGrantedAccess);
            /// <summary>
            ///     The WlanGetProfileList function retrieves the list of profiles in preference order.
            /// </summary>
            /// <param name="hClientHandle">The client's session handle, obtained by a previous call to the WlanOpenHandle function.</param>
            /// <param name="pInterfaceGuid">The GUID of the wireless interface. A list of the GUIDs for wireless interfaces on the local computer can be retrieved using the WlanEnumInterfaces function.</param>
            /// <param name="pReserved">Reserved for future use. Must be set to NULL.</param>
            /// <param name="ppProfileList">A PWLAN_PROFILE_INFO_LIST structure that contains the list of profile information.</param>
            /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/nf-wlanapi-wlangetprofilelist">WlanGetProfileList</see>
            [DllImport(WLanApiDll, SetLastError = true)]
            public static extern int WlanGetProfileList(
                [In] IntPtr hClientHandle, 
                [In, MarshalAs(UnmanagedType.LPStruct)] Guid pInterfaceGuid, 
                [In] IntPtr pReserved, 
                [Out] out IntPtr ppProfileList);
            /// <summary>
            ///     The WlanOpenHandle function opens a connection to the server.
            /// </summary>
            /// <param name="dwClientVersion">The highest version of the WLAN API that the client supports.</param>
            /// <param name="pReserved">Reserved for future use. Must be set to NULL.</param>
            /// <param name="pdwNegotiatedVersion">
            ///     The version of the WLAN API that will be used in this session. This value is usually the highest version supported by both the
            ///     client and server.
            /// </param>
            /// <param name="ClientHandle">
            ///     A handle for the client to use in this session. This handle is used by other functions throughout the session.
            /// </param>
            /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/nf-wlanapi-wlanopenhandle">WlanOpenHandle</see>
            [DllImport(WLanApiDll, SetLastError = true)]
            public static extern uint WlanOpenHandle(uint dwClientVersion, IntPtr pReserved, [Out] out uint pdwNegotiatedVersion, out IntPtr ClientHandle);
            /// <summary>
            ///     The WlanQueryInterface function queries various parameters of a specified interface.
            /// </summary>
            /// <param name="hClientHandle">The client's session handle, obtained by a previous call to the WlanOpenHandle function.</param>
            /// <param name="pInterfaceGuid">The GUID of the interface to be queried.</param>
            /// <param name="OpCode">A WLAN_INTF_OPCODE value that specifies the parameter to be queried.</param>
            /// <param name="pReserved">Reserved for future use. Must be set to NULL.</param>
            /// <param name="pdwDataSize">The size of the ppData parameter, in bytes.</param>
            /// <param name="ppData">
            ///     Pointer to the memory location that contains the queried value of the parameter specified by the OpCode parameter.
            /// </param>
            /// <param name="pWlanOpcodeValueType">
            ///     If passed a non-NULL value, points to a WLAN_OPCODE_VALUE_TYPE value that specifies the type of opcode returned. This parameter
            ///     may be NULL.
            /// </param>
            /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/nf-wlanapi-wlanqueryinterface">WlanQueryInterface</see>
            [DllImport(WLanApiDll, SetLastError = true)]
            public static extern int WlanQueryInterface(
                [In] IntPtr hClientHandle, 
                [In, MarshalAs(UnmanagedType.LPStruct)] Guid pInterfaceGuid, 
                [In] WLAN_INTF_OPCODE OpCode, 
                [In, Out] IntPtr pReserved, 
                [Out] out int pdwDataSize, 
                [Out] out IntPtr ppData, 
                [Out] out WLAN_OPCODE_VALUE_TYPE pWlanOpcodeValueType);
            /// <summary>
            ///     The WlanReasonCodeToString function retrieves a string that describes a specified reason code.
            /// </summary>
            /// <param name="dwReasonCode">A WLAN_REASON_CODE value of which the string description is requested.</param>
            /// <param name="dwBufferSize">
            ///     The size of the buffer used to store the string, in WCHAR. If the reason code string is longer than the buffer, it will be
            ///     truncated and NULL-terminated. If dwBufferSize is larger than the actual amount of memory allocated to pStringBuffer, then an
            ///     access violation will occur in the calling program.
            /// </param>
            /// <param name="pStringBuffer">
            ///     Pointer to a buffer that will receive the string. The caller must allocate memory to pStringBuffer before calling WlanReasonCodeToString.
            /// </param>
            /// <param name="pReserved">Reserved for future use. Must be set to NULL.</param>
            /// <returns>WlanReasonCodeToString</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/nf-wlanapi-wlanreasoncodetostring">WlanReasonCodeToString</see>
            [DllImport(WLanApiDll, SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern uint WlanReasonCodeToString(uint dwReasonCode, uint dwBufferSize, StringBuilder pStringBuffer, IntPtr pReserved);
            /// <summary>
            ///     The WlanSetInterface function sets user-configurable parameters for a specified interface.
            /// </summary>
            /// <param name="hClientHandle">The client's session handle, obtained by a previous call to the WlanOpenHandle function.</param>
            /// <param name="pInterfaceGuid">The GUID of the interface to be configured.</param>
            /// <param name="OpCode">A WLAN_INTF_OPCODE value that specifies the parameter to be set.</param>
            /// <param name="dwDataSize">
            ///     The size of the pData parameter, in bytes. If dwDataSize is larger than the actual amount of memory allocated to pData, then an
            ///     access violation will occur in the calling program.
            /// </param>
            /// <param name="pData">
            ///     The value to be set as specified by the OpCode parameter. The type of data pointed to by pData must be appropriate for the
            ///     specified OpCode. Use the table above to determine the type of data to use.
            /// </param>
            /// <param name="pReserved">Reserved for future use. Must be set to NULL.</param>
            /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/nf-wlanapi-wlansetinterface">WlanSetInterface</see>
            [DllImport(WLanApiDll, SetLastError = true)]
            public static extern int WlanSetInterface(
                [In] IntPtr hClientHandle, 
                [In, MarshalAs(UnmanagedType.LPStruct)] Guid pInterfaceGuid, 
                [In] WLAN_INTF_OPCODE OpCode, 
                [In] uint dwDataSize, 
                [In] IntPtr pData, 
                [In, Out] IntPtr pReserved);
            /// <summary>
            ///     The WlanSetProfile function sets the content of a specific profile.
            /// </summary>
            /// <param name="hClientHandle">The client's session handle, obtained by a previous call to the WlanOpenHandle function.</param>
            /// <param name="pInterfaceGuid">The GUID of the interface.</param>
            /// <param name="dwFlags">The flags to set on the profile.</param>
            /// <param name="strProfileXml">
            ///     Contains the XML representation of the profile. The WLANProfile element is the root profile element. To view sample profiles, see
            ///     Wireless Profile Samples. There is no predefined maximum string length.
            /// </param>
            /// <param name="strAllUserProfileSecurity">
            ///     Sets the security descriptor string on the all-user profile. For more information about profile permissions, see the Remarks section.
            /// </param>
            /// <param name="bOverwrite">
            ///     Specifies whether this profile is overwriting an existing profile. If this parameter is FALSE and the profile already exists, the
            ///     existing profile will not be overwritten and an error will be returned.
            /// </param>
            /// <param name="pReserved">Reserved for future use. Must be set to NULL.</param>
            /// <param name="pdwReasonCode">A WLAN_REASON_CODE value that indicates why the profile is not valid.</param>
            /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/nf-wlanapi-wlansetprofile">WlanSetProfile</see>
            [DllImport(WLanApiDll, SetLastError = true)]
            public static extern int WlanSetProfile(
                [In] IntPtr hClientHandle, 
                [In, MarshalAs(UnmanagedType.LPStruct)] Guid pInterfaceGuid, 
                [In] WLAN_PROFILE_FLAGS dwFlags, 
                [In, MarshalAs(UnmanagedType.LPWStr)] string strProfileXml, 
                [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string strAllUserProfileSecurity, 
                [In] bool bOverwrite, 
                [In] IntPtr pReserved, 
                [Out] out WLAN_REASON_CODE pdwReasonCode);
            /// <summary>
            ///     The WlanScan function requests a scan for available networks on the indicated interface.
            /// </summary>
            /// <param name="hClientHandle">The client's session handle, obtained by a previous call to the WlanOpenHandle function.</param>
            /// <param name="pInterfaceGuid">The GUID of the interface to be queried.</param>
            /// <param name="pDot11Ssid">
            ///     A pointer to a DOT11_SSID structure that specifies the SSID of the network to be scanned. This parameter is optional. When set to
            ///     NULL, the returned list contains all available networks. Windows XP with SP3 and Wireless LAN API for Windows XP with SP2: This
            ///     parameter must be NULL.
            /// </param>
            /// <param name="pIeData">
            ///     A pointer to an information element to include in probe requests. This parameter points to a WLAN_RAW_DATA structure that may
            ///     include client provisioning availability information and 802.1X authentication requirements.Windows XP with SP3 and Wireless LAN
            ///     API for Windows XP with SP2: This parameter must be NULL.
            /// </param>
            /// <param name="pReserved">Reserved for future use. Must be set to NULL.</param>
            /// <returns>If the function succeeds, the return value is ERROR_SUCCESS.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/wlanapi/nf-wlanapi-wlanscan">WlanScan</see>
            [DllImport(WLanApiDll, SetLastError = true)]
            public static extern uint WlanScan(IntPtr hClientHandle, ref Guid pInterfaceGuid, IntPtr pDot11Ssid, IntPtr pIeData, IntPtr pReserved);
        }
    }
}
