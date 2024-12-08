using System;
using System.Runtime.InteropServices;

using static Win32.Common.Unmanaged.Shared;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <summary>
        ///     Magnification interactions.
        /// </summary>
        public partial class Magnification
        {
            /// <summary>
            ///     Prototype for a callback function that implements a custom transform for image scaling.
            /// </summary>
            /// <param name="hwnd">The magnification window.</param>
            /// <param name="srcdata">The input data.</param>
            /// <param name="srcheader">The description of the input format.</param>
            /// <param name="destdata">The output data.</param>
            /// <param name="destheader">The description of the output format.</param>
            /// <param name="unclipped">The coordinates of the scaled version of the source bitmap.</param>
            /// <param name="clipped">The coordinates of the window to which the scaled bitmap is clipped.</param>
            /// <param name="dirty">The region that needs to be refreshed.</param>
            /// <returns>Returns TRUE if successful, or FALSE otherwise.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/magnification/nc-magnification-magimagescalingcallback">MagImageScalingCallback</see>
            [return: MarshalAs(UnmanagedType.Bool)]
            [Obsolete("The MagImageScalingCallback function is deprecated in Windows 7 and later, and should not be used in new applications. There is no alternate functionality.")]
            public delegate bool MagImageScalingCallback(IntPtr hwnd, IntPtr srcdata, MAGIMAGEHEADER srcheader, IntPtr destdata, MAGIMAGEHEADER destheader, RECT unclipped, RECT clipped, HRGN dirty);

            /// <summary>
            ///     Gets the color transformation matrix for a magnifier control.
            /// </summary>
            /// <param name="hwnd">The magnification window.</param>
            /// <param name="pEffect">The color transformation matrix, or NULL if no color effect has been set.</param>
            /// <returns>Returns TRUE if successful, or FALSE otherwise.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/magnification/nf-magnification-maggetcoloreffect">MagGetColorEffect</see>
            [DllImport(MagnificationDll)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool MagGetColorEffect(IntPtr hwnd, out MAGCOLOREFFECT pEffect);
            /// <summary>
            ///     Retrieves the color transformation matrix associated with the full-screen magnifier.
            /// </summary>
            /// <param name="pEffect">The color transformation matrix, or the identity matrix if no color effect has been set.</param>
            /// <returns>Returns TRUE if successful, or FALSE otherwise.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/magnification/nf-magnification-maggetfullscreencoloreffect">MagGetFullscreenColorEffect</see>
            [DllImport(MagnificationDll)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool MagGetFullscreenColorEffect(out MAGCOLOREFFECT pEffect);
            /// <summary>
            ///     Retrieves the magnification settings for the full-screen magnifier.
            /// </summary>
            /// <param name="pMagLevel">
            ///     The current magnification factor for the full-screen magnifier. A value of 1.0 indicates that the screen content is not being
            ///     magnified. A value above 1.0 indicates the scale factor for magnification. A value less than 1.0 is not valid.
            /// </param>
            /// <param name="pxOffset">
            ///     The x-coordinate offset for the upper-left corner of the unmagnified view. The offset is relative to the upper-left corner of the
            ///     primary monitor, in unmagnified coordinates.
            /// </param>
            /// <param name="pyOffset">
            ///     The y-coordinate offset for the upper-left corner of the unmagnified view. The offset is relative to the upper-left corner of the
            ///     primary monitor, in unmagnified coordinates.
            /// </param>
            /// <returns>Returns TRUE if successful, or FALSE otherwise.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/magnification/nf-magnification-maggetfullscreentransform">MagGetFullscreenTransform</see>
            [DllImport(MagnificationDll, SetLastError = false, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool MagGetFullscreenTransform(out float pMagLevel, out int pxOffset, out int pyOffset);
            /// <summary>
            ///     Retrieves the registered callback function that implements a custom transform for image scaling.
            /// </summary>
            /// <param name="hwnd">The magnification window.</param>
            /// <returns>Returns the registered MagImageScalingCallback callback function, or NULL if no callback is registered.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/magnification/nf-magnification-maggetimagescalingcallback">MagGetImageScalingCallback</see>
            [DllImport(MagnificationDll, SetLastError = false, ExactSpelling = true)]
            [Obsolete("The MagGetImageScalingCallback function is deprecated in Windows 7 and later, and should not be used in new applications. There is no alternate functionality.")]
            public static extern MagImageScalingCallback MagGetImageScalingCallback(IntPtr hwnd);
            /// <summary>
            ///     Retrieves the current input transformation for pen and touch input, represented as a source rectangle and a destination rectangle.
            /// </summary>
            /// <param name="pfEnabled">TRUE if input translation is enabled, or FALSE if not.</param>
            /// <param name="pRectSource">
            ///     The source rectangle, in unmagnified screen coordinates, that defines the area of the screen that is magnified.
            /// </param>
            /// <param name="pRectDest">
            ///     The destination rectangle, in screen coordinates, that defines the area of the screen where the magnified screen content is
            ///     displayed. Pen and touch input in this rectangle is mapped to the source rectangle.
            /// </param>
            /// <returns>Returns TRUE if successful, or FALSE otherwise.</returns>
            /// <remarks>
            ///     The input transformation maps the coordinate space of the magnified screen content to the actual (unmagnified) screen coordinate
            ///     space. This enables the system to pass touch and pen input that is entered in magnified screen content, to the correct UI element
            ///     on the screen. For example, without input transformation, input is passed to the element located at the unmagnified screen
            ///     coordinates, not to the item that appears in the magnified screen content.
            /// </remarks>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/magnification/nf-magnification-maggetinputtransform">MagGetInputTransform</see>
            [DllImport(MagnificationDll, SetLastError = false, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool MagGetInputTransform([MarshalAs(UnmanagedType.Bool)] out bool pfEnabled, out RECT pRectSource, out RECT pRectDest);
            /// <summary>
            ///     Retrieves the list of windows that are magnified or excluded from magnification.
            /// </summary>
            /// <param name="hwnd">The magnification window.</param>
            /// <param name="pdwFilterMode">The filter mode, as set by MagSetWindowFilterList.</param>
            /// <param name="count">The number of windows to retrieve, or 0 to retrieve a count of windows in the filter list.</param>
            /// <param name="pHWND">The list of window handles.</param>
            /// <returns>Returns the count of window handles in the filter list, or -1 if the hwnd parameter is not valid.</returns>
            /// <remarks>
            ///     First call the method with a count of 0 to retrieve the count of windows in the filter list. Use the retrieved count to allocate
            ///     sufficient memory for the retrieved list of window handles.
            /// </remarks>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/magnification/nf-magnification-maggetwindowfilterlist">MagGetWindowFilterList</see>
            [DllImport(MagnificationDll, SetLastError = false, ExactSpelling = true)]
            public static extern int MagGetWindowFilterList(IntPtr hwnd, out MW_FILTERMODE pdwFilterMode, int count, [Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 2)] IntPtr[] pHWND);
            /// <summary>
            ///     Gets the rectangle of the area that is being magnified.
            /// </summary>
            /// <param name="hwnd">The magnification window.</param>
            /// <param name="pRect">The rectangle that is being magnified, in desktop coordinates.</param>
            /// <returns>Returns TRUE if successful, or FALSE otherwise.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/magnification/nf-magnification-maggetwindowsource">MagGetWindowSource</see>
            [DllImport(MagnificationDll, SetLastError = false, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool MagGetWindowSource(IntPtr hwnd, out RECT pRect);
            /// <summary>
            ///     Retrieves the transformation matrix associated with a magnifier control.
            /// </summary>
            /// <param name="hwnd">The magnification window.</param>
            /// <param name="pTransform">The transformation matrix.</param>
            /// <returns>Returns TRUE if successful, or FALSE otherwise.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/magnification/nf-magnification-maggetwindowtransform">MagGetWindowTransform</see>
            [DllImport(MagnificationDll, SetLastError = false)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool MagGetWindowTransform(IntPtr hwnd, out MAGTRANSFORM pTransform);
            /// <summary>
            ///     Creates and initializes the magnifier run-time objects.
            /// </summary>
            /// <returns>Returns TRUE if initialization was successful; otherwise FALSE.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/magnification/nf-magnification-maginitialize">MagInitialize</see>
            [DllImport(MagnificationDll)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool MagInitialize();
            /// <summary>
            ///     Sets the color transformation matrix for a magnifier control.
            /// </summary>
            /// <param name="hwnd">The magnification window.</param>
            /// <param name="pEffect">The color transformation matrix, or NULL to remove the current color effect, if any.</param>
            /// <returns>Returns TRUE if successful, or FALSE otherwise.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/magnification/nf-magnification-magsetcoloreffect">MagSetColorEffect</see>
            [DllImport(MagnificationDll)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool MagSetColorEffect(IntPtr hwnd, in MAGCOLOREFFECT pEffect);
            /// <summary>
            ///     Changes the color transformation matrix associated with the full-screen magnifier.
            /// </summary>
            /// <param name="pEffect">The new color transformation matrix. This parameter must not be NULL.</param>
            /// <returns>Returns TRUE if successful, or FALSE otherwise.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/magnification/nf-magnification-magsetfullscreencoloreffect">MagSetFullscreenColorEffect</see>
            [DllImport(MagnificationDll)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool MagSetFullscreenColorEffect(in MAGCOLOREFFECT pEffect);
            /// <summary>
            ///     Changes the magnification settings for the full-screen magnifier.
            /// </summary>
            /// <param name="magLevel">
            ///     The new magnification factor for the full-screen magnifier. The minimum value of this parameter is 1.0, and the maximum value is
            ///     4096.0. If this value is 1.0, the screen content is not magnified and no offsets are applied.
            /// </param>
            /// <param name="xOffset">
            ///     The new x-coordinate offset, in pixels, for the upper-left corner of the magnified view. The offset is relative to the upper-left
            ///     corner of the primary monitor, in unmagnified coordinates. The minimum value of the parameter is -262144, and the maximum value is 262144.
            /// </param>
            /// <param name="yOffset">
            ///     The new y-coordinate offset, in pixels, for the upper-left corner of the magnified view. The offset is relative to the upper-left
            ///     corner of the primary monitor, in unmagnified coordinates. The minimum value of the parameter is -262144, and the maximum value is 262144.
            /// </param>
            /// <returns>Returns TRUE if successful, or FALSE otherwise.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/magnification/nf-magnification-magsetfullscreentransform">MagSetFullscreenTransform</see>
            [DllImport(MagnificationDll)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool MagSetFullscreenTransform(float magLevel, int xOffset, int yOffset);
            /// <summary>
            ///     Sets the callback function for external image filtering and scaling.
            /// </summary>
            /// <param name="hwnd">The handle of the magnification window.</param>
            /// <param name="callback">The callback function, or NULL to remove a callback that was previously set.</param>
            /// <returns>Returns TRUE if successful, or FALSE otherwise.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/magnification/nf-magnification-magsetimagescalingcallback">MagSetImageScalingCallback</see>
            [DllImport(MagnificationDll, SetLastError = false, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            [Obsolete("The MagSetImageScalingCallback function is deprecated in Windows 7 and later, and should not be used in new applications. There is no alternate functionality.")]
            public static extern bool MagSetImageScalingCallback(IntPtr hwnd, MagImageScalingCallback callback);
            /// <summary>
            ///     Sets the current active input transformation for pen and touch input, represented as a source rectangle and a destination rectangle.
            /// </summary>
            /// <param name="fEnabled">TRUE to enable input transformation, or FALSE to disable it.</param>
            /// <param name="pRectSource">
            ///     The new source rectangle, in unmagnified screen coordinates, that defines the area of the screen to magnify. This parameter is
            ///     ignored if bEnabled is FALSE.
            /// </param>
            /// <param name="pRectDest">
            ///     The new destination rectangle, in unmagnified screen coordinates, that defines the area of the screen where the magnified screen
            ///     content is displayed. Pen and touch input in this rectangle is mapped to the source rectangle. This parameter is ignored if
            ///     bEnabled is FALSE.
            /// </param>
            /// <returns>Returns TRUE if successful, or FALSE otherwise.</returns>
            /// <remarks>
            ///     The input transformation maps the coordinate space of the magnified screen content to the actual (unmagnified) screen coordinate
            ///     space. This enables the system to pass pen and touch input that is entered in magnified screen content, to the correct UI element
            ///     on the screen. For example, without input transformation, input is passed to the element located at the unmagnified screen
            ///     coordinates, not to the item that appears in the magnified screen content.
            /// </remarks>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/magnification/nf-magnification-magsetinputtransform">MagSetInputTransform</see>
            [DllImport(MagnificationDll, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool MagSetInputTransform([MarshalAs(UnmanagedType.Bool)] bool fEnabled, [Optional] in RECT pRectSource, [Optional] in RECT pRectDest);
            /// <summary>
            ///     Sets the list of windows to be magnified or the list of windows to be excluded from magnification.
            /// </summary>
            /// <param name="hwnd">The handle of the magnification window.</param>
            /// <param name="dwFilterMode">The <see cref="MW_FILTERMODE"/>.</param>
            /// <param name="count">The number of window handles in the list.</param>
            /// <param name="pHWND">The list of window handles.</param>
            /// <returns>Returns TRUE if successful, or FALSE otherwise.</returns>
            /// <remarks>
            ///     Only one window list is used. You can specify either MW_FILTERMODE_INCLUDE or MW_FILTERMODE_EXCLUDE, depending on whether it is
            ///     more convenient to list included windows or excluded windows.
            /// </remarks>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/magnification/nf-magnification-magsetwindowfilterlist">MagSetWindowFilterList</see>
            [DllImport(MagnificationDll, SetLastError = false, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool MagSetWindowFilterList(IntPtr hwnd, MW_FILTERMODE dwFilterMode, int count, [In, MarshalAs(UnmanagedType.LPArray, SizeConst = 2)] IntPtr[] pHWND);
            /// <summary>
            ///     Sets the source rectangle for the magnification window.
            /// </summary>
            /// <param name="hwnd">The magnification window.</param>
            /// <param name="rect">The rectangle to be magnified, in desktop coordinates.</param>
            /// <returns>Returns TRUE if successful, or FALSE otherwise.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/magnification/nf-magnification-magsetwindowsource">MagSetWindowSource</see>
            [DllImport(MagnificationDll, SetLastError = false, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool MagSetWindowSource(IntPtr hwnd, RECT rect);
            /// <summary>
            ///     Sets the transformation matrix for a magnifier control.
            /// </summary>
            /// <param name="hwnd">The magnification window.</param>
            /// <param name="pTransform">A transformation matrix.</param>
            /// <returns>Returns TRUE if successful, or FALSE otherwise.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/magnification/nf-magnification-magsetwindowtransform">MagSetWindowTransform</see>
            [DllImport(MagnificationDll)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool MagSetWindowTransform(IntPtr hwnd, MagImageScalingCallback pTransform);
            /// <summary>
            ///     Shows or hides the system cursor.
            /// </summary>
            /// <param name="fShowCursor">TRUE to show the system cursor, or FALSE to hide it.</param>
            /// <returns>Returns TRUE if successful, or FALSE otherwise.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/magnification/nf-magnification-magshowsystemcursor">MagShowSystemCursor</see>
            [DllImport(MagnificationDll)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool MagShowSystemCursor([MarshalAs(UnmanagedType.Bool)] bool fShowCursor);
            /// <summary>
            ///     Destroys the magnifier run-time objects.
            /// </summary>
            /// <returns>Returns TRUE if successful, or FALSE otherwise.</returns>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/magnification/nf-magnification-maguninitialize">MagUninitialize</see>
            [DllImport(MagnificationDll)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool MagUninitialize();

            #region Undocumented APIs
            /* **********************************************************
             * These APIs allow for smoothing of a bitmap or full screen
             * As these are undocumented they're not guaranteed to work
             * **********************************************************/

            /// <summary>
            ///     Enables or disables smoothing for the full-screen magnifier.
            /// </summary>
            /// <param name="useSmoothing"></param>
            /// <returns></returns>
            [DllImport(MagnificationDll, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool MagSetFullscreenUseBitmapSmoothing(bool useSmoothing);
            /// <summary>
            ///     Enables or disables smoothing for the bitmap magnifier.
            /// </summary>
            /// <param name="useSmoothing"></param>
            /// <returns></returns>
            [DllImport(MagnificationDll, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool MagSetLensUseBitmapSmoothing(bool useSmoothing);
            #endregion
        }
    }
}
