using System;
using System.IO;
using System.Threading.Tasks;

using Win32.Common.Services.Serialization;

using Microsoft.Extensions.Logging;

namespace Win32.Common.Services.Telemetry
{
    /// <summary>
    ///     
    /// </summary>
    public class TelemetryService : ITelemetryService
    {
        private readonly ISerializationService _serializationService;
        private readonly ILogger<TelemetryService> _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TelemetryService"/> class.
        /// </summary>
        /// <param name="loggingService">An instance of a <see cref="ILogger{TelemetryService}"/>.</param>
        /// <param name="serializationService">An instance of a <see cref="ISerializationService"/>.</param>
        public TelemetryService(ILogger<TelemetryService> loggingService, ISerializationService serializationService)
        {
            _serializationService = serializationService;
            _logger = loggingService;
        }

        /// <summary>
        ///     Gets telemetry from file.
        /// </summary>
        /// <typeparam name="T">The <see cref="ITelemetry"/> to update or create.</typeparam>
        /// <param name="telemetry">The <typeparamref name="T"/> to get.</param>
        /// <returns>An instance of <typeparamref name="T"/> from file.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="telemetry"/> is null.</exception>
        public async Task<T?> GetTelemetry<T>(T telemetry) where T : class, ITelemetry, new()
        {
            if (telemetry is null)
                throw new ArgumentNullException(nameof(telemetry));

            _logger.LogDebug("Getting telemetry for {name}.", typeof(T).Name);
            // the path and filename for the telemetry type.
            var directory = Environment.CurrentDirectory;
            var fileName = $"{telemetry.GetType().Name}.json";
            var fullPath = Path.Combine(directory, fileName);
            // check if the file exists and returns a new instance of telemetry if it doesn't
            return File.Exists(fullPath) is false
                ? new T()
                : await _serializationService.DeserializeAsync<T>(new DirectoryInfo(directory), fileName);
        }
        /// <summary>
        ///     Updates or creates a telemetry file.
        /// </summary>
        /// <typeparam name="T">The <see cref="ITelemetry"/> to update or create.</typeparam>
        /// <param name="telemetry">The <typeparamref name="T"/> to update.</param>
        /// <returns><see cref="Task"/></returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="telemetry"/> is null.</exception>
        public async Task UpdateTelemetry<T>(T telemetry) where T : class, ITelemetry
        {
            if (telemetry is null)
                throw new ArgumentNullException(nameof(telemetry));

            _logger.LogInformation("Updating telemetry for {name}.", typeof(T).Name);
            // the path and filename for the telemetry type.
            var directory = Environment.CurrentDirectory;
            var fileName = $"{telemetry.GetType().Name}.json";
            var fullPath = Path.Combine(directory, fileName);

            var lastUpdated = File.GetLastWriteTime(fullPath);
            if (lastUpdated > DateTime.Now)
                throw new Exception("File on system is newer than the file information in the provided telemetry. Get updated telemetry and update as needed.");

            // let serialization service figure out the name of the file.
            await _serializationService.SerializeAsync<T>(telemetry, new DirectoryInfo(directory), fileName);
        }
    }
}
