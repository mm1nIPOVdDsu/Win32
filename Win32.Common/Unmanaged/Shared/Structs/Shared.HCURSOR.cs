using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

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
            ///     Provides a handle to cursor.
            /// </summary>
            [StructLayout(LayoutKind.Sequential), DebuggerDisplay("{handle}")]
            public struct HCURSOR : IGraphicsObjectHandle
            {
                private readonly IntPtr handle;

                /// <summary>
                ///     Initializes a new instance of the <see cref="HCURSOR"/> struct.
                /// </summary>
                /// <param name="preexistingHandle">An <see cref="IntPtr"/> object that represents the pre-existing handle to use.</param>
                public HCURSOR(IntPtr preexistingHandle) => handle = preexistingHandle;

                /// <summary>
                ///     Returns an invalid handle by instantiating a <see cref="HCURSOR"/> object with <see cref="IntPtr.Zero"/>.
                /// </summary>
                public static HCURSOR NULL => new(IntPtr.Zero);

                /// <summary>
                ///     Gets a value indicating whether this instance is a null handle.
                /// </summary>
                public bool IsNull => handle == IntPtr.Zero;

                /// <summary>
                ///     Performs an explicit conversion from <see cref="HCURSOR"/> to <see cref="IntPtr"/>.
                /// </summary>
                /// <param name="h">The handle.</param>
                /// <returns>The result of the conversion.</returns>
                public static explicit operator IntPtr(HCURSOR h) => h.handle;

                /// <summary>
                ///     Performs an implicit conversion from <see cref="IntPtr"/> to <see cref="HCURSOR"/>.
                /// </summary>
                /// <param name="h">The pointer to a handle.</param>
                /// <returns>The result of the conversion.</returns>
                public static implicit operator HCURSOR(IntPtr h) => new(h);

                /// <summary>
                ///     Performs an implicit conversion from <see cref="HGDIOBJ"/> to <see cref="HCURSOR"/>.
                /// </summary>
                /// <param name="h">The pointer to a GDI handle.</param>
                /// <returns>The result of the conversion.</returns>
                public static implicit operator HCURSOR(HGDIOBJ h) => new((IntPtr)h);

                /// <summary>
                ///     Implements the operator ! which returns <see langword="true"/> if the handle is invalid.
                /// </summary>
                /// <param name="hMem">The <see cref="HCURSOR"/> instance.</param>
                /// <returns>The result of the operator.</returns>
                public static bool operator !(HCURSOR hMem) => hMem.IsNull;

                /// <summary>
                ///     Implements the operator !=.
                /// </summary>
                /// <param name="h1">The first handle.</param>
                /// <param name="h2">The second handle.</param>
                /// <returns>The result of the operator.</returns>
                public static bool operator !=(HCURSOR h1, HCURSOR h2) => !(h1 == h2);

                /// <summary>
                ///     Implements the operator ==.
                /// </summary>
                /// <param name="h1">The first handle.</param>
                /// <param name="h2">The second handle.</param>
                /// <returns>The result of the operator.</returns>
                public static bool operator ==(HCURSOR h1, HCURSOR h2) => h1.Equals(h2);

                /// <inheritdoc/>
                public override bool Equals(object? obj) => obj is HCURSOR h && handle == h.handle;

                /// <inheritdoc/>
                public override int GetHashCode() => handle.GetHashCode();

                /// <inheritdoc/>
                public IntPtr DangerousGetHandle() => handle;
            }
        }
    }
}
