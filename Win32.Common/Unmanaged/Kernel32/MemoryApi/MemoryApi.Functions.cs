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
            public partial class MemoryApi
            {
                /// <summary>
                ///     Changes the protection on a region of committed pages in the virtual address space of the calling process. To change the
                ///     access protection of any process, use the VirtualProtectEx function.
                /// </summary>
                /// <param name="lpAddress">
                ///     The address of the starting page of the region of pages whose access protection attributes are to be changed.
                /// </param>
                /// <param name="dwSize">
                ///     The size of the region whose access protection attributes are to be changed, in bytes. The region of affected pages includes
                ///     all pages containing one or more bytes in the range from the lpAddress parameter to (lpAddress+dwSize). This means that a
                ///     2-byte range straddling a page boundary causes the protection attributes of both pages to be changed.
                /// </param>
                /// <param name="flNewProtect">The <see cref="MEM_PROTECTION"/> option.</param>
                /// <param name="lpflOldProtect">
                ///     A pointer to a variable that receives the previous access protection value of the first page in the specified region of pages.
                ///     If this parameter is NULL or does not point to a valid variable, the function fails.
                /// </param>
                /// <returns>If the function succeeds, the return value is nonzero.</returns>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/memoryapi/nf-memoryapi-virtualprotect">VirtualProtect</seealso>
                [return: MarshalAs(UnmanagedType.Bool)]
                [DllImport(Kernel32Dll, CharSet = CharSet.Unicode, SetLastError = true)]
                public static extern bool VirtualProtect([In] byte[] lpAddress, IntPtr dwSize, MEM_PROTECTION flNewProtect, out int lpflOldProtect);
            }
        }
    }
}
