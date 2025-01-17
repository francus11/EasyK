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
    public class ButtonControlJsonConverter : JsonConverter<ButtonControl>
    {
        public override ButtonControl? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, ButtonControl value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("id", value.Id);
            writer.WritePropertyName("action");
            JsonSerializer.Serialize(writer, value.PressAction, options);
            writer.WriteEndObject();
        }
    }
}
