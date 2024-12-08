using System;

namespace Win32.Common.Services.Encryption
{
    /// <summary>
    ///     A service for encrypting and decrypting using AES certificates.
    /// </summary>
    public interface IAESEncryptionService : IServiceBase
    {
        /// <summary>
        ///     Decrypts string data using the provided AES key.
        /// </summary>
        /// <param name="data">The string data to decrypt.</param>
        /// <param name="key">The string value of a private key.</param>
        /// <returns>The decrypted data as a string.</returns>
        /// <exception cref="ArgumentNullException">If data or key is null or empty.</exception>
        /// <exception cref="FormatException">If the key is not in UTF8 formatting.</exception>
        string Decrypt(string data, string key);
        /// <summary>
        ///     Encrypts a string using the provided key.
        /// </summary>
        /// <param name="data">The string to encrypt.</param>
        /// <param name="key">The key to use for encrypting.</param>
        /// <returns>An encrypted string.</returns>
        /// <exception cref="ArgumentNullException">If data or key is null or empty.</exception>
        /// <exception cref="FormatException">If the key is not in UTF8 formatting.</exception>
        string Encrypt(string data, string key);
        /// <summary>
        ///     Generates an AES key used for encrypting and decrypting.
        /// </summary>
        /// <returns>The string value of the key.</returns>
        string GenerateKey();
    }
}
