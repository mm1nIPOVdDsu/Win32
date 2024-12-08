using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Win32.Common.Services.Serialization
{
    /// <summary>
    ///     Serializes strings and files, to and from JSON or XML formatting.
    /// </summary>
    public interface ISerializationService : IServiceBase
    {
        /// <summary>
        ///     Deserializes a string into an instance of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The string value to deserialize into <typeparamref name="T"/>.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the string.</param>
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        T? Deserialize<T>(string value, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null);
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
        T? Deserialize<T>(DirectoryInfo directory, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null);
        /// <summary>
        ///     Deserializes a file to an instance of <typeparamref name="T"/>. 
        /// </summary>
        /// <typeparam name="T">The object type to deserialize to.</typeparam>
        /// <param name="directory">The directory where the file exists.</param>
        /// <param name="fileName">The file name of the file to deserialize.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the file.</param> 
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        T? Deserialize<T>(DirectoryInfo directory, string fileName, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null);
        /// <summary>
        ///     Deserializes a file to an instance of <typeparamref name="T"/>. 
        /// </summary>
        /// <typeparam name="T">The object type to deserialize to.</typeparam>
        /// <param name="directory">The directory where the file exists.</param>
        /// <param name="fileName">The file name of the file to deserialize.</param>
        /// <param name="isEncrypted"></param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the file.</param> 
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        T? Deserialize<T>(DirectoryInfo directory, string fileName, bool isEncrypted, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null);

        /// <summary>
        ///     Deserializes a string into an instance of an object.
        /// </summary>
        /// <param name="value">The string value to deserialize into an object.</param>
        /// <param name="type">The type of object to deserialize to.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the string.</param>
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        /// <returns>An instance of an object.</returns>
        object? Deserialize(string value, Type type, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null);

        /// <summary>
        ///     Asynchronously deserializes a string into an instance of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The string value to deserialize into <typeparamref name="T"/>.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the string.</param>
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        Task<T?> DeserializeAsync<T>(string value, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null);
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
        Task<T?> DeserializeAsync<T>(DirectoryInfo directory, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null);
        /// <summary>
        ///     Asynchronously deserializes a file to an instance of <typeparamref name="T"/>. 
        /// </summary>
        /// <typeparam name="T">The object type to deserialize to.</typeparam>
        /// <param name="directory">The directory where the file exists.</param>
        /// <param name="fileName">The file name of the file to deserialize.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the file.</param> 
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        Task<T?> DeserializeAsync<T>(DirectoryInfo directory, string fileName, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null);
        /// <summary>
        ///     Asynchronously deserializes a file to an instance of <typeparamref name="T"/>. 
        /// </summary>
        /// <typeparam name="T">The object type to deserialize to.</typeparam>
        /// <param name="directory">The directory where the file exists.</param>
        /// <param name="fileName">The file name of the file to deserialize.</param>
        /// <param name="isEncrypted"></param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the file.</param> 
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        Task<T?> DeserializeAsync<T>(DirectoryInfo directory, string fileName, bool isEncrypted, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null);
        /// <summary>
        ///     Asynchronously deserializes a string into an instance of an object.
        /// </summary>
        /// <param name="value">The string value to deserialize into an object.</param>
        /// <param name="type">The type of object to deserialize to.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the string.</param>
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        /// <returns>An instance of an object.</returns>
        Task<object?> DeserializeAsync(string value, Type type, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null);

        /// <summary>
        ///     Serializes an object into a string.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the output string.</param>
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        /// <returns>A string formatted into the type defined by <see cref="SerializationFormat"/>.</returns>
        string Serialize<T>(T value, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null);
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
        void Serialize<T>(T value, DirectoryInfo directory, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null);
        /// <summary>
        ///     Serializes an instance of <typeparamref name="T"/> to a file. If the file already exists, it will be overwritten.
        /// </summary>
        /// <typeparam name="T">The object type to serialize to a file.</typeparam>
        /// <param name="value">An instance of <typeparamref name="T"/>.</param>
        /// <param name="directory">The directory to save the file to.</param>
        /// <param name="fileName">The file name of the file to serialize.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the output file.</param>
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        void Serialize<T>(T value, DirectoryInfo directory, string fileName, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null);
        /// <summary>
        ///     Serializes an instance of <typeparamref name="T"/> to a file. If the file already exists, it will be overwritten.
        /// </summary>
        /// <typeparam name="T">The object type to serialize to a file.</typeparam>
        /// <param name="value">An instance of <typeparamref name="T"/>.</param>
        /// <param name="directory">The directory to save the file to.</param>
        /// <param name="fileName">The file name of the file to serialize.</param>
        /// <param name="encrypt">True to encrypt the file after its written to the file system.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the output file.</param>
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        void Serialize<T>(T value, DirectoryInfo directory, string fileName, bool encrypt, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null);

        /// <summary>
        ///     Asynchronously serializes an object into a string.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the output string.</param>
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        /// <returns>A string formatted into the type defined by <see cref="SerializationFormat"/>.</returns>
        Task<string> SerializeAsync<T>(T value, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null);
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
        Task SerializeAsync<T>(T value, DirectoryInfo directory, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null);
        /// <summary>
        ///     Asynchronously serializes an instance of <typeparamref name="T"/> to a file. If the file already exists, it will be overwritten.
        /// </summary>
        /// <typeparam name="T">The object type to serialize to a file.</typeparam>
        /// <param name="value">An instance of <typeparamref name="T"/>.</param>
        /// <param name="directory">The directory to save the file to.</param>
        /// <param name="fileName">The file name of the file to serialize.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the output file.</param>
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        Task SerializeAsync<T>(T value, DirectoryInfo directory, string fileName, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null);
        /// <summary>
        ///     Asynchronously serializes an instance of <typeparamref name="T"/> to a file. If the file already exists, it will be overwritten.
        /// </summary>
        /// <typeparam name="T">The object type to serialize to a file.</typeparam>
        /// <param name="value">An instance of <typeparamref name="T"/>.</param>
        /// <param name="directory">The directory to save the file to.</param>
        /// <param name="fileName">The file name of the file to serialize.</param>
        /// <param name="encrypt">True to encrypt the file after its written to the file system.</param>
        /// <param name="formatting">The <see cref="SerializationFormat"/> of the output file.</param>
        /// <param name="serializerOptions">Options for serialization. If null, default options are used.</param>
        Task SerializeAsync<T>(T value, DirectoryInfo directory, string fileName, bool encrypt, SerializationFormat formatting = SerializationFormat.Json, JsonSerializerOptions? serializerOptions = null);
    }
}
