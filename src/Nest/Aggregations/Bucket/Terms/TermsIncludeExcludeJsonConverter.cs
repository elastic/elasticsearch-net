using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	internal class TermsIncludeExcludeJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;

			var termsInclude = new TermsIncludeExclude();

			switch (reader.TokenType)
			{
				case JsonToken.StartArray:
					termsInclude.Values = serializer.Deserialize<IEnumerable<string>>(reader);
					break;
				case JsonToken.String:
					termsInclude.Pattern = (string)reader.Value;
					break;
				default:
					throw new JsonSerializationException($"Unexpected token {reader.TokenType} when deserializing {nameof(TermsIncludeExclude)}");
			}

			return termsInclude;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var termsIncludeExclude = (TermsIncludeExclude)value;

			if (termsIncludeExclude == null)
				writer.WriteNull();
			else if (termsIncludeExclude.Values != null)
				serializer.Serialize(writer, termsIncludeExclude.Values);
			else
			{
				writer.WriteValue(termsIncludeExclude.Pattern);
			}
		}
	}
}
