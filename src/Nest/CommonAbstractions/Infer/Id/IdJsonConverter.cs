using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
    internal class IdJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => typeof(Id) == objectType;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return new Id(reader.Value as string);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var id = value as Id;
            if (id == null)
            {
                writer.WriteNull();
                return;
            }
            writer.WriteValue(id.Value);
        }
    }
}
