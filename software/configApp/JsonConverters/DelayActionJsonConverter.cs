using configApp.Actions;
using configApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;

namespace configApp.JsonConverters
{
    public class DelayActionJsonConverter : JsonConverter<DelayAction>
    {
        public override DelayAction? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            DelayAction action = null;

            using (var document = JsonDocument.ParseValue(ref reader))
            {
                var root = document.RootElement;

                try
                {
                    action = new DelayAction(root.GetProperty("duration").GetInt32());
                }
                catch (KeyNotFoundException e)
                {
                    throw;
                }
            }

            return action;
        }

        public override void Write(Utf8JsonWriter writer, DelayAction value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("type", typeof(DelayAction).Name);

            writer.WritePropertyName("details");
            writer.WriteStartObject();
            writer.WriteNumber("duration", value.Delay);
            writer.WriteEndObject();

            writer.WriteEndObject();

        }
    }
}
