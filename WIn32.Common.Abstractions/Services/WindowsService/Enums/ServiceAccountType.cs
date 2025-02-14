﻿namespace Win32.Common.Services.WindowsService
{
    /// <summary>
    ///     The service account type a service is running under.
    /// </summary>
    public enum ServiceAccountType
    {
        /// <summary>
        ///     An account that acts as a non-privileged user on the local computer, and presents
        ///     anonymous credentials to any remote server.
        /// </summary>
        LocalService = 0,
        /// <summary>
        ///     An account that provides extensive local privileges, and presents the computer's
        ///     credentials to any remote server.
        /// </summary>
        NetworkService = 1,
        /// <summary>
        ///     An account, used by the service control manager, that has extensive privileges
        ///     on the local computer and acts as the computer on the network.
        /// </summary>
        LocalSystem = 2,
        /// <summary>
        ///     An account defined by a specific user on the network. Specifying User for the
        ///     System.ServiceProcess.ServiceProcessInstaller.Account member causes the system
        ///     to prompt for a valid user name and password when the service is installed, unless
        ///     you set values for both the System.ServiceProcess.ServiceProcessInstaller.Username
        ///     and System.ServiceProcess.ServiceProcessInstaller.Password properties of your
        ///     System.ServiceProcess.ServiceProcessInstaller instance.
        /// </summary>
        User = 3
    }
}
