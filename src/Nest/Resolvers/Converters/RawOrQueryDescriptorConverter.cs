using System;
using System.Linq;
using Nest.Settings;
using Newtonsoft.Json;
using Nest.DSL;

namespace Nest.Resolvers.Converters
{
    public class RawOrQueryDescriptorConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var type = value.GetType();
            var rawProperty = type.GetProperty("RawQuery");
            var descriptorProperty = type.GetProperty("Descriptor");
            
            var raw = rawProperty.GetValue(value, null) as string;
            var descriptor = descriptorProperty.GetValue(value, null);

            if (!string.IsNullOrEmpty(raw))
            {
                writer.WriteRaw(raw);
            }
            else
            {
                serializer.Serialize(writer, descriptor);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                                        JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsGenericType 
                && objectType.GetGenericTypeDefinition() == typeof (RawOrQueryDescriptor<>);
        }
    }
}