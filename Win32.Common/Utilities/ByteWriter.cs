using System;
using System.IO;
using System.Text;

namespace Win32.Common.Utilities
{
    /// <summary>
    ///     
    /// </summary>
    public class ByteWriter
    {
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteByte(byte[] buffer, int offset, byte value) => buffer[offset] = value;
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteByte(byte[] buffer, ref int offset, byte value)
        {
            buffer[offset] = value;
            offset += 1;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="bytes"></param>
        public static void WriteBytes(byte[] buffer, int offset, byte[] bytes) => WriteBytes(buffer, offset, bytes, bytes.Length);
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="bytes"></param>
        public static void WriteBytes(byte[] buffer, ref int offset, byte[] bytes)
        {
            WriteBytes(buffer, offset, bytes);
            offset += bytes.Length;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="bytes"></param>
        /// <param name="length"></param>
        public static void WriteBytes(byte[] buffer, int offset, byte[] bytes, int length) => Array.Copy(bytes, 0, buffer, offset, length);
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="bytes"></param>
        /// <param name="length"></param>
        public static void WriteBytes(byte[] buffer, ref int offset, byte[] bytes, int length)
        {
            Array.Copy(bytes, 0, buffer, offset, length);
            offset += length;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteAnsiString(byte[] buffer, int offset, string value) => WriteAnsiString(buffer, offset, value, value.Length);
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteAnsiString(byte[] buffer, ref int offset, string value) => WriteAnsiString(buffer, ref offset, value, value.Length);
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        /// <param name="maximumLength"></param>
        public static void WriteAnsiString(byte[] buffer, int offset, string value, int maximumLength)
        {
            var bytes = ASCIIEncoding.GetEncoding(28591).GetBytes(value);
            Array.Copy(bytes, 0, buffer, offset, Math.Min(value.Length, maximumLength));
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        /// <param name="fieldLength"></param>
        public static void WriteAnsiString(byte[] buffer, ref int offset, string value, int fieldLength)
        {
            WriteAnsiString(buffer, offset, value, fieldLength);
            offset += fieldLength;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteUTF16String(byte[] buffer, int offset, string value) => WriteUTF16String(buffer, offset, value, value.Length);
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteUTF16String(byte[] buffer, ref int offset, string value) => WriteUTF16String(buffer, ref offset, value, value.Length);
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        /// <param name="maximumNumberOfCharacters"></param>
        public static void WriteUTF16String(byte[] buffer, int offset, string value, int maximumNumberOfCharacters)
        {
            var bytes = UnicodeEncoding.Unicode.GetBytes(value);
            var maximumNumberOfBytes = Math.Min(value.Length, maximumNumberOfCharacters) * 2;
            Array.Copy(bytes, 0, buffer, offset, maximumNumberOfBytes);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        /// <param name="numberOfCharacters"></param>
        public static void WriteUTF16String(byte[] buffer, ref int offset, string value, int numberOfCharacters)
        {
            WriteUTF16String(buffer, offset, value, numberOfCharacters);
            offset += numberOfCharacters * 2;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteNullTerminatedAnsiString(byte[] buffer, int offset, string value)
        {
            WriteAnsiString(buffer, offset, value);
            WriteByte(buffer, offset + value.Length, 0x00);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteNullTerminatedAnsiString(byte[] buffer, ref int offset, string value)
        {
            WriteNullTerminatedAnsiString(buffer, offset, value);
            offset += value.Length + 1;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteNullTerminatedUTF16String(byte[] buffer, int offset, string value)
        {
            WriteUTF16String(buffer, offset, value);
            WriteBytes(buffer, offset + (value.Length * 2), new byte[] { 0x00, 0x00 });
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        public static void WriteNullTerminatedUTF16String(byte[] buffer, ref int offset, string value)
        {
            WriteNullTerminatedUTF16String(buffer, offset, value);
            offset += (value.Length * 2) + 2;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="bytes"></param>
        public static void WriteBytes(Stream stream, byte[] bytes) => stream.Write(bytes, 0, bytes.Length);
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="bytes"></param>
        /// <param name="count"></param>
        public static void WriteBytes(Stream stream, byte[] bytes, int count) => stream.Write(bytes, 0, count);
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="value"></param>
        public static void WriteAnsiString(Stream stream, string value) => WriteAnsiString(stream, value, value.Length);
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="value"></param>
        /// <param name="fieldLength"></param>
        public static void WriteAnsiString(Stream stream, string value, int fieldLength)
        {
            var bytes = ASCIIEncoding.GetEncoding(28591).GetBytes(value);
            stream.Write(bytes, 0, Math.Min(bytes.Length, fieldLength));
            if (bytes.Length < fieldLength)
            {
                var zeroFill = new byte[fieldLength - bytes.Length];
                stream.Write(zeroFill, 0, zeroFill.Length);
            }
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="value"></param>
        public static void WriteUTF8String(Stream stream, string value)
        {
            var bytes = UnicodeEncoding.UTF8.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="value"></param>
        public static void WriteUTF16String(Stream stream, string value)
        {
            var bytes = UnicodeEncoding.Unicode.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="value"></param>
        public static void WriteUTF16BEString(Stream stream, string value)
        {
            var bytes = UnicodeEncoding.BigEndianUnicode.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }
    }
}
