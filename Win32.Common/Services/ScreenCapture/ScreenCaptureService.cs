using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

using Microsoft.Extensions.Logging;

using static Win32.Common.Unmanaged.Shared;
using static Win32.Common.Unmanaged.User32.WinUser;

namespace Win32.Common.Services.ScreenCapture
{
    /// <summary>
    ///     Functionality for taking screen captures of windows.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class ScreenCaptureService : IScreenCaptureService
    {
        private readonly ILogger<ScreenCaptureService> _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScreenCaptureService"/> class.
        /// </summary>
        /// <param name="logger">An instance of a <see cref="ILogger{ScreenCaptureService}"/>.</param>
        public ScreenCaptureService(ILogger<ScreenCaptureService> logger) => _logger = logger;

        /// <summary>
        ///     Takes a screen capture of the window that's in the foreground.
        /// </summary>
        /// <param name="imageType">The type of <see cref="CaptureImageType"/> used when saving an image.</param>
        /// <returns><see cref="ScreenCaptureContext"/>.</returns>
        public ScreenCaptureContext GetScreenCapture(CaptureImageType imageType)
        {
            _logger.LogDebug("Screen capture foreground window.");
            var handle = GetForegroundWindow();
            var captureWindow = new ScreenCaptureWindow()
            {
                GatherDiagnostic = false,
                Handle = handle
            };
            return GetScreenCapture(captureWindow, imageType);
        }
        /// <summary>
        ///     Takes a screen capture of a window defined <see cref="IScreenCaptureWindow"/>.
        /// </summary>
        /// <param name="captureWindow"><see cref="IScreenCaptureWindow"/></param>
        /// <param name="imageType">The type of <see cref="CaptureImageType"/> used when saving an image.</param>
        /// <returns><see cref="ScreenCaptureContext"/>.</returns>
        public ScreenCaptureContext GetScreenCapture(IScreenCaptureWindow captureWindow, CaptureImageType imageType)
        {
            // TODO: add more image types.
            _logger.LogDebug("Getting screenshot for window.");
            switch (imageType)
            {
                case CaptureImageType.Bitmap:
                    return GetBitmapScreenCapture(captureWindow.Handle);
                default:
                    return GetBitmapScreenCapture(captureWindow.Handle);
            }
        }

        /// <summary>
        ///     Takes a screen capture of a window defined by its handle.
        /// </summary>
        /// <param name="handle">The window handle.</param>
        /// <returns><see cref="ScreenCaptureContext"/> where T is a <see cref="Bitmap"/>.</returns>
        private ScreenCaptureContext GetBitmapScreenCapture(IntPtr handle)
        {
            if (handle == IntPtr.Zero)
                throw new Exception("Window handle cannot be zero.");

            var screenCaptureContext = new ScreenCaptureContext()
            {
                ImageType = CaptureImageType.Bitmap
            };

            _logger.LogDebug("Getting the User32.RECT for the window.");
            var rectangle = default(RECT);
            if (GetWindowRect(handle, ref rectangle) == false)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            var width = rectangle.right - rectangle.left;
            var height = rectangle.bottom - rectangle.top;

            new Point(rectangle.left, rectangle.top); // no idea what this does or if its needed
            var bitmap = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                try
                {
                    _logger.LogDebug("Creating image of the window.");
                    var hdc = graphics.GetHdc();
                    PrintWindow(handle, hdc, 0u);
                    graphics.ReleaseHdc(hdc);
                    screenCaptureContext.Content = bitmap;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error capturing main application window.");
                }
                return screenCaptureContext;
            }
        }
        /// <summary>
        ///     Checks if the provided handle comes from the current process.
        /// </summary>
        /// <param name="handle">The handle of the window to test.</param>
        /// <returns>True if the handle comes from the current process.</returns>
        private bool EnsureHandleBelongsToThisProcess(IntPtr handle)
        {
            if (handle == IntPtr.Zero)
            {
                _logger.LogDebug("IntPtr.Zero was is not a valid window handle.");
                return false;
            }

            _logger.LogDebug("Getting windows thread process id for provided window handle.");
            if (GetWindowThreadProcessId(handle, out var processId) is not 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            var id = Environment.ProcessId;
            if (id != processId.ToInt32())
            {
                _logger.LogDebug("Requested window handle {id} does not belong to this process {processId}.", id, processId.ToInt32());
                return false;
            }

            return true;
        }
    }
}
