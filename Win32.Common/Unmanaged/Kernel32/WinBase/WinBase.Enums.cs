namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <inheritdoc/>
        public partial class Kernel32
        {
            /// <summary>
            ///     WinBase interactions.
            /// </summary>
            public partial class WinBase
            {
                /// <summary>
                ///     Specifies a firmware type.
                /// </summary>
                public enum FIRMWARE_TYPE
                {
                    /// <summary>
                    ///     The firmware type is unknown.
                    /// </summary>
                    FirmwareTypeUnknown,
                    /// <summary>
                    ///     The computer booted in legacy BIOS mode.
                    /// </summary>
                    FirmwareTypeBios,
                    /// <summary>
                    ///     The computer booted in UEFI mode.
                    /// </summary>
                    FirmwareTypeUefi,
                    /// <summary>
                    ///     Not implemented.
                    /// </summary>
                    FirmwareTypeMax
                }
            }
        }
    }
}
