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
            ///     ProcessEnv interactions.
            /// </summary>
            public partial class ProcessEnv
            {
                /// <summary>
                ///     Retrieves a handle to the specified standard device (standard input, standard output, or standard error).
                /// </summary>
                /// <param name="nStdHandle"><see cref="STD_HANDLE"/></param>
                /// <returns>If the function succeeds, the return value is a handle to the specified device, or a redirected handle set by a previous call to SetStdHandle.</returns>
                [DllImport(Kernel32Dll, SetLastError = true)]
                public static extern IntPtr GetStdHandle(STD_HANDLE nStdHandle);
                /// <summary>
                ///     Sets the handle for the specified standard device (standard input, standard output, or standard error).
                /// </summary>
                /// <param name="nStdHandle"><see cref="STD_HANDLE"/></param>
                /// <param name="handle">The handle for the standard device.</param>
                [DllImport(Kernel32Dll, SetLastError = true)]
                public static extern void SetStdHandle(STD_HANDLE nStdHandle, IntPtr handle);
            }
        }
    }
}