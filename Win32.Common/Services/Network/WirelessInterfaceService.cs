#if false
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

using Microsoft.Extensions.Logging;

using static Win32.Common.Unmanaged.WLanApi;

namespace Win32.Common.Services.Network
{
    /// <summary>
    ///     
    /// </summary>
    public class WirelessInterfaceService : IWirelessInterfaceService
    {
        private readonly ILogger<WirelessInterfaceService> _logger;

        /// <summary>
        ///     
        /// </summary>
        /// <param name="logger"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public WirelessInterfaceService(ILogger<WirelessInterfaceService> logger) 
        {
            if(logger is null)
                throw new ArgumentNullException(nameof(logger));

            _logger = logger;
        }

        /// <summary>
        ///     Gets a list of available wireless adapters.
        /// </summary>
        /// <returns><see cref="List{WirelessInterfaceInfo}"/></returns>
        public List<WirelessInterfaceInfo> GetWirelessInterfaces() 
        {
#if WINDOWS
            // NOTE: If using windows API ->
            //       var currentConnection = NetworkInformation.GetInternetConnectionProfile();
            //       var results = await DeviceInformation.FindAllAsync(WiFiAdapter.GetDeviceSelector());
            //       var adapter = results[0];
            //       var adapter = await WiFiAdapter.FromIdAsync(result.Id);
            //       await adapter.ScanAsync();
            //       var availableNetworks = adapter.NetworkReport.AvailableNetworks;
            //       var connected = await adapter.ConnectAsync(availableNetwork, WiFiReconnectionKind.Automatic, new Windows.Security.Credentials.PasswordCredential("", "", ""));
#elif NET6_0_OR_GREATER
            var clientHandle = IntPtr.Zero;
            var interfacesHandle = IntPtr.Zero;
            try
            {
                if (WlanOpenHandle(2, IntPtr.Zero, out var negotiatedVersion, out clientHandle) is not 0)
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                if (WlanEnumInterfaces(clientHandle, IntPtr.Zero, ref interfacesHandle) is not 0)
                    throw new Win32Exception(Marshal.GetLastWin32Error());

                var wlanInterfaces = new WLAN_INTERFACE_INFO_LIST(interfacesHandle);
                var interfaces = new List<WirelessInterfaceInfo>();
                for (var i = 0; i < wlanInterfaces.dwNumberOfItems - 1; i++)
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
                if (clientHandle != IntPtr.Zero)
                    WlanCloseHandle(clientHandle, IntPtr.Zero);
                if (interfacesHandle != IntPtr.Zero)
                    WlanFreeMemory(interfacesHandle);
            }
#elif ANDROID
#endif

        }
    }

    /// <summary>
    ///     
    /// </summary>
    public interface IWirelessInterfaceService
    {
        /// <summary>
        ///     Gets a list of available wireless adapters.
        /// </summary>
        /// <returns><see cref="List{WirelessInterfaceInfo}"/></returns>
        List<WirelessInterfaceInfo> GetWirelessInterfaces();
    }
}
#endif