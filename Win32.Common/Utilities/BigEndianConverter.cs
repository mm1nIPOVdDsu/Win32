using System;

namespace Win32.Common.Utilities
{
    /// <summary>
    ///     
    /// </summary>
    public class BigEndianConverter
    {
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static ushort ToUInt16(byte[] buffer, int offset) => (ushort)((buffer[offset + 0] << 8) | (buffer[offset + 1] << 0));
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static short ToInt16(byte[] buffer, int offset) => (short)ToUInt16(buffer, offset);
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static uint ToUInt32(byte[] buffer, int offset) => (uint)((buffer[offset + 0] << 24) | (buffer[offset + 1] << 16) | (buffer[offset + 2] << 8) | (buffer[offset + 3] << 0));
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static int ToInt32(byte[] buffer, int offset) => (int)ToUInt32(buffer, offset);
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static ulong ToUInt64(byte[] buffer, int offset) => (((ulong)ToUInt32(buffer, offset + 0)) << 32) | ToUInt32(buffer, offset + 4);
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static long ToInt64(byte[] buffer, int offset) => (long)ToUInt64(buffer, offset);
        /// <summary>
        ///     
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static Guid ToGuid(byte[] buffer, int offset) => 
            new(ToUInt32(buffer, offset + 0),
                ToUInt16(buffer, offset + 4),
                ToUInt16(buffer, offset + 6),
                buffer[offset + 8],
                buffer[offset + 9],
                buffer[offset + 10],
                buffer[offset + 11],
                buffer[offset + 12],
                buffer[offset + 13],
                buffer[offset + 14],
                buffer[offset + 15]);
        /// <summary>
        ///     
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(ushort value)
        {
            var result = new byte[2];
            result[0] = (byte)((value >> 8) & 0xFF);
            result[1] = (byte)((value >> 0) & 0xFF);
            return result;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(short value) => GetBytes((ushort)value);
        /// <summary>
        ///     
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(uint value)
        {
            var result = new byte[4];
            result[0] = (byte)((value >> 24) & 0xFF);
            result[1] = (byte)((value >> 16) & 0xFF);
            result[2] = (byte)((value >> 8) & 0xFF);
            result[3] = (byte)((value >> 0) & 0xFF);

            return result;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(int value) => GetBytes((uint)value);
        /// <summary>
        ///     
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(ulong value)
        {
            var result = new byte[8];
            Array.Copy(GetBytes((uint)(value >> 32)), 0, result, 0, 4);
            Array.Copy(GetBytes((uint)(value & 0xFFFFFFFF)), 0, result, 4, 4);

            return result;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(long value) => GetBytes((ulong)value);
        /// <summary>
        ///     
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(Guid value)
        {
            var result = value.ToByteArray();
            if (BitConverter.IsLittleEndian)
            {
                // reverse first 4 bytes
                var temp = result[0];
                result[0] = result[3];
                result[3] = temp;

                temp = result[1];
                result[1] = result[2];
                result[2] = temp;

                // reverse next 2 bytes
                temp = result[4];
                result[4] = result[5];
                result[5] = temp;

                // reverse next 2 bytes
                temp = result[6];
                result[6] = result[7];
                result[7] = temp;
            }
            return result;
        }
    }
}
