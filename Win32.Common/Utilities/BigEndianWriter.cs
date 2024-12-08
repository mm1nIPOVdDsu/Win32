using System;
using System.IO;

namespace Win32.Common.Utilities
{
    /// <summary>
    ///     
    /// </summary>
    public class BigEndianWriter
    {
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteInt16(byte[] buffer, int offset, short value)
        {
            var bytes = BigEndianConverter.GetBytes(value);
            Array.Copy(bytes, 0, buffer, offset, bytes.Length);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteInt16(byte[] buffer, ref int offset, short value)
        {
            WriteInt16(buffer, offset, value);
            offset += 2;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteUInt16(byte[] buffer, int offset, ushort value)
        {
            var bytes = BigEndianConverter.GetBytes(value);
            Array.Copy(bytes, 0, buffer, offset, bytes.Length);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteUInt16(byte[] buffer, ref int offset, ushort value)
        {
            WriteUInt16(buffer, offset, value);
            offset += 2;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteUInt24(byte[] buffer, int offset, uint value)
        {
            var bytes = BigEndianConverter.GetBytes(value);
            Array.Copy(bytes, 1, buffer, offset, 3);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteUInt24(byte[] buffer, ref int offset, uint value)
        {
            WriteUInt24(buffer, offset, value);
            offset += 3;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteInt32(byte[] buffer, int offset, int value)
        {
            var bytes = BigEndianConverter.GetBytes(value);
            Array.Copy(bytes, 0, buffer, offset, bytes.Length);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteInt32(byte[] buffer, ref int offset, int value)
        {
            WriteInt32(buffer, offset, value);
            offset += 4;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteUInt32(byte[] buffer, int offset, uint value)
        {
            var bytes = BigEndianConverter.GetBytes(value);
            Array.Copy(bytes, 0, buffer, offset, bytes.Length);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteUInt32(byte[] buffer, ref int offset, uint value)
        {
            WriteUInt32(buffer, offset, value);
            offset += 4;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteInt64(byte[] buffer, int offset, long value)
        {
            var bytes = BigEndianConverter.GetBytes(value);
            Array.Copy(bytes, 0, buffer, offset, bytes.Length);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteInt64(byte[] buffer, ref int offset, long value)
        {
            WriteInt64(buffer, offset, value);
            offset += 8;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteUInt64(byte[] buffer, int offset, ulong value)
        {
            var bytes = BigEndianConverter.GetBytes(value);
            Array.Copy(bytes, 0, buffer, offset, bytes.Length);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteUInt64(byte[] buffer, ref int offset, ulong value)
        {
            WriteUInt64(buffer, offset, value);
            offset += 8;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteGuid(byte[] buffer, int offset, Guid value)
        {
            var bytes = BigEndianConverter.GetBytes(value);
            Array.Copy(bytes, 0, buffer, offset, bytes.Length);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteGuid(byte[] buffer, ref int offset, Guid value)
        {
            WriteGuid(buffer, offset, value);
            offset += 16;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="value"></param>
        public static void WriteInt16(Stream stream, short value)
        {
            var bytes = BigEndianConverter.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="value"></param>
        public static void WriteUInt16(Stream stream, ushort value)
        {
            var bytes = BigEndianConverter.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="value"></param>
        public static void WriteUInt24(Stream stream, uint value)
        {
            var bytes = BigEndianConverter.GetBytes(value);
            stream.Write(bytes, 1, 3);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="value"></param>
        public static void WriteInt32(Stream stream, int value)
        {
            var bytes = BigEndianConverter.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="value"></param>
        public static void WriteUInt32(Stream stream, uint value)
        {
            var bytes = BigEndianConverter.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="value"></param>
        public static void WriteInt64(Stream stream, long value)
        {
            var bytes = BigEndianConverter.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="value"></param>
        public static void WriteUInt64(Stream stream, ulong value)
        {
            var bytes = BigEndianConverter.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="value"></param>
        public static void WriteGuid(Stream stream, Guid value)
        {
            var bytes = BigEndianConverter.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
    }
}
