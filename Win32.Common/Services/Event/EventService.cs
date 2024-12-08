using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

using Win32.Common.Attributes;

using Microsoft.Extensions.Logging;

namespace Win32.Common.Services.Event
{
    /// <summary>
    ///     Services for publishing messages.
    /// </summary>
    [Singleton]
    public class EventService : IEventService
    {
        private readonly List<PublishedMessage> _lastPublishedMessages = new();
        private readonly List<PublishedTimer> _publishedTimers = new();
        private readonly List<Subscription> _subscriptions = new();

        private readonly object _lastPublishedMessagesLocker = new();
        private readonly object _subscriptionStateLocker = new();
        private readonly object _publishedTimersLocker = new();
        private readonly object _subscriptionLocker = new();

        private readonly ILogger<EventService> _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EventService"/> class.
        /// </summary>
        /// <param name="logger">An instance of a <see cref="ILogger{EventService}"/>.</param>
        public EventService(ILogger<EventService> logger)
        {
            if (logger is null)
                throw new ArgumentNullException(nameof(logger));

            _logger = logger;
        }

        /// <summary>
        ///     Gets the message that was last published.
        /// </summary>
        /// <typeparam name="T">The <typeparamref name="T"/> message to get.</typeparam>
        /// <returns>The last <typeparamref name="T"/> if it exists.</returns>
        public T? GetLastPublishedMessage<T>() where T : IEventMessage => GetLastPublishedMessage<T>("");

        /// <summary>
        ///     Gets the message that was last published.
        /// </summary>
        /// <typeparam name="T">The <typeparamref name="T"/> message to get.</typeparam>
        /// <param name="name">The name of the message to retrieve.</param>
        /// <returns>The last <typeparamref name="T"/> if it exists.</returns>
        public T? GetLastPublishedMessage<T>(string name) where T : IEventMessage
        {
            var lastPublished = FindLastPublishedMessage<T>(name);
            return lastPublished == null ? default : lastPublished.Message;
        }

        /// <summary>
        ///     Sets the publish state of an event for a subscription.
        /// </summary>
        /// <param name="subscriptionId">The <see cref="Guid"/> id of the subscription.</param>
        /// <param name="canPublish">If true, the subscription will receive event message.</param>
        public void SetPublishState(Guid subscriptionId, bool canPublish)
        {
            lock (_subscriptionStateLocker)
            {
                var subscription = _subscriptions.FirstOrDefault(s => s.Id == subscriptionId);
                if (subscription is null)
                    return;

                subscription.CanPublish = canPublish;
            }
        }

        /// <summary>
        ///     Published a message.
        /// </summary>
        /// <typeparam name="T">The <typeparamref name="T"/> message to publish.</typeparam>
        /// <param name="message">The <typeparamref name="T"/> instance that will get published.</param>
        /// <param name="name">A unique name for the message.</param>
        public void Publish<T>(T message, string name = "") where T : IEventMessage
        {
            if (message is null)
                throw new ArgumentNullException(nameof(message));

            if (message.EventMessageId == default)
                message.EventMessageId = Guid.NewGuid();

            void setDateTime(T m)
            {
                if (m.MessageDateTime == default || m.MessageDateTime == DateTime.MinValue)
                    m.MessageDateTime = DateTime.Now;
            }

            lock (_lastPublishedMessagesLocker)
            {
                var published = FindLastPublishedMessage<T>(name);
                // remove the last published message with the same name
                if (published != null)
                    _lastPublishedMessages.Remove(published);
                // add the message to the last published
                _lastPublishedMessages.Add(new PublishedMessage<T>(message, name));
            }

            List<Subscription> tempSubs;
            lock (_subscriptionLocker) { tempSubs = new List<Subscription>(_subscriptions); }

            // for loops are faster than foreach loops and we're string to make this part a quick as possible
            for (var i = 0; i < tempSubs.Count; i++)
            {
                // don't raise the event for the subscription if paused.
                if (!tempSubs[i].CanPublish)
                    continue;

                if (tempSubs[i] is Subscription<T> toBeHandled && toBeHandled.Name == name)
                {
                    setDateTime(message);
                    toBeHandled.Action(message);
                }

                if (tempSubs is SubscriptionEmptyMessage<T> toBeHandledWithEmptyMessage && toBeHandledWithEmptyMessage.Name == name)
                {
                    setDateTime(message);
                    toBeHandledWithEmptyMessage.Action();
                }
            }
        }

