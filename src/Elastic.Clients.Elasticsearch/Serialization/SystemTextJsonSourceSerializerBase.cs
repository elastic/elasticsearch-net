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

public abstract class SystemTextJsonSourceSerializerBase : SerializerBase
{
	public JsonSerializerOptions Options { get; init; }

	public override T Deserialize<T>(Stream stream)
	{
		if (stream.CanSeek && stream.Length == 0)
			return default;

#if NET6_0_OR_GREATER
#pragma warning disable IDE0022 // Use expression body for methods
			return JsonSerializer.Deserialize<T>(stream, Options);
#pragma warning restore IDE0022 // Use expression body for methods
#else

		// This is "safer" but not as efficient due to potentially large string allocations
		using var reader = new StreamReader(stream);
		return (T)JsonSerializer.Deserialize(reader.ReadToEnd(), typeof(T), Options);

		// TODO - Review if we can polyfil with improvements from https://github.com/dotnet/runtime/pull/54632
		// NOTE: This requires many internal types from STJ and is quite complex to achieve cleanly.

		// This is another option but is also quie inefficient and we would need to correctly size the buffer first
		//using var ms = _settings.MemoryStreamFactory.Create();
		//var buffer = ArrayPool<byte>.Shared.Rent(1024);
		//var total = 0;
		//int read;
		//while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
		//{
		//	ms.Write(buffer, 0, read);
		//	total += read;
		//}
		//var span = ms.TryGetBuffer(out var segment)
		//	? new ReadOnlyMemory<byte>(segment.Array, segment.Offset, total).Span
		//	: new ReadOnlyMemory<byte>(ms.ToArray()).Span;

		//return span.Length > 0 ? JsonSerializer.Deserialize<T>(span, Options) : default;
#endif
	}

	public override object Deserialize(Type type, Stream stream)
	{
		using var reader = new StreamReader(stream);
		return JsonSerializer.Deserialize(reader.ReadToEnd(), type, Options);
	}

	// TODO - Return ValueTask?
	public override Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default) => JsonSerializer.DeserializeAsync<T>(stream, Options, cancellationToken).AsTask();

	public override Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default) =>
		JsonSerializer.DeserializeAsync(stream, type, Options, cancellationToken).AsTask();

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
