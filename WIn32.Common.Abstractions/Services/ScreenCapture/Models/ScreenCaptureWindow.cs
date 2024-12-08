using System;

namespace Win32.Common.Services.ScreenCapture
{
    /// <summary>
    ///     Information about a window to capture a screenshot of.
    /// </summary>
    public class ScreenCaptureWindow : IScreenCaptureWindow
    {
        /// <summary>
        ///     The handle of the window to capture
        /// </summary>
        public IntPtr Handle { get; set; }
        /// <summary>
        ///     Whether to gather diagnostic data about the process that owns the handle
        /// </summary>
        public bool GatherDiagnostic { get; set; }
        /// <summary>
        ///     The path the image will be saved to.
        /// </summary>
        public string SavePath { get; set; } = "";
    }
}
