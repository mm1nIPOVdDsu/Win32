using System;

namespace Win32.Common.Services.ScreenCapture
{
    /// <summary>
    ///     Defines screen capture information.
    /// </summary>
    public interface IScreenCaptureWindow
    {
        /// <summary>
        ///     The handle of the window to capture
        /// </summary>
        IntPtr Handle { get; }
        /// <summary>
        ///     Whether to gather diagnostic data about the process that owns the handle
        /// </summary>
        bool GatherDiagnostic { get; }
        /// <summary>
        ///     The path the image will be saved to.
        /// </summary>
        string SavePath { get; }
    }
}
