// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Json;

public sealed class PropertyNameConverter :
	JsonConverter<PropertyName>
{
	public override PropertyName Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		reader.ValidateToken(JsonTokenType.String);

		return new PropertyName(reader.GetString()!);
	}

	public override void Write(Utf8JsonWriter writer, PropertyName value, JsonSerializerOptions options)
	{
		var settings = options.GetContext<IElasticsearchClientSettings>();
		var fieldName = settings.Inferrer.PropertyName(value);

		writer.WriteStringValue(fieldName);
	}

	public override PropertyName ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		reader.ValidateToken(JsonTokenType.PropertyName);

		return new PropertyName(reader.GetString()!);
	}

	public override void WriteAsPropertyName(Utf8JsonWriter writer, PropertyName value, JsonSerializerOptions options)
	{
		var settings = options.GetContext<IElasticsearchClientSettings>();

		writer.WritePropertyName(settings.Inferrer.PropertyName(value));
	}
}
