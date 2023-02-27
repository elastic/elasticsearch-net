// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Aggregations;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// The built in internal serializer that the high level client Elastic.Clients.Elasticsearch uses.
/// </summary>
internal class DefaultRequestResponseSerializer : SystemTextJsonSerializer
{
	private readonly IElasticsearchClientSettings _settings;

	public DefaultRequestResponseSerializer(IElasticsearchClientSettings settings)
	{
		Options = new JsonSerializerOptions
		{	
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
			IncludeFields = true,
			Converters =
				{
					new KeyValuePairConverterFactory(settings),
					new SourceConverterFactory(settings),
					new CalendarIntervalConverter(),
					new IndexNameConverter(settings),
					new ObjectToInferredTypesConverter(),
					new IdConverter(settings),
					new FieldConverter(settings),
					new FieldValuesConverter(settings),
					new LazyJsonConverter(settings),
					new RelationNameConverter(settings),
					new JoinFieldConverter(settings),
					new CustomJsonWriterConverterFactory(settings),
					new RoutingConverter(settings),
					new SelfSerializableConverterFactory(settings),
					new SelfDeserializableConverterFactory(settings),
					new SelfTwoWaySerializableConverterFactory(settings),
					new IndicesJsonConverter(settings),
					new IdsConverter(settings),
					new IsADictionaryConverterFactory(),
					//new ResolvableReadonlyDictionaryConverterFactory(settings),
					new ResponseItemConverterFactory(),
					new UnionConverter(),
					new ExtraSerializationData(settings),
					new DictionaryResponseConverterFactory(settings)
				},
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.AllowNamedFloatingPointLiterals
		};

		ElasticsearchClient.SettingsTable.Add(Options, settings);

		_settings = settings;
	}

	public override void Serialize<T>(T data, Stream writableStream,
		SerializationFormatting formatting = SerializationFormatting.None)
	{
		if (data is IStreamSerializable streamSerializable)
		{
			streamSerializable.Serialize(writableStream, _settings, formatting);
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
			return streamSerializable.SerializeAsync(stream, _settings, formatting);
		}

		return base.SerializeAsync(data, stream, formatting, cancellationToken);
	}
}
