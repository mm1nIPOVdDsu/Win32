using System;
using System.IO;

namespace Win32.Common.Extensions
{
    /// <summary>
    ///     
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        ///     
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public static long CopyStream(this Stream input, Stream output) => CopyStream(input, output, Int64.MaxValue);
        /// <summary>
        ///     
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static long CopyStream(this Stream input, Stream output, long count)
        {
            const int MaxBufferSize = 1048576; // 1 MB
            var bufferSize = (int)Math.Min(MaxBufferSize, count);
            var buffer = new byte[bufferSize];
            var totalBytesRead = 0L;
            while (totalBytesRead < count)
            {
                var numberOfBytesToRead = (int)Math.Min(bufferSize, count - totalBytesRead);
                var bytesRead = input.Read(buffer, 0, numberOfBytesToRead);
                totalBytesRead += bytesRead;
                output.Write(buffer, 0, bytesRead);
                if (bytesRead == 0) // no more bytes to read from input stream
                {
                    return totalBytesRead;
                }
            }
            return totalBytesRead;
        }
    }
}
