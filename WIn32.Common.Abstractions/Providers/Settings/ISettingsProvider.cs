using System;

using Win32.Common.Services.Settings;

namespace Win32.Common.Providers.Settings
{

    /// <summary>
    ///     Provides backbone for settings data storage.
    /// </summary>
    /// <typeparam name="TSetting"><see cref="ISettings"/></typeparam>
    /// <typeparam name="TConfig"><see cref="ISettingsProviderConfig"/></typeparam>
    public interface ISettingsProvider<TSetting, TConfig> : IProvider, IDisposable
        where TSetting : class, ISettings, new()
        where TConfig : class, ISettingsProviderConfig
    {
        /// <summary>
        ///     The <see cref="ISettings"/> object.
        /// </summary>
        TSetting? Settings { get; }

        /// <summary>
        ///     Gets <see cref="ISettings"/> from the <see cref="IProvider"/>.
        /// </summary>
        /// <remarks>
        ///     Returns a default instance of <typeparamref name="TSetting"/> if the <see cref="ISettings"/> doesn't exist.
        /// </remarks>
        /// <returns>An instance of <see cref="ISettings"/> from the <see cref="IProvider"/>.</returns>
        TSetting GetSettings();
        /// <summary>
        ///     Initializes the class. Must be run prior to using this class.
        /// </summary>
        /// <param name="settingsConfig">A class that derives from <see cref="ISettingsProviderConfig"/>.</param>
        void Initialize(TConfig settingsConfig);
        /// <summary>
        ///     Updates the settings associated with <typeparamref name="TSetting"/>.
        /// </summary>
        /// <param name="settings">The updated settings.</param>
        void UpdateSettings(TSetting settings);
    }

    /// <summary>
    ///     Defines a class as a config for a settings provider.
    /// </summary>
    public interface ISettingsProviderConfig { }
}
