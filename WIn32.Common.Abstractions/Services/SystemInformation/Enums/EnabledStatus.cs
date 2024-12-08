namespace Win32.Common.Services.SystemInformation
{
    /// <summary>
    ///     The enabled state of an item.
    /// </summary>
    public enum EnabledStatus : byte
    {
        /// <summary>
        ///     The state is unknown.
        /// </summary>
        Unknown = 0,
        /// <summary>
        ///     The item is enabled.
        /// </summary>
        Enabled = 1,
        /// <summary>
        ///     The item is disabled.
        /// </summary>
        Disabled = 2,
    }
}
