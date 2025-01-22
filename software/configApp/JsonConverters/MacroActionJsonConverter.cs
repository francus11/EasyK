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
    public class MacroActionJsonConverter : JsonConverter<MacroAction>
    {
        public override MacroAction? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            MacroAction macroAction = null;
            List<IAction> actionList = new List<IAction>();

            using (var document = JsonDocument.ParseValue(ref reader))
            {
                var root = document.RootElement;


                foreach (var obj in root.EnumerateArray())
                {
                    actionList.Add(JsonSerializer.Deserialize<IAction>(obj.GetRawText(), options));
                }
            }

            macroAction = new MacroAction(actionList);

            return macroAction;
        }

        public override void Write(Utf8JsonWriter writer, MacroAction value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("type", "MacroAction");
            writer.WritePropertyName("actions");
            writer.WriteStartArray();
            foreach (var action in value.ActionList)
            {
                JsonSerializer.Serialize(writer, action, options);
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}
