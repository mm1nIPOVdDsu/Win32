namespace Win32.Common.Services.WindowsService
{
    /// <summary>
    ///     The status of a service.
    /// </summary>
    public enum ServiceStatus : int
    {
        /// <summary>
        ///     The service is running.
        /// </summary>
        Running = 4,
        /// <summary>
        ///     The service is starting.
        /// </summary>
        StartPending = 2,
        /// <summary>
        ///     The service is stopped.
        /// </summary>
        Stopped = 1,
        /// <summary>
        ///     The service is stopping.
        /// </summary>
        StopPending = 3,
        /// <summary>
        ///     The service status is unknown.
        /// </summary>
        Unknown = 10
    }
}
