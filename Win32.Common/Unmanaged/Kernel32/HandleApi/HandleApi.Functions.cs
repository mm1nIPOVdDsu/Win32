using System;
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
            ///     HandleApi interactions.
            /// </summary>
            public partial class HandleApi
            {
                /// <summary>
                ///     Closes an open object handle.
                /// </summary>
                /// <param name="handle">A valid handle to an open object.</param>
                /// <returns>True if successful.</returns>
                [DllImport(Kernel32Dll, SetLastError = true)]
                public static extern bool CloseHandle(IntPtr handle);
            }
        }
    }
}
