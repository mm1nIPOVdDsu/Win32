using System;
using System.Threading.Tasks;

namespace Win32.Common.Services.Event
{
    /// <summary>
    ///     Defines an event service.
    /// </summary>
    public interface IEventService : IServiceBase
    {
        /// <summary>
        ///     Gets the message that was last published.
        /// </summary>
        /// <typeparam name="T">The <typeparamref name="T"/> message to get.</typeparam>
        /// <returns>The last <typeparamref name="T"/> if it exists.</returns>
        T? GetLastPublishedMessage<T>() where T : IEventMessage;
        /// <summary>
        ///     Gets the message that was last published.
        /// </summary>
        /// <typeparam name="T">The <typeparamref name="T"/> message to get.</typeparam>
        /// <param name="name">The name of the message to retrieve.</param>
        /// <returns>The last <typeparamref name="T"/> if it exists.</returns>
        T? GetLastPublishedMessage<T>(string name) where T : IEventMessage;

        /// <summary>
        ///     Pauses events from being raised for a subscription.
        /// </summary>
        /// <param name="subscriptionId">The <see cref="Guid"/> id of the subscription.</param>
        /// <param name="canPublish">If true, the subscription will receive event message.</param>
        void SetPublishState(Guid subscriptionId, bool canPublish);

        /// <summary>
        ///     Published a message.
        /// </summary>
        /// <typeparam name="T">The <typeparamref name="T"/> message to publish.</typeparam>
        /// <param name="message">The <typeparamref name="T"/> instance that will get published.</param>
        /// <param name="name">A unique name for the message.</param>
        void Publish<T>(T message, string name = "") where T : IEventMessage;

        /// <summary>
        ///     Published a message asynchronously.
        /// </summary>
        /// <param name="name">The name of the message to publish.</param>
        /// <returns><see cref="Task"/></returns>
        Task PublishAsync(string name);
        /// <summary>
        ///     Published a message asynchronously.
        /// </summary>
        /// <param name="name">The name of the message to publish.</param>
        /// <param name="interval">The interval which the message will publish.</param>
        /// <param name="fireImmediately">
        ///     True if the event should publish immediately or wait until the first interval period.
        /// </param>
        /// <returns><see cref="Task"/></returns>
        Task PublishAsync(string name, double interval, bool fireImmediately = false);
        /// <summary>
        ///     Published a message asynchronously.
        /// </summary>
        /// <typeparam name="T">The <typeparamref name="T"/> message to publish.</typeparam>
        /// <param name="message">The <typeparamref name="T"/> instance that will get published.</param>
        /// <returns><see cref="Task"/></returns>
        Task PublishAsync<T>(T message) where T : IEventMessage;
        /// <summary>
        ///     Published a message asynchronously.
        /// </summary>
        /// <typeparam name="T">The <typeparamref name="T"/> message to publish.</typeparam>
        /// <param name="message">The <typeparamref name="T"/> instance that will get published.</param>
        /// <param name="name">A unique name for the message.</param>
        /// <returns><see cref="Task"/></returns>
        Task PublishAsync<T>(T message, string name) where T : IEventMessage;
        /// <summary>
        ///     Published a message asynchronously.
        /// </summary>
        /// <typeparam name="T">The <typeparamref name="T"/> message to publish.</typeparam>
        /// <param name="message">The <typeparamref name="T"/> instance that will get published.</param>
        /// <param name="name">A unique name for the message.</param>
        /// <param name="interval">The interval which the message will publish.</param>
        /// <param name="fireImmediately">
        ///     True if the event should publish immediately or wait until the first interval period.
        /// </param>
        /// <returns><see cref="Task"/></returns>
        Task PublishAsync<T>(T message, string name, double interval, bool fireImmediately = false) where T : IEventMessage;

