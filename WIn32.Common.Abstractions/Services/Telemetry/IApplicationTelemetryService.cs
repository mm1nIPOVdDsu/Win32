using System;

namespace Win32.Common.Services.Telemetry
{
    /// <summary>
    ///     Provides telemetry reporting to Application Insights for an application.
    /// </summary>
    public interface IApplicationTelemetryService : IDisposable
    {
        /// <summary>
        ///     Initializes the session with application insights.
        /// </summary>
        /// <param name="connectionString">The connection string for the application insights endpoint.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="connectionString"/> is null or empty.</exception>
        void Initialize(string connectionString);
        /// <summary>
        ///     Sends a message to the trace endpoint of application insights.
        /// </summary>
        /// <param name="msg">The message to send.</param>
        /// <exception cref="NullReferenceException">Thrown if initialization has not completed successfully.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="msg"/> is null or empty.</exception>
        void Track(string msg);
        /// <summary>
        ///     Sends an exception to the exception endpoint of application insights.
        /// </summary>
        /// <param name="msg">The message to send with the exception.</param>
        /// <param name="ex">The exception to send.</param>
        /// <param name="unhandledException">True if the exception is unhandled. Default is false.</param>
        /// <exception cref="NullReferenceException">Thrown if initialization has not completed successfully.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="msg"/> is null or empty.</exception>
        void TrackException(string msg, Exception ex, bool unhandledException = false);
    }
}
