using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Nest
{
	public class Inferrer
	{
		private readonly IConnectionSettingsValues _connectionSettings;
		private IdResolver IdResolver { get; }
		private IndexNameResolver IndexNameResolver { get; }
		private TypeNameResolver TypeNameResolver { get; }
		private RelationNameResolver RelationNameResolver { get; }
		private FieldResolver FieldResolver { get; }
		private RoutingResolver RoutingResolver { get; }

		internal ConcurrentDictionary<Type, JsonContract> Contracts { get; }
		internal ConcurrentDictionary<Type, Action<MultiGetHitJsonConverter.MultiHitTuple, JsonSerializer, ICollection<IMultiGetHit<object>>>> CreateMultiHitDelegates { get; }
		internal ConcurrentDictionary<Type, Action<MultiSearchResponseJsonConverter.SearchHitTuple, JsonSerializer, IDictionary<string, object>>> CreateSearchResponseDelegates { get; }

		public Inferrer(IConnectionSettingsValues connectionSettings)
		{
			connectionSettings.ThrowIfNull(nameof(connectionSettings));
			this._connectionSettings = connectionSettings;
			this.IdResolver = new IdResolver(connectionSettings);
			this.IndexNameResolver = new IndexNameResolver(connectionSettings);
			this.TypeNameResolver = new TypeNameResolver(connectionSettings);
			this.RelationNameResolver = new RelationNameResolver(connectionSettings);
			this.FieldResolver = new FieldResolver(connectionSettings);
			this.RoutingResolver = new RoutingResolver(connectionSettings, this.IdResolver);

			this.Contracts = new ConcurrentDictionary<Type, JsonContract>();
			this.CreateMultiHitDelegates = new ConcurrentDictionary<Type, Action<MultiGetHitJsonConverter.MultiHitTuple, JsonSerializer, ICollection<IMultiGetHit<object>>>>();
			this.CreateSearchResponseDelegates = new ConcurrentDictionary<Type, Action<MultiSearchResponseJsonConverter.SearchHitTuple, JsonSerializer, IDictionary<string, object>>>();
		}

		public string Resolve(IUrlParameter urlParameter) => urlParameter.GetString(this._connectionSettings);

		public string Field(Field field) => this.FieldResolver.Resolve(field);

		public string PropertyName(PropertyName property) => this.FieldResolver.Resolve(property);

		public string IndexName<T>() where T : class => this.IndexNameResolver.Resolve<T>();

		public string IndexName(IndexName index) => this.IndexNameResolver.Resolve(index);

		public string Id<T>(T instance) where T : class => this.IdResolver.Resolve(instance);

		public string Id(Type type, object instance) => this.IdResolver.Resolve(type, instance);

		public string TypeName<T>() where T : class => this.TypeNameResolver.Resolve<T>();

		public string TypeName(TypeName type) => this.TypeNameResolver.Resolve(type);

		public string RelationName<T>() where T : class => this.RelationNameResolver.Resolve<T>();

		public string RelationName(RelationName type) => this.RelationNameResolver.Resolve(type);

		public string Routing<T>(T document) => this.RoutingResolver.Resolve(document);
		
		public string Routing(Type type, object instance) => this.RoutingResolver.Resolve(type, instance);
	}
}
