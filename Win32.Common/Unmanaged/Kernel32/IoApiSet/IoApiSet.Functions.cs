using System;
using System.Runtime.InteropServices;

using Microsoft.Win32.SafeHandles;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <inheritdoc/>
        public partial class Kernel32
        {
            /// <summary>
            ///     IoApiSet interactions.
            /// </summary>
            public partial class IoApiSet
            {
                /// <summary>
                ///     Sends a control code directly to a specified device driver, causing the corresponding device to perform the corresponding operation.
                /// </summary>
                /// <param name="hDevice">A handle to the device on which the operation is to be performed. The device is typically a volume, directory, file, or stream. To retrieve a device handle, use the CreateFile function. For more information, see Remarks.</param>
                /// <param name="dwIoControlCode">The control code for the operation. This value identifies the specific operation to be performed and the type of device on which to perform it.</param>
                /// <param name="lpInBuffer">A pointer to the input buffer that contains the data required to perform the operation. The format of this data depends on the value of the dwIoControlCode parameter.</param>
                /// <param name="nInBufferSize">The size of the input buffer, in bytes.</param>
                /// <param name="lpOutBuffer">A pointer to the output buffer that is to receive the data returned by the operation. The format of this data depends on the value of the dwIoControlCode parameter.</param>
                /// <param name="nOutBufferSize">The size of the output buffer, in bytes.</param>
                /// <param name="lpBytesReturned">A pointer to a variable that receives the size of the data stored in the output buffer, in bytes.</param>
                /// <param name="lpOverlapped">A pointer to an OVERLAPPED structure.</param>
                /// <returns>If the operation fails or is pending, the return value is zero. To get extended error information, call GetLastError.</returns>
                [DllImport(Kernel32Dll, SetLastError = true)]
                public static extern int DeviceIoControl(int hDevice, int dwIoControlCode, byte[] lpInBuffer, int nInBufferSize, byte[] lpOutBuffer, int nOutBufferSize, ref int lpBytesReturned, IntPtr lpOverlapped);
                /// <summary>
                ///     Sends a control code directly to a specified device driver, causing the corresponding device to perform the corresponding operation.
                /// </summary>
                /// <param name="hDevice">A handle to the device on which the operation is to be performed. The device is typically a volume, directory, file, or stream. To retrieve a device handle, use the CreateFile function. For more information, see Remarks.</param>
                /// <param name="dwIoControlCode">The control code for the operation. This value identifies the specific operation to be performed and the type of device on which to perform it.</param>
                /// <param name="lpInBuffer">A pointer to the input buffer that contains the data required to perform the operation. The format of this data depends on the value of the dwIoControlCode parameter.</param>
                /// <param name="nInBufferSize">The size of the input buffer, in bytes.</param>
                /// <param name="lpOutBuffer">A pointer to the output buffer that is to receive the data returned by the operation. The format of this data depends on the value of the dwIoControlCode parameter.</param>
                /// <param name="nOutBufferSize">The size of the output buffer, in bytes.</param>
                /// <param name="lpBytesReturned">A pointer to a variable that receives the size of the data stored in the output buffer, in bytes.</param>
                /// <param name="lpOverlapped">A pointer to an OVERLAPPED structure.</param>
                /// <returns>If the operation fails or is pending, the return value is zero. To get extended error information, call GetLastError.</returns>
                [DllImport(Kernel32Dll, SetLastError = true)]
                public static extern int DeviceIoControl(SafeFileHandle hDevice, int dwIoControlCode, ref STORAGE_PROPERTY_QUERY lpInBuffer, int nInBufferSize, out STORAGE_DEVICE_DESCRIPTOR lpOutBuffer, int nOutBufferSize, out int lpBytesReturned, IntPtr lpOverlapped);
            }
        }
    }
}