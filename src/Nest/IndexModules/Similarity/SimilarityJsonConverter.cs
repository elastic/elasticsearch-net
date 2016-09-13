using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Nest
{
	internal class SimilarityJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => typeof(ISimilarity).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
		public override bool CanWrite => false;
		public override bool CanRead => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject o = JObject.Load(reader);

			JProperty typeProperty = o.Property("type");
			if (typeProperty == null) return null;

			var typePropertyValue = typeProperty.Value.ToString();
			var itemType = Type.GetType("Nest." + typePropertyValue + "Similarity", false, true);
			if (itemType != null) return o.ToObject(itemType, ElasticContractResolver.Empty);

			var dict = o.ToObject<Dictionary<string, object>>();
			return new CustomSimilarity(dict);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
