using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(SignificantTermsIncludeExcludeJsonConverter))]
	public class SignificantTermsIncludeExclude
	{
		[JsonIgnore]
		public string Pattern { get; set; }

		[JsonIgnore]
		public IEnumerable<string> Values { get; set; }

		public SignificantTermsIncludeExclude(string pattern)
		{
			Pattern = pattern;
		}

		public SignificantTermsIncludeExclude(IEnumerable<string> values)
		{
			Values = values;
		}
	}

	internal class SignificantTermsIncludeExcludeJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;

			SignificantTermsIncludeExclude termsInclude;

			switch (reader.TokenType)
			{
				case JsonToken.StartArray:
					termsInclude = new SignificantTermsIncludeExclude(serializer.Deserialize<IEnumerable<string>>(reader));
					break;
				case JsonToken.String:
					termsInclude = new SignificantTermsIncludeExclude((string)reader.Value);
					break;
				default:
					throw new JsonSerializationException(
						$"Unexpected token {reader.TokenType} when deserializing {nameof(SignificantTermsIncludeExclude)}");
			}

			return termsInclude;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var termsIncludeExclude = (SignificantTermsIncludeExclude)value;

			if (termsIncludeExclude == null)
				writer.WriteNull();
			else if (termsIncludeExclude.Values != null)
				serializer.Serialize(writer, termsIncludeExclude.Values);
			else
				writer.WriteValue(termsIncludeExclude.Pattern);
		}
	}
}
