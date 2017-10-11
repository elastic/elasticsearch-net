using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	internal class ChildrenJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var children = value as Children;
			if (children == null || children.Count == 0)
			{
				writer.WriteNull();
				return;
			}
			var settings = serializer.GetConnectionSettings();
			var resolved = children.Cast<IUrlParameter>().ToList();
			if (resolved.Count == 1)
			{
				writer.WriteValue(resolved[0].GetString(settings));
				return;
			}
			writer.WriteStartArray();
			foreach(var r in resolved)
				writer.WriteValue(r.GetString(settings));
			writer.WriteEndArray();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var c = new Children();
			if (reader.TokenType == JsonToken.String)
			{
				var type = reader.Value.ToString();
				c.Add(type);
				return c;
			}
			if (reader.TokenType != JsonToken.StartArray) return null;
			var types = new List<TypeName> { };
			while (reader.TokenType != JsonToken.EndArray)
			{
				var type = reader.ReadAsString();
				if (reader.TokenType == JsonToken.String)
					types.Add(type);
			}
			c.AddRange(types);
			return c;
		}

	}
}
