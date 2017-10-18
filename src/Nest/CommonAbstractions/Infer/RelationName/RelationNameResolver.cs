using System;
using System.Collections.Concurrent;

namespace Nest
{
	public class RelationNameResolver
	{
		private readonly IConnectionSettingsValues _connectionSettings;
		private readonly ConcurrentDictionary<Type, string> RelationNames = new ConcurrentDictionary<Type, string>();

		public RelationNameResolver(IConnectionSettingsValues connectionSettings)
		{
			connectionSettings.ThrowIfNull(nameof(connectionSettings));
			this._connectionSettings = connectionSettings;
		}

		public string Resolve<T>() where T : class => this.Resolve(typeof(T));

		public string Resolve(RelationName t) => t?.Name ?? this.ResolveType(t?.Type);

		private string ResolveType(Type type)
		{
			if (type == null) return null;
			string typeName;

			if (RelationNames.TryGetValue(type, out typeName))
				return typeName;

			if (_connectionSettings.DefaultRelationNames.TryGetValue(type, out typeName))
			{
				RelationNames.TryAdd(type, typeName);
				return typeName;
			}

			var att = ElasticsearchTypeAttribute.From(type);
			if (att != null && !att.Name.IsNullOrEmpty())
				typeName = att.Name;
			else
				typeName = _connectionSettings.DefaultTypeNameInferrer(type);

			RelationNames.TryAdd(type, typeName);
			return typeName;
		}

	}
}
