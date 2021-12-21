// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch
{
	/// <summary>
	/// Converts an <see cref="IndexName"/> to and from its JSON representation.
	/// </summary>
	internal class IndexNameConverter : JsonConverter<IndexName?>
	{
		private readonly IElasticsearchClientSettings _settings;

		public IndexNameConverter(IElasticsearchClientSettings settings) => _settings = settings;

		public override IndexName? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.String)
			{
				reader.Read();
				return null;
			}

			IndexName? indexName = reader.GetString();
			return indexName;
		}

		public override void Write(Utf8JsonWriter writer, IndexName? value, JsonSerializerOptions options)
		{
			if (value is null)
			{
				writer.WriteNullValue();
				return;
			}

			writer.WriteStringValue(_settings.Inferrer.IndexName(value));
		}
	}
}
