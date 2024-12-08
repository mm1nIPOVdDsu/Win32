using System;
using System.Collections.Generic;

namespace Win32.Common.Services.Settings
{
    /// <summary>
    ///     Defines the settings for a user.
    /// </summary>
    public interface ISettings
    {
        /// <summary>
        ///     The unique ID for the settings.
        /// </summary>
        Guid UUID { get; }
        /// <summary>
        ///     The name of the user the settings apply to.
        /// </summary>
        string Username { get; }
        /// <summary>
        ///     The list of settings for a user.
        /// </summary>
        List<ISetting> Settings { get; }
        /// <summary>
        ///     The version of the settings schema.
        /// </summary>
        Version Version { get; }
    }

    /// <summary>
    ///     A single setting.
    /// </summary>
    public interface ISetting
    {
        /// <summary>
        ///     The name of the setting.
        /// </summary>
        public string Name { get; }
        /// <summary>
        ///     The setting value.
        /// </summary>
        public object Value { get; }
    }
}
