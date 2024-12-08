using System;
using System.Runtime.Versioning;

using Win32.Common.Services.Identity;
using Win32.Common.Services.Registry;
using Win32.Common.Services.Serialization;
using Win32.Common.Services.Settings;

using Microsoft.Extensions.Logging;

namespace Win32.Common.Providers.Settings
{
    /// <summary>
    ///     Allows the use of the registry as settings provider.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class SettingsRegistryProvider<TSetting> : ISettingsProvider<TSetting, SettingsRegistryConfig>, ISettingsRegistryProvider
        where TSetting : class, ISettings, new()
    {
        /// <summary>
        ///     The registry path where settings are located.
        /// </summary>
        private const string SETTINGS_PATH = "Software\\Policies\\Common";

        private readonly ILogger<SettingsRegistryProvider<TSetting>> _logger;
        private readonly ISerializationService _serializationService;
        private readonly IRegistryMonitorService _registryMonitor;
        private readonly IRegistryService _registryService;
        private readonly IIdentityService _identityService;

        private SettingsRegistryConfig? config = null;
        //private TSetting? currentSettings = null;
        private string? settingsFullPath = null;
        private RegistryRoot registryRoot;
        private bool initialized = false;
        private string? userSid = null;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SettingsRegistryProvider{TSetting}"/> class.
        /// </summary>
        /// <param name="logger">An instance of <see cref="ILogger{SettingsRegistryProvider}"/>.</param>
        /// <param name="registryService">An instance of <see cref="IRegistryService"/>.</param>
        /// <param name="registryMonitor">An instance of <see cref="IRegistryMonitorService"/>.</param>
        /// <param name="identityService">An instance of <see cref="IIdentityService"/>.</param>
        /// <param name="serializationService">An instance of <see cref="ISerializationService"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="logger"/>, <paramref name="registryService"/>, <paramref name="registryMonitor"/>, or <paramref name="identityService"/> are null.</exception>
        public SettingsRegistryProvider(ILogger<SettingsRegistryProvider<TSetting>> logger, IRegistryService registryService, IRegistryMonitorService registryMonitor, IIdentityService identityService, ISerializationService serializationService)
        {
            _serializationService = serializationService is not null ? serializationService : throw new ArgumentNullException(nameof(serializationService));
            _registryMonitor = registryMonitor is not null ? registryMonitor : throw new ArgumentNullException(nameof(registryMonitor));
            _registryService = registryService is not null ? registryService : throw new ArgumentNullException(nameof(registryService));
            _identityService = identityService is not null ? identityService : throw new ArgumentNullException(nameof(identityService));
            _logger = logger is not null ? logger : throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        ///     The current settings.
        /// </summary>
        public TSetting? Settings { get; private set; }

        /// <summary>
        ///     Initializes the class. Must be run prior to using this class.
        /// </summary>
        /// <param name="registryConfig">Configuration information to use for this instance of the <see cref="ISettingsRegistryProvider"/></param>
        public void Initialize(SettingsRegistryConfig registryConfig)
        {
            if (config is null)
                throw new ArgumentNullException(nameof(config));
            if (initialized is true)
                throw new Exception("Settings registry provider has already been initialized.");
            if (string.IsNullOrEmpty(registryConfig.SettingsName))
                throw new ArgumentException("Settings name is required.");

            _logger.LogInformation("Initializing settings registry provider.");
            config = registryConfig;

            try
            {
                if (config.Scope == SettingsScope.User)
                {
                    _logger.LogDebug("Settings provider scope is user, getting user SID.");
                    registryRoot = RegistryRoot.Users;
                    using (var user = _identityService.GetCurrentUserFromProcess("explorer"))
                    {
                        if (user.User is null)
                            throw new Exception("Cannot get user information from the Windows Identity.");

                        userSid = user.User.Value;
                        _logger.LogDebug("User SID is {userSid}.", userSid);

                        settingsFullPath = $"{userSid}\\{SETTINGS_PATH}";
                    }
                }
                else
                {
                    _logger.LogDebug("Settings provider scope is global.");
                    registryRoot = RegistryRoot.LocalMachine;
                    settingsFullPath = $"{SETTINGS_PATH}";
                }

                _logger.LogInformation("Retrieving settings if they exist.");
                var settingsString = GetCurrentSettings();
                Settings = _serializationService.Deserialize<TSetting>(settingsString);
                initialized = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initializing the registry provider.");
                throw;
            }
        }

        /// <summary>
        ///     Adds new settings
        /// </summary>
        /// <param name="settings"></param>
        public void AddSettings(ISettings settings)
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
            if (initialized is false)
                throw new Exception("Initialization must be called before other methods.");
            if (settings is null)
                throw new ArgumentNullException(nameof(settings));
            if (string.IsNullOrEmpty(settingsFullPath))
                throw new NullReferenceException("Settings full path is null or empty.");
            if (config is null)
                throw new NullReferenceException("Settings config cannot be null.");

            _logger.LogInformation("Updating settings.");
            _logger.LogDebug("Serializing settings into a string.");
            var serializedSettings = _serializationService.Serialize(settings);
            if (_registryService.SetKeyValue(registryRoot, settingsFullPath, config.SettingsName, serializedSettings) == false)
                throw new Exception("An error occurred updating registry settings.");
        }

        /// <summary>
        ///     Gets settings from the registry.
        /// </summary>
        /// <returns>The string settings from registry or the default <typeparamref name="TSetting"/> as a string.</returns>
        private string GetCurrentSettings()
        {
            if (string.IsNullOrEmpty(settingsFullPath))
                throw new NullReferenceException("Settings full path is null or empty.");
            if (config is null)
                throw new NullReferenceException("Settings config is null.");

            _logger.LogDebug("Getting settings from the registry.");
            var value = _registryService.GetKeyValue<string>(registryRoot, settingsFullPath, config.SettingsName);

            if (string.IsNullOrEmpty(value))
            {
                _logger.LogDebug("Settings don't exist in {registryRoot}\\{settingsFullPath}, creating default settings.", registryRoot, settingsFullPath);
                if (_registryService.SetKeyValue(registryRoot, settingsFullPath, config.SettingsName, new TSetting()) == false)
                    throw new Exception("An error occurred updating registry settings.");

                _logger.LogDebug("Settings written to registry.");
                value = _serializationService.Serialize(new TSetting());
            }

            return value;
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
    ///     Configuration information to use for the <see cref="ISettingsRegistryProvider"/>.
    /// </summary>
    public class SettingsRegistryConfig : ISettingsProviderConfig
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
    /// <summary>
    ///     Defines the scope of the settings.
    /// </summary>
    public enum SettingsScope
    {
        /// <summary>
        ///     Settings are specific to the logged on user.
        /// </summary>
        User,
        /// <summary>
        ///     The settings are global in scope.
        /// </summary>
        Global
    }
}
