using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(IncludeExcludeJsonConverter))]
	public class IncludeExclude
	{
		[JsonIgnore]
		public string Pattern { get; set; }

		[JsonIgnore]
		public IEnumerable<string> Values { get; set; }

		public IncludeExclude(string pattern) => Pattern = pattern;

		public IncludeExclude(IEnumerable<string> values) => Values = values;
	}

	internal class IncludeExcludeJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;

			IncludeExclude termsInclude;

			switch (reader.TokenType)
			{
				case JsonToken.StartArray:
					termsInclude = new IncludeExclude(serializer.Deserialize<IEnumerable<string>>(reader));
					break;
				case JsonToken.String:
					termsInclude = new IncludeExclude((string)reader.Value);
					break;
				default:
					throw new JsonSerializationException(
						$"Unexpected token {reader.TokenType} when deserializing {nameof(IncludeExclude)}");
			}

			return termsInclude;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var termsIncludeExclude = (IncludeExclude)value;

			if (termsIncludeExclude == null)
				writer.WriteNull();
			else if (termsIncludeExclude.Values != null)
				serializer.Serialize(writer, termsIncludeExclude.Values);
			else
				writer.WriteValue(termsIncludeExclude.Pattern);
		}
	}
}
