using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Win32.Common.Services.Serialization;

using Microsoft.Extensions.Logging;

namespace Win32.Common.Services.Encryption
{
    /// <summary>
    ///     Service for encrypting and decrypting data using asymmetric keys.
    /// </summary>
    public class AsymmetricalEncryptionService : IAsymmetricalEncryptionService
    {
        //private readonly IAsymmetricalEncryptionInfo _asymmetricalEncryptionInfo;
        private readonly ILogger<AsymmetricalEncryptionService> _logger;
        private readonly ISerializationService _serializationService;
        private readonly Dictionary<KeyType, RSAKeyValue> _keys = new();
        //private readonly DirectoryInfo _keyFilesDirectory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsymmetricalEncryptionService"/> class.
        /// </summary>
        /// <param name="logger">An instance of a <see cref="ILogger{AsymmetricalEncryptionService}"/>.</param>
        /// <param name="serializationService">An instance of the <see cref="ISerializationService"/>.</param>
        public AsymmetricalEncryptionService(ILogger<AsymmetricalEncryptionService> logger, ISerializationService serializationService)
        {
            _serializationService = serializationService;
            _logger = logger;
        }

        /// <summary>
        ///     Creates a public/private key pair in the provided directory.
        /// </summary>
        /// <param name="directory">The directory to save the key to.</param>
        /// <param name="overwrite">True if an existing key should be overwritten.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="directory"/> is null or empty.</exception>
        /// <exception cref="Exception">Thrown if keys are present in <paramref name="directory"/> and <paramref name="overwrite"/> is false.</exception>
        public async Task CreateKeys(string directory, bool overwrite = false)
        {
            if (string.IsNullOrEmpty(directory))
                throw new ArgumentNullException(nameof(directory));
            if (Directory.Exists(directory) is false)
                Directory.CreateDirectory(directory);
            if ((File.Exists(Path.Combine(directory, $"{KeyType.Private}Key.xml")) is true || File.Exists(Path.Combine(directory, $"{KeyType.Public}Key.xml")) is true) && overwrite is false)
                throw new Exception($"Key files already exist in {directory}.");

            _logger.LogInformation("Creating keys and saving to {directory}.", directory);
            using (var RSA = new RSACryptoServiceProvider())
            {
                var publicKeyString = RSA.ToXmlString(false);
                var privateKeyString = RSA.ToXmlString(true);

                var keys = new Dictionary<KeyType, RSAKeyValue>()
                {
                    {
                        KeyType.Public,
                        _serializationService.Deserialize<RSAKeyValue>(publicKeyString, SerializationFormat.Json)!
                    },
                    {
                        KeyType.Private,
                        _serializationService.Deserialize<RSAKeyValue>(privateKeyString, SerializationFormat.Json)!
                    }
                };

                await SaveKeys(keys, directory);
            }
        }
        /// <summary>
        ///     Loads keys from a specified directory.
        /// </summary>
        /// <param name="directory">The directory where the keys will be saved.</param>
        /// <param name="overwriteExisting">If keys have already been loaded, should they be replaced.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="directory"/> is null or empty.</exception>
        /// <exception cref="DirectoryNotFoundException">Thrown if <paramref name="directory"/> cannot be found.</exception>
        /// <exception cref="Exception">Thrown if keys have already been loaded and <paramref name="overwriteExisting"/> is false.</exception>
        public async Task LoadKeys(string directory, bool overwriteExisting = false)
        {
            if (string.IsNullOrEmpty(directory))
                throw new ArgumentNullException(nameof(directory));
            if (Directory.Exists(directory) is false)
                throw new DirectoryNotFoundException(directory);
            if (_keys != null && overwriteExisting is false)
                throw new Exception("Keys have already been loaded.");

            _logger.LogInformation("Loading keys from {directory}.", directory);
            var response = new Dictionary<KeyType, RSAKeyValue>();
            if (File.Exists(Path.Combine(directory, $"{KeyType.Public}Key.xml")) is true)
            {
                var keyInfo = await _serializationService.DeserializeAsync<RSAKeyValue>(new DirectoryInfo(directory), $"{KeyType.Public}Key.xml", SerializationFormat.Xml);
                if (keyInfo is null)
                    throw new Exception("Could not load key from file.");

                response.Add(KeyType.Public, keyInfo);
            }
            if (File.Exists(Path.Combine(directory, $"{KeyType.Private}Key.xml")) is true)
            {
                var keyInfo = _serializationService.Deserialize<RSAKeyValue>(new DirectoryInfo(directory), $"{KeyType.Private}Key.xml", SerializationFormat.Xml);
                if (keyInfo is null)
                    throw new Exception("Could not load key from file.");

                response.Add(KeyType.Private, keyInfo);
            }
        }

