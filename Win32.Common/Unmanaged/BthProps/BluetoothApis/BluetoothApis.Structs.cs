using System;
using System.Runtime.InteropServices;

using static Win32.Common.Unmanaged.Shared;

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
                ///     The BLUETOOTH_DEVICE_INFO structure provides information about a Bluetooth device.
                /// </summary>
                [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
                public struct BLUETOOTH_DEVICE_INFO
                {
                    /// <summary>
                    ///     Size of the BLUETOOTH_DEVICE_INFO structure, in bytes.
                    /// </summary>
                    public UInt32 dwSize;
                    /// <summary>
                    ///     Address of the device.
                    /// </summary>
                    public UInt64 Address;
                    /// <summary>
                    ///     Class of the device.
                    /// </summary>
                    public uint ulClassofDevice;
                    /// <summary>
                    ///     Specifies whether the device is connected.
                    /// </summary>
                    public bool fConnected;
                    /// <summary>
                    ///     Specifies whether the device is a remembered device. Not all remembered devices are authenticated.
                    /// </summary>
                    public bool fRemembered;
                    /// <summary>
                    ///     Specifies whether the device is authenticated, paired, or bonded. All authenticated devices are remembered.
                    /// </summary>
                    public bool fAuthenticated;
                    /// <summary>
                    ///     Last time the device was seen, in the form of a <see cref="SYSTEMTIME"/> structure.
                    /// </summary>
                    public SYSTEMTIME stLastSeen;
                    /// <summary>
                    ///     Last time the device was used, in the form of a <see cref="SYSTEMTIME"/> structure.
                    /// </summary>
                    public SYSTEMTIME stLastUsed;
                    /// <summary>
                    ///     Name of the device.
                    /// </summary>
                    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 248)]
                    public string szName;

                    /// <summary>
                    ///     Initializes the structure.
                    /// </summary>
                    public void Initialize() => dwSize = (uint)Marshal.SizeOf(typeof(BLUETOOTH_DEVICE_INFO));
                }
                /// <summary>
                ///     The BLUETOOTH_DEVICE_SEARCH_PARAMS structure specifies search criteria for Bluetooth device searches.
                /// </summary>
                [StructLayout(LayoutKind.Sequential)]
                public struct BLUETOOTH_DEVICE_SEARCH_PARAMS
                {
                    /// <summary>
                    ///     The size, in bytes, of the structure.
                    /// </summary>
                    public UInt32 dwSize;
                    /// <summary>
                    ///     A value that specifies that the search should return authenticated Bluetooth devices.
                    /// </summary>
                    public bool fReturnAuthenticated;
                    /// <summary>
                    ///     A value that specifies that the search should return remembered Bluetooth devices.
                    /// </summary>
                    public bool fReturnRemembered;
                    /// <summary>
                    ///     A value that specifies that the search should return unknown Bluetooth devices.
                    /// </summary>
                    public bool fReturnUnknown;
                    /// <summary>
                    ///     A value that specifies that the search should return connected Bluetooth devices.
                    /// </summary>
                    public bool fReturnConnected;
                    /// <summary>
                    ///     A value that specifies that a new inquiry should be issued.
                    /// </summary>
                    public bool fIssueInquiry;
                    /// <summary>
                    ///     A value that indicates the time out for the inquiry, expressed in increments of 1.28 seconds. For example, an inquiry of 12.8 seconds has a cTimeoutMultiplier value of 10. The maximum value for this member is 48. When a value greater than 48 is used, the calling function immediately fails and returns E_INVALIDARG.
                    /// </summary>
                    public byte cTimeoutMultiplier;
                    /// <summary>
                    ///     A handle for the radio on which to perform the inquiry. Set to NULL to perform the inquiry on all local Bluetooth radios.
                    /// </summary>
                    public IntPtr hRadio;

                    /// <summary>
                    ///     Initializes the struct.
                    /// </summary>
                    public void Initialize() => dwSize = (uint)Marshal.SizeOf(typeof(BLUETOOTH_DEVICE_SEARCH_PARAMS));
                }
                /// <summary>
                ///     The BLUETOOTH_FIND_RADIO_PARAMS structure facilitates enumerating installed Bluetooth radios.
                /// </summary>
                [StructLayout(LayoutKind.Sequential)]
                public struct BLUETOOTH_FIND_RADIO_PARAMS
                {
                    public BLUETOOTH_FIND_RADIO_PARAMS() => dwSize = (UInt32)Marshal.SizeOf(typeof(BLUETOOTH_FIND_RADIO_PARAMS));

                    /// <summary>
                    ///     Initializes the struct.
                    /// </summary>
                    public UInt32 dwSize;
                }
                /// <summary>
                ///     The BLUETOOTH_OOB_DATA_INFO structure contains data used to authenticate prior to establishing an Out-of-Band device pairing.
                /// </summary>
                public struct BLUETOOTH_OOB_DATA_INFO
                {
                    /// <summary>
                    ///     A 128-bit cryptographic key used for two-way authentication.
                    /// </summary>
                    public byte[] C;
                    /// <summary>
                    ///     A randomly generated number used for one-way authentication. If this number is 
                    ///     not provided by the device initiating the OOB session, this value is 0.
                    /// </summary>
                    public byte[] R;
                }

                /// <summary>
                ///     The BLUETOOTH_RADIO_INFO structure contains information about a Bluetooth radio.
                /// </summary>
                [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
                public struct BLUETOOTH_RADIO_INFO
                {
                    /// <summary>
                    ///     Size, in bytes, of the structure.
                    /// </summary>
                    internal UInt32 dwSize;
                    /// <summary>
                    ///     Address of the local Bluetooth radio.
                    /// </summary>
                    internal UInt64 address;
                    /// <summary>
                    ///     Name of the local Bluetooth radio.
                    /// </summary>
                    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = BLUETOOTH_MAX_NAME_SIZE)]
                    internal string szName;
                    /// <summary>
                    ///     Device class for the local Bluetooth radio.
                    /// </summary>
                    internal UInt32 ulClassOfDevice;
                    /// <summary>
                    ///     This member contains data specific to individual Bluetooth device manufacturers.
                    /// </summary>
                    internal UInt16 lmpSubversion;
                    /// <summary>
                    ///     Manufacturer of the Bluetooth radio, expressed as a BTH_MFG_Xxx value. For more information about the Bluetooth assigned numbers document and a current list of values, see the Bluetooth specification at www.bluetooth.com.
                    /// </summary>
                    internal UInt16 manufacturer;

                    /// <summary>
                    ///     Initializes the struct.
                    /// </summary>
                    internal void Initialize() => dwSize = (UInt32)Marshal.SizeOf(typeof(BLUETOOTH_FIND_RADIO_PARAMS));
                }
            }
        }
    }
}
