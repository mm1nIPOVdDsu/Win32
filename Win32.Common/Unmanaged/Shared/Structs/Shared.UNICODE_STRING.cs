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
            ///     The UNICODE_STRING structure is used to define Unicode strings.
            /// </summary>
            /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/ntdef/ns-ntdef-_unicode_string">UNICODE_STRING</see>
            [StructLayout(LayoutKind.Sequential)]
            public struct UNICODE_STRING : IDisposable
            {
                public ushort Length;
                public ushort MaximumLength;
                private IntPtr Buffer;

                /// <summary>
                ///     Initializes a new instance of the <see cref="UNICODE_STRING"/> struct.
                /// </summary>
                /// <param name="value"></param>
                public UNICODE_STRING(string value)
                {
                    Length = (ushort)(value.Length * 2);
                    MaximumLength = (ushort)(value.Length + 2);
                    Buffer = Marshal.StringToHGlobalUni(value);
                }

                /// <summary>
                ///     Performs application-defined tasks associated with freeing, releasing, or resetting
                ///     unmanaged resources.
                /// </summary>
                public void Dispose()
                {
                    Marshal.FreeHGlobal(Buffer);
                    Buffer = IntPtr.Zero;
                }
                /// <inheritdoc/>
                public override string? ToString()
                {
                    if (Buffer == IntPtr.Zero)
                        return string.Empty;

                    return Marshal.PtrToStringUni(Buffer);
                }
            }
        }
    }
}