        /// <summary>
        ///     Decrypts a file using the loaded keys.
        /// </summary>
        /// <param name="file">The <see cref="FileInfo"/> for the file to decrypt.</param>
        /// <exception cref="ArgumentNullException">Thrown when FileInfo is null.</exception>
        /// <exception cref="FileNotFoundException">Thrown if the specified file cannot be found.</exception>
        /// <exception cref="FileLoadException">Thrown if the file is empty.</exception>
        public async Task DecryptFile(FileInfo file)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file), "File info cannot be null.");
            if (file.Exists is false)
                throw new FileNotFoundException($"{file.Name} is not in the directory {file.DirectoryName}.");
            if (file.Length is 0)
                throw new FileLoadException("File cannot be empty.");

            _logger.LogInformation("Decrypting the file {FullName}.", file.FullName);
            var fileText = File.ReadAllText(file.FullName);
            var decryptedText = await DecryptText(fileText);

            await File.WriteAllTextAsync(file.FullName, decryptedText);
        }
        /// <summary>
        ///     Decrypts a string using keys that have been previously loaded.
        /// </summary>
        /// <param name="text">The string to decrypt.</param>
        /// <returns>A decrypted string.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="text"/>  is empty.</exception>
        /// <exception cref="Exception">Thrown if a private key has not been loaded.</exception>
        public async Task<string> DecryptText(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text), "Text to decrypt cannot be null or empty.");
            if (_keys.ContainsKey(KeyType.Private) is false)
                throw new Exception("Private key is required to decrypt files.");

            var keyText = await _serializationService.SerializeAsync(_keys[KeyType.Private], SerializationFormat.Xml);
            _logger.LogInformation("Decrypting text using provided keys.");
            using (var RSAalg = new RSACryptoServiceProvider())
            {
                RSAalg.FromXmlString(keyText);

                var dataToDecrypt = Convert.FromBase64String(text);
                var blockSize = (RSAalg.KeySize / 8) - 11;
                if (dataToDecrypt.Length <= blockSize)
                {
                    var encrypted = RSAalg.Decrypt(dataToDecrypt, false);
                    return Convert.ToBase64String(encrypted);
                }

                var modulusSize = blockSize + 11;
                using (var msin = new MemoryStream(dataToDecrypt))
                using (var msout = new MemoryStream(modulusSize))
                {
                    var buffer = new byte[modulusSize];
                    int bytesRead;
                    do
                    {
                        bytesRead = msin.Read(buffer, 0, modulusSize);
                        if (bytesRead > 0)
                        {
                            var decrypted = RSAalg.Decrypt(buffer, false);
                            msout.Write(decrypted, 0, decrypted.Length);
                            Array.Clear(decrypted, 0, decrypted.Length);
                        }
                    } while (bytesRead > 0);

                    var decryptedBytes = msout.ToArray();
                    var decryptedText = Encoding.ASCII.GetString(decryptedBytes);
                    return decryptedText;
                }
            }
        }

        /// <summary>
        ///     Encrypts a file using keys that have been previously loaded.
        /// </summary>
        /// <param name="file">The <see cref="FileInfo"/> for the file to encrypt.</param>
        /// <exception cref="ArgumentNullException">Thrown if FileInfo is null.</exception>
        /// <exception cref="FileNotFoundException">Thrown if the specified file cannot be found.</exception>
        /// <exception cref="FileLoadException">Thrown if the file is empty.</exception>
        /// <exception cref="Exception">Thrown if a private key has not been loaded.</exception>
        public async Task EncryptFile(FileInfo file)
        {
            if (file is null)
                throw new Exception("File cannot be null.");
            if (file.Exists is false)
                throw new Exception($"{file.Name} is not in the directory {file.DirectoryName}.");
            if (file.Length is 0)
                throw new Exception("File cannot be empty.");
            if (_keys.ContainsKey(KeyType.Private) is false)
                throw new Exception("Private key is required to encrypt files.");

            _logger.LogInformation("Encrypting the file {FullName}.", file.FullName);
            var fileText = File.ReadAllText(file.FullName);
            var encryptedText = await EncryptText(fileText);

            await File.WriteAllTextAsync(file.FullName, encryptedText);
        }
        /// <summary>
        ///     Encrypts a string using keys that have been previously loaded.
        /// </summary>
        /// <param name="text">The string to encrypt.</param>
        /// <returns>An encrypted string.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="text"/> is empty.</exception>
        /// <exception cref="Exception">Thrown if a private key has not been loaded.</exception>
        public async Task<string> EncryptText(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new Exception("Text cannot be empty.");
            if (_keys.ContainsKey(KeyType.Private) == false)
                throw new Exception("Private key is required to decrypt files.");

            //var keyText = string.Empty;
            //if (_keys.ContainsKey(KeyType.Public) == true)
            //    keyText = await _serializationService.SerializeAsync(_keys[KeyType.Public], SerializationFormat.Xml);
            //else if (_keys.ContainsKey(KeyType.Private) == true)
            //    keyText = await _serializationService.SerializeAsync(_keys[KeyType.Private], SerializationFormat.Xml);
            //else
            //    throw new Exception("At least one key is needed to decrypt data.");

            byte[] dataToEncrypt;
            if (IsBase64(text) is false)
            {
                var encoded = Encoding.Default.GetBytes(text);
                var asdf = Convert.ToBase64String(encoded);
                dataToEncrypt = Convert.FromBase64String(asdf);
            }
            else
            {
                dataToEncrypt = Convert.FromBase64String(text);
            }

            _logger.LogInformation("Encrypting text using provided keys.");
            var privateKeyText = await _serializationService.SerializeAsync(_keys[KeyType.Private], SerializationFormat.Xml);
            using (var RSAalg = new RSACryptoServiceProvider())
            {
                RSAalg.FromXmlString(privateKeyText);
                var blockSize = (RSAalg.KeySize / 8) - 11;
                if (dataToEncrypt.Length <= blockSize)
                {
                    var encrypted = RSAalg.Encrypt(dataToEncrypt, false);
                    return Convert.ToBase64String(encrypted);
                }

                var modulusSize = blockSize;
                using (var msin = new MemoryStream(dataToEncrypt))
                using (var msout = new MemoryStream(modulusSize))
                {
                    var buffer = new byte[modulusSize];
                    int bytesRead;
                    do
                    {
                        bytesRead = msin.Read(buffer, 0, modulusSize);
                        if (bytesRead > 0)
                        {
                            var encrypted = RSAalg.Encrypt(buffer, false);
                            msout.Write(encrypted, 0, encrypted.Length);
                            Array.Clear(encrypted, 0, encrypted.Length);
                        }
                    } while (bytesRead > 0);

                    var encryptedBytes = msout.ToArray();
                    var encryptedText = Convert.ToBase64String(encryptedBytes);
                    return encryptedText;
                }
            }
        }

        #region Private Methods
        /// <summary>
        ///     Loads keys in <paramref name="directoryInfo"/>.
        /// </summary>
        /// <param name="directoryInfo">The <see cref="DirectoryInfo"/> of the directory that contains the keys.</param>
        /// <returns>A <see cref="Dictionary{TKey, TValue}"/> where TKey is <see cref="KeyType"/> and TValue is <see cref="RSAKeyValue"/>.</returns>
        /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist.</exception>
        private async Task<Dictionary<KeyType, RSAKeyValue>> LoadKeys(DirectoryInfo directoryInfo)
        {
            if (directoryInfo is null)
                throw new DirectoryNotFoundException("Directory info cannot be null.");

            var directory = directoryInfo.FullName;
            var response = new Dictionary<KeyType, RSAKeyValue>();

            _logger.LogDebug("Loading keys from {FullName}.", directoryInfo.FullName);
            if (File.Exists(Path.Combine(directory, $"{KeyType.Public}Key.xml")) is true)
            {
                var keyInfo = await _serializationService.DeserializeAsync<RSAKeyValue>(directoryInfo, $"{KeyType.Public}Key", SerializationFormat.Xml);
                if (keyInfo is null)
                    throw new Exception("Could not load key from file.");

                response.Add(KeyType.Public, keyInfo);
            }
            if (File.Exists(Path.Combine(directory, $"{KeyType.Private}Key.xml")) is true)
            {
                var keyInfo = await _serializationService.DeserializeAsync<RSAKeyValue>(directoryInfo, $"{KeyType.Private}Key", SerializationFormat.Xml);
                if (keyInfo is null)
                    throw new Exception("Could not load key from file.");

                response.Add(KeyType.Private, keyInfo);
            }
            return response;
        }
        /// <summary>
        ///     Saves a key pair to the directory at <paramref name="path"/>.
        /// </summary>
        /// <param name="keys">The <see cref="Dictionary{TKey, TValue}"/> where TKey is <see cref="KeyType"/> and TValue is <see cref="RSAKeyValue"/>.</param>
        /// <param name="path">The directory to save the keys to.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="keys"/> does not contain any keys.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="path"/> is null or empty.</exception>
        private async Task SaveKeys(Dictionary<KeyType, RSAKeyValue> keys, string path)
        {
            if (keys.Any() is false)
                throw new ArgumentException("Key values cannot be empty.");
            if (string.IsNullOrEmpty(Path.GetExtension(path)) is false)
                throw new ArgumentNullException(nameof(path));

            _logger.LogDebug("Saving keys to {path}.", path);
            var directoryInfo = new DirectoryInfo(path);
            if (directoryInfo.Exists is false)
                directoryInfo.Create();

            foreach (var key in keys)
            {
                await _serializationService.SerializeAsync(key.Value, directoryInfo, $"{key.Key}Key", SerializationFormat.Xml);
            }
        }
        /// <summary>
        ///     Determines is a string is a Base64 encoded string.
        /// </summary>
        /// <param name="base64String">The string to validate.</param>
        /// <returns>True if the string is Base64 encoded.</returns>
        private bool IsBase64(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
                return false;

            _logger.LogDebug("Checking if string is Base64 encoded.");
            // Credit: https://stackoverflow.com/users/794764/oybek
            if (string.IsNullOrEmpty(base64String) || base64String.Length % 4 is not 0 || base64String.Contains(' ') || base64String.Contains('\t') || base64String.Contains('\r') || base64String.Contains('\n'))
                return false;

            try { Convert.FromBase64String(base64String); }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error converting Base-64 string to byte array.");
                return false;
            }
            return true;
        }
        #endregion
    }
}