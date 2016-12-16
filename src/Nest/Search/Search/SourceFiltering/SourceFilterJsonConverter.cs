using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class SourceFilterJsonConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var filter = value as ISourceFilter;
			if (filter == null)
				return;
			if (filter.Disable)
				writer.WriteValue(false);
			else
			{
				writer.WriteStartObject();
				if (filter.Include != null)
				{
					writer.WritePropertyName("include");
					serializer.Serialize(writer, filter.Include);
				}
				if (filter.Exclude != null)
				{
					writer.WritePropertyName("exclude");
					serializer.Serialize(writer, filter.Exclude);
				}
				writer.WriteEndObject();
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;

			var filter = new SourceFilter();
			switch (reader.TokenType)
			{
				case JsonToken.Boolean:
					filter.Disable = !(bool)reader.Value;
					break;
				case JsonToken.String:
					filter.Include = new[] { (string)reader.Value };
					break;
				case JsonToken.StartArray:
					var include = new List<string>();
					while (reader.Read() && reader.TokenType != JsonToken.EndArray)
						include.Add((string)reader.Value);
					filter.Include = include.ToArray();
					break;
				default:
					serializer.Populate(reader, filter);
					break;
			}

			return filter;
		}
	}
}
