using System;
using System.Runtime.InteropServices;

using static Win32.Common.Unmanaged.Shared;

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
            ///     Describes a color transformation matrix that a magnifier control uses to apply a color effect to magnified screen content.
            /// </summary>
            /// <remarks>
            ///     The values in the matrix are for red, blue, green, alpha, and color translation. For more information, see Using a Color Matrix to
            ///     Transform a Single Color in the Windows GDI+ documentation.
            /// </remarks>
            [StructLayout(LayoutKind.Sequential)]
            public struct MAGCOLOREFFECT
            {
                private const int dimLen = 5;

                private float transform00;
                private float transform01;
                private float transform02;
                private float transform03;
                private float transform04;
                private float transform10;
                private float transform11;
                private float transform12;
                private float transform13;
                private float transform14;
                private float transform20;
                private float transform21;
                private float transform22;
                private float transform23;
                private float transform24;
                private float transform30;
                private float transform31;
                private float transform32;
                private float transform33;
                private float transform34;
                private float transform40;
                private float transform41;
                private float transform42;
                private float transform43;
                private float transform44;

                /// <summary>
                ///     Initializes a new instance of the <see cref="MAGCOLOREFFECT"/> struct with initial values.
                /// </summary>
                /// <param name="values">The values.</param>
                public MAGCOLOREFFECT(float[,] values) : this() => transform = values;

                /// <summary>
                ///     <para>Type: <c>float[5,5]</c></para>
                ///     <para>The transformation matrix.</para>
                /// </summary>
                public float this[int x, int y]
                {
                    get
                    {
                        if (x < 0 || x >= dimLen || y < 0 || y >= dimLen)
                            throw new ArgumentOutOfRangeException();
                        unsafe
                        {
                            fixed (float* f = &transform00)
                            {
                                return f[(x * dimLen) + y];
                            }
                        }
                    }
                    set
                    {
                        if (x < 0 || x >= dimLen || y < 0 || y >= dimLen)
                            throw new ArgumentOutOfRangeException();
                        unsafe
                        {
                            fixed (float* f = &transform00)
                            {
                                f[(x * dimLen) + y] = value;
                            }
                        }
                    }
                }

                /// <summary>
                ///     <para>Type: <c>float[5,5]</c></para>
                ///     <para>The transformation matrix.</para>
                /// </summary>
#pragma warning disable IDE1006 // Naming Styles
                public float[,] transform
#pragma warning restore IDE1006 // Naming Styles
                {
                    get => new float[,] { { transform00, transform01, transform02, transform03, transform04 },
                    { transform10, transform11, transform12, transform13, transform14 },
                    { transform20, transform21, transform22, transform23, transform24 },
                    { transform30, transform31, transform32, transform33, transform34 },
                    { transform40, transform41, transform42, transform43, transform44 } };
                    set
                    {
                        if (value is null) throw new ArgumentNullException();
                        if (value.Rank != 2 && value.GetLength(0) != dimLen && value.GetLength(1) != dimLen)
                            throw new ArgumentOutOfRangeException();
                        unsafe
                        {
                            fixed (float* f = &transform00)
                            {
                                for (var x = 0; x < dimLen; x++)
                                {
                                    for (var y = 0; y < dimLen; y++)
                                        f[(x * dimLen) + y] = value[x, y];
                                }
                            }
                        }
                    }
                }

                /// <summary>
                ///     Gets a value indicating whether this <see cref="MAGCOLOREFFECT"/> is empty (all values are zero).
                /// </summary>
                /// <value>This property is <see langword="true"/> if this <see cref="MAGCOLOREFFECT"/> is empty; otherwise, <see langword="false"/>.</value>
                public bool IsEmpty
                {
                    get
                    {
                        unsafe
                        {
                            fixed (float* f = &transform00)
                            {
                                for (var x = 0; x < dimLen; x++)
                                {
                                    for (var y = 0; y < dimLen; y++)
                                    {
                                        if (f[(x * dimLen) + y] != 0f)
                                            return false;
                                    }
                                }

                                return true;
                            }
                        }
                    }
                }

                /// <summary>
                ///     Gets a value indicating whether this <see cref="MAGTRANSFORM"/> is the identity matrix.
                /// </summary>
                /// <value>This property is <see langword="true"/> if this <see cref="MAGTRANSFORM"/> is identity; otherwise, <see langword="false"/>.</value>
                public bool IsIdentity
                {
                    get
                    {
                        unsafe
                        {
                            fixed (float* f = &transform00)
                            {
                                for (var x = 0; x < dimLen; x++)
                                {
                                    for (var y = 0; y < dimLen; y++)
                                    {
                                        if (f[(x * dimLen) + y] != (x == y ? 1f : 0f))
                                            return false;
                                    }
                                }

                                return true;
                            }
                        }
                    }
                }

                /// <summary>
                ///     Determines whether the specified <see cref="System.Object"/>, is equal to this instance.
                /// </summary>
                /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
                /// <returns><see langword="true"/> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <see langword="false"/>.</returns>
                public override bool Equals(object? obj) => obj is MAGCOLOREFFECT m && Equals(m);

                /// <summary>
                ///     Determines whether the specified <see cref="MAGCOLOREFFECT"/>, is equal to this instance.
                /// </summary>
                /// <param name="effect">The <see cref="MAGCOLOREFFECT"/> to compare with this instance.</param>
                /// <returns><see langword="true"/> if the specified <see cref="MAGCOLOREFFECT"/> is equal to this instance; otherwise, <see langword="false"/>.</returns>
                public bool Equals(MAGCOLOREFFECT effect)
                {
                    unsafe
                    {
                        fixed (float* f1 = &transform00)
                        {
                            var f2 = &effect.transform00;
                            for (var x = 0; x < dimLen; x++)
                            {
                                for (var y = 0; y < dimLen; y++)
                                {
                                    if (f1[(x * dimLen) + y] != f2[(x * dimLen) + y])
                                        return false;
                                }
                            }

                            return true;
                        }
                    }
                }

                /// <summary>
                ///     Returns a hash code for this instance.
                /// </summary>
                /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
                public override int GetHashCode() => transform.GetHashCode();

                /// <summary>
                ///     Implements the operator ==.
                /// </summary>
                /// <param name="lhs">The left-hand side argument.</param>
                /// <param name="rhs">The right-hand side argument.</param>
                /// <returns>The result of the operator.</returns>
                public static bool operator ==(MAGCOLOREFFECT lhs, MAGCOLOREFFECT rhs) => lhs.Equals(rhs);

                /// <summary>
                ///     Implements the operator !=.
                /// </summary>
                /// <param name="lhs">The left-hand side argument.</param>
                /// <param name="rhs">The right-hand side argument.</param>
                /// <returns>The result of the operator.</returns>
                public static bool operator !=(MAGCOLOREFFECT lhs, MAGCOLOREFFECT rhs) => !(lhs == rhs);

                /// <summary>
                ///     An Identity Matrix for MAGCOLOREFFECT.
                /// </summary>
                public static readonly MAGCOLOREFFECT Identity = new MAGCOLOREFFECT { transform00 = 1, transform11 = 1, transform22 = 1, transform33 = 1, transform44 = 1 };
            }
            /// <summary>
            ///     Describes a transformation matrix that a magnifier control uses to magnify screen content.
            /// </summary>
            public struct MAGTRANSFORM : IEquatable<MAGTRANSFORM>
            {
                private const int dimLen = 3;

                private float transform00;
                private float transform01;
                private float transform02;
                private float transform10;
                private float transform11;
                private float transform12;
                private float transform20;
                private float transform21;
                private float transform22;

                /// <summary>
                ///     Initializes a new instance of the <see cref="MAGTRANSFORM"/> struct with initial values.
                /// </summary>
                /// <param name="values">The values.</param>
                public MAGTRANSFORM(float[,] values) : this() => transform = values;

                /// <summary>
                ///     Initializes a new instance of the <see cref="MAGTRANSFORM"/> struct with the magnification value set into the transformation matrix.
                /// </summary>
                /// <param name="magnification">The magnification value.</param>
                public MAGTRANSFORM(float magnification) : this() { transform00 = transform11 = magnification; transform22 = 1f; }

                /// <summary>
                ///     <para>Type: <c>float[3,3]</c></para>
                ///     <para>The transformation matrix.</para>
                /// </summary>
                public float this[int x, int y]
                {
                    get
                    {
                        if (x < 0 || x >= dimLen || y < 0 || y >= dimLen)
                            throw new ArgumentOutOfRangeException();
                        unsafe
                        {
                            fixed (float* f = &transform00)
                            {
                                return f[(x * dimLen) + y];
                            }
                        }
                    }
                    set
                    {
                        if (x < 0 || x >= dimLen || y < 0 || y >= dimLen)
                            throw new ArgumentOutOfRangeException();
                        unsafe
                        {
                            fixed (float* f = &transform00)
                            {
                                f[x * dimLen + y] = value;
                            }
                        }
                    }
                }
                /// <summary>
                ///     <para>Type: <c>float[3,3]</c></para>
                ///     <para>The transformation matrix.</para>
                /// </summary>
#pragma warning disable IDE1006 // Naming Styles
                public float[,] transform
#pragma warning restore IDE1006 // Naming Styles
                {
                    get => new float[,] { { transform00, transform01, transform02 },
                    { transform10, transform11, transform12 },
                    { transform20, transform21, transform22 } };
                    set
                    {
                        if (value is null) throw new ArgumentNullException("Value is null.");
                        if (value.Rank != 2 && value.GetLength(0) != dimLen && value.GetLength(1) != dimLen)
                            throw new ArgumentOutOfRangeException("Value is out of range.");
                        unsafe
                        {
                            fixed (float* f = &transform00)
                            {
                                for (var x = 0; x < dimLen; x++)
                                {
                                    for (var y = 0; y < dimLen; y++)
                                    {
                                        f[(x * dimLen) + y] = value[x, y];
                                    }
                                }
                            }
                        }
                    }
                }
#pragma warning restore IDE1006 // Naming Styles
                /// <summary>
                ///     Gets a value indicating whether this <see cref="MAGTRANSFORM"/> is empty (all values are zero).
                /// </summary>
                /// <value>This property is <see langword="true"/> if this <see cref="MAGTRANSFORM"/> is empty; otherwise, <see langword="false"/>.</value>
                public bool IsEmpty
                {
                    get
                    {
                        unsafe
                        {
                            fixed (float* f = &transform00)
                            {
                                for (var x = 0; x < dimLen; x++)
                                {
                                    for (var y = 0; y < dimLen; y++)
                                    {
                                        if (f[(x * dimLen) + y] != 0f)
                                            return false;
                                    }
                                }

                                return true;
                            }
                        }
                    }
                }
                /// <summary>
                ///     Gets a value indicating whether this <see cref="MAGTRANSFORM"/> is the identity matrix.
                /// </summary>
                /// <value>This property is <see langword="true"/> if this <see cref="MAGTRANSFORM"/> is identity; otherwise, <see langword="false"/>.</value>
                public bool IsIdentity
                {
                    get
                    {
                        unsafe
                        {
                            fixed (float* f = &transform00)
                            {
                                for (var x = 0; x < dimLen; x++)
                                {
                                    for (var y = 0; y < dimLen; y++)
                                    {
                                        if (f[(x * dimLen) + y] != (x == y ? 1f : 0f))
                                            return false;
                                    }
                                }

                                return true;
                            }
                        }
                    }
                }

                /// <summary>
                ///     Determines whether the specified <see cref="System.Object"/>, is equal to this instance.
                /// </summary>
                /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
                /// <returns><see langword="true"/> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <see langword="false"/>.</returns>
                public override bool Equals(object? obj) => obj is MAGTRANSFORM m && Equals(m);
                /// <summary>
                ///     Determines whether the specified <see cref="MAGTRANSFORM"/>, is equal to this instance.
                /// </summary>
                /// <param name="effect">The <see cref="MAGTRANSFORM"/> to compare with this instance.</param>
                /// <returns><see langword="true"/> if the specified <see cref="MAGTRANSFORM"/> is equal to this instance; otherwise, <see langword="false"/>.</returns>
                public bool Equals(MAGTRANSFORM effect)
                {
                    unsafe
                    {
                        fixed (float* f1 = &transform00)
                        {
                            var f2 = &effect.transform00;
                            for (var x = 0; x < dimLen; x++)
                            {
                                for (var y = 0; y < dimLen; y++)
                                {
                                    if (f1[(x * dimLen) + y] != f2[x * dimLen + y])
                                        return false;
                                }
                            }

                            return true;
                        }
                    }
                }
                /// <summary>
                ///     Returns a hash code for this instance.
                /// </summary>
                /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
                public override int GetHashCode() => transform.GetHashCode();

                /// <summary>
                ///     Implements the operator ==.
                /// </summary>
                /// <param name="lhs">The left-hand side argument.</param>
                /// <param name="rhs">The right-hand side argument.</param>
                /// <returns>The result of the operator.</returns>
                public static bool operator ==(MAGTRANSFORM lhs, MAGTRANSFORM rhs) => lhs.Equals(rhs);
                /// <summary>
                ///     Implements the operator !=.
                /// </summary>
                /// <param name="lhs">The left-hand side argument.</param>
                /// <param name="rhs">The right-hand side argument.</param>
                /// <returns>The result of the operator.</returns>
                public static bool operator !=(MAGTRANSFORM lhs, MAGTRANSFORM rhs) => !(lhs == rhs);
                /// <summary>
                ///     An Identity Matrix for MAGTRANSFORM.
                /// </summary>
                public static readonly MAGTRANSFORM Identity = new() { transform00 = 1, transform11 = 1, transform22 = 1 };
            }
            /// <summary>
            ///     Describes an image format.
            /// </summary>
            public struct MAGIMAGEHEADER
            {
                /// <summary>
                ///     The width of the image.
                /// </summary>
                public uint width;
                /// <summary>
                ///     The height of the image.
                /// </summary>
                public uint height;
                /// <summary>
                ///     A WICPixelFormatGUID (declared in wincodec.h) that specifies the pixel format of the image. For a list of available pixel
                ///     formats, see the Native Pixel Formats topic.
                /// </summary>
                public Guid format;
                /// <summary>
                ///     The stride, or number of bytes in a row of the image.
                /// </summary>
                public uint stride;
                /// <summary>
                ///     The offset of the start of the image data from the beginning of the file.
                /// </summary>
                public uint offset;
                /// <summary>
                ///     The size of the data.
                /// </summary>
                public SizeT cbSize;
            }
        }
    }
}
