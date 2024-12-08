using System;

namespace Win32.Common.Services.Registry
{
    /// <summary>
    ///     Registry change event information.
    /// </summary>
    public class RegistryChangeEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RegistryChangeEventArgs"/> class.
        /// </summary>
        /// <param name="monitor">The <see cref="IRegistryMonitorService"/> instance that raised the event.</param>
        public RegistryChangeEventArgs(IRegistryMonitorService monitor)
        {
            if (monitor is null)
                throw new ArgumentNullException(nameof(monitor));

            Monitor = monitor;
        }

        /// <summary>
        ///     The <see cref="IRegistryMonitorService"/> instance that raised the event.
        /// </summary>
        public IRegistryMonitorService Monitor { get; }
        /// <summary>
        ///     An <see cref="System.Exception"/> object if an error occurred.
        /// </summary>
        public Exception? Exception { get; set; }
        /// <summary>
        ///     True if the service has stopped.
        /// </summary>
        public bool Stop { get; set; } = false;
    }
}
