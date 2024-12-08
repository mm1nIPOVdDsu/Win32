#if false
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using static Win32.Common.Unmanaged.WLanApi;

namespace Win32.Common.Services.Network
{
    // TODO: Add wireless event: https://github.com/emoacht/ManagedNativeWifi/blob/master/Source/ManagedNativeWifi/ConnectionChangedEventArgs.cs
    // TODO: Connect to network: https://github.com/emoacht/ManagedNativeWifi/blob/master/Source/ManagedNativeWifi/NativeWifi.cs#L644
    // TODO: Callback class: https://github.com/emoacht/ManagedNativeWifi/blob/master/Source/ManagedNativeWifi/Win32/BaseMethod.cs
    // Additional signatures: https://github.com/emoacht/ManagedNativeWifi/blob/master/Source/ManagedNativeWifi/Win32/NativeMethod.cs

    /// <summary>
    ///     
    /// </summary>
    public class WirelessNetworkService
    {
        #region Wireless Profiles
        // Profiles
        // Samples: https://learn.microsoft.com/en-us/windows/win32/nativewifi/wpa2-personal-profile-sample
        // Schema: https://learn.microsoft.com/en-us/windows/win32/nativewifi/wlan-profileschema-elements
        // NOTE: use string.Format() to inject the SSID and the Password
        private const string WPA2_PROFILE = "<?xml version=\"1.0\" encoding=\"US-ASCII\"?>" +
            "<WLANProfile xmlns=\"https://www.microsoft.com/networking/WLAN/profile/v1\">" +
                "<name>SampleWPA2PSK</name>" +
                "<SSIDConfig>" +
                    "<SSID>" +
                        "<name>{0}</name>" +
                    "</SSID>" +
                "</SSIDConfig>" +
                "<connectionType>ESS</connectionType>" +
                "<connectionMode>auto</connectionMode>" +
                "<autoSwitch>false</autoSwitch>" +
                "<MSM>" +
                    "<security>" +
                        "<authEncryption>" +
                            "<sharedKey>" +
                                "<keyType>passPhrase</keyType>" +
                                "<protected>false</protected>" +
                                "<keyMaterial>{1}</keyMaterial>" +
                            "</sharedKey>" +
                            "<authentication>WPA2PSK</authentication>" +
                            "<encryption>AES</encryption>" +
                            "<useOneX>false</useOneX>" +
                        "</authEncryption>" +
                    "</security>" +
                "</MSM>" +
            "</WLANProfile>";
        private const string WPA_PROFILE = "<?xml version=\"1.0\" encoding=\"US-ASCII\"?>" +
            "<WLANProfile xmlns=\"https://www.microsoft.com/networking/WLAN/profile/v1\">" +
                "<name>SampleWPAPSK</name>" +
                "<SSIDConfig>" +
                    "<SSID>" +
                        "<name>{0}</name>" +
                    "</SSID>" +
                "</SSIDConfig>" +
                "<connectionType>ESS</connectionType>" +
                "<connectionMode>auto</connectionMode>" +
                "<autoSwitch>false</autoSwitch>" +
                "<MSM>" +
                    "<security>" +
                        "<authEncryption>" +
                            "<sharedKey>" +
                                "<keyType>passPhrase</keyType>" +
                                "<protected>false</protected>" +
                                "<keyMaterial>{0}</keyMaterial>" +
                            "</sharedKey>" +
                            "<authentication>WPAPSK</authentication>" +
                            "<encryption>TKIP</encryption>" +
                            "<useOneX>false</useOneX>" +
                        "</authEncryption>" +
                    "</security>" +
                "</MSM>" +
            "</WLANProfile>";
        #endregion

        private readonly ILogger<WirelessNetworkService> _logger;

        private WirelessInterfaceInfo? _interfaceInfo;

        /// <summary>
        ///     
        /// </summary>
        /// <param name="logger"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public WirelessNetworkService(ILogger<WirelessNetworkService> logger)
        {
            _logger= logger is not null ? logger : throw new ArgumentNullException(nameof(logger));
        }

