using System;
using System.Runtime.InteropServices;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <summary>
        ///     Shared interactions.
        /// </summary>
        public partial class Shared
        {
            /// <summary>
            ///     A driver sets an IRP's I/O status block to indicate the final status of an I/O request, before calling IoCompleteRequest for the IRP.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/wdm/ns-wdm-_io_status_block">IO_STATUS_BLOCK</see>
            [StructLayoutAttribute(LayoutKind.Sequential)]
            public struct IO_STATUS_BLOCK
            {
                /// <summary>
                ///     This is the completion status, either STATUS_SUCCESS if the requested operation was completed successfully or an
                ///     informational, warning, or error STATUS_XXX value. For more information, see Using NTSTATUS values.
                /// </summary>
                public UInt32 Status;
                /// <summary>
                ///     This is set to a request-dependent value. For example, on successful completion of a transfer request, this is set to the
                ///     number of bytes transferred. If a transfer request is completed with another STATUS_XXX, this member is set to zero.
                /// </summary>
                public IntPtr Information;
            }
        }
    }
}