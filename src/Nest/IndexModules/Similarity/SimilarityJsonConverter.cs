using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers;

namespace Nest
{
	internal class SimilarityCollectionJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => typeof(ISimilarity).IsAssignableFrom(objectType);
		public override bool CanWrite => false;
		public override bool CanRead => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject o = JObject.Load(reader);

			JProperty typeProperty = o.Property("type");
			if (typeProperty == null) return null;

			var typePropertyValue = typeProperty.Value.ToString();
			var itemType = Type.GetType("Nest." + typePropertyValue + "Similarity", false, true);
			return o.ToObject(itemType, ElasticContractResolver.Empty);
			//return serializer.Deserialize(o.CreateReader(), itemType) as ISimilarity;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
