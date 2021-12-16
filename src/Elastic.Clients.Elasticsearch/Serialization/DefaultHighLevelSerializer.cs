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
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	/// <summary>The built in internal serializer that the high level client Elastic.Clients.Elasticsearch uses.</summary>
	internal class DefaultHighLevelSerializer : Serializer
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
				IncludeFields= true,
				Converters =
				{
					//new InterfaceConverterFactory(settings),
					//new ConvertAsConverterFactory(settings),
					new IndexNameConverter(settings),
					new ObjectToInferredTypesConverter(),
					new IdConverter(settings),
					new FieldConverter(settings),
					new SortCollectionConverter(settings),
					//new FieldNameQueryConverterFactory(settings),
					new CustomJsonWriterConverterFactory(settings),
					new RoutingConverter(settings),
					new SelfSerializableConverterFactory(settings),
					new IndicesJsonConverter(settings),
					//new FieldConverterFactory(settings),
					new JsonStringEnumConverter(),  //required for source serialisation
					
					new DictionaryConverter(),
					//new BucketsConverterFactory(),
					new UnionConverter()
				},
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			};
			_settings = settings;
		}

		internal JsonSerializerOptions Options { get; }

		// TODO - This is not ideal as we allocate a large string - No stream based sync overload - We should use a pooled byte array in the future
		public override T Deserialize<T>(Stream stream)
		{
			// TODO: Review this as buffer may be too small

			using var ms = new MemoryStream();
			var buffer = ArrayPool<byte>.Shared.Rent(1024);
			var total = 0;
			int read;
			while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
			{
				ms.Write(buffer, 0, read);
				total += read;
			}
			var span = ms.TryGetBuffer(out var segment)
				? new ReadOnlyMemory<byte>(segment.Array, segment.Offset, total).Span
				: new ReadOnlyMemory<byte>(ms.ToArray()).Span;

			return span.Length > 0 ? JsonSerializer.Deserialize<T>(span, Options) : default;

			////if (stream.Length == 0) // throws on some responses
			////	return default;
			//using var reader = new StreamReader(stream);


			//// TODO: Remove - Just for testing
			////return default;

			//try
			//{
			//	return JsonSerializer.Deserialize<T>(reader.ReadToEnd(), Options);
			//}
			//catch (JsonException ex) when (ex.Message.StartsWith("The input does not contain any JSON tokens. Expected the input to start with a valid JSON token, when isFinalBlock is true."))
			//{
			//	return default;
			//}
		}

		public override object Deserialize(Type type, Stream stream) =>
			throw new NotImplementedException();

		// TODO - Return ValueTask?
		public override Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default) => JsonSerializer.DeserializeAsync<T>(stream, Options, cancellationToken).AsTask();

		public override Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default) =>
			JsonSerializer.DeserializeAsync(stream, type, Options, cancellationToken).AsTask();

		// TODO - This is not ideal as we allocate a large string - No stream based sync overload - View GitHub for better solutions
		public override void Serialize<T>(T data, Stream writableStream,
			SerializationFormatting formatting = SerializationFormatting.None)
		{
			if (data is IStreamSerializable streamSerializable)
			{
				streamSerializable.Serialize(writableStream, _settings, formatting);
				return;
			}

			var json = JsonSerializer.Serialize(data, Options);
			using var writer = new StreamWriter(writableStream, Encoding, 4096, true);
			writer.Write(json);
		}

		public override Task SerializeAsync<T>(T data, Stream stream,
			SerializationFormatting formatting = SerializationFormatting.None,
			CancellationToken cancellationToken = default
		)
		{
			if (data is IStreamSerializable streamSerializable)
			{
				return streamSerializable.SerializeAsync(stream, _settings, formatting);
			}

			return JsonSerializer.SerializeAsync(stream, data, Options, cancellationToken);
		}
	}

	//public class SystemTextJsonSerializer : IElasticsearchSerializer
	//{
	//	public static readonly SystemTextJsonSerializer Instance = new SystemTextJsonSerializer();

	//	private readonly Lazy<JsonSerializerOptions> _indented;
	//	private readonly Lazy<JsonSerializerOptions> _none;

	//	public IReadOnlyCollection<JsonConverter> AdditionalConverters { get; }
	//	private IList<JsonConverter> BakedInConverters { get; } = new List<JsonConverter>
	//	{
	//		{ new ExceptionConverter() },
	//		{ new DynamicDictionaryConverter() }
	//	};

	//	public SystemTextJsonSerializer() : this(null) { }

	//	public SystemTextJsonSerializer(IEnumerable<JsonConverter> converters)
	//	{
	//		AdditionalConverters = converters != null
	//			? new ReadOnlyCollection<JsonConverter>(converters.ToList())
	//			: EmptyReadOnly<JsonConverter>.Collection;
	//		_indented = new Lazy<JsonSerializerOptions>(() => CreateSerializerOptions(Indented));
	//		_none = new Lazy<JsonSerializerOptions>(() => CreateSerializerOptions(None));
	//	}

	//	/// <summary>
	//	/// Creates <see cref="JsonSerializerOptions"/> used for serialization.
	//	/// Override on a derived serializer to change serialization.
	//	/// </summary>
	//	protected virtual JsonSerializerOptions CreateSerializerOptions(SerializationFormatting formatting)
	//	{
	//		var options = new JsonSerializerOptions
	//		{
	//			IgnoreNullValues = true,
	//			WriteIndented = formatting == Indented,
	//		};
	//		foreach (var converter in BakedInConverters)
	//			options.Converters.Add(converter);
	//		foreach (var converter in AdditionalConverters)
	//			options.Converters.Add(converter);

	//		return options;

	//	}

	//	private static bool TryReturnDefault<T>(Stream stream, out T deserialize)
	//	{
	//		deserialize = default;
	//		return stream == null || stream == Stream.Null || (stream.CanSeek && stream.Length == 0);
	//	}

	//	private static MemoryStream ToMemoryStream(Stream stream)
	//	{
	//		if (stream is MemoryStream m)
	//			return m;
	//		var length = stream.CanSeek ? stream.Length : (long?)null;
	//		var wrapped = length.HasValue ? new MemoryStream(new byte[length.Value]) : new MemoryStream();
	//		stream.CopyTo(wrapped);
	//		return wrapped;
	//	}

	//	private static ReadOnlySpan<byte> ToReadOnlySpan(Stream stream)
	//	{
	//		using var m = ToMemoryStream(stream);

	//		if (m.TryGetBuffer(out var segment))
	//			return segment;

	//		var a = m.ToArray();
	//		return new ReadOnlySpan<byte>(a).Slice(0, a.Length);
	//	}

	//	private JsonSerializerOptions GetFormatting(SerializationFormatting formatting) => formatting == None ? _none.Value : _indented.Value;

	//	public object Deserialize(Type type, Stream stream)
	//	{
	//		if (TryReturnDefault(stream, out object deserialize))
	//			return deserialize;

	//		var buffered = ToReadOnlySpan(stream);
	//		return JsonSerializer.Deserialize(buffered, type, _none.Value);
	//	}

	//	public T Deserialize<T>(Stream stream)
	//	{
	//		if (TryReturnDefault(stream, out T deserialize))
	//			return deserialize;

	//		var buffered = ToReadOnlySpan(stream);
	//		return JsonSerializer.Deserialize<T>(buffered, _none.Value);
	//	}

	//	public void Serialize<T>(T data, Stream stream, SerializationFormatting formatting = None)
	//	{
	//		using var writer = new Utf8JsonWriter(stream);
	//		if (data == null)
	//			JsonSerializer.Serialize(writer, null, typeof(object), GetFormatting(formatting));
	//		//TODO validate if we can avoid boxing by checking if data is typeof(object)
	//		else
	//			JsonSerializer.Serialize(writer, data, data.GetType(), GetFormatting(formatting));
	//	}

	//	public async Task SerializeAsync<T>(T data, Stream stream, SerializationFormatting formatting = None,
	//		CancellationToken cancellationToken = default
	//	)
	//	{
	//		if (data == null)
	//			await JsonSerializer.SerializeAsync(stream, null, typeof(object), GetFormatting(formatting), cancellationToken).ConfigureAwait(false);
	//		else
	//			await JsonSerializer.SerializeAsync(stream, data, data.GetType(), GetFormatting(formatting), cancellationToken).ConfigureAwait(false);
	//	}

	//	//TODO return ValueTask, breaking change? probably 8.0
	//	public Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default)
	//	{
	//		if (TryReturnDefault(stream, out object deserialize))
	//			return Task.FromResult(deserialize);

	//		return JsonSerializer.DeserializeAsync(stream, type, _none.Value, cancellationToken).AsTask();
	//	}

	//	public Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default)
	//	{
	//		if (TryReturnDefault(stream, out T deserialize))
	//			return Task.FromResult(deserialize);

	//		return JsonSerializer.DeserializeAsync<T>(stream, _none.Value, cancellationToken).AsTask();
	//	}
	//}

	internal class ThrowHelper
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void ThrowJsonException(string? message = null) => throw new JsonException(message);
	}

	//// TODO: Generate these
	//public class UnassignedInformationReasonConverter : JsonConverter<UnassignedInformationReason>
	//{
	//	public override UnassignedInformationReason Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	//	{
	//		var enumString = reader.GetString();

	//		switch (enumString)
	//		{
	//			case "REROUTE_CANCELLED":
	//				return UnassignedInformationReason.RerouteCancelled;
	//			case "REPLICA_ADDED":
	//				return UnassignedInformationReason.ReplicaAdded;
	//			case "INDEX_CREATED":
	//				return UnassignedInformationReason.IndexCreated;
	//		}

	//		ThrowHelper.ThrowJsonException($"An unknown value for the enum {nameof(UnassignedInformationReason)} was found in the JSON.");

	//		return default;
	//	}

	//	public override void Write(Utf8JsonWriter writer, UnassignedInformationReason value, JsonSerializerOptions options)
	//	{
	//		switch (value)
	//		{
	//			case UnassignedInformationReason.RerouteCancelled:
	//				writer.WriteStringValue("REROUTE_CANCELLED");
	//				return;
	//			case UnassignedInformationReason.ReplicaAdded:
	//				writer.WriteStringValue("REPLICA_ADDED");
	//				return;
	//		}

	//		writer.WriteNullValue();
	//	}
	//}
}
