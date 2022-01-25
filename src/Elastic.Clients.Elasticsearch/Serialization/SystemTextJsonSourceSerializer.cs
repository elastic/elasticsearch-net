// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

public abstract class SourceSerializer : Serializer
{
}

public abstract class SystemTextJsonSourceSerializer : SourceSerializer
{
	//private readonly SerializerRegistrationInformation _state;

	// TODO - Implement diagnostics - Types are internal to transport, so either, return to wrapped version, or move to client / implement manually.
	//private static DiagnosticSource DiagnosticSource { get; } = new DiagnosticListener(Serializer.SourceName);

	//protected SystemTextJsonSourceSerializer() => _state = new SerializerRegistrationInformation(GetType(), "source");

	public JsonSerializerOptions Options { get; init; }

	public override T Deserialize<T>(Stream stream)
	{
		if (TryReturnDefault(stream, out T deserialize))
			return deserialize;

		return JsonSerializer.Deserialize<T>(stream, Options);
	}

	public override object Deserialize(Type type, Stream stream)
	{
		if (TryReturnDefault(stream, out object deserialize))
			return deserialize;

		return JsonSerializer.Deserialize(stream, type, Options);
	}

	private static bool TryReturnDefault<T>(Stream stream, out T deserialize)
	{
		deserialize = default;
		return stream == null || stream == Stream.Null || (stream.CanSeek && stream.Length == 0);
	}

	public override ValueTask<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default) => JsonSerializer.DeserializeAsync<T>(stream, Options, cancellationToken);

	public override ValueTask<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default) =>
		JsonSerializer.DeserializeAsync(stream, type, Options, cancellationToken);

	public override void Serialize<T>(T data, Stream writableStream,
		SerializationFormatting formatting = SerializationFormatting.None)
	{
		using var writer = new Utf8JsonWriter(writableStream);
		JsonSerializer.Serialize(writer, data, typeof(T), Options);
	}

	public override Task SerializeAsync<T>(T data, Stream stream,
		SerializationFormatting formatting = SerializationFormatting.None,
		CancellationToken cancellationToken = default) => JsonSerializer.SerializeAsync(stream, data, Options, cancellationToken);
}
