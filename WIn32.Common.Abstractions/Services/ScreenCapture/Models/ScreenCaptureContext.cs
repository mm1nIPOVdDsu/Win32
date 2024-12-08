namespace Win32.Common.Services.ScreenCapture
{
    /// <summary>
    ///     The image of a screen capture.
    /// </summary>
    public class ScreenCaptureContext
    {
        /// <summary>
        ///     The type of image contained in <see cref="Content"/>.
        /// </summary>
        public CaptureImageType ImageType { get; set; }
        /// <summary>
        ///     The image type.
        /// </summary>
        public object? Content { get; set; }
    }
}
