using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.Resolvers.Converters
{
	public class IndexSettingsConverter : JsonConverter
	{
		private void WriteSettingObject(JsonWriter writer, JObject obj)
		{
			writer.WriteStartObject();
			foreach (var property in obj.Children<JProperty>())
			{
				writer.WritePropertyName(property.Name);
				if (property.Value is JObject)
					this.WriteSettingObject(writer, property.Value as JObject);
				else
					writer.WriteValue(property.Value);
			}
			writer.WriteEndObject();

		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var indexSettings = (IndexSettings)value;

			writer.WriteStartObject();

			WriteSettings(writer, serializer, indexSettings);

			WriteMappings(writer, serializer, indexSettings);

			WriteWarmers(writer, serializer, indexSettings);

			WriteAliases(writer, serializer, indexSettings);

			writer.WriteEndObject();
		}

		private void WriteSettings(JsonWriter writer, JsonSerializer serializer, IndexSettings indexSettings)
		{
			writer.WritePropertyName("settings");
			writer.WriteStartObject();
			WriteIndexSettings(writer, serializer, indexSettings);
			writer.WriteEndObject();
		}

		private static void WriteAliases(JsonWriter writer, JsonSerializer serializer, IndexSettings indexSettings)
		{
			if (indexSettings.Aliases == null || indexSettings.Aliases.Count <= 0) return;
			writer.WritePropertyName("aliases");
			serializer.Serialize(writer, indexSettings.Aliases);
		}

		private static void WriteWarmers(JsonWriter writer, JsonSerializer serializer, IndexSettings indexSettings)
		{
			if (indexSettings.Warmers.Count <= 0) return;
			writer.WritePropertyName("warmers");
			serializer.Serialize(writer, indexSettings.Warmers);
		}

		private static void WriteMappings(JsonWriter writer, JsonSerializer serializer, IndexSettings indexSettings)
		{
			if (indexSettings.Mappings.Count <= 0) return;
			var contract = serializer.ContractResolver as SettingsContractResolver;
			if (contract == null || contract.ConnectionSettings == null) return;

			writer.WritePropertyName("mappings");
			serializer.Serialize(
				writer,
				indexSettings.Mappings.ToDictionary(m =>
				{
					var name = contract.Infer.FieldName(m.Name);
					if (name.IsNullOrEmpty())
						throw new DslException("{0} should have a name!".F(m.GetType()));
					return name;
				})
			);
		}

		private void WriteIndexSettings(JsonWriter writer, JsonSerializer serializer, IndexSettings indexSettings)
		{
			writer.WritePropertyName("index");
			writer.WriteStartObject();
			if (indexSettings.Settings.HasAny())
			{
				foreach (var kv in indexSettings.Settings)
				{
					writer.WritePropertyName(kv.Key);
					if (kv.Value is JObject)
						this.WriteSettingObject(writer, kv.Value as JObject);
					else
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
				serializer.Serialize(writer, indexSettings.Analysis);
			}

			if (
				indexSettings.Similarity.CustomSimilarities.Count > 0
				|| !string.IsNullOrEmpty(indexSettings.Similarity.Default)
				)
			{
				writer.WritePropertyName("similarity");
				serializer.Serialize(writer, indexSettings.Similarity);
			}

			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
										JsonSerializer serializer)
		{
			var result = new IndexSettings();
			if (reader.TokenType != JsonToken.StartObject) return result;

			var jsonObject = JObject.Load(reader);
			if (jsonObject["settings"] != null && jsonObject["settings"]["index"] != null)
			{
				var settings = jsonObject["settings"]["index"];
				var dictionary = new Dictionary<string, object>();
				serializer.Populate(settings.CreateReader(), dictionary);
				result.Settings = dictionary;
				result.AsExpando = DynamicResponse.Create(dictionary);
				
				foreach (var rootProperty in settings.Children<JProperty>())
				{
					if (rootProperty.Name.Equals("analysis", StringComparison.InvariantCultureIgnoreCase))
					{
						result.Analysis = serializer.Deserialize<AnalysisSettings>(rootProperty.Value.CreateReader());
						result.Settings.Remove(rootProperty.Name);
					}
					else if (rootProperty.Name.Equals("similarity", StringComparison.InvariantCultureIgnoreCase))
					{
						result.Similarity = serializer.Deserialize<SimilaritySettings>(rootProperty.Value.CreateReader());
						result.Settings.Remove(rootProperty.Name);
					}
				}
			}
			if (jsonObject["aliases"] != null)
			{
				var a = serializer.Deserialize<Dictionary<string, CreateAliasOperation>>(jsonObject["aliases"].CreateReader());
				result.Aliases = a.ToDictionary(kv => kv.Key, kv => kv.Value as ICreateAliasOperation);
			}
			if (jsonObject["mappings"] != null)
			{
				var mappings = serializer.Deserialize<Dictionary<string, RootObjectMapping>>(jsonObject["mappings"].CreateReader());
				result.Mappings = mappings.Select(kv =>
				{
					var name = kv.Key;
					kv.Value.Name = name;
					return kv.Value;
				}).ToList();
			}
			
			if (jsonObject["warmers"] != null)
			{
				var warmers = serializer.Deserialize<Dictionary<string, WarmerMapping>>(jsonObject["warmers"].CreateReader());
				result.Warmers = warmers.ToDictionary(kv=>kv.Key, kv =>
				{
					kv.Value.Name = kv.Key;
					return kv.Value;
				});
			}
			return result;
		}

		private static Type _type = typeof(IndexSettings);
		public override bool CanConvert(Type objectType)
		{
			return objectType == _type;
		}
	}
}