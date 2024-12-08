using System;
using System.Security.Cryptography;
using System.Text;

using Microsoft.Extensions.Logging;

namespace Win32.Common.Services.Encryption
{
    /// <summary>
    ///     Service for managing RSA Encryption.
    /// </summary>
    public class RSAEncryptionService : IRSAEncryptionService
    {
        private readonly ILogger<RSAEncryptionService> _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RSAEncryptionService"/> class.
        /// </summary>
        /// <param name="logger">An instance of a <see cref="ILogger{RSAEncryptionService}"/>.</param>
        public RSAEncryptionService(ILogger<RSAEncryptionService> logger) => _logger = logger;

        /// <summary>
        ///     Generates a public and private key pair.
        /// </summary>
        /// <returns><see cref="RSAKey"/></returns>
        public RSAKey GenerateKey()
        {
            _logger.LogInformation("Generating RSA Keys.");
            var rSACryptoServiceProvider = new RSACryptoServiceProvider();
            var privateKey = Convert.ToBase64String(rSACryptoServiceProvider.ExportCspBlob(includePrivateParameters: true));
            var publicKey = Convert.ToBase64String(rSACryptoServiceProvider.ExportCspBlob(includePrivateParameters: false));

            return new RSAKey(privateKey, publicKey);
        }

        /// <summary>
        ///     Decrypts an RSA encrypted string.
        /// </summary>
        /// <param name="data">The encrypted data.</param>
        /// <param name="privateKey">The private key value.</param>
        /// <returns>A decrypted string.</returns>
        public string Decrypt(string data, string privateKey)
        {
            if (string.IsNullOrEmpty(data))
                throw new ArgumentNullException(nameof(data));
            if (string.IsNullOrEmpty(privateKey))
                throw new ArgumentNullException(nameof(privateKey));

            _logger.LogInformation("Encrypting data with provided key.");
            using (var rSACryptoServiceProvider = new RSACryptoServiceProvider())
            {
                rSACryptoServiceProvider.ImportCspBlob(Convert.FromBase64String(privateKey));
                var bytes = rSACryptoServiceProvider.Decrypt(Convert.FromBase64String(data), fOAEP: false);
                return Encoding.UTF8.GetString(bytes);
            }
        }

        /// <summary>
        ///     Encrypts a string.
        /// </summary>
        /// <param name="data">The string to encrypt.</param>
        /// <param name="publicKey">The public key used for encryption.</param>
        /// <returns>An encrypted string.</returns>
        public string Encrypt(string data, string publicKey)
        {
            if (string.IsNullOrEmpty(data))
                throw new ArgumentNullException(nameof(data));
            if (string.IsNullOrEmpty(publicKey))
                throw new ArgumentNullException(nameof(publicKey));

            _logger.LogInformation("Encrypting data with provided key.");
            using (var rSACryptoServiceProvider = new RSACryptoServiceProvider())
            {
                rSACryptoServiceProvider.ImportCspBlob(Convert.FromBase64String(publicKey));
                return Convert.ToBase64String(rSACryptoServiceProvider.Encrypt(Encoding.UTF8.GetBytes(data), fOAEP: false));
            }
        }
    }
}