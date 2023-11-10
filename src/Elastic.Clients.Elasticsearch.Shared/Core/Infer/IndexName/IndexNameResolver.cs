// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless;
#else
namespace Elastic.Clients.Elasticsearch;
#endif

internal class IndexNameResolver
{
	private readonly IElasticsearchClientSettings _transportClientSettings;

	public IndexNameResolver(IElasticsearchClientSettings connectionSettings)
	{
		connectionSettings.ThrowIfNull(nameof(connectionSettings));
		_transportClientSettings = connectionSettings;
	}

	public string Resolve<T>() => Resolve(typeof(T));

	public string Resolve(IndexName i)
	{
		if (string.IsNullOrEmpty(i?.Name))
			return PrefixClusterName(i, Resolve(i?.Type));

		ValidateIndexName(i.Name);
		return PrefixClusterName(i, i.Name);
	}

	public string Resolve(Type type)
	{
		var indexName = _transportClientSettings.DefaultIndex;
		var defaultIndices = _transportClientSettings.DefaultIndices;
		if (defaultIndices != null && type != null)
		{
			if (defaultIndices.TryGetValue(type, out var value) && !string.IsNullOrEmpty(value))
				indexName = value;
		}
		ValidateIndexName(indexName);
		return indexName;
	}

	private static string PrefixClusterName(IndexName i, string name) => i.Cluster.IsNullOrEmpty() ? name : $"{i.Cluster}:{name}";

	// ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
	private static void ValidateIndexName(string indexName)
	{
		if (string.IsNullOrWhiteSpace(indexName))
			throw new ArgumentException(
				"Index name is null for the given type and no default index is set. "
				+ "Map an index name using ConnectionSettings.DefaultMappingFor<TDocument>() "
				+ "or set a default index using ConnectionSettings.DefaultIndex()."
			);
	}
}
