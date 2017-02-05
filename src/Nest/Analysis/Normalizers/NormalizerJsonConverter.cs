using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class NormalizerJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;
		public override bool CanWrite => false;
		public override bool CanRead => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var o = JObject.Load(reader);

			var typeProperty = o.Property("type");
			if (typeProperty == null) return null;

			var typePropertyValue = typeProperty.Value.ToString();
			switch(typePropertyValue)
			{
				default:
					return o.ToObject<CustomNormalizer>(ElasticContractResolver.Empty);
			}
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}
	}
}
