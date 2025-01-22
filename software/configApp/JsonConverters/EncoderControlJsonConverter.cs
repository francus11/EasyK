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
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, EncoderControl value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
