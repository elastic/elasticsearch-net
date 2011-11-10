using System;
using System.Linq;
using ElasticSearch.Client.Settings;
using Newtonsoft.Json;

namespace ElasticSearch.Client.Resolvers.Converters
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

            writer.WriteEndObject();

            writer.WritePropertyName("analysis");
            writer.WriteStartObject();

            writer.WritePropertyName("analyzer");
            serializer.Serialize(writer, indexSettings.Analysis.Analyzer);

            writer.WriteEndObject();

            writer.WriteEndObject();

            writer.WritePropertyName("mappings");
            serializer.Serialize(writer, indexSettings.Mappings.ToDictionary(m => m.Name));

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