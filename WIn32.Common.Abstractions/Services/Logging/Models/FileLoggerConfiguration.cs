using System;
using System.IO;
using System.Reflection;

using Microsoft.Extensions.Logging;

namespace Win32.Common.Services.Logging
{
    /// <summary>
    ///     Configuration settings for a file logger.
    /// </summary>
    public interface IFileLoggerConfiguration
    {
        /// <summary>
        ///     The directory of the log file.
        /// </summary>
        string Directory { get; }
        /// <summary>
        ///     The name of the log file.
        /// </summary>
        string FileName { get; }
        /// <summary>
        ///     The full path to the log file.
        /// </summary>
        string FullPath { get; }
        /// <summary>
        ///     The maximum log level that can be written to a log.
        /// </summary>
        LogLevel LogLevel { get; }
        /// <summary>
        ///     The maximum size, in bytes, of a log file before creating a new file.
        /// </summary>
        uint MaxFileLength { get; }
        /// <summary>
        ///     The number of to retain a log file.
        /// </summary>
        ushort RetentionCount { get; }
        /// <summary>
        ///     The number of log files to keep if multiple are generated per day.
        /// </summary>
        uint RetentionDays { get; }
    }

    /// <summary>
    ///     Configuration settings for a file logger.
    /// </summary>
    public class FileLoggerConfiguration : IFileLoggerConfiguration
    {
        //private const string FileNameDateFormat = "yy-MM-dd_HHmm";
        // max file length of 10MB
        private const uint LOG_FILE_MAX_LENGTH_MIN = 10000000;
        private const string LOG_FILE_EXTENSION = ".log";
        private const string LOGS_SUB_DIRECTORY = "Logs";
        /// <summary>
        ///     Initializes a new instance of the <see cref="FileLoggerConfiguration"/> class.
        /// </summary>
        public FileLoggerConfiguration() { }

        /// <summary>
        ///     The directory of the log file.
        /// </summary>
        public string Directory
        {
            get => _directory;
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                if (!string.IsNullOrEmpty(_directory))
                    throw new ArgumentException("Cannot change path once it is set.");

                _directory = value;
                // NOTE: put logs in the Logs sub-directory to make it more common for all applications
                // don't want to double append the logs directory
                if (_directory.EndsWith(LOGS_SUB_DIRECTORY, StringComparison.InvariantCultureIgnoreCase) is false)
                    _directory += $"\\{LOGS_SUB_DIRECTORY}";
            }
        }
        private string _directory = string.Empty;
        /// <summary>
        ///     The name of the log file.
        /// </summary>
        public string FileName
        {
            get => _filename;
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                if (!string.IsNullOrEmpty(_filename))
                    throw new ArgumentException("Cannot change log file name once it is set.");

                // make sure we have a common extension for log files
                _filename = value;
                var extension = Path.GetExtension(_filename);
                // this accounts for uppercase as well so if the passed in file name
                // is all upper case, it's not equal and the file extension gets set
                // to the LOG_FILE_EXTENSION value.
                if (string.IsNullOrEmpty(extension) || extension != LOG_FILE_EXTENSION)
                {
                    // if the file name doesn't have an extension, trying to do a replace won't help
                    _filename = _filename.Replace(extension, "");
                    _filename = $"{_filename}{LOG_FILE_EXTENSION}";
                }

                // NOTE: for unit tests, this will return "testhost"
                var baseFileName = "";
                try { baseFileName = $"{Assembly.GetEntryAssembly()?.FullName?.Split(',')[0]}-"; }
                catch (Exception) { /* Eating this exception */ }

                if (string.IsNullOrEmpty(baseFileName) is false && _filename.Contains(baseFileName) is false)
                    _filename = $"{baseFileName}{_filename}";
            }
        }
        private string _filename = string.Empty;
        /// <summary>
        ///     The full path to the log file.
        /// </summary>
        public string FullPath => Path.Combine(Directory, FileName);
        /// <summary>
        ///     The maximum log level that can be written to a log.
        /// </summary>
        public LogLevel LogLevel { get; set; }
        /// <summary>
        ///     The maximum size, in bytes, of a log file before creating a new file.
        /// </summary>
        public uint MaxFileLength
        {
            get => _maxFileLength;
            set => _maxFileLength = value < LOG_FILE_MAX_LENGTH_MIN ? LOG_FILE_MAX_LENGTH_MIN : value;
        }
        private uint _maxFileLength = LOG_FILE_MAX_LENGTH_MIN;
        /// <summary>
        ///     The number of log files to keep if multiple are generated per day.
        /// </summary>
        public ushort RetentionCount { get; set; }
        /// <summary>
        ///     The number of to retain a log file.
        /// </summary>
        public uint RetentionDays { get; set; }
    }
}
