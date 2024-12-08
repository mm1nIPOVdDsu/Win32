using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <summary>
        ///     Kernel32 interactions.
        /// </summary>
        public partial class Kernel32
        {
            /// <summary>
            ///     LibLoaderApi interactions.
            /// </summary>
            public partial class LibLoaderApi
            {
                /// <summary>
                ///     Loads the specified module into the address space of the calling process. The specified module may cause other modules to be loaded.
                /// </summary>
                /// <param name="lpFileName">
                ///     The name of the module. This can be either a library module (a .dll file) or an executable module (an .exe file). If the
                ///     specified module is an executable module, static imports are not loaded; instead, the module is loaded as if by LoadLibraryEx
                ///     with the DONT_RESOLVE_DLL_REFERENCES flag.
                /// </param>
                /// <returns>If the function succeeds, the return value is a handle to the module.</returns>
                /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-loadlibrarya">LoadLibrary</see>
                [DllImport(Kernel32Dll, SetLastError = true, CharSet = CharSet.Ansi)]
                public static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);
                /// <summary>
                ///     Loads the specified module into the address space of the calling process. The specified module may cause other modules to be loaded.
                /// </summary>
                /// <param name="lpFileName">
                ///     A string that specifies the file name of the module to load. This name is not related to the name stored in a library module
                ///     itself, as specified by the LIBRARY keyword in the module-definition (.def) file.
                /// </param>
                /// <param name="hReservedNull">This parameter is reserved for future use. It must be NULL.</param>
                /// <param name="dwFlags">
                ///     The action to be taken when loading the module. If no flags are specified, the behavior of this function is identical to that
                ///     of the <see cref="LoadLibrary"/> function. This parameter can be one of the following values.
                /// </param>
                /// <returns>If the function succeeds, the return value is a handle to the loaded module.</returns>
                /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-loadlibraryexa#parameters">LoadLibraryEx</see>
                [DllImport(Kernel32Dll, SetLastError = true)]
                public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LOAD_LIBRARY_FLAGS dwFlags);
                /// <summary>
                ///     Retrieves the address of an exported function (also known as a procedure) or variable from the specified dynamic-link library (DLL).
                /// </summary>
                /// <param name="dllPtr">
                ///     A handle to the DLL module that contains the function or variable. The LoadLibrary, LoadLibraryEx, LoadPackagedLibrary, or
                ///     GetModuleHandle function returns this handle.
                /// </param>
                /// <param name="functionName">
                ///     The function or variable name, or the function's ordinal value. If this parameter is an ordinal value, it must be in the
                ///     low-order word; the high-order word must be zero.
                /// </param>
                /// <returns>If the function succeeds, the return value is the address of the exported function or variable.</returns>
                /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-getprocaddress">GetProcAddress</see>
                [DllImport(Kernel32Dll)]
                public static extern IntPtr GetProcAddress(IntPtr dllPtr, string functionName);
                /// <summary>
                ///     Frees the loaded dynamic-link library (DLL) module and, if necessary, decrements its reference count. When the reference count
                ///     reaches zero, the module is unloaded from the address space of the calling process and the handle is no longer valid.
                /// </summary>
                /// <param name="dllPtr">A handle to the loaded library module.</param>
                /// <returns>If the function succeeds, the return value is nonzero.</returns>
                /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-freelibrary">FreeLibrary</see>
                [DllImport(Kernel32Dll)]
                public static extern bool FreeLibrary(IntPtr dllPtr);
                /// <summary>
                ///     Calls <see cref="FreeLibrary(nint)"/> 1000 times until successful.
                /// </summary>
                /// <param name="dllPtr">The library to free.</param>
                /// <returns>True if successful.</returns>
                public static async Task<bool> UnloadLibrary(IntPtr dllPtr)
                {
                    // NOTE:
                    // See https://learn.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-freelibrary#remarks about why this loop is executed.
                    while (FreeLibrary(dllPtr))
                    {
                        await Task.Delay(0);
                    }

                    return true;
                }
            }
        }
    }
}
