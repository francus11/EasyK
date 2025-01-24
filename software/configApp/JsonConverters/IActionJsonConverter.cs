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
            IAction action = null;

            using (var document = JsonDocument.ParseValue(ref reader))
            {
                var root = document.RootElement;

                if (root.TryGetProperty("type", out var typeElement))
                {
                    var type = typeElement.GetString();
                    if (type == typeof(DelayAction).Name)
                    {
                        action = JsonSerializer.Deserialize<DelayAction>(root.GetProperty("details").GetRawText(), options);
                    }
                    else if (type == typeof(KeyboardAction).Name)
                    {
                        action = JsonSerializer.Deserialize<KeyboardAction>(root.GetProperty("details").GetRawText(), options);
                    }
                    else if (type == typeof(MacroAction).Name)
                    {
                        action = JsonSerializer.Deserialize<MacroAction>(root.GetProperty("actions").GetRawText(), options);
                    }
                    else
                    {
                        throw new NotSupportedException();
                    }
                }
            }

            return action;
        }

        public override void Write(Utf8JsonWriter writer, IAction value, JsonSerializerOptions options)
        {
            if (value is DelayAction delayAction)
            {
                JsonSerializer.Serialize(writer, delayAction, options);
            }
            else if (value is KeyboardAction keyboardAction)
            {
                JsonSerializer.Serialize(writer, keyboardAction, options);
            }
            else if (value is MacroAction macroAction)
            {
                JsonSerializer.Serialize(writer, macroAction, options);
            }
            else if (value is null)
            {
                
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
