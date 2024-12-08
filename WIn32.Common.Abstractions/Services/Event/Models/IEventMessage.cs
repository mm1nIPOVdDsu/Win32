using System;

namespace Win32.Common.Services.Event
{
    /// <summary>
    ///     Represents a message that can be raised as an event and distributed to subscribers.
    /// </summary>
    /// <typeparam name="T"><see cref="EventMessage"/></typeparam>
    public interface IEventMessage<T> : IEventMessage
    {
        /// <summary>
        ///     The message information of the event.
        /// </summary>
        T Message { get; set; }
    }

    /// <summary>
    ///     Represents a message that can be raised as an event and distributed to subscribers.
    /// </summary>
    public interface IEventMessage
    {
        /// <summary>
        ///     The event message ID as a <see cref="Guid"/>.
        /// </summary>
        Guid EventMessageId { get; set; }
        /// <summary>
        ///     The <see cref="DateTime"/> the message was created.
        /// </summary>
        DateTime MessageDateTime { get; set; }
    }
}
