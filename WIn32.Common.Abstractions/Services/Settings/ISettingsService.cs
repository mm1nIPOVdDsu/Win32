using System;

namespace Win32.Common.Services.Settings
{
    /// <summary>
    ///     Service for creating, getting, and updating settings for <see cref="ISettings"/>.
    /// </summary>
    public interface ISettingsService<T> : IServiceBase, IDisposable
        where T : class, ISettings, new()
    {
        /// <summary>
        ///     The file name of the file on disk associated with <typeparamref name="T"/>.
        /// </summary>
        string FileName { get; }
        /// <summary>
        ///     The directory of the file on disk associated with <typeparamref name="T"/>.
        /// </summary>
        string Path { get; }
        /// <summary>
        ///     Settings information.
        /// </summary>
        T? Settings { get; set; }

        /// <summary>
        ///     TODO: Summary
        /// </summary>
        /// <returns></returns>
        T GetSettings();
        /// <summary>
        ///     Updates the settings file associated with <typeparamref name="T"/>.
        /// </summary>
        /// <remarks>
        ///     If Settings is null, a default instance is created.
        /// </remarks>
        void UpdateSettings();
    }
}
