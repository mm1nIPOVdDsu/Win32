using System;
using System.Runtime.InteropServices;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <inheritdoc/>
        public partial class BthProps
        {
            /// <summary>
            ///     Bluetooth interactions.
            /// </summary>
            public partial class BluetoothApis
            {
                /// <summary>
                ///     The BluetoothAuthenticateDeviceEx function sends an authentication request to a remote Bluetooth device. Additionally, this function allows for out-of-band data to be passed into the function call for the device being authenticated.
                /// </summary>
                /// <param name="hwndParentIn">The window to parent the authentication wizard. If NULL, the wizard will be parented off the desktop.</param>
                /// <param name="hRadioIn">A valid local radio handle or NULL. If NULL, then all radios will be tried. If any of the radios succeed, then the call will succeed.</param>
                /// <param name="pbtdiInout">A pointer to a BLUETOOTH_DEVICE_INFO structure describing the device being authenticated.</param>
                /// <param name="pbtOobData">Pointer to device specific out-of-band data to be provided with this API call. If NULL, then a UI is displayed to continue the authentication process. If not NULL, no UI is displayed.</param>
                /// <param name="authenticationRequirement">An BLUETOOTH_AUTHENTICATION_REQUIREMENTS value that specifies the protection required for authentication.</param>
                /// <returns>Returns ERROR_SUCCESS upon successful completion.</returns>
                [DllImport(BthPropsCpl, SetLastError = true)]
                public static extern uint BluetoothAuthenticateDeviceEx(IntPtr hwndParentIn, IntPtr hRadioIn, ref BLUETOOTH_DEVICE_INFO pbtdiInout, BLUETOOTH_OOB_DATA_INFO pbtOobData, uint authenticationRequirement);
                /// <summary>
                ///     The BluetoothEnableIncomingConnections function modifies whether a local Bluetooth radio accepts incoming connections.
                /// </summary>
                /// <param name="hRadio">Valid local radio handle for which to change whether incoming connections are enabled, or NULL. If NULL, the attempt to modify incoming connection acceptance iterates through all local radios; if any radio is modified by the call, the function call succeeds.</param>
                /// <param name="fEnable">Flag specifying whether incoming connection acceptance is to be enabled or disabled. Set to TRUE to enable incoming connections, set to FALSE to disable incoming connections.</param>
                /// <returns>Returns TRUE if the incoming connection state was successfully changed. If hRadio is NULL, a return value of TRUE indicates that at least one local radio state was successfully changed. Returns FALSE if incoming connection state was not changed; if hRadio was NULL, no radio accepted the state change.</returns>
                [DllImport(IrPropsCpl, SetLastError = true)]
                public static extern bool BluetoothEnableIncomingConnection(IntPtr hRadio, bool fEnable);
                /// <summary>
                ///     The BluetoothFindDeviceClose function closes an enumeration handle associated with a device query.
                /// </summary>
                /// <param name="hFind">Handle for the query to be closed. Obtained in a previous call to the BluetoothFindFirstDevice function.</param>
                /// <returns>Returns TRUE when the handle is successfully closed. Returns FALSE upon error. Call the GetLastError function for more information on the failure.</returns>
                [DllImport(IrPropsCpl, SetLastError = true)]
                public static extern bool BluetoothFindDeviceClose(IntPtr hFind);
                /// <summary>
                ///     The BluetoothFindFirstDevice function begins the enumeration Bluetooth devices.
                /// </summary>
                /// <param name="searchParams">Pointer to a BLUETOOTH_DEVICE_SEARCH_PARAMS structure. The dwSize member of the BLUETOOTH_DEVICE_SEARCH_PARAMS structure pointed to by pbtsp must match the size of the structure.</param>
                /// <param name="deviceInfo">Pointer to a BLUETOOTH_DEVICE_INFO structure into which information about the first Bluetooth device found is placed. The dwSize member of the BLUETOOTH_DEVICE_INFO structure pointed to by pbtdi must match the size of the structure, or the call to the BluetoothFindFirstDevice function fails.</param>
                /// <returns>Returns a valid handle to the first Bluetooth device upon successful completion, and the pbtdi parameter points to information about the device. When this handle is no longer needed, it must be closed via the BluetoothFindDeviceClose.</returns>
                [DllImport(IrPropsCpl, SetLastError = true)]
                public static extern IntPtr BluetoothFindFirstDevice(ref BLUETOOTH_DEVICE_SEARCH_PARAMS searchParams, ref BLUETOOTH_DEVICE_INFO deviceInfo);
                /// <summary>
                ///     The BluetoothFindFirstRadio function begins the enumeration of local Bluetooth radios.
                /// </summary>
                /// <param name="pbtfrp">Pointer to a BLUETOOTH_FIND_RADIO_PARAMS structure. The dwSize member of the BLUETOOTH_FIND_RADIO_PARAMS structure pointed to by pbtfrp must match the size of the structure.</param>
                /// <param name="phRadio">Pointer to where the first enumerated radio handle will be returned. When no longer needed, this handle must be closed via CloseHandle.</param>
                /// <returns>In addition to the handle indicated by phRadio, calling this function will also create a HBLUETOOTH_RADIO_FIND handle for use with the BluetoothFindNextRadio function. When this handle is no longer needed, it must be closed via the BluetoothFindRadioClose.</returns>
                [DllImport(IrPropsCpl, SetLastError = true)]
                public static extern IntPtr BluetoothFindFirstRadio(ref BLUETOOTH_FIND_RADIO_PARAMS pbtfrp, out IntPtr phRadio);
                /// <summary>
                ///     The BluetoothFindNextDevice function finds the next Bluetooth device.
                /// </summary>
                /// <param name="hFind">Handle for the query obtained in a previous call to the BluetoothFindFirstDevice function.</param>
                /// <param name="pbtdi">Pointer to a BLUETOOTH_DEVICE_INFO structure into which information about the next Bluetooth device found is placed. The dwSize member of the BLUETOOTH_DEVICE_INFO structure pointed to by pbtdi must match the size of the structure, or the call to BluetoothFindNextDevice fails.</param>
                /// <returns>Returns TRUE when the next device is successfully found, and the pbtdi parameter points to information about the device.</returns>
                [DllImport(IrPropsCpl, SetLastError = true)]
                public static extern bool BluetoothFindNextDevice(IntPtr hFind, ref BLUETOOTH_DEVICE_INFO pbtdi);
                /// <summary>
                ///     The BluetoothFindNextRadio function finds the next Bluetooth radio.
                /// </summary>
                /// <param name="hFind">Handle returned by a previous call to the BluetoothFindFirstRadio function. Use BluetoothFindRadioClose to close this handle when it is no longer needed.</param>
                /// <param name="phRadio">Pointer to where the next enumerated radio handle will be returned. When no longer needed, this handle must be released via CloseHandle.</param>
                /// <returns>Returns TRUE when the next available radio is found. Returns FALSE when no new radios are found.Call the GetLastError function for more information on the error. </returns>
                [DllImport(IrPropsCpl, SetLastError = true)]
                public static extern bool BluetoothFindNextRadio(IntPtr hFind, out IntPtr phRadio);
                /// <summary>
                ///     The BluetoothFindRadioClose function closes the enumeration handle associated with finding Bluetooth radios.
                /// </summary>
                /// <param name="hFind">Enumeration handle to close, obtained with a previous call to the BluetoothFindFirstRadio function.</param>
                /// <returns>True if successful.</returns>
                [DllImport(IrPropsCpl, SetLastError = true)]
                public static extern bool BluetoothFindRadioClose(IntPtr hFind);
                /// <summary>
                ///     The BluetoothGetDeviceInfo function retrieves information about a remote Bluetooth device. The Bluetooth 
                ///     device must have been previously identified through a successful device inquiry function call.
                /// </summary>
                /// <param name="hRadio">
                ///     A handle to a local radio, obtained from a call to the BluetoothFindFirstRadio or similar functions, 
                ///     or from a call to the SetupDiEnumerateDeviceInterfaces function.
                /// </param>
                /// <param name="pbtdi">
                ///     A pointer to a BLUETOOTH_DEVICE_INFO structure into which data about the first Bluetooth device will be 
                ///     placed. For more information, see Remarks.
                /// </param>
                /// <remarks>
                ///     The Bluetooth device for which data is obtained must have been previously identified through a successful 
                ///     device inquiry function call.
                ///     
                ///     In the <see cref="BLUETOOTH_DEVICE_INFO"/> structure pointed to by pbtdi, the dwSize member must be equivalent to the 
                ///     size, in bytes, of the structure. The Address member of the BLUETOOTH_DEVICE_INFO structure must 
                ///     contain the Bluetooth address of the remote device.
                /// </remarks>
                /// <returns>
                ///     Returns ERROR_SUCCESS upon success, indicating that data about the remote Bluetooth device was 
                ///     retrieved. Returns error codes upon failure. The following table lists common error codes 
                ///     associated with the BluetoothGetDeviceInfo function.
                /// </returns>
                [DllImport(IrPropsCpl, SetLastError = true)]
                public static extern uint BluetoothGetDeviceInfo(IntPtr hRadio, ref BLUETOOTH_DEVICE_INFO pbtdi);
                /// <summary>
                ///     The BluetoothGetRadioInfo function obtains information about a Bluetooth radio.
                /// </summary>
                /// <param name="hRadio">A handle to a local Bluetooth radio, obtained by calling the BluetoothFindFirstRadio or similar functions, or the SetupDiEnumerateDeviceInterfances function.</param>
                /// <param name="pRadioInfo">A pointer to a BLUETOOTH_RADIO_INFO structure into which information about the radio will be placed. The dwSize member of the BLUETOOTH_RADIO_INFO structure must match the size of the structure.</param>
                /// <returns>Response code.</returns>
                [DllImport(IrPropsCpl, SetLastError = true)]
                public static extern UInt32 BluetoothGetRadioInfo(IntPtr hRadio, ref BLUETOOTH_RADIO_INFO pRadioInfo);
            }
        }
    }
}
