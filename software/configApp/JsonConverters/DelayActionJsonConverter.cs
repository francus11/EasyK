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
    public class DelayActionJsonConverter : JsonConverter<DelayAction>
    {
        public override DelayAction? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, DelayAction value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("delay", value.Delay);
            writer.WriteEndObject();

        }
    }
}
