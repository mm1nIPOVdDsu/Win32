namespace Win32.Common.Services.Registry
{
    /// <summary>
    ///     The registry root nodes of the system.
    /// </summary>
    public enum RegistryRoot
    {
        /// <summary>
        ///     The HKEY_CLASSES_ROOT registry root node.
        /// </summary>
        ClassesRoot,
        /// <summary>
        ///     The HKEY_CURRENT_USER registry root node.
        /// </summary>
        CurrentUser,
        /// <summary>
        ///     The HKEY_LOCAL_MACHINE registry root node.
        /// </summary>
        LocalMachine,
        /// <summary>
        ///     The HKEY_USERS registry root node.
        /// </summary>
        Users,
    }
}