        ////https://github.com/cveld/ManagedWifi/blob/master/WifiExample/WifiExample.cs
        ////https://github.com/cveld/ManagedWifi/tree/master/ManagedWifi
        //// get list of connected networks: https://github.com/emoacht/ManagedNativeWifi/blob/master/Source/ManagedNativeWifi.Simple/NativeWifi.cs#L417
        //// https://github.com/emoacht/ManagedNativeWifi/blob/master/Source/ManagedNativeWifi.Simple/NativeWifi.cs#L417
        /////https://github.com/DigiExam/simplewifi
        /////https://github.com/DigiExam/simplewifi/tree/example
        ///// - https://github.com/DigiExam/simplewifi/blob/master/SimpleWifi/AuthRequest.cs
        //public bool ConnectToNetwork(Guid interfaceId, string ssid, SecureString password)
        //{
        //    if (Guid.Empty == interfaceId)
        //        throw new ArgumentException("The wireless interface ID must be a valid guid.");
        //    if (string.IsNullOrEmpty(ssid))
        //        throw new ArgumentNullException(nameof(ssid));

        //    var clientHandle = IntPtr.Zero;
        //    var interfacesHandle = IntPtr.Zero;

        //    try
        //    {
        //        if (WlanOpenHandle(2, IntPtr.Zero, out var negotiatedVersion, out clientHandle) is not 0)
        //            throw new Win32Exception(Marshal.GetLastWin32Error());

        //        var connectionParams = new WLAN_CONNECTION_PARAMETERS
        //        {
        //            wlanConnectionMode = WLAN_CONNECTION_MODE.wlan_connection_mode_profile,
        //            strProfile = ssid,
        //            dot11BssType = Dot11BssType.Any,
        //            dot11PhyType = Dot11PhyType.Any,
        //            dwFlags = 0
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while trying to connect to the wireless network {networkName}.", ssid);
        //        throw;
        //    }

        //    var wlanConnectionParameters = new WLAN_CONNECTION_PARAMETERS
        //    {
        //        dot11BssType = DOT11_BSS_TYPE.dot11_BSS_type_any,
        //        dwFlags = 0,
        //        strProfile = "dlink",
        //        wlanConnectionMode = WLAN_CONNECTION_MODE.wlan_connection_mode_auto
        //    };
        //    WlanConnect(ClientHandle, ref pInterfaceGuid, ref wlanConnectionParameters, new IntPtr());
        //}