        /// <summary>
        ///     Published a message asynchronously.
        /// </summary>
        /// <param name="name">The name of the message to publish.</param>
        /// <returns><see cref="Task"/></returns>
        public Task PublishAsync(string name) =>
            Task.Factory.StartNew(() => Publish(name));
        /// <summary>
        ///     Published a message asynchronously.
        /// </summary>
        /// <param name="name">The name of the message to publish.</param>
        /// <param name="interval">The interval which the message will publish.</param>
        /// <param name="fireImmediately">
        ///     True if the event should publish immediately or wait until the first interval period.
        /// </param>
        /// <returns><see cref="Task"/></returns>
        public Task PublishAsync(string name, double interval, bool fireImmediately = false) =>
            Task.Factory.StartNew(() => Publish(name, interval, fireImmediately));
        /// <summary>
        ///     Published a message asynchronously.
        /// </summary>
        /// <typeparam name="T">The <typeparamref name="T"/> message to publish.</typeparam>
        /// <param name="message">The <typeparamref name="T"/> instance that will get published.</param>
        /// <returns><see cref="Task"/></returns>
        public Task PublishAsync<T>(T message) where T : IEventMessage =>
            Task.Factory.StartNew(() => Publish(message));
        /// <summary>
        ///     Published a message asynchronously.
        /// </summary>
        /// <typeparam name="T">The <typeparamref name="T"/> message to publish.</typeparam>
        /// <param name="message">The <typeparamref name="T"/> instance that will get published.</param>
        /// <param name="name">A unique name for the message.</param>
        /// <returns><see cref="Task"/></returns>
        public Task PublishAsync<T>(T message, string name) where T : IEventMessage =>
            Task.Factory.StartNew(() => Publish(message, name));
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
        public Task PublishAsync<T>(T message, string name, double interval, bool fireImmediately = false) where T : IEventMessage =>
            Task.Factory.StartNew(() => Publish(message, name, interval, fireImmediately));

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
        public Task PublishAndSubscribeAsync(string name, double interval, Action action, bool fireImmediately = false) =>
            Task.Factory.StartNew(() => PublishAndSubscribe(name, interval, action, fireImmediately));
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
        public Task PublishAndSubscribeAsync<T>(T message, string name, double interval, Action<T> action, bool fireImmediately = false) where T : IEventMessage =>
            Task.Factory.StartNew(() => PublishAndSubscribe(message, name, interval, action, fireImmediately));
        /// <summary>
        ///     Stops publishing all messages.
        /// </summary>
        public void StopPublishing()
        {
            lock (_publishedTimersLocker)
            {
                _publishedTimers.ForEach(pt =>
                {
                    pt.Timer.Stop();
                    pt.Timer.Close();
                    _publishedTimers.Remove(pt);
                });
            }
        }
        /// <summary>
        ///     Stops publishing a specific message.
        /// </summary>
        /// <param name="name">The name of the message to stop publishing.</param>
        public void StopPublishing(string name)
        {
            lock (_publishedTimersLocker)
            {
                var publishedTimer = _publishedTimers
                    .FirstOrDefault(t => t.Name == name);

                if (publishedTimer == null)
                    return;

                publishedTimer.Timer.Stop();
                publishedTimer.Timer.Close();
                _publishedTimers.Remove(publishedTimer);
            }
        }

