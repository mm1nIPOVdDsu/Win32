using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Security;
using System.Threading;

using Win32.Common.Services.User;

using Microsoft.Extensions.Logging;

namespace Win32.Common.Services.EventLog
{
    /// <summary>
    ///     Handles writing messages to the Windows event log.
    /// </summary>
    [SupportedOSPlatform("Windows")]
    public class EventLogService : IEventLogService
    {
        private readonly CancellationTokenSource _cts = new();
        private readonly ILogger<EventLogService> _logger;
        private readonly IUserService _userService;
        private readonly CancellationToken _ct;
        private readonly object _lock = new();

        private System.Diagnostics.EventLog? eventLogInstance;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EventLogService"/> class.
        /// </summary>
        /// <param name="logger">An instance of <see cref="ILogger{EventLogService}"/>.</param>
        /// <param name="userService">An instance of <see cref="IUserService"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="logger"/> is null.</exception>
        public EventLogService(ILogger<EventLogService> logger, IUserService userService)
        {
            _userService = userService is not null ? userService : throw new ArgumentNullException(nameof(userService));
            _logger = logger is not null ? logger : throw new ArgumentNullException(nameof(logger));
            _ct = _cts.Token;
        }

        /// <summary>
        ///     The event log name of the open event log.
        /// </summary>
        public string? EventLogName { get; private set; }
        /// <summary>
        ///     The name of the source of the open event log.
        /// </summary>
        public string? EventLogSource { get; private set; }

        /// <summary>
        ///     Closes the currently open event log source.
        /// </summary>
        /// <returns>True if successfully closed.</returns>
        public bool Close()
        {
            lock (_lock)
            {
                try
                {
                    if (eventLogInstance is null)
                    {
                        _logger.LogInformation("An event log source has not been created or opened so one cannot be closed.");
                        return false;
                    }

                    _logger.LogInformation("Closing event log.");
                    eventLogInstance.Dispose();
                }
                finally
                {
                    eventLogInstance = null;
                    EventLogSource = "";
                    EventLogName = "";
                }

                _logger.LogInformation("Event log successfully closed.");
                return true;
            }
        }
        /// <summary>
        ///     Deletes an event log source.
        /// </summary>
        /// <remarks>If deleting the currently opened event log source, it will be closed.</remarks>
        /// <param name="eventLogSource">The name of the event log source to delete.</param>
        /// <returns>True if successful.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="eventLogSource"/> is null or empty.</exception>
        /// <exception cref="SecurityException">Thrown if the application does not have permission to delete an event log source.</exception>
        public bool Delete(string eventLogSource)
        {
            if (string.IsNullOrEmpty(eventLogSource))
                throw new ArgumentNullException(nameof(eventLogSource));
            if (_userService.IsAdministrator() is false)
                throw new SecurityException("Application requires administrative rights to manage the event log.");

            lock (_lock)
            {
                _logger.LogDebug("Checking if the event log source '{source}' is already open.", eventLogSource);
                if (EventLogSource?.Equals(eventLogSource, StringComparison.InvariantCultureIgnoreCase) is true && eventLogInstance is not null)
                {
                    _logger.LogInformation("Event log source {sourceName} is currently open. Closing event log source prior to deleting.", eventLogSource);
                    eventLogInstance.Dispose();
                    eventLogInstance = null;
                    EventLogSource = "";
                    EventLogName = "";
                }

                _logger.LogInformation("Deleting {source}.", eventLogSource);
                System.Diagnostics.EventLog.DeleteEventSource(eventLogSource);
                return true;
            }
        }
        /// <summary>
        ///     Determines whether or not an event log source exists.
        /// </summary>
        /// <param name="eventLogSource">The name of the event log source to check.</param>
        /// <returns>True if the source exists.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="eventLogSource"/> is null or empty.</exception>
        /// <exception cref="SecurityException">Thrown if the application is not running with administrative rights.</exception>
        public bool Exists(string eventLogSource)
        {
            if (string.IsNullOrEmpty(eventLogSource))
                throw new ArgumentNullException(nameof(eventLogSource));
            if (_userService.IsAdministrator() is false)
                throw new SecurityException("Application requires administrative rights to manage the event log.");

            _logger.LogInformation("Checking if {source} exists.", eventLogSource);
            return System.Diagnostics.EventLog.SourceExists(eventLogSource);
        }
        /// <summary>
        ///     Gets event log message entries from the open event log source.
        /// </summary>
        /// <param name="count">The number of message entries to return.</param>
        /// <param name="startingIndex">The message index to start at.</param>
        /// <returns>Returns an <see cref="IEnumerable{EventLogMessageEntry}"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="count"/> is less than 0.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="startingIndex"/> is less than 0.</exception>
        /// <exception cref="InvalidOperationException">Thrown if <see cref="Open"/> has not been called.</exception>
        public IEnumerable<EventLogMessageEntry> Get(int count, int startingIndex = 0)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (startingIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startingIndex));

