using System;

using Microsoft.Extensions.Logging;

namespace Win32.Common.Services.Logging
{
    /// <summary>
    ///     An implementation of <see cref="ILogger"/> that writes to the file system.
    /// </summary>
    public interface IFileLogger : ILogger, IDisposable { }
}
