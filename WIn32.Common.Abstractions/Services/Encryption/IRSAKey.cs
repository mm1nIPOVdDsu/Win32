namespace Win32.Common.Services.Encryption
{
    /// <summary>
    ///     Represents an RSA encryption key.
    /// </summary>
    internal interface IRSAKey
    {
        /// <summary>
        ///     The private key.
        /// </summary>
        string PrivateKey { get; }
        /// <summary>
        ///     The public key.
        /// </summary>
        string PublicKey { get; }
    }
}
