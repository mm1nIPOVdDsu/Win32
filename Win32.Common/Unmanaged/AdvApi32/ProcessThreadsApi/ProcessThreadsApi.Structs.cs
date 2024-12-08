using System;
using System.Runtime.InteropServices;

using static Win32.Common.Unmanaged.AdvApi32.WinBase;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <inheritdoc/>
        public partial class AdvApi32
        {
            /// <summary>
            ///     ProcessThreadsApi interactions.
            /// </summary>
            public partial class ProcessThreadsApi
            {
                /// <summary>
                ///     Contains information about a newly created process and its primary thread. It is used with the CreateProcess, CreateProcessAsUser, CreateProcessWithLogonW, or CreateProcessWithTokenW function.
                /// </summary>
                /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/processthreadsapi/ns-processthreadsapi-process_information">PROCESS_INFORMATION</seealso>
                [StructLayout(LayoutKind.Sequential)]
                public struct PROCESS_INFORMATION
                {
                    /// <summary>
                    ///     A handle to the newly created process
                    /// </summary>
                    public IntPtr hProcess;
                    /// <summary>
                    ///     A handle to the primary thread of the newly created process.
                    /// </summary>
                    public IntPtr hThread;
                    /// <summary>
                    ///     A value that can be used to identify a process.
                    /// </summary>
                    public uint dwProcessId;
                    /// <summary>
                    ///     A value that can be used to identify a thread.
                    /// </summary>
                    public uint dwThreadId;
                }
                /// <summary>
                ///     Specifies the window station, desktop, standard handles, and appearance of the main window for a process at creation time.
                /// </summary>
                /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/processthreadsapi/ns-processthreadsapi-startupinfoa">STARTUPINFO</seealso>
                [StructLayout(LayoutKind.Sequential)]
                public struct STARTUPINFO
                {
                    /// <summary>
                    ///     The size of the structure, in bytes.
                    /// </summary>
                    public int cb;
                    /// <summary>
                    ///     Reserved; must be NULL.
                    /// </summary>
                    public string lpReserved;
                    /// <summary>
                    ///     The name of the desktop, or the name of both the desktop and window station for this process. A backslash in the string indicates that the string includes both the desktop and window station names.
                    /// </summary>
                    public string lpDesktop;
                    /// <summary>
                    ///     For console processes, this is the title displayed in the title bar if a new console window is created. If NULL, the name of the executable file is used as the window title instead. 
                    /// </summary>
                    public string lpTitle;
                    /// <summary>
                    ///     If dwFlags specifies STARTF_USEPOSITION, this member is the x offset of the upper left corner of a window if a new window is created, in pixels.
                    /// </summary>
                    public uint dwX;
                    /// <summary>
                    ///     If dwFlags specifies STARTF_USEPOSITION, this member is the y offset of the upper left corner of a window if a new window is created, in pixels.
                    /// </summary>
                    public uint dwY;
                    /// <summary>
                    ///     If dwFlags specifies STARTF_USESIZE, this member is the width of the window if a new window is created, in pixels.
                    /// </summary>
                    public uint dwXSize;
                    /// <summary>
                    ///     If dwFlags specifies STARTF_USESIZE, this member is the height of the window if a new window is created, in pixels.
                    /// </summary>
                    public uint dwYSize;
                    /// <summary>
                    ///     If dwFlags specifies STARTF_USECOUNTCHARS, if a new console window is created in a console process, this member specifies the screen buffer width, in character columns.
                    /// </summary>
                    public uint dwXCountChars;
                    /// <summary>
                    ///     If dwFlags specifies STARTF_USECOUNTCHARS, if a new console window is created in a console process, this member specifies the screen buffer height, in character rows
                    /// </summary>
                    public uint dwYCountChars;
                    /// <summary>
                    ///     If dwFlags specifies STARTF_USEFILLATTRIBUTE, this member is the initial text and background colors if a new console window is created in a console application.
                    /// </summary>
                    public uint dwFillAttribute;
                    /// <summary>
                    ///     A <see cref="STARTF"/> that determines whether certain STARTUPINFO members are used when the process creates a window.
                    /// </summary>
                    public STARTF dwFlags;
                    /// <summary>
                    ///     If dwFlags specifies STARTF_USESHOWWINDOW, this member can be any of the values that can be specified in the nCmdShow parameter for the ShowWindow function, except for SW_SHOWDEFAULT.
                    /// </summary>
                    public short wShowWindow;
                    /// <summary>
                    ///     Reserved for use by the C Run-time; must be zero.
                    /// </summary>
                    public short cbReserved2;
                    /// <summary>
                    ///     Reserved for use by the C Run-time; must be NULL.
                    /// </summary>
                    public IntPtr lpReserved2;
                    /// <summary>
                    ///     If dwFlags specifies STARTF_USESTDHANDLES, this member is the standard input handle for the process.
                    /// </summary>
                    public IntPtr hStdInput;
                    /// <summary>
                    ///     If dwFlags specifies STARTF_USESTDHANDLES, this member is the standard output handle for the process.
                    /// </summary>
                    public IntPtr hStdOutput;
                    /// <summary>
                    ///     If dwFlags specifies STARTF_USESTDHANDLES, this member is the standard error handle for the process.
                    /// </summary>
                    public IntPtr hStdError;
                }
            }
        }
    }
}
