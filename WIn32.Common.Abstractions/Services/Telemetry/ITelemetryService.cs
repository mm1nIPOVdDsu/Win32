using System;
using System.Threading.Tasks;

namespace Win32.Common.Services.Telemetry
{
    /// <summary>
    ///     Provides services to create, get, and update <see cref="ITelemetry"/> for an assembly.
    /// </summary>
    public interface ITelemetryService : IServiceBase
    {
        /// <summary>
        ///     Gets telemetry from file.
        /// </summary>
        /// <typeparam name="T">The <see cref="ITelemetry"/> to update or create.</typeparam>
        /// <param name="telemetry">The <typeparamref name="T"/> to get.</param>
        /// <returns>An instance of <typeparamref name="T"/> from file.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="telemetry"/> is null.</exception>
        Task<T?> GetTelemetry<T>(T telemetry) where T : class, ITelemetry, new();
        /// <summary>
        ///     Updates or creates a telemetry file.
        /// </summary>
        /// <typeparam name="T">The <see cref="ITelemetry"/> to update or create.</typeparam>
        /// <param name="telemetry">The <typeparamref name="T"/> to update.</param>
        /// <returns><see cref="Task"/></returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="telemetry"/> is null.</exception>
        Task UpdateTelemetry<T>(T telemetry) where T : class, ITelemetry;
    }
}
