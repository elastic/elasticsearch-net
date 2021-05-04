// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Elastic.Transport;
using Nest.Utf8Json;

namespace Nest
{
	public class Inferrer
	{
		private readonly IConnectionSettingsValues _connectionSettings;

		public Inferrer(IConnectionSettingsValues connectionSettings)
		{
			connectionSettings.ThrowIfNull(nameof(connectionSettings));
			_connectionSettings = connectionSettings;
			IdResolver = new IdResolver(connectionSettings);
			IndexNameResolver = new IndexNameResolver(connectionSettings);
			RelationNameResolver = new RelationNameResolver(connectionSettings);
			FieldResolver = new FieldResolver(connectionSettings);
			RoutingResolver = new RoutingResolver(connectionSettings, IdResolver);

			CreateMultiHitDelegates =
				new ConcurrentDictionary<Type,
					Action<MultiGetResponseFormatter.MultiHitTuple, IJsonFormatterResolver, ICollection<IMultiGetHit<object>>>>();
			CreateSearchResponseDelegates =
				new ConcurrentDictionary<Type,
					Action<MultiSearchResponseFormatter.SearchHitTuple, IJsonFormatterResolver, IDictionary<string, IResponse>>>();
		}

		internal ConcurrentDictionary<Type, Action<MultiGetResponseFormatter.MultiHitTuple, IJsonFormatterResolver, ICollection<IMultiGetHit<object>>>
			>
			CreateMultiHitDelegates { get; }

		internal ConcurrentDictionary<Type,
				Action<MultiSearchResponseFormatter.SearchHitTuple, IJsonFormatterResolver, IDictionary<string, IResponse>>>
			CreateSearchResponseDelegates { get; }

		private FieldResolver FieldResolver { get; }
		private IdResolver IdResolver { get; }
		private IndexNameResolver IndexNameResolver { get; }
		private RelationNameResolver RelationNameResolver { get; }
		private RoutingResolver RoutingResolver { get; }

		public string Resolve(IUrlParameter urlParameter) => urlParameter.GetString(_connectionSettings);

		public string Field(Field field) => FieldResolver.Resolve(field);

		public string PropertyName(PropertyName property) => FieldResolver.Resolve(property);

		public string IndexName<T>() where T : class => IndexNameResolver.Resolve<T>();

		public string IndexName(IndexName index) => IndexNameResolver.Resolve(index);

		public string Id<T>(T instance) where T : class => IdResolver.Resolve(instance);

		public string Id(Type type, object instance) => IdResolver.Resolve(type, instance);

		public string RelationName<T>() where T : class => RelationNameResolver.Resolve<T>();

		public string RelationName(RelationName type) => RelationNameResolver.Resolve(type);

		public string Routing<T>(T document) => RoutingResolver.Resolve(document);

		public string Routing(Type type, object instance) => RoutingResolver.Resolve(type, instance);
	}
}
