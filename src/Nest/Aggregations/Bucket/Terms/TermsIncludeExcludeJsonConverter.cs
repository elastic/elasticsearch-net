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
				case JsonToken.StartObject:
					while (reader.TokenType != JsonToken.EndObject)
					{
						reader.Read();

						if (reader.TokenType == JsonToken.PropertyName)
						{
							var propertyName = (string)reader.Value;
							switch (propertyName)
							{
								case "partition":
									termsInclude.Partition = Convert.ToInt64(reader.ReadAsDouble());
									break;
								case "num_partitions":
									termsInclude.NumberOfPartitions = Convert.ToInt64(reader.ReadAsDouble());
									break;
								case "pattern":
									termsInclude.Pattern = reader.ReadAsString();
									break;
								case "flags":
									termsInclude.Flags = reader.ReadAsString();
									break;
							}
						}
					}
					break;
				default:
					throw new JsonSerializationException($"Unexpected token {reader.TokenType} when deserializing {nameof(TermsIncludeExclude)}");
			}

			return termsInclude;
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
				if (termsIncludeExclude.Pattern.IsNullOrEmpty())
				{
                    writer.WritePropertyName("partition");
                    writer.WriteValue(termsIncludeExclude.Partition);
                    writer.WritePropertyName("num_partitions");
                    writer.WriteValue(termsIncludeExclude.NumberOfPartitions);
				}
				else
				{
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
}
