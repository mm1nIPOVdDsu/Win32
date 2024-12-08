using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

using static Win32.Common.Unmanaged.User32.WinUser;

namespace Win32.Common
{
    /// <inheritdoc/>
    internal partial class Unmanaged
    {
        /// <summary>
        ///     Shared interactions.
        /// </summary>
        public partial class Shared
        {
            /// <summary>
            ///     Provides a handle to drawing brush.
            /// </summary>
            [StructLayout(LayoutKind.Sequential), DebuggerDisplay("{handle}")]
            public struct HBRUSH : IGraphicsObjectHandle
            {
                private readonly IntPtr handle;

                /// <summary>
                ///     Initializes a new instance of the <see cref="HBRUSH"/> struct.
                /// </summary>
                /// <param name="preexistingHandle">An <see cref="IntPtr"/> object that represents the pre-existing handle to use.</param>
                public HBRUSH(IntPtr preexistingHandle) => handle = preexistingHandle;

                /// <summary>
                ///     Returns an invalid handle by instantiating a <see cref="HBRUSH"/> object with <see cref="IntPtr.Zero"/>.
                /// </summary>
                public static HBRUSH NULL => new(IntPtr.Zero);

                /// <summary>
                ///     Gets a value indicating whether this instance is a null handle.
                /// </summary>
                public bool IsNull => handle == IntPtr.Zero;

                /// <summary>
                ///     Performs an explicit conversion from <see cref="HBRUSH"/> to <see cref="IntPtr"/>.
                /// </summary>
                /// <param name="h">The handle.</param>
                /// <returns>The result of the conversion.</returns>
                public static explicit operator IntPtr(HBRUSH h) => h.handle;

                /// <summary>
                ///     Performs an implicit conversion from <see cref="IntPtr"/> to <see cref="HBRUSH"/>.
                /// </summary>
                /// <param name="h">The pointer to a handle.</param>
                /// <returns>The result of the conversion.</returns>
                public static implicit operator HBRUSH(IntPtr h) => new(h);

                /// <summary>
                ///     Performs an implicit conversion from <see cref="HGDIOBJ"/> to <see cref="HBRUSH"/>.
                /// </summary>
                /// <param name="h">The pointer to a GDI handle.</param>
                /// <returns>The result of the conversion.</returns>
                public static implicit operator HBRUSH(HGDIOBJ h) => new((IntPtr)h);

                /// <summary>
                ///     Performs an implicit conversion from <see cref="SystemColorIndex"/> to <see cref="HBRUSH"/>.
                /// </summary>
                /// <param name="i">The color index.</param>
                /// <returns>The result of the conversion.</returns>
                public static implicit operator HBRUSH(SystemColorIndex i) => new((IntPtr)i);

                /// <summary>
                ///     Implements the operator ! which returns <see langword="true"/> if the handle is invalid.
                /// </summary>
                /// <param name="hMem">The <see cref="HBRUSH"/> instance.</param>
                /// <returns>The result of the operator.</returns>
                public static bool operator !(HBRUSH hMem) => hMem.IsNull;

                /// <summary>
                ///     Implements the operator !=.
                /// </summary>
                /// <param name="h1">The first handle.</param>
                /// <param name="h2">The second handle.</param>
                /// <returns>The result of the operator.</returns>
                public static bool operator !=(HBRUSH h1, HBRUSH h2) => !(h1 == h2);

                /// <summary>
                ///     Implements the operator ==.
                /// </summary>
                /// <param name="h1">The first handle.</param>
                /// <param name="h2">The second handle.</param>
                /// <returns>The result of the operator.</returns>
                public static bool operator ==(HBRUSH h1, HBRUSH h2) => h1.Equals(h2);

                /// <inheritdoc/>
                public override bool Equals(object? obj) => obj is HBRUSH h && handle == h.handle;

                /// <inheritdoc/>
                public override int GetHashCode() => handle.GetHashCode();

                /// <inheritdoc/>
                public IntPtr DangerousGetHandle() => handle;
            }
        }
    }
}
