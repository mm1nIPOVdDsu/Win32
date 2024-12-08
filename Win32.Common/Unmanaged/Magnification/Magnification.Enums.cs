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
            ///     The magnification filter mode.
            /// </summary>
            public enum MW_FILTERMODE : uint
            {
                /// <summary>
                ///     Magnify the windows.
                /// </summary>
                MW_FILTERMODE_INCLUDE = 1,
                /// <summary>
                ///     Exclude the windows from magnification.
                /// </summary>
                MW_FILTERMODE_EXCLUDE = 0
            }
        }
    }
}
