// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Aggregations;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

internal class CustomizedNamingPolicy : JsonNamingPolicy
{
	private readonly Func<string, string> _namingAction;

	public CustomizedNamingPolicy(Func<string, string> namingAction) => _namingAction = namingAction;

	public override string ConvertName(string name) => _namingAction(name);
}

/// <summary>
/// The built in internal serializer that the high level client Elastic.Clients.Elasticsearch uses.
/// </summary>
internal class DefaultRequestResponseSerializer : SystemTextJsonSourceSerializer
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
					new SourceConverterFactory(settings),
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
					new JoinFieldConverter(settings),
					new CustomJsonWriterConverterFactory(settings),
					new RoutingConverter(settings),
					new SelfSerializableConverterFactory(settings),
					new SelfDeserializableConverterFactory(settings),
					new SelfTwoWaySerializableConverterFactory(settings),
					new IndicesJsonConverter(settings),
					new DictionaryConverter(settings),
					new PropertyNameConverter(settings),
					new IsADictionaryConverter(),
					new UnionConverter()
				},
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};

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
