using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class TermsIncludeExcludeJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;

		public override bool CanRead => false;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var termsIncludeExclude = (TermsIncludeExclude)value;

			if (termsIncludeExclude.Values != null)
			{
				serializer.Serialize(writer, termsIncludeExclude.Values);
			}
			else
			{
				writer.WriteStartObject();
				writer.WritePropertyName("pattern");
				writer.WriteValue(termsIncludeExclude.Pattern);
				if (!termsIncludeExclude.Flags.IsNullOrEmpty())
				{
					writer.WritePropertyName("flags");
					writer.WriteValue(termsIncludeExclude.Flags);
				}
				writer.WriteEndObject();
			}
		}
	}
}
