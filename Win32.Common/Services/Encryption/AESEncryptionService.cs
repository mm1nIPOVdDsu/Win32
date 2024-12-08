using System;
using System.Security.Cryptography;
using System.Text;

using Microsoft.Extensions.Logging;

namespace Win32.Common.Services.Encryption
{
    /// <summary>
    ///     A service for encrypting and decrypting using AES certificates.
    /// </summary>
    public class AESEncryptionService : IAESEncryptionService
    {
        private readonly ILogger<AESEncryptionService> _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AESEncryptionService"/> class.
        /// </summary>
        /// <param name="logger">An instance of a <see cref="ILogger{AESEncryptionService}"/>.</param>
        public AESEncryptionService(ILogger<AESEncryptionService> logger) => _logger = logger;

        /// <summary>
        ///     Decrypts string data using the provided AES key.
        /// </summary>
        /// <param name="data">The string data to decrypt.</param>
        /// <param name="key">The string value of a private key.</param>
        /// <returns>The decrypted data as a string.</returns>
        /// <exception cref="ArgumentNullException">If data or key is null or empty.</exception>
        /// <exception cref="FormatException">If the key is not in UTF8 formatting.</exception>
        public string Decrypt(string data, string key)
        {
            _logger.LogInformation("Decrypting data with provided key.");
            if (string.IsNullOrEmpty(data))
                throw new ArgumentNullException(nameof(data));
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            using (var aes = Aes.Create())
            {
                var array = Encoding.UTF8.GetString(Convert.FromBase64String(key)).Split(',');
                if (array.Length != 2)
                {
                    throw new FormatException("Incorrect key format");
                }

                aes.IV = Convert.FromBase64String(array[0]);
                aes.Key = Convert.FromBase64String(array[1]);
                var cryptoTransform = aes.CreateDecryptor();
                var array2 = Convert.FromBase64CharArray(data.ToCharArray(), 0, data.Length);
                return Encoding.UTF8.GetString(cryptoTransform.TransformFinalBlock(array2, 0, array2.Length));
            }
        }
        /// <summary>
        ///     Encrypts a string using the provided key.
        /// </summary>
        /// <param name="data">The string to encrypt.</param>
        /// <param name="key">The key to use for encrypting.</param>
        /// <returns>An encrypted string.</returns>
        /// <exception cref="ArgumentNullException">If data or key is null or empty.</exception>
        /// <exception cref="FormatException">If the key is not in UTF8 formatting.</exception>
        public string Encrypt(string data, string key)
        {
            if (string.IsNullOrEmpty(data))
                throw new ArgumentNullException(nameof(data));
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            _logger.LogInformation("Encrypting data with provided key.");
            using (var aes = Aes.Create())
            {
                var array = Encoding.UTF8.GetString(Convert.FromBase64String(key)).Split(',');
                if (array.Length != 2)
                {
                    throw new FormatException("Incorrect key format");
                }

                aes.IV = Convert.FromBase64String(array[0]);
                aes.Key = Convert.FromBase64String(array[1]);
                var bytes = Encoding.UTF8.GetBytes(data);
                return Convert.ToBase64String(aes.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length));
            }
        }
        /// <summary>
        ///     Generates an AES key used for encrypting and decrypting.
        /// </summary>
        /// <returns>The string value of the key.</returns>
        public string GenerateKey()
        {
            _logger.LogInformation("Generating AES Keys.");
            var s = string.Empty;
            using (var aes = Aes.Create())
            {
                aes.GenerateIV();
                var str = Convert.ToBase64String(aes.IV);
                aes.GenerateKey();
                var str2 = Convert.ToBase64String(aes.Key);
                s = str + "," + str2;
            }
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
        }
    }
}
