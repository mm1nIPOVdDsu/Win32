namespace Win32.Common.Services.Network
{
    /// <summary>
    ///     Defines the state of a wireless interface.
    /// </summary>
    public enum WirelessInterfaceState : uint
    {
        /// <summary>
        ///     The interface is not ready.
        /// </summary>
        NotReady = 0,
        /// <summary>
        ///     The interface is connected.
        /// </summary>
        Connected = 1,
        /// <summary>
        ///     A wireless ad-hoc network was formed.
        /// </summary>
        AdHocNetworkFormed = 2,
        /// <summary>
        ///     The interface is disconnecting.
        /// </summary>
        Disconnecting = 3,
        /// <summary>
        ///     The interface is disconnected.
        /// </summary>
        Disconnected = 4,
        /// <summary>
        ///     The interface is associating.
        /// </summary>
        Associating = 5,
        /// <summary>
        ///     The interface is discovering.
        /// </summary>
        Discovering = 6,
        /// <summary>
        ///     The interface is authenticating.
        /// </summary>
        Authenticating = 7,
    }
}
