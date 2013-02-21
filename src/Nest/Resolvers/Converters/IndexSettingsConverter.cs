using System;
using System.Linq;
using Newtonsoft.Json;

namespace Nest.Resolvers.Converters
{
  using System.Text;

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

        // allready in indexSetting.Setting
	      /*
		    if (indexSettings.NumberOfReplicas.HasValue) 
			{
		        writer.WritePropertyName("number_of_shards");
		        writer.WriteValue(indexSettings.NumberOfShards);
	        }
	        if (indexSettings.NumberOfShards.HasValue)
	        {
		        writer.WritePropertyName("number_of_replicas");
		        writer.WriteValue(indexSettings.NumberOfReplicas);
	        }
         */
	        if (indexSettings.Settings.HasAny())
	        {
		        foreach (var kv in indexSettings.Settings)
		        {
			        writer.WritePropertyName(kv.Key);
			        writer.WriteValue(kv.Value);
		        }
	        }

	        if (
				indexSettings.Analysis.Analyzers.Count > 0
				|| indexSettings.Analysis.TokenFilters.Count > 0
				|| indexSettings.Analysis.Tokenizers.Count > 0
				|| indexSettings.Analysis.CharFilters.Count > 0
		        )
	        {
		        writer.WritePropertyName("analysis");
		        writer.WriteStartObject();
		        if (indexSettings.Analysis.Analyzers.Count > 0)
		        {
			        writer.WritePropertyName("analyzer");
			        serializer.Serialize(writer, indexSettings.Analysis.Analyzers);
		        }

		        if (indexSettings.Analysis.TokenFilters.Count > 0)
		        {
			        writer.WritePropertyName("filter");
			        serializer.Serialize(writer, indexSettings.Analysis.TokenFilters);
		        }
		        if (indexSettings.Analysis.Tokenizers.Count > 0)
		        {
			        writer.WritePropertyName("tokenizer");
			        serializer.Serialize(writer, indexSettings.Analysis.Tokenizers);
		        }
		        if (indexSettings.Analysis.CharFilters.Count > 0)
		        {
			        writer.WritePropertyName("char_filter");
			        serializer.Serialize(writer, indexSettings.Analysis.CharFilters);
		        }

		        writer.WriteEndObject();
	        }

			writer.WriteEndObject();

      if (!string.IsNullOrEmpty(indexSettings.Similarity.IndexSimilarityProvider)
          || !string.IsNullOrEmpty(indexSettings.Similarity.SearchSimilarityProvider))
      {
        writer.WritePropertyName("similarity");
        writer.WriteStartObject();
        if (!string.IsNullOrEmpty(indexSettings.Similarity.IndexSimilarityProvider))
        {
          writer.WritePropertyName("index");
          writer.WriteStartObject();
          writer.WritePropertyName("type");
          serializer.Serialize(writer, indexSettings.Similarity.IndexSimilarityProvider);
          writer.WriteEndObject();
        }

        if (!string.IsNullOrEmpty(indexSettings.Similarity.SearchSimilarityProvider))
        {
          writer.WritePropertyName("search");
          writer.WriteStartObject();
          writer.WritePropertyName("type");
          serializer.Serialize(writer, indexSettings.Similarity.SearchSimilarityProvider);
          writer.WriteEndObject();
        }
        writer.WriteEndObject();
      }

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
          return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (IndexSettings);
        }
    }
}