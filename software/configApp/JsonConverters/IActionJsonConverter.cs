using configApp.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace configApp.JsonConverters
{
    public class IActionJsonConverter : JsonConverter<IAction>
    {
        public override IAction? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, IAction value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            if (value is DelayAction delayAction)
            {
                writer.WriteString("type", typeof(DelayAction).Name);
                writer.WritePropertyName("details");
                JsonSerializer.Serialize(writer, delayAction, options);
            }
            else if (value is KeyboardAction keyboardAction)
            {
                writer.WriteString("type", typeof(KeyboardAction).Name);
                writer.WritePropertyName("details");
                JsonSerializer.Serialize(writer, keyboardAction, options);
            }
            else if (value is MacroAction macroAction)
            {
                writer.WriteString("type", typeof(MacroAction).Name);
                writer.WritePropertyName("details");
                JsonSerializer.Serialize(writer, macroAction, options);
            }
            else if (value is null)
            {
                //TODO implement null value

                //writer.WriteNullValue();

                //writer.WriteStartObject();
                //writer.write
                //writer.WriteEndObject();
            }
            else
            {
                throw new NotSupportedException();
            }
            writer.WriteEndObject();
        }
    }
}
