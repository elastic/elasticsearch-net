//// Licensed to Elasticsearch B.V under one or more agreements.
//// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
//// See the LICENSE file in the project root for more information

//using System;
//using System.Collections.Concurrent;

//namespace Elastic.Clients.Elasticsearch
//{
//	public class RelationNameResolver
//	{
//		private readonly ITransportClientSettingsValues _transportClientSettings;
//		private readonly ConcurrentDictionary<Type, string> _relationNames = new ConcurrentDictionary<Type, string>();

//		public RelationNameResolver(ITransportClientSettingsValues connectionSettings)
//		{
//			connectionSettings.ThrowIfNull(nameof(connectionSettings));
//			_transportClientSettings = connectionSettings;
//		}

//		public string Resolve<T>() where T : class => Resolve(typeof(T));

//		public string Resolve(RelationName t) => t?.Name ?? ResolveType(t?.Type);

//		private string ResolveType(Type type)
//		{
//			if (type == null) return null;

//			string typeName;

//			if (_relationNames.TryGetValue(type, out typeName))
//				return typeName;

//			if (_transportClientSettings.DefaultRelationNames.TryGetValue(type, out typeName))
//			{
//				_relationNames.TryAdd(type, typeName);
//				return typeName;
//			}

//			var att = ElasticsearchTypeAttribute.From(type);
//			if (att != null && !att.RelationName.IsNullOrEmpty())
//				typeName = att.RelationName;
//			else
//				typeName = type.Name.ToLowerInvariant();

//			_relationNames.TryAdd(type, typeName);
//			return typeName;
//		}
//	}
//}
