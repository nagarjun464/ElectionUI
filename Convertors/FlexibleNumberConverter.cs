using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Electionapp.UI.Converters
{
    public class FlexibleNumberConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonTokenType.String => reader.GetString() ?? "0",
                JsonTokenType.Number => reader.GetInt32().ToString(),
                _ => "0"
            };
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }
}
