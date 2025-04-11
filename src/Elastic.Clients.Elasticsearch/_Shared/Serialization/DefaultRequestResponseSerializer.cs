// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// The built-in internal serializer that the <see cref="ElasticsearchClient"/> uses to serialize built in types.
/// </summary>
internal sealed class DefaultRequestResponseSerializer : SystemTextJsonSerializer
{
	private readonly IElasticsearchClientSettings _settings;

	public DefaultRequestResponseSerializer(IElasticsearchClientSettings settings) :
		base(new DefaultRequestResponseSerializerOptionsProvider(settings))
	{
		_settings = settings;
	}

	public override void Serialize<T>(T data, Stream writableStream,
		SerializationFormatting formatting = SerializationFormatting.None)
	{
		if (data is IStreamSerializable streamSerializable)
		{
			streamSerializable.Serialize(writableStream, _settings, SerializationFormatting.None);
			return;
		}

		base.Serialize(data, writableStream, formatting);
	}

	public override Task SerializeAsync<T>(T data, Stream stream,
		SerializationFormatting formatting = SerializationFormatting.None,
		CancellationToken cancellationToken = default)
	{
		if (data is IStreamSerializable streamSerializable)
		{
			return streamSerializable.SerializeAsync(stream, _settings, SerializationFormatting.None);
		}

		return base.SerializeAsync(data, stream, formatting, cancellationToken);
	}

	public override T Deserialize<T>(Stream stream)
	{
		if (typeof(IStreamSerializable).IsAssignableFrom(typeof(T)))
		{
		}

		return base.Deserialize<T>(stream);
	}

	public override object? Deserialize(Type type, Stream stream)
	{
		if (typeof(IStreamSerializable).IsAssignableFrom(type))
		{
		}

		return base.Deserialize(type, stream);
	}

	public override ValueTask<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = new CancellationToken())
	{
		if (typeof(IStreamSerializable).IsAssignableFrom(typeof(T)))
		{
		}

		return base.DeserializeAsync<T>(stream, cancellationToken);
	}

	public override ValueTask<object?> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = new CancellationToken())
	{
		if (typeof(IStreamSerializable).IsAssignableFrom(type))
		{
		}

		return base.DeserializeAsync(type, stream, cancellationToken);
	}

	protected override bool SupportsFastPath(Type type) => !typeof(IStreamSerializable).IsAssignableFrom(type);
}

/// <summary>
/// The options-provider for the built-in <see cref="DefaultRequestResponseSerializer"/>.
/// </summary>
internal sealed class DefaultRequestResponseSerializerOptionsProvider :
	TransportSerializerOptionsProvider
{
	internal DefaultRequestResponseSerializerOptionsProvider(IElasticsearchClientSettings settings) :
		base(CreateDefaultBuiltInConverters(settings), null, MutateOptions)
	{
	}

	private static IReadOnlyCollection<JsonConverter> CreateDefaultBuiltInConverters(IElasticsearchClientSettings settings) =>
	[
		// For context aware JsonConverter/JsonConverterFactory implementations.
		new ContextProvider<IElasticsearchClientSettings>(settings),

		new ObjectToInferredTypesConverter(),

		// Marker types
		new SourceMarkerConverterFactory(settings),
		new DateTimeMarkerConverter(),
		new DateTimeSecondsMarkerConverter(),
		new DateTimeMillisMarkerConverter(),
		new DateTimeNanosMarkerConverter(),
		new DateTimeSecondsFloatMarkerConverter(),
		new DateTimeMillisFloatMarkerConverter(),
		new TimeSpanSecondsMarkerConverter(),
		new TimeSpanMillisMarkerConverter(),
		new TimeSpanNanosMarkerConverter(),
		new TimeSpanSecondsFloatMarkerConverter(),
		new TimeSpanMillisFloatMarkerConverter(),

		new SingleOrManyFieldsMarkerConverter(),

		new FieldValuesConverter(),

		// TODO: Remove after https://github.com/elastic/elasticsearch-specification/issues/2238 is implemented
		new StringifiedBoolConverter(),
		new StringifiedIntConverter(),
		new StringifiedLongConverter(),
		new StringifiedSingleConverter(),
		new StringifiedDoubleConverter(),
	];

	private static void MutateOptions(JsonSerializerOptions options)
	{
		options.TypeInfoResolver = new DefaultJsonTypeInfoResolver();
		options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
		options.IncludeFields = true;
		options.NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.AllowNamedFloatingPointLiterals;
		options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
	}
}
