using System.Runtime.InteropServices;

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
                ///     Contains the time of the last input.
                /// </summary>
                [StructLayout(LayoutKind.Sequential)]
                public struct LASTINPUTINFO
                {
                    public static readonly int SizeOf = Marshal.SizeOf(typeof(LASTINPUTINFO));

                    /// <summary>
                    ///     The size of the structure, in bytes. This member must be set to sizeof(LASTINPUTINFO).
                    /// </summary>
                    [MarshalAs(UnmanagedType.U4)]
                    public uint cbSize;
                    /// <summary>
                    ///     The tick count when the last input event was received.
                    /// </summary>
                    [MarshalAs(UnmanagedType.U4)]
                    public uint dwTime;
                }
                // NOTE: See Shared
                ///// <summary>
                /////     The RECT structure defines a rectangle by the coordinates of its upper-left and lower-right corners.
                ///// </summary>
                //public struct RECT
                //{
                //    /// <summary>
                //    ///     Specifies the x-coordinate of the upper-left corner of the rectangle.
                //    /// </summary>
                //    public readonly int left;
                //    /// <summary>
                //    ///     Specifies the y-coordinate of the upper-left corner of the rectangle.
                //    /// </summary>
                //    public readonly int top;
                //    /// <summary>
                //    ///     Specifies the x-coordinate of the lower-right corner of the rectangle.
                //    /// </summary>
                //    public readonly int right;
                //    /// <summary>
                //    ///     Specifies the y-coordinate of the lower-right corner of the rectangle.
                //    /// </summary>
                //    public readonly int bottom;
                //}
            }
        }
    }
}