            lock (_lock)
            {
                if (eventLogInstance is null)
                    throw new InvalidOperationException("An event log source has not been created or opened.");

                _logger.LogInformation("Getting previous message from event log source {eventLogSource} in the event log {eventLogName}.", eventLogInstance.Source, eventLogInstance.LogDisplayName);
                if (eventLogInstance.Entries.Count < count)
                    count = eventLogInstance.Entries.Count;
                // TODO: this math needs to be tested
                if (count + startingIndex > eventLogInstance.Entries.Count)
                    count = eventLogInstance.Entries.Count - startingIndex;

                _logger.LogDebug("Copying previous message from event log source {eventLogSource} in the event log {eventLogName}.", eventLogInstance.Source, eventLogInstance.LogDisplayName);
                var eventLogEntries = new System.Diagnostics.EventLogEntry[count];
                eventLogInstance.Entries.CopyTo(eventLogEntries, startingIndex);

                _logger.LogDebug("Iterating {messageCount} previous messages.", eventLogEntries.Length);
                foreach (var entry in eventLogEntries)
                {
                    if (_ct.IsCancellationRequested)
                    {
                        _logger.LogWarning("Cancellation was requested, exiting.");
                        break;
                    }
                    yield return new EventLogMessageEntry(eventLogInstance.Source, eventLogInstance.Log)
                    {
                        EventId = entry.InstanceId,
                        Message = entry.Message,
                        MessageType = (EventLogMessageType)entry.EntryType
                    };
                }
            }
        }
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
        public bool Open(string eventLogName, string eventLogSource)
        {
            if (string.IsNullOrEmpty(eventLogName))
                throw new ArgumentNullException(nameof(eventLogName));
            if (string.IsNullOrEmpty(eventLogSource))
                throw new ArgumentNullException(nameof(eventLogSource));
            if (_userService.IsAdministrator() is false)
                throw new SecurityException("Application requires administrative rights to manage the event log.");

            lock (_lock)
            {
                if (eventLogInstance is not null)
                    throw new InvalidOperationException($"The event log source {eventLogSource} has already been opened.");

                _logger.LogInformation("Attempting to create the event log source {eventLogSource} in the event log {eventLogName}.", eventLogSource, eventLogName);

                // if you're getting an exception here, close visual studio and open it as an admin
                _logger.LogDebug("Checking if the {eventLogSource} already exists.", eventLogSource);
                if (System.Diagnostics.EventLog.SourceExists(eventLogSource) is false)
                {
                    _logger.LogDebug("Creating the event log source {eventLogSource} in {eventLogName}.", eventLogSource, eventLogName);
                    System.Diagnostics.EventLog.CreateEventSource(eventLogSource, eventLogName);
                    // wait for the event log source to be created.
                    System.Threading.Thread.Sleep(1000);
                }
                // using the machine name of "." just means "this machine"
                _logger.LogDebug("Opening the event log source {eventLogSource} in {eventLogName}.", eventLogSource, eventLogName);
                eventLogInstance = new System.Diagnostics.EventLog(eventLogName, ".", eventLogSource);

                _logger.LogInformation("Successfully created the event log source {eventLogSource} in {eventLogName}.", eventLogSource, eventLogName);
                EventLogSource = eventLogSource;
                EventLogName = eventLogName;
                return true;
            }
        }
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
        public bool Write(string message, EventLogMessageType type = EventLogMessageType.Information, int eventId = 0)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message));

            lock (_lock)
            {
                if (eventLogInstance is null)
                    throw new InvalidOperationException("Must create or open the event log prior to writing messages to it.");

                _logger.LogInformation("Writing message to windows event log source {eventLogSource}.", eventLogInstance.Source);
                eventLogInstance.WriteEntry(message, (System.Diagnostics.EventLogEntryType)type, eventId);
            }

            return true;
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
        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (_disposed)
                    return;

                if (disposing)
                {
                    _cts.Cancel();
                    Close();
                    _cts.Dispose();
                }

                _disposed = true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        bool _disposed = false;
    }
}
