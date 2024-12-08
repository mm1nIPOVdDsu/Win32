using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

using Win32.Common.Attributes;
using Win32.Common.Extensions;

using Microsoft.Extensions.Logging;

namespace Win32.Common.Services.Logging
{
    /// <summary>
    ///     A logger that writes to the file system.
    /// </summary>
    [Singleton]
    public class FileLogger : ILogger, IDisposable
    {
        // check every 30 seconds to see if the log file has reached its max size.
        private const double checkLogSizeInterval = 30000;
        private const ushort callerMaxLength = 30;
        private const ushort dateMaxLength = 18;
        private const ushort fileMaxLength = 25;
        private const ushort lineMaxLength = 5;
        private const ushort logMaxLength = 11;
        // the maximum number of incremental log files that can be created. more for debug level.
        private const ushort MAX_INCREMENTS = 10;

        private readonly System.Timers.Timer _timer = new(checkLogSizeInterval);
        private readonly Func<FileLoggerConfiguration> _configuration;
        private readonly string stackTraceLineBuffer = new(' ', 5);

        private static StreamWriter? streamWriter;

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileLogger"/> class.
        /// </summary>
        /// <param name="configuration"></param>
        public FileLogger(Func<FileLoggerConfiguration> configuration)
        {
            if (configuration is null || configuration() is null)
                throw new ArgumentNullException(nameof(configuration));

            // NOTE: Not calling "configuration()" in case the configuration changes in FileLoggerProvider.
            _configuration = configuration;
            if (streamWriter is not null)
                return;

            if (string.IsNullOrEmpty(_configuration().Directory))
                _configuration().Directory = Environment.CurrentDirectory;
            if (string.IsNullOrEmpty(_configuration().FileName))
                _configuration().FileName = "General.log";

            _timer.Start();
            _timer.Elapsed += LogSizeTimer_Elapsed!;

            Init();
        }

        /// <summary>
        ///     Initializes the file logger. Adds a header, opens the stream, and removes old log files.
        /// </summary>
        private void Init()
        {
            // one log file per session
            IncrementFiles(_configuration().FullPath);
            SetupLogFile();
            WriteHeader();
            CleanUp();
        }

        /// <summary>
        ///     Begins a logical operation scope.
        /// </summary>
        /// <typeparam name="TState">The identifier for the scope.</typeparam>
        /// <param name="state">The type of the state to begin scope for.</param>
        /// <returns>An <see cref="IDisposable"/> that ends the logical operation scope on dispose.</returns>
#pragma warning disable CS8633 // Nullability in constraints for type parameter doesn't match the constraints for type parameter in implicitly implemented interface method'.
        public IDisposable BeginScope<TState>(TState state) where TState : notnull => default!;
#pragma warning restore CS8633 // Nullability in constraints for type parameter doesn't match the constraints for type parameter in implicitly implemented interface method'.
        /// <summary>
        ///     Checks if the given logLevel is enabled.
        /// </summary>
        /// <param name="logLevel">Level to be checked.</param>
        /// <returns>true if enabled.</returns>
        public bool IsEnabled(LogLevel logLevel) => logLevel >= _configuration().LogLevel;
        /// <summary>
        ///     Writes a log entry.
        /// </summary>
        /// <typeparam name="TState">The type of the object to be written.</typeparam>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">Id of the event.</param>
        /// <param name="state">The entry to be written. Can be also an object.</param>
        /// <param name="exception">The exception related to this entry.</param>
        /// <param name="formatter">Function to create a System.String message of the state and exception.</param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;
            // make sure we're not disposed.
            if (_disposed == true)
                return;

            var filePath = "UNKNOWN";
            var memberName = "UNKNOWN";
            var lineNumber = 0;

