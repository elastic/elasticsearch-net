using System;
using Elastic.Transport;
using Elastic.Clients.Elasticsearch.Core;
using System.Collections.Concurrent;
using System.Reflection;

namespace Elastic.Clients.Elasticsearch
{
	public class Inferrer
	{
		private readonly IElasticsearchClientSettings _elasticsearchClientSettings;

		public Inferrer(IElasticsearchClientSettings elasticsearchClientSettings)
		{
			elasticsearchClientSettings.ThrowIfNull(nameof(elasticsearchClientSettings));
			_elasticsearchClientSettings = elasticsearchClientSettings;
			IdResolver = new IdResolver(elasticsearchClientSettings);
			IndexNameResolver = new IndexNameResolver(elasticsearchClientSettings);
			//RelationNameResolver = new RelationNameResolver(connectionSettings);
			//FieldResolver = new FieldResolver(elasticsearchClientSettings);
			//RoutingResolver = new RoutingResolver(connectionSettings, IdResolver);

			//CreateMultiHitDelegates =
			//	new ConcurrentDictionary<Type,
			//		Action<MultiGetResponseFormatter.MultiHitTuple, IJsonFormatterResolver, ICollection<IMultiGetHit<object>>>>();
			//CreateSearchResponseDelegates =
			//	new ConcurrentDictionary<Type,
			//		Action<MultiSearchResponseFormatter.SearchHitTuple, IJsonFormatterResolver, IDictionary<string, IResponse>>>();
		}

		//internal ConcurrentDictionary<Type, Action<MultiGetResponseFormatter.MultiHitTuple, IJsonFormatterResolver, ICollection<IMultiGetHit<object>>>
		//	>
		//	CreateMultiHitDelegates { get; }

		//internal ConcurrentDictionary<Type,
		//		Action<MultiSearchResponseFormatter.SearchHitTuple, IJsonFormatterResolver, IDictionary<string, IResponse>>>
		//	CreateSearchResponseDelegates { get; }

		//private FieldResolver FieldResolver { get; }
		private IdResolver IdResolver { get; }

		private IndexNameResolver IndexNameResolver { get; }
		//private RelationNameResolver RelationNameResolver { get; }
		//private RoutingResolver RoutingResolver { get; }

		public string Resolve(IUrlParameter urlParameter) => urlParameter.GetString(_elasticsearchClientSettings);

		//public string Field(Field field) => FieldResolver.Resolve(field);

		//public string PropertyName(PropertyName property) => FieldResolver.Resolve(property);

		//public string IndexName<T>() where T : class => IndexNameResolver.Resolve<T>();

		public string IndexName(IndexName index) => IndexNameResolver.Resolve(index);

		public string Id<T>(T instance) where T : class => IdResolver.Resolve(instance);

		public string Id(Type type, object instance) => IdResolver.Resolve(type, instance);

		//public string RelationName<T>() where T : class => RelationNameResolver.Resolve<T>();

		//public string RelationName(RelationName type) => RelationNameResolver.Resolve(type);

		//public string Routing<T>(T document) => RoutingResolver.Resolve(document);

		//public string Routing(Type type, object instance) => RoutingResolver.Resolve(type, instance);
	}

	public class IndexNameResolver
	{
		private readonly IElasticsearchClientSettings _connectionSettings;

		public IndexNameResolver(IElasticsearchClientSettings connectionSettings)
		{
			connectionSettings.ThrowIfNull(nameof(connectionSettings));
			_connectionSettings = connectionSettings;
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
			var indexName = _connectionSettings.DefaultIndex;
			var defaultIndices = _connectionSettings.DefaultIndices;
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
}
