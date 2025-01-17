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
    internal class MacroConfJsonConverter : JsonConverter<List<IControl>>
    {
        public override List<IControl>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, List<IControl> value, JsonSerializerOptions options)
        {
            var buttons = value.OfType<ButtonControl>().ToList();
            var encoders = value.OfType<EncoderControl>().ToList();

            // Rozpocznij obiekt JSON
            writer.WriteStartObject();

            // Jeśli istnieją elementy typu ButtonControl, zapisz je w tabeli "buttons"
            if (buttons.Any())
            {
                writer.WritePropertyName("buttons");
                writer.WriteStartArray();
                foreach (var button in buttons)
                {
                    JsonSerializer.Serialize(writer, button, options);
                }
                writer.WriteEndArray();
            }

            // Jeśli istnieją elementy typu EncoderControl, zapisz je w tabeli "encoders"
            if (encoders.Any())
            {
                writer.WritePropertyName("encoders");
                writer.WriteStartArray();
                foreach (var encoder in encoders)
                {
                    JsonSerializer.Serialize(writer, encoder, options);
                }
                writer.WriteEndArray();
            }

            // Zakończ obiekt JSON
            writer.WriteEndObject();
        }
    }
}
