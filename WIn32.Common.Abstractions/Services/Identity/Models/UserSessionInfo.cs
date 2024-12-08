namespace Win32.Common.Services.Identity
{
    /// <summary>
    ///     Holds basic information about a user session.
    /// </summary>
    public class UserSessionInfo
    {
        /// <summary>
        ///     The id of the session.
        /// </summary>
        public uint SessionId { get; set; }
        /// <summary>
        ///     The user name for the session.
        /// </summary>
        public string? UserName { get; set; } = "";
    }
}
