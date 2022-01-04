// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Buffers;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Aggregations;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

/// <summary>The built in internal serializer that the high level client Elastic.Clients.Elasticsearch uses.</summary>
internal class DefaultHighLevelSerializer : SerializerBase
{
	private static readonly UTF8Encoding Encoding = new(false);
	private readonly IElasticsearchClientSettings _settings;

	public DefaultHighLevelSerializer(JsonSerializerOptions? options = null) => Options =
		options ?? new JsonSerializerOptions
		{
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
			Converters =
			{
					//new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
					new DictionaryConverter(),
					new UnionConverter()
			}
		};

	// ctor added so we can pass down settings. TODO: review this design, perhaps have a method AddConverter which can be called instead?
	public DefaultHighLevelSerializer(IElasticsearchClientSettings settings)
	{
		Options = new JsonSerializerOptions
		{
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
			IncludeFields = true,
			Converters =
				{
					//new InterfaceConverterFactory(settings),
					//new ConvertAsConverterFactory(settings),
					//new SourceConverterFactory(settings),
					//new ReadOnlyIndexNameDictionaryConverter(settings),
					new ReadOnlyIndexNameDictionaryConverterFactory(settings),
					new CalendarIntervalConverter(),
					new IndexNameConverter(settings),
					new ObjectToInferredTypesConverter(),
					new IdConverter(settings),
					new FieldConverter(settings),
					new FieldValuesConverter(settings),
					new SortCollectionConverter(settings),
					new LazyDocumentConverter(settings),
					new RelationNameConverter(settings),
					//new FieldNameQueryConverterFactory(settings),
					new CustomJsonWriterConverterFactory(settings),
					new RoutingConverter(settings),
					new SelfSerializableConverterFactory(settings),
					new SelfDeserializableConverterFactory(settings),
					new IndicesJsonConverter(settings),
					//new FieldConverterFactory(settings),
					//new JsonStringEnumConverter(),  //required for source serialisation
					
					new DictionaryConverter(),
					//new BucketsConverterFactory(),
					new UnionConverter()
				},
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};
		_settings = settings;
	}

	internal JsonSerializerOptions Options { get; }

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

#if !NET6_0_OR_GREATER
	internal static ReadBufferState ReadFromStream(
			Stream utf8Json,
			ReadBufferState bufferState)
	{
		while (true)
		{
			var bytesRead = utf8Json.Read(
			bufferState.Buffer, bufferState.BytesInBuffer, bufferState.Buffer.Length - bufferState.BytesInBuffer);

			if (bytesRead == 0)
			{
				bufferState.IsFinalBlock = true;
				break;
			}

			bufferState.BytesInBuffer += bytesRead;

			if (bufferState.BytesInBuffer == bufferState.Buffer.Length)
			{
				break;
			}
		}

		return bufferState;
	}
#endif

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
		if (data is IStreamSerializable streamSerializable)
		{
			streamSerializable.Serialize(writableStream, _settings, formatting);
			return;
		}

		using var writer = new Utf8JsonWriter(writableStream);
		JsonSerializer.Serialize(writer, data, typeof(T), Options);
	}

	public override Task SerializeAsync<T>(T data, Stream stream,
		SerializationFormatting formatting = SerializationFormatting.None,
		CancellationToken cancellationToken = default)
	{
		if (data is IStreamSerializable streamSerializable)
		{
			return streamSerializable.SerializeAsync(stream, _settings, formatting);
		}

		return JsonSerializer.SerializeAsync(stream, data, Options, cancellationToken);
	}
}

internal class ThrowHelper
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	public static void ThrowJsonException(string? message = null) => throw new JsonException(message);
}

#if !NET6_0_OR_GREATER
// Borrowed and slightly adapted from System.Text.Json
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
internal struct ReadBufferState : IDisposable
{
	private const int Utf8BomLength = 3;

	public byte[] Buffer;
	public int BytesInBuffer;
	public int ClearMax;
	public bool IsFirstIteration;
	public bool IsFinalBlock;

	public ReadBufferState(int defaultBufferSize)
	{
		Buffer = ArrayPool<byte>.Shared.Rent(Math.Max(defaultBufferSize, Utf8BomLength));
		BytesInBuffer = ClearMax = 0;
		IsFirstIteration = true;
		IsFinalBlock = false;
	}

	public void Dispose()
	{
		// Clear only what we used and return the buffer to the pool
		new Span<byte>(Buffer, 0, ClearMax).Clear();

		var toReturn = Buffer;
		Buffer = null!;

		ArrayPool<byte>.Shared.Return(toReturn);
	}
}
#endif
