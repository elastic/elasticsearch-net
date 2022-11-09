// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

public sealed class Inferrer
{
	private readonly IElasticsearchClientSettings _elasticsearchClientSettings;

	public Inferrer(IElasticsearchClientSettings elasticsearchClientSettings)
	{
		elasticsearchClientSettings.ThrowIfNull(nameof(elasticsearchClientSettings));
		_elasticsearchClientSettings = elasticsearchClientSettings;
		IdResolver = new IdResolver(elasticsearchClientSettings);
		IndexNameResolver = new IndexNameResolver(elasticsearchClientSettings);
		RelationNameResolver = new RelationNameResolver(elasticsearchClientSettings);
		FieldResolver = new FieldResolver(elasticsearchClientSettings);
		RoutingResolver = new RoutingResolver(elasticsearchClientSettings, IdResolver);

		//CreateMultiHitDelegates =
		//	new ConcurrentDictionary<Type,
		//		Action<MultiGetResponseFormatter.MultiHitTuple, IJsonFormatterResolver, ICollection<IMultiGetHit<object>>>>();
		//CreateSearchResponseDelegates =
		//	new ConcurrentDictionary<Type,
		//		Action<MultiSearchResponseFormatter.SearchHitTuple, IJsonFormatterResolver, IDictionary<string, ElasticsearchResponse>>>();
	}

	//internal ConcurrentDictionary<Type, Action<MultiGetResponseFormatter.MultiHitTuple, IJsonFormatterResolver, ICollection<IMultiGetHit<object>>>
	//	>
	//	CreateMultiHitDelegates { get; }

	//internal ConcurrentDictionary<Type,
	//		Action<MultiSearchResponseFormatter.SearchHitTuple, IJsonFormatterResolver, IDictionary<string, ElasticsearchResponse>>>
	//	CreateSearchResponseDelegates { get; }

	private FieldResolver FieldResolver { get; }
	private IdResolver IdResolver { get; }
	private IndexNameResolver IndexNameResolver { get; }
	private RelationNameResolver RelationNameResolver { get; }
	private RoutingResolver RoutingResolver { get; }

	public string Resolve(IUrlParameter urlParameter) => urlParameter.GetString(_elasticsearchClientSettings);

	public string Field(Field field) => FieldResolver.Resolve(field);

	public string PropertyName(PropertyName property) => FieldResolver.Resolve(property);

	public string IndexName<T>() => IndexNameResolver.Resolve<T>();

	public string IndexName(IndexName index) => IndexNameResolver.Resolve(index);

	public string Id<T>(T instance) => IdResolver.Resolve(instance);

	public string Id(Type type, object instance) => IdResolver.Resolve(type, instance);

	public string RelationName<T>() => RelationNameResolver.Resolve<T>();

	public string RelationName(RelationName type) => RelationNameResolver.Resolve(type);

	public string Routing<T>(T document) => RoutingResolver.Resolve(document);

	public string Routing(Type type, object instance) => RoutingResolver.Resolve(type, instance);
}
