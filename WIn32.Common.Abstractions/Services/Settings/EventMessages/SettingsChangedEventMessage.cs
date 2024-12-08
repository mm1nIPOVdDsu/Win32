using Win32.Common.Services.Event;

namespace Win32.Common.Services.Settings
{
    /// <summary>
    ///     An event raised when a settings file has changed.
    /// </summary>
    public class SettingsChangedEventMessage<T> : EventMessage where T : ISettings
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SettingsChangedEventMessage{ISettings}"/> class.
        /// </summary>
        /// <param name="settings"><see cref="ISettings"/></param>
        public SettingsChangedEventMessage(T settings) => Settings = settings;

        /// <summary>
        ///     The <see cref="ISettings"/> information.
        /// </summary>
        public T Settings { get; }
    }
}
