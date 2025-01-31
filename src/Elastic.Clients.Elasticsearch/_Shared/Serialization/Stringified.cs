// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class StringifiedLongConverter : JsonConverter<long>
{
	public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => ReadStringifiedLong(ref reader);

	public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options) => writer.WriteNumberValue(value);

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

internal sealed class StringifiedIntegerConverter : JsonConverter<int>
{
	public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => ReadStringifiedInteger(ref reader);

	public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options) => writer.WriteNumberValue(value);

	public static int ReadStringifiedInteger(ref Utf8JsonReader reader)
	{
		if (reader.TokenType == JsonTokenType.PropertyName)
			reader.Read();

		if (reader.TokenType == JsonTokenType.String)
		{
			var intString = reader.GetString();

			if (!int.TryParse(intString, out var intValue))
			{
				throw new JsonException("Unable to parse string value to integer.");
			}

			return intValue;
		}

		return reader.GetInt32();
	}
}

internal sealed class StringifiedBoolConverter : JsonConverter<bool>
{
	public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => ReadStringifiedBool(ref reader);

	public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) => writer.WriteBooleanValue(value);

	public static bool ReadStringifiedBool(ref Utf8JsonReader reader)
	{
		if (reader.TokenType == JsonTokenType.PropertyName)
			reader.Read();

		if (reader.TokenType == JsonTokenType.String)
		{
			var boolString = reader.GetString();

			if (!bool.TryParse(boolString, out var boolValue))
			{
				throw new JsonException("Unable to parse string value to bool.");
			}

			return boolValue;
		}

		return reader.GetBoolean();
	}
}
