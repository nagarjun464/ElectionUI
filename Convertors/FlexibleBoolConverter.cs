using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Electionapp.UI.Converters
{
    public class FlexibleBoolConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonTokenType.String => reader.GetString() ?? "false",
                JsonTokenType.True => "true",
                JsonTokenType.False => "false",
                _ => "false"
            };
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }
}
