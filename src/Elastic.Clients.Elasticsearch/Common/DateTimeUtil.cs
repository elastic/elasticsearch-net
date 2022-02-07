// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

internal static class DateTimeUtil
{
	public static readonly DateTimeOffset UnixEpoch = new(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);
}

[JsonConverter(typeof(EpochMillisConverter))]
public readonly struct EpochMillis
{
	public static EpochMillis Zero = new(0);

	public EpochMillis() => MillisecondsSinceEpoch = 0;

	public EpochMillis(long millisecondsSinceEpoch) => MillisecondsSinceEpoch = millisecondsSinceEpoch;

	public static EpochMillis Parse(string millisecondsSinceEpoch)
	{
		if (!long.TryParse(millisecondsSinceEpoch, out var parsedValue))
			throw new InvalidOperationException($"Unable to parse value '{millisecondsSinceEpoch}' to epoch milliseconds.");
				
		return new EpochMillis(parsedValue);
	}

	public long MillisecondsSinceEpoch { get; private init; }

	public DateTimeOffset DateTimeOffset => DateTimeUtil.UnixEpoch.AddMilliseconds(MillisecondsSinceEpoch);
}

internal sealed class EpochMillisConverter : JsonConverter<EpochMillis>
{
	public override EpochMillis Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.String)
		{
			var valueAsString = reader.GetString();
			return EpochMillis.Parse(valueAsString);
		}
		else if (reader.TokenType == JsonTokenType.Number)
		{
			var value = reader.GetInt64();
			return new EpochMillis(value);
		}

		throw new JsonException("Value could not be parsed to EpochMillis");
	}

	public override void Write(Utf8JsonWriter writer, EpochMillis value, JsonSerializerOptions options) => writer.WriteNumberValue(value.MillisecondsSinceEpoch);
}
