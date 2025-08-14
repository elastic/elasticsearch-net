// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#nullable enable

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Aggregations.Json;

public sealed class BucketsPathConverter : JsonConverter<BucketsPath>
{
	public override BucketsPath? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return (reader.TokenType) switch
		{
			JsonTokenType.Null => null,
			JsonTokenType.String => BucketsPath.Single(reader.ReadValue<string>(options)!),
			JsonTokenType.StartArray => BucketsPath.Array(reader.ReadCollectionValue<string>(options, null)!.ToArray()),
			JsonTokenType.StartObject => BucketsPath.Dictionary(reader.ReadDictionaryValue<string, string>(options, null, null)!),
			_ => throw new JsonException($"Unexpected token '{reader.TokenType}'.")
		};
	}

	public override void Write(Utf8JsonWriter writer, BucketsPath value, JsonSerializerOptions options)
	{
		switch (value._kind)
		{
			case BucketsPath.Kind.Single:
				writer.WriteStringValue((string)value._value);
				break;

			case BucketsPath.Kind.Array:
				writer.WriteCollectionValue(options, (string[])value._value, null);
				break;

			case BucketsPath.Kind.Dictionary:
				writer.WriteDictionaryValue(options, (Dictionary<string, string>)value._value, null, null);
				break;

			default:
				throw new ArgumentOutOfRangeException();
		}
	}
}