        /// <summary>
        ///     Gets a list of available wireless adapters.
        /// </summary>
        /// <returns><see cref="List{WirelessInterfaceInfo}"/></returns>
        public List<WirelessInterfaceInfo> GetWirelessInterfaces() 
        {
            var clientHandle = IntPtr.Zero;
            var interfacesHandle = IntPtr.Zero;

            // NOTE: If using windows API ->
            //       var result = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(WiFiAdapter.GetDeviceSelector());

            try
            {
                if (WlanOpenHandle(2, IntPtr.Zero, out var negotiatedVersion, out clientHandle) is not 0)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                if(WlanEnumInterfaces(clientHandle, IntPtr.Zero, ref interfacesHandle) is not 0)
                        throw new Win32Exception(Marshal.GetLastWin32Error());

                var wlanInterfaces = new WLAN_INTERFACE_INFO_LIST(interfacesHandle);
                var interfaces = new List<WirelessInterfaceInfo>();
                for(var i = 0; i < wlanInterfaces.dwNumberOfItems - 1; i++)
                {
                    var wlanInterface = wlanInterfaces.InterfaceInfo[i];
                    interfaces.Add(new WirelessInterfaceInfo()
                    {
                        Description = wlanInterface.strInterfaceDescription,
                        Id = wlanInterface.InterfaceGuid,
                        State = (WirelessInterfaceState)wlanInterface.isState
                    });
                }

                return interfaces;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting network interfaces.");
                throw;
            }
            finally 
            {
                if(clientHandle != IntPtr.Zero)
                    WlanCloseHandle(clientHandle, IntPtr.Zero);
                if (interfacesHandle != IntPtr.Zero)
                    WlanFreeMemory(interfacesHandle);
            }
        }
        /// <summary>
        ///     Gets a list of available networks for a wireless interface.
        /// </summary>
        /// <param name="interfaceInfo"></param>
        /// <returns><see cref="List{WLAN_AVAILABLE_NETWORK}"/></returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="interfaceInfo"/> is null.</exception>
        /// <exception cref="Exception">Thrown if <see cref="WirelessInterfaceInfo.Id"/> is <see cref="Guid.Empty"/>.</exception>
        public List<WirelessNetworkInfo> GetAvailableNetworks(WirelessInterfaceInfo interfaceInfo) 
        {
            if(interfaceInfo is null)
                throw new ArgumentNullException(nameof(interfaceInfo));
            if (interfaceInfo.Id == Guid.Empty)
                throw new Exception("Adapter Guid cannot be null.");

            var clientHandle = IntPtr.Zero;
            var networksHandle = IntPtr.Zero;
            var adapterId = interfaceInfo.Id;
            try
            {
                if (WlanOpenHandle(2, IntPtr.Zero, out var negotiatedVersion, out clientHandle) is not 0)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                if (WlanGetAvailableNetworkList(clientHandle, ref adapterId, WLAN_AVAILABLE_NETWORK_INCLUDE.WLAN_AVAILABLE_NETWORK_INCLUDE_ALL_ADHOC_PROFILES, IntPtr.Zero, ref networksHandle) is not 0)
                    throw new Win32Exception(Marshal.GetLastWin32Error());

                var adapterNetworks = new WLAN_AVAILABLE_NETWORK_LIST(networksHandle);
                var networks = new List<WirelessNetworkInfo>();
                for (var i = 0; i < adapterNetworks.dwNumberOfItems - 1; i++)
                {
                    var adapterNetwork = adapterNetworks.wlanAvailableNetwork[i];
                    networks.Add(new WirelessNetworkInfo()
                    {
                        Connectable = adapterNetwork.bNetworkConnectable,
                        DefaultAuthAlgorithm = adapterNetwork.dot11DefaultAuthAlgorithm.ToString(),
                        DefaultCipherAlgorithm = adapterNetwork.dot11DefaultCipherAlgorithm.ToString(),
                        Name = adapterNetwork.strProfileName,
                        NotConnectableReason = adapterNetwork.wlanNotConnectableReason.ToString(),
                        SecurityEnabled = adapterNetwork.bSecurityEnabled,
                        SignalQuality = adapterNetwork.wlanSignalQuality,
                        SSID = adapterNetwork.dot11Ssid.ucSSID
                    });
                }
                return networks;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting available networks.");
                throw;
            }
            finally
            {
                if (clientHandle != IntPtr.Zero)
                    WlanCloseHandle(clientHandle, IntPtr.Zero);
                if (networksHandle != IntPtr.Zero)
                    WlanFreeMemory(networksHandle);
            }
        }
        /// <summary>
        ///     Gets a list of available networks for a wireless interface.
        /// </summary>
        /// <param name="adapterId">The <see cref="Guid"/> id of the wireless interface.</param>
        /// <returns><see cref="List{WLAN_AVAILABLE_NETWORK}"/></returns>
        /// <exception cref="Exception"></exception>
        public List<WirelessNetworkInfo> GetAvailableNetworks(Guid adapterId) 
        {
            if (adapterId == Guid.Empty)
                throw new ArgumentException("Adapter Guid cannot be null.");

            var clientHandle = IntPtr.Zero;
            var networksHandle = IntPtr.Zero;
            try
            {
                if (WlanOpenHandle(2, IntPtr.Zero, out var negotiatedVersion, out clientHandle) is not 0)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                if (WlanGetAvailableNetworkList(clientHandle, ref adapterId, WLAN_AVAILABLE_NETWORK_INCLUDE.WLAN_AVAILABLE_NETWORK_INCLUDE_ALL_ADHOC_PROFILES, IntPtr.Zero, ref networksHandle) is not 0)
                    throw new Win32Exception(Marshal.GetLastWin32Error());

                var adapterNetworks = new WLAN_AVAILABLE_NETWORK_LIST(networksHandle);
                var networks = new List<WirelessNetworkInfo>();
                for (var i = 0; i < adapterNetworks.dwNumberOfItems - 1; i++)
                {
                    var adapterNetwork = adapterNetworks.wlanAvailableNetwork[i];
                    networks.Add(new WirelessNetworkInfo()
                    {
                        Connectable = adapterNetwork.bNetworkConnectable,
                        DefaultAuthAlgorithm = adapterNetwork.dot11DefaultAuthAlgorithm.ToString(),
                        DefaultCipherAlgorithm = adapterNetwork.dot11DefaultCipherAlgorithm.ToString(),
                        Name = adapterNetwork.strProfileName,
                        NotConnectableReason = adapterNetwork.wlanNotConnectableReason.ToString(),
                        SecurityEnabled = adapterNetwork.bSecurityEnabled,
                        SignalQuality = adapterNetwork.wlanSignalQuality,
                        SSID = adapterNetwork.dot11Ssid.ucSSID
                    });
                }
                return networks;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting available networks.");
                throw;
            }
            finally
            {
                if (clientHandle != IntPtr.Zero)
                    WlanCloseHandle(clientHandle, IntPtr.Zero);
                if (networksHandle != IntPtr.Zero)
                    WlanFreeMemory(networksHandle);
            }

        }
        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Win32Exception"></exception>
        public IEnumerable<string> GetConnectedNetworkSsids()
        {
            var clientHandle = IntPtr.Zero;
            var interfaceList = IntPtr.Zero;
            var queryData = IntPtr.Zero;

            try
            {
                if (WlanOpenHandle(2, IntPtr.Zero, out var negotiatedVersion, out clientHandle) is not 0)
                    throw new Win32Exception(Marshal.GetLastWin32Error());

                if (WlanEnumInterfaces(clientHandle, IntPtr.Zero, ref interfaceList) is not 0)
                    yield break;

                var interfaceInfoList = new WLAN_INTERFACE_INFO_LIST(interfaceList);

                _logger.LogDebug("Found {networkCount} wireless interfaces.", interfaceInfoList.InterfaceInfo.Length);
                foreach (var interfaceInfo in interfaceInfoList.InterfaceInfo)
                {
                    _logger.LogDebug("Querying network interface for connected network.", interfaceInfo);
                    if (WlanQueryInterface(clientHandle,interfaceInfo.InterfaceGuid, WLAN_INTF_OPCODE.wlan_intf_opcode_current_connection, IntPtr.Zero, out var dataSize, out queryData, out var opcodeValueType) is not 0)
                        continue;

                    var connection = Marshal.PtrToStructure<WLAN_CONNECTION_ATTRIBUTES>(queryData);
                    if (connection.isState != WLAN_INTERFACE_STATE.wlan_interface_state_connected)
                    {
                        _logger.LogDebug("Network interface is not connected to network.", interfaceInfo);
                        continue;
                    }

                    var association = connection.wlanAssociationAttributes;
                    Console.WriteLine("Interface: {0}, SSID: {1}, BSSID: {2}, Signal: {3}",
                        interfaceInfo.strInterfaceDescription,
                        association.dot11Ssid,
                        association.dot11Bssid,
                        association.wlanSignalQuality);

                    var ssid = association.dot11Ssid.ToString();
                    if (string.IsNullOrEmpty(ssid))
                        continue;

                    yield return ssid;
                }
            }
            finally
            {
                if (queryData != IntPtr.Zero)
                    WlanFreeMemory(queryData);
                if (interfaceList != IntPtr.Zero)
                    WlanFreeMemory(interfaceList);
                if (clientHandle != IntPtr.Zero)
                    WlanCloseHandle(clientHandle, IntPtr.Zero);
            }
        }

