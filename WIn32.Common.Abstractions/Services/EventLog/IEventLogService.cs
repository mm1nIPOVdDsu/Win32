using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Security;

namespace Win32.Common.Services.EventLog
{
    /// <summary>
    ///     Handles writing messages to the Windows event log.
    /// </summary>
    [SupportedOSPlatform("Windows")]
    public interface IEventLogService : IDisposable
    {
        /// <summary>
        ///     The event log name of the open event log.
        /// </summary>
        string? EventLogName { get; }
        /// <summary>
        ///     The name of the source of the open event log.
        /// </summary>
        string? EventLogSource { get; }

        /// <summary>
        ///     Closes the currently open event log source.
        /// </summary>
        /// <returns>True if successfully closed.</returns>
        bool Close();
        /// <summary>
        ///     Deletes an event log source.
        /// </summary>
        /// <remarks>If deleting the currently opened event log source, it will be closed.</remarks>
        /// <param name="eventLogSource">The name of the event log source to delete.</param>
        /// <returns>True if successful.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="eventLogSource"/> is null or empty.</exception>
        /// <exception cref="SecurityException">Thrown if the application does not have permission to delete an event log source.</exception>
        bool Delete(string eventLogSource);
        /// <summary>
        ///     Determines whether or not an event log source exists.
        /// </summary>
        /// <param name="eventLogSource">The name of the event log source to check.</param>
        /// <returns>True if the source exists.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="eventLogSource"/> is null or empty.</exception>
        /// <exception cref="SecurityException">Thrown if the application is not running with administrative rights.</exception>
        bool Exists(string eventLogSource);
        /// <summary>
        ///     Gets event log message entries from the open event log source.
        /// </summary>
        /// <param name="count">The number of message entries to return.</param>
        /// <param name="startingIndex">The message index to start at.</param>
        /// <returns>Returns an <see cref="IEnumerable{EventLogMessageEntry}"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="count"/> is less than 0.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="startingIndex"/> is less than 0.</exception>
        /// <exception cref="InvalidOperationException">Thrown if <see cref="Open"/> has not been called.</exception>
        IEnumerable<EventLogMessageEntry> Get(int count, int startingIndex = 0);
        /// <summary>
        ///     Opens and/or creates an event log source in the Windows Event Log.
        /// </summary>
        /// <param name="eventLogName">The event log name such as Application, Security, or System. This will almost always be Application.</param>
        /// <remarks>If a new event log is getting created, it will take time before it is available to use.</remarks>
        /// <param name="eventLogSource">The name of the source that will write entries. This is typically the name of the application.</param>
        /// <returns>True if successful.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="eventLogName"/> is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="eventLogSource"/> is null or empty.</exception>
        /// <exception cref="SecurityException">Thrown if the application does not have permission to open an event log.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the event log source has already been initialized.</exception>
        bool Open(string eventLogName, string eventLogSource);
        /// <summary>
        ///     Writes a message to the windows event log.
        /// </summary>
        /// <remarks>Call <see cref="Open"/> before attempting to write to the event log.</remarks>
        /// <param name="message">The message to write to the event log.</param>
        /// <param name="type">[Optional] The <see cref="EventLogMessageType"/> of the message. Default is <see cref="EventLogMessageType.Information"/>.</param>
        /// <param name="eventId">[Optional] The id of the message. Default is 0.</param>
        /// <returns>True if the message was written.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="message"/> is null or empty.</exception>
        /// <exception cref="InvalidOperationException">
        ///     Thrown if an event log source has not been created or opened. Call <see cref="Open"/> before attempting to write to the
        ///     event log.
        /// </exception>
        bool Write(string message, EventLogMessageType type = EventLogMessageType.Information, int eventId = 0);
    }
}
