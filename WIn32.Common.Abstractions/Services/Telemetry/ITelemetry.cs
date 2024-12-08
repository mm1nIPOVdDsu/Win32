using System;
using System.Text.Json.Serialization;

namespace Win32.Common.Services.Telemetry
{
    /// <summary>
    ///     Definition for telemetry for an application/assembly.
    /// </summary>
    public interface ITelemetry
    {
        /// <summary>
        ///     The <see cref="DateTime"/> the telemetry was retrieved from file.
        /// </summary>
        /// <remarks>
        ///     This is to make sure we're not overwriting a newer file.
        /// </remarks>
        [JsonIgnore]
        DateTime TimeRetreived => DateTime.Now;
        /// <summary>
        ///     The version of the telemetry schema.
        /// </summary>
        string Version { get; }
        /// <summary>
        ///     The <see cref="DateTime"/> the telemetry was synchronized to the
        ///     server.
        /// </summary>
        DateTime SyncDate { get; }
    }
}
