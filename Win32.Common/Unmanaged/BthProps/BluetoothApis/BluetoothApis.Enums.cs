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
                ///     The BLUETOOTH_AUTHENTICATION_REQUIREMENTS enumeration specifies the 'Man in the Middle' protection required for authentication.
                /// </summary>
                public enum BLUETOOTH_AUTHENTICATION_REQUIREMENTS : uint
                {
                    /// <summary>
                    ///     Protection against a "Man in the Middle" attack is not required for authentication.
                    /// </summary>
                    BLUETOOTH_MITM_ProtectionNotRequired = 0,
                    /// <summary>
                    ///     Protection against a "Man in the Middle" attack is required for authentication.
                    /// </summary>
                    BLUETOOTH_MITM_ProtectionRequired = 0x1,
                    /// <summary>
                    ///     Protection against a "Man in the Middle" attack is not required for bonding.
                    /// </summary>
                    BLUETOOTH_MITM_ProtectionNotRequiredBonding = 0x2,
                    /// <summary>
                    ///     Protection against a "Man in the Middle" attack is required for bonding.
                    /// </summary>
                    BLUETOOTH_MITM_ProtectionRequiredBonding = 0x3,
                    /// <summary>
                    ///     Protection against a "Man in the Middle" attack is not required for General Bonding.
                    /// </summary>
                    BLUETOOTH_MITM_ProtectionNotRequiredGeneralBonding = 0x4,
                    /// <summary>
                    ///     Protection against a "Man in the Middle" attack is required for General Bonding.
                    /// </summary>
                    BLUETOOTH_MITM_ProtectionRequiredGeneralBonding = 0x5,
                    /// <summary>
                    ///     Protection against "Man in the Middle" attack is not defined.
                    /// </summary>
                    BLUETOOTH_MITM_ProtectionNotDefined = 0xff
                }

                /// <summary>
                ///     Error codes associated with the BluetoothGetDeviceInfo function.
                /// </summary>
                public enum GetDeviceResponseType
                {
                    /// <summary>
                    ///     The size of the BLUETOOTH_DEVICE_INFO is not compatible. Check the dwSize member of the 
                    ///     BLUETOOTH_DEVICE_INFO structure.
                    /// </summary>
                    ERROR_REVISION_MISMATCH,
                    /// <summary>
                    ///     The radio is not known by the system, or the Address member of the BLUETOOTH_DEVICE_INFO 
                    ///     structure is all zeros.
                    /// </summary>
                    ERROR_NOT_FOUND,
                    /// <summary>
                    ///     The pbtdi parameter is NULL.
                    /// </summary>
                    ERROR_INVALID_PARAMETER
                }
            }
        }
    }
}