            try
            {
                lock (_writeLock)
                {
                    try
                    {
                        var stackTrace = exception is null ? new StackTrace(true) : new StackTrace(exception, true);
                        var relevantFrame = stackTrace.GetFrames().Where(x => x.HasSource() is true).LastOrDefault();
                        if (relevantFrame is not null)
                        {
#if DEBUG
                            try
                            {
                                Debug.WriteLine("");
                                Debug.WriteLine($"Frame File Name: {relevantFrame.GetFileName()}");
                                Debug.WriteLine($"Frame File Line Number: {relevantFrame.GetFileLineNumber()}");
                                Debug.WriteLine($"Frame File Column Number: {relevantFrame.GetFileColumnNumber()}");

                                var method = relevantFrame.GetMethod();
                                if (method is not null)
                                {
                                    Debug.WriteLine($"Frame Method Name: {method.Name}");
                                    Debug.WriteLine($"Frame Declaring Type Name: {method.DeclaringType?.Name}");
                                    Debug.WriteLine($"Assembly loaded modules: {string.Join(',', method.DeclaringType?.Assembly.GetLoadedModules().Where(x => x is not null).Select(x => x.Name)!)}");
                                }

                                Debug.WriteLine($"Frame Has Source: {relevantFrame.HasSource()}");
                                Debug.WriteLine("");
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("Error getting frame data.");
                                Debug.WriteLine(ex.Message);
                                Debug.WriteLine("");
                            }
#endif
                            filePath = relevantFrame.GetFileName();
                            memberName = relevantFrame.GetMethod()?.Name;
                            lineNumber = relevantFrame.GetFileLineNumber();
                        }

                        if (relevantFrame is not null)
                        {
                            filePath = relevantFrame.GetFileName();
                            memberName = relevantFrame.GetMethod()?.Name;
                            lineNumber = relevantFrame.GetFileLineNumber();
                        }
                    }
                    catch (Exception ex) { Debug.WriteLine(ex.Message); }

                    // this is a big assumption about class name. might be easier to get the stack?
                    var fileName = Path.GetFileNameWithoutExtension(filePath);
                    var message = formatter(state, exception);
                    var logEntry = FormatMessage(DateTime.Now.ToString("MM/dd HH:mm:ss.fff"), logLevel.ToString(), filePath!, memberName!, lineNumber.ToString(), message);

                    //var logEntry = $"{DateTime.UtcNow.ToString().PadLeft(dateColumnWidth)},{logLevel.ToString().PadLeft(levelColumnWidth)}," +
                    //               $"{fileName.PadLeft(classColumnWidth)},{memberName.PadLeft(methodColumnWidth)}," +
                    //               $"{lineNumber.ToString().PadLeft(lineColumnWidth)},{message}";

                    streamWriter?.WriteLine(logEntry);
                    streamWriter?.Flush();

                    if (logLevel is LogLevel.Error or LogLevel.Critical && exception is not null)
                    {
                        // write the exception message
                        if (message.Equals(exception.Message) is false)
                        {
                            // don't write the exception message if it was already written.
                            streamWriter?.WriteLine($"  {exception.Message}");
                        }
                        streamWriter?.WriteLine(exception.StackTrace);

                        // add a space to make reading the log file easier
                        streamWriter?.WriteLine("");
                        streamWriter?.Flush();
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
        }
        private readonly object _writeLock = new();

        #region Private Methods
        /// <summary>
        ///     Does initial setup on log file.
        /// </summary>
        private void SetupLogFile()
        {
            var logFile = new FileInfo(_configuration().FullPath);
            if (logFile is null)
                throw new NullReferenceException(nameof(logFile));

            if (logFile.Directory?.Exists == false)
                logFile.Directory.Create();
            if (logFile.Exists == false)
                logFile.Create().Close();

            if (logFile.Length > _configuration().MaxFileLength)
            {
                if (streamWriter is not null)
                {
                    streamWriter.Flush();
                    streamWriter.Dispose();
                }

                // increment all log files
                // NOTE: we don't want an exception to stop writing to log.
                try { IncrementFiles(_configuration().FullPath); }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Debug.WriteLine(ex.ToString());
                }

                // setup a new log file
                SetupLogFile();
                return;
            }

#if DEBUG
            // create the stream writer. not allowing other processes access to the file.
            // NOTE: keeping read/write for unit tests.
            streamWriter = new StreamWriter(logFile.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite));
#else
            // create the stream writer. not allowing other processes access to the file.
            streamWriter = new StreamWriter(logFile.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None));
#endif
            // alternate method with less granularity.
            //logFile.AppendText();
            if (logFile.Length > 0)
            {
                // add a space between log sessions only if there's no text yet.
                streamWriter.WriteLine("");
                streamWriter.Flush();
            }
        }
        /// <summary>
        ///     Writes a header to the log file.
        /// </summary>
        private void WriteHeader()
        {
            try
            {
                var loggerConfig = _configuration;
                if (new FileInfo(_configuration().FullPath).Length > 0)
                {
                    Debug.WriteLine("Lines already written to log file, not writing header.");
                    return;
                }

                // overkill? probably.
                var headerText = FormatMessage("Date", "Level", "File", "Method", "Line", "Message");
                var assembly = Assembly.GetEntryAssembly();
                if (assembly is null)
                    throw new NullReferenceException(nameof(assembly));

                var name = assembly.GetName();
                var appInfo = $"{name.Name} version {name.Version}, published {File.GetCreationTime(assembly.Location)}.";
                var systemInfo = $"OS Version: {Environment.OSVersion.Version}; Machine Name: {Environment.MachineName}.";
                var rowWidth = headerText.Length > systemInfo.Length ? headerText.Length : systemInfo.Length;

                if (streamWriter is null)
                    throw new Exception("Stream to log file is not open.");

                // header
                streamWriter.WriteLine($"**{new string('*', rowWidth)}**");
                streamWriter.WriteLine($"* {new string(' ', rowWidth)} *");
                streamWriter.WriteLine($"* {appInfo}{new string(' ', rowWidth - appInfo.Length)} *");
                streamWriter.WriteLine($"* {systemInfo}{new string(' ', rowWidth - systemInfo.Length)} *");
                streamWriter.WriteLine($"* {new string(' ', rowWidth)} *");
                streamWriter.WriteLine($"**{new string('*', rowWidth)}**");
                streamWriter.Flush();

                // column headings
                streamWriter.WriteLine(headerText);
                streamWriter.Flush();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }
        /// <summary>
        ///     Removes old log files from the machine
        /// </summary>
        private void CleanUp()
        {
            try
            {
                var retentionDate = DateTime.Now.Subtract(TimeSpan.FromDays(_configuration().RetentionDays));

                // get the file name and extension to use in the file search
                var fileName = Path.GetFileNameWithoutExtension(_configuration().FullPath);
                var fileExtension = Path.GetExtension(_configuration().FullPath);

                // get all log files older than the retention date, ignore if only one file found, sort by ascending
                var logFiles = new DirectoryInfo(_configuration().Directory).GetFiles($"*{fileExtension}", SearchOption.TopDirectoryOnly).OrderBy(x => x.CreationTime);
                var logFilesCount = logFiles.Count();
                if (logFilesCount < _configuration().RetentionCount) // TODO: Add this to the config
                    return;

                if (logFilesCount > _configuration().RetentionCount)
                {
                    var toDelete = logFilesCount - _configuration().RetentionCount;
                    foreach (var logFile in logFiles)
                    {
                        if (toDelete == 0)
                            return;

                        logFile.Delete();
                        toDelete--;
                    }
                }
                else
                {
                    // get log files greater than 'n' days old and delete
                    var oldLogFiles = logFiles.Where(x => x.CreationTime < retentionDate);
                    foreach (var oldLogFile in oldLogFiles)
                        oldLogFile.Delete();
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
        }
        /// <summary>
        ///     Increments the existing log files.
        /// </summary>
        /// <param name="fileFullName">The full name and path of the file that should be incremented.</param>
        /// <example>
        ///     if the file name passed in is Win32.Common-General.log, the returned value could be Win32.Common-General.log. if the name already exists,
        ///     the function will enumerate until a unique index is found.
        /// </example>
        /// <returns>A unique formatted file name.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="fileFullName"/> is null or empty.</exception>
        private void IncrementFiles(string fileFullName)
        {
            if (string.IsNullOrEmpty(fileFullName))
                throw new ArgumentNullException(nameof(fileFullName));
            if (Path.HasExtension(fileFullName) is false)
                throw new Exception("File must have an extension.");
            if (File.Exists(fileFullName) is false)
                return;

            var fileName = Path.GetFileNameWithoutExtension(fileFullName);
            var fileExtension = Path.GetExtension(fileFullName);
            var directory = Path.GetDirectoryName(fileFullName)!;

            var files = Directory.GetFiles(directory, $"{fileName}*.*").Select(x => new FileInfo(x)).OrderByDescending(x => x.LastWriteTime).ToList();
            var fileCount = files.Count;
            var countLength = fileCount.Length();

            for (var i = fileCount; i > 0; i--)
            {
                if (i >= MAX_INCREMENTS)
                {
                    files[i - 1].Delete();
                    continue;
                }

                // determine the length of incremental leading numbers to use.
                // ex: if there are 20 log files, this will rename the first 9
                //     to be 01, 02, 03, etc.. 
                //     if there are 100 log files, this will rename the first 99
                //     to be 001, 002...090, 091, etc.
                var fileNumber = new string('0', countLength - i.Length());

                var tempFileName = $"{fileName}_{fileNumber}{i}{fileExtension}";
                var tempFilePath = Path.Combine(directory, tempFileName);

                try { files[i - 1].MoveTo(tempFilePath); }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Debug.WriteLine(ex.ToString());
                }
            }
        }
        /// <summary>
        ///     Formats a log message to make it easier to read in the console window.
        /// </summary>
        /// <param name="messageTime">The <see cref="DateTime"/> that the log message was created.</param>
        /// <param name="logLevel">The <see cref="LogLevel"/> of the message.</param>
        /// <param name="sourceFilePath">The file that called the logger.</param>
        /// <param name="sourceName">The method that called the logger.</param>
        /// <param name="sourceLineNumber">The line where the call originated.</param>
        /// <param name="message">The message to write to the console.</param>
        private string FormatMessage(string messageTime, string logLevel, string sourceFilePath, string sourceName, string sourceLineNumber, string message)
        {
            sourceFilePath ??= "";

            var fileName = Path.GetFileNameWithoutExtension(sourceFilePath);

            var time = messageTime[..(dateMaxLength > messageTime.Length ? messageTime.Length : dateMaxLength)].PadRight(dateMaxLength);
            var levelString = logLevel[..(logMaxLength > logLevel.Length ? logLevel.Length : logMaxLength)].PadRight(logMaxLength);
            var fileString = fileName?[..(fileMaxLength > fileName.Length ? fileName.Length : fileMaxLength)].PadRight(fileMaxLength);
            var methodString = sourceName[..(callerMaxLength > sourceName.Length ? sourceName.Length : callerMaxLength)].PadRight(callerMaxLength);
            var lineNumberString = sourceLineNumber.PadRight(lineMaxLength);

            var line = $"{time} | {levelString} | {fileString} | {methodString} | {lineNumberString} | {message}";
            return line;
        }
        /// <summary>
        ///     Handles timer tick for checking log file size.
        /// </summary>
        /// <param name="sender"><see cref="object"/></param>
        /// <param name="e"><see cref="System.Timers.ElapsedEventArgs"/></param>
        private void LogSizeTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //var logFileName = Path.Combine(loggerConfig.Directory, $"{baseFileName}{loggerConfig.FileName}");
            var logFile = new FileInfo(_configuration().FullPath);

            // file length is less than the max, return
            if (logFile.Length < _configuration().MaxFileLength)
                return;

            lock (_writeLock)
            {
                Init();
            }
        }
        #endregion

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
        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (_disposed)
                    return;

                if (disposing)
                {
                    streamWriter?.Close();
                    streamWriter?.Dispose();
                    _timer.Dispose();
                }

                _disposed = true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        private bool _disposed = false;
    }
}
