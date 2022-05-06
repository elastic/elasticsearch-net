// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

internal class DefaultSourceSerializer : SystemTextJsonSourceSerializer
{
	public DefaultSourceSerializer(IElasticsearchClientSettings settings, JsonSerializerOptions? options = null) => Options =
		options ?? new JsonSerializerOptions
		{
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
			Converters =
			{
					new JsonStringEnumConverter(),
					new DictionaryConverter(settings),
					new UnionConverter(),
					new IdConverter(settings),
					new RelationNameConverter(settings),
					new RoutingConverter(settings),
					new PropertyNameConverter(settings),
					new JoinFieldConverter(settings),
					new LazyDocumentConverter(settings),
			},
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};
}
