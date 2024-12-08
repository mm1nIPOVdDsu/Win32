﻿namespace Win32.Common
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
                ///     The action to be taken when loading the module
                /// </summary>
                /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-loadlibraryexa#parameters">LOAD_LIBRARY_FLAGS</see>
                public enum LOAD_LIBRARY_FLAGS : uint
                {
                    /// <summary>
                    ///     If this value is used, and the executable module is a DLL, the system does not call DllMain for process and thread
                    ///     initialization and termination. Also, the system does not load additional executable modules that are referenced by the
                    ///     specified module.
                    /// </summary>
                    DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
                    /// <summary>
                    ///     If this value is used, the system does not check AppLocker rules or apply Software Restriction Policies for the DLL. This
                    ///     action applies only to the DLL being loaded and not to its dependencies. This value is recommended for use in setup
                    ///     programs that must run extracted DLLs during installation.
                    /// </summary>
                    LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
                    /// <summary>
                    ///     If this value is used, the system maps the file into the calling process's virtual address space as if it were a data
                    ///     file. Nothing is done to execute or prepare to execute the mapped file.
                    /// </summary>
                    LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
                    /// <summary>
                    ///     Similar to LOAD_LIBRARY_AS_DATAFILE, except that the DLL file is opened with exclusive write access for the calling
                    ///     process. Other processes cannot open the DLL file for write access while it is in use. However, the DLL can still be
                    ///     opened by other processes.
                    /// </summary>
                    LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
                    /// <summary>
                    ///     If this value is used, the system maps the file into the process's virtual address space as an image file. However, the
                    ///     loader does not load the static imports or perform the other usual initialization steps. Use this flag when you want to
                    ///     load a DLL only to extract messages or resources from it.
                    /// </summary>
                    LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
                    /// <summary>
                    ///     If this value is used, the application's installation directory is searched for the DLL and its dependencies. Directories
                    ///     in the standard search path are not searched. This value cannot be combined with LOAD_WITH_ALTERED_SEARCH_PATH.
                    /// </summary>
                    LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,
                    /// <summary>
                    ///     This value is a combination of LOAD_LIBRARY_SEARCH_APPLICATION_DIR, LOAD_LIBRARY_SEARCH_SYSTEM32, and
                    ///     LOAD_LIBRARY_SEARCH_USER_DIRS. Directories in the standard search path are not searched. This value cannot be combined
                    ///     with LOAD_WITH_ALTERED_SEARCH_PATH.
                    /// </summary>
                    LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,
                    /// <summary>
                    ///     If this value is used, the directory that contains the DLL is temporarily added to the beginning of the list of
                    ///     directories that are searched for the DLL's dependencies. Directories in the standard search path are not searched. The
                    ///     lpFileName parameter must specify a fully qualified path. This value cannot be combined with LOAD_WITH_ALTERED_SEARCH_PATH.
                    /// </summary>
                    LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,
                    /// <summary>
                    ///     If this value is used, %windows%\system32 is searched for the DLL and its dependencies. Directories in the standard search
                    ///     path are not searched. This value cannot be combined with LOAD_WITH_ALTERED_SEARCH_PATH.
                    /// </summary>
                    LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,
                    /// <summary>
                    ///     If this value is used, directories added using the AddDllDirectory or the SetDllDirectory function are searched for the
                    ///     DLL and its dependencies. If more than one directory has been added, the order in which the directories are searched is
                    ///     unspecified. Directories in the standard search path are not searched. This value cannot be combined with LOAD_WITH_ALTERED_SEARCH_PATH.
                    /// </summary>
                    LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,
                    /// <summary>
                    ///     If this value is used and lpFileName specifies an absolute path, the system uses the alternate file search strategy
                    ///     discussed in the Remarks section to find associated executable modules that the specified module causes to be loaded. If
                    ///     this value is used and lpFileName specifies a relative path, the behavior is undefined.
                    /// </summary>
                    LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008,
                    /// <summary>
                    ///     Specifies that the digital signature of the binary image must be checked at load time. This value requires Windows 8.1,
                    ///     Windows 10 or later.
                    /// </summary>
                    LOAD_LIBRARY_REQUIRE_SIGNED_TARGET = 0x00000080,
                    /// <summary>
                    ///     If this value is used, loading a DLL for execution from the current directory is only allowed if it is under a directory
                    ///     in the Safe load list.
                    /// </summary>
                    LOAD_LIBRARY_SAFE_CURRENT_DIRS = 0x00002000
                }
            }
        }
    }
}
