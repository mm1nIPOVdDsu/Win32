using System;

using Microsoft.Win32.SafeHandles;

using static Win32.Common.Unmanaged.Kernel32;

/// <inheritdoc/>
internal partial class Unmanaged
{
    /// <summary>
    ///     Kernel32 interactions.
    /// </summary>
    public partial class Kernel32 
    {
        /// <summary>
        ///     A <see cref="System.Runtime.InteropServices.SafeHandle"/> implementation for a volume.
        /// </summary>
        public class FindVolumeSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            /// <summary>
            ///     Initializes a new instance of the <see cref="FindVolumeSafeHandle"/> class.
            /// </summary>
            private FindVolumeSafeHandle(): base(true) { }
            /// <summary>
            ///     Initializes a new instance of the <see cref="FindVolumeSafeHandle"/> class.
            /// </summary>
            /// <param name="volumeHandle">The volume's handle as an <see cref="IntPtr"/>.</param>
            /// <param name="ownsHandle">True if the handle should be reliably released.</param>
            public FindVolumeSafeHandle(IntPtr volumeHandle, bool ownsHandle) : base(ownsHandle) => SetHandle(volumeHandle);
            /// <summary>
            ///     Frees the volume's handle.
            /// </summary>
            /// <returns>True if successful.</returns>
            protected override bool ReleaseHandle() => FileApi.FindVolumeClose(handle);
        }
    }
}
