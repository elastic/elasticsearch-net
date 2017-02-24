using System;
using System.Collections.Concurrent;

namespace Nest_5_2_0
{
	public class TypeNameResolver
	{
		private readonly IConnectionSettingsValues _connectionSettings;
		private readonly ConcurrentDictionary<Type, string> TypeNames = new ConcurrentDictionary<Type, string>();

		public TypeNameResolver(IConnectionSettingsValues connectionSettings)
		{
			connectionSettings.ThrowIfNull(nameof(connectionSettings));
			this._connectionSettings = connectionSettings;
		}

		public string Resolve<T>() where T : class => this.Resolve(typeof(T));

		public string Resolve(TypeName t) => t?.Name ?? this.ResolveType(t?.Type);

		private string ResolveType(Type type)
		{
			if (type == null) return null;
			string typeName;

			if (TypeNames.TryGetValue(type, out typeName))
				return typeName;

			if (_connectionSettings.DefaultTypeNames.TryGetValue(type, out typeName))
			{
				TypeNames.TryAdd(type, typeName);
				return typeName;
			}

			var att = ElasticsearchTypeAttribute.From(type);
			if (att != null && !att.Name.IsNullOrEmpty())
				typeName = att.Name;
			else
				typeName = _connectionSettings.DefaultTypeNameInferrer(type);

			TypeNames.TryAdd(type, typeName);
			return typeName;
		}

	}
}
