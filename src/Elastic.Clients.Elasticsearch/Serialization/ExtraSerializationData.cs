// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// This actually does not ever get used as a converter. It's just an ugly hack to provide a way to
/// access out settings in generated converters without requiring a mechanism to conditionally add them.
/// </summary>
internal sealed class ExtraSerializationData : JsonConverter<ExtraSerializationData>
{
	public ExtraSerializationData(IElasticsearchClientSettings settings) => Settings = settings;

	public IElasticsearchClientSettings Settings { get; }

	public override ExtraSerializationData? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
	public override void Write(Utf8JsonWriter writer, ExtraSerializationData value, JsonSerializerOptions options) => throw new NotImplementedException();
}
