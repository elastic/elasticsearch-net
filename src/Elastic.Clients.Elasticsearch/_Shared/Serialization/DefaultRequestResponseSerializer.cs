// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
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

#if !NET8_0_OR_GREATER
	private readonly object _lock = new();
#endif

	public DefaultRequestResponseSerializer(IElasticsearchClientSettings settings) :
		base(new DefaultRequestResponseSerializerOptionsProvider(settings))
	{
		_settings = settings;

		LinkSettings(settings);
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
			return streamSerializable.SerializeAsync(stream, _settings, SerializationFormatting.None);

		return base.SerializeAsync(data, stream, formatting, cancellationToken);
	}

	/// <summary>
	/// Links the <see cref="JsonSerializerOptions"/> of this serializer to the given <see cref="IElasticsearchClientSettings"/>.
	/// </summary>
	private void LinkSettings(IElasticsearchClientSettings settings)
	{
		var options = GetJsonSerializerOptions(SerializationFormatting.None);
		var indentedOptions = GetJsonSerializerOptions(SerializationFormatting.Indented);

#if NET8_0_OR_GREATER
		ElasticsearchClient.SettingsTable.TryAdd(options, settings);
		ElasticsearchClient.SettingsTable.TryAdd(indentedOptions, settings);
#else
		lock (_lock)
		{
			if (!ElasticsearchClient.SettingsTable.TryGetValue(options, out _))
			{
				ElasticsearchClient.SettingsTable.Add(options, settings);
			}

			if (!ElasticsearchClient.SettingsTable.TryGetValue(indentedOptions, out _))
			{
				ElasticsearchClient.SettingsTable.Add(indentedOptions, settings);
			}
		}
#endif
	}
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
		new KeyValuePairConverterFactory(settings),
		new ObjectToInferredTypesConverter(),
		new SourceConverterFactory(settings),
		new SelfSerializableConverterFactory(settings),
		new SelfDeserializableConverterFactory(settings),
		new SelfTwoWaySerializableConverterFactory(settings),
		// Explicitly registered before `IsADictionaryConverterFactory` as we want this specialised converter to match
		new FieldValuesConverter(),
		new IsADictionaryConverterFactory(),
		new ResponseItemConverterFactory(),
		new DictionaryResponseConverterFactory(settings),
		new UnionConverter(),
		// TODO: Remove after https://github.com/elastic/elasticsearch-specification/issues/2238 is implemented
		new StringifiedLongConverter(),
		new StringifiedIntegerConverter(),
		new StringifiedBoolConverter()
	];

	private static void MutateOptions(JsonSerializerOptions options)
	{
		options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
		options.IncludeFields = true;
		options.NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.AllowNamedFloatingPointLiterals;
		options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
	}
}
