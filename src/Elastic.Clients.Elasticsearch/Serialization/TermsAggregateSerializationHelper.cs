// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text;
using Elastic.Clients.Elasticsearch.Aggregations;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal static class TermsAggregateSerializationHelper
{
	private static readonly byte[] s_buckets = Encoding.UTF8.GetBytes("buckets");
	private static readonly byte[] s_key = Encoding.UTF8.GetBytes("key");
	private static readonly byte s_period = (byte)'.';

	public static bool TryDeserializeTermsAggregate(string aggregateName, ref Utf8JsonReader reader, JsonSerializerOptions options, out IAggregate? aggregate)
	{
		aggregate = null;

		// We take a copy here so we can read forward to establish the term key type before we resume with final deserialization.
		var readerCopy = reader;

		if (JsonHelper.TryReadUntilStringPropertyValue(ref readerCopy, s_buckets))
		{
			if (readerCopy.TokenType != JsonTokenType.StartArray)
				throw new Exception("TODO");

			readerCopy.Read();

			if (readerCopy.TokenType == JsonTokenType.EndArray) // We have no buckets
			{
				if (aggregateName.Equals("sterms", StringComparison.Ordinal))
				{
					var agg = JsonSerializer.Deserialize<StringTermsAggregate>(ref reader, options);
					aggregate = agg;
					return true;
				}

				if (aggregateName.Equals("lterms", StringComparison.Ordinal))
				{
					var agg = JsonSerializer.Deserialize<LongTermsAggregate>(ref reader, options);
					aggregate = agg;
					return true;
				}

				if (aggregateName.Equals("dterms", StringComparison.Ordinal))
				{
					var agg = JsonSerializer.Deserialize<DoubleTermsAggregate>(ref reader, options);
					aggregate = agg;
					return true;
				}

				if (aggregateName.Equals("multi_terms", StringComparison.Ordinal))
				{
					var agg = JsonSerializer.Deserialize<DoubleTermsAggregate>(ref reader, options);
					aggregate = agg;
					return true;
				}

				throw new JsonException($"Unable to deserialize empty terms aggregate for '{aggregateName}'.");
			}
			else
			{
				if (readerCopy.TokenType != JsonTokenType.StartObject)
					throw new Exception("TODO"); // TODO!

				if (JsonHelper.TryReadUntilStringPropertyValue(ref readerCopy, s_key))
				{
					if (readerCopy.TokenType == JsonTokenType.String)
					{
						var agg = JsonSerializer.Deserialize<StringTermsAggregate>(ref reader, options);
						aggregate = agg;
						return true;
					}
					else if (readerCopy.TokenType == JsonTokenType.Number)
					{
						var value = readerCopy.ValueSpan; // TODO - May need to check for sequence

						if (value.IndexOf(s_period) > -1 && readerCopy.TryGetDouble(out _))
						{
							var agg = JsonSerializer.Deserialize<DoubleTermsAggregate>(ref reader, options);
							aggregate = agg;
							return true;
						}
						else if (readerCopy.TryGetInt64(out _))
						{
							var agg = JsonSerializer.Deserialize<LongTermsAggregate>(ref reader, options);
							aggregate = agg;
							return true;
						}
					}
					else if (readerCopy.TokenType == JsonTokenType.StartArray)
					{
						var agg = JsonSerializer.Deserialize<MultiTermsAggregate>(ref reader, options);
						aggregate = agg;
						return true;
					}
					else
					{
						throw new JsonException("Unhandled token type when parsing the terms aggregate response");
					}
				}
			}
		}

		return false;
	}
}
