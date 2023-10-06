// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// <para>Lazily deserializable JSON.</para>
/// <para>Holds raw JSON bytes which can be lazily deserialized to a specific <see cref="Type"/> using the source serializer at a later time.</para>
/// </summary>
[JsonConverter(typeof(LazyJsonConverter))]
public readonly struct LazyJson
{
	internal LazyJson(byte[] bytes, IElasticsearchClientSettings settings)
	{
		Bytes = bytes;
		Settings = settings;
	}

	internal byte[]? Bytes { get; }
	internal IElasticsearchClientSettings? Settings { get; }

	/// <summary>
	/// Creates an instance of <typeparamref name="T" /> from this
	/// <see cref="LazyJson" /> instance.
	/// </summary>
	/// <typeparam name="T">The type</typeparam>
	public T? As<T>()
	{
		if (Bytes is null || Settings is null || Bytes.Length == 0)
			return default;

		using var ms = Settings.MemoryStreamFactory.Create(Bytes);
		return Settings.SourceSerializer.Deserialize<T>(ms);
	}
}

internal sealed class LazyJsonConverter : JsonConverter<LazyJson>
{
	private IElasticsearchClientSettings _settings;

	public override LazyJson Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		InitializeSettings(options);

		using var jsonDoc = JsonSerializer.Deserialize<JsonDocument>(ref reader);
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
