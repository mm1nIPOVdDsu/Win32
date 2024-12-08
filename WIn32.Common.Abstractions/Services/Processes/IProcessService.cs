namespace Win32.Common.Services.Processes
{
    /// <summary>
    ///     Service for interacting with processes.
    /// </summary>
    public interface IProcessService : IServiceBase
    {
        /// <summary>
        ///     Determines if a process is running with admin rights.
        /// </summary>
        /// <param name="processId">The id of the process.</param>
        /// <returns>True if the process is running with admin rights.</returns>
        bool IsAdminGroupMember(int processId);
    }
}