        //public void Asdf()
        //{
        //    IntPtr ppAvailableNetworkList = new IntPtr();
        //    Guid pInterfaceGuid = ((WLAN_INTERFACE_INFO)wlanInterfaceInfoList.InterfaceInfo[0]).InterfaceGuid;
        //    WlanGetAvailableNetworkList(ClientHandle, ref pInterfaceGuid, WLAN_AVAILABLE_NETWORK_INCLUDE_ALL_MANUAL_HIDDEN_PROFILES, new IntPtr(), ref ppAvailableNetworkList);
        //    WLAN_AVAILABLE_NETWORK_LIST wlanAvailableNetworkList = new WLAN_AVAILABLE_NETWORK_LIST(ppAvailableNetworkList);

        //    for (var i = 0; i < wlanAvailableNetworkList.dwNumberOfItems; i++)
        //    {
        //        WLAN_AVAILABLE_NETWORK network = wlanAvailableNetworkList.wlanAvailableNetwork[i];
        //        Console.WriteLine("Available Network: ");
        //        Console.WriteLine("SSID: " + network.dot11Ssid.ucSSID);
        //        Console.WriteLine("Encrypted: " + network.bSecurityEnabled);
        //        Console.WriteLine("Signal Strength: " + network.wlanSignalQuality);
        //        Console.WriteLine("Default Authentication: " + network.dot11DefaultAuthAlgorithm.ToString());
        //        Console.WriteLine("Default Cipher: " + network.dot11DefaultCipherAlgorithm.ToString());
        //        Console.WriteLine();
        //    }
        //}
    }
}
#endif