        /// <summary>
        ///     Subscribes to a message.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> of the calling method that will execute.</param>
        /// <param name="name">The name of the message.</param>
        /// <returns>The ID as a <see cref="Guid"/> of the subscription. This ID is used to unsubscribe from an Event Message.</returns>
        public Guid Subscribe(Action action, string name)
        {
            lock (_subscriptionLocker)
            {
                var subscription = FindSubscription(action, name);

                if (subscription != null)
                    return subscription.Id;

                subscription = new SubscriptionEmptyMessage<EmptyMessage>(action, name);
                _subscriptions.Add(subscription);
                return subscription.Id;
            }
        }
        /// <summary>
        ///     Subscribes to a message.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> of the calling method that will execute.</param>
        /// <param name="name">The name of the message.</param>
        /// <returns>The ID as a <see cref="Guid"/> of the subscription. This ID is used to unsubscribe from an Event Message.</returns>
        public Task<Guid> SubscribeAsync(Action action, string name) =>
            Task.Factory.StartNew(() => Subscribe(action, name));
        /// <summary>
        ///     Subscribes to a message.
        /// </summary>
        /// <typeparam name="T">The <see cref="IEventMessage"/> to subscribe to.</typeparam>
        /// <param name="action">The <see cref="Action"/> of the calling method that will execute.</param>
        /// <returns>The ID as a <see cref="Guid"/> of the subscription. This ID is used to unsubscribe from an Event Message.</returns>
        public Guid Subscribe<T>(Action<T> action) where T : IEventMessage => Subscribe(action, "");
        /// <summary>
        ///     Subscribes to a message.
        /// </summary>
        /// <typeparam name="T">The <see cref="IEventMessage"/> to subscribe to.</typeparam>
        /// <param name="action">The <see cref="Action"/> of the calling method that will execute.</param>
        /// <returns>The ID as a <see cref="Guid"/> of the subscription. This ID is used to unsubscribe from an Event Message.</returns>
        public Task<Guid> SubscribeAsync<T>(Action<T> action) where T : IEventMessage =>
            Task.Factory.StartNew(() => Subscribe(action));
        /// <summary>
        ///     Subscribes to a message.
        /// </summary>
        /// <typeparam name="T">The <see cref="IEventMessage"/> to subscribe to.</typeparam>
        /// <param name="action">The <see cref="Action"/> of the calling method that will execute.</param>
        /// <param name="name">A unique name for the message.</param>
        /// <returns>The ID as a <see cref="Guid"/> of the subscription. This ID is used to unsubscribe from an Event Message.</returns>
        public Guid Subscribe<T>(Action<T> action, string name) where T : IEventMessage
        {
            lock (_subscriptionLocker)
            {
                var subscription = FindSubscription(action, name);

                if (subscription != null)
                    return subscription.Id;

                subscription = new Subscription<T>(action, name);
                _subscriptions.Add(subscription);
                return subscription.Id;
            }
        }
        /// <summary>
        ///     Subscribes to a message.
        /// </summary>
        /// <typeparam name="T">The <see cref="IEventMessage"/> to subscribe to.</typeparam>
        /// <param name="action">The <see cref="Action"/> of the calling method that will execute.</param>
        /// <param name="name">A unique name for the message.</param>
        /// <returns>The ID as a <see cref="Guid"/> of the subscription. This ID is used to unsubscribe from an Event Message.</returns>
        public Task<Guid> SubscribeAsync<T>(Action<T> action, string name) where T : IEventMessage =>
            Task.Factory.StartNew(() => Subscribe(action, name));

        /// <summary>
        ///     Unsubscribes from a message.
        /// </summary>
        /// <param name="subscriptionId">The <see cref="Guid"/> id of the subscription.</param>
        public void Unsubscribe(Guid subscriptionId)
        {
            if (subscriptionId == Guid.Empty)
                throw new ArgumentException("Subscription id cannot be a default value.");

            lock (_subscriptionLocker)
            {
                var subscription = _subscriptions.FirstOrDefault(s => s.Id == subscriptionId);
                if (subscription != null)
                    _subscriptions.Remove(subscription);
            }
        }
        /// <summary>
        ///     Unsubscribes from a message.
        /// </summary>
        /// <typeparam name="T">The <see cref="IEventMessage"/> to unsubscribe from.</typeparam>
        /// <param name="action">The <see cref="Action"/> of the calling method that will execute.</param>
        public void Unsubscribe<T>(Action<T> action) where T : IEventMessage
        {
            lock (_subscriptionLocker)
            {
                var subscription = FindSubscription(action);
                if (subscription != null)
                    _subscriptions.Remove(subscription);
            }
        }

        /// <summary>
        ///     Finds a subscription.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> used in a subscription.</param>
        /// <param name="name">The name of the subscription.</param>
        /// <returns>A <see cref="Subscription"/>.</returns>
        private Subscription? FindSubscription(Action action, string name) =>
            _subscriptions
                .Where(s => s.Name == name)
                .Where(s => s is Subscription<EmptyMessage>)
                .Cast<SubscriptionEmptyMessage<EmptyMessage>>()
                .FirstOrDefault(s => s.Action == action);
        /// <summary>
        ///     Finds a subscription.
        /// </summary>
        /// <typeparam name="T">The <see cref="IEventMessage"/> type of the subscription.</typeparam>
        /// <param name="action">The <see cref="Action"/> used in a subscription.</param>
        /// <returns>A <see cref="Subscription"/>.</returns>
        private Subscription<T>? FindSubscription<T>(Action<T> action) where T : IEventMessage =>
            _subscriptions
                .Where(s => s is Subscription<T>)
                .Cast<Subscription<T>>()
                .FirstOrDefault(s => s.Action == action);
        /// <summary>
        ///     Finds a subscription.
        /// </summary>
        /// <typeparam name="T">The <see cref="IEventMessage"/> type of the subscription.</typeparam>
        /// <param name="action">The <see cref="Action"/> used in a subscription.</param>
        /// <param name="name">The name of the subscription.</param>
        /// <returns>A <see cref="Subscription"/>.</returns>
        private Subscription? FindSubscription<T>(Action<T> action, string name) where T : IEventMessage =>
            _subscriptions
                .Where(s => s.Name == name)
                .Where(s => s is Subscription<T>)
                .Cast<Subscription<T>>()
                .FirstOrDefault(s => s.Action == action);

