using System;

namespace Nest
{
	public class IndexNameResolver
	{
		private readonly IElasticsearchClientSettings _elasticsearchClientSettings;

		public IndexNameResolver(IElasticsearchClientSettings elasticsearchClientSettings)
		{
			elasticsearchClientSettings.ThrowIfNull(nameof(elasticsearchClientSettings));
			_elasticsearchClientSettings = elasticsearchClientSettings;
		}

		public string Resolve<T>() where T : class => Resolve(typeof(T));

		public string Resolve(IndexName i)
		{
			if (string.IsNullOrEmpty(i?.Name))
				return PrefixClusterName(i, Resolve(i?.Type));

			ValidateIndexName(i.Name);
			return PrefixClusterName(i, i.Name);
		}

		public string Resolve(Type type)
		{
			var indexName = _elasticsearchClientSettings.DefaultIndex;
			var defaultIndices = _elasticsearchClientSettings.DefaultIndices;
			if (defaultIndices != null && type != null)
			{
				if (defaultIndices.TryGetValue(type, out var value) && !string.IsNullOrEmpty(value))
					indexName = value;
			}

			ValidateIndexName(indexName);
			return indexName;
		}

		private static string PrefixClusterName(IndexName i, string name) =>
			i.Cluster.IsNullOrEmpty() ? name : $"{i.Cluster}:{name}";

		// ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
		private static void ValidateIndexName(string indexName)
		{
			if (string.IsNullOrWhiteSpace(indexName))
			{
				throw new ArgumentException(
					"Index name is null for the given type and no default index is set. "
					+ "Map an index name using ConnectionSettings.DefaultMappingFor<TDocument>() "
					+ "or set a default index using ConnectionSettings.DefaultIndex()."
				);
			}
		}
	}
}
