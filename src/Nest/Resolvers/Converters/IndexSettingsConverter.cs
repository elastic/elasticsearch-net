using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Domain.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.Resolvers.Converters
{
	using System.Text;

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

			writer.WritePropertyName("settings");
			writer.WriteStartObject();

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
				var contract = serializer.ContractResolver as ElasticContractResolver;
				if (contract != null && contract.ConnectionSettings != null)
				{
					writer.WritePropertyName("mappings");
					serializer.Serialize(
						writer,
						indexSettings.Mappings.ToDictionary(m =>
						{
							var name = contract.Infer.PropertyName(m.Name);
							if (name.IsNullOrEmpty())
								throw new DslException("{0} should have a name!".F(m.GetType()));
							return name;
						})
					);
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
			JObject o = JObject.Load(reader);
			var result = new IndexSettings();
			var dictionary = new Dictionary<string, object>();
			serializer.Populate(o.CreateReader(), dictionary);
			result.Settings = dictionary;
			result._ = ElasticsearchDynamic.Create(dictionary);
			foreach (var rootProperty in o.Children<JProperty>())
			{
				if (rootProperty.Name.Equals("analysis", StringComparison.InvariantCultureIgnoreCase))
				{
					result.Analysis = serializer.Deserialize<AnalysisSettings>(rootProperty.Value.CreateReader());
					result.Settings.Remove(rootProperty.Name);
				}
				else if (rootProperty.Name.Equals("warmers", StringComparison.InvariantCultureIgnoreCase))
				{
					foreach (var jWarmer in rootProperty.Value.Children<JProperty>())
					{
						result.Warmers[jWarmer.Name] = serializer.Deserialize<WarmerMapping>(jWarmer.Value.CreateReader());
					}
					result.Settings.Remove(rootProperty.Name);
				}
				else if (rootProperty.Name.Equals("similarity", StringComparison.InvariantCultureIgnoreCase))
				{
					var baseSimilarity = ((JObject)rootProperty.Value).Property("base");
					if (baseSimilarity != null)
					{
						baseSimilarity.Remove();
						result.Similarity = new SimilaritySettings(((JObject)baseSimilarity.Value).Property("type").Value.ToString());
					}
					else
					{
						result.Similarity = new SimilaritySettings();
					}

					foreach (var similarityProperty in rootProperty.Value.Children<JProperty>())
					{
						var typeProperty = ((JObject)similarityProperty.Value).Property("type");
						typeProperty.Remove();
						var customSimilarity = new CustomSimilaritySettings(similarityProperty.Name, typeProperty.Value.ToString());
						foreach (var similaritySetting in similarityProperty.Value.Children<JProperty>())
						{
							customSimilarity.SimilarityParameters.Add(similaritySetting.Name, similaritySetting.Value.ToString());
						}

						result.Similarity.CustomSimilarities.RemoveAll(x => x.Name == customSimilarity.Name);
						result.Similarity.CustomSimilarities.Add(customSimilarity);
					}
					result.Settings.Remove(rootProperty.Name);
				}
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