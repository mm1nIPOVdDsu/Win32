using System;
using System.IO;
using System.Threading.Tasks;

namespace Win32.Common.Services.Encryption
{
    /// <summary>
    ///     Service for encrypting and decrypting data using asymmetric keys.
    /// </summary>
    public interface IAsymmetricalEncryptionService : IServiceBase
    {
        /// <summary>
        ///     Creates a public/private key pair in the provided directory.
        /// </summary>
        /// <param name="directory">The directory to save the key to.</param>
        /// <param name="overwrite">True if an existing key should be overwritten.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="directory"/> is null or empty.</exception>
        /// <exception cref="Exception">Thrown if keys are present in <paramref name="directory"/> and <paramref name="overwrite"/> is false.</exception>
        Task CreateKeys(string directory, bool overwrite = false);
        /// <summary>
        ///     Decrypts a file using the loaded keys.
        /// </summary>
        /// <param name="file">The <see cref="FileInfo"/> for the file to decrypt.</param>
        /// <exception cref="ArgumentNullException">Thrown when FileInfo is null.</exception>
        /// <exception cref="FileNotFoundException">Thrown if the specified file cannot be found.</exception>
        /// <exception cref="FileLoadException">Thrown if the file is empty.</exception>
        Task DecryptFile(FileInfo file);
        /// <summary>
        ///     Decrypts a string using keys that have been previously loaded.
        /// </summary>
        /// <param name="text">The string to decrypt.</param>
        /// <returns>A decrypted string.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="text"/>  is empty.</exception>
        /// <exception cref="Exception">Thrown if a private key has not been loaded.</exception>
        Task<string> DecryptText(string text);
        /// <summary>
        ///     Encrypts a file using keys that have been previously loaded.
        /// </summary>
        /// <param name="file">The <see cref="FileInfo"/> for the file to encrypt.</param>
        /// <exception cref="ArgumentNullException">Thrown if FileInfo is null.</exception>
        /// <exception cref="FileNotFoundException">Thrown if the specified file cannot be found.</exception>
        /// <exception cref="FileLoadException">Thrown if the file is empty.</exception>
        /// <exception cref="Exception">Thrown if a private key has not been loaded.</exception>
        Task EncryptFile(FileInfo file);
        /// <summary>
        ///     Encrypts a string using keys that have been previously loaded.
        /// </summary>
        /// <param name="text">The string to encrypt.</param>
        /// <returns>An encrypted string.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="text"/> is empty.</exception>
        /// <exception cref="Exception">Thrown if a private key has not been loaded.</exception>
        Task<string> EncryptText(string text);
        /// <summary>
        ///     Loads keys from a specified directory.
        /// </summary>
        /// <param name="directory">The directory where the keys will be saved.</param>
        /// <param name="overwriteExisting">If keys have already been loaded, should they be replaced.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="directory"/> is null or empty.</exception>
        /// <exception cref="DirectoryNotFoundException">Thrown if <paramref name="directory"/> cannot be found.</exception>
        /// <exception cref="Exception">Thrown if keys have already been loaded and <paramref name="overwriteExisting"/> is false.</exception>
        Task LoadKeys(string directory, bool overwriteExisting = false);
    }
}
