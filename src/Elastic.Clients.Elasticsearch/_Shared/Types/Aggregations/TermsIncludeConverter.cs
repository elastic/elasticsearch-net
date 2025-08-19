// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#nullable enable

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Aggregations.Json;

public sealed class TermsIncludeConverter : JsonConverter<TermsInclude>
{
	public override TermsInclude? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		switch (reader.TokenType)
		{
			case JsonTokenType.StartArray:
				return new TermsInclude(reader.ReadCollectionValue<string>(options, null)!);

			case JsonTokenType.StartObject:
				long partition = 0;
				long numberOfPartitions = 0;
				while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
				{
					if (reader.TokenType == JsonTokenType.PropertyName)
					{
						var propertyName = reader.GetString();
						reader.Read();
						switch (propertyName)
						{
							case "partition":
								partition = reader.GetInt64();
								break;

							case "num_partitions":
								numberOfPartitions = reader.GetInt64();
								break;

							default:
								throw new JsonException($"Unexpected property name '{propertyName}' encountered when deserializing TermsInclude.");
						}
					}
				}
				return new TermsInclude(partition, numberOfPartitions);

			case JsonTokenType.String:
				return new TermsInclude(reader.ReadValue<string>(options)!);

			default:
				throw new JsonException($"Unexpected token {reader.TokenType} when deserializing {nameof(TermsInclude)}");
		}
	}

	public override void Write(Utf8JsonWriter writer, TermsInclude value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			throw new ArgumentNullException(nameof(value));
		}

		if (value.Values is not null)
		{
			writer.WriteCollectionValue(options, value.Values, null);
			return;
		}

		if (value.Partition.HasValue && value.NumberOfPartitions.HasValue)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("partition");
			writer.WriteNumberValue(value.Partition.Value);
			writer.WritePropertyName("num_partitions");
			writer.WriteNumberValue(value.NumberOfPartitions.Value);
			writer.WriteEndObject();
			return;
		}

		writer.WriteStringValue(value.RegexPattern);
	}
}
