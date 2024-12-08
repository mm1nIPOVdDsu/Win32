namespace Win32.Common.Services.Encryption
{
    /// <summary>
    ///     Represents an RSA encryption key.
    /// </summary>
    public class RSAKey : IRSAKey
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RSAKey"/> class.
        /// </summary>
        /// <param name="privateKey">The private key value.</param>
        /// <param name="publicKey">The public key value.</param>
        public RSAKey(string privateKey, string publicKey)
        {
            PrivateKey = privateKey;
            PublicKey = publicKey;
        }

        /// <summary>
        ///     The private key.
        /// </summary>
        public string PrivateKey { get; private set; }
        /// <summary>
        ///     The public key.
        /// </summary>
        public string PublicKey { get; private set; }
    }
}
