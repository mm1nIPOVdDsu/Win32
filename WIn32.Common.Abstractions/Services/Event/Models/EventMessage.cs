using System;

namespace Win32.Common.Services.Event
{
    /// <summary>
    ///     Represents a message that can be raised as an event and distributed to subscribers.
    /// </summary>
    /// <typeparam name="T"><see cref="EventMessage"/></typeparam>
    public class EventMessage<T> : EventMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EventMessage"/> class.
        /// </summary>
        /// <param name="message"><see cref="EventMessage"/></param>
        public EventMessage(T message)
        {
            Message = message;
        }

        /// <summary>
        ///     The message information of the event.
        /// </summary>
        public T Message { get; }
    }

    /// <summary>
    ///     Represents a message that can be raised as an event and distributed to subscribers.
    /// </summary>
    public class EventMessage : IEventMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EventMessage"/> class.
        /// </summary>
        public EventMessage()
        {
            EventMessageId = Guid.NewGuid();
            MessageDateTime = DateTime.Now;
        }
        /// <summary>
        ///     Initializes a new instance of the <see cref="EventMessage"/> class.
        /// </summary>
        /// <param name="eventMessageId">The event message ID as a <see cref="Guid"/>.</param>
        /// <param name="messageDateTime">The <see cref="DateTime"/> the message was created.</param>
        public EventMessage(Guid eventMessageId, DateTime messageDateTime)
        {
            EventMessageId = eventMessageId;
            MessageDateTime = messageDateTime;
        }

        /// <summary>
        ///     The event message ID as a <see cref="Guid"/>.
        /// </summary>
        public Guid EventMessageId { get; set; }
        /// <summary>
        ///     The <see cref="DateTime"/> the message was created.
        /// </summary>
        public DateTime MessageDateTime { get; set; }
    }
}
