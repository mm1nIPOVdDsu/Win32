using System;

using Win32.Common.Providers.Logging;
using Win32.Common.Services.Logging;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

namespace Win32.Common.Extensions
{
    /// <summary>
    ///     Extensions for <see cref="FileLogger"/> for adding to <see cref="IHost"/>.
    /// </summary>
    public static class FileLoggingProviderExtensions
    {
        /// <summary>
        ///     
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddFileLogger(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, FileLoggerProvider>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<FileLoggerConfiguration>, FileLoggerConfigurationSetup>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IOptionsChangeTokenSource<FileLoggerConfiguration>, LoggerProviderOptionsChangeTokenSource<FileLoggerConfiguration, FileLoggerProvider>>());
            //LoggerProviderOptions.RegisterProviderOptions<FileLoggerConfiguration, FileLoggerProvider>(builder.Services);

            return builder;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddFileLogger(this ILoggingBuilder builder, Action<FileLoggerConfiguration> configure)
        {
            if (configure is null)
                throw new ArgumentNullException(nameof(configure));

            builder.SetMinimumLevel(LogLevel.Debug);
            builder.AddFileLogger();
            builder.Services.Configure(configure);

            return builder;
        }
    }
    /// <summary>
    ///     Setup class needed to configure file logger.
    /// </summary>
    internal class FileLoggerConfigurationSetup : ConfigureFromConfigurationOptions<FileLoggerConfiguration>
    {
        /// <summary>
        ///     Constructor that takes the IConfiguration instance to bind against.
        /// </summary>
        public FileLoggerConfigurationSetup(ILoggerProviderConfiguration<FileLoggerProvider> providerConfiguration) : base(providerConfiguration.Configuration) { }
    }
}
