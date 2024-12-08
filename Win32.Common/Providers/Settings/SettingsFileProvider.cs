using System;

using Win32.Common.Services.Settings;

using Microsoft.Extensions.Logging;

namespace Win32.Common.Providers.Settings
{
    /// <summary>
    ///     
    /// </summary>
    public class SettingsFileProvider<TSetting> : ISettingsProvider<TSetting, SettingsFileConfig>, ISettingsFileProvider
        where TSetting : class, ISettings, new()
    {
        private readonly ILogger<SettingsFileProvider<TSetting>> _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SettingsFileProvider{TSetting}"/> class.
        /// </summary>
        /// <param name="logger">An instance of <see cref="ILogger{SettingsFileProvider}"/>.</param>
        public SettingsFileProvider(ILogger<SettingsFileProvider<TSetting>> logger)
        {
            if (_logger is null)
                throw new ArgumentNullException(nameof(logger));

            _logger = logger;
        }

        /// <summary>
        ///     The current settings.
        /// </summary>
        public TSetting? Settings { get; private set; }

        /// <summary>
        ///     Initializes the class. Must be run prior to using this class.
        /// </summary>
        /// <param name="registryConfig">Configuration information to use for this instance of the <see cref="ISettingsRegistryProvider"/></param>
        public void Initialize(SettingsFileConfig registryConfig)
        {

        }
        public TSetting GetSettings()
        {
            return null;
        }
        /// <summary>
        ///     Updates the settings associated with <typeparamref name="TSetting"/>.
        /// </summary>
        /// <param name="settings">The updated settings.</param>
        public void UpdateSettings(TSetting settings)
        {

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

                }

                _disposed = true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        private bool _disposed = false;
    }

    /// <summary>
    ///     Configuration information to use for the <see cref="ISettingsFileProvider"/>.
    /// </summary>
    public class SettingsFileConfig : ISettingsProviderConfig
    {
        /// <summary>
        ///     The scope of the settings.
        /// </summary>
        public SettingsScope Scope { get; set; }
        /// <summary>
        ///     The name of the settings.
        /// </summary>
        public string SettingsName { get; set; } = string.Empty;
    }
}
