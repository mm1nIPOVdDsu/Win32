namespace Win32.Common.Services.Network
{
    /// <summary>
    ///     Contains information about an available wireless network.
    /// </summary>
    public class WirelessNetworkInfo
    {
        /// <summary>
        ///     Contains the profile name associated with the network. If the network does not have a profile, this member will be empty.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        ///     The SSID of the visible wireless network.
        /// </summary>
        public string? SSID { get; set; }
        /// <summary>
        ///     Indicates whether the network is connectable or not. If set to TRUE, the network is connectable, otherwise the network cannot be connected to.
        /// </summary>
        public bool Connectable { get; set; }
        /// <summary>
        ///     A value that indicates why a network cannot be connected to. This member is only valid when Connectable is FALSE.
        /// </summary>
        public string? NotConnectableReason { get; set; }
        /// <summary>
        ///     A percentage value that represents the signal quality of the network. A value of 0 implies an actual RSSI signal strength of -100 dbm. A value of 100 implies an actual RSSI signal strength of -50 dbm.
        /// </summary>
        public uint SignalQuality { get; set; } = 0;
        /// <summary>
        ///     Indicates whether security is enabled on the network.
        /// </summary>
        public bool SecurityEnabled { get; set; }
        /// <summary>
        ///     The default authentication algorithm used to join this network.
        /// </summary>
        public string? DefaultAuthAlgorithm { get; set; }
        /// <summary>
        ///     The default cipher algorithm to be used when joining this network.
        /// </summary>
        public string? DefaultCipherAlgorithm { get; set; }
    }
}
