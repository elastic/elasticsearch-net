using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class PropertyJsonConverter : JsonConverter
	{
		public override bool CanWrite => false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var jObject = JObject.Load(reader);
			JToken typeToken;
			JToken propertiesToken;
			var type = string.Empty;
			var hasType = jObject.TryGetValue("type", out typeToken);
			if (hasType)
				type = typeToken.Value<string>().ToLowerInvariant();
			else if (jObject.TryGetValue("properties", out propertiesToken))
				type = "object";

			serializer.TypeNameHandling = TypeNameHandling.None;

			switch (type)
			{
				case "text":
					return jObject.ToObject<TextProperty>();
				case "keyword":
					return jObject.ToObject<KeywordProperty>();
				case "string":
#pragma warning disable 618
					return jObject.ToObject<StringProperty>();
#pragma warning restore 618
				case "float":
				case "double":
				case "byte":
				case "short":
				case "integer":
				case "long":
				case "scaled_float":
				case "half_float":
					return jObject.ToObject<NumberProperty>();
				case "date":
					return jObject.ToObject<DateProperty>();
				case "boolean":
					return jObject.ToObject<BooleanProperty>();
				case "binary":
					return jObject.ToObject<BinaryProperty>();
				case "object":
					return jObject.ToObject<ObjectProperty>();
				case "nested":
					return jObject.ToObject<NestedProperty>();
				case "ip":
					return jObject.ToObject<IpProperty>();
				case "geo_point":
					return jObject.ToObject<GeoPointProperty>();
				case "geo_shape":
					return jObject.ToObject<GeoShapeProperty>();
				case "attachment":
					return jObject.ToObject<AttachmentProperty>();
				case "completion":
					return jObject.ToObject<CompletionProperty>();
				case "token_count":
					return jObject.ToObject<TokenCountProperty>();
				case "murmur3":
					return jObject.ToObject<Murmur3HashProperty>();
				case "percolator":
					return jObject.ToObject<PercolatorProperty>();
			}

			return null;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(IProperty);
		}
	}
}
