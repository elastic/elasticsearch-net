// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch;

internal sealed class FieldConverter :
	JsonConverter<Field>
{
	public override Field Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		reader.ValidateToken(JsonTokenType.String);

		return new Field(reader.GetString()!);
	}

	public override void Write(Utf8JsonWriter writer, Field value, JsonSerializerOptions options)
	{
		var settings = options.GetContext<IElasticsearchClientSettings>();
		var fieldName = settings.Inferrer.Field(value);

		writer.WriteStringValue(fieldName);
	}

	public override Field ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		reader.ValidateToken(JsonTokenType.PropertyName);

		return new Field(reader.GetString()!);
	}

	public override void WriteAsPropertyName(Utf8JsonWriter writer, Field value, JsonSerializerOptions options)
	{
		var settings = options.GetContext<IElasticsearchClientSettings>();

		writer.WritePropertyName(settings.Inferrer.Field(value));
	}
}
