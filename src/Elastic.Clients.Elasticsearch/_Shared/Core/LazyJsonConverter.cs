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
	private IElasticsearchClientSettings? _settings;

	public override LazyJson Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		InitializeSettings(options);

#pragma warning disable IL2026, IL3050 // The `TypeInfoResolver` for `RequestResponseConverter` knows how to handle `JsonElement`.
		return new LazyJson(JsonSerializer.Deserialize<JsonElement>(ref reader, options), _settings!);
#pragma warning restore IL2026, IL3050
	}

	private void InitializeSettings(JsonSerializerOptions options)
	{
		if (_settings is not null)
		{
			return;
		}

		if (!ContextProvider<IElasticsearchClientSettings>.TryGetContext(options, out _settings))
		{
			ThrowHelper.ThrowJsonExceptionForMissingSettings();
		}
	}

	public override void Write(Utf8JsonWriter writer, LazyJson value, JsonSerializerOptions options)
	{
		throw new NotImplementedException("We only ever expect to deserialize LazyJson on responses.");
	}
}
