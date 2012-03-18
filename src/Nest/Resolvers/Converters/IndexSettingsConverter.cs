using System;
using System.Linq;
using Newtonsoft.Json;

namespace Nest.Resolvers.Converters
{
    public class IndexSettingsConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var indexSettings = (IndexSettings) value;

            writer.WriteStartObject();

            writer.WritePropertyName("settings");
            writer.WriteStartObject();

            writer.WritePropertyName("index");
            writer.WriteStartObject();

            writer.WritePropertyName("number_of_shards");
            writer.WriteValue(indexSettings.NumberOfShards);
            writer.WritePropertyName("number_of_replicas");
            writer.WriteValue(indexSettings.NumberOfReplicas);

            foreach (var kv in indexSettings.Settings)
            {
                writer.WritePropertyName(kv.Key);
                writer.WriteValue(kv.Value);
            }

            writer.WriteEndObject();

            writer.WritePropertyName("analysis");
            writer.WriteStartObject();

            writer.WritePropertyName("analyzer");
            serializer.Serialize(writer, indexSettings.Analysis.Analyzer);

            if (indexSettings.Analysis.TokenFilters.Count > 0)
            {
                writer.WritePropertyName("filter");
                serializer.Serialize(writer, indexSettings.Analysis.TokenFilters);
            }

            writer.WriteEndObject();

            writer.WriteEndObject();

            if (indexSettings.Mappings.Count > 0)
            { 
                writer.WritePropertyName("mappings");
                serializer.Serialize(writer, indexSettings.Mappings.ToDictionary(m => m.Name));
            }

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                                        JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (IndexSettings);
        }
    }
}