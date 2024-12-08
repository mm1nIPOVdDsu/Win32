// This an example of creating a configuration for logging. Has not been implemented and is not complete.
// https://github.com/PawelGerr/Thinktecture.Logging.Configuration/blob/master/example/Thinktecture.Extensions.Logging.Configuration.Example/Program.cs
#if false
using System;

using Microsoft.Extensions.Configuration;

namespace Win32.Common.Providers.Logging
{
    /// <summary>
    ///     Extension methods for <see cref="IConfigurationBuilder"/>.
    /// </summary>
    public static class ConfigurationBuildExtensions
    {
        /// <summary>
        ///     Adds logging configuration provider <paramref name="builder"/>.
        /// </summary>
        /// <param name="builder">Configuration builder the logging configuration to add to.</param>
        /// <param name="configuration">Logging configuration to add to the <paramref name="builder"/>.</param>
        /// <param name="parentPath">Path the logging section.</param>
        /// <returns>An instance of <see cref="IConfigurationBuilder"/>.</returns>
        public static IConfigurationBuilder AddLoggingConfiguration(this IConfigurationBuilder builder, IFileLoggingConfigurationProviderCollection configuration, params string[] parentPath)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            return builder.Add(new FileLoggingConfigurationSource(configuration, parentPath));
        }
    }
}
#endif