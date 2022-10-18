// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

internal sealed class RoutingConverter : JsonConverter<Routing>
{
	private readonly IElasticsearchClientSettings _settings;

	public RoutingConverter(IElasticsearchClientSettings settings) => _settings = settings;

	public override Routing? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
		reader.TokenType == JsonTokenType.Number
			? new Routing(reader.GetInt64())
			: new Routing(reader.GetString());

	public override void Write(Utf8JsonWriter writer, Routing value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		if (value.Document is not null)
		{
			var documentId = _settings.Inferrer.Routing(value.Document.GetType(), value.Document);

			if (documentId is null)
			{
				writer.WriteNullValue();
				return;
			}

			writer.WriteStringValue(documentId);
		}
		else if (value.DocumentGetter is not null)
		{
			var doc = value.DocumentGetter();
			var documentId = _settings.Inferrer.Routing(doc.GetType(), doc);
			writer.WriteStringValue(documentId);
		}
		else if (value.LongValue.HasValue)
		{
			writer.WriteNumberValue(value.LongValue.Value);
		}
		else
		{
			writer.WriteStringValue(value.StringValue);
		}
	}
}
