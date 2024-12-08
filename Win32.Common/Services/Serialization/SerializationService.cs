using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

using Win32.Common.Abstractions.Exceptions;

using Microsoft.Extensions.Logging;

namespace Win32.Common.Services.Serialization
{
    /// <summary>
    ///     Serializes strings and files, to and from JSON or XML formatting.
    /// </summary>
    public class SerializationService : ISerializationService
    {
        private readonly ILogger<SerializationService> _loggingService;
        private readonly JsonSerializerOptions defaultOptions = new()
        {
            WriteIndented = true,
            IgnoreReadOnlyProperties = false
        };

        /// <summary>
        ///     Initializes a new instance of the <see cref="SerializationService"/> class.
        /// </summary>
        /// <param name="loggingService">An instance of a <see cref="ILogger{SerializationService}"/>.</param>
        public SerializationService(ILogger<SerializationService> loggingService) => _loggingService = loggingService;

        /// <summary>
        ///     Deserializes a string into an instance of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The string value to deserialize into <typeparamref name="T"/>.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the string.</param>
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        public T? Deserialize<T>(string value, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            _loggingService.LogDebug("Deserializing {name}.", typeof(T).Name);
            // set the serializer options
            serializerOptions ??= defaultOptions;
            if (formatting == SerializationFormat.Json)
            {
                return JsonSerializer.Deserialize<T>(value, serializerOptions);
            }
            else
            {
                var serializer = new XmlSerializer(typeof(T));
                using (var stringReader = new StringReader(value))
                using (var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings() { }))
                {
                    return (T?)serializer.Deserialize(xmlReader);
                }
            }
        }
        /// <summary>
        ///     Deserializes a file to an instance of <typeparamref name="T"/>. 
        /// </summary>
        /// <remarks>
        ///     The file name is the type name of T combined with the formatting as the file extension. So if T was 
        ///     of type Test and formatting was JSON, the file expected would be Test.json.
        /// </remarks>
        /// <typeparam name="T">The object type to deserialize to.</typeparam>
        /// <param name="directory">The directory where the file exists.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the file.</param> 
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        /// <exception cref="DirectoryNotFoundException">If the provided directory does not exist.</exception>
        /// <exception cref="DirectoryNotFoundException">If the file cannot be found in the directory provided.</exception>
        public T? Deserialize<T>(DirectoryInfo directory, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null) =>
            Deserialize<T>(directory, typeof(T).Name, false, formatting, serializerOptions);
        /// <summary>
        ///     Deserializes a file to an instance of <typeparamref name="T"/>. 
        /// </summary>
        /// <typeparam name="T">The object type to deserialize to.</typeparam>
        /// <param name="directory">The directory where the file exists.</param>
        /// <param name="fileName">The file name of the file to deserialize.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the file.</param>
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        public T? Deserialize<T>(DirectoryInfo directory, string fileName, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null) =>
            Deserialize<T>(directory, fileName, false, formatting, serializerOptions);
        /// <summary>
        ///     Deserializes a file to an instance of <typeparamref name="T"/>. 
        /// </summary>
        /// <typeparam name="T">The object type to deserialize to.</typeparam>
        /// <param name="directory">The directory where the file exists.</param>
        /// <param name="fileName">The file name of the file to deserialize.</param>
        /// <param name="isEncrypted">Reserved for future use. True if the file is encrypted.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the file.</param> 
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        public T? Deserialize<T>(DirectoryInfo directory, string fileName, bool isEncrypted = false, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null)
        {
            if (directory is null)
                throw new ArgumentNullException(nameof(directory));

            if (!directory.Exists)
            {
                _loggingService.LogDebug("{directory} does not exist. Creating the directory.", directory.Name);
                Directory.CreateDirectory(directory.FullName);
            }
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = typeof(T).Name;
                _loggingService.LogDebug("File name is empty, setting to {fileName}.", fileName);
            }
            var extension = Path.GetExtension(fileName).Replace(".", string.Empty);
            if (string.IsNullOrEmpty(extension))
            {
                extension = formatting.ToString().ToLower();
                _loggingService.LogDebug("Setting the extension of {fileName} to {extension}.", fileName, extension);
                fileName = $"{fileName}.{extension}";
            }
            if (!extension.Equals(SerializationFormat.Xml.ToString(), StringComparison.InvariantCultureIgnoreCase) && !extension.Equals(SerializationFormat.Json.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                throw new InvalidFileExtensionException($"The file extension {extension} is not a valid extension for the serialization service. Please use one of the extension from the SerializationFormat enum.");
            }

            var fileFullName = $"{directory.FullName}\\{fileName}";
            if (!File.Exists(fileFullName))
            {
                _loggingService.LogDebug("{fileFullName} does not exist, returning default {name}.", fileFullName, typeof(T).Name);
                return default;
            }

            // using File.Encrypt only encrypts the underlying file so opening the file will still show clear text. Other users will be unable to view the file however.
            //if(isEncrypted)
            //{
            //    _logger.Debug($"Decrypting {fileFullName}.");
            //    File.Decrypt(fileFullName);
            //}

            _loggingService.LogDebug("Loading the file {fileFullName}.", fileFullName);
            var contents = File.ReadAllText($"{fileFullName}");

            //if(isEncrypted)
            //{
            //    _logger.Debug($"Re-encrypting {fileFullName}.");
            //    File.Encrypt(fileFullName);
            //}

            // set the serializer options
            serializerOptions ??= defaultOptions;
            var response = Deserialize<T>(contents, formatting, serializerOptions);
            return response;
        }

