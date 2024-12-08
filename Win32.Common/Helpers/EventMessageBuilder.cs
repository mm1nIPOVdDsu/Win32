using System;
using System.Text;

using Win32.Common.Services.Event;

using Microsoft.Extensions.Logging;

namespace Win32.Common.Helpers
{
    /// <summary>
    ///     TODO: Summary
    /// </summary>
    public static class EventMessageBuilder
    {
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static LogMessage LogMessage(LogLevel logLevel, string message)
        {
            return new LogMessage
            {
                Message = message,
                Category = logLevel.ToString(),
                StackTrace = Environment.StackTrace
            };
        }
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static LogMessage LogMessage(LogLevel logLevel, string message, Exception exception)
        {
            return new LogMessage
            {
                Message = message,
                Category = logLevel.ToString(),
                StackTrace = exception.StackTrace,
                ExceptionType = exception.GetType().FullName
            };
        }
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="target"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static LogMessage LogMessage(string methodName, string target, Exception exception)
        {
            var sb = new StringBuilder();
            var ex = exception;
            while (ex != null)
            {
                sb.Append(ex.Message);
                ex = ex.InnerException;
                if (ex != null)
                {
                    sb.Append(" --> ");
                }
            }

            var message = $"method {methodName} of target {target} threw exception {sb}";
            return new LogMessage
            {
                Message = message,
                Category = LogLevel.Error.ToString(),
                StackTrace = exception.StackTrace,
                ExceptionType = exception.GetType().FullName
            };
        }

        /// <summary>
        ///     TODO: Summary
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static EventMessage<LogMessage> BuildLogMessage(LogLevel logLevel, string message) => new(LogMessage(logLevel, message));
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static EventMessage<LogMessage> BuildLogMessage(LogLevel logLevel, string message, Exception exception) => new(LogMessage(logLevel, message, exception));
        /// <summary>
        ///     TODO: Summary
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="target"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static EventMessage<LogMessage> BuildLogMessage(string methodName, string target, Exception exception) => new(LogMessage(methodName, target, exception));
    }

    /// <summary>
    ///     Defines a log message used in an <see cref="EventMessage"/>
    /// </summary>
    public class LogMessage : ILogMessage
    {
        /// <summary>
        ///     A message to write to a log handler.
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        ///     The category of the log entry.
        /// </summary>
        public string? Category { get; set; }
        /// <summary>
        ///     A stack trace if it exists.
        /// </summary>
        public string? StackTrace { get; set; }
        /// <summary>
        ///     The type of exception.
        /// </summary>
        public string? ExceptionType { get; set; }
    }

    /// <summary>
    ///     Defines a log message used in an <see cref="EventMessage"/>
    /// </summary>
    public interface ILogMessage
    {
        /// <summary>
        ///     A message to write to a log handler.
        /// </summary>
        string? Message { get; set; }
        /// <summary>
        ///     The category of the log entry.
        /// </summary>
        string? Category { get; set; }
        /// <summary>
        ///     A stack trace if it exists.
        /// </summary>
        string? StackTrace { get; set; }
        /// <summary>
        ///     The type of exception.
        /// </summary>
        string? ExceptionType { get; set; }
    }
}
