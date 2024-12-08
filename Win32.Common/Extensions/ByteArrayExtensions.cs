using System;

namespace Win32.Common.Extensions
{
    /// <summary>
    ///     Extensions for <see cref="byte"/> arrays.
    /// </summary>
    public static class ByteArrayExtensions
    {
        /// <summary>
        ///     TODO: Summary.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static byte[] Concatenate(this byte[] a, byte[] b)
        {
            var result = new byte[a.Length + b.Length];
            Array.Copy(a, 0, result, 0, a.Length);
            Array.Copy(b, 0, result, a.Length, b.Length);
            return result;
        }
        /// <summary>
        ///     TODO: Summary.
        /// </summary>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <returns></returns>
        public static bool AreByteArraysEqual(this byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (var index = 0; index < array1.Length; index++)
            {
                if (array1[index] != array2[index])
                {
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        ///     TODO: Summary.
        /// </summary>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] XOR(this byte[] array1, byte[] array2)
        {
            if (array1.Length == array2.Length)
            {
                return XOR(array1, 0, array2, 0, array1.Length);
            }
            else
            {
                throw new ArgumentException("Arrays must be of equal length");
            }
        }
        /// <summary>
        ///     TODO: Summary.
        /// </summary>
        /// <param name="array1"></param>
        /// <param name="offset1"></param>
        /// <param name="array2"></param>
        /// <param name="offset2"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static byte[] XOR(this byte[] array1, int offset1, byte[] array2, int offset2, int length)
        {
            if (offset1 + length <= array1.Length && offset2 + length <= array2.Length)
            {
                var result = new byte[length];
                for (var index = 0; index < length; index++)
                {
                    result[index] = (byte)(array1[offset1 + index] ^ array2[offset2 + index]);
                }
                return result;
            }
            else
            {
                throw new ArgumentOutOfRangeException("A provided offset exceeds the size of an array.");
            }
        }
    }
}
