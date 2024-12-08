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
            ///     ConsoleApi interactions.
            /// </summary>
            public partial class ConsoleApi
            {
                /// <summary>
                ///     Allocates a new console for the calling process.
                /// </summary>
                /// <remarks>
                ///     A process can be associated with only one console, so the AllocConsole function fails if the calling process already has a console. 
                ///     A process can use the FreeConsole function to detach itself from its current console, then it can call AllocConsole to create a 
                ///     new console or AttachConsole to attach to another console.
                /// </remarks>
                /// <returns>If the function succeeds, the return value is nonzero.</returns>
                [DllImport(Kernel32Dll)]
                public static extern bool AllocConsole();
                /// <summary>
                ///     
                /// </summary>
                /// <returns></returns>
                [DllImport(Kernel32Dll)]
                public static extern bool FreeConsole();
                /// <summary>
                ///     Retrieves the window handle used by the console associated with the calling process.
                /// </summary>
                /// <returns>The return value is a handle to the window used by the console associated with the calling process or NULL if there is no such associated console.</returns>
                [DllImport(Kernel32Dll)]
                public static extern IntPtr GetConsoleWindow();
            }
        }
    }
}
