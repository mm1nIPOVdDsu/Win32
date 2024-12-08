using System;
using System.IO;
using System.Text;

namespace Win32.Common.Utilities
{
    /// <summary>
    ///     
    /// </summary>
    public class ByteReader
    {
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static byte ReadByte(byte[] buffer, int offset) => buffer[offset];
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static byte ReadByte(byte[] buffer, ref int offset)
        {
            offset++;
            return buffer[offset - 1];
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] ReadBytes(byte[] buffer, int offset, int length)
        {
            var result = new byte[length];
            Array.Copy(buffer, offset, result, 0, length);
            return result;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] ReadBytes(byte[] buffer, ref int offset, int length)
        {
            offset += length;
            return ReadBytes(buffer, offset - length, length);
        }
        /// <summary>
        ///     Will return the ANSI string stored in the buffer.
        /// </summary>
        /// <remarks>ASCIIEncoding.ASCII.GetString will convert some values to '?' (byte value of 63). Any codepage will do, but the only one that Mono supports is 28591.</remarks>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string ReadAnsiString(byte[] buffer, int offset, int count) => ASCIIEncoding.GetEncoding(28591).GetString(buffer, offset, count);
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string ReadAnsiString(byte[] buffer, ref int offset, int count)
        {
            offset += count;
            return ReadAnsiString(buffer, offset - count, count);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="numberOfCharacters"></param>
        /// <returns></returns>
        public static string ReadUTF16String(byte[] buffer, int offset, int numberOfCharacters)
        {
            var numberOfBytes = numberOfCharacters * 2;
            return Encoding.Unicode.GetString(buffer, offset, numberOfBytes);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="numberOfCharacters"></param>
        /// <returns></returns>
        public static string ReadUTF16String(byte[] buffer, ref int offset, int numberOfCharacters)
        {
            var numberOfBytes = numberOfCharacters * 2;
            offset += numberOfBytes;
            return ReadUTF16String(buffer, offset - numberOfBytes, numberOfCharacters);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static string ReadNullTerminatedAnsiString(byte[] buffer, int offset)
        {
            var builder = new StringBuilder();
            var c = (char)ByteReader.ReadByte(buffer, offset);
            while (c != '\0')
            {
                builder.Append(c);
                offset++;
                c = (char)ByteReader.ReadByte(buffer, offset);
            }
            return builder.ToString();
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static string ReadNullTerminatedUTF16String(byte[] buffer, int offset)
        {
            var builder = new StringBuilder();
            var c = (char)LittleEndianConverter.ToUInt16(buffer, offset);
            while (c != 0)
            {
                builder.Append(c);
                offset += 2;
                c = (char)LittleEndianConverter.ToUInt16(buffer, offset);
            }
            return builder.ToString();
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static string ReadNullTerminatedAnsiString(byte[] buffer, ref int offset)
        {
            var result = ReadNullTerminatedAnsiString(buffer, offset);
            offset += result.Length + 1;
            return result;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static string ReadNullTerminatedUTF16String(byte[] buffer, ref int offset)
        {
            var result = ReadNullTerminatedUTF16String(buffer, offset);
            offset += (result.Length * 2) + 2;
            return result;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static byte[] ReadBytes(Stream stream, int count)
        {
            var temp = new MemoryStream();
            Extensions.StreamExtensions.CopyStream(stream, temp, count);
            return temp.ToArray();
        }
        /// <summary>
        ///     Return all bytes from current stream position to the end of the stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ReadAllBytes(Stream stream)
        {
            var temp = new MemoryStream();
            Extensions.StreamExtensions.CopyStream(stream, temp);
            return temp.ToArray();
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ReadAnsiString(Stream stream, int length)
        {
            var buffer = ReadBytes(stream, length);
            return ASCIIEncoding.GetEncoding(28591).GetString(buffer);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string ReadNullTerminatedAnsiString(Stream stream)
        {
            var builder = new StringBuilder();
            var c = (char)stream.ReadByte();
            while (c != '\0')
            {
                builder.Append(c);
                c = (char)stream.ReadByte();
            }
            return builder.ToString();
        }
    }
}
