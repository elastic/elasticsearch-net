using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class PropertyJsonConverter : JsonConverter
	{
		public override bool CanWrite
		{
			get { return false; }
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject o = JObject.Load(reader);
			JToken typeToken;
			JToken propertiesToken;
			var type = string.Empty;
			var hasType = o.TryGetValue("type", out typeToken);
			if (hasType)
				type = typeToken.Value<string>().ToLowerInvariant();
			else if (o.TryGetValue("properties", out propertiesToken))
				type = "object";

			serializer.TypeNameHandling = TypeNameHandling.None;

			switch (type)
			{
				case "text":
					return o.ToObject<TextProperty>();
				case "keyword":
					return o.ToObject<KeywordProperty>();
				case "string":
					return o.ToObject<StringProperty>();
				case "float":
				case "double":
				case "byte":
				case "short":
				case "integer":
				case "long":
					return o.ToObject<NumberProperty>();
				case "date":
					return o.ToObject<DateProperty>();
				case "boolean":
					return o.ToObject<BooleanProperty>();
				case "binary":
					return o.ToObject<BinaryProperty>();
				case "object":
					return o.ToObject<ObjectProperty>();
				case "nested":
					return o.ToObject<NestedProperty>();
				case "ip":
					return o.ToObject<IpProperty>();
				case "geo_point":
					return o.ToObject<GeoPointProperty>();
				case "geo_shape":
					return o.ToObject<GeoShapeProperty>();
				case "attachment":
					return o.ToObject<AttachmentProperty>();
				case "completion":
					return o.ToObject<CompletionProperty>();
				case "token_count":
					return o.ToObject<TokenCountProperty>();
				case "murmur3":
					return o.ToObject<Murmur3HashProperty>();
			}

			return null;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(IProperty);
		}
	}
}
