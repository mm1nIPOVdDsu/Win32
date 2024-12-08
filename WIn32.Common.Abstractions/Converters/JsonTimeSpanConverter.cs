using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Win32.Common.Converters
{
    /// <summary>
    ///     JSON converter to convert TimeSpan to a millisecond string.
    /// </summary>
    public class JsonTimeSpanConverter : JsonConverter<TimeSpan>
    {
        /// <summary>
        ///     Read and convert the JSON to <see cref="TimeSpan"/>.
        /// </summary>
        /// <remarks>
        /// A converter may throw any Exception, but should throw <cref>JsonException</cref> when the JSON is invalid.
        /// </remarks>
        /// <param name="reader">The <see cref="Utf8JsonReader"/> to read from.</param>
        /// <param name="typeToConvert">The <see cref="Type"/> being converted.</param>
        /// <param name="options">The <see cref="JsonSerializerOptions"/> being used.</param>
        /// <returns>The value that was converted.</returns>
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(typeToConvert != typeof(double) && typeToConvert != typeof(int) && typeToConvert != typeof(uint))
                throw new ArgumentException("Type to convert must be either a double or integer.");
            
            var value = reader.GetString();
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value to parse is null.");

            return TimeSpan.Parse(value);
        }
        /// <summary>
        ///     Write the value as JSON.
        /// </summary>
        /// <remarks>
        ///     A converter may throw any Exception, but should throw <see cref="JsonException"/> when the JSON cannot be created.
        /// </remarks>
        /// <param name="writer">The <see cref="Utf8JsonWriter"/> to write to.</param>
        /// <param name="value">The value to convert.</param>
        /// <param name="options">The <see cref="JsonSerializerOptions"/> being used.</param>
        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            if(writer is null)
                throw new ArgumentNullException(nameof(writer));

            writer.WriteStringValue(value.ToString());
        }
    }
}
