// This an example of creating a configuration for logging. Has not been implemented and is not complete.
// https://github.com/PawelGerr/Thinktecture.Logging.Configuration/blob/master/example/Thinktecture.Extensions.Logging.Configuration.Example/Program.cs
#if false
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Win32.Common.Providers.Logging
{
    // trying to add the ability to change the log level at run time 

    /// <summary>
    ///     Reconfigures <see cref="ILogger"/> and <see cref="ILogger{TCategoryName}"/> at runtime.
    /// </summary>
    public class FileLoggingConfiguration : IFileLoggingConfiguration, IFileLoggingConfigurationProviderCollection
    {
        private readonly List<IFileLoggingConfigurationProvider> _providers;

        /// <summary>
        ///     Initializes new instance of <see cref="LoggingConfiguration"/>.
        /// </summary>
        public FileLoggingConfiguration()
        {
            _providers = new List<IFileLoggingConfigurationProvider>();
        }

        /// <inheritdoc/>
        public void SetLevel(LogLevel level, string? category = null, string? provider = null)
        {
            foreach (var p in _providers)
            {
                p.SetLevel(level, category, provider);
            }
        }

        /// <inheritdoc/>
        public void ResetLevel(string? category = null, string? provider = null)
        {
            foreach (var p in _providers)
            {
                p.ResetLevel(category, provider);
            }
        }

        /// <inheritdoc/>
        int IFileLoggingConfigurationProviderCollection.Count => _providers.Count;

        /// <inheritdoc/>
        void IFileLoggingConfigurationProviderCollection.Add(IFileLoggingConfigurationProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            _providers.Add(provider);
        }

        /// <inheritdoc/>
        void IFileLoggingConfigurationProviderCollection.Remove(IFileLoggingConfigurationProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            _providers.Remove(provider);
        }
    }
    /// <summary>
    ///     TODO
    /// </summary>
    public class FileLoggingConfigurationSource : IConfigurationSource
    {
        private readonly IFileLoggingConfigurationProviderCollection _providerCollection;
        private readonly IEnumerable<string> _parentPath;

        /// <summary>
        ///     Initializes new instance of <see cref="FileLoggingConfigurationSource"/>.
        /// </summary>
        /// <param name="providerCollection">Logging configuration provider collection that newly created providers are going to be added in.</param>
        /// <param name="parentPath">Path to logging section.</param>
        /// `
        public FileLoggingConfigurationSource(IFileLoggingConfigurationProviderCollection providerCollection, params string[] parentPath)
        {
            _providerCollection = providerCollection ?? throw new ArgumentNullException(nameof(providerCollection));
            _parentPath = parentPath ?? throw new ArgumentNullException(nameof(parentPath));

            if (_parentPath.Any(string.IsNullOrWhiteSpace))
                throw new ArgumentException("The segments of the parent path must be null nor empty.");
        }

        /// <summary>
        ///     TODO
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            var provider = new FileLoggingConfigurationProvider(_parentPath);
            _providerCollection.Add(provider);

            return provider;
        }
    }
    /// <summary>
    ///     TODO
    /// </summary>
    public class FileLoggingConfigurationProvider : ConfigurationProvider, IFileLoggingConfigurationProvider
    {
        private readonly IEnumerable<string> _parentPath;

        /// <summary>
        ///     Initializes new instance of <see cref="FileLoggingConfigurationProvider"/>.
        /// </summary>
        /// <param name="parentPath">Path to logging section.</param>
        public FileLoggingConfigurationProvider(IEnumerable<string> parentPath)
        {
            _parentPath = parentPath ?? throw new ArgumentNullException(nameof(parentPath));

            if (_parentPath.Any(string.IsNullOrWhiteSpace))
                throw new ArgumentException("The segments of the parent path must be null nor empty.");
        }

        /// <summary>
        ///     TODO
        /// </summary>
        /// <param name="level"></param>
        /// <param name="category"></param>
        /// <param name="provider"></param>
        public void SetLevel(LogLevel level, string? category = null, string? provider = null)
        {
            // returns something like "Console:LogLevel:Thinktecture"
            var path = BuildLogLevelPath(category, provider);
            // returns log level like "Debug"
            var levelName = GetLevelName(level);
            // Data and OnReload() are provided by the base class
            Data[path] = levelName;
            OnReload(); // notifies other components
        }
        /// <summary>
        ///     TODO
        /// </summary>
        /// <param name="category"></param>
        /// <param name="provider"></param>
        /// <exception cref="ArgumentException"></exception>
		public void ResetLevel(string? category = null, string? provider = null)
        {
            if (!string.IsNullOrEmpty(category) || !string.IsNullOrWhiteSpace(provider))
            {
                var path = BuildLogLevelPath(category, provider);
                Data.Remove(path);
            }
            else
            {
                Data.Clear();
            }

            OnReload();
        }

        /// <summary>
        ///     TODO
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private static string GetLevelName(LogLevel level)
        {
            return Enum.GetName(typeof(LogLevel), level) ?? throw new ArgumentException($"Provided value is not a valid {nameof(LogLevel)}: {level}", nameof(level));
        }
        /// <summary>
        ///     TODO
        /// </summary>
        /// <param name="category"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        private string BuildLogLevelPath(string? category, string? provider)
        {
            var segments = _parentPath.ToList();

            if (!string.IsNullOrWhiteSpace(provider))
                segments.Add(provider!.Trim());

            segments.Add("LogLevel");
            segments.Add(string.IsNullOrWhiteSpace(category) ? "Default" : category!.Trim());
            return ConfigurationPath.Combine(segments);
        }
    }

    /// <summary>
    ///     Reconfigures <see cref="ILogger"/> and <see cref="ILogger{TCategoryName}"/> at runtime.
    /// </summary>
	public interface IFileLoggingConfiguration
    {
        /// <summary>
        ///     Sets <paramref name="level"/> for provided <paramref name="category"/> and <paramref name="provider"/> if provided.
        /// </summary>
        /// <param name="level">Log level.</param>
        /// <param name="category">Logging category.</param>
        /// <param name="provider">Logging provider.</param>
        void SetLevel(LogLevel level, string? category = null, string? provider = null);

        /// <summary>
        ///     Resets the log level for provided <paramref name="category"/> and <paramref name="provider"/>.
        /// </summary>
        /// <param name="category">Logging category.</param>
        /// <param name="provider">Logging provider.</param>
        void ResetLevel(string? category = null, string? provider = null);
    }
    /// <summary>
    ///     TODO
    /// </summary>
    public interface IFileLoggingConfigurationProvider
    {
        /// <summary>
        ///     TODO
        /// </summary>
        /// <param name="category"></param>
        /// <param name="provider"></param>
        void ResetLevel(string? category = null, string? provider = null);
        /// <summary>
        ///     TODO
        /// </summary>
        /// <param name="level"></param>
        /// <param name="category"></param>
        /// <param name="provider"></param>
        void SetLevel(LogLevel level, string? category = null, string? provider = null);
    }
    /// <summary>
    ///     A collection of <see cref="IFileLoggingConfigurationProviderCollection"/>.
    /// </summary>
    public interface IFileLoggingConfigurationProviderCollection
    {
        /// <summary>
        ///     Number of providers.
        /// </summary>
        int Count { get; }

        /// <summary>
        ///     Adds a provider to collection.
        /// </summary>
        /// <param name="provider">Provider to add.</param>
        void Add(IFileLoggingConfigurationProvider provider);

        /// <summary>
        ///     Removes a provider from collection.
        /// </summary>
        /// <param name="provider">Provider to remove.</param>
        void Remove(IFileLoggingConfigurationProvider provider);
    }
}
#endif