        /// <summary>
        ///     Finds the last published message of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="IEventMessage"/> event message.</typeparam>
        /// <param name="name">The name of the message.</param>
        /// <returns><see cref="PublishedMessage{IEventMessage}"/></returns>
        private PublishedMessage<T>? FindLastPublishedMessage<T>(string name) where T : IEventMessage =>
            _lastPublishedMessages
                .Where(m => m.Name == name)
                .Where(m => m is PublishedMessage<T>)
                .Cast<PublishedMessage<T>>()
                .FirstOrDefault();
        /// <summary>
        ///     Publishes a message.
        /// </summary>
        /// <param name="name">The name of the message.</param>
        private void Publish(string name) => Publish(new EmptyMessage(), name);
        /// <summary>
        ///     Publishes a message.
        /// </summary>
        /// <param name="name">The name of the message.</param>
        /// <param name="interval">The interval which the message will publish.</param>
        /// <param name="fireImmediately">
        ///     True if the event should publish immediately or wait until the first interval period.
        /// </param>
        private void Publish(string name, double interval, bool fireImmediately = false) => Publish(new EmptyMessage(), name, interval, fireImmediately);
        /// <summary>
        ///     Publishes a message.
        /// </summary>
        /// <typeparam name="T">The <typeparamref name="T"/> message to publish.</typeparam>
        /// <param name="message">The <typeparamref name="T"/> instance that will get published.</param>
        /// <param name="name">The name of the message.</param>
        /// <param name="interval">The interval which the message will publish.</param>
        /// <param name="fireImmediately">
        ///     True if the event should publish immediately or wait until the first interval period.
        /// </param>
        private void Publish<T>(T message, string name, double interval, bool fireImmediately = false) where T : IEventMessage
        {
            lock (_publishedTimersLocker)
            {
                var publishedTimer = _publishedTimers
                    .Where(t => t is PublishedTimer<T>)
                    .Cast<PublishedTimer<T>>()
                    .FirstOrDefault(t => t.Name == name);

                if (publishedTimer is not null)
                    return;

                publishedTimer = new PublishedTimer<T>(message, name, interval);
                _publishedTimers.Add(publishedTimer);

                publishedTimer.Timer.Elapsed += (sender, args) => Publish(message, name);
                publishedTimer.Timer.Start();
            }

            if (fireImmediately)
                Publish(message, name);
        }
        /// <summary>
        ///     Publishes a message.
        /// </summary>
        /// <param name="name">The name of the message.</param>
        /// <param name="interval">The interval which the message will publish.</param>
        /// <param name="action">The <see cref="Action"/> of the calling method that will execute.</param>
        /// <param name="fireImmediately">
        ///     True if the event should publish immediately or wait until the first interval period.
        /// </param>
        /// <returns>The ID as a <see cref="Guid"/> of the subscription. This ID is used to unsubscribe from an Event Message.</returns>
        private Guid PublishAndSubscribe(string name, double interval, Action action, bool fireImmediately = false)
        {
            var subscriptionId = Subscribe(action, name);
            Publish(name, interval, fireImmediately);

            return subscriptionId;
        }
        /// <summary>
        ///     Publishes a message.
        /// </summary>
        /// <typeparam name="T">The <typeparamref name="T"/> message to publish.</typeparam>
        /// <param name="message">The <typeparamref name="T"/> instance that will get published.</param>
        /// <param name="name">The name of the message.</param>
        /// <param name="interval">The interval which the message will publish.</param>
        /// <param name="action">The <see cref="Action"/> of the calling method that will execute.</param>
        /// <param name="fireImmediately">
        ///     True if the event should publish immediately or wait until the first interval period.
        /// </param>
        /// <returns>The ID as a <see cref="Guid"/> of the subscription. This ID is used to unsubscribe from an Event Message.</returns>
        private Guid PublishAndSubscribe<T>(T message, string name, double interval, Action<T> action, bool fireImmediately = false) where T : IEventMessage
        {
            var subscriptionId = Subscribe(action, name);
            Publish(message, name, interval, fireImmediately);

            return subscriptionId;
        }

