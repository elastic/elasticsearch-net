using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	internal class TermsExcludeJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;

			TermsExclude termsExclude;
			switch (reader.TokenType)
			{
				case JsonToken.StartArray:
					termsExclude = new TermsExclude(serializer.Deserialize<IEnumerable<string>>(reader));
					break;
				case JsonToken.String:
					termsExclude = new TermsExclude((string)reader.Value);
					break;
				default:
					throw new JsonSerializationException($"Unexpected token {reader.TokenType} when deserializing {nameof(TermsInclude)}");
			}

			return termsExclude;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var termsExclude = (TermsExclude)value;

			if (termsExclude == null)
				writer.WriteNull();
			else if (termsExclude.Values != null)
				serializer.Serialize(writer, termsExclude.Values);
			else
				writer.WriteValue(termsExclude.Pattern);
		}
	}
}