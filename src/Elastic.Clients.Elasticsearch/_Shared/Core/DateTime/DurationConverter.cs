// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Json;

public sealed class DurationConverter : JsonConverter<Duration>
{
	public override Duration? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var token = reader.TokenType;

		switch (token)
		{
			case JsonTokenType.String:
				return new Duration(reader.GetString());

			case JsonTokenType.Number:
				var milliseconds = reader.GetInt64();
				if (milliseconds == -1)
					return Duration.MinusOne;
				if (milliseconds == 0)
					return Duration.Zero;
				return new Duration(milliseconds);

			default:
				return null;
		}
	}

	public override void Write(Utf8JsonWriter writer, Duration value, JsonSerializerOptions options)
	{
		if (value == Duration.MinusOne)
			writer.WriteNumberValue(-1);
		else if (value == Duration.Zero)
			writer.WriteNumberValue(0);
		else if (value.Factor.HasValue && value.Interval.HasValue)
			writer.WriteStringValue(value.ToString());
		else if (value.Milliseconds != null)
			writer.WriteNumberValue((long)value.Milliseconds);
	}
}
