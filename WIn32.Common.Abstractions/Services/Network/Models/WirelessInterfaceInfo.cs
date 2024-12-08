using System;

namespace Win32.Common.Services.Network
{
    /// <summary>
    ///     Information about a network adapter.
    /// </summary>
    public class WirelessInterfaceInfo
    {
        /// <summary>
        ///     The id of the interface.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        ///     The description of the interface.
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        ///     The state of the wireless network.
        /// </summary>
        public WirelessInterfaceState State { get; set; }   
    }
}
