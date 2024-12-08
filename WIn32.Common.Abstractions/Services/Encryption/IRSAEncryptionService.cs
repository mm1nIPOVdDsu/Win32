namespace Win32.Common.Services.Encryption
{
    /// <summary>
    ///     Service for managing RSA Encryption.
    /// </summary>
    public interface IRSAEncryptionService : IServiceBase
    {
        /// <summary>
        ///     Decrypts an RSA encrypted string.
        /// </summary>
        /// <param name="data">The encrypted data.</param>
        /// <param name="privateKey">The private key value.</param>
        /// <returns>A decrypted string.</returns>
        string Decrypt(string data, string privateKey);
        /// <summary>
        ///     Encrypts a string.
        /// </summary>
        /// <param name="data">The string to encrypt.</param>
        /// <param name="publicKey">The public key used for encryption.</param>
        /// <returns>An encrypted string.</returns>
        string Encrypt(string data, string publicKey);
        /// <summary>
        ///     Generates a public and private key pair.
        /// </summary>
        /// <returns><see cref="RSAKey"/></returns>
        RSAKey GenerateKey();
    }
}