// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Json;

public sealed class NumberConverter :
	JsonConverter<Number>
{
	public override Number Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		reader.ValidateToken(JsonTokenType.Number);

		if (reader.TryGetDouble(out var d))
		{
			return new(d);
		}

		if (reader.TryGetInt64(out var l))
		{
			return new(l);
		}

		throw new JsonException("Could not read JSON value as number.");
	}

	public override void Write(Utf8JsonWriter writer, Number value, JsonSerializerOptions options)
	{
		if (value.TryGetDouble(out var d))
		{
			writer.WriteNumberValue(d);
			return;
		}

		if (value.TryGetLong(out var l))
		{
			writer.WriteNumberValue(l);
			return;
		}

		throw new JsonException($"The '{nameof(Number)}' does not contain a value.");
	}
}
