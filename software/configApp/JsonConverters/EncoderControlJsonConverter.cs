using configApp.Actions;
using configApp.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace configApp.JsonConverters
{
    class EncoderControlJsonConverter : JsonConverter<EncoderControl>
    {
        public override EncoderControl? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var encoder = new EncoderControl();

            using (var document = JsonDocument.ParseValue(ref reader))
            {
                var root = document.RootElement;

                if (root.TryGetProperty("id", out var idElement))
                {
                    encoder.Id = idElement.GetInt32();
                }

                if (root.TryGetProperty("actionLeft", out var actionLeftElement))
                {
                    encoder.ActionLeft = JsonSerializer.Deserialize<IAction>(actionLeftElement.GetRawText(), options);
                }

                if (root.TryGetProperty("actionRight", out var actionRightElement))
                {
                    encoder.ActionRight = JsonSerializer.Deserialize<IAction>(actionRightElement.GetRawText(), options);
                }

                if (root.TryGetProperty("actionButton", out var actionButtonElement))
                {
                    encoder.ActionButton = JsonSerializer.Deserialize<IAction>(actionButtonElement.GetRawText(), options);
                }
            } 
            
            return encoder;
        }

        public override void Write(Utf8JsonWriter writer, EncoderControl value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteNumber("id", value.Id);

            writer.WritePropertyName("actionLeft");
            JsonSerializer.Serialize(writer, value.ActionLeft, options);
            writer.WritePropertyName("actionRight");
            JsonSerializer.Serialize(writer, value.ActionRight, options);
            writer.WritePropertyName("actionButton");
            JsonSerializer.Serialize(writer, value.ActionButton, options);

            writer.WriteEndObject();
        }
    }
}
