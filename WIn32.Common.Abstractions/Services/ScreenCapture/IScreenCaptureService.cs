namespace Win32.Common.Services.ScreenCapture
{
    /// <summary>
    ///     Functionality for taking screen captures of windows.
    /// </summary>
    public interface IScreenCaptureService : IServiceBase
    {
        /// <summary>
        ///     Takes a screen capture of the window that's in the foreground.
        /// </summary>
        /// <param name="imageType">The type of <see cref="CaptureImageType"/> used when saving an image.</param>
        /// <returns><see cref="ScreenCaptureContext"/>.</returns>
        ScreenCaptureContext GetScreenCapture(CaptureImageType imageType);
        /// <summary>
        ///     Takes a screen capture of a window defined <see cref="IScreenCaptureWindow"/>.
        /// </summary>
        /// <param name="captureWindow"><see cref="IScreenCaptureWindow"/></param>
        /// <param name="imageType">The type of <see cref="CaptureImageType"/> used when saving an image.</param>
        /// <returns><see cref="ScreenCaptureContext"/>.</returns>
        ScreenCaptureContext GetScreenCapture(IScreenCaptureWindow captureWindow, CaptureImageType imageType);
    }
}
