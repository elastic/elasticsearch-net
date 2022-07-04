// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	internal sealed class IdConverter : JsonConverter<Id>
	{
		private readonly IElasticsearchClientSettings _settings;

		public IdConverter(IElasticsearchClientSettings settings) => _settings = settings;

		public override void WriteAsPropertyName(Utf8JsonWriter writer, Id value, JsonSerializerOptions options) => writer.WritePropertyName(((IUrlParameter)value).GetString(_settings));

		public override Id ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.GetString();

		public override Id? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
			reader.TokenType == JsonTokenType.Number
				? new Id(reader.GetInt64())
				: new Id(reader.GetString());

		public override void Write(Utf8JsonWriter writer, Id value, JsonSerializerOptions options)
		{
			if (value is null)
			{
				writer.WriteNullValue();
				return;
			}

			if (value.Document is not null)
			{
				var documentId = _settings.Inferrer.Id(value.Document.GetType(), value.Document);
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
}
