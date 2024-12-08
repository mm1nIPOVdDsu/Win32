namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <inheritdoc/>
        public partial class Kernel32
        {
            /// <summary>
            ///     ProcessEnv interactions.
            /// </summary>
            public partial class ProcessEnv
            {
                /// <summary>
                ///     The standard device.
                /// </summary>
                public enum STD_HANDLE : uint
                {
                    /// <summary>
                    ///     The standard input device.
                    /// </summary>
                    STD_INPUT_HANDLE = 0xFFFFFFF6,
                    /// <summary>
                    ///     The standard output device. 
                    /// </summary>
                    STD_OUTPUT_HANDLE = 0xFFFFFFF5,
                    /// <summary>
                    ///     The standard error device.
                    /// </summary>
                    STD_ERROR_HANDLE = 0xFFFFFFF4
                }
            }
        }
    }
}