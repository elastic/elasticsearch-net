// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

using Elastic.Transport.Extensions;

internal sealed class SourceConverter<T> : JsonConverter<SourceMarker<T>>
{
	private readonly IElasticsearchClientSettings _settings;

	public SourceConverter(IElasticsearchClientSettings settings) => _settings = settings;

	public override SourceMarker<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new()
	{
		Source = _settings.SourceSerializer.Deserialize<T>(ref reader, null)
	};

	public override void Write(Utf8JsonWriter writer, SourceMarker<T> value, JsonSerializerOptions options) => _settings.SourceSerializer.Serialize(value.Source, writer, null);
}
