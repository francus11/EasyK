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
    public class KeyboardActionJsonConverter : JsonConverter<KeyboardAction>
    {
        public override KeyboardAction? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            KeyboardAction action = null;

            using (var document = JsonDocument.ParseValue(ref reader))
            {
                var root = document.RootElement;

                try
                {
                    //TODO do something with modifiers
                    action = new KeyboardAction(
                        (ClickType)root.GetProperty("state").GetInt32(), 
                        KeyToHidUsageIdMapper.GetKey(root.GetProperty("key").GetInt32()), 
                        new List<ModifierKeys>()
                        );
                }
                catch (KeyNotFoundException e)
                {
                    throw;
                }
            }

            return action;
        }

        public override void Write(Utf8JsonWriter writer, KeyboardAction value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("type", typeof(KeyboardAction).Name);
            writer.WritePropertyName("details");
            writer.WriteStartObject();
            writer.WriteNumber("state", (int)value.ClickType);
            int key = KeyToHidUsageIdMapper.GetUsageId(value.CombinationKey.Value) ?? 0;
            writer.WriteNumber("key", key);
            writer.WriteEndObject();
            writer.WriteEndObject();
        }
    }
}
