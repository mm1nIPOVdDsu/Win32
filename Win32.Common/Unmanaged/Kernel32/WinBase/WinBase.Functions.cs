using System.Runtime.InteropServices;

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
                ///     Retrieves the firmware type of the local computer.
                /// </summary>
                /// <param name="FirmwareType">A pointer to a <see cref="FIRMWARE_TYPE"/> enumeration.</param>
                /// <returns>True if successful.</returns>
                [DllImport(Kernel32Dll, SetLastError = true)]
                public static extern bool GetFirmwareType(out FIRMWARE_TYPE FirmwareType);
                /// <summary>
                ///     Indicates if the OS was booted from a VHD container.
                /// </summary>
                /// <param name="NativeVhdBoot">Pointer to a variable that receives a boolean indicating if the OS was booted from a VHD.</param>
                [DllImport(Kernel32Dll, SetLastError = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool IsNativeVhdBoot(out bool NativeVhdBoot);
                /// <summary>
                ///     Retrieves the session identifier of the console session.
                /// </summary>
                /// <remarks>
                ///     The console session is the session that is currently attached to the physical console. Note that it is not necessary that Remote Desktop Services be running for this function to succeed.
                /// </remarks>
                /// <returns>The session identifier of the session that is attached to the physical console. If there is no session attached to the physical console, (for example, if the physical console session is in the process of being attached or detached), this function returns 0xFFFFFFFF.</returns>
                [DllImport(Kernel32Dll, SetLastError = true)]
                public static extern uint WTSGetActiveConsoleSessionId();
            }
        }
    }
}