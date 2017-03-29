using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	internal class TermsIncludeJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;

			TermsInclude termsInclude;
			switch (reader.TokenType)
			{
				case JsonToken.StartArray:
					termsInclude =new TermsInclude(serializer.Deserialize<IEnumerable<string>>(reader));
					break;
				case JsonToken.StartObject:
					long partition = 0;
					long numberOfPartitions = 0;
					while (reader.TokenType != JsonToken.EndObject)
					{
						reader.Read();
						if (reader.TokenType == JsonToken.PropertyName)
						{
							var propertyName = (string)reader.Value;
							switch (propertyName)
							{
								case "partition":
									partition = Convert.ToInt64(reader.ReadAsDouble());
									break;
								case "num_partitions":
									numberOfPartitions = Convert.ToInt64(reader.ReadAsDouble());
									break;
							}
						}
					}
					termsInclude = new TermsInclude(partition, numberOfPartitions);
					break;
				case JsonToken.String:
					termsInclude = new TermsInclude((string)reader.Value);
					break;
				default:
					throw new JsonSerializationException($"Unexpected token {reader.TokenType} when deserializing {nameof(TermsInclude)}");
			}

			return termsInclude;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var termsIncludeExclude = (TermsInclude)value;

			if (termsIncludeExclude == null)
				writer.WriteNull();
			else if (termsIncludeExclude.Values != null)
				serializer.Serialize(writer, termsIncludeExclude.Values);
			else if (termsIncludeExclude.Partition.HasValue && termsIncludeExclude.NumberOfPartitions.HasValue)
			{
				writer.WriteStartObject();
				writer.WritePropertyName("partition");
				writer.WriteValue(termsIncludeExclude.Partition);
				writer.WritePropertyName("num_partitions");
				writer.WriteValue(termsIncludeExclude.NumberOfPartitions);
				writer.WriteEndObject();
			}
			else
				writer.WriteValue(termsIncludeExclude.Pattern);
		}
	}
}
