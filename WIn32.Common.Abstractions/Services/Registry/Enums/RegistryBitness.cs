namespace Win32.Common.Services.Registry
{
    /// <summary>
    ///     The x86 or x64 registry view.
    /// </summary>
    public enum RegistryBitness
    {
        /// <summary>
        ///     The x86/32-bit registry view.
        /// </summary>
        x86 = 512,
        /// <summary>
        ///     The x64/64-bit registry view.
        /// </summary>
        x64 = 256
    }
}
