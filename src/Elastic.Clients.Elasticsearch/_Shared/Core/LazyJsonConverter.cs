// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Json;

public sealed class LazyJsonConverter : JsonConverter<LazyJson>
{
	private IElasticsearchClientSettings _settings;

	public override LazyJson Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		InitializeSettings(options);

		// TODO: fixme
#pragma warning disable IL2026, IL3050
		using var jsonDoc = JsonSerializer.Deserialize<JsonDocument>(ref reader);
#pragma warning restore IL2026, IL3050
		using var stream = _settings.MemoryStreamFactory.Create();

		var writer = new Utf8JsonWriter(stream);
		jsonDoc.WriteTo(writer);
		writer.Flush();

		return new LazyJson(stream.ToArray(), _settings);
	}

	public override void Write(Utf8JsonWriter writer, LazyJson value, JsonSerializerOptions options) => throw new NotImplementedException("We only ever expect to deserialize LazyJson on responses.");

	private void InitializeSettings(JsonSerializerOptions options)
	{
		if (_settings is null)
		{
			if (!options.TryGetClientSettings(out var settings))
				ThrowHelper.ThrowJsonExceptionForMissingSettings();

			_settings = settings;
		}
	}
}
