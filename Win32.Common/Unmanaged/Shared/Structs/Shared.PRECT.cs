﻿using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
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
            ///     Defines the coordinates of the upper-left and lower-right corners of a rectangle.
            /// </summary>
            /// <remarks>
            ///     By convention, the right and bottom edges of the rectangle are normally considered exclusive. In other words, the pixel whose
            ///     coordinates are ( right, bottom ) lies immediately outside of the rectangle. For example, when RECT is passed to the FillRect
            ///     function, the rectangle is filled up to, but not including, the right column and bottom row of pixels. This structure is identical
            ///     to the RECT structure.
            /// </remarks>
            [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(PRECTConverter))]
            public class PRECT : IEquatable<PRECT>, IEquatable<RECT>, IEquatable<Rectangle>
            {
                internal RECT rect;

                /// <summary>
                ///     Initializes a new instance of the <see cref="PRECT"/> class with all values set to 0.
                /// </summary>
                public PRECT() { }

                /// <summary>
                ///     Initializes a new instance of the <see cref="PRECT"/> class.
                /// </summary>
                /// <param name="left">The left.</param>
                /// <param name="top">The top.</param>
                /// <param name="right">The right.</param>
                /// <param name="bottom">The bottom.</param>
                public PRECT(int left, int top, int right, int bottom) => rect = new RECT(left, top, right, bottom);

                /// <summary>
                ///     Initializes a new instance of the <see cref="PRECT"/> class.
                /// </summary>
                /// <param name="r">The <see cref="Rectangle"/> structure.</param>
                public PRECT(Rectangle r) => rect = new RECT(r);

                /// <summary>
                ///     Initializes a new instance of the <see cref="PRECT"/> class.
                /// </summary>
                /// <param name="r">The r.</param>
                [ExcludeFromCodeCoverage]
                private PRECT(RECT r) => rect = r;

                /// <summary>
                ///     The x-coordinate of the upper-left corner of the rectangle.
                /// </summary>
                public int Left
                {
                    get => rect.left;
                    set => rect.left = value;
                }

                /// <summary>
                ///     The y-coordinate of the upper-left corner of the rectangle.
                /// </summary>
                public int Top
                {
                    get => rect.top;
                    set => rect.top = value;
                }

                /// <summary>
                ///     he x-coordinate of the lower-right corner of the rectangle.
                /// </summary>
                public int Right
                {
                    get => rect.right;
                    set => rect.right = value;
                }

                /// <summary>
                ///     he y-coordinate of the lower-right corner of the rectangle.
                /// </summary>
                public int Bottom
                {
                    get => rect.bottom;
                    set => rect.bottom = value;
                }

                /// <summary>
                ///     Gets or sets the x-coordinate of the upper-left corner of this <see cref="PRECT"/> structure.
                /// </summary>
                /// <value>The x-coordinate of the upper-left corner of this <see cref="PRECT"/> structure. The default is 0.</value>
                public int X
                {
                    get => rect.X;
                    set => rect.X = value;
                }

                /// <summary>
                ///     Gets or sets the y-coordinate of the upper-left corner of this <see cref="PRECT"/> structure.
                /// </summary>
                /// <value>The y-coordinate of the upper-left corner of this <see cref="PRECT"/> structure. The default is 0.</value>
                public int Y
                {
                    get => rect.Y;
                    set => rect.Y = value;
                }

                /// <summary>
                ///     Gets or sets the height of this <see cref="PRECT"/> structure.
                /// </summary>
                /// <value>The height of this <see cref="PRECT"/> structure. The default is 0.</value>
                public int Height
                {
                    get => rect.Height;
                    set => rect.Height = value;
                }

                /// <summary>
                ///     Gets or sets the width of this <see cref="PRECT"/> structure.
                /// </summary>
                /// <value>The width of this <see cref="PRECT"/> structure. The default is 0.</value>
                public int Width
                {
                    get => rect.Width;
                    set => rect.Width = value;
                }

                /// <summary>
                ///     Gets or sets the coordinates of the upper-left corner of this <see cref="PRECT"/> structure.
                /// </summary>
                /// <value>A Point that represents the upper-left corner of this <see cref="PRECT"/> structure.</value>
                public POINT Location
                {
                    get => rect.Location;
                    set => rect.Location = value;
                }

                /// <summary>
                ///     Gets or sets the size of this <see cref="PRECT"/> structure.
                /// </summary>
                /// <value>A Size that represents the width and height of this <see cref="PRECT"/> structure.</value>
                public SIZE Size
                {
                    get => rect.Size;
                    set => rect.Size = value;
                }

                /// <summary>
                ///     Tests whether all numeric properties of this <see cref="RECT"/> have values of zero.
                /// </summary>
                /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
                public bool IsEmpty => rect.IsEmpty;

                /// <summary>
                ///     Performs an implicit conversion from <see cref="PRECT"/> to <see cref="RECT"/>.
                /// </summary>
                /// <param name="r">The <see cref="PRECT"/> to convert.</param>
                /// <returns>The result of the conversion.</returns>
                public static implicit operator RECT?(PRECT r) => r?.rect;

                /// <summary>
                ///     Performs an implicit conversion from <see cref="PRECT"/> to <see cref="Rectangle"/>.
                /// </summary>
                /// <param name="r">The <see cref="PRECT"/> to convert.</param>
                /// <returns>The result of the conversion.</returns>
                public static implicit operator Rectangle?(PRECT r) => r?.rect;

                /// <summary>
                ///     Performs an implicit conversion from <see cref="System.Nullable{Rectangle}"/> to <see cref="PRECT"/>.
                /// </summary>
                /// <param name="r">The <see cref="Rectangle"/> to convert.</param>
                /// <returns>The result of the conversion.</returns>
                public static implicit operator PRECT?(Rectangle? r) => r.HasValue ? new PRECT(r.Value) : null;

                /// <summary>
                ///     Performs an implicit conversion from <see cref="System.Nullable{RECT}"/> to <see cref="PRECT"/>.
                /// </summary>
                /// <param name="r">The <see cref="RECT"/> to convert.</param>
                /// <returns>The result of the conversion.</returns>
                public static implicit operator PRECT?(RECT? r) => r.HasValue ? new PRECT(r.Value) : null;

                /// <summary>
                ///     Implements the operator ==.
                /// </summary>
                /// <param name="r1">The first <see cref="PRECT"/> structure.</param>
                /// <param name="r2">The second <see cref="PRECT"/> structure.</param>
                /// <returns>The result of the operator.</returns>
                public static bool operator ==(PRECT r1, PRECT r2)
                {
                    if (ReferenceEquals(r1, r2))
                        return true;
                    if (r1 is null || r2 is null)
                        return false;
                    return r1.Equals(r2);
                }

                /// <summary>
                ///     Implements the operator !=.
                /// </summary>
                /// <param name="r1">The first <see cref="PRECT"/> structure.</param>
                /// <param name="r2">The second <see cref="PRECT"/> structure.</param>
                /// <returns>The result of the operator.</returns>
                public static bool operator !=(PRECT r1, PRECT r2) => !(r1 == r2);

                /// <summary>
                ///     Determines whether the specified <see cref="PRECT"/>, is equal to this instance.
                /// </summary>
                /// <param name="r">The <see cref="PRECT"/> to compare with this instance.</param>
                /// <returns><c>true</c> if the specified <see cref="PRECT"/> is equal to this instance; otherwise, <c>false</c>.</returns>
                public bool Equals(PRECT? r) => rect == r?.rect;

                /// <summary>
                ///     Determines whether the specified <see cref="RECT"/>, is equal to this instance.
                /// </summary>
                /// <param name="r">The <see cref="RECT"/> to compare with this instance.</param>
                /// <returns><c>true</c> if the specified <see cref="RECT"/> is equal to this instance; otherwise, <c>false</c>.</returns>
                public bool Equals(RECT r) => rect.Equals(r);

                /// <summary>
                ///     Determines whether the specified <see cref="Rectangle"/>, is equal to this instance.
                /// </summary>
                /// <param name="r">The <see cref="Rectangle"/> to compare with this instance.</param>
                /// <returns><c>true</c> if the specified <see cref="Rectangle"/> is equal to this instance; otherwise, <c>false</c>.</returns>
                public bool Equals(Rectangle r) => rect.Equals(r);

                /// <summary>
                ///     Determines whether the specified <see cref="object"/>, is equal to this instance.
                /// </summary>
                /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
                /// <returns><c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
                public override bool Equals(object? obj) => rect.Equals(obj);

                /// <summary>
                ///     Returns a hash code for this instance.
                /// </summary>
                /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
                public override int GetHashCode() => rect.GetHashCode();

                /// <summary>
                ///     Returns a <see cref="string"/> that represents this instance.
                /// </summary>
                /// <returns>A <see cref="string"/> that represents this instance.</returns>
                public override string ToString() => rect.ToString();
            }

            /// <summary>
            ///     
            /// </summary>
            internal class PRECTConverter : RECTConverter
            {
                public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
                {
                    var b = base.ConvertFrom(context, culture, value);
                    return b is RECT r ? new PRECT(r) : b;
                }

                public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
                {
                    if (value is PRECT prect && destinationType == typeof(InstanceDescriptor))
                    {
                        var ctor = typeof(PRECT).GetConstructor(new[] { typeof(int), typeof(int), typeof(int), typeof(int) });
                        return new InstanceDescriptor(ctor, new object[] { prect.Left, prect.Top, prect.Right, prect.Bottom });
                    }
                    return base.ConvertTo(context, culture, value, destinationType);
                }

                public override object CreateInstance(ITypeDescriptorContext? context, IDictionary? propertyValues)
                {
                    if (propertyValues is null)
                        throw new ArgumentNullException(nameof(propertyValues));

                    var left = propertyValues["left"] ?? 0;
                    var top = propertyValues["top"] ?? 0;
                    var right = propertyValues["right"] ?? 0;
                    var bottom = propertyValues["bottom"] ?? 0;

                    return left is int l && top is int t && right is int r && bottom is int b ? new PRECT(l, t, r, b) :
                        throw new ArgumentException(@"Invalid property value.");
                }

#if NET6_0_OR_GREATER
                [RequiresUnreferencedCode(null)]
#endif
                public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext? context, object? value, Attribute[]? attributes)
                {
                    var props = TypeDescriptor.GetProperties(typeof(PRECT), attributes);
                    return props.Sort(new[] { "left", "top", "right", "bottom" });
                }
            }

            internal class RECTConverter : TypeConverter
            {
                public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType) =>
                    sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);


                public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType) =>
                    destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);

                public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
                {
                    if (value is string strValue)
                    {
                        var text = strValue.Trim();
                        if (text.Length == 0)
                            return null;

                        culture ??= CultureInfo.CurrentCulture;
                        var tokens = text.Split(culture.TextInfo.ListSeparator[0]);
                        if (tokens.Length == 4)
                        {
                            var intConverter = TypeDescriptor.GetConverter(typeof(int));
                            var values = Array.ConvertAll(tokens, i => Convert.ToInt32(intConverter.ConvertFromString(context, culture, i)));
                            return new RECT(values[0], values[1], values[2], values[3]);
                        }
                    }
                    return base.ConvertFrom(context, culture, value);
                }

                public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
                {
                    if (destinationType is null)
                        throw new ArgumentNullException(nameof(destinationType));

                    if (value is not RECT rect)
                        return base.ConvertTo(context, culture, value, destinationType);

                    culture ??= CultureInfo.CurrentCulture;

                    if (destinationType == typeof(string))
                        return IntConvertToString(context, culture, rect);

                    if (destinationType == typeof(InstanceDescriptor))
                    {
                        var ctor = typeof(RECT).GetConstructor(new[] { typeof(int), typeof(int), typeof(int), typeof(int) });
                        return new InstanceDescriptor(ctor, new[] { rect.left, rect.top, rect.right, rect.bottom });
                    }

                    return base.ConvertTo(context, culture, value, destinationType);
                }

                public override object CreateInstance(ITypeDescriptorContext? context, IDictionary? propertyValues)
                {
                    if (propertyValues is null)
                        throw new ArgumentNullException(nameof(propertyValues));

                    var left = propertyValues["left"];
                    var top = propertyValues["top"];
                    var right = propertyValues["right"];
                    var bottom = propertyValues["bottom"];

                    return left is int l && top is int t && right is int r && bottom is int b ? new RECT(l, t, r, b) :
                        throw new ArgumentException(@"Invalid property value.");
                }

                public override bool GetCreateInstanceSupported(ITypeDescriptorContext? context) => true;

