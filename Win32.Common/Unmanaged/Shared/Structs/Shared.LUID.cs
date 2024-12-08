using System;
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
            ///     The LUID structure is an opaque structure that specifies an identifier that is guaranteed to be unique on the local machine.
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct LUID
            {
                /// <summary>
                ///     Specifies a DWORD that contains the unsigned lower numbers of the id.
                /// </summary>
                public UInt32 LowPart;
                /// <summary>
                ///     Specifies a LONG that contains the signed high numbers of the id.
                /// </summary>
                public Int32 HighPart;

                /// <summary>
                ///     Initializes a new instance of the <see cref="LUID"/> class.
                /// </summary>
                /// <param name="value">The <see cref="ulong"/> value for a LUID.</param>
                public LUID(ulong value)
                {
                    LowPart = (UInt32)(value & 0xffffffffL);
                    HighPart = (Int32)(value >> 32);
                }
                /// <summary>
                ///     Initializes a new instance of the <see cref="LUID"/> class.
                /// </summary>
                /// <param name="value">The <see cref="LUID"/> value for a LUID.</param>
                public LUID(LUID value)
                {
                    LowPart = value.LowPart;
                    HighPart = value.HighPart;
                }
                /// <summary>
                ///     Initializes a new instance of the <see cref="LUID"/> class.
                /// </summary>
                /// <param name="value">The <see cref="string"/> value for a LUID.</param>
                public LUID(string value)
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(value, @"^0x[0-9A-Fa-f]+$"))
                    {
                        // if the passed LUID string is of form 0xABC123
                        var uintVal = Convert.ToUInt64(value, 16);
                        LowPart = (UInt32)(uintVal & 0xffffffffL);
                        HighPart = (Int32)(uintVal >> 32);
                    }
                    else if (System.Text.RegularExpressions.Regex.IsMatch(value, @"^\d+$"))
                    {
                        // if the passed LUID string is a decimal form
                        var uintVal = UInt64.Parse(value);
                        LowPart = (UInt32)(uintVal & 0xffffffffL);
                        HighPart = (Int32)(uintVal >> 32);
                    }
                    else
                    {
                        var argEx = new ArgumentException("Passed LUID string value is not in a hex or decimal form", value);
                        throw argEx;
                    }
                }
                /// <inheritdoc/>
                public override int GetHashCode()
                {
                    var Value = ((UInt64)HighPart << 32) + LowPart;
                    return Value.GetHashCode();
                }
                /// <inheritdoc/>
                public override bool Equals(object? obj) => obj is LUID lUID && (((ulong)this) == lUID);
                /// <inheritdoc/>
                public override string ToString()
                {
                    var Value = ((UInt64)HighPart << 32) + LowPart;
                    return $"0x{Value:x}";
                }
                /// <inheritdoc/>
                public static bool operator ==(LUID x, LUID y)
                {
                    return x == ((ulong)y);
                }
                /// <inheritdoc/>
                public static bool operator !=(LUID x, LUID y)
                {
                    return x != ((ulong)y);
                }
                /// <inheritdoc/>
                public static implicit operator ulong(LUID luid)
                {
                    // enable casting to a ulong
                    var Value = (UInt64)luid.HighPart << 32;
                    return Value + luid.LowPart;
                }
            }
        }
    }
}
