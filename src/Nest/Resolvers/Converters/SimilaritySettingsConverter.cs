using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Nest.Resolvers.Converters
{
	public class SimilaritySettingsConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(SimilaritySettings);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject o = JObject.Load(reader);
			var result = existingValue as SimilaritySettings ?? new SimilaritySettings();

			foreach(var rootProperty in o.Children<JProperty>())
			{
				var typeProperty = ((JObject)rootProperty.Value).Property("type");
				var itemType = Type.GetType("Nest." + typeProperty.Value.ToString() + "Similarity", false, true);
				var similarity = serializer.Deserialize(rootProperty.Value.CreateReader(), itemType) as SimilarityBase;
				result.CustomSimilarities.Add(rootProperty.Name, similarity);
			}

			return result;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var similarityValue = (SimilaritySettings)value;

			if(similarityValue.CustomSimilarities.Count > 0)
			{
				serializer.Serialize(writer, similarityValue.CustomSimilarities);
			}

			if (!string.IsNullOrEmpty(similarityValue.Default))
			{
				writer.WritePropertyName("similarity.default.type");
				writer.WriteValue(similarityValue.Default);
			}
		}
	}
}
