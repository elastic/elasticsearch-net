// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.Serialization;
#else
namespace Elastic.Clients.Elasticsearch.Serialization;
#endif

/// <summary>
/// The built in internal serializer that the <see cref="ElasticsearchClient"/> uses to serialize
/// built in types.
/// </summary>
internal class DefaultRequestResponseSerializer : SystemTextJsonSerializer
{
	private readonly JsonSerializerOptions _jsonSerializerOptions;

	public DefaultRequestResponseSerializer(IElasticsearchClientSettings settings) : base(settings)
	{
		_jsonSerializerOptions = new JsonSerializerOptions
		{
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
			IncludeFields = true,
			Converters =
				{
					new KeyValuePairConverterFactory(settings),
					new ObjectToInferredTypesConverter(),
					new SourceConverterFactory(settings),
					new CustomJsonWriterConverterFactory(settings),
					new SelfSerializableConverterFactory(settings),
					new SelfDeserializableConverterFactory(settings),
					new SelfTwoWaySerializableConverterFactory(settings),
					new FieldValuesConverter(), // explicitly registered before IsADictionaryConverterFactory as we want this specialised converter to match
					new IsADictionaryConverterFactory(),
					new ResponseItemConverterFactory(),
					new DictionaryResponseConverterFactory(settings),
					new UnionConverter()
				},
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.AllowNamedFloatingPointLiterals
		};

		Initialize();
	}

	public override void Serialize<T>(T data, Stream writableStream,
		SerializationFormatting formatting = SerializationFormatting.None)
	{
		if (data is IStreamSerializable streamSerializable)
		{
			streamSerializable.Serialize(writableStream, Settings, SerializationFormatting.None);
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
			return streamSerializable.SerializeAsync(stream, Settings, SerializationFormatting.None);
		}

		return base.SerializeAsync(data, stream, formatting, cancellationToken);
	}

	protected override JsonSerializerOptions CreateJsonSerializerOptions() => _jsonSerializerOptions;
}
