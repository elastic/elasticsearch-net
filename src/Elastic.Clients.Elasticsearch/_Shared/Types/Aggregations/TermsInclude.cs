// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable enable
namespace Elastic.Clients.Elasticsearch.Aggregations;

/// <summary>
/// Filters which terms to include in the response.
/// </summary>
[JsonConverter(typeof(TermsIncludeConverter))]
public class TermsInclude
{
	/// <summary>
	/// Creates an instance of <see cref="TermsInclude" /> that uses a regular expression pattern
	/// to determine the terms to include in the response.
	/// </summary>
	/// <param name="regexPattern">The regular expression pattern.</param>
	public TermsInclude(string regexPattern)
	{
		if (regexPattern is null)
			throw new ArgumentNullException(nameof(regexPattern));

		RegexPattern = regexPattern;
	}

	/// <summary>
	/// Creates an instance of <see cref="TermsInclude" /> that uses a collection of terms
	/// to include in the response.
	/// </summary>
	/// <param name="values">The exact terms to include.</param>
	public TermsInclude(IEnumerable<string> values)
	{
		if (values is null)
			throw new ArgumentNullException(nameof(values));

		Values = values;
	}

	/// <summary>
	/// Creates an instance of <see cref="TermsInclude" /> that partitions the terms into a number of
	/// partitions to receive in multiple requests. Used to process many unique terms.
	/// </summary>
	/// <param name="partition">The 0-based partition number for this request.</param>
	/// <param name="numberOfPartitions">The total number of partitions.</param>
	public TermsInclude(long partition, long numberOfPartitions)
	{
		Partition = partition;
		NumberOfPartitions = numberOfPartitions;
	}

	/// <summary>
	/// The total number of partitions we are interested in.
	/// </summary>
	public long? NumberOfPartitions { get; }

	/// <summary>
	/// The current partition of terms we are interested in.
	/// </summary>
	public long? Partition { get; }

	/// <summary>
	/// The regular expression pattern to determine terms to include in the response.
	/// </summary>
	public string? RegexPattern { get; }

	/// <summary>
	/// Collection of terms to include in the response.
	/// </summary>
	public IEnumerable<string>? Values { get; }
}

internal sealed class TermsIncludeConverter : JsonConverter<TermsInclude>
{
	public override TermsInclude? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.Null)
		{
			reader.Read();
			return null;
		}

		TermsInclude termsInclude;

		switch (reader.TokenType)
		{
			case JsonTokenType.StartArray:
				var terms = JsonSerializer.Deserialize<string[]>(ref reader, options) ?? Array.Empty<string>();
				termsInclude = new TermsInclude(terms);
				break;
			case JsonTokenType.StartObject:
				long partition = 0;
				long numberOfPartitions = 0;
				while(reader.Read() && reader.TokenType != JsonTokenType.EndObject)
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
				termsInclude = new TermsInclude(partition, numberOfPartitions);
				break;
			case JsonTokenType.String:
				var regex = reader.GetString();
				termsInclude = new TermsInclude(regex!);
				break;
			default:
				throw new JsonException($"Unexpected token {reader.TokenType} when deserializing {nameof(TermsInclude)}");
		}

		return termsInclude;
	}

	public override void Write(Utf8JsonWriter writer, TermsInclude value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		if (value.Values is not null)
		{
			JsonSerializer.Serialize(writer, value.Values, options);
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
