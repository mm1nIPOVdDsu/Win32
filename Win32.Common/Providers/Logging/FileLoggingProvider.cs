using System;
using System.Collections.Concurrent;
using System.Linq;

using Win32.Common.Services.Logging;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Win32.Common.Providers.Logging
{
    /// <summary>
    ///     An implementation of <see cref="ILogger"/> that writes to the file system.
    /// </summary>
    [ProviderAlias("FileLogger")]
    public sealed class FileLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, FileLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);
        private readonly IDisposable? _onChangeToken;

        private FileLoggerConfiguration _currentConfig;

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileLoggerProvider"/> class.
        /// </summary>
        /// <param name="config"><see cref="IOptionsMonitor{FileLoggerConfiguration}"/></param>
        public FileLoggerProvider(IOptionsMonitor<FileLoggerConfiguration> config)
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName)
        {
            if (_loggers.Count > 0)
                return _loggers.First().Value;

            return _loggers.GetOrAdd(categoryName, name => new FileLogger(GetCurrentConfig));
        }

        /// <summary>
        ///     Gets the current configuration.
        /// </summary>
        /// <returns><see cref="FileLoggerConfiguration"/></returns>
        public FileLoggerConfiguration GetCurrentConfig() => _currentConfig;
        /// <summary>
        ///     Sets the log level for the application.
        /// </summary>
        /// <param name="level">The <see cref="LogLevel"/> to set.</param>
        public void SetLogLevel(LogLevel level)
        {
            if (level == _currentConfig.LogLevel)
                return;

            _currentConfig.LogLevel = level;
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting
        ///     unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting
        ///     unmanaged resources.
        /// </summary>
        /// <param name="disposing">When disposing managed objects.</param>
        internal void Dispose(bool disposing)
        {
            try
            {
                if (_disposed)
                    return;

                if (disposing)
                {
                    _loggers.Clear();
                    _onChangeToken?.Dispose();
                }

                _disposed = true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        bool _disposed = false;
    }
}
