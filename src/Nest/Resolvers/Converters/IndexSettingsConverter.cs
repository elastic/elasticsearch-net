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
			var indexSettings = (IndexSettings)value;

			writer.WriteStartObject();

			writer.WritePropertyName("settings");
			writer.WriteStartObject();

			writer.WritePropertyName("index");
			writer.WriteStartObject();

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

			if (indexSettings.Similarity != null)
			{
				writer.WritePropertyName("similarity");
				writer.WriteStartObject();

				if (!string.IsNullOrEmpty(indexSettings.Similarity.BaseSimilarity))
				{
					writer.WritePropertyName("base");
					writer.WriteStartObject();
					writer.WritePropertyName("type");
					writer.WriteValue(indexSettings.Similarity.BaseSimilarity);
					writer.WriteEndObject();
				}

				if (indexSettings.Similarity.CustomSimilarities != null)
				{
					foreach (var customSimilarity in indexSettings.Similarity.CustomSimilarities)
					{
						writer.WritePropertyName(customSimilarity.Name);
						writer.WriteStartObject();
						writer.WritePropertyName("type");
						writer.WriteValue(customSimilarity.Type);
						if (customSimilarity.SimilarityParameters.HasAny())
						{
							foreach (var kv in customSimilarity.SimilarityParameters)
							{
								writer.WritePropertyName(kv.Key);
								writer.WriteValue(kv.Value);
							}
						}
						writer.WriteEndObject();
					}
				}
				writer.WriteEndObject();
			}

			writer.WriteEndObject();
			if (indexSettings.Mappings.Count > 0)
			{
				var settings = serializer.ContractResolver as ElasticResolver;
				if (settings != null && settings.ConnectionSettings != null)
				{
					writer.WritePropertyName("mappings");
					serializer.Serialize(writer,
					                     indexSettings.Mappings.ToDictionary(m => m.TypeNameMarker.Resolve(settings.ConnectionSettings)));
				}
			}
			if (indexSettings.Warmers.Count > 0)
			{
				writer.WritePropertyName("warmers");
				serializer.Serialize(writer, indexSettings.Warmers);
			}

			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
										JsonSerializer serializer)
		{
			return null;
		}

		private static Type _type = typeof (IndexSettings);
		public override bool CanConvert(Type objectType)
		{
			return objectType == _type;
		}
	}
}