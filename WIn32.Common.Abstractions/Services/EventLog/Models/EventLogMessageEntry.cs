using System;

namespace Win32.Common.Services.EventLog
{
    /// <summary>
    ///     Represents an event log message.
    /// </summary>
    public class EventLogMessageEntry
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EventLogMessageEntry"/> class.
        /// </summary>
        /// <param name="source">The name of the source that will write entries. This is typically the name of the application.</param>
        /// <param name="name">The event log name such as Application, Security, or System. This will almost always be Application.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is null or empty.</exception>
        public EventLogMessageEntry(string source, string name)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            Source = source;
            Name = name;
        }

        /// <summary>
        ///     The name of the source that will write entries. This is typically the name of the application.
        /// </summary>
        public string Source { get; }
        /// <summary>
        ///     The event log name such as Application, Security, or System. This will almost always be Application.
        /// </summary>
        public string Name { get; }
        /// <summary>
        ///     The message to write to the event log.
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        ///     The <see cref="EventLogMessageType"/> of the message.
        /// </summary>
        public EventLogMessageType MessageType { get; set; }
        /// <summary>
        ///     The id of the message.
        /// </summary>
        public long EventId { get; set; } = 0;
    }
}
