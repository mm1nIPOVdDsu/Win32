namespace Win32.Common.Services.EventLog
{
    /// <summary>
    ///     The type of event log entry of a message.
    /// </summary>
    public enum EventLogMessageType : int
    {
        /// <summary>
        ///     The event log entry will appear as an error message.
        /// </summary>
        Error = 1,
        /// <summary>
        ///     The event log entry will appear as a warning message.
        /// </summary>
        Warning = 2,
        /// <summary>
        ///     The event log entry will appear as an information message.
        /// </summary>
        Information = 4,
    }
}