        /// <summary>
        ///     An empty subscription message.
        /// </summary>
        /// <typeparam name="T">The <see cref="IEventMessage"/> type of the message.</typeparam>
        private class SubscriptionEmptyMessage<T> : Subscription where T : IEventMessage
        {
            /// <summary>
            ///     Initializes a new instance of the <see cref="SubscriptionEmptyMessage{T}"/> class.
            /// </summary>
            /// <param name="action">The <see cref="Action"/> of the calling method that will execute.</param>
            /// <param name="name">The name of the message.</param>
            public SubscriptionEmptyMessage(Action action, string name)
            {
                Action = action;
                Name = name;
            }

            /// <summary>
            ///     The <see cref="Action"/> of the calling method that will execute.
            /// </summary>
            public Action Action { get; }
        }
        /// <summary>
        ///     Represents a subscription to an <see cref="IEventMessage"/>.
        /// </summary>
        /// <typeparam name="T"><see cref="IEventMessage"/></typeparam>
        private class Subscription<T> : Subscription where T : IEventMessage
        {
            /// <summary>
            ///     Initializes a new instance of the <see cref="SubscriptionEmptyMessage{T}"/> class.
            /// </summary>
            /// <param name="action">The <see cref="Action"/> of the calling method that will execute.</param>
            /// <param name="name">The name of the message.</param>
            public Subscription(Action<T> action, string name)
            {
                Action = action;
                Name = name;
            }

            /// <summary>
            ///     The <see cref="Action"/> of the calling method that will execute.
            /// </summary>
            public Action<T> Action { get; }
        }
        /// <summary>
        ///     Represents a subscription to an <see cref="IEventMessage"/>.
        /// </summary>
        private abstract class Subscription
        {
            /// <summary>
            ///     Initializes a new instance of the <see cref="Subscription"/> class.
            /// </summary>
            protected Subscription() => Id = Guid.NewGuid();

            /// <summary>
            ///     The subscription Id.
            /// </summary>
            public Guid Id { get; }
            /// <summary>
            ///     The name of the message.
            /// </summary>
            public string Name { get; protected set; } = "";
            /// <summary>
            ///     If true, the subscription will receive event messages.
            /// </summary>
            public bool CanPublish { get; set; } = true;
        }
        /// <summary>
        ///     Represents a message that can be published.
        /// </summary>
        /// <typeparam name="T"><see cref="IEventMessage"/></typeparam>
        private class PublishedMessage<T> : PublishedMessage
        {
            /// <summary>
            ///     Initializes a new instance of the <see cref="SubscriptionEmptyMessage{T}"/> class.
            /// </summary>
            /// <param name="message">An <see cref="IEventMessage"/>.</param>
            /// <param name="name">The name of the message.</param>
            public PublishedMessage(T message, string name)
            {
                Message = message;
                Name = name;
            }

            /// <summary>
            ///     The <see cref="IEventMessage"/> to publish.
            /// </summary>
            public T Message { get; }
        }
        /// <summary>
        ///     Represents a message that can be published.
        /// </summary>
        private abstract class PublishedMessage
        {
            /// <summary>
            ///     The name of the message.
            /// </summary>
            public string Name { get; protected set; } = "";
        }
        /// <summary>
        ///     Represents a message that re-publishes a message at an interval.
        /// </summary>
        private abstract class PublishedTimer
        {
            /// <summary>
            ///     The name of the message.
            /// </summary>
            public abstract string Name { get; }
            /// <summary>
            ///     The time for re-publishing a message.
            /// </summary>
            public abstract Timer Timer { get; }
        }
        /// <summary>
        ///     Represents a message that re-publishes a message at an interval.
        /// </summary>
        /// <typeparam name="T"><see cref="IEventMessage"/></typeparam>
        private class PublishedTimer<T> : PublishedTimer
        {
            private readonly PublishedMessage<T> _publishedMessage;

            /// <summary>
            ///     Initializes a new instance of the <see cref="SubscriptionEmptyMessage{T}"/> class.
            /// </summary>
            /// <param name="message">The message to publish.</param>
            /// <param name="name">The name of the message.</param>
            /// <param name="interval">The time for re-publishing a message.</param>
            public PublishedTimer(T message, string name, double interval)
            {
                _publishedMessage = new PublishedMessage<T>(message, name);
                Timer = new Timer(interval);
            }

            /// <summary>
            ///     The name of the message.
            /// </summary>
            public override string Name => _publishedMessage.Name;
            /// <summary>
            ///     The time for re-publishing a message.
            /// </summary>
            public override Timer Timer { get; }
        }
        /// <summary>
        ///     An empty message.
        /// </summary>
        private class EmptyMessage : EventMessage { }
    }
}
