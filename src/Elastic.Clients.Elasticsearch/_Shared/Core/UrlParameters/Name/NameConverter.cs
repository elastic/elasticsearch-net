// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Json;

public sealed class NameConverter :
	JsonConverter<Name>
{
	public override Name? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		reader.ValidateToken(JsonTokenType.String);

		return reader.GetString()!;
	}

	public override Name ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert,
		JsonSerializerOptions options)
	{
		reader.ValidateToken(JsonTokenType.PropertyName);

		return reader.GetString()!;
	}

	public override void Write(Utf8JsonWriter writer, Name value, JsonSerializerOptions options)
	{
		if (value?.Value is null)
		{
			throw new ArgumentNullException(nameof(value));
		}

		writer.WriteStringValue(value.Value);
	}

	public override void WriteAsPropertyName(Utf8JsonWriter writer, Name value, JsonSerializerOptions options)
	{
		if (value?.Value is null)
		{
			throw new ArgumentNullException(nameof(value));
		}

		writer.WritePropertyName(value.Value);
	}
}