        /// <summary>
        ///     Publishes and subscribes to a message asynchronously.
        /// </summary>
        /// <param name="name">A unique name for the message.</param>
        /// <param name="interval">The interval which the message will publish.</param>
        /// <param name="action">The <see cref="Action"/> of the calling method that will execute.</param>
        /// <param name="fireImmediately">
        ///     True if the event should publish immediately or wait until the first interval period.
        /// </param>
        /// <returns><see cref="Task"/></returns>
        Task PublishAndSubscribeAsync(string name, double interval, Action action, bool fireImmediately = false);
        /// <summary>
        ///     Publishes and subscribes to a message asynchronously.
        /// </summary>
        /// <typeparam name="T">The <typeparamref name="T"/> message to publish.</typeparam>
        /// <param name="message">The <typeparamref name="T"/> instance that will get published.</param>
        /// <param name="name">A unique name for the message.</param>
        /// <param name="interval">The interval which the message will publish.</param>
        /// <param name="action">The <see cref="Action"/> of the calling method that will execute.</param>
        /// <param name="fireImmediately">
        ///     True if the event should publish immediately or wait until the first interval period.
        /// </param>
        /// <returns><see cref="Task"/></returns>
        Task PublishAndSubscribeAsync<T>(T message, string name, double interval, Action<T> action, bool fireImmediately = false) where T : IEventMessage;

        /// <summary>
        ///     Stops publishing all messages.
        /// </summary>
        void StopPublishing();
        /// <summary>
        ///     Stops publishing a specific message.
        /// </summary>
        /// <param name="name">The name of the message to stop publishing.</param>
        void StopPublishing(string name);

        /// <summary>
        ///     Subscribes to a message.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> of the calling method that will execute.</param>
        /// <param name="name">The name of the message.</param>
        /// <returns>The ID as a <see cref="Guid"/> of the subscription. This ID is used to unsubscribe from an Event Message.</returns>
        Guid Subscribe(Action action, string name);
        /// <summary>
        ///     Subscribes to a message.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> of the calling method that will execute.</param>
        /// <param name="name">The name of the message.</param>
        /// <returns>The ID as a <see cref="Guid"/> of the subscription. This ID is used to unsubscribe from an Event Message.</returns>
        Task<Guid> SubscribeAsync(Action action, string name);
        /// <summary>
        ///     Subscribes to a message.
        /// </summary>
        /// <typeparam name="T">The <see cref="IEventMessage"/> to subscribe to.</typeparam>
        /// <param name="action">The <see cref="Action"/> of the calling method that will execute.</param>
        /// <returns>The ID as a <see cref="Guid"/> of the subscription. This ID is used to unsubscribe from an Event Message.</returns>
        Guid Subscribe<T>(Action<T> action) where T : IEventMessage;
        /// <summary>
        ///     Subscribes to a message.
        /// </summary>
        /// <typeparam name="T">The <see cref="IEventMessage"/> to subscribe to.</typeparam>
        /// <param name="action">The <see cref="Action"/> of the calling method that will execute.</param>
        /// <returns>The ID as a <see cref="Guid"/> of the subscription. This ID is used to unsubscribe from an Event Message.</returns>
        Task<Guid> SubscribeAsync<T>(Action<T> action) where T : IEventMessage;
        /// <summary>
        ///     Subscribes to a message.
        /// </summary>
        /// <typeparam name="T">The <see cref="IEventMessage"/> to subscribe to.</typeparam>
        /// <param name="action">The <see cref="Action"/> of the calling method that will execute.</param>
        /// <param name="name">A unique name for the message.</param>
        /// <returns>The ID as a <see cref="Guid"/> of the subscription. This ID is used to unsubscribe from an Event Message.</returns>
        Guid Subscribe<T>(Action<T> action, string name) where T : IEventMessage;
        /// <summary>
        ///     Subscribes to a message.
        /// </summary>
        /// <typeparam name="T">The <see cref="IEventMessage"/> to subscribe to.</typeparam>
        /// <param name="action">The <see cref="Action"/> of the calling method that will execute.</param>
        /// <param name="name">A unique name for the message.</param>
        /// <returns>The ID as a <see cref="Guid"/> of the subscription. This ID is used to unsubscribe from an Event Message.</returns>
        Task<Guid> SubscribeAsync<T>(Action<T> action, string name) where T : IEventMessage;
        /// <summary>
        ///     Unsubscribes from a message.
        /// </summary>
        /// <param name="subscriptionId">The <see cref="Guid"/> id of the subscription.</param>
        void Unsubscribe(Guid subscriptionId);
        /// <summary>
        ///     Unsubscribes from a message.
        /// </summary>
        /// <typeparam name="T">The <see cref="IEventMessage"/> to unsubscribe from.</typeparam>
        /// <param name="action">The <see cref="Action"/> of the calling method that will execute.</param>
        void Unsubscribe<T>(Action<T> action) where T : IEventMessage;
    }
}
