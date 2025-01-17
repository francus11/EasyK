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
    public class KeyboardActionJsonConverter : JsonConverter<KeyboardAction>
    {
        public override KeyboardAction? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, KeyboardAction value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteNumber("type", (int)value.ClickType);
            int key = KeyToHidUsageIdMapper.GetUsageId(value.CombinationKey.Value) ?? 0;
            writer.WriteNumber("key", key);
            writer.WriteEndObject();
        }
    }
}
