using System;
using System.Runtime.InteropServices;

using static Win32.Common.Unmanaged.Shared;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <inheritdoc/>
        public partial class User32
        {
            /// <summary>
            ///     WinUser interactions.
            /// </summary>
            public partial class WinUser
            {
                /// <summary>
                ///     Passes message information to the specified window procedure.
                /// </summary>
                /// <param name="lpPrevWndFunc">
                ///     The previous window procedure. If this value is obtained by calling the GetWindowLong function with the nIndex parameter set
                ///     to GWL_WNDPROC or DWL_DLGPROC, it is actually either the address of a window or dialog box procedure, or a special internal
                ///     value meaningful only to CallWindowProc.
                /// </param>
                /// <param name="hWnd">A handle to the window procedure to receive the message.</param>
                /// <param name="Msg">The message.</param>
                /// <param name="wParam">
                ///     Additional message-specific information. The contents of this parameter depend on the value of the Msg parameter.
                /// </param>
                /// <param name="lParam">
                ///     Additional message-specific information. The contents of this parameter depend on the value of the Msg parameter.
                /// </param>
                /// <returns>The return value specifies the result of the message processing and depends on the message sent.</returns>
                /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-callwindowprocw">CallWindowProcW</seealso>
                [DllImport(User32Dll, EntryPoint = "CallWindowProcW", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
                public static extern IntPtr CallWindowProcW([In] byte[] lpPrevWndFunc, IntPtr hWnd, int Msg, [In, Out] byte[] wParam, IntPtr lParam);
                /// <summary>
                ///     Closes an open handle to a desktop object.
                /// </summary>
                /// <param name="hDesktop">
                ///     A handle to the desktop to be closed. This can be a handle returned by the CreateDesktop, OpenDesktop, or OpenInputDesktop
                ///     functions. Do not specify the handle returned by the GetThreadDesktop function.
                /// </param>
                /// <returns>If the function succeeds, the return value is nonzero.</returns>
                /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-closedesktop">CloseDesktop</seealso>
                [DllImport(User32Dll, SetLastError = true)]
                public static extern IntPtr CloseDesktop(IntPtr hDesktop);
                /// <summary>
                ///     Deletes an item from the specified menu. If the menu item opens a menu or submenu, this function destroys the handle to the
                ///     menu or submenu and frees the memory used by the menu or submenu.
                /// </summary>
                /// <param name="hMenu">A handle to the menu to be changed.</param>
                /// <param name="uPosition">The menu item to be deleted, as determined by the uFlags parameter.</param>
                /// <param name="uFlags">Indicates how the uPosition parameter is interpreted.</param>
                /// <returns>If the function succeeds, the return value is nonzero.</returns>
                /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-deletemenu">DeleteMenu</seealso>
                [DllImport(User32Dll, SetLastError = true)]
                public static extern bool DeleteMenu(IntPtr hMenu, MY_BY uPosition, uint uFlags);
                /// <summary>
                ///     Retrieves a handle to the foreground window (the window with which the user is currently working). The system assigns a
                ///     slightly higher priority to the thread that creates the foreground window than it does to other threads.
                /// </summary>
                /// <returns>
                ///     The return value is a handle to the foreground window. The foreground window can be NULL in certain circumstances, such as
                ///     when a window is losing activation.
                /// </returns>
                /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getforegroundwindow">GetForegroundWindow</seealso>
                [DllImport(User32Dll)]
                public static extern IntPtr GetForegroundWindow();
                /// <summary>
                ///     Retrieves the time of the last input event.
                /// </summary>
                /// <param name="plii">A pointer to a <see cref="LASTINPUTINFO"/> structure that receives the time of the last input event.</param>
                /// <returns>True if successful.</returns>
                /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getlastinputinfo">GetLastInputInfo</seealso>
                [DllImport(User32Dll, SetLastError = true)]
                public static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
                /// <summary>
                ///     Enables the application to access the window menu (also known as the system menu or the control menu) for copying and modifying.
                /// </summary>
                /// <param name="hWnd">A handle to the window that will own a copy of the window menu.</param>
                /// <param name="bRevert">
                ///     The action to be taken. If this parameter is FALSE, GetSystemMenu returns a handle to the copy of the window menu currently in
                ///     use. The copy is initially identical to the window menu, but it can be modified. If this parameter is TRUE, GetSystemMenu
                ///     resets the window menu back to the default state. The previous window menu, if any, is destroyed.
                /// </param>
                /// <returns>
                ///     If the bRevert parameter is FALSE, the return value is a handle to a copy of the window menu. If the bRevert parameter is
                ///     TRUE, the return value is NULL.
                /// </returns>
                /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getsystemmenu">GetSystemMenu</seealso>
                [DllImport(User32Dll, SetLastError = true)]
                public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
                /// <summary>
                ///     Retrieves the specified system metric or system configuration setting.
                /// </summary>
                /// <remarks>All dimensions retrieved by GetSystemMetrics are in pixels.</remarks>
                /// <param name="smIndex">The <see cref="SystemMetric">system metric or configuration setting</see> to be retrieved.</param>
                /// <returns>If the function succeeds, the return value is the requested system metric or configuration setting.</returns>
                /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getsystemmetrics">GetSystemMetrics</seealso>
                [DllImport(User32Dll, SetLastError = true)]
                public static extern int GetSystemMetrics(SystemMetric smIndex);
                /// <summary>
                ///     Retrieves the dimensions of the bounding rectangle of the specified window. The dimensions are given in screen coordinates
                ///     that are relative to the upper-left corner of the screen.
                /// </summary>
                /// <remarks>
                ///     GetLastInputInfo does not provide system-wide user input information across all running sessions. Rather, GetLastInputInfo
                ///     provides session-specific user input information for only the session that invoked the function.
                /// </remarks>
                /// <param name="hWnd">A handle to the window.</param>
                /// <param name="rectangle">
                ///     A pointer to a RECT structure that receives the screen coordinates of the upper-left and lower-right corners of the window.
                /// </param>
                /// <returns>Type: BOOL</returns>
                /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowrect">GetWindowRect</seealso>
                [DllImport(User32Dll)]
                public static extern bool GetWindowRect(IntPtr hWnd, ref RECT rectangle);
                /// <summary>
                ///     Retrieves the identifier of the thread that created the specified window and, optionally, the identifier of the process that
                ///     created the window.
                /// </summary>
                /// <param name="hWnd">A handle to the window.</param>
                /// <param name="processId">
                ///     A pointer to a variable that receives the process identifier. If this parameter is not NULL, GetWindowThreadProcessId copies
                ///     the identifier of the process to the variable; otherwise, it does not.
                /// </param>
                /// <returns>The return value is the identifier of the thread that created the window.</returns>
                /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowthreadprocessid">GetWindowThreadProcessId</seealso>
                [DllImport(User32Dll)]
                public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out IntPtr processId);
                /// <summary>
                ///     Determines the visibility state of the specified window.
                /// </summary>
                /// <remarks>winuser.h</remarks>
                /// <param name="hWnd">A handle to the window to be tested.</param>
                /// <returns>
                ///     If the specified window, its parent window, its parent's parent window, and so forth, have the WS_VISIBLE style, the return
                ///     value is nonzero. Otherwise, the return value is zero.
                /// </returns>
                /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-iswindowvisible">IsWindowVisible</seealso>
                [DllImport(User32Dll)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool IsWindowVisible(IntPtr hWnd);
                /// <summary>
                ///     Opens the desktop that receives user input.
                /// </summary>
                /// <param name="dwFlags">
                ///     This parameter can be zero or the following value.
                ///
                ///     DF_ALLOWOTHERACCOUNTHOOK = 0x0001 Allows processes running in other accounts on the desktop to set hooks in this process.
                /// </param>
                /// <param name="fInherit">
                ///     If this value is TRUE, processes created by this process will inherit the handle. Otherwise, the processes do not inherit this handle.
                /// </param>
                /// <param name="dwDesiredAccess">The access to the desktop.</param>
                /// <returns>
                ///     If the function succeeds, the return value is a handle to the desktop that receives user input. When you are finished using
                ///     the handle, call the CloseDesktop function to close it.
                /// </returns>
                /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-openinputdesktop">OpenInputDesktop</seealso>
                [DllImport(User32Dll, SetLastError = true)]
                public static extern IntPtr OpenInputDesktop(uint dwFlags, bool fInherit, uint dwDesiredAccess);
                /// <summary>
                ///     The PrintWindow function copies a visual window into the specified device context (DC), typically a printer DC.
                /// </summary>
                /// <param name="hWnd">A handle to the window that will be copied.</param>
                /// <param name="d">A handle to the device context.</param>
                /// <param name="nFlags">The drawing options.</param>
                /// <returns>True if successful.</returns>
                /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-printwindow">PrintWindow</seealso>
                [DllImport(User32Dll)]
                public static extern bool PrintWindow(IntPtr hWnd, IntPtr d, uint nFlags);
                /// <summary>
                ///     Brings the thread that created the specified window into the foreground and activates the window. Keyboard input is directed
                ///     to the window, and various visual cues are changed for the user. The system assigns a slightly higher priority to the thread
                ///     that created the foreground window than it does to other threads.
                /// </summary>
                /// <param name="hWnd">A handle to the window that should be activated and brought to the foreground.</param>
                /// <returns>
                ///     If the window was brought to the foreground, the return value is nonzero. If the window was not brought to the foreground, the
                ///     return value is zero.
                /// </returns>
                /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setforegroundwindow">SetForegroundWindow</seealso>
                [DllImport(User32Dll, SetLastError = true)]
                public static extern bool SetForegroundWindow(IntPtr hWnd);
                /// <summary>
                ///     Sets the specified window's show state.
                /// </summary>
                /// <param name="hWnd">A handle to the window.</param>
                /// <param name="nCmdShow">
                ///     Controls how the window is to be shown. This parameter is ignored the first time an application calls ShowWindow, if the
                ///     program that launched the application provides a STARTUPINFO structure. Otherwise, the first time ShowWindow is called, the
                ///     value should be the value obtained by the WinMain function in its nCmdShow parameter. In subsequent calls, this parameter can
                ///     be one of the following values.
                /// </param>
                /// <returns>
                ///     If the window was previously visible, the return value is nonzero. If the window was previously hidden, the return value is zero.
                /// </returns>
                /// <seealso href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow">ShowWindow</seealso>
                [DllImport(User32Dll)]
                public static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);
            }
        }
    }
}