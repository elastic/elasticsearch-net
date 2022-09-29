// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

internal sealed class StringifiedLongConverter : JsonConverter<long?>
{
	public override long? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => ReadStringifiedLong(ref reader);

	public override void Write(Utf8JsonWriter writer, long? value, JsonSerializerOptions options) => writer.WriteNumberValue(value.Value);

	public static long ReadStringifiedLong(ref Utf8JsonReader reader)
	{
		if (reader.TokenType == JsonTokenType.PropertyName)
			reader.Read();

		if (reader.TokenType == JsonTokenType.String)
		{
			var longString = reader.GetString();

			if (!long.TryParse(longString, out var longValue))
			{
				throw new JsonException("Unable to parse string value to long.");
			}

			return longValue;
		}

		return reader.GetInt64();

	}
}
