﻿using System;
using System.IO;

namespace Win32.Common.Utilities
{
    /// <summary>
    ///     
    /// </summary>
    public class BigEndianReader
    {
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static short ReadInt16(byte[] buffer, ref int offset)
        {
            offset += 2;
            return BigEndianConverter.ToInt16(buffer, offset - 2);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static ushort ReadUInt16(byte[] buffer, ref int offset)
        {
            offset += 2;
            return BigEndianConverter.ToUInt16(buffer, offset - 2);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static uint ReadUInt24(byte[] buffer, int offset) => (uint)((buffer[offset + 0] << 16) | (buffer[offset + 1] << 8) | (buffer[offset + 2] << 0));
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static uint ReadUInt24(byte[] buffer, ref int offset)
        {
            offset += 3;
            return ReadUInt24(buffer, offset - 3);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static int ReadInt32(byte[] buffer, ref int offset)
        {
            offset += 4;
            return BigEndianConverter.ToInt32(buffer, offset - 4);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static uint ReadUInt32(byte[] buffer, ref int offset)
        {
            offset += 4;
            return BigEndianConverter.ToUInt32(buffer, offset - 4);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static long ReadInt64(byte[] buffer, ref int offset)
        {
            offset += 8;
            return BigEndianConverter.ToInt64(buffer, offset - 8);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static ulong ReadUInt64(byte[] buffer, ref int offset)
        {
            offset += 8;
            return BigEndianConverter.ToUInt64(buffer, offset - 8);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static Guid ReadGuid(byte[] buffer, ref int offset)
        {
            offset += 16;
            return BigEndianConverter.ToGuid(buffer, offset - 16);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static short ReadInt16(Stream stream)
        {
            var buffer = new byte[2];
            stream.Read(buffer, 0, 2);
            return BigEndianConverter.ToInt16(buffer, 0);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static ushort ReadUInt16(Stream stream)
        {
            var buffer = new byte[2];
            stream.Read(buffer, 0, 2);
            return BigEndianConverter.ToUInt16(buffer, 0);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static uint ReadUInt24(Stream stream)
        {
            var buffer = new byte[4];
            stream.Read(buffer, 1, 3);
            return BigEndianConverter.ToUInt32(buffer, 0);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static int ReadInt32(Stream stream)
        {
            var buffer = new byte[4];
            stream.Read(buffer, 0, 4);
            return BigEndianConverter.ToInt32(buffer, 0);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static uint ReadUInt32(Stream stream)
        {
            var buffer = new byte[4];
            stream.Read(buffer, 0, 4);
            return BigEndianConverter.ToUInt32(buffer, 0);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static long ReadInt64(Stream stream)
        {
            var buffer = new byte[8];
            stream.Read(buffer, 0, 8);
            return BigEndianConverter.ToInt64(buffer, 0);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static ulong ReadUInt64(Stream stream)
        {
            var buffer = new byte[8];
            stream.Read(buffer, 0, 8);
            return BigEndianConverter.ToUInt64(buffer, 0);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static Guid ReadGuid(Stream stream)
        {
            var buffer = new byte[16];
            stream.Read(buffer, 0, 16);
            return BigEndianConverter.ToGuid(buffer, 0);
        }
    }
}