        /// <summary>
        ///     Deserializes a string into an instance of an object.
        /// </summary>
        /// <param name="value">The string value to deserialize into an object.</param>
        /// <param name="type">The type of object to deserialize to.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the string.</param>
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        /// <returns>An instance of an object.</returns>
        public object? Deserialize(string value, Type type, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            _loggingService.LogDebug("Deserializing {name}.", type.Name);
            // set the serializer options
            serializerOptions ??= defaultOptions;
            if (formatting == SerializationFormat.Json)
            {
                return JsonSerializer.Deserialize(value, type, serializerOptions);
            }
            else
            {
                var serializer = new XmlSerializer(typeof(object));
                using (var stringReader = new StringReader(value))
                using (var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings() { }))
                {
                    return serializer.Deserialize(xmlReader);
                }
            }
        }

        /// <summary>
        ///     Asynchronously deserializes a string into an instance of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The string value to deserialize into <typeparamref name="T"/>.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the string.</param>
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        public Task<T?> DeserializeAsync<T>(string value, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null) =>
            Task.Factory.StartNew(() => Deserialize<T>(value, formatting, serializerOptions));
        /// <summary>
        ///     Asynchronously deserializes a file to an instance of <typeparamref name="T"/>. 
        /// </summary>
        /// <remarks>
        ///     The file name is the type name of T combined with the formatting as the file extension. So if T was 
        ///     of type Test and formatting was JSON, the file expected would be Test.json.
        /// </remarks>
        /// <typeparam name="T">The object type to deserialize to.</typeparam>
        /// <param name="directory">The directory where the file exists.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the file.</param> 
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        /// <exception cref="DirectoryNotFoundException">If the provided directory does not exist.</exception>
        /// <exception cref="DirectoryNotFoundException">If the file cannot be found in the directory provided.</exception>
        public Task<T?> DeserializeAsync<T>(DirectoryInfo directory, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null) =>
            Task.Factory.StartNew(() => Deserialize<T>(directory, formatting, serializerOptions));
        /// <summary>
        ///     Asynchronously deserializes a file to an instance of <typeparamref name="T"/>. 
        /// </summary>
        /// <typeparam name="T">The object type to deserialize to.</typeparam>
        /// <param name="directory">The directory where the file exists.</param>
        /// <param name="fileName">The file name of the file to deserialize.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the file.</param> 
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        public Task<T?> DeserializeAsync<T>(DirectoryInfo directory, string fileName, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null) =>
            Task.Factory.StartNew(() => Deserialize<T>(directory, fileName, formatting, serializerOptions));
        /// <summary>
        ///     Asynchronously deserializes a file to an instance of <typeparamref name="T"/>. 
        /// </summary>
        /// <typeparam name="T">The object type to deserialize to.</typeparam>
        /// <param name="directory">The directory where the file exists.</param>
        /// <param name="fileName">The file name of the file to deserialize.</param>
        /// <param name="isEncrypted">Reserved for future use. True if the file is encrypted.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the file.</param> 
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        public Task<T?> DeserializeAsync<T>(DirectoryInfo directory, string fileName, bool isEncrypted = false, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null) =>
            Task.Factory.StartNew(() => Deserialize<T>(directory, fileName, isEncrypted, formatting, serializerOptions));
        /// <summary>
        ///     Asynchronously deserializes a string into an instance of an object.
        /// </summary>
        /// <param name="value">The string value to deserialize into an object.</param>
        /// <param name="type">The type of object to deserialize to.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the string.</param>
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        /// <returns>An instance of an object.</returns>
        public Task<object?> DeserializeAsync(string value, Type type, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null) =>
            Task.Factory.StartNew(() => Deserialize(value, type, formatting, serializerOptions));

        /// <summary>
        ///     Serializes an object into a string.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the output string.</param>
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        /// <returns>A string formatted into the type defined by <see cref="SerializationFormat"/>.</returns>
        public string Serialize<T>(T value, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            // set the serializer options
            serializerOptions ??= defaultOptions;

            _loggingService.LogDebug("Serializing {name}.", typeof(T).Name);
            if (formatting == SerializationFormat.Json)
            {
                return JsonSerializer.Serialize(value, serializerOptions);
            }
            else
            {
                var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                var serializer = new XmlSerializer(value.GetType());
                var settings = new XmlWriterSettings
                {
                    Indent = false,
                    OmitXmlDeclaration = true
                };

                using (var stream = new StringWriter())
                using (var writer = XmlWriter.Create(stream, settings))
                {
                    serializer.Serialize(writer, value, emptyNamespaces);
                    return stream.ToString();
                }
            }
        }
        /// <summary>
        ///     Serializes an instance of <typeparamref name="T"/> to a file. If the file already exists, it will be overwritten.
        /// </summary>
        /// <remarks>
        ///     The file name is the type name of T combined with the formatting as the file extension. So if T was 
        ///     of type Test and formatting was JSON, the file expected would be Test.json.
        /// </remarks>
        /// <typeparam name="T">The object type to serialize to a file.</typeparam>
        /// <param name="value">An instance of <typeparamref name="T"/>.</param>
        /// <param name="directory">The directory to save the file to.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the output file.</param>
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        public void Serialize<T>(T value, DirectoryInfo directory, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null) =>
            Serialize<T>(value, directory, typeof(T).Name, false, formatting, serializerOptions);
        /// <summary>
        ///     Serializes an instance of <typeparamref name="T"/> to a file. If the file already exists, it will be overwritten.
        /// </summary>
        /// <typeparam name="T">The object type to serialize to a file.</typeparam>
        /// <param name="value">An instance of <typeparamref name="T"/>.</param>
        /// <param name="directory">The directory to save the file to.</param>
        /// <param name="fileName">The file name of the file to serialize.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the output file.</param>
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        public void Serialize<T>(T value, DirectoryInfo directory, string fileName, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null) =>
            Serialize<T>(value, directory, fileName, false, formatting, serializerOptions);
        /// <summary>
        ///     Serializes an instance of <typeparamref name="T"/> to a file. If the file already exists, it will be overwritten.
        /// </summary>
        /// <typeparam name="T">The object type to serialize to a file.</typeparam>
        /// <param name="value">An instance of <typeparamref name="T"/>.</param>
        /// <param name="directory">The directory to save the file to.</param>
        /// <param name="fileName">The file name of the file to serialize.</param>
        /// <param name="encrypt">True to encrypt the file so only the logged in user profile can access. The file is decrypted when the user is logged in.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the output file.</param>
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        public void Serialize<T>(T value, DirectoryInfo directory, string fileName, bool encrypt = false, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));
            if (directory is null)
                throw new ArgumentNullException(nameof(directory));

            if (!directory.Exists)
            {
                _loggingService.LogDebug("Creating the directory {fullName}.", directory.FullName);
                directory.Create();
            }
            var extension = Path.GetExtension(fileName).Replace(".", string.Empty);
            if (string.IsNullOrEmpty(extension))
            {
                extension = formatting.ToString().ToLower();
                _loggingService.LogDebug("Setting the extension on {fileName} to {extension}.", fileName, extension);
                fileName = $"{fileName}.{extension}";
            }
            if (!extension.Equals(SerializationFormat.Xml.ToString(), StringComparison.InvariantCultureIgnoreCase) && !extension.Equals(SerializationFormat.Json.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                throw new InvalidFileExtensionException($"The file extension {extension} is not a valid extension for the serialization service. Please use one of the extension from the SerializationFormat enum.");
            }

            // set the serializer options
            serializerOptions ??= defaultOptions;
            var stringValue = Serialize(value, formatting, serializerOptions);
            _loggingService.LogDebug("Writing {fileName} to {fullName}.", fileName, directory.FullName);
            File.WriteAllText($"{directory.FullName}\\{fileName}", stringValue);

            //if(encrypt)
            //{
            //    _logger.Debug($"Encrypting the file {fileName}.");
            //    File.Encrypt($"{directory.FullName}\\{fileName}");
            //}
        }

        /// <summary>
        ///     Asynchronously serializes an object into a string.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the output string.</param>
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        /// <returns>A string formatted into the type defined by <see cref="SerializationFormat"/>.</returns>
        public Task<string> SerializeAsync<T>(T value, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null) =>
            Task.Factory.StartNew(() => Serialize(value, formatting, serializerOptions));
        /// <summary>
        ///     Asynchronously serializes an instance of <typeparamref name="T"/> to a file. If the file already exists, it will be overwritten.
        /// </summary>
        /// <remarks>
        ///     The file name is the type name of T combined with the formatting as the file extension. So if T was 
        ///     of type Test and formatting was JSON, the file expected would be Test.json.
        /// </remarks>
        /// <typeparam name="T">The object type to serialize to a file.</typeparam>
        /// <param name="value">An instance of <typeparamref name="T"/>.</param>
        /// <param name="directory">The directory to save the file to.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the output file.</param>
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        public Task SerializeAsync<T>(T value, DirectoryInfo directory, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null) =>
            Task.Factory.StartNew(() => Serialize(value, directory, formatting));
        /// <summary>
        ///     Asynchronously serializes an instance of <typeparamref name="T"/> to a file. If the file already exists, it will be overwritten.
        /// </summary>
        /// <typeparam name="T">The object type to serialize to a file.</typeparam>
        /// <param name="value">An instance of <typeparamref name="T"/>.</param>
        /// <param name="directory">The directory to save the file to.</param>
        /// <param name="fileName">The file name of the file to serialize.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the output file.</param>
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        public Task SerializeAsync<T>(T value, DirectoryInfo directory, string fileName, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null) =>
            Task.Factory.StartNew(() => Serialize(value, directory, fileName, formatting, serializerOptions));
        /// <summary>
        ///     Asynchronously serializes an instance of <typeparamref name="T"/> to a file. If the file already exists, it will be overwritten.
        /// </summary>
        /// <typeparam name="T">The object type to serialize to a file.</typeparam>
        /// <param name="value">An instance of <typeparamref name="T"/>.</param>
        /// <param name="directory">The directory to save the file to.</param>
        /// <param name="fileName">The file name of the file to serialize.</param>
        /// <param name="encrypt">True to encrypt the file so only the logged in user profile can access. The file is decrypted when the user is logged in.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the output file.</param>
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        public Task SerializeAsync<T>(T value, DirectoryInfo directory, string fileName, bool encrypt = false, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null) =>
            Task.Factory.StartNew(() => Serialize(value, directory, fileName, encrypt, formatting, serializerOptions));
    }
}