#if NET6_0_OR_GREATER
                [RequiresUnreferencedCode(null)]
#endif
                public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext? context, object value, Attribute[]? attributes)
                {
                    var props = TypeDescriptor.GetProperties(typeof(RECT), attributes);
                    return props.Sort(new[] { "left", "top", "right", "bottom" });
                }

                public override bool GetPropertiesSupported(ITypeDescriptorContext? context) => true;

                protected static string IntConvertToString(ITypeDescriptorContext? context, CultureInfo culture, RECT rect)
                {
                    if (context is null)
                        throw new ArgumentNullException(nameof(context));
                    if (culture is null)
                        throw new ArgumentNullException(nameof(culture));
                    if (context is null)
                        throw new ArgumentNullException(nameof(context));

                    var intConverter = TypeDescriptor.GetConverter(typeof(int));
                    if (intConverter is null)
                        throw new NullReferenceException(nameof(intConverter));

                    var args = new string?[4];
                    var nArg = 0;

                    args[nArg++] = intConverter.ConvertToString(context, culture, rect.left);
                    args[nArg++] = intConverter.ConvertToString(context, culture, rect.top);
                    args[nArg++] = intConverter.ConvertToString(context, culture, rect.right);
                    args[nArg++] = intConverter.ConvertToString(context, culture, rect.bottom);

                    return string.Join(culture.TextInfo.ListSeparator + " ", args);
                }
            }
        }
    }
}