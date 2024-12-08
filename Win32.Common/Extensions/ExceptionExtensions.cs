using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Win32.Common.Extensions
{
    /// <summary>
    ///     Extensions for <see cref="Exception"/>.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        ///     Gets the name of the file that raised from an exception.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/></param>
        /// <returns>The name of the file.</returns>
        public static string? ExceptionFileName(this Exception exception)
        {
            var rootFrame = exception.RootFrame();
            return rootFrame is null ? "" : Path.GetFileName(rootFrame.GetFileName());
        }
        /// <summary>
        ///     Gets the name of the path of the file that raised from an exception.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/></param>
        /// <returns>The path of the file.</returns>
        public static string? ExceptionFilePath(this Exception exception)
        {
            var rootFrame = exception.RootFrame();
            return rootFrame is null ? "" : rootFrame.GetFileName();
        }
        /// <summary>
        ///     Gets the file line number where the exception was raised.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/></param>
        /// <returns>The line number of the file.</returns>
        public static int ExceptionLineNumber(this Exception exception)
        {
            var rootFrame = exception.RootFrame();
            return rootFrame is null ? 0 : rootFrame.GetFileLineNumber();
        }
        /// <summary>
        ///     Gets the name of the method that raised the exception.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/></param>
        /// <returns>The method name.</returns>
        public static string? ExceptionMethodName(this Exception exception)
        {
            var rootFrame = exception.RootFrame();
            return rootFrame is null ? "" : rootFrame.GetMethod()?.Name;
        }
        /// <summary>
        ///     Formats an exception message to log format.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> to format.</param>
        /// <param name="withHeader">True will include a header with the exception message.</param>
        /// <returns>The formatted string.</returns>
        public static string? Format(this Exception exception, bool withHeader = false)
        {
            try
            {
                var exceptionList = new List<Exception>();
                var innerException = exception;
                while (innerException is not null)
                {
                    exceptionList.Add(innerException);
                    innerException = innerException.InnerException;
                }

                var lastException = exceptionList.Last();
                var lastStackFrames = new StackTrace(lastException, true)?.GetFrames()?.ToList();
                if (lastStackFrames is null)
                {
                    return "";
                }

                var headerErrorType = lastException.GetType().Name;
                var headerErrorMessage = lastException.Message;
                var headerLastFrame = lastStackFrames.Where(x => string.IsNullOrEmpty(x.GetFileName()) != true).Last();

                var message = string.Empty;
                if (withHeader)
                {
                    message = $"{headerErrorType} in {Path.GetFileName(headerLastFrame.GetFileName())}: {headerErrorMessage}{Environment.NewLine}";
                }

                for (var ed = 0; ed < exceptionList.Count; ed++)
                {
                    var exceptionDetail = exceptionList[ed];
                    var stackFrames = new StackTrace(lastException, true)?.GetFrames()?.ToList();
                    if (stackFrames is null || stackFrames.Count is 0)
                    {
                        continue;
                    }

                    var lastFrame = stackFrames.Where(x => string.IsNullOrEmpty(Path.GetFileName(x.GetFileName())) != true).First();
                    var methodName = lastFrame.GetMethod();
                    var lineNumber = lastFrame.GetFileLineNumber();
                    var fileName = Path.GetFileName(lastFrame.GetFileName());

                    var frameMessage = $"at file {fileName} method {methodName} line {lineNumber}";
                    message += $"{frameMessage.PadLeft(ed + 1 + frameMessage.Length)}.{Environment.NewLine}";
                }

                return message;
            }
            catch { return exception.Message; } // probably not the best idea.
        }

        /// <summary>
        ///     Gets the exceptions first stack frame.
        /// </summary>
        /// <param name="exception"><see cref="Exception"/></param>
        /// <returns>The first <see cref="StackFrame"/>.</returns>
        private static StackFrame? RootFrame(this Exception exception)
        {
            var stackTrace = new StackTrace(exception, true);
            return stackTrace?.GetFrame(0);
        }
        /// <summary>
        ///     Creates a list from the exception and all inner exceptions.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> to create a list from.</param>
        /// <returns><see cref="List{T}"/></returns>
        private static List<Exception> ToList(this Exception exception)
        {
            var exceptionList = new List<Exception>();
            var innerException = exception;
            while (innerException is not null)
            {
                exceptionList.Add(innerException);
                innerException = innerException.InnerException;
            }

            return exceptionList;
        }
        /// <summary>
        ///     Creates a list of the exception's stack frames that have required property values.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> to the stack frames for.</param>
        /// <returns><see cref="List{T}"/></returns>
        private static List<StackFrame>? GetStackFrames(this Exception exception)
        {
            var frames = new StackTrace(exception, true)?.GetFrames()?.ToList();
            if (frames == null)
            {
                return null;
            }

            // only get the frames that have file names...GAC assemblies (I think) won't return this info.
            var filteredFrames = frames.Where(x => string.IsNullOrEmpty(x.GetFileName()) != true).ToList();
            return filteredFrames;
        }
    }